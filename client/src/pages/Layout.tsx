import { Outlet } from 'react-router'
import { Navbar } from '../components/Navbar.tsx'

export default function Layout() {
    return (
        <>
            <Navbar />
            <Outlet />
        </>
    )
}