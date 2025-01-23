
import { useState, useEffect } from "react";
import { useLocation } from "react-router-dom";
import { styled } from 'styled-components';
import Navbar from "../Components/HomePageComponents/Navbar";
import ProductsDiv from "../Components/HomePageComponents/ProductsDiv";
import FilterOptionsDiv from "../Components/HomePageComponents/FilterOptionsDiv";
import './HomePage.css';

function HomePage() {
    const [products, setProducts] = useState([]);
    const [isLoggedIn, setIsLoggedIn] = useState(() => (
        localStorage.getItem("token") === null ? false : true
    ));
    const location = useLocation();

    console.log(location.token);
    useEffect(() => {
        setIsLoggedIn(localStorage.getItem("token") === null ? false : true);

    }, [location.token]);

    return (
        <>
            <Navbar></Navbar>
            <div id='container'>
                <FilterOptionsDiv products={products} setProducts={setProducts}></FilterOptionsDiv>
                <ProductsDiv products={products} setProducts={setProducts}></ProductsDiv>
            </div>
        </>
    );
};

export default HomePage;
