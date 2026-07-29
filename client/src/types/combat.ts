export interface CombatRequest {
    campaignId: number;
    characterId: number;
    abilityName: string;
    initiative: number;
    roll: number;
    narrative: string;
}