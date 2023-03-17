import { useGameAssetStore } from '../stores/GameAssetStore'
import { useWorldStore } from '../stores/WorldStore';
import { useAuthStore } from '../stores/AuthStore';
import { useUIStore } from '../stores/UIStore';

import { spawnHex, spawnSite } from '../threejs/ActorManager'
import { siteTypeToURI } from "../classes/Worldmap";
import { HexVector } from '../classes/HexVector'
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader'
import { TextureLoader } from 'three';
import { usePartyStore } from '../stores/PartyStore';
import { TileDTOs } from '../stores/000Singletons';

export async function requestPartyVision(q, r) {
    const worldStore = useWorldStore()
    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${useAuthStore().token}`
        },
        credentials: 'include',
        body: JSON.stringify({
            AxialCoordinates: {
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
    .then(async newHexesData => {
        const oldDTOs = new TileDTOs().buffer
        let spawningVectors = []
        let deletionVectors = []

        for (let i = 0; i < oldDTOs.length; i++) {
            let matchFound = false
            var qOld = oldDTOs[i].AxialCoordinates.Q
            var rOld = oldDTOs[i].AxialCoordinates.R

            for (let j = 0; j < newHexesData.length; j++) {
                var qNew = newHexesData[j].Q
                var rNew = newHexesData[j].R
                if (qOld === qNew && rOld === rNew) {
                    matchFound = true
                    break
                }
            }
            if (!matchFound) {
                deletionVectors.push(new HexVector(qOld, rOld))
            }
        }
        for (let j = 0; j < newHexesData.length; j++) {
            let matchFound = false
            var qNew = newHexesData[j].Q
            var rNew = newHexesData[j].R

            for (let i = 0; i < oldDTOs.length; i++) {
                var qOld = oldDTOs[i].AxialCoordinates.Q
                var rOld = oldDTOs[i].AxialCoordinates.R

                if (qOld === qNew && rOld === rNew) {
                    matchFound = true
                    break
                }
            }
            if (!matchFound) {
                if (newHexesData[j].site)
                    spawningVectors.push(new HexVector(qNew, rNew, newHexesData[j].site.type))
                else {
                    spawningVectors.push(new HexVector(qNew, rNew))
                }
            }
        }
        const texLoader = new TextureLoader()
        const loader = new GLTFLoader()

        for (let deletionVector of deletionVectors) {
            worldStore.disposeTileAt(deletionVector)
        }
        for (let spawningVector of spawningVectors) {
            spawnHex(loader, texLoader, worldStore, useGameAssetStore(), spawningVector, (69*spawningVector.Q + 420*spawningVector.R)%3, spawningVector.siteType)
            if (spawningVector.siteType){
                worldStore.previewModelURI = siteTypeToURI(spawningVector.siteType)
                spawnSite(worldStore, useGameAssetStore(), spawningVector, spawningVector.siteType)
            }
        }        
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
            AxialCoordinates: { Q: q, R: r},
            SiteType: 0
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