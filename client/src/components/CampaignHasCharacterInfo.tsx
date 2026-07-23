import CharacterSummaryCard from './CharacterSummary.tsx'

export function NoCharacterInfo(props) {
    return(
        <div className={"p-4 flex flex-col gap-2"}>
            <p className={"text-xl"}>It doesn't look like there are any characters for this campaign! Invite some friends:</p>
            <p className={"text-center text-3xl font-bold"}>{props.campaignCode}</p>
        </div>
    )
}

export function CharacterInfo(props) {
    return(
        <div>
            <div className={"p-4"}>
                {
                    props.characters.map(character => 
                        <CharacterSummaryCard character={character} key={character.id}/>
                    )
                }
            </div>
            <p className={"text-center text-xl"}>Invite more friends: {props.campaignCode}</p>
        </div>
    )
}