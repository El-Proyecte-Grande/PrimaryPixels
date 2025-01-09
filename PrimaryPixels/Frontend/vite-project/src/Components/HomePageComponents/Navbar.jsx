import { useState, useEffect } from 'react';
import { styled } from 'styled-components';
import './Navbar.css';

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



function Navbar() {

    return (
        <StyledNav>
            <img id="logo" src="/primary-pixels-logo.png" />
            <input type="search" id="searchbar" />
            <div className='auth-buttons'>
                <StyledButton>Login</StyledButton>
                <StyledButton>Register</StyledButton>
            </div>
        </StyledNav>
    );
};

export default Navbar;