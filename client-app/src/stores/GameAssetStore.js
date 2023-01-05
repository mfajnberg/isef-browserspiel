import { defineStore } from 'pinia' 

export const useGameAssetStore = defineStore('GameAssetStore', {
    id: 'GameAssetStore',
    state: () => ({
        HexTileModel: null
    }),
    getters: {
    },
    actions: {
        loadModel() {
            fetch("/api/static-assets/hex-tile-glb")
            .then((response) => console.log(response))
        },
    },
})