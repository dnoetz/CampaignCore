import { useState, useEffect } from 'react'
import type { ChangeEvent, FormEvent } from 'react'
import { useParams } from 'react-router'
import type { PlayableCampaignInfo } from '../../types/campaign.ts'
import type { CombatRequest } from '../../types/combat.ts'
import { getPlayableCampaign, rollSix, rollTwenty, ExecuteTurn } from '../../api/campaign.ts'
import CampaignActionComponent from '../../components/CampaignActionComponent.tsx'

export default function PlayCampaign() {
    const { id } = useParams();

    const [campaignInfo, setCampaignInfo] = useState<PlayableCampaignInfo>({
        id: Number(id),
        name: '',
        campaignCode: '',
        characters: [],
        campaignActions: []
    })
    
    const [combatRequest, setCombatRequest] = useState<CombatRequest>({
        campaignId: Number(id),
        characterId: 0,
        abilityName: '',
        initiative: 0,
        roll: 0,
        narrative: ''
    })

    async function loadCampaign() {
        const campaign = await getPlayableCampaign(Number(id));
        setCampaignInfo({
            id: Number(id),
            name: campaign.name,
            campaignCode: campaign.campaignCode,
            characters: campaign.characters,
            campaignActions: campaign.campaignActions,
        });
    }

    useEffect(() => { loadCampaign(); }, [id]);

    const handleRollSix = async () => {
        const result = await rollSix();
        setCombatRequest({
            ...combatRequest,
            roll: result
        });
    };

    const handleRollTwenty = async () => {
        const result = await rollTwenty();
        setCombatRequest({
            ...combatRequest,
            initiative: result
        });
    };

    const handleSelectChange = (e: ChangeEvent<HTMLSelectElement>) => {
        setCombatRequest({
            ...combatRequest,
            [e.target.name]: e.target.value
        })
    }

    const handleInputChange = (e: ChangeEvent<HTMLInputElement>) => {
        setCombatRequest({
            ...combatRequest,
            [e.target.name]: e.target.value
        });
    };

    const SubmitCombatRequest = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        await ExecuteTurn(combatRequest.campaignId, Number(combatRequest.characterId), combatRequest.abilityName, combatRequest.initiative, combatRequest.roll, combatRequest.narrative);
        setCombatRequest({
            ...combatRequest,
            initiative: 0,
            roll: 0,
        })
        await loadCampaign();
        setCombatRequest({ ...combatRequest, initiative: 0, roll: 0 });
    };

    return (
        <div className={"p-4 h-[94vh] flex flex-col gap-4"}>
            <h1 className={"text-center text-4xl"}> {campaignInfo.name}</h1>
            <div className={"border-2 border-black p-4 mt-4 flex flex-col gap-2 h-1/2"}>
                <h3 className={"text-center text-2xl font-bold"}>Campaign Actions</h3>
                <div className={"flex flex-col gap-2 overflow-scroll"}>
                    {campaignInfo.campaignActions.map((action) => <CampaignActionComponent key={action.id} campaignAction={action}/> )}
                </div>
            </div>
            <form className={"border-2 border-black h-2/5 flex flex-col gap-2"} onSubmit={SubmitCombatRequest}>
                <div className={"flex flex-row h-1/2 p-4"}>
                    <div className={"flex flex-col w-1/3 gap-3 justify-between"}>
                        <div>
                            <button type={"button"} className={"border-2 border-black p-2 rounded-md disabled:bg-gray-400"} onClick={handleRollTwenty} disabled={combatRequest.initiative !== 0}>Roll Initiative</button>
                            <span>{combatRequest.initiative}</span>
                        </div>
                        <div>
                            <button type={"button"} className={"border-2 border-black p-2 rounded-md disabled:bg-gray-400"} onClick={handleRollSix} disabled={combatRequest.roll !== 0}>Roll Damage</button>
                            <span>{combatRequest.roll}</span>
                        </div>
                    </div>
                    <div className={"w-[70%] ml-4"}>
                        <input name={"narrative"} className={"border-2 border-gray-500/50 h-full w-full"} onChange={handleInputChange}/>
                    </div>
                </div>
                <div className={"flex flex-row h-1/8 justify-around"}>
                    <select name={"characterId"} value={combatRequest.characterId} onChange={handleSelectChange} className={"border-2 border-black rounded-md w-[46%]"}>
                        <option value="">-- pick --</option>
                        {campaignInfo.characters.map((character) => <option key={character.id} value={character.id}>{character.name}</option>)}
                    </select>
                    <select name={"abilityName"} value={combatRequest.abilityName} onChange={handleSelectChange} className={"border-2 border-black rounded-md w-[46%]"}>
                        <option value="">-- pick --</option>
                        <option value={"ReapersMark"}>Reaper's Mark</option>
                        <option value={"Necrosis"}>Necrosis</option>
                    </select>
                </div>
                <button className={"border-2 border-black m-4 rounded-md"} type={"submit"}>Execute Turn</button>
            </form>
        </div>
    )
}