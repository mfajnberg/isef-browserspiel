import { defineStore } from 'pinia' 

export const useWorldmapStore = defineStore('WorldmapStore', {
    id: 'WorldmapStore',
    state: () => ({
        visibleHexData: {},
        hexTiles: [],
        sites: [],

        hoveredItem: {},
        hoveredItemName: "",
    }),
    getters: {
        someGetter: (state) => state.count,
        getHexes: (state) => JSON.parse(JSON.stringify(state.visibleHexData))
    },
    actions: {
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
        updateHovered(item) {
            if (item)
            {
                this.hoveredItem = item
                this.hoveredItemName = item.parent.name
                return
            }
            this.hoveredItem = null
            this.hoveredItemName = "⠀"
        }

    },
})