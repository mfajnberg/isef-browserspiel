import { defineStore } from 'pinia' 
import { HexVector } from '../classes/HexVector'
import { useWorldStore } from './WorldStore'

export const usePartyStore = defineStore('PartyStore', {
    id: 'PartyStore',
    state: () => ({
        party: null, // .location.Q
        pawn3d: null,
        avatar: null,

        travelOK: false,
        traveling: false,
        start: null,
        goal: null,
        
        portraitUri: ""
    }),
    getters: {
        getPortraitUri:(state) => state.portraitUri,
    },
    actions: {
        updatePawn3dPosition() {
            const partyLocationHexVector = new HexVector(this.party.location.Q, this.party.location.R)
            const posX = partyLocationHexVector.getWorldXFromAxialQ()
            const posZ = partyLocationHexVector.getWorldZFromAxialR()
            try {
                this.pawn3d.position.x = posX
                this.pawn3d.position.z = posZ
            } catch (error) { 
                // console.log(error) 
            }
        }
    },
})
