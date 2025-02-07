import './AuthPage.scss';
import { useState, useEffect } from 'react';
import { useNavigate } from "react-router-dom";
import { api } from "../Axios/api";


export default function RegistrationPage() {
    const navigate = useNavigate();
    const [userData, setUserData] = useState({ username: "", password: "", confirmPassword: "", email: "" });
    const [registerError, setRegisterError] = useState("");


    async function handleRegister(e) {
        e.preventDefault();
        if (userData.password != userData.confirmPassword) {
            setRegisterError("Passwords must match!")
            return;
        }
        if (userData.password.length < 8) {
            setRegisterError("Password length must be at least 8 character!");
            return;
        }
        try {
            const response = await api.post("/Auth/register", {
                username: userData.username,
                password: userData.password,
                email: userData.email
            });
            if (response.status == 201) {
                // if registration was successful, login automatically and navigate
                const loginResponse = await api.post("/Auth/login", {
                    email: userData.email,
                    password: userData.password
                },
                    {
                        headers: { "Content-Type": "application/json" }
                    }
                );
                if (loginResponse.status == 200) {
                    const data = await loginResponse.data
                    localStorage.setItem('token', data.token);
                    navigate("/");
                }
            }
        } catch (error) {
            if (error.response && error.response.data) {
                const firstError = Object.values(error.response.data)
                setRegisterError(firstError || "An error occurred during registration.");
            } else {
                setRegisterError("Unexpected error. Please try again.");
            }
        }
    }

    return (
        <div className='registration-page'>
            <div className='registration-page-container'>
                <div className='registration-left'>
                    {registerError != "" && <div className='registration-error'> {registerError} </div>}
                    <div className='registration-header'>
                        <div className='get-started'> Get Started! </div>
                        <div className='already'>
                            <div className='have-account'> Already have an account? </div>
                            <button className='already-button' onClick={() => navigate("/login")}> Sign in </button>
                        </div>
                    </div>
                    <div className='registration-form-div'>
                        <form className="registration-form" onSubmit={(e) => handleRegister(e)}>
                            <label className="registration-label" htmlFor="username-input"> Username</label>
                            <input className="registration-input" type="text" id="username-input" onChange={(e) => setUserData(prev => ({ ...prev, username: e.target.value }))} />
                            <label className="registration-label" htmlFor="email-input"> Email</label>
                            <input className="registration-input" type="text" id="email-input" onChange={(e) => setUserData(prev => ({ ...prev, email: e.target.value }))} />
                            <label className="registration-label" htmlFor="password-input"> Password</label>
                            <input className="registration-input" type="password" id="password-input" onChange={(e) => setUserData(prev => ({ ...prev, password: e.target.value }))} />
                            <label className="registration-label" htmlFor="confirmPassword-input"> Confirm password</label>
                            <input className="registration-input" type="password" id="confirmPassword-input" onChange={(e) => setUserData(prev => ({ ...prev, confirmPassword: e.target.value }))} />
                            <button className="sign-up-button"> Sign up </button>
                        </form>
                    </div>
                </div>
                <div className='registration-right'>
                    <img onClick={() => navigate("/")} src="/primary-pixels-logo.png" className='logo'></img>
                </div>
            </div>
        </div>

    )
}