import type { CharacterSummary } from '../types/character.ts'

export function CharacterSummaryCard(props: { character: CharacterSummary}) {
    return(
        <div className={"flex flex-row justify-between text-xl"}>
            <p>{props.character.name}</p>
            <div className={"flex flex-row justify-end gap-2"}>
                <p>Level {props.character.level}</p>
                <p>{props.character.playerClass}</p>
            </div>
        </div>
    )
}

export default CharacterSummaryCard