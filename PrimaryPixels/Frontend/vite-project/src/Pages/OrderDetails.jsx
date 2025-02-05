import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom"
import { apiWithAuth } from "../Axios/api";
import "./OrderDetails.scss"

export default function OrderDetails() {

    const { orderId } = useParams();
    const navigate = useNavigate();
    const [orderDetails, setOrderDetails] = useState([]);

    useEffect(() => {
        async function getOrderDetails() {
            const response = await apiWithAuth.get(`/api/orderDetails/order/${orderId}`)
            const data = await response.data;
            setOrderDetails(data);
        }
        getOrderDetails();
    }, [])


    return (
        <>
            <button onClick={() => navigate(-1)} className="back-button"> &#10094; back </button>
            <h2 className="order-number"> ORDER #{orderId}</h2>
            <table className="orders">
                <thead className="orders-details-table-head">
                    <tr className="orders-details">
                        <th> </th>
                        <th> Product Name </th>
                        <th> Quantity </th>
                        <th> Unit Price </th>
                        <th> Total Price </th>
                    </tr>
                </thead>
                <tbody className="orders-details-table-body">
                    {orderDetails.map((od) =>
                        <tr className="order-details" key={od.id}>
                            <td> <img src={od.product.image} className="order-details-image" /> </td>
                            <td> {od.product.name} </td>
                            <td> {od.quantity} </td>
                            <td> {formatHUF(od.unitPrice)} </td>
                            <td> {formatHUF(od.totalPrice)} </td>
                        </tr>

                    )}
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td className="total-price" > Total: {formatHUF(orderDetails.reduce((acc, curr) => acc + curr.totalPrice, 0))}</td>
                    </tr>
                </tbody>
            </table >
        </>
    )
}

function formatHUF(amount) {
    const amountStr = amount.toString();
    const formattedAmount = amountStr.replace(/\B(?=(\d{3})+(?!\d))/g, '.');
    return `${formattedAmount} HUF`;
}