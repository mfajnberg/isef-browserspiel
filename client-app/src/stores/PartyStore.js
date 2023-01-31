import { defineStore } from 'pinia' 

export const usePartyStore = defineStore('PartyStore', {
    id: 'PartyStore',
    state: () => ({
        coordinates: { 
            Q: 0,
            R: 0 
        },
        avatar: null, // creature
        members: [], // array of creatures; also used as an array of avatar options for new players
        backpack: [] // array of items

    }),
    getters: {

    },
    actions: {

    },
})
