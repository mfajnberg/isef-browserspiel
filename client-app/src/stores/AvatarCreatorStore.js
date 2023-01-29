import { defineStore } from 'pinia' 
import { requestAvatarChoices } from '../services/AvatarCreatorService'

export const useCreatorStore = defineStore('CreatorStore', {
    id: 'CreatorStore',
    state: () => ({
        statBlocks: []

    }),
    getters: {

    },
    actions: {
        async getAvatarCreationChoices() {
            await requestAvatarChoices(this)
        },
        async postAvatarChoice() {
            //...
        }
    },
})
