import { useGameAssetStore } from '../stores/GameAssetStore'
import { useWorldStore } from '../stores/WorldStore';
import { useAuthStore } from '../stores/AuthStore';
import { useUIStore } from '../stores/UIStore';
import {TileDTOs } from "../stores/000Singletons";

import { initHex, spawnSite } from '../threejs/ActorManager'
import { siteTypeToURI, Worldmap } from "../classes/Worldmap";
import { HexVector } from '../classes/HexVector'
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader'
import { TextureLoader } from 'three';

export async function requestGetWorldSliceAdmin(q, r) {
    const worldStore = useWorldStore()
    const gameAssetStore = useGameAssetStore()
    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${useAuthStore().token}`
        },
        credentials: 'include',
        body: JSON.stringify({
            axialCoordinates: {Q: q, R: r},
            siteType: 0
        })
    }
    const URL = (!useUIStore().devMode) ? "/api/admin/world/get" : "/api/party/vision.json" 
    await fetch(URL, options)
    .then(async response => { 
        worldStore.response = response
        return await response.json()
    })
    .then(async data => {
        worldStore.disposeAll()
        const loader = new GLTFLoader()

        // set hex model reference
        if (!useUIStore().devMode) {
            for (let asset of gameAssetStore.assets3d) {
                if (asset.name == "HexBase.glb") {
                    gameAssetStore.hexModel = asset.data
                }
            }
        }
        let vectors = Worldmap.makeHexGridVectors(5)
        const texLoader = new TextureLoader()
        
        vectors.forEach(hexPosition => {
            initHex(loader, texLoader, worldStore, useGameAssetStore(), hexPosition, hexPosition.Q%3)
        })

        data.forEach(element => {
            const vector = new HexVector(element.Q, element.R)

            // spawn site object3d
            if (element.site){
                worldStore.previewModelURI = siteTypeToURI(element.site.type)
                spawnSite(worldStore, useGameAssetStore(), vector)
            }
        })
    })
    .catch(error => { 
        console.log(error) 
    })
}

export async function requestWorldSave(authStore) {
    const sites = new TileDTOs()
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