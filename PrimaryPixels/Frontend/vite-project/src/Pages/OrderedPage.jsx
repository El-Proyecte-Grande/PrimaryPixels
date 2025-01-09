import { useParams } from "react-router-dom";

export default function OrderedPage() {

    const { orderId } = useParams();
    return (
        <>
            <h1> The order was submitted successfully </h1>
            <h1> ORDER NUMBER: {orderId}</h1>
        </>
    )
}