import { Link, useNavigate } from 'react-router'
import { useAuth } from '../context/AuthContext.tsx'
export function Navbar() {
    const { isAuthenticated, logout } = useAuth();
    const navigate = useNavigate();
    
    const handleLogout = () => {
        logout();
        navigate('/user/login');
    }
    return(
        <div className={"border-b-1 border-black p-4 flex flex-row gap-2"}>
            <Link to={"/"} className={"underline text-blue-500 hover:no-underline active:text-blue-800"}>Home</Link>
            {!isAuthenticated &&
                <Link to={"/user/login"} className={"underline text-blue-500 hover:no-underline active:text-blue-800"}>Login</Link>}
            {!isAuthenticated &&
                <Link to={"/user/register"} className={"underline text-blue-500 hover:no-underline active:text-blue-800"}>Register</Link>}
            {isAuthenticated &&
                <Link to={"/character/create"} className={"underline text-blue-500 hover:no-underline active:text-blue-800"}>Join A Campaign</Link>}
            {isAuthenticated &&
                <Link to={"/campaigns/all"} className={"underline text-blue-500 hover:no-underline active:text-blue-800"}>All Campaigns</Link>}
            {isAuthenticated &&
                <p className={"underline text-blue-500 hover:no-underline active:text-blue-800"} onClick={handleLogout}>Logout</p>}
        </div>
    )
}