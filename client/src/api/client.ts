import { getToken } from './auth.ts'

const BASE_URL = 'https://campaigncore-app-d0d6c2djh9dbeqby.canadacentral-01.azurewebsites.net';

export async function apiFetch(path: string, options: RequestInit = {}) {
    const token = getToken();
    const res = await fetch(`${BASE_URL}${path}`, {
        ...options,
        headers: {
            'Content-Type': 'application/json',
            ...(token ? { Authorization: `Bearer ${token}` } : {}),
            ...options.headers,
        },
    });
    if (!res.ok) throw new Error(`Request failed: ${res.status}`);
    const text = await res.text();
    return text ? JSON.parse(text) : null;
}