export default function CampaignAction(props) {
    const dateObj = new Date(props.campaignAction.timestamp);
    const formattedDateTime = dateObj.toLocaleString();
    
    return (
        <div className={"border-1 border-gray-500/25 p-2 rounded-md"}>
            <div className={"flex flex-row gap-2"}>
                <p className={"text-gray-500"}>{props.campaignAction.id}.</p>
                <div className={"flex flex-col gap-"}>
                    <p className={"text-blue-800"}>{props.campaignAction.narrative}</p>
                    <p className={"text-red-600"}>{props.campaignAction.result}</p>
                </div>
            </div>
            <div className={"flex flex-row justify-between mt-2"}>
                <p className={"text-xs text-gray-500"}>{formattedDateTime}</p>
                <p className={"text-xs text-gray-500"}>{props.campaignAction.actionType}</p>
            </div>
        </div>
    )
}