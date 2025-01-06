import { useState, useEffect } from "react"
import { useParams } from "react-router-dom";
import './CartPage.scss';
export default function CartPage() {

    const { userId } = useParams();
    const [orderInfo, setOrderInfo] = useState({});
    const [productsInCart, setProductsInCart] = useState([]);

    useEffect(() => {
        async function getProducts() {
            const respone = await fetch(`http://localhost:44319/api/ShoppingCartItems/${userId}`);
            if (response.ok) {
                const data = response.json();
                setProductsInCart(data);
            }
        }
        getProducts();
    }, [])

    return (
        <div className="page-div">
            <table className="left-section">
                <thead>
                    <tr>
                        <th> NAME </th>
                        <th> QUANTITY </th>
                        <th> PRICE </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td> IPHONE 24 </td>
                        <td> 4 </td>
                        <td> 1399000 </td>
                        <td ><button className="add-button"> + </button></td>
                        <td><button className="delete-button"> - </button></td>
                    </tr>
                    <tr>
                        <td> IPHONE 24 </td>
                        <td> 4 </td>
                        <td> 1399000 </td>
                        <td ><button className="add-button"> + </button></td>
                        <td><button className="delete-button"> - </button></td>
                    </tr>
                </tbody>
            </table>
            <div className="right-section">
                <form className="for-div">
                    <input class="fname" type="text" name="name" placeholder="Name" />
                    <input type="text" name="name" placeholder="City" />
                    <input type="text" name="name" placeholder="Postcode" />
                    <input type="text" name="name" placeholder="Address" />
                    <button type="submit" href="/">ORDER</button>
                </form>
                <div className="infos-div">
                    <p className="price"> Total Price: 500 €</p>
                    <button className="order-button"> ORDER </button>
                </div>
            </div>
        </div >
    )
}


