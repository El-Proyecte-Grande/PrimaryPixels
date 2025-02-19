import { useState, useEffect } from "react"
import { api } from "../Axios/api";


export default function ResetPassowrd() {

    const [email, setEmail] = useState("");
    const [errorMessage, setErrorMessage] = useState("");
    const [success, setSuccess] = useState(false);

    async function handleReset(e) {
        e.preventDefault();
        try {
            const response = await api.post("/api/User/forgot-password",
                JSON.stringify({ Email: email }),
                {
                    headers: {
                        "Content-Type": "application/json"
                    }
                });
            if (response.status == 200) {
                setErrorMessage("");
                setSuccess(true);
            }
        }
        catch (error) {
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
                <p className="text-2xl font-mono font-black border-b-2 border-gray-200 pb-4"> PASSWORD RESET </p>
                <p className="my-4 border-[#e6db55] rounded-lg text-gray-500 bg-[#ffffe0] border-2 p-4">Forgotten your password? Enter your e-mail address below, and we'll send you an e-mail allowing you to reset it.</p>
                <form onSubmit={(e) => handleReset(e)} className="flex flex-col">
                    <input onChange={(e) => setEmail(e.target.value)} className="w-full mt-2 h-10 border-[#dddddd] border-2 rounded-md pl-4" placeholder="E-mail Address" />
                    <button type="submit" className="mt-8 border-2 bg-[#5cb85c] border-[#4cae4c] text-white w-44 h-10 font-mono flex items-center justify-center" > Reset Password </button>
                    {errorMessage != "" && <p className="mt-4 text-red-600"> {errorMessage} </p>}
                    {success && <p className="text-green-500 mt-4"> Email Sent! </p>}
                </form>
            </div>
        </div>

    )
}