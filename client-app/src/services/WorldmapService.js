import { useGameAssetStore } from '../stores/GameAssetStore'
import { useWorldStore } from '../stores/WorldStore';
import { useAuthStore } from '../stores/AuthStore';
import { useUIStore } from '../stores/UIStore';

import { initHex, spawnSite } from '../threejs/ActorManager'
import { siteTypeToURI } from "../classes/Worldmap";
import { HexVector } from '../classes/HexVector'
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader'
import { TextureLoader } from 'three';
import { usePartyStore } from '../stores/PartyStore';
import { Hexes3d } from '../stores/000Singletons';

export async function requestPartyVision(q, r) {
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
            axialCoordinates: {
                Q: q,
                R: r
            },
            siteType: 0
        })
    }
    const URL = (!useUIStore().devMode) ? "/api/party/vision" : "/api/party/vision.json"
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
        data.forEach(element => {
            const vector = new HexVector(element.Q, element.R)
            const texLoader = new TextureLoader()

            // spawn hex object3d
            initHex(loader, texLoader, worldStore, useGameAssetStore(), vector, vector.Q%3)

            // spawn site object3d
            if (element.site){
                worldStore.previewModelURI = siteTypeToURI(element.site.type)
                spawnSite(worldStore, useGameAssetStore(), vector)
            }
        })
        usePartyStore().updatePawn3dPosition()
    })
}

export async function requestTravel(q, r) {
    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${useAuthStore().token}`
        },
        body: JSON.stringify([{
            axialCoordinates: { Q: q, R: r},
            siteType: 0
        }])
    }
    await fetch("/api/party/travel", options)
    .then( (response) => { 
        if (response.ok) {
            usePartyStore().travelOK = true
            usePartyStore().party.location = {
                Q: useWorldStore().clickedItem.userData.Q,
                R: useWorldStore().clickedItem.userData.R
            }
            console.log("Traveling...") 
        }
    })
    .catch(error => { 
        console.log(error) 
    })
}