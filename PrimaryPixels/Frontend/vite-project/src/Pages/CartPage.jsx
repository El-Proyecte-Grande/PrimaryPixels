import { useState, useEffect } from "react"
import { useParams, useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";
import './CartPage.scss';
import { apiWithAuth } from "../Axios/api";
import Navbar from "../Components/HomePageComponents/Navbar";
export default function CartPage() {

    const { userId } = useParams();
    const [orderInfo, setOrderInfo] = useState({ firstName: "", lastName: "", city: "", postcode: "", address: "", orderProducts: [] });
    const [productsInCart, setProductsInCart] = useState([]);
    const navigate = useNavigate();

    const [isLoggedIn, setIsLoggedIn] = useState(() => (

        localStorage.getItem("token") === null ? false : true
    ));

    // If we are not logged in or if we want to search for other user's cart, it redirect us to the home page.
    useEffect(() => {
        const token = localStorage.getItem("token");
        const decodedToken = jwtDecode(token);
        if (token == null || decodedToken.sub !== userId) {
            navigate('/');
        }
    }, [navigate])

    // Get cart's products
    useEffect(() => {
        async function getProducts() {
            const response = await apiWithAuth.get(`/api/ShoppingCartItem/user/${userId}`);
            if (response.status == 200) {
                const data = response.data;
                setProductsInCart(data);
            }
        }
        getProducts();
    }, [])


    // Edit quantity of a shoppingCartItem or delete when quantity = 0;
    async function editQuantity(num, productId, quantity) {

        // delete if quantity will be 0
        if (quantity + num == 0) {
            const response = await apiWithAuth.delete(`/api/ShoppingCartItem/${productId}`)
            if (response.status == 200) {
                // if user deleted a product, should refresh immediately
                setProductsInCart(prev => prev.filter(product => product.id !== productId));
            }
            return;

        }
        // Increase or decrease the quantity number
        setProductsInCart(prev =>
            prev.map(product =>
                product.id === productId
                    ? {
                        ...product,
                        quantity: product.quantity + num,
                        totalPrice: (product.quantity + num) * product.unitPrice, // Update the totalPrice based on the new quantity
                    }
                    : product
            )
        );
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
        const response = await apiWithAuth.post("/api/order",
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
        const orderId = response.data;
        navigate(`/order/success/${orderId}`);
    }



    return (
        <>
            <Navbar isLoggedIn={isLoggedIn} />
            <div className="page-div">
                <table className="left-section-cart">
                    <thead>
                        <tr className="headers">
                            <th className="header-text"> NAME </th>
                            <th className="header-text"> QUANTITY </th>
                            <th className="header-text"> PRICE </th>
                        </tr>
                    </thead>
                    <tbody className="cart-body">
                        {productsInCart.map(x => <tr key={x.id} className="cart-item">
                            <td className="product-property"> {x.product.name} </td>
                            <td className="product-property"> {x.quantity} </td>
                            <td className="product-property"> {x.totalPrice} </td>
                            <td className="button-cell"> <button className="amount-button" onClick={(e) => editQuantity(1, x.id, x.quantity)}> + </button> </td>
                            <td className="button-cell"> <button className="amount-button" onClick={(e) => editQuantity(-1, x.id, x.quantity)}> - </button> </td>
                        </tr>)}
                    </tbody>
                </table>
                <div className="right-section-cart">
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
                        <p className="price"> Total Price: {productsInCart.reduce((sum, product) => sum + product.totalPrice, 0)} HUF</p>
                    </div>
                </div>
            </div >
        </>

    )
}


