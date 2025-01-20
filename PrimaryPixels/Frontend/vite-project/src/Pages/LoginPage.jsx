import './LoginPage.css';
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';
import {api} from "../Axios/api"

const getUserData = (email, password) => {
    if (email === "" || password === "") throw new Error("The email and/or password that you have to provide is/are missing!");
    return { email: email, password: password };
};

const authenticate = async (email, password, navigate) => {
    try {
        const response = await api.post("/Auth/Login", getUserData(email, password), {
            headers: { "Content-Type": "application/json" }
        });
        if (response.status !== 200) {
            throw new Error("Login was not successful!");
        }
        const data = await response.data
        localStorage.setItem('token', data.token);
        navigate("/", { token: data.token });
    } catch (error) {
        console.error(error);
        alert(error.message);
    }
};



function LoginPage() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const navigate = useNavigate();

    return (
        <div id='login-page'>
            <p>Login to your account:</p>
            <div id='email-container'>
                <label htmlFor="email-input">Email: </label>
                <input type="email" name="email-input" id="email-input" onChange={(e) => setEmail(e.target.value)} />
            </div>
            <div id="password-container">
                <label htmlFor="password-input">Password: </label>
                <input type="password" name="password-input" id="password-input" onChange={(e) => setPassword(e.target.value)} />
            </div>
            <button type='button' onClick={() => { authenticate(email, password, navigate) }}>Login</button>
            <button type='button' onClick={() => navigate("/")}>Cancel</button>
        </div>
    );
};

export default LoginPage;