import { Creature } from "../classes/Creature"

export async function fetchGetChoices(creatorStore) {
    const options = {
        method: 'GET',
        headers: {
            'Content-Type': 'text/plain',
            'Authorization': `Bearer ${localStorage.getItem("token")}`,
            credentials: 'include'
        },
        credentials: 'include',
    }
    
    await fetch("api/avatar/choices.json", options)
    .then((response) => response.json())
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

export async function requestPostChoice(authStore, creatorStore, name) {
    const options = {
        method: 'Post',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${authStore.token}`
        },
        credentials: 'include',
        body: JSON.stringify({
            name: name
        })
    }
    await fetch(`api/avatar/select`, options)
    .then(response => {
        creatorStore.creationResponse = response
        return response.text()
    }).then(text => {
        console.log(text)
    })
}