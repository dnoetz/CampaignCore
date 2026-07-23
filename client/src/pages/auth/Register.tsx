import { Link, useNavigate } from 'react-router'
import { ChangeEvent, useState, FormEvent } from 'react'
import { register } from '../../api/auth.ts'

function Register() {
    const navigate = useNavigate();
    interface RegisterData {
        username: string;
        firstName: string;
        lastName: string;
        password: string;
        confirmPassword: string;
        email: string;
        birthday: string
    };

    const [formData, setFormData] = useState<RegisterData>({
        username: '', 
        firstName: '', 
        lastName: '', 
        password: '', 
        confirmPassword: '',
        email: '', 
        birthday: ''
    });

    const handleInputChange = (e: ChangeEvent<HTMLInputElement>) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value
        });
    };
    
    const [error, setError] = useState<string | null>(null);

    const handleRegistration = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        if (formData.password != formData.confirmPassword) {
            setError("Passwords do not match!");
            return;
        }
        try {
            await register(formData.username, formData.firstName, formData.lastName, formData.password, formData.email, new Date(formData.birthday));
            navigate('/user/login');
        } catch {
            setError("Registration failed — that email or username may be taken");
        }
    };
    
    return (
        <div className={"flex flex-col p-4 justify-center h-dvh"}>
            <form className={"flex flex-col gap-2 p-4 border-black border-2 rounded-sm"} onSubmit={handleRegistration}>
                <h1 className={"text-6xl text-center p-2 mb-4"}>Sign Up</h1>
                <div className={"flex flex-row justify-between"}>
                    <div className={"flex flex-col w-3/7"}>
                        <label>First Name</label>
                        <input name={"firstName"} placeholder={"John"} className={"border-black border-2 p-2 rounded-sm"} onChange={handleInputChange}/>
                    </div>
                    <div className={"flex flex-col w-5/9"}>
                        <label>Last Name</label>
                        <input name={"lastName"} placeholder={"Doe"} className={"border-black border-2 p-2 rounded-sm"} onChange={handleInputChange}/>
                    </div>
                </div>
                <div className={"flex flex-col"}>
                    <label>Email</label>
                    <input name={"email"} placeholder={"example@example.com"} className={"border-black border-2 p-2 rounded-sm"} onChange={handleInputChange}/>
                </div>
                <div className={"flex flex-col"}>
                    <label>Birthday</label>
                    <input name={"birthday"} placeholder={"MM-DD-YYYY"} className={"border-black border-2 p-2 rounded-sm"} onChange={handleInputChange}/>
                </div>
                <div className={"flex flex-col"}>
                    <label>Username</label>
                    <input name={"username"} placeholder={"example"} className={"border-black border-2 p-2 rounded-sm"} onChange={handleInputChange}/>
                </div>
                <div className={"flex flex-col"}>
                    <label>Password</label>
                    <input name={"password"} placeholder={"abc123"} className={"border-black border-2 p-2 rounded-sm"} onChange={handleInputChange}/>
                </div>
                <div className={"flex flex-col"}>
                    <label>Confirm Password</label>
                    <input name={"confirmPassword"} placeholder={"abc123"} className={"border-black border-2 p-2 rounded-sm"} onChange={handleInputChange}/>
                </div>
                <button className={"border-black border-2 p-2 w-1/2 self-center rounded-lg mt-2 mb-2"} type={"submit"}>Create Account</button>
                <p className={"self-center p-2"}>Already have an account? <Link to={"/user/login"} className={"underline text-blue-500 hover:no-underline active:text-blue-800"}>Sign in</Link></p>
            </form>
        </div>
    )
}

export default Register