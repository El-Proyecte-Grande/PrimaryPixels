import { useState, useEffect, useCallback } from 'react';
import { styled } from 'styled-components';
import './Navbar.css';
import { useNavigate } from 'react-router-dom';
import { jwtDecode } from "jwt-decode";
import { useLocation } from "react-router-dom";
import { debounce } from "lodash";
import { api } from "../../Axios/api"

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

function Navbar() {
    const navigate = useNavigate();
    const [searchedProducts, setSearchedProducts] = useState([]);
    const [query, setQuery] = useState("");

    const [isLoggedIn, setIsLoggedIn] = useState(() => (

        localStorage.getItem("token") === null ? false : true
    ));

    const location = useLocation();

    useEffect(() => {
        setIsLoggedIn(localStorage.getItem("token") === null ? false : true);

    }, [location.token]);

    useEffect(() => {
        getSearchedProducts(query);
    }, [query])


    const getSearchedProducts = useCallback(
        debounce(async (searchTerm) => {
            if (searchTerm === "") {
                setSearchedProducts([]);
                return;
            }
            try {
                const response = await api.get(`/api/product/search?name=${searchTerm}`);
                setSearchedProducts(response.data);
            } catch (error) {
                console.error("Error fetching products:", error);
            }
        }, 1000),
        []
    );

    return (
        <StyledNav>
            <img id="logo" src="/primary-pixels-logo.png" onClick={(e) => navigate("/")} />
            <div className='search-div'>
                <input type="search" id="searchbar" onChange={(e) => setQuery(e.target.value)} placeholder='Enter product name here' />
                {searchedProducts.length > 0 &&
                    <div className='search-products-div'>
                        {searchedProducts.map((p) =>
                            <div key={p.id} className='search-product-div' onClick={() => navigate(`/product/${p.id}`)}>
                                <img src={p.image} className='search-product-img'></img>
                                <p className='search-product-name'> {p.name} </p>
                            </div>
                        )}
                    </div>
                }
            </div>
            <div className='auth-buttons'>
                {isLoggedIn ? <StyledButton onClick={() => navigate(`/cart/${getUserId()}`)}>Cart</StyledButton> : ""}
                <StyledButton onClick={() => { !isLoggedIn ? navigate("/login") : localStorage.removeItem("token"); setIsLoggedIn(false) }}>{!isLoggedIn ? "Login" : "Logout"}</StyledButton>
                {!isLoggedIn ? <StyledButton onClick={() => navigate("/register")}>Register</StyledButton> : ""}
                {isLoggedIn && <img src="../../../public/user.png" className='profile-logo' onClick={() => navigate(`/profile/${getUserId()}`)} />}
            </div>
        </StyledNav>
    );
};

export default Navbar;


