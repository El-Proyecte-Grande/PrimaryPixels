import Navbar from "../Components/HomePageComponents/Navbar"
import { useParams, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { jwtDecode } from "jwt-decode";
import ProfileSideBar from "../Components/ProfileComponents/ProfileSideBar";
import "./ProfilePage.scss";
import { apiWithAuth } from "../Axios/api"
import ResetPassword from "../Components/ProfileComponents/ResetPassword";


export default function ProfilePage() {

    const { userId } = useParams();
    const navigate = useNavigate();
    const [page, setPage] = useState("profile");
    const [orders, setOrders] = useState([]);
    const [userData, setUserData] = useState({});
    const [resetPwd, setResetPwd] = useState(false);
    const [resetPasswordData, setResetPasswordData] = useState({ currentPassword: "", newPassword: "", confirmNewPassword: "" });
    const [errorMessage, setErrorMessage] = useState("");
    const [successMessage, setSuccessMessage] = useState("");

    // If we are not logged in or if we want to search for other user's cart, it redirect us to the home page.
    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token == null) navigate('/');
        const decodedToken = jwtDecode(token);
        if (decodedToken.sub !== userId) {
            navigate('/');
        }
    }, [navigate])


    // After display success message for 4 seconds, it dissappears.

    useEffect(() => {
        setTimeout(() => {
            setSuccessMessage("")
        }, 4000);
    }, [successMessage])

    useEffect(() => {
        setTimeout(() => {
            setErrorMessage("")
        }, 4000);
    }, [errorMessage])

    // request: get current user's previous orders
    useEffect(() => {
        async function getOrders() {
            const response = await apiWithAuth.get("/api/Order/User");
            if (response.status == 200) {
                setOrders(response.data);
            }
        }
        getOrders()
    }, [])

    // request: get basic information about current user
    useEffect(() => {
        async function getUserInfos() {
            const response = await apiWithAuth.get("/api/User");
            if (response.status == 200) {
                setUserData(response.data);
            }
            else {
                console.error("Couldn't fetch user's data!");
            }
        }
        getUserInfos();
    }, [])

    // request: Reset password
    async function resetPassword(e) {
        e.preventDefault();
        if (resetPasswordData.newPassword != resetPasswordData.confirmNewPassword) {
            setErrorMessage("New passwords must match!");
            return;
        }
        if (resetPasswordData.newPassword.length < 8) {
            setErrorMessage("Password length must be at least 8 character!");
            return;
        }
        try {
            const response = await apiWithAuth.patch("/api/User",
                JSON.stringify({ currentPassword: resetPasswordData.currentPassword, newPassword: resetPasswordData.newPassword, userId }),
                {
                    headers: {
                        "Content-Type": "application/json"
                    }
                }
            );
            if (response.status == 200) {
                setResetPwd(false);
                setSuccessMessage("Password change was successful!");
            }
        }
        catch (error) {
            if (error.response && error.response.data) {
                const firstError = Object.values(error.response.data)
                setErrorMessage(firstError || "An error occurred during registration.");
            }
        }
    }

    return (
        <>
            <Navbar />
            <ProfileSideBar setPage={setPage} />
            <div className="profile-page">
                {page == "profile" && (
                    <div className="profile">
                        <img className="profile-page-image" src="../../public/user.png"></img>
                        <p className="user-info"> Email: {userData.email}</p>
                        <p className="user-info"> Nickname: {userData.username}</p>
                        <div className="profile-password">
                            <p className="user-info"> Password: **********</p>
                            <button className="pwd-reset-button" onClick={() => setResetPwd(prev => !prev)}> RESET PASSWOWRD </button>
                        </div>
                    </div>
                )}
                {resetPwd && (
                    <>
                        <ResetPassword setResetPwd={setResetPwd} setResetPasswordData={setResetPasswordData} resetPassword={resetPassword} errorMessage={errorMessage} />
                    </>
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
                {successMessage != "" && (
                    <div className="success-popup">
                        <p className="success-text">Password change was successful!</p>
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