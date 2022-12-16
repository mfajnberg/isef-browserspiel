import { defineStore } from 'pinia' 

export const useStore = defineStore('ReactiveStore', {
    id: 'MyStoreId',
    state: () => ({
        count: 1,
        visibleHexData: {},
        THREE_hexGrid: [],
        THREE_sites: [],
        hoveredItem: ""
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
                const response = await fetch("VisibleHexes_example.json")
                this.visibleHexData = response

                // // real data:
                // const response = await fetch("https://localhost:5001/getVisibleHexes")
                // const json = await response.json()
                // console.log(json)
                // this.visibleHexData = json
            
        },
        updateHovered(item) {
            this.hoveredItem = item
        }
    },
})