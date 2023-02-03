import { Creature } from "../classes/Creature"

export async function requestGetChoices(authStore, creatorStore) {
    const options = {
        method: 'GET',
        headers: {
            'Content-Type': 'text/plain',
            'Authorization': `Bearer ${authStore.token}`,
        }
    }
    await fetch("api/avatar/choices", options)
    .then(async (response) => await response.json())
    .then(avatarChoices => {
        for (let choice of avatarChoices) {
            let avatar = new Creature(
                choice.name,
                choice.description,
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

export async function requestPostChoice(authStore, creatorStore, partyStore, name) {
    const options = {
        method: 'Post',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${authStore.token}`
        },
        body: JSON.stringify(name)
    }
    await fetch(`api/avatar/select`, options)
    .then(async response => { 
        creatorStore.creationResponse = response
        if (response.ok) {
            partyStore.avatar = await response.json()
        }
     })
}

// async function requestPartyCreate()