import './ProductsDiv.css';
import { jwtDecode } from "jwt-decode";
import { useNavigate } from 'react-router-dom';





function ProductsDiv({ products }) {
    const navigate = useNavigate();

    async function addToCart(productId) {

        const token = localStorage.getItem("token");
        const decodedToken = jwtDecode(token);
        console.log(decodedToken.sub);
        console.log(productId)
    }

    return (
        <div id="products-container">
            {products.map((product, index) => (
                <div key={index} className='product' >
                    <img className='product-image' onClick={() => navigate(`/product/${product.id}`)} src={product.image} alt={`This would be a picture of ${product.name}`}></img>
                    <div className="product-infos">
                        <p className="product-info"> {product.name} </p>
                        <div className='finance'>
                            <p className="product-info"> {product.price} HUF</p>
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
