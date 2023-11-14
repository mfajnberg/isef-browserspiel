import { useGameAssetStore } from '../stores/GameAssetStore'
import { useWorldStore } from '../stores/WorldStore';
import { useAuthStore } from '../stores/AuthStore';
import { useUIStore } from '../stores/UIStore';
import {TileDTOs } from "../stores/000Singletons";

import { spawnHex, spawnSite } from '../threejs/ActorManager'
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
            AxialCoordinates: {Q: q, R: r},
            SiteType: 0
        })
    }
    const URL = (!useUIStore().devMode) ? "/api/admin/world/get" : "/api/party/vision.json" 
    await fetch(URL, options)
    .then(async response => { 
        worldStore.response = response
        return await response.json()
    })
    .then(async data => {
        worldStore.disposeAllTiles()
        const loader = new GLTFLoader()

        // set hex model reference
        if (!useUIStore().devMode) {
            for (let asset of gameAssetStore.assets3d) {
                if (asset.name == "HexBase.glb") {
                    gameAssetStore.hexModel = asset.data
                }
            }
        }
        
        let vectors = Worldmap.makeHexGridVectors(4)
        const texLoader = new TextureLoader()
        vectors.forEach(vec => {
            console.log(vec.Q + "||" + vec.R)
            const hexPosition = new HexVector(vec.Q + data[0].Q, vec.R + data[0].R)
            spawnHex(loader, texLoader, worldStore, useGameAssetStore(), hexPosition, (69*hexPosition.Q + 420*hexPosition.R)%3)
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