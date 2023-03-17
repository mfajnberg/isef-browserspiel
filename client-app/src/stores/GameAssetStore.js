import { defineStore } from 'pinia' 
import { Howl } from 'howler'
import * as THREE from 'three'
import { requestGetAsset } from '../services/gameAssetService'

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
                , '3dCursorCross.glb', '3dCursorArrow.glb', 'HexBase.glb',
                'Arissa.fbx', 'Idle.fbx', 'Walking.fbx'
            ],        
        
        hexTextureURIs: [
            "/assets/grass_texture_1.jpg", 
            "/assets/grass_texture_2.jpg", 
            "/assets/grass_texture_3.jpg", 
    ],
            
        pointerDownSound: new Howl({
            src: 'isef_pointer_down.mp3',
            volume: .3
        }),

        pointerUpSound: new Howl({
            src: 'isef_pointer_up.mp3',
            volume: .4
        }),

        placeObjectSound: new Howl({
            src: 'isef_place_building.mp3',
            volume: .04
        }),

        coinCollectSound: new Howl({
            src: 'isef_collect_coins.mp3',
            volume: .25
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
        async fetchAsset(uri) {
            await requestGetAsset(uri)
        },
        async devFetchHexBase() {
            this.hexModel = await(await fetch("HexBase.glb")).arrayBuffer()
        }
    },
})