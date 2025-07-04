import { useParams, useNavigate } from "react-router-dom";
import "./OrderedPage.scss";
import Navbar from "../Components/HomePageComponents/Navbar"
import { useState } from "react";
import BackButton from "../Components/BackButton/BackButton";

export default function OrderedPage() {

    const { orderId } = useParams();
    const navigate = useNavigate();
    const [isLoggedIn, setIsLoggedIn] = useState(() => (
        localStorage.getItem("token") === null ? false : true
    ));

    return (
        <>
            <Navbar isLoggedIn={isLoggedIn} />
            <div className="ordered-page">
                <h2> The order was submitted successfully </h2>
                <h2> ORDER NUMBER: {orderId}</h2>
                <BackButton/>
            </div>
        </>
    )
}