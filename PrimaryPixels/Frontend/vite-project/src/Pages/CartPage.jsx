import { useState, useEffect } from "react"
import { useParams } from "react-router-dom";
import './CartPage.scss';
export default function CartPage() {

    const { userId } = useParams();
    const [orderInfo, setOrderInfo] = useState({});
    const [productsInCart, setProductsInCart] = useState([]);

    useEffect(() => {
        async function getProducts() {
            const response = await fetch(`https://localhost:44319/api/ShoppingCartItem/user/${userId}`);
            if (response.ok) {
                const data = await response.json();
                setProductsInCart(data);
            }
        }
        getProducts();
    }, [])

    useEffect(() => {
        console.log(productsInCart);
    })

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
                    {productsInCart.map(x => <tr>
                        <td> {x.product.name} </td>
                        <td> {x.quantity} </td>
                        <td> {x.totalPrice} </td>
                        <td> <button className="increase-button"> + </button> </td>
                        <td> <button className="decrease-button"> - </button> </td>
                    </tr>)}
                </tbody>
            </table>
            <div className="right-section">
                <form className="for-div">
                    <input className="fname" type="text" name="name" placeholder="Name" />
                    <input type="text" name="name" placeholder="City" />
                    <input type="text" name="name" placeholder="Postcode" />
                    <input type="text" name="name" placeholder="Address" />
                    <button type="submit" href="/">ORDER</button>
                </form>
                <div className="infos-div">
                    <p className="price"> Total Price: {productsInCart.reduce((sum, product) => sum + product.totalPrice, 0)} €</p>
                </div>
            </div>
        </div >
    )
}


