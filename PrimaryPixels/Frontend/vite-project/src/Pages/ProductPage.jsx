import { useState, useEffect, } from "react";
import { useParams } from "react-router-dom"
import './productPage.scss';

export default function ProductPage() {

    const { id } = useParams();
    const [product, setProduct] = useState({});

    useEffect(() => {
        async function fetchProduct() {
            const response = await fetch(`https://localhost:44319/api/Product/${id}`);
            const data = await response.json();
            setProduct(data)
        }
        fetchProduct();
    }, [])

    return (
        <div className="main-product">
            <div className="left-section">
                <div className="product-image-div">
                    <img className="product-image" alt="image" src={product.image}></img>
                </div>
                <div className="product-order-infos">
                    <div className="product-price">
                        <p className="price"> {product.price} € </p>
                    </div>
                    <div className="add-to-cart-div">
                        <button className="add-to-cart-button"> ADD TO CART </button>
                    </div>
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
                                    {key.toUpperCase()}: {value.toString()}
                                </p>
                            )}
                        </>
                    )
                    )}

                </div>
            </div>
        </div>
    )
}