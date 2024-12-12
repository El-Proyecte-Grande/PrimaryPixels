import { useState, useEffect, } from "react";
import { useParams } from "react-router-dom"
import './productPage.scss';

export default function ProductPage() {

    const { id } = useParams();
    const [product, setProduct] = useState({});

    useEffect(() => {
        async function fetchProduct() {
            const response = await fetch("http://localhost:8000/api/Phone/3");
            const data = await response.json();
            console.log(data);
        }
        fetchProduct();
    }, [])

    return (
        <div className="main-product">
            <div className="left-section">
                <div className="product-image-div">
                    <img alt="image"></img>
                </div>
                <div className="product-order-infos">
                    <div className="product-price">
                        <p className="price"> 10.999 HUF </p>
                    </div>
                    <div className="add-to-cart-div">
                        <button className="add-to-cart-button"> ADD TO CART </button>
                    </div>
                </div>
            </div>
            <div className="right-section">
                <div className="product-name-div">
                    <p className="product-name"> UltraGIGMEGA GAMER PC 3000 </p>
                </div>
                <div className="product-details-div">
                    <p className="property"> CPU: i3-6100 </p>
                    <p className="property"> RAM: 8GB </p>
                </div>
            </div>
        </div>
    )
}