import * as THREE from 'three'
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader.js'

import { HexVector } from './HexVector'
import { Worldmap } from './actors/Worldmap'
import { HexTile } from './actors/HexTile'
import { ActorBase } from './actors/ActorBase'

export async function initActors(scene, worldStore, assetStore) {
    const _gltfLoader = new GLTFLoader()

    // dummy data
    await worldStore.loadVisibleHexTiles()
    
    worldStore.worldmap = new Worldmap()
    const visible = Worldmap.makeHexGridVectors(5)
    
    await assetStore.loadHex()
    for (let i = 0; i < visible.length; i++) {
        initHex(_gltfLoader, scene, worldStore, assetStore, visible[i])
    }

    initCursor(_gltfLoader, scene, worldStore)
}

function initHex(loader, scene, worldStore, assetStore, hexData) {
    const newHex = new HexTile(hexData)

    // 3d model
    loader.parse(assetStore.getHexModel, '', (loadedHex) => {
        loadedHex.scene.traverse((child) => {
            if (child.isMesh) {
                child.material = new THREE.MeshStandardMaterial({
                    color: 'rgb(10,65,20)',
                    side: THREE.DoubleSide
                });
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

function initCursor(loader, scene, worldStore) {
    let hexVector = new HexVector(0, 0)
    const newSite = new ActorBase(hexVector)

    loader.load('HexCursor.glb', (loadedCursor => {
        loadedCursor.scene.traverse((child) => {
            if (child.isMesh) {
                child.material = new THREE.MeshStandardMaterial({
                    color: 'rgb(255,245,235)',
                    roughness: .5,
                    metalness: 1,
                    side: THREE.DoubleSide,
                    shadowSide: THREE.DoubleSide
                })
            child.castShadow = true
            }
        })
        loadedCursor.scene.translateY(.01)
        
        worldStore.worldmap.sites.push(loadedCursor)
        scene.add(loadedCursor.scene)
    }))
}