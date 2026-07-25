import { createBrowserRouter, RouterProvider} from 'react-router'
import Home from './pages/Home.tsx'
import Login from './pages/auth/Login.tsx'
import Register from './pages/auth/Register.tsx'
import CreateCampaign from './pages/campaign/CreateCampaign.tsx'
import ViewCampaign from './pages/campaign/ViewCampaign.tsx'
import ViewAllCampaigns from './pages/campaign/ViewAllCampaigns.tsx'
import { AuthProvider } from './context/AuthContext.tsx'
import { ProtectedRoute } from './api/ProtectedRoute.tsx'
import { Navbar } from './components/Navbar.tsx'
import Layout from './pages/Layout.tsx'
import CreateCharacter from './pages/campaign/CreateCharacter.tsx'

const router = createBrowserRouter([
    {
        element: <Layout/>,
        children: [
            {path: '/', element: <Home/>},
            {path: 'user/login', element: <Login/>},
            {path: 'user/register', element: <Register/>},
            {
                element: <ProtectedRoute/>,
                children: [
                    {path: 'campaign/create', element: <CreateCampaign/>},
                    {path: `campaign/:id`, element: <ViewCampaign/>},
                    {path: 'campaigns/all', element: <ViewAllCampaigns/>},
                    {path: 'character/create', element: <CreateCharacter />}
                ],
            },
        ]
    }
]);

export default function App() {
  return (
      <AuthProvider>
          <RouterProvider router={router} />
      </AuthProvider>
  );
}
