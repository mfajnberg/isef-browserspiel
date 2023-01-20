import { defineStore } from 'pinia' 
import { HexVector } from '../threejs/HexVector'

export const useWorldStore = defineStore('WorldStore', {
    id: 'WorldStore',
    state: () => ({
        worldmap: null,
        editorMode: true,

        cursorURL: "test",
        changedCursorURL: false,
        cursor: null,
        
        placables: [],
        intersectables: [],
        
        hoveredItem: null,
    }),
    getters: {
        getHexes: (state) => JSON.parse(JSON.stringify(state.visibleHexData)),
        getIntersectables: (state) => state.intersectables,
        getHovered: (state) => {
            if (state.hoveredItem != null) {
                return state.hoveredItem.axial
            }
            return ""
            }
    },
    actions: {
        async loadVisibleHexTiles() {
                // dummy data:
                const response = await (await fetch("VisibleHexes_example.json")).json()
                this.visibleHexData = response          
        },

        // EDITOR MODE...

        saveLayout(layoutData, name) {
            // save to localStorage
            // post request
        },

        loadLayout(name) {
            // get request
            // update current layout state
            // save to session storage
        }
    },
})