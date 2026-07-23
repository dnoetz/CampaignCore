import { useState, useEffect } from 'react'
import { getAllCampaigns } from '../../api/campaign.ts'
import CampaignSummaryCard from '../../components/CampaignSummaryCard.tsx'
import type { Campaign } from '../../types/campaign.ts'
function ViewAllCampaigns() {

    const [campaigns, setCampaigns] = useState<Campaign[]>([])

    useEffect(() => {
        async function load () {
            const campaigns = await getAllCampaigns()
            setCampaigns(campaigns)
        }
        load();
    }, []);
    
    return(
        <div className={"p-4 flex flex-col h-dvh justify-center"}>
            <div className={"p-4 gap-4 flex flex-col"}>
                <h1 className={"text-5xl text-center"}>Campaign List</h1>
                <div className={"flex flex-col gap-2 p-4 border-2 rounded-lg border-gray-500/50"}>
                    <p className={"flex flex-row justify-between font-bold text-xl"}><span>Campaign Name</span><span>Share Code</span></p>
                    <div className={"flex flex-col gap-2"}>
                        {campaigns.map(campaign => <CampaignSummaryCard campaign={campaign} key={campaign.id}/>)}
                    </div>
                </div>
            </div>
        </div>
    )
}

export default ViewAllCampaigns