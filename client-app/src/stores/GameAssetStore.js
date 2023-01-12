import { defineStore } from 'pinia' 

export const useGameAssetStore = defineStore('GameAssetStore', {
    id: 'GameAssetStore',
    state: () => ({
        HexModel: null
    }),
    getters: {
        getHexModel: (state) => state.HexModel,
    },
    actions: {
        async loadModel() {
            // await fetch("/model")
            await fetch("Hex_meter.glb")
            .then((response) => response.arrayBuffer())
            .then(arrayBuffer => {
                this.HexModel = arrayBuffer
            })
        },
    },
})