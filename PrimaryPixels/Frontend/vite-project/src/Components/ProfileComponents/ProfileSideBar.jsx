import './ProfileSideBar.scss';

export default function ProfileSideBar({ setPage }) {
    return (
        <div id='profile-side'>
            <button className='profile-buttons' onClick={() => setPage("profile")}>Profile</button>
            <button className='profile-buttons' onClick={() => setPage("orders")}>Orders</button>
        </div>
    )

}