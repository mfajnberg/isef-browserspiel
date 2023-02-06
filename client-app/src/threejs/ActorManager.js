import * as THREE from 'three'
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader'

import { Worldmap } from '../classes/Worldmap'
import { Sites } from '../stores/000Singletons'

export async function initActors(scene, worldStore, assetStore) {
    // dummy data
    // await worldStore.loadVisibleHexTiles()
    const loader = new GLTFLoader()
    worldStore.worldmap = new Worldmap()
    const visible = Worldmap.makeHexGridVectors(2)
    
    // // replace with calls to proper asset loading service
    await assetStore.loadHex()
    // await assetStore.loadForest()
    // await assetStore.loadCliffs()
    // await assetStore.loadHouse()
    // await assetStore.loadTent()
    // await assetStore.loadChest()
    // await assetStore.loadCrystal()

    for (let i = 0; i < visible.length; i++) {
        initHex(loader, scene, worldStore, assetStore, visible[i], i%3)
    }

    loadHexCursor(loader, scene, worldStore, null)
    try {
        loadSitePreview(loader, scene, worldStore, null)
    }
    catch { }
}

function initHex(loader, scene, worldStore, assetStore, hexData, randomRotation) {
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
        
        const sites = new Sites()
        sites.buffer.push({
            AxialCoordinates: {
                Q: hexData.Q + worldStore.getAbsoluteZeroOffset.Q,
                R: hexData.R + worldStore.getAbsoluteZeroOffset.R
            },
            SiteType: 0
        })
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

export function spawnSite(loader, scene, worldStore, hexVector, modelUrl) {
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

        // occupy correct hex object3d
        let hexTile
        for (let hex of worldStore.hexes3d) {
            if (hex.userData.Q === hexVector.Q && hex.userData.R === hexVector.R) {
                hexTile = hex
            }
        }
        hexTile.userData.free = false

        // set type in sites.buffer
        const sites = new Sites()
        for (let hex of sites.buffer) {
            if (hex.AxialCoordinates.Q === hexVector.Q + worldStore.getAbsoluteZeroOffset.Q && 
                hex.AxialCoordinates.R === hexVector.R + worldStore.getAbsoluteZeroOffset.R) {
                    hex.SiteType = 100 // programmatically decide the exact type
            }
        }

        // save reference of new site to worldStore
        worldStore.sites3d.push(loadedObject.scene)
    }))
}

export function dispose(object3d) {
    object3d.children.forEach(child => {
        disposeObject(child);
    });
    object3d.geometry && object3d.geometry.dispose()
    object3d.material && object3d.material.dispose()
    object3d.texture && object3d.texture.dispose()
    try {object3d.parent.remove(object3d)} catch(e) {}
    try {object3d.dispose()} catch(e) {}
}