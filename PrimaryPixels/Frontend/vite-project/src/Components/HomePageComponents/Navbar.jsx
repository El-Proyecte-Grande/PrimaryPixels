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
    background-color: white;
    align-items:center;
    position: fixed;
    z-index:20;
    border-bottom: 2px solid rgba(24, 2, 2, 0.13);
`;

const StyledButton = styled.button`
    background-color: #FAFBFC;
  border: 1px solid rgba(27, 31, 35, 0.15);
  border-radius: 6px;
  box-shadow: rgba(27, 31, 35, 0.04) 0 1px 0, rgba(255, 255, 255, 0.25) 0 1px 0 inset;
  box-sizing: border-box;
  color: #24292E;
  cursor: pointer;
  display: inline-block;
  font-family: -apple-system, system-ui, "Segoe UI", Helvetica, Arial, sans-serif, "Apple Color Emoji", "Segoe UI Emoji";
  font-size: 14px;
  font-weight: 500;
  line-height: 26px;
  width: 5rem;
  list-style: none;
  padding: 6px 16px;
  position: relative;
  transition: background-color 0.2s cubic-bezier(0.3, 0, 0.5, 1);
  margin-right:2rem;
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


