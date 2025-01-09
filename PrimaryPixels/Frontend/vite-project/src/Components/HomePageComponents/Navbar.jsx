import { useState, useEffect } from 'react';
import { styled } from 'styled-components';
import './Navbar.css';
import { useNavigate } from 'react-router-dom';

const StyledNav = styled.nav`
    width: 99.7%;
    height: 12vh;
    font-size: 1.2em;
    display: flex;
    border-radius: 8px;
    border: 2px solid black;
    background-color: rgba(161,161,161,0.7);
`;

const StyledButton = styled.button`
    margin-left: 3vw;
    margin-right: 5vw;
    background-color: #26a5a4;
`;

const navigate = useNavigate();

function Navbar(){

    return(
        <StyledNav>
            <img id="logo" src="/primary-pixels-logo.png"/>
            <p id="input-message">Search: </p>
            <input type="search" id="searchbar"/>
            <StyledButton onClick={() => navigate("/login")}>Login</StyledButton>
            <StyledButton>Register</StyledButton>
        </StyledNav>
    );
};

export default Navbar;