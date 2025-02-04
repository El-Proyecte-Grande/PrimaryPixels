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


    return (
        <>
            <Navbar />
            <ProfileSideBar setPage={setPage} />
            <div className="profile-page">
                {page == "profile" && (
                    <h2> PROFILE </h2>
                )}
                {page == "orders" && (
                    <table className="orders">
                        <thead className="orders-table-head">
                            <tr className="orders-table-row">
                                <th> ID </th>
                                <th> DATE </th>
                                <th> PRICE </th>
                                <th> CITY </th>
                                <th> ADDRESS </th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody className="orders-table-body">
                            {orders.map((o) =>
                                <tr className="order" key={o.id}>
                                    <td> {o.id} </td>
                                    <td> {o.orderDate} </td>
                                    <td> {formatHUF(o.price)} </td>
                                    <td> {o.city} </td>
                                    <td> {o.address} </td>
                                    <td>
                                        <button className="view-order-button" onClick={() => navigate(`/orderdetails/${o.id}`)}> View </button>
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
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