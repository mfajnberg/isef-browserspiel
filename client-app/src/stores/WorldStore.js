import { defineStore } from 'pinia' 
import { HexVector } from '../threejs/classes/HexVector'

export const useWorldStore = defineStore('WorldStore', {
    id: 'WorldStore',
    state: () => ({
        worldmap: null,

        intersectables: [],
        hoveredItem: null,
        hoveredItemName: "",
    }),
    getters: {
        getHexes: (state) => JSON.parse(JSON.stringify(state.visibleHexData)),
        getIntersectables: (state) => state.intersectables
    },
    actions: {
        async loadVisibleHexTiles() {
                // dummy data:
                const response = await (await fetch("VisibleHexes_example.json")).json()
                this.visibleHexData = response          
        },
        updateHovered(item) {
            if (item)
            {
                let axial = item.parent.name.split("|")
                let q = parseInt(axial[0])
                let r = parseInt(axial[1])
                let vec = new HexVector(q, r)
                this.hoveredItem = this.worldmap.findHex(vec)
                this.hoveredItemName = this.hoveredItem.object3d.scene.name
                return
            }
            this.hoveredItem = null
            this.hoveredItemName = ""
        }

    },
})