import * as THREE from 'three'

import { HexVector } from './HexVector'
import { Worldmap } from './actors/Worldmap'
import { HexTile } from './actors/HexTile'
import { ActorBase } from './actors/ActorBase'
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader'

export async function initActors(scene, loader, worldStore, assetStore) {
    // dummy data
    // await worldStore.loadVisibleHexTiles()
    
    worldStore.worldmap = new Worldmap()
    const visible = Worldmap.makeHexGridVectors(5)
    
    await assetStore.loadHex()

    for (let i = 0; i < visible.length; i++) {
        initHex(loader, scene, worldStore, assetStore, visible[i], i%3)
    }

    loadHexCursor(loader, scene, worldStore, null)
    loadSitePreview(loader, scene, worldStore, null)
}

function initHex(loader, scene, worldStore, assetStore, hexData, randomRotation) {
    const newHex = new HexTile(hexData)
    loader.parse(assetStore.getHexModel, '', (loadedObject) => {
        loadedObject.scene.traverse((child) => {
            if (child.isMesh) {
                child.material = new THREE.MeshStandardMaterial({
                    map: assetStore.hexTextures[randomRotation],
                })
                child.material.map.wrapS = THREE.RepeatWrapping
                child.material.map.wrapT = THREE.ClampToEdgeWrapping
                child.material.map.offset.set(.5, .5)
                child.material.map.repeat.set(.5, .5)
                child.receiveShadow = true
            }
        })
        loadedObject.scene.translateX(hexData.getWorldXFromAxialQ())
        loadedObject.scene.translateZ(hexData.getWorldZFromAxialR())
        loadedObject.scene.userData.Q = hexData.Q
        loadedObject.scene.userData.R = hexData.R
        loadedObject.scene.userData.free = true
        loadedObject.scene.name = `${hexData.Q}|${hexData.R}`
        scene.add(loadedObject.scene)
        worldStore.hexes3d.push(loadedObject.scene)
    })

}

function loadHexCursor(loader, scene, worldStore) {

    loader.load('HexCursor.glb', (loadedObject => {
        loadedObject.scene.traverse((child) => {
            if (child.isMesh) {
            child.castShadow = true
            }
        })
        worldStore.cursor = loadedObject.scene
        scene.add(loadedObject.scene)
    }))
}

export function loadSitePreview(loader, scene, worldStore) {
    
    loader.load(worldStore.previewUrl, (loadedObject => {
        loadedObject.scene.traverse((child) => {
            if (child.isMesh) {
            child.castShadow = true
            }
        })
        loadedObject.scene.visible = false

        if (worldStore.preview != null) {
            worldStore.preview.removeFromParent()
        }
        
        worldStore.preview = loadedObject.scene
        scene.add(loadedObject.scene)
    }))
}

export function spawnActor(loader, scene, worldStore, hexVector, modelUrl) {
    loader.load(modelUrl, (loadedObject => {
        loadedObject.scene.traverse((child) => {
            if (child.isMesh) {
            child.castShadow = true
            }
        })
        scene.add(loadedObject.scene)

        loadedObject.scene.translateX(hexVector.getWorldXFromAxialQ())
        loadedObject.scene.translateZ(hexVector.getWorldZFromAxialR())
        loadedObject.scene.userData.hexVector = hexVector

        let hexTile
        for (let hex of worldStore.hexes3d) {
            if (hex.userData.Q === hexVector.Q && hex.userData.R === hexVector.R) {
                hexTile = hex
            }
        }

        hexTile.userData.free = false
    }))
}