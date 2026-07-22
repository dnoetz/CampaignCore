export function CharacterSummary(props) {
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

export default CharacterSummary