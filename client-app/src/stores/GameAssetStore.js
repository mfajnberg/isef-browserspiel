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
                    new THREE.TextureLoader().load('grass_texture_3.jpg')],
        pointerDownSound: new Howl({
            src: 'isef_pointer_down.mp3',
            volume: 1
        }),

        pointerUpSound: new Howl({
            src: 'isef_pointer_up.mp3',
            volume: 1
        }),

        placeObjectSound: new Howl({
            src: 'isef_place_building.mp3',
            volume: .1
        }),
            
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
            await fetch("tree_cluster_fir_1.glb")
            .then((response) => response.arrayBuffer())
            .then(arrayBuffer => {
                this.HexCursor = arrayBuffer
            })
        },
    },
})