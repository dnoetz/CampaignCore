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

export async function getPlayableCampaign(campaignId: number) {
    return apiFetch(`/api/campaigns/play-campaign/${campaignId}`, {
        method: 'GET'
    });
}

export async function rollSix() {
    return apiFetch('/api/dice/roll-six', {
        method: 'GET'
    });
}

export async function rollTwenty() {
    return apiFetch('/api/dice/roll-twenty', {
        method: 'GET'
    });
}

export async function ExecuteTurn(campaignId: number, characterId: number, abilityName: string, initiative: number, roll: number, narrative: string) {
    return apiFetch('/api/combat/combat-turn', {
        method: 'PUT',
        body: JSON.stringify({ campaignId, characterId, abilityName, initiative, roll, narrative }),
    });
}