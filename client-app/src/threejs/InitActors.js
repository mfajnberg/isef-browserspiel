import * as THREE from 'three'
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader.js'

import { HexVector } from './classes/HexVector'

export async function initActors(scene, worldStore, assetStore) {
    const _gltfLoader = new GLTFLoader()

    // dummy data
    await worldStore.loadVisibleHexTiles()
    const visible = worldStore.getHexes
    await assetStore.loadModel()

    console.log(assetStore.getHexModel)

    for (let i = 0; i < visible.length; i++) {
        initHex(_gltfLoader, scene, worldStore, assetStore, visible[i])
    }

    initHelmetExample(_gltfLoader, scene, worldStore)
}

function initHex(loader, scene, worldStore, assetStore, hexData) {
    loader.parse(assetStore.getHexModel, '', (loadedHex) => {
        console.log(loadedHex)
        loadedHex.scene.traverse((child) => {
            if (child.isMesh) {
                child.material = new THREE.MeshStandardMaterial({
                    color: 'rgb(5,40,15)',
                    side: THREE.DoubleSide
                });
            child.receiveShadow = true
            }
        })
        loadedHex.scene.name = hexData.direction
        var hexVector = new HexVector(hexData.Q, hexData.R)
        loadedHex.scene.translateX(hexVector.WorldX)
        loadedHex.scene.translateZ(hexVector.WorldY)
        worldStore.THREE_hexGrid.push(loadedHex.scene)
        scene.add(loadedHex.scene)
    })
}

function initHelmetExample(loader, scene, store) {
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
        loadedHelmet.scene.translateY(25)
        loadedHelmet.scene.scale.set(80,80,80)
        loadedHelmet.scene.name = "Helm"
        
        store.THREE_sites.push(loadedHelmet.scene)
        scene.add(loadedHelmet.scene)
    }))
}