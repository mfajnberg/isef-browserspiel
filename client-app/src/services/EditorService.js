import { GLTFLoader } from "three/examples/jsm/loaders/GLTFLoader";
import { HexVector } from "../classes/HexVector";
import { Sites } from "../stores/000Singletons";
import { spawnSite } from "../threejs/ActorManager";

export async function requestGetWorldSliceAdmin(authStore, worldStore) {
    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${authStore.token}`,
        },
        credentials: 'include',
        body: JSON.stringify({
            axialCoordinates: {
                Q: worldStore.getAbsoluteZeroOffset.R,
                R: worldStore.getAbsoluteZeroOffset.Q
            },
            siteType: 0
        })
    }
    await fetch("/api/admin/world/get", options)
    // await fetch("/api/admin/world/get.json")
        .then(async response => { 
            worldStore.response = response
            await response.json()
        })
        .then(data => {
            const loader = new GLTFLoader()
            data.forEach(element => {
                console.log(element)
                if (element.site){
                    if (element.site.type === 100) {
                    spawnSite(loader, worldStore.scene, worldStore, new HexVector(element.Q, element.R), 'forest_1.glb')
                }
                }
            });
        }).catch(error => { console.log(error) })
}

export async function requestWorldSave(authStore) {
    const sites = new Sites()
    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${authStore.token}`,
        },
        credentials: 'include',
        body: JSON.stringify(sites.buffer)
    }
    await fetch("/api/admin/world/save", options)
        .then(response => { return response.text() })
        .then(data => { console.log(data) })
        .catch(error => { console.log(error) })
}