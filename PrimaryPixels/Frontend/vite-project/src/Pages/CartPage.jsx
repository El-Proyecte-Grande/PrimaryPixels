import { useState, useEffect } from "react"
import { useParams, useNavigate } from "react-router-dom";
import './CartPage.scss';
import api from "../Axios/api";
export default function CartPage() {

    const { userId } = useParams();
    const [orderInfo, setOrderInfo] = useState({ firstName: "", lastName: "", city: "", postcode: "", address: "", orderProducts: [] });
    const [productsInCart, setProductsInCart] = useState([]);
    const navigate = useNavigate();


    useEffect(() => {
        document.body.style.background = "linear-gradient(315deg, rgba(60, 132, 206, 1) 38%, rgba(48, 238, 226, 1) 68%)";
    })


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


    // Edit quantity of a shoppingCartItem or delete when quantity = 0;
    async function editQuantity(num, productId, quantity) {

        // delete if quantity will be 0
        if (quantity + num == 0) {
            const response = await fetch(`https://localhost:44319/api/ShoppingCartItem/${productId}`, {
                method: "DELETE"
            })
            if (response.ok) {
                // if user deleted a product, should refresh immediately
                setProductsInCart(prev => prev.filter(product => product.id !== productId));
            }
            return;

        }
        // Increase or decrease the quantity number
        setProductsInCart(prev =>
            prev.map(product =>
                product.id === productId
                    ? { ...product, quantity: product.quantity + num }
                    : product
            )
        )
    }


    // when the user clicks on the "ORDER" button
    async function submitOrder(e) {
        e.preventDefault();
        const updatedOrderInfo = {
            ...orderInfo,
            orderProducts: productsInCart.map(p => ({
                productId: p.productId,
                quantity: p.quantity,
            })),
        };
        const response = await api.post("https://localhost:44319/api/order",
            JSON.stringify(updatedOrderInfo),
            {
                headers: {
                    "Content-Type": "application/json"
                },
            })
        if (response.status !== 200) {
            console.error("Failed to submit order");
            return;
        }
        console.log("Order submitted successfully!");
        navigate("/");
    }



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
                    {productsInCart.map(x => <tr key={x.id}>
                        <td> {x.product.name} </td>
                        <td> {x.quantity} </td>
                        <td> {x.totalPrice} </td>
                        <td> <button className="increase-button" onClick={(e) => editQuantity(1, x.id, x.quantity)}> + </button> </td>
                        <td> <button className="decrease-button" onClick={(e) => editQuantity(-1, x.id, x.quantity)}> - </button> </td>
                    </tr>)}
                </tbody>
            </table>
            <div className="right-section">
                <form className="form-div" onSubmit={(e) => submitOrder(e)}>
                    <input className="order-input" type="text" placeholder="First Name"
                        onChange={e => setOrderInfo(prev => ({ ...prev, firstName: e.target.value }))} />
                    <input className="order-input" type="text" placeholder="Last Name"
                        onChange={e => setOrderInfo(prev => ({ ...prev, lastName: e.target.value }))} />
                    <input className="order-input" type="text" placeholder="City"
                        onChange={e => setOrderInfo(prev => ({ ...prev, city: e.target.value }))} />
                    <input className="order-input" type="text" placeholder="Postcode"
                        onChange={e => setOrderInfo(prev => ({ ...prev, postcode: e.target.value }))} />
                    <input className="order-input" type="text" placeholder="Address"
                        onChange={e => setOrderInfo(prev => ({ ...prev, address: e.target.value }))} />
                    <button type="submit" className="order-button" href="/">ORDER</button>
                </form>
                <div className="infos-div">
                    <p className="price"> Total Price: {productsInCart.reduce((sum, product) => sum + product.totalPrice, 0)} €</p>
                </div>
            </div>
        </div >
    )
}


