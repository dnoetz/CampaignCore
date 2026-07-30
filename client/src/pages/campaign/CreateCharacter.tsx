import { useNavigate } from 'react-router'
import { useState } from 'react'
import type { ChangeEvent, FormEvent } from 'react'
import type { CreateCharacter } from '../../types/character.ts'
import { createCharacter } from '../../api/campaign.ts'

export default function CreateCharacter() {
    const navigate = useNavigate();
    
    const [formData, setFormData] = useState<CreateCharacter>({
        playerClass: '',
        name: '',
        campaignCode: '',
    });

    const handleInputChange = (e: ChangeEvent<HTMLInputElement>) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value
        });
    };
    
    const handleSelectChange = (e: ChangeEvent<HTMLSelectElement>) => {
        setFormData({
            ...formData,
            playerClass: e.target.value
        })
    }

    const handleCharacterCreation = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        await createCharacter(formData.playerClass, formData.name, formData.campaignCode);
        navigate('/');
    };
    
    return (
        <div className={"flex flex-col p-4 justify-center h-dvh"}>
            <form className={"flex flex-col gap-2 p-4 border-black border-2 rounded-sm"} onSubmit={handleCharacterCreation}>
                <h1 className={"text-6xl text-center p-2 mb-4"}>Create Character</h1>
                <div className={"flex flex-col"}>
                    <p className={"text-center text-xl"}>Choose Your Class</p>
                    <select id={"playerClass"} value={formData.playerClass} onChange={handleSelectChange} className={"border-2 border-black"}>
                        <option value={""}>--Select Class--</option>
                        <option value={"Necromancer"}>Necromancer</option>
                    </select>
                </div>
                <div className={"flex flex-col"}>
                    <label>Name</label>
                    <input name={"name"} className={"border-black border-2 p-2 rounded-sm"} placeholder={"Mortis"} onChange={handleInputChange}/>
                </div>
                <div className={"flex flex-col"}>
                    <label>Campaign Code</label>
                    <input name={"campaignCode"} className={"border-black border-2 p-2 rounded-sm"} placeholder={"123!ABC"} onChange={handleInputChange}/>
                </div>
                <button className={"border-black border-2 p-2 w-1/2 self-center rounded-lg mt-2 mb-2"} type={"submit"}>Create Character</button>
            </form>
        </div>
    )
}