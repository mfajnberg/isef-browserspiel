import * as THREE from 'three'
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader.js'

import { AxialToWorldXY } from './HexVector'

export async function initActors(scene, store) {
    const _gltfLoader = new GLTFLoader()
    await store.loadVisibleHexTiles()
    const visible = store.getHexes
    console.log(visible)
    for (let i = 0; i < visible.length; i++) {
        initHex(_gltfLoader, scene, store, visible[i])
    }
    initHelmetExample(_gltfLoader, scene, store)
}

function initHex(loader, scene, store, hexData) {
    loader.load('Hex_meter.glb', (loadedHex => {
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
        var SceneVector = AxialToWorldXY(hexData.Q, hexData.R)
        loadedHex.scene.translateX(SceneVector.x)
        loadedHex.scene.translateZ(SceneVector.y)
        store.THREE_hexGrid.push(loadedHex.scene)
        scene.add(loadedHex.scene)
    }))
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