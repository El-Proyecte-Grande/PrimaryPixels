import './ProductsDiv.css';
import { useNavigate } from 'react-router-dom';
import { apiWithAuth } from "../../Axios/api"





function ProductsDiv({ products }) {
    const navigate = useNavigate();

    async function addToCart(productId) {

        const token = localStorage.getItem("token");
        if (token == null) return;
        const response = await apiWithAuth.post("/api/ShoppingCartItem",
            JSON.stringify({ productId: productId }),
            {
                headers: {
                    "Content-Type": "application/json"
                },
            }
        )
    }


    return (
        <div id="products-container">
            {products.map((product, index) => (
                <div key={index} className='product' >
                    <img className='product-image' onClick={() => navigate(`/product/${product.id}`)} src={product.image} alt={`This would be a picture of ${product.name}`}></img>
                    <div className="product-infos">
                        <p className="product-info"> {product.name} </p>
                        <div className='finance'>
                            <p className="product-info"> {formatHUF(product.price)}</p>
                            <div>
                                <button onClick={() => addToCart(product.id)} className='add-to-cart-button'> ADD </button>
                            </div>
                        </div>
                    </div>

                </div>
            ))}
        </div>
    );
};

export default ProductsDiv;

function formatHUF(amount) {
    const amountStr = amount.toString();
    const formattedAmount = amountStr.replace(/\B(?=(\d{3})+(?!\d))/g, '.');
    return `${formattedAmount} HUF`;
}