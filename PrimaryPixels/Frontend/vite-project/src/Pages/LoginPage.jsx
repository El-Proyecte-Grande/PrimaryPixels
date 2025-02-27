import './AuthPage.scss';
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';
import { api } from "../Axios/api"



function LoginPage() {
    const [userData, setUserData] = useState({ email: "", password: "" });
    const [loginError, setLoginError] = useState("");
    const navigate = useNavigate();

    const authenticate = async (e) => {
        try {
            e.preventDefault();
            const response = await api.post("/Auth/login", userData, {
                headers: { "Content-Type": "application/json" }
            });
            if (response.status == 200) {
                const data = await response.data
                localStorage.setItem('token', data.token);
                navigate("/", { token: data.token });
            }
        } catch (error) {
            if (error.response && error.response.data) {
                const firstError = Object.values(error.response.data)
                setLoginError(firstError || "An error occurred during registration.");
            } else {
                setLoginError("Unexpected error. Please try again.");
            }
        }
    };

    return (
        <div className='registration-page'>
            <div className='registration-page-container'>
                <div className='registration-left'>
                    {loginError != "" && <div className='registration-error'> {loginError} </div>}
                    <div className='registration-header'>
                        <div className='get-started'> Welcome back! </div>
                        <div className='already'>
                            <div className='have-account'> Don't have an account? </div>
                            <button className='already-button' onClick={() => navigate("/register")}> Sign up </button>
                        </div>
                    </div>
                    <div className='registration-form-div'>
                        <form className="registration-form" onSubmit={(e) => authenticate(e)}>
                            <label className="registration-label" htmlFor="email-input"> Email</label>
                            <input className="registration-input" type="text" id="email-input" onChange={(e) => setUserData(prev => ({ ...prev, email: e.target.value }))} />
                            <label className="registration-label" htmlFor="password-input"> Password</label>
                            <input className="registration-input" type="password" id="password-input" onChange={(e) => setUserData(prev => ({ ...prev, password: e.target.value }))} />
                            <button className="sign-up-button"> Sign in </button>
                            <p className='mt-10 cursor-pointer' onClick={() => navigate("/reset/password")}> Forgot Password? </p>
                        </form>
                    </div>
                </div>
                <div className='registration-right'>
                    <img onClick={() => navigate("/")} src="/primary-pixels-logo.png" className='logo'></img>
                </div>
            </div>
        </div>
    );
};

export default LoginPage;