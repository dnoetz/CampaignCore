import { createContext, useContext, useState } from 'react'
import type { ReactNode } from 'react'
import { login as apiLogin, saveToken, getToken } from '../api/auth'

interface AuthContextType {
    isAuthenticated: boolean
    login: (email: string, password: string) => Promise<void>
    logout: () => void
}

const AuthContext = createContext<AuthContextType | undefined>(undefined)

export function AuthProvider({ children }: { children: ReactNode }) {
    const [token, setToken] = useState<string | null>(getToken())

    async function login(email: string, password: string) {
        const data = await apiLogin(email, password)
        saveToken(data.token)
        setToken(data.token)
    }

    function logout() {
        localStorage.removeItem('token')
        setToken(null)
    }

    return (
        <AuthContext.Provider value={{ isAuthenticated: !!token, login, logout }}>
            {children}
        </AuthContext.Provider>
    )
}

export function useAuth() {
    const ctx = useContext(AuthContext)
    if (!ctx) throw new Error('useAuth must be used within AuthProvider')
    return ctx
}