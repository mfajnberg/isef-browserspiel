import { defineStore } from 'pinia' 
import { Howl } from 'howler'
import * as THREE from 'three'

export const useGameAssetStore = defineStore('GameAssetStore', {
    id: 'GameAssetStore',
    state: () => ({
        hexModel: null,
        forestModel: null,
        cliffsModel: null,
        houseModel: null,
        tentModel: null,
        chestModel: null,
        crystalModel: null,

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
        getForestModel: (state) => state.forestModel,
        getCliffsModel: (state) => state.cliffsModel,
        getHouseModel: (state) => state.houseModel,
        getTentModel: (state) => state.tentModel,
        getChestModel: (state) => state.chestModel,
        getCrystalModel: (state) => state.crystalModel,
        getHexCursor: (state) => state.hexCursor,
    },
    actions: {
        // factor out the name of the model to get
        async loadHex() {
            await fetch("HexBase.glb", {
            // await fetch("api/asset/?name=hex.glb", {
                headers: {
                    // Authorization: `Bearer ${localStorage.getItem(token)}`,
                    credentials: 'include',
                    "Content-Type": "application/octet-stream"
                }
            })
            .then((response) => response.arrayBuffer())
            .then(arrayBuffer => {
                this.hexModel = arrayBuffer
            })
        },
        async loadForest() {
            await fetch("api/asset/?name=forest.glb", {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem(token)}`,
                    credentials: 'include',
                    "Content-Type": "application/octet-stream"
                }
            })
            .then((response) => response.arrayBuffer())
            .then(arrayBuffer => {
                this.forestModel = arrayBuffer
            })
        },
        async loadCliffs() {
            await fetch("api/asset/?name=forest.glb", {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem(token)}`,
                    credentials: 'include',
                    "Content-Type": "application/octet-stream"
                }
            })
            .then((response) => response.arrayBuffer())
            .then(arrayBuffer => {
                this.forestModel = arrayBuffer
            })
        },
        async loadHouse() {
            await fetch("api/asset/?name=house.glb", {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem(token)}`,
                    credentials: 'include',
                    "Content-Type": "application/octet-stream"
                }
            })            .then((response) => response.arrayBuffer())
            .then(arrayBuffer => {
                this.houseModel = arrayBuffer
            })
        },
        async loadTent() {
            await fetch("api/asset/?name=tent.glb", {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem(token)}`,
                    credentials: 'include',
                    "Content-Type": "application/octet-stream"
                }
            })            .then((response) => response.arrayBuffer())
            .then(arrayBuffer => {
                this.tentModel = arrayBuffer
            })
        },
        async loadChest() {
            await fetch("api/asset/?name=chest.glb", {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem(token)}`,
                    credentials: 'include',
                    "Content-Type": "application/octet-stream"
                }
            })            .then((response) => response.arrayBuffer())
            .then(arrayBuffer => {
                this.chestModel = arrayBuffer
            })
        },
        async loadCrystal() {
            await fetch("api/asset/?name=crystal.glb", {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem(token)}`,
                    credentials: 'include',
                    "Content-Type": "application/octet-stream"
                }
            })            .then((response) => response.arrayBuffer())
            .then(arrayBuffer => {
                this.crystalModel = arrayBuffer
            })
        },
        async loadHexCursor() {
            await fetch("api/asset/?name=cursor.glb", {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem(token)}`,
                    credentials: 'include',
                    "Content-Type": "application/octet-stream"
                }
            })            
            .then((response) => response.arrayBuffer())
            .then(arrayBuffer => {
                this.hexCursor = arrayBuffer
            })
        },
    },
})