import { Link } from 'react-router'

function Login() {
    return (
        <div className={"flex flex-col p-4 justify-center h-dvh"}>
            <form className={"flex flex-col gap-2 p-4 border-black border-2 rounded-sm"}>
                <h1 className={"text-6xl text-center p-4 mb-4"}>Sign In</h1>
                <label>Email</label>
                <input name={"email"} placeholder={"example@example.com"} className={"border-black border-2 p-2 rounded-sm"}/>
                <label>Password</label>
                <input name={"password"} placeholder={"abc123"} className={"border-black border-2 p-2 rounded-sm"}/>
                <button className={"border-black border-2 p-2 w-1/2 self-center rounded-lg mt-2 mb-2"} type={"submit"}>Login</button>
                <p className={"self-center p-2"}>Don't have an account? <Link to={"/register"} className={"underline text-blue-500 hover:no-underline active:text-blue-800"}>Sign up</Link></p>
            </form>
        </div>
    )
}

export default Login