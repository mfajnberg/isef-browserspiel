import * as THREE from 'three'
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader.js'

import { HexVector } from './HexVector'
import { Worldmap } from './actors/Worldmap'
import { HexTile } from './actors/HexTile'
import { ActorBase } from './actors/ActorBase'

export async function initActors(scene, loader, worldStore, assetStore) {
    // dummy data
    await worldStore.loadVisibleHexTiles()
    
    worldStore.worldmap = new Worldmap()
    const visible = Worldmap.makeHexGridVectors(4)
    
    await assetStore.loadHex()

    for (let i = 0; i < visible.length; i++) {
        initHex(loader, scene, worldStore, assetStore, visible[i], i%3)
    }

    loadWorldCursor('tower_medieval.glb', loader, scene, worldStore, null)
}

function initHex(loader, scene, worldStore, assetStore, hexData, randomRotation) {
    const newHex = new HexTile(hexData)
    var tex

    // 3d model
    loader.parse(assetStore.getHexModel, '', (loadedHex) => {
        loadedHex.scene.traverse((child) => {
            if (child.isMesh) {
                child.material = new THREE.MeshStandardMaterial({
                    map: assetStore.hexTextures[randomRotation],
                })
                tex = child.material
                tex.map.wrapS = THREE.RepeatWrapping
                tex.map.wrapT = THREE.ClampToEdgeWrapping
                tex.map.offset.set(.5, .5)
                tex.map.repeat.set(.5, .5)
                child.receiveShadow = true
            }
        })
        loadedHex.scene.translateX(hexData.getWorldXFromAxialQ())
        loadedHex.scene.translateZ(hexData.getWorldZFromAxialR())
        scene.add(loadedHex.scene)
        worldStore.intersectables.push(loadedHex.scene)
        newHex.object3d = loadedHex
        loadedHex.scene.userData.wrapper = newHex
        loadedHex.scene.name = `${hexData.Q}|${hexData.R}`
        worldStore.worldmap.hexGrid.push(newHex)

    })

}

export function loadWorldCursor(modelURL, loader, scene, worldStore) {

    loader.load(modelURL, (loadedCursor => {
        loadedCursor.scene.traverse((child) => {
            if (child.isMesh) {
            child.castShadow = true
            }
        })
        loadedCursor.scene.visible = false

        if (worldStore.cursor != null) {
            worldStore.cursor.removeFromParent()
        }
        
        worldStore.cursor = loadedCursor.scene
        scene.add(loadedCursor.scene)
    }))
}