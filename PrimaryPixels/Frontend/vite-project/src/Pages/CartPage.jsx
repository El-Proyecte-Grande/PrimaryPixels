import { useState, useEffect } from "react"
import { useParams } from "react-router-dom";
import './CartPage.scss';
export default function CartPage() {

    const { userId } = useParams();
    const [orderInfo, setOrderInfo] = useState({});
    const [productsInCart, setProductsInCart] = useState([]);

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
    async function editQuantity(num, productId, quantity, deleteId) {

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
                <form className="form-div">
                    <input className="order-input" type="text" name="name" placeholder="Name" />
                    <input className="order-input" type="text" name="name" placeholder="City" />
                    <input className="order-input" type="text" name="name" placeholder="Postcode" />
                    <input className="order-input" type="text" name="name" placeholder="Address" />
                    <button type="submit" className="order-button" href="/">ORDER</button>
                </form>
                <div className="infos-div">
                    <p className="price"> Total Price: {productsInCart.reduce((sum, product) => sum + product.totalPrice, 0)} €</p>
                </div>
            </div>
        </div >
    )
}


