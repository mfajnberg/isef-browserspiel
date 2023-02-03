import { defineStore } from 'pinia' 

export const usePartyStore = defineStore('PartyStore', {
    id: 'PartyStore',
    state: () => ({
        coordinates: { 
            Q: 0,
            R: 0 
        },
        pawn3d: null,
        
        party: null,
        avatar: null, // creature (player avatar)
    }),
    getters: {
        getCoordinates: (state) => state.coordinates,
        getPawn: (state) => state.pawn3d,
        getAvatar: (state) => state.avatar,
        getMembers: (state) => state.members,
        getBackpack: (state) => state.backpack
    },
    actions: {
        updatePartyLocation(hexVector) {
            // set pawn world transform

        }

    },
})
