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
            axialCoordinates: worldStore.getAbsoluteZeroOffset,
            siteType: 0
        })
    }
    await fetch("/api/admin/world/get", options)
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