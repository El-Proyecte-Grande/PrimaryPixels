import './LoginPage.css';
import { useNavigate } from 'react-router-dom';


const navigate = useNavigate();



const getUserDataFromForm = () => {
    const email = document.getElementById("email-input").nodeValue;
    const password = document.getElementById("password-input").nodeValue;
    if(!email || !password) throw new Error("The email and/or password that you have to provide is/are missing!");
    return { email: email, password: password};
};

const authenticate = async () => {
    try {
        const response = await fetch("https://localhost:7029/Auth/Login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(getUserDataFromForm())
        });
        if (!response.ok) throw new Error("Login was not successful!");
        const data = await response.json();
        localStorage.setItem('token', data.token);
    } catch (error) {
        console.error(error);
        alert(error.message);
    }
};



function LoginPage(){
    return (
        <div id='login-page'>
            <p>Login to your account:</p>
            <div id='email-container'>
                <label htmlFor="email-input">Email: </label>
                <input type="email" name="email-input" id="email-input" />
            </div>
            <div id="password-container">
                <label htmlFor="password-input">Password: </label>
                <input type="password" name="password-input" id="password-input" />
            </div>
            <button type='submit' onSubmit={() => authenticate()}>Login</button>
            <button type='button' onClick={() => navigate("/")}>Cancel</button>
        </div>
    );
};

export default LoginPage;