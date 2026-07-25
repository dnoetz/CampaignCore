import { apiFetch } from './client.ts'

export async function createCampaign(name: string) {
    return apiFetch('/api/campaigns/create-campaign', {
        method: 'POST',
        body: JSON.stringify({ name }),
    });
}

export async function getCampaign(id: number) {
    return apiFetch(`/api/campaigns/get-campaign/${id}`, {
        method: 'GET'
    })
}

export async function getAllCampaigns() {
    return apiFetch('/api/campaigns/all-campaigns', {
        method: 'GET'
    })
}

export async function createCharacter(playerClass: string, name: string, campaignCode: string) {
    return apiFetch('/api/campaigns/add-character-to-campaign', {
        method: 'POST',
        body: JSON.stringify({ playerClass, name, campaignCode }),
    });
}