import { GLTFLoader } from "three/examples/jsm/loaders/GLTFLoader";
import { HexVector } from "../classes/HexVector";
import { siteTypeToURI } from "../classes/Worldmap";
import { Sites } from "../stores/000Singletons";
import { useGameAssetStore } from "../stores/GameAssetStore";
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
            worldStore.disposeAll() 
            const loader = new GLTFLoader()
            data.forEach(element => {
                if (element.site){
                    worldStore.previewModelURI = siteTypeToURI(element.site.type)
                    spawnSite(loader, worldStore, useGameAssetStore(),
                        new HexVector(
                            element.Q - worldStore.getAbsoluteZeroOffset.Q, 
                            element.R - worldStore.getAbsoluteZeroOffset.R
                        ), element.site.type
                    )
                    
                }
            })
            worldStore.previewModelURI = "HexPreview2.glb"
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