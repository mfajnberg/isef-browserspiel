import { defineStore } from 'pinia' 

export const useWorldStore = defineStore('WorldStore', {
    id: 'WorldStore',
    state: () => ({
        hexes3d: [],

        cursor: null,

        previewUrl: "",
        changedPreviewURL: false,
        preview: null,

        hoveredItem: null,

        objectSnapped: false,

        sitesBuffer: [],
    }),
    getters: {
        loadHexData: (state) => JSON.parse(JSON.stringify(state.visibleHexData)),
        getIntersectables: (state) => state.hexes3d,
        getHoveredName: (state) => {
            if (state.hoveredItem != null) {
                return state.hoveredItem.name
            }
            return ""
            },


    },
    actions: {
        async loadVisibleTiles() {
            // dummy data:
            const response = await (await fetch("VisibleHexes_example.json")).json()
            this.visibleHexData = response          
        },

            
        getHexTile(axialQ, axialR) {

        },
        // EDITOR MODE...

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