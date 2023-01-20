import { defineStore } from 'pinia' 
import { Howl } from 'howler'
import * as THREE from 'three'
import { TextureLoader } from 'three'

export const useGameAssetStore = defineStore('GameAssetStore', {
    id: 'GameAssetStore',
    state: () => ({
        HexModel: null,
        HexCursor: null,
        hexTextures: [new THREE.TextureLoader().load('grass_texture_1.jpg'), 
                    new THREE.TextureLoader().load('grass_texture_2.jpg'), 
                    new THREE.TextureLoader().load('grass_texture_3.jpg')]
    }),
    getters: {
        getHexModel: (state) => state.HexModel,
        getHexCursor: (state) => state.HexCursor,
    },
    actions: {
        async loadHex() {
            // await fetch("/model")
            await fetch("HexBase.glb")
            .then((response) => response.arrayBuffer())
            .then(arrayBuffer => {
                this.HexModel = arrayBuffer
            })
        },
        async loadHexCursor() {
            // await fetch("/model")
            await fetch("tower_medieval.glb")
            .then((response) => response.arrayBuffer())
            .then(arrayBuffer => {
                this.HexCursor = arrayBuffer
            })
        },
    },
})