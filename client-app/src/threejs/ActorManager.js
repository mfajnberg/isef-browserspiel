import * as THREE from 'three'
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader'
import { FBXLoader } from 'three/examples/jsm/loaders/FBXLoader'

import { URIToSiteType, Worldmap } from '../classes/Worldmap'
import {TileDTOs, Sites3d, Hexes3d } from '../stores/000Singletons'
import { useGameAssetStore } from '../stores/GameAssetStore'
import { AnimationLoader } from 'three'
import { useUIStore } from '../stores/UIStore'
import { usePartyStore } from '../stores/PartyStore'
import { useWorldStore } from '../stores/WorldStore'
import { HexVector } from '../classes/HexVector'
import { randFloat } from 'three/src/math/MathUtils'

export function spawnHex(loader, texLoader, worldStore, gameAssetStore, hexData, randomRotation, siteType) {
    loader.parse(gameAssetStore.getHexModel, '', (loadedObject) => {
        const spawnedHex = loadedObject.scene
        spawnedHex.traverse(child => {
            if (child.isMesh) {
                child.material = new THREE.MeshStandardMaterial({
                    map: texLoader.load(gameAssetStore.hexTextureURIs[Math.abs(randomRotation)])
                })
                child.material.map.wrapS = THREE.RepeatWrapping
                child.material.map.wrapT = THREE.ClampToEdgeWrapping
                child.material.map.offset.set(.5, .5)
                child.material.map.repeat.set(.5, .5)
                child.receiveShadow = true
            }
        })
        spawnedHex.translateX(hexData.getWorldXFromAxialQ())
        spawnedHex.translateZ(hexData.getWorldZFromAxialR())
        spawnedHex.name = `${hexData.Q}|${hexData.R}`
        spawnedHex.userData.Q = hexData.Q
        spawnedHex.userData.R = hexData.R
        spawnedHex.userData.isBlocked = false
        spawnedHex.userData.siteType = siteType
        spawnedHex.userData.FCost = 0
        spawnedHex.userData.GCost = 0
        spawnedHex.userData.HCost = 0
        spawnedHex.userData.CameFrom = null
        const partyStore = usePartyStore()
        if (hexData.Q === partyStore.party.location.Q
            && hexData.R === partyStore.party.location.R) 
        {
            partyStore.start = spawnedHex
        }      
        worldStore.scene.add(spawnedHex)
        new Hexes3d().buffer.push(spawnedHex)
        new TileDTOs().buffer.push({
            AxialCoordinates: {
                // Q: hexData.Q + worldStore.getAbsoluteZeroOffset.Q,
                // R: hexData.R + worldStore.getAbsoluteZeroOffset.R
                Q: hexData.Q,
                R: hexData.R
            },
            SiteType: siteType
        })
    })
}

export async function loadPawn3d() {
    for (let asset of useGameAssetStore().assets3d) {
        if (asset.name == "Arissa.fbx")
            var model = asset.data
    }
    const modelBlob = new Blob([model], { type: 'application/octet-stream' })
    if (!useUIStore().devMode)
        var modelURL = URL.createObjectURL(modelBlob)
    
    else {
        var modelURL = "Arissa.fbx"
    }
    
    new FBXLoader().load(modelURL, (loadedObject => {
        loadedObject.scale.set(.005, .005, .005)
        loadedObject.traverse((child) => {
            if (child.isMesh) {
            child.castShadow = true
            child.receiveShadow = true
            }
        })
        let partyPos = new HexVector(usePartyStore().party.location.Q, 
                                     usePartyStore().party.location.R)
        loadedObject.translateX(partyPos.getWorldXFromAxialQ())
        loadedObject.translateZ(partyPos.getWorldZFromAxialR())
        loadedObject.rotateY(.5236)
        usePartyStore().pawn3d = loadedObject
        playAnim(useWorldStore(), 'Idle.fbx')
        useWorldStore().scene.add(loadedObject)
    }))
}

export function playAnim(worldStore, animationURL) {
    for (let asset of useGameAssetStore().assets3d) {
        if (asset.name == animationURL) {
            var anim = asset.data
        }
    }
    const animBlob = new Blob([anim], { type: 'application/octet-stream' })
    let animURL = URL.createObjectURL(animBlob)

    if (!useUIStore().devMode) {} 
        else { animURL = animationURL}

    new FBXLoader().load(animURL, (loadedAnim) => {
        worldStore.animationMixer = new THREE.AnimationMixer(usePartyStore().pawn3d)
        const action = worldStore.animationMixer.clipAction(loadedAnim.animations[0])
        action.play()
    })
}

export async function loadHexCursor() {
    const worldStore = useWorldStore()
    try {
        dispose(worldStore.cursor)
    } catch (e) {}
    
    if (!useUIStore().devMode) {
        for (let asset of useGameAssetStore().assets3d) {
            if (asset.name == "3dCursorArrow.glb") {
                var model = asset.data
            }
        }
        var modelBlob = new Blob([model], { type: 'application/octet-stream' })
        var modelURL = URL.createObjectURL(modelBlob)
    }
    else {
        var modelURL = "3dCursorArrow.glb"
    }
    new GLTFLoader().load(modelURL, (loadedObject => {
        worldStore.cursor = loadedObject.scene
        worldStore.scene.add(loadedObject.scene)
    }))
}

export async function loadSitePreview(worldStore) {
    try {
        dispose(worldStore.preview)
    } catch (error) { console.log(error)}
    
    if (!useUIStore().devMode) {
        for (let asset of useGameAssetStore().assets3d) {
            if (asset.name == worldStore.previewModelURI) {
                var model = asset.data
            }
        }
    }
    else {
        model = await (await fetch("3dCursorCross.glb")).arrayBuffer()
    }

    new GLTFLoader().parse(model, '', (loadedObject => {
        loadedObject.scene.traverse((child) => {
            if (child.isMesh) {
            child.castShadow = false
            }
        })
        loadedObject.scene.visible = true
        worldStore.preview = loadedObject.scene
        worldStore.scene.add(loadedObject.scene)
    }))
}

export async function spawnSite(worldStore, gameAssetStore, hexVector, siteType) {
    if (!useUIStore().devMode) {
        for (let asset of gameAssetStore.assets3d) {
            if (asset.name == worldStore.previewModelURI) {
                var model = asset.data
            }
        }
    }
    else {
        model = await (await fetch("flag.glb")).arrayBuffer()
    }
    new GLTFLoader().parse(model, '', (loadedObject => {
        loadedObject.scene.traverse((child) => {
            if (child.isMesh) {
                child.castShadow = true
            }
        })
        worldStore.scene.add(loadedObject.scene)
        
        loadedObject.scene.translateX(hexVector.getWorldXFromAxialQ())
        loadedObject.scene.translateZ(hexVector.getWorldZFromAxialR())

        loadedObject.scene.rotateY(randFloat(1, 999))
        loadedObject.scene.userData.hexVector = hexVector

        // occupy correct hex object3d
        for (let hex of new Hexes3d().buffer) {
            if (hex.userData.Q === hexVector.Q && hex.userData.R === hexVector.R) {
                hex.userData.siteType = siteType
                hex.userData.isBlocked = true
            }
        }
        // set type in sites.buffer
        const tileDTOs = new TileDTOs()
        for (let tileDTO of tileDTOs.buffer) {
            if (tileDTO.AxialCoordinates.Q === hexVector.Q && 
                tileDTO.AxialCoordinates.R === hexVector.R) {
                    if (siteType != undefined)
                        tileDTO.SiteType = siteType
                    else {
                        tileDTO.SiteType = URIToSiteType(worldStore.previewModelURI)
                    }
            }
        }

        // save reference of new site to worldStore
        const sites3d = new Sites3d()
        sites3d.buffer.push(loadedObject.scene)
    }))
}

export function dispose(object3d) {
    try {
        object3d.children.forEach(child => {
            dispose(child);
        });
        object3d.geometry && object3d.geometry.dispose()
        object3d.material && object3d.material.dispose()
        object3d.texture && object3d.texture.dispose()
        try {object3d.parent.remove(object3d)} catch(e) {}
        try {object3d.dispose()} catch(e) {}
    } catch {}
}