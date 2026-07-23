import { useNavigate } from 'react-router'
import type { Campaign } from '../types/campaign.ts'

export default function CampaignSummaryCard(props: {campaign: Campaign}) {
    const navigate = useNavigate();
    
    function handleNav() {
        navigate(`/campaign/${props.campaign.id}`)
    }
    
    return(
        <div className={"flex flex-row justify-between border-1 border-gray-500/25 p-2 hover:bg-gray-200/50 hover:cursor-pointer active:bg-gray-400/25"} onClick={handleNav}>
            <p>{props.campaign.name}</p>
            <p>{props.campaign.campaignCode}</p>
        </div>
    )
}