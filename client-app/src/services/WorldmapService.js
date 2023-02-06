import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader'
import { HexVector } from '../classes/HexVector'
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
        console.log("Updating sites for editing...") 
        // factor out into world store method
        if (worldStore.sites3d.length > 0) {
            worldStore.sites3d.forEach(object3D => {
                dispose(object3D)
            })
        }
        worldStore.sites3d = []
        const loader = new GLTFLoader()
        data.forEach(element => {
            console.log(element)
            if (element.site){
                if (element.site.type === 100) {
                    spawnSite(loader, worldStore.scene, worldStore, 
                        new HexVector(element.Q, element.R), 'forest_1.glb')
                }
            }
        })
    })
    .catch(error => { 
        // console.log(error) 
    })
}