import './LoginPage.css';
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';


const getUserData = (email, password) => {
    if(email === "" || password === "") throw new Error("The email and/or password that you have to provide is/are missing!");
    return { email: email, password: password};
};

const authenticate = async (email, password, navigate) => {
    try {
        const response = await fetch("https://localhost:7029/Auth/Login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(getUserData(email, password))
        });
        if (!response.ok) throw new Error("Login was not successful!");
        const data = await response.json();
        localStorage.setItem('token', data.token);
        navigate("/",  { token: data.token});
    } catch (error) {
        console.error(error);
        alert(error.message);
    }
};



function LoginPage(){
    const [ email, setEmail] = useState("");
    const [ password, setPassword] = useState("");
    
    const navigate = useNavigate();

    return (
        <div id='login-page'>
            <p>Login to your account:</p>
            <div id='email-container'>
                <label htmlFor="email-input">Email: </label>
                <input type="email" name="email-input" id="email-input" onChange={(e) => setEmail(e.target.value)}/>
            </div>
            <div id="password-container">
                <label htmlFor="password-input">Password: </label>
                <input type="password" name="password-input" id="password-input" onChange={(e) => setPassword(e.target.value)}/>
            </div>
            <button type='button' onClick={() => {authenticate(email, password, navigate)}}>Login</button>
            <button type='button' onClick={() => navigate("/")}>Cancel</button>
        </div>
    );
};

export default LoginPage;