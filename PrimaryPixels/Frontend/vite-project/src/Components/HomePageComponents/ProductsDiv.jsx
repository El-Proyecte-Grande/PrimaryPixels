import './ProductsDiv.css';
import { useNavigate } from 'react-router-dom';





function ProductsDiv({ products }) {
    const navigate = useNavigate();
    return (
        <div id="products-container">
            {products.map((product, index) => (
                <div key={index} className='product' onClick={() => navigate(`/product/${product.id}`)}>
                    <img className='product-image' src={product.image} alt={`This would be a picture of ${product.name}`}></img>
                    <div className="product-infos">
                        <p className="product-info"> {product.name} </p>
                        <div className='finance'>
                            <p className="product-info"> {product.price} HUF</p>
                            <div>
                                <button className='add-to-cart-button'> ADD </button>
                            </div>
                        </div>
                    </div>

                </div>
            ))}
        </div>
    );
};

export default ProductsDiv;
