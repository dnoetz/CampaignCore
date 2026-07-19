import { Link, useNavigate } from 'react-router'
import { ChangeEvent, useState, FormEvent } from 'react'
import { login } from '../../api/auth.ts'

function Login() {
    const navigate = useNavigate();
    interface LoginData {
        email: string;
        password: string;
    };
    
    const [formData, setFormData] = useState<LoginData>({
        email: '',
        password: ''
    });
    
    const handleInputChange = (e: ChangeEvent<HTMLInputElement>) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value
        });
    };
    
    const handleLogin = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const data = await login(formData.email, formData.password);
        localStorage.setItem('token', data.token);
        navigate('/');
    };
    
    return (
        <div className={"flex flex-col p-4 justify-center h-dvh"}>
            <form className={"flex flex-col gap-2 p-4 border-black border-2 rounded-sm"} onSubmit={handleLogin}>
                <h1 className={"text-6xl text-center p-2 mb-4"}>Sign In</h1>
                <div className={"flex flex-col"}>
                <label>Email</label>
                <input name={"email"} placeholder={"example@example.com"} className={"border-black border-2 p-2 rounded-sm"} onChange={handleInputChange}/>
                </div>
                <div className={"flex flex-col"}>
                <label>Password</label>
                <input name={"password"} placeholder={"abc123"} className={"border-black border-2 p-2 rounded-sm"} onChange={handleInputChange}/>
                </div>
                <button className={"border-black border-2 p-2 w-1/2 self-center rounded-lg mt-2 mb-2"} type={"submit"}>Login</button>
                <p className={"self-center p-2"}>Don't have an account? <Link to={"/register"} className={"underline text-blue-500 hover:no-underline active:text-blue-800"}>Sign up</Link></p>
            </form>
        </div>
    )
}

export default Login