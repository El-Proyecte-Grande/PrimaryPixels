import { useState, useEffect, } from "react";
import { useParams } from "react-router-dom"
import { apiWithAuth, api } from "../Axios/api";
import './ProductPage.scss'
import Navbar from "../Components/HomePageComponents/Navbar";

export default function ProductPage() {

    const { id } = useParams();
    const [product, setProduct] = useState({});

    const [isLoggedIn, setIsLoggedIn] = useState(() => (

        localStorage.getItem("token") === null ? false : true
    ));

    useEffect(() => {
        async function fetchProduct() {
            const response = await api.get(`/api/Product/${id}`)
            const data = await response.data
            setProduct(data)
        }
        fetchProduct();
    }, [id])

    async function AddToCart() {
        const response = await apiWithAuth.post("/api/ShoppingCartItem",
            JSON.stringify({ productId: id }),
            {
                headers: {
                    "Content-Type": "application/json"
                },
            }
        )
    }

    return (
        <>
            <Navbar isLoggedIn={isLoggedIn} />
            <div className="main-product">
                <div className="left-section">
                    <div className="product-image-div">
                        <img className="product-image" alt="image" src={product.image}></img>
                    </div>

                </div>
                <div className="right-section">
                    <div className="product-name-div">
                        <p className="product-name"> {product.name} </p>

                    </div>
                    <div className="product-details-div">
                        {Object.entries(product).map(([key, value]) => (
                            <>
                                {key != "image" && key != "totalSold" && key != "name" && key != "id" && key != "price" && (
                                    <p className="property" key={key}>
                                        {key.toUpperCase()}: {value == false || value == true ? value == true ? "✅" : "❌" : value.toString()}
                                    </p>
                                )}
                            </>
                        )
                        )}
                    </div>
                    <div className="product-order-infos">
                        <div className="product-price">
                            {product.price && <p className="price"> {formatHUF(product.price)} </p>}


                        </div>

                        <div className="add-to-cart-div">
                            <button className="add-to-cart-buttonn" onClick={(e) => AddToCart()}> ADD TO CART </button>
                        </div>
                    </div>
                </div>
            </div>
        </>

    )
}

function formatHUF(amount) {
    const amountStr = amount.toString();
    const formattedAmount = amountStr.replace(/\B(?=(\d{3})+(?!\d))/g, '.');
    return `${formattedAmount} HUF`;
}