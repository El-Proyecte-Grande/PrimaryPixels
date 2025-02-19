import { useState, useEffect } from "react";
import { useParams } from "react-router-dom"
import { api } from "../Axios/api"

export default function ResetPassword() {

    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [errorMessage, setErrorMessage] = useState("");
    const [success, setSuccess] = useState(false);
    const { token } = useParams();

    async function handleChange(e) {
        e.preventDefault();
        if (password !== confirmPassword) {
            setErrorMessage("Passwords must match!");
            return;
        }
        if (password.length < 8) {
            setErrorMessage("Passwords must be longer than 7 characters!");
            return;
        }
        if (!/[A-Z]/.test(password)) {
            setErrorMessage("Password must contain at least one uppercase letter!");
            return;
        }
        try {
            const response = await api.post("/api/User/reset-password",
                JSON.stringify({ Email: email, Token: token, NewPassword: password }),
                {
                    headers: {
                        "Content-Type": "application/json"
                    }
                });
            if (response.status == 200) {
                setErrorMessage("");
                setSuccess(true);
            }
        } catch (error) {
            if (error.response) {
                setSuccess(false)
                setErrorMessage(error.response.data);
            } else {
                console.error("Error message:", error.message);
            }
        }
    }

    return (
        <div className="flex items-center justify-center h-screen">
            <div className="w-[22rem] h-[27rem] border-gray-200 border-2 rounded-xl p-8">
                <p className="text-2xl font-mono font-black border-b-2 border-gray-200 pb-4"> Change Password </p>
                <form onSubmit={(e) => handleChange(e)} className="flex flex-col">
                    <label htmlFor="newPassword" className="mt-8 mb-2 font-bold"> New Password</label>
                    <input id="newPassword" onChange={(e) => setPassword(e.target.value)} className="w-full h-10 border-[#dddddd] border-2 rounded-md pl-4" placeholder="New Password" />
                    <label htmlFor="confirmNewPassword" className="mt-4 mb-2 font-bold"> Confirm New Password</label>
                    <input id="confirmNewPassword" onChange={(e) => setConfirmPassword(e.target.value)} className="w-full h-10 border-[#dddddd] border-2 rounded-md pl-4" placeholder="Confirm New Password" />
                    <button type="submit" className="mt-8 border-2 bg-[#5cb85c] border-[#4cae4c] text-white w-44 h-10 font-mono flex items-center justify-center" > Change Password </button>
                    {errorMessage != "" && <p className="mt-4 text-red-600"> {errorMessage} </p>}
                    {success && <p className="text-green-500 mt-4"> Reset Was Successful! </p>}
                </form>
            </div>
        </div>
    )
}