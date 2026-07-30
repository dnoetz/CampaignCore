import { useState, useEffect } from 'react'
import { useParams, useNavigate } from 'react-router'
import { getCampaign } from '../../api/campaign.ts'
import { NoCharacterInfo, CharacterInfo } from '../../components/CampaignHasCharacterInfo.tsx'
import type { CampaignInfo } from '../../types/campaign.ts'

function ViewCampaign() {
    const { id } = useParams();
    const navigate = useNavigate();
    
    const [campaignInfo, setCampaignInfo] = useState<CampaignInfo>({
        id: Number(id),
        name: '',
        campaignCode: '',
        characters: [],
        owner: {
            id: 0,
            username: ''
        }
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
    
    function handlePlayCampaign() {
        navigate(`/campaign/play/${Number(id)}`)
    }
    
    return(
        <div className={"flex flex-col h-dvh justify-center p-4 gap-3"}>
            <div className={"flex flex-col p-4 border-black border-2 gap-2"}>
                <h1 className={"text-center text-6xl "}>{campaignInfo.name}</h1>
                <h2 className={"text-center text-2xl"}>Owner: {campaignInfo.owner?.username}</h2>
                { campaignInfo.characters.length === 0 ? <NoCharacterInfo campaignCode={campaignInfo.campaignCode} /> : <CharacterInfo campaignCode={campaignInfo.campaignCode} characters={campaignInfo.characters}/>}
            </div>
            <div className={"flex flex-row justify-between"}>
                <button className={"border-2 w-1/3 border-gray-900/50 rounded-md text-lg p-1 hover:bg-gray-200/50 hover:cursor-pointer active:bg-gray-400/25"} onClick={handleNav}>Back to all</button>
                <button className={"border-2 w-1/3 border-gray-900/50 rounded-md text-lg p-1 hover:bg-gray-200/50 hover:cursor-pointer active:bg-gray-400/25"} onClick={handlePlayCampaign}>Play Campaign</button>
            </div>
        </div>
        )
}

export default ViewCampaign