import { useState, useEffect } from 'react'
import { useParams, useNavigate } from 'react-router'
import { getCampaign } from '../../api/campaign.ts'
import { NoCharacterInfo, CharacterInfo } from '../../components/CampaignHasCharacterInfo.tsx'

function ViewCampaign() {
    const { id } = useParams();
    const navigate = useNavigate();
    interface CampaignInfo {
        name: string
        campaignCode: string
        characters: Array<object>
        owner: object
    }
    
    const [campaignInfo, setCampaignInfo] = useState<CampaignInfo>({
        name: '',
        campaignCode: '',
        characters: [],
        owner: {}
    })
    
    useEffect(() => {
        async function load () {
            const campaign = await getCampaign(Number(id))
            setCampaignInfo({
                ...campaignInfo,
                name: campaign.name,
                campaignCode: campaign.campaignCode,
                characters: campaign.characters,
                owner: campaign.owner
            });
        }
        load();
    }, [id]);
    
    function handleNav() {
        navigate('/campaigns/all')
    }
    
    return(
        <div className={"flex flex-col h-dvh justify-center p-4 gap-3"}>
            <div className={"flex flex-col p-4 border-black border-2 gap-2"}>
                <h1 className={"text-center text-6xl "}>{campaignInfo.name}</h1>
                <h2 className={"text-center text-2xl"}>Owner: {campaignInfo.owner.username}</h2>
                { campaignInfo.characters.length === 0 ? <NoCharacterInfo campaignCode={campaignInfo.campaignCode} /> : <CharacterInfo campaignCode={campaignInfo.campaignCode} characters={campaignInfo.characters}/>}
            </div>
            <button className={"border-2 w-1/3 border-gray-900/50 rounded-md text-lg p-1 hover:bg-gray-200/50 hover:cursor-pointer active:bg-gray-400/25"} onClick={handleNav}>Back to all</button>
        </div>
        )
}

export default ViewCampaign