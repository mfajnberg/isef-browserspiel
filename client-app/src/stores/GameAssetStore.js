import { defineStore } from 'pinia' 

export const useGameAssetStore = defineStore('GameAssetStore', {
    id: 'GameAssetStore',
    state: () => ({
        HexModel: null,
        HexCursor: null
    }),
    getters: {
        getHexModel: (state) => state.HexModel,
        getHexCursor: (state) => state.HexCursor,
    },
    actions: {
        async loadHex() {
            // await fetch("/model")
            await fetch("HexBase.glb")
            .then((response) => response.arrayBuffer())
            .then(arrayBuffer => {
                this.HexModel = arrayBuffer
            })
        },
        async loadHexCursor() {
            // await fetch("/model")
            await fetch("HexCursor.glb")
            .then((response) => response.arrayBuffer())
            .then(arrayBuffer => {
                this.HexCursor = arrayBuffer
            })
        },
    },
})