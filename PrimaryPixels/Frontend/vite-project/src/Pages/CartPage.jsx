import { useState, useEffect } from "react"
import { useParams } from "react-router-dom";
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
        <div>
            <div className="left-section">

            </div>
            <div className="right-section">
                <div className="form-div">
                    <label for="name"> Name </label>
                    <input id="name" onChange={e => setOrderInfo(prev => ({ ...prev, name: e.target.value }))} />
                    <label for="city"> City </label>
                    <input id="city" onChange={e => setOrderInfo(prev => ({ ...prev, city: e.target.value }))} />
                    <label for="postcode"> Postcode </label>
                    <input id="postcode" onChange={e => setOrderInfo(prev => ({ ...prev, postcode: e.target.value }))} />
                    <label for="street"> Street </label>
                    <input id="street" onChange={e => setOrderInfo(prev => ({ ...prev, street: e.target.value }))} />
                </div>
                <div className="infos-div">
                    <p className="price"> Total Price: 500 €</p>
                    <button className="order-button"> ORDER </button>
                </div>
            </div>
        </div>
    )
}