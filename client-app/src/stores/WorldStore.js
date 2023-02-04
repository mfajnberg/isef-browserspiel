import { defineStore } from 'pinia' 
import { requestGetHexTiles } from '../services/WorldmapService'
import { initActors } from '../threejs/ActorManager'

export const useWorldStore = defineStore('WorldStore', {
    id: 'WorldStore',
    state: () => ({
        initialized: false,

        absoluteZeroOffset: {
            Q: 0, 
            R: 0
        },
        
        hexes3d: [],
        scene: null,
        sites3d: [],
        
        cursor: null,
        preview: null,
        hoveredItem: null,
        objectSnapped: false,
        
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
        getAbsoluteZeroOffset: (state) => state.absoluteZeroOffset
    },
    actions: {
        setAbsoluteZeroOffset(q, r) {
            this.absoluteZeroOffset.Q = q
            this.absoluteZeroOffset.R = r
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