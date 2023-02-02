import { defineStore } from 'pinia' 
import { fetchGetChoices, fetchPostChoice } from '../services/AvatarCreatorService'

export const useCreatorStore = defineStore('CreatorStore', {
    id: 'CreatorStore',
    state: () => ({
        statBlocks: [],
        creationResponse: null

    }),
    getters: {
        getStatBlocks:(state)=>state.statBlocks,
        getCreationResponse:(state)=>state.creationResponse

    },
    actions: {
        async fetchAvatarCreationChoices() {
            await fetchGetChoices(this)
        },
    },
})
