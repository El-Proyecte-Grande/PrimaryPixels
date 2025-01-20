import { useState, useEffect } from 'react';
import { styled } from 'styled-components';
import './Navbar.css';
import { useNavigate } from 'react-router-dom';
import { jwtDecode } from "jwt-decode";

const StyledNav = styled.nav`
    width: 99.7%;
    height: 12vh;
    font-size: 1.2em;
    display: flex;
    border-radius: 8px;
    background-color: rgb(227, 231, 225);
    align-items:center;
    position: fixed;
    z-index:20;
`;

const StyledButton = styled.button`
    margin-left: 3vw;
    background-color:rgb(255, 255, 255);
`;

const getUserId = () => {
    const token = localStorage.getItem("token");
    const decodedToken = jwtDecode(token);
    return decodedToken.sub;
};

function Navbar({ isLoggedIn, setIsLoggedIn }) {
    const navigate = useNavigate();
    return (
        <StyledNav>
            <img id="logo" src="/primary-pixels-logo.png" />
            <input type="search" id="searchbar" />
            <div className='auth-buttons'>
                {isLoggedIn ? <StyledButton onClick={() => navigate(`/cart/${getUserId()}`)}>Cart</StyledButton> : ""}
                <StyledButton onClick={() => { !isLoggedIn ? navigate("/login") : localStorage.removeItem("token"); setIsLoggedIn(false) }}>{!isLoggedIn ? "Login" : "Logout"}</StyledButton>
                {!isLoggedIn ? <StyledButton onClick={() => navigate("/register")}>Register</StyledButton> : ""}
            </div>
        </StyledNav>
    );
};

export default Navbar;