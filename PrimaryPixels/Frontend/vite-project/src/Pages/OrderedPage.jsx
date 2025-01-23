import { useParams } from "react-router-dom";
import "./OrderedPage.scss";
import Navbar from "../Components/HomePageComponents/Navbar"
import { useState } from "react";

export default function OrderedPage() {

    const { orderId } = useParams();
    const [isLoggedIn, setIsLoggedIn] = useState(() => (

        localStorage.getItem("token") === null ? false : true
    ));
    return (<>
        <Navbar isLoggedIn={isLoggedIn} />
        <div className="ordered-page">
            <h1> The order was submitted successfully </h1>
            <h1> ORDER NUMBER: {orderId}</h1>
        </div>
    </>

    )
}