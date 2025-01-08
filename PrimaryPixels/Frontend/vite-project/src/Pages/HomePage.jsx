
import { useState, useEffect } from "react";
import { styled } from 'styled-components';
import Navbar from "../Components/HomePageComponents/Navbar";
import ProductsDiv from "../Components/HomePageComponents/ProductsDiv";
import FilterOptionsDiv from "../Components/HomePageComponents/FilterOptionsDiv";
import './HomePage.css';

function HomePage(){
const [products, setProducts] = useState([]);

    return (
        <>
            <Navbar></Navbar>
            <div id='products-container'>
                <FilterOptionsDiv products={products} setProducts={setProducts}></FilterOptionsDiv>
                <ProductsDiv products={products} setProducts={setProducts}></ProductsDiv>
            </div>
        </>
    );
};

export default HomePage;
