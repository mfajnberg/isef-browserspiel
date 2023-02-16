import * as THREE from 'three'
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader'
import { FBXLoader } from 'three/examples/jsm/loaders/FBXLoader'

import { Worldmap } from '../classes/Worldmap'
import { Sites, Sites3d } from '../stores/000Singletons'
import { useGameAssetStore } from '../stores/GameAssetStore'
import { AnimationLoader } from 'three'

export async function initActors(worldStore, gameAssetStore) {
    // dummy data
    // await worldStore.loadVisibleHexTiles()
    const visible = Worldmap.makeHexGridVectors(3)
    worldStore.worldmap = new Worldmap()
    
    const loader = new GLTFLoader()
    const texLoader = new THREE.TextureLoader()
    const tex1 = texLoader.load('grass_texture_1.jpg')
    const tex2 = texLoader.load('grass_texture_2.jpg')
    const tex3 = texLoader.load('grass_texture_3.jpg')
    const hexTextures = [tex1, tex2, tex3]
    
    
    // // replace with calls to proper asset loading service
    await gameAssetStore.loadHex()
    await gameAssetStore.loadHexCursor()
    worldStore.previewModelURI = "HexPreview.glb"
    await gameAssetStore.loadHexPreview()
    // await assetStore.loadForest()
    // await assetStore.loadCliffs()
    // await assetStore.loadHouse()
    // await assetStore.loadTent()
    // await assetStore.loadChest()
    // await assetStore.loadCrystal()
    
    if (worldStore.cursor == null)
        loadHexCursor(loader, worldStore, null)

    for (let i = 0; i < visible.length; i++) {
        initHex(loader, worldStore, gameAssetStore, visible[i], i%3, hexTextures)
    }

    if (!worldStore.character)
        loadCharacter(worldStore, gameAssetStore)
}

function initHex(loader, worldStore, gameAssetStore, hexData, randomRotation, hexTextures) {
    loader.parse(gameAssetStore.getHexModel, '', (loadedObject) => {
        loadedObject.scene.traverse(child => {
            if (child.isMesh) {
                child.material = new THREE.MeshStandardMaterial({
                    map: hexTextures[randomRotation]
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
        loadedObject.scene.userData.isBlocked = false
        loadedObject.scene.userData.FCost = 0
        loadedObject.scene.userData.GCost = 0
        loadedObject.scene.userData.HCost = 0
        loadedObject.scene.userData.CameFrom = null
        loadedObject.scene.name = `${hexData.Q}|${hexData.R}`

        worldStore.scene.add(loadedObject.scene)
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

function loadCharacter(worldStore, gameAssetStore) {
    for (let asset of gameAssetStore.assets3d) {
        if (asset.name == "Arissa.fbx")
            var model = asset.data
    }
    const modelBlob = new Blob([model], { type: 'application/octet-stream' })
    const loader = new FBXLoader()
    try {
        loader.load(URL.createObjectURL(modelBlob), (loadedObject => {
            loadedObject.scale.set(.01, .01, .01)
    
            loadedObject.traverse((child) => {
                if (child.isMesh) {
                child.castShadow = true
                child.receiveShadow = true
                }
            })
            loadedObject.rotateY(.5236)
            worldStore.character = loadedObject
            
            playAnim(worldStore, gameAssetStore, 'Idle.fbx')

            worldStore.scene.add(loadedObject)
        }))
    } catch (e) {
        console.log(e)
    }
}

export function playAnim(worldStore, gameAssetStore, animationURL) {
    for (let asset of gameAssetStore.assets3d) {
        if (asset.name == animationURL) {
            var anim = asset.data
        }
    }
    const animBlob = new Blob([anim], { type: 'application/octet-stream' })
    const loader = new FBXLoader()
    loader.load(URL.createObjectURL(animBlob), (loadedAnim) => {
        worldStore.animationMixer = new THREE.AnimationMixer(worldStore.character)
        const action = worldStore.animationMixer.clipAction(loadedAnim.animations[0])
        action.play()
    })
}

function loadHexCursor(loader, worldStore) {
    loader.load('HexCursor.glb', (loadedObject => {
        worldStore.cursor = loadedObject.scene
        worldStore.scene.add(loadedObject.scene)
    }))
}

export function loadSitePreview(loader, worldStore, gameAssetStore) {
    for (let asset of gameAssetStore.assets3d) {
        if (asset.name == worldStore.previewModelURI) {
            var model = asset.data
        }
    }
    loader.parse(model, '', (loadedObject => {
        loadedObject.scene.traverse((child) => {
            if (child.isMesh) {
            child.castShadow = false
            }
        })
        loadedObject.scene.visible = true

        try {
            dispose(worldStore.preview)
        } catch (e) {}
        
        worldStore.preview = loadedObject.scene
        worldStore.scene.add(loadedObject.scene)
    }))
}

export function spawnSite(loader, worldStore, gameAssetStore, hexVector) {
    for (let asset of gameAssetStore.assets3d) {
        if (asset.name == worldStore.previewModelURI) {
            var model = asset.data
        }
    }
    loader.parse(model, '', (loadedObject => {
        loadedObject.scene.traverse((child) => {
            if (child.isMesh) {
            child.castShadow = true
            }
        })
        worldStore.scene.add(loadedObject.scene)

        loadedObject.scene.translateX(hexVector.getWorldXFromAxialQ())
        loadedObject.scene.translateZ(hexVector.getWorldZFromAxialR())
        loadedObject.scene.userData.hexVector = hexVector

        // occupy correct hex object3d
        for (let hex of worldStore.hexes3d) {
            if (hex.userData.Q === hexVector.Q && hex.userData.R === hexVector.R) {
                hex.userData.isBlocked = true
            }
        }
        // set type in sites.buffer
        const sites = new Sites()
        for (let site of sites.buffer) {
            if (site.AxialCoordinates.Q === hexVector.Q + worldStore.getAbsoluteZeroOffset.Q && 
                site.AxialCoordinates.R === hexVector.R + worldStore.getAbsoluteZeroOffset.R) {
                    site.SiteType = 100 // programmatically decide the exact type
            }
        }

        // save reference of new site to worldStore
        const sites3d = new Sites3d()
        sites3d.buffer.push(loadedObject.scene)
    }))
}

export function dispose(object3d) {
    object3d.children.forEach(child => {
        dispose(child);
    });
    object3d.geometry && object3d.geometry.dispose()
    object3d.material && object3d.material.dispose()
    object3d.texture && object3d.texture.dispose()
    try {object3d.parent.remove(object3d)} catch(e) {}
    try {object3d.dispose()} catch(e) {}
}