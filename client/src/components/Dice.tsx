import { useState } from 'react'

export default function Dice() {
    const [initiative, setInitiative] = useState<number>(0);
    const [damage, setDamage] = useState<number>(0);
    
    
    return (
        <div className={"flex flex-col m-4 h-1/2 w-1/3 gap-3"}>
            <div>
                <button className={"border-2 border-black p-1 rounded-md"}>Roll Initiative</button>
            </div>
            <div>
                <button className={"border-2 border-black p-1 rounded-md"}>Roll Damage</button>
            </div>
        </div>
    )
}