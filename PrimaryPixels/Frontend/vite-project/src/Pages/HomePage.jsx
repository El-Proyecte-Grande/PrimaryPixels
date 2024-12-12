
import { useState, useEffect } from "react";
import { styled } from 'styled-components';
import Navbar from "../Components/HomePageComponents/Navbar";
import ProductsDiv from "../Components/HomePageComponents/ProductsDiv";



function HomePage(){
    return (
        <>
            <Navbar></Navbar>
            <ProductsDiv></ProductsDiv>
        </>
    );
};

export default HomePage;
