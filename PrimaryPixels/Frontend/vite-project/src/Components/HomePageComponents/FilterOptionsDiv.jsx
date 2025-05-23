import { useState, useEffect } from 'react';
import Loading from '../LoadingComponent/Loading';
import './FilterOptionsDiv.css';
import { api } from "../../Axios/api.js"



const fetchProducts = async (filter) => {
    try {
        const response = await api.get(`/api/${filter === "Popular" ? "Product/Popular" : filter}`);
        if (response.status !== 200) {
            throw new Error("Products were not found or error during accessing the data!");
        }
        return response.data;
    } catch (error) {
        console.error(error);
        alert(error.message);
        return [];
    }
}

const filterChanger = (event, setFilter) => {
    const targetElement = event.target;
    setFilter(targetElement.textContent);
}


function FilterOptionsDiv({ products, setProducts }) {
    const [loading, setLoading] = useState(false);
    const [filter, setFilter] = useState("Product");

    useEffect(() => {
        setLoading(true);
        fetchProducts(filter).then(data => {
            if (data.length === 0) {
                alert("No products were found!!");
            }
            setProducts(data);
            setLoading(false);
        });
    }, [filter]);

    if (loading) {
        return <Loading></Loading>;
    }
    return (
        <div id='filter-buttons'>
            <button key='Popular' type='button' className='filterButton' onClick={(event) => filterChanger(event, setFilter)}>Popular</button>
            <button key='Phone' type='button' className='filterButton' onClick={(event) => filterChanger(event, setFilter)}>Phone</button>
            <button key='Headphone' type='button' className='filterButton' onClick={(event) => filterChanger(event, setFilter)}>Headphone</button>
            <button key='Computer' type='button' className='filterButton' onClick={(event) => filterChanger(event, setFilter)}>Computer</button>
        </div>
    );
};

export default FilterOptionsDiv;