import { useNavigate } from 'react-router'
import { ChangeEvent, useState } from 'react'
import { createCampaign } from '../../api/campaign.ts'

function CreateCampaign() {
    const navigate = useNavigate();
    const [name, setName] = useState<string>('');

    const handleInputChange = (e: ChangeEvent<HTMLInputElement>) => {
        setName(e.target.value);
    };

    const handleCampaignCreate = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const data = await createCampaign(name);
        navigate('/');
    };
    
    return (
        <div className={"h-dvh p-4 flex flex-col place-content-center"}>
            <form className={"flex flex-col gap-2 p-4 border-black border-2 rounded-sm"} onSubmit={handleCampaignCreate}>
                <h1 className={"text-6xl text-center p-2 mb-4"}>Name Your Campaign</h1>
                <input name={"name"} placeholder={"Campaign Name"} className={"border-black border-2 p-2 rounded-sm"} onChange={handleInputChange}/>
                <button className={"border-black border-2 p-2 w-1/2 self-center rounded-lg mt-2 mb-2"} type={"submit"}>Create Campaign</button>
            </form>
        </div>
    )
}

export default CreateCampaign