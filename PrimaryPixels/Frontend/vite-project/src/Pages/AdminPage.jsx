import { useState, useEffect } from "react"
import AdminPageSidebar from "../Components/AdminPageSidebar"
import './AdminPage.scss';
import Navbar from "../Components/HomePageComponents/Navbar";
import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";

export default function AdminPage() {

    const [current, setCurrent] = useState("Phone");
    const navigate = useNavigate();

    // Check if the logged user have admin role
    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token == null) navigate("/")
        const decodedToken = jwtDecode(token);
        console.log(decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]);
        if (decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] != "Admin") {
            // navigate('/');
        }
    }, [])

    return (
        <>
            <Navbar />
            <AdminPageSidebar setCurrent={setCurrent} />
            <div className="admin-page">
                <form className="product-form">
                    {current === "Computer" ? (
                        <>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Name </label>
                                <input className="product-input" />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Image </label>
                                <input className="product-input" />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Price </label>
                                <input className="product-input" />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> RAM </label>
                                <input className="product-input" />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> CPU </label>
                                <input className="product-input" />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Internal Memory </label>
                                <input className="product-input" />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> DVD Player </label>
                                <input type="checkbox" className="product-input" />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Availability </label>
                                <input type="checkbox" className="product-input" />
                            </div>
                        </>

                    ) : current === "Phone" ? (
                        <>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Name </label>
                                <input className="product-input" />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Image </label>
                                <input className="product-input" />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Price </label>
                                <input className="product-input" />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> RAM </label>
                                <input className="product-input" />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> CPU </label>
                                <input className="product-input" />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Internal Memory </label>
                                <input className="product-input" />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Card Independency </label>
                                <input type="checkbox" className="product-input" />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Availability </label>
                                <input type="checkbox" className="product-input" />
                            </div>
                        </>
                    ) : current === "Headphone" ? (
                        <>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Name </label>
                                <input className="product-input" />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Image </label>
                                <input className="product-input" />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Price </label>
                                <input className="product-input" />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Wireless </label>
                                <input type="checkbox" className="product-input" />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Availability </label>
                                <input type="checkbox" className="product-input" />
                            </div>
                        </>
                    ) : (
                        <></>
                    )}
                    <button className="add-product-button"> ADD PRODUCT </button>
                </form>
            </div>
        </>
    )
}