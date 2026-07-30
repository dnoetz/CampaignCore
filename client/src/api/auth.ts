export async function login(email: string, password: string) {
    const res = await fetch('https://campaigncore-app-d0d6c2djh9dbeqby.canadacentral-01.azurewebsites.net/api/auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password }),
    })
    if (!res.ok) throw new Error('Login failed');
    return res.json();
}

export async function register(username: string, firstName: string, lastName: string, password: string, email: string, birthday: Date) {
    const res = await fetch('https://campaigncore-app-d0d6c2djh9dbeqby.canadacentral-01.azurewebsites.net/api/users/register-user', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, firstName, lastName, password, email, birthday })
    });
    if (!res.ok) throw new Error('Registration failed');
}

export function saveToken(token: string) {
    localStorage.setItem('token', token);
}

export function getToken() {
    return localStorage.getItem('token');
}