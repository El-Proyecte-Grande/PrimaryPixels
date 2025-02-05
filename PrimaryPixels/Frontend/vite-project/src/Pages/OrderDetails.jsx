import { useEffect, useState } from "react";
import { useParams } from "react-router-dom"
import { apiWithAuth } from "../Axios/api";

export default function OrderDetails() {

    const { orderId } = useParams();
    const [orderDetails, setOrderDetails] = useState([]);

    useEffect(() => {
        async function getOrderDetails() {
            const response = await apiWithAuth.get(`/api/orderDetails/order/${orderId}`)
            const data = await response.data;
            setOrderDetails(data);
        }
        getOrderDetails();
    }, [])

    useEffect(() => {
        console.log(orderDetails);
    }, [orderDetails])

    return (
        <>
            <h1> ORDERDETAILS </h1>
        </>
    )
}