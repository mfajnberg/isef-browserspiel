import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader'
import { HexVector } from '../classes/HexVector'
import { useGameAssetStore } from '../stores/GameAssetStore'
import { spawnSite } from '../threejs/ActorManager'
import { siteTypeToURI } from "../classes/Worldmap";

export async function requestGetHexTiles(authStore, worldStore) {
    const options = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${authStore.token}`
        }
    }
    await fetch("/api/party/vision", options)
    .then(async response => { 
        worldStore.response = response
        return await response.json()
    })
    .then(data => {
        worldStore.disposeAll()
        const loader = new GLTFLoader()
        data.forEach(element => {
            if (element.site){
                worldStore.previewModelURI = siteTypeToURI(element.site.type)
                spawnSite(loader, worldStore, useGameAssetStore(),
                    new HexVector(
                        element.Q - data[0].Q, // take the coordinates of the result's first element, it should not always be 0|0
                        element.R - data[0].R
                        // element.Q - worldStore.getAbsoluteZeroOffset.Q, // take the coordinates of the result's first element, it should not always be 0|0
                        // element.R - worldStore.getAbsoluteZeroOffset.R
                    )
                )
            }
        })
    })
    .catch(error => { 
        // console.log(error) 
    })
}

export async function requestTravel(authStore, worldStore) {
    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${authStore.token}`
        },
        body: JSON.stringify([{
            axialCoordinates: worldStore.getAbsoluteZeroOffset,
            siteType: 0
        }])
    }
    await fetch("/api/party/travel", options)
    .then( () => { 
        console.log("Traveling...") 
    })
    .catch(error => { 
        console.log(error) 
    })
}