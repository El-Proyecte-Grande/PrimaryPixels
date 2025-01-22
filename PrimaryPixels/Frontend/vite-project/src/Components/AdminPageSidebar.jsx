import "../Pages/AdminPage.scss"

export default function AdminPageSidebar({ setCurrent }) {

    return (
        <div className="admin-sidebar">
            <div className="sidebar-element" onClick={() => setCurrent("Phone")}> Phone </div>
            <div className="sidebar-element" onClick={() => setCurrent("Computer")}> Computer </div>
            <div className="sidebar-element" onClick={() => setCurrent("Headphone")}> Headphone </div>
        </div>
    )
}