import { Creature } from "../classes/Creature"

export async function requestAvatarChoices(creatorStore) {
    const options = {
        method: 'GET',
        headers: {
            'Content-Type': 'text/plain',
            'Authorization': `Bearer ${localStorage.getItem("token")}`
        },
        credentials: 'include',
    }
    
    await fetch("api/avatar/choices.json", options)
    .then((response) => response.json())
    .then(avatarChoices => {
        for (let choice of avatarChoices) {
            let avatar = new Creature(
                choice.name,
                choice.portrait,
                choice.intellect,
                choice.discipline,
                choice.power,
                choice.agility,
                choice.lucidity,
                choice.charisma,
                choice.alignment,
                choice.temperament,
                choice.morale
            )
            creatorStore.statBlocks.push(avatar)
        }
    })
}