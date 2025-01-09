import './ProductsDiv.css';





function ProductsDiv({ products }){
    return(
        <div id="products-container">
            {products.map( (product, index) => (
                <div key={index} className='product'>
                    <img className='productImage' src={product.image} alt={`This would be a picture of ${product.name}`}></img>
                    <p className="productInfo"> {product.name} </p>
                    <p className="productInfo"> {product.price} HUF</p>
                </div>
            ))}
        </div>
    );
};

export default ProductsDiv;
