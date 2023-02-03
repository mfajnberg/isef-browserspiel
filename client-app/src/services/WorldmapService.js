import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader'
import { HexVector } from '../classes/HexVector'
import { spawnSite } from '../threejs/ActorManager'

export async function requestGetHexTiles(authStore, worldStore) {
    const options = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${authStore.token}`
        }
    }
    console.log(authStore.token)

    await fetch("/api/party/vision", options)
        .then(async response => {
            return await response.json()
        })
        .then(data => {
            console.log("Creating GLTFLoader...") 
            const loader = new GLTFLoader()
            worldStore.sites3d.forEach(site => {
                object3D.children.forEach(child => {
                    disposeObject3D(child)
                })
                object3D.geometry && object3D.geometry.dispose()
                object3D.material && object3D.material.dispose()
                object3D.texture && object3D.texture.dispose()
                object3D.parent.remove(object3D)
                object3D.dispose()
            })
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
        .catch(error => { console.log(error) })
}