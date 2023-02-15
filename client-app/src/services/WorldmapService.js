import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader'
import { HexVector } from '../classes/HexVector'
import { useGameAssetStore } from '../stores/GameAssetStore'
import { dispose, spawnSite } from '../threejs/ActorManager'

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
        console.log("Placing adventure sites...") 
        worldStore.disposeAll()
        const loader = new GLTFLoader()
        data.forEach(element => {
            if (element.site){
                if (element.site.type === 100) {
                    worldStore.previewModelURI = 'forest_1.glb'
                    spawnSite(loader, worldStore, useGameAssetStore(),
                        new HexVector(element.Q, element.R))
                }
            }
        })
    })
    .catch(error => { 
        // console.log(error) 
    })
}