import Navbar from "../Components/HomePageComponents/Navbar"
import { useParams, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { jwtDecode } from "jwt-decode";
import ProfileSideBar from "../Components/ProfileComponents/ProfileSideBar";
import "./ProfilePage.scss";
import { apiWithAuth } from "../Axios/api"


export default function ProfilePage() {

    const { userId } = useParams();
    const navigate = useNavigate();
    const [page, setPage] = useState("profile");
    const [orders, setOrders] = useState([]);

    // If we are not logged in or if we want to search for other user's cart, it redirect us to the home page.
    useEffect(() => {
        const token = localStorage.getItem("token");
        const decodedToken = jwtDecode(token);
        if (token == null || decodedToken.sub !== userId) {
            navigate('/');
        }

    }, [navigate])

    useEffect(() => {
        async function getOrders() {
            const response = await apiWithAuth.get("/api/Order/User");
            if (response.status == 200) {
                setOrders(response.data);
            }
        }
        getOrders()
    }, [])


    useEffect(() => {
        console.log(orders);
    }, [orders])

    return (
        <>
            <Navbar />
            <ProfileSideBar setPage={setPage} />
            <div className="profile-page">
                {page == "profile" && (
                    <h2> PROFILE </h2>
                )}
                {page == "orders" && (
                    <div className="orders">
                        {orders.map(o => <div key={o.id} className="order">
                            <div className="order-info"> #{o.id} </div>
                        </div>)}
                    </div>
                )}
            </div>
        </>
    )
}

function formatHUF(amount) {
    const amountStr = amount.toString();
    const formattedAmount = amountStr.replace(/\B(?=(\d{3})+(?!\d))/g, '.');
    return `${formattedAmount} HUF`;
}