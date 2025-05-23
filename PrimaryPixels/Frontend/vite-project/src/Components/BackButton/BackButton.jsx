import { useNavigate } from "react-router-dom";



export default function BackButton(){
    const navigate = useNavigate();
    return(
        <div className="">
            <button type="button" className="" onClick={() => navigate("/")}>Back</button>
        </div>                

    )
}
