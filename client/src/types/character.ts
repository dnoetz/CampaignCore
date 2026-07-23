export interface CharacterSummary {
    id: number;
    name: string;
    playerClass: string;
    level: number
}

export interface CharacterInfo {
    id: number;
    name: string;
    level: number;
    experienceToLevel: number;
    maxHitpoints: number;
    currentHitpoints: number;
    agility: number;
    intelligence: number;
    strength: number;
    vitality: number;
    charisma: number;
    playerClass: string;
    isDead: boolean;
}