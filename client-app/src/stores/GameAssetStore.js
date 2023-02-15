import { defineStore } from 'pinia' 
import { Howl } from 'howler'
import * as THREE from 'three'

export const useGameAssetStore = defineStore('GameAssetStore', {
    id: 'GameAssetStore',
    state: () => ({
        hexModel: null,

        hexCursor: null,
        
        modelURIs:
            ['forest_1', 'flag', 'house', 
            'tent_field_camp', 'crystals', 
            'chest_lp', 'tree_ancient'],

        pointerDownSound: new Howl({
            src: 'isef_pointer_down.mp3',
            volume: .6
        }),

        pointerUpSound: new Howl({
            src: 'isef_pointer_up.mp3',
            volume: .6
        }),

        placeObjectSound: new Howl({
            src: 'isef_place_building.mp3',
            volume: .1
        }),

        assets3d: [], // name: {string}, model: {arrayBuffer}
        assetsLoaded: false
            
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
                    // Authorization: `Bearer ${useAuthStore().token}`,
                    credentials: 'include',
                    "Content-Type": "application/octet-stream"
                }
            })
            .then(async (response) => await response.arrayBuffer())
            .then(arrayBuffer => {
                this.hexModel = arrayBuffer
            })
        },
        async loadAsset(uri, authStore) {
            await fetch(`api/asset/glb?name=${uri}.glb`, {
                headers: {
                    Authorization: `Bearer ${authStore.token}`,
                    credentials: 'include',
                    "Content-Type": "application/octet-stream"
                }
            })
            .then(async (response) => await response.arrayBuffer())
            .then(arrayBuffer => {
                this.assets3d.push({
                    name: uri, model: arrayBuffer
                })
                console.log(this.assets3d)
            })
        },
        async loadHexCursor() {
            await fetch("HexCursor.glb", {
                headers: {
                    // Authorization: `Bearer ${localStorage.getItem(token)}`,
                    credentials: 'include',
                    "Content-Type": "application/octet-stream"
                }
            })            
            .then(async (response) => await response.arrayBuffer())
            .then(arrayBuffer => {
                this.hexCursor = arrayBuffer
            })
        },
    },
})