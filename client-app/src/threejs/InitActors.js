import * as THREE from 'three'
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader.js'

import { HexVector } from './classes/HexVector'
import { Worldmap } from './classes/Worldmap'
import { HexTile } from './classes/HexTile'
import { ActorBase } from './classes/ActorBase'

export async function initActors(scene, worldStore, assetStore) {
    const _gltfLoader = new GLTFLoader()

    // dummy data
    await worldStore.loadVisibleHexTiles()
    
    worldStore.worldmap = new Worldmap()
    const visible = Worldmap.makeHexGridVectors(5)
    console.log(visible)
    
    await assetStore.loadModel()
    for (let i = 0; i < visible.length; i++) {
        initHex(_gltfLoader, scene, worldStore, assetStore, visible[i])
    }

    initSiteExample(_gltfLoader, scene, worldStore)
}

function initHex(loader, scene, worldStore, assetStore, hexData) {
    const newHex = new HexTile(hexData)

    // 3d model
    loader.parse(assetStore.getHexModel, '', (loadedHex) => {
        loadedHex.scene.traverse((child) => {
            if (child.isMesh) {
                child.material = new THREE.MeshStandardMaterial({
                    color: 'rgb(5,40,15)',
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
        loadedHex.scene.name = `${hexData.Q}|${hexData.R}`
        worldStore.worldmap.hexGrid.push(newHex)
    })

}

function initSiteExample(loader, scene, worldStore) {
    let hexVector = new HexVector(0, 0)
    const newSite = new ActorBase(hexVector)

    loader.load('helm_2.glb', (loadedHelmet => {
        loadedHelmet.scene.traverse((child) => {
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
        loadedHelmet.scene.translateY(40)
        loadedHelmet.scene.scale.set(80,80,80)
        newSite.displayName = "Helm"
        
        worldStore.worldmap.sites.push(loadedHelmet)
        scene.add(loadedHelmet.scene)
    }))
}