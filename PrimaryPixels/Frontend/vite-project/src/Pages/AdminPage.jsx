import { useState, useEffect } from "react"
import AdminPageSidebar from "../Components/AdminPageSidebar"
import './AdminPage.scss';
import Navbar from "../Components/HomePageComponents/Navbar";
import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";
import { apiWithAuth } from "../Axios/api";

export default function AdminPage() {

    const [current, setCurrent] = useState("Phone");
    const navigate = useNavigate();
    const [product, setProduct] = useState({});

    // Check if the logged user have admin role
    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token == null) navigate("/")
        const decodedToken = jwtDecode(token);
        console.log(decodedToken);
        if (decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] != "Admin") {
            // navigate('/');
        }
    }, [])

    async function handleSubmit(e) {
        e.preventDefault();
        const response = await apiWithAuth.post(`http://localhost:8000/api/${current}`,
            JSON.stringify(product),
            {
                headers: {
                    "Content-Type": "application/json"
                },
            })
    }

    useEffect(() => {
        console.log(product);
    }, [product])

    return (
        <>
            <Navbar />
            <AdminPageSidebar setCurrent={setCurrent} />
            <div className="admin-page">
                <form onSubmit={(e) => handleSubmit(e)} className="product-form">
                    {current === "Computer" ? (
                        <>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Name </label>
                                <input className="product-input" onChange={(e) => setProduct(prev => ({ ...prev, name: e.target.value }))} />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Image </label>
                                <input className="product-input" onChange={(e) => setProduct(prev => ({ ...prev, image: e.target.value }))} />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Price </label>
                                <input className="product-input" onChange={(e) => setProduct(prev => ({ ...prev, price: e.target.value }))} />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name" > RAM </label>
                                <input className="product-input" onChange={(e) => setProduct(prev => ({ ...prev, ram: e.target.value }))} />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name" > CPU </label>
                                <input className="product-input" onChange={(e) => setProduct(prev => ({ ...prev, cpu: e.target.value }))} />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> Internal Memory </label>
                                <input className="product-input" onChange={(e) => setProduct(prev => ({ ...prev, internalMemory: e.target.value }))} />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name"> DVD Player </label>
                                <input type="checkbox" className="product-input" onChange={(e) => setProduct(prev => ({ ...prev, dvdPlayer: e.target.value }))} />
                            </div>
                            <div className="form-element">
                                <label className="product-label" htmlFor="name" > Availability </label>
                                <input type="checkbox" className="product-input" onChange={(e) => setProduct(prev => ({ ...prev, availability: e.target.value }))} />
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