import { defineStore } from 'pinia' 
import { Howl } from 'howler'
import * as THREE from 'three'

export const useGameAssetStore = defineStore('GameAssetStore', {
    id: 'GameAssetStore',
    state: () => ({
        hexModel: null,
        hexCursor: null,
        hexTextures: [new THREE.TextureLoader().load('grass_texture_1.jpg'), 
                    new THREE.TextureLoader().load('grass_texture_2.jpg'), 
                    new THREE.TextureLoader().load('grass_texture_3.jpg')],
        pointerDownSound: new Howl({
            src: 'isef_pointer_down.mp3',
            volume: .7
        }),

        pointerUpSound: new Howl({
            src: 'isef_pointer_up.mp3',
            volume: .7
        }),

        placeObjectSound: new Howl({
            src: 'isef_place_building.mp3',
            volume: .1
        }),
            
    }),
    getters: {
        getHexModel: (state) => state.hexModel,
        getHexCursor: (state) => state.hexCursor,
    },
    actions: {
        async loadHex() {
            // await fetch("/model")
            await fetch("HexBase.glb")
            .then((response) => response.arrayBuffer())
            .then(arrayBuffer => {
                this.hexModel = arrayBuffer
            })
        },
        async loadHexCursor() {
            // await fetch("/model")
            await fetch("tree_cluster_fir_1.glb")
            .then((response) => response.arrayBuffer())
            .then(arrayBuffer => {
                this.hexCursor = arrayBuffer
            })
        },
    },
})