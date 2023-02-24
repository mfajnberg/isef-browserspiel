import { Creature } from "../classes/Creature"
import { useUIStore } from "../stores/UIStore"

export async function requestGetChoices(authStore, creatorStore) {
    if (!useUIStore().devMode) {
        var URL = "api/avatar/choices"
    } else {
        var URL = "api/avatar/choices.json"
    }
    const options = {
        method: 'GET',
        headers: {
            'Content-Type': 'text/plain',
            'Authorization': `Bearer ${authStore.token}`,
        }
    }
    await fetch(URL, options)
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
            const data = await response.json()
            partyStore.party = data.party
            partyStore.avatar = data.avatar
        }
     })
}

// async function requestPartyCreate()