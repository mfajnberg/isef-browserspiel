import { defineStore } from 'pinia' 

export const useWorldEditorStore = defineStore('WorldEditorStore', {
    id: 'WorldEditorStore',
    state: () => ({
        // current layout
        worldmap: null,

        // input & HUD
        hoveredHex: null, // hexTile
        hoveredDisplayName: "", // `{hexTile.site.name} ({axialQ}, {axialR})`
    }),
    getters: {
        someGetter: (state) => state.count,
        getHexes: (state) => JSON.parse(JSON.stringify(state.visibleHexData))
    },
    actions: {
        initializeGrid(data) {
            initializeGridEmpty(data.radius)
            // fill sites according to data
        },

        initializeGridEmpty(radius) {
            this.worldmap.generateMap()
        },

        save(layout, name) {
            // save to localStorage
            // post request
        },

        loadLayout(name) {
            // get request
            // update current layout state
            // save to session storage
        },

        updateHovered(item) {
            if (item)
            {
                this.hoveredHex = item
                this.hoveredDisplayName = `${item.site.name} (${item.axialQ}, ${item.axialR})`
                return
            }
            this.hoveredItem = null
            this.hoveredDisplayName = "⠀"
        }
    },
})