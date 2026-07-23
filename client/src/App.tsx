import { createBrowserRouter, RouterProvider} from 'react-router'
import Home from './pages/Home.tsx'
import Login from './pages/auth/Login.tsx'
import Register from './pages/auth/Register.tsx'
import CreateCampaign from './pages/campaign/CreateCampaign.tsx'
import ViewCampaign from './pages/campaign/ViewCampaign.tsx'
import ViewAllCampaigns from './pages/campaign/ViewAllCampaigns.tsx'

const router = createBrowserRouter([
    {
        path: '/',
        element: <Home />
    },
    {
        path: 'user/login',
        element: <Login />
    },
    {
        path: 'user/register',
        element: <Register />
    },
    {
        path: 'campaign/create',
        element: <CreateCampaign />
    },
    {
        path: `campaign/:id`,
        element: <ViewCampaign />
    },
    {
        path: 'campaigns/all',
        element: <ViewAllCampaigns />
    }
]);

export default function App() {
  return <RouterProvider router={router} />;
}
