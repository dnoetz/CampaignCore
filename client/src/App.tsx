import { createBrowserRouter, RouterProvider} from 'react-router'
import Home from './pages/Home.tsx'
import Login from './pages/auth/Login.tsx'
import Register from './pages/auth/Register.tsx'
import CreateCampaign from './pages/campaign/CreateCampaign.tsx'
import ViewCampaign from './pages/campaign/ViewCampaign.tsx'

const router = createBrowserRouter([
    {
        path: '/',
        element: <Home />
    },
    {
        path: 'login',
        element: <Login />
    },
    {
        path: 'register',
        element: <Register />
    },
    {
        path: 'create-campaign',
        element: <CreateCampaign />
    },
    {
        path: `get-campaign/:id`,
        element: <ViewCampaign />
    }
]);

export default function App() {
  return <RouterProvider router={router} />;
}
