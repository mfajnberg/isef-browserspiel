import { defineStore } from 'pinia' 
import { HexVector } from '../classes/HexVector'

export const useWorldStore = defineStore('WorldStore', {
    id: 'WorldStore',
    state: () => ({
        hexes3d: [],

        cursor: null,
        preview: null,

        hoveredItem: null,

        objectSnapped: false,
        sitesBuffer: [],

        worldData: null
    }),
    getters: {
        getIntersectables: (state) => state.hexes3d,
        getHoveredName: (state) => {
            if (state.hoveredItem != null) {
                return state.hoveredItem.name
            }
            return ""
            },

    },
    actions: {
        saveLayout() {
            // post request
        },

        loadLayout(name) {
            // get request
            // update current layout state
            // save to session storage
        }
    },
})