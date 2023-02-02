import { defineStore } from 'pinia' 
import { HexVector } from '../classes/HexVector'
import { requestGetHexTiles } from '../services/WorldmapService'
import { initActors } from '../threejs/ActorManager'
import { useGameAssetStore } from './GameAssetStore'
import { toRaw } from 'vue'

export const useWorldStore = defineStore('WorldStore', {
    id: 'WorldStore',
    state: () => ({
        initialized: false,

        relativeZero: {Q: 0, R: 0},
        
        hexes3d: [],
        scene: null,
        sites3d: [],
        
        cursor: null,
        preview: null,
        hoveredItem: null,
        objectSnapped: false,
        
        sitesBuffer: [],
        response: null
    }),
    getters: {
        getScene: (state) => state.scene,
        getHexes3d: (state) => state.hexes3d,
        getSites: (state) => state.sites3d,
        getSitesBuffer: (state) => state.sitesBuffer,
        getHoveredName: (state) => {
            if (state.hoveredItem != null) {
                return state.hoveredItem.name
            }
            return ""
            },
        getRelativeZero: (state) => state.relativeZero
    },
    actions: {
        setRelativeZeroQ(val) {
            this.relativeZero.Q = val
        },
        setRelativeZeroR(val) {
            this.relativeZero.R = val
        },

        async fetchWorldData() {
            await requestGetHexTiles(this)
        },
        setScene(scene) {
            this.scene = scene
        },
        ACTION(assetStore) {
            if (!this.initialized) {
                initActors(this.scene, this, assetStore)
                this.initialized = true
            }
        },
    },
})