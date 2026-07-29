import type { CharacterSummary } from './character.ts'
import type { Owner } from './user.ts'
export interface CampaignInfo {
    id: number;
    name: string;
    campaignCode: string;
    characters: Array<CharacterSummary>;
    owner: Owner;
}

export interface Campaign {
    id: number
    name: string
    campaignCode: string
}

export interface CampaignAction {
    id: number;
    narrative: string;
    actionType: string;
    result: string;
    timestamp: string;
}

export interface PlayableCampaignInfo {
    id: number;
    name: string;
    campaignCode: string;
    characters: Array<CharacterSummary>;
    campaignActions: Array<CampaignAction>;
}