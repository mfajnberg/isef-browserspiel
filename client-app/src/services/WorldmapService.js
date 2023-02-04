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
    await fetch("/api/party/vision", options)
    .then(async response => { 
        worldStore.response = response
        return await response.json()
    })
    .then(data => {
        console.log("Updating sites for editing...") 
        if (worldStore.sites3d.length > 0) {
            worldStore.sites3d.forEach(object3D => {
                disposeObject(object3D)
                function disposeObject(obj3d) {
                    obj3d.children.forEach(child => {
                        disposeObject(child);
                    });
                    obj3d.geometry && obj3d.geometry.dispose()
                    obj3d.material && obj3d.material.dispose()
                    obj3d.texture && obj3d.texture.dispose()
                    try {obj3d.parent.remove(obj3d)} catch(e) {}
                    try {obj3d.dispose()} catch(e) {}
                }
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