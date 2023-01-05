import { defineStore } from 'pinia' 

export const useWorldmapStore = defineStore('WorldmapStore', {
    id: 'WorldmapStore',
    state: () => ({
        count: 1,
        visibleHexData: {},
        THREE_hexGrid: [],
        THREE_sites: [],
        hoveredItem: "",

        HexTileModel: null
    }),
    getters: {
        someGetter: (state) => state.count,
        getHexes: (state) => JSON.parse(JSON.stringify(state.visibleHexData))
    },
    actions: {
        increment() {
            this.count++
        },
        async loadVisibleHexTiles() {
                // dummy data:
                const response = await (await fetch("VisibleHexes_example.json")).json()
                this.visibleHexData = response
                // // real data:
                // const response = await fetch("https://localhost:5001/getVisibleHexes")
                // const json = await response.json()
                // console.log(json)
                // this.visibleHexData = json
            
        },
        
        // async loadHexTileModel() {
        //     const response = await fetch("https://localhost:5001/static-assets/Hex_meter.glb")
        // },

        updateHovered(item) {
            this.hoveredItem = item
        }
    },
})