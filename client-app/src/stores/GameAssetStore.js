import { defineStore } from 'pinia' 
import { Howl } from 'howler'
import * as THREE from 'three'

export const useGameAssetStore = defineStore('GameAssetStore', {
    id: 'GameAssetStore',
    state: () => ({
        hexModel: null,
        hexCursor: null,
        hexPreview: null,
        
        modelURIs:
            [
                'forest_1.glb', 'flag.glb', 'house.glb', 'tent_field_camp.glb', 
                    'crystals.glb', 'chest_lp.glb', 'tree_ancient.glb', 
                'HexPreview.glb', 
                'Arissa.fbx', 'Idle.fbx', 'Walking.fbx'
            ],        

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

        assets3d: [], // name: {string}, data: {arrayBuffer}
        assetsLoaded: false
            
    }),
    getters: {
        getHexModel: (state) => state.hexModel,
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
        async loadHexPreview() {
            await fetch("HexPreview.glb", {
                headers: {
                    // Authorization: `Bearer ${localStorage.getItem(token)}`,
                    credentials: 'include',
                    "Content-Type": "application/octet-stream"
                }
            })            
            .then(async (response) => await response.arrayBuffer())
            .then(arrayBuffer => {
                this.assets3d.push({
                    name: "HexPreview."
                })
                this.hexPreview = arrayBuffer
            })
        },
        async fetchAsset(uri, authStore) {
            await fetch(`api/asset/get?name=${uri}`, {
                headers: {
                    Authorization: `Bearer ${authStore.token}`,
                    credentials: 'include',
                    "Content-Type": "application/octet-stream"
                }
            })
            .then(async (response) => await response.arrayBuffer())
            .then(arrayBuffer => {
                this.assets3d.push({
                    name: uri, data: arrayBuffer
                })
            })
        },
    },
})