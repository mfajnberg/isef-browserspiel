import { defineStore } from 'pinia' 

export const useUIStore = defineStore('UIStore', {
    id: 'UIStore',
    state: () => ({
        
        showingAuthentication: false,
        showingAvatarCreator: false,
        showingWorldmap: false,
        showingImprint: false,
    }),
    actions: {

        showAuthentication() {
            this.showingAuthentication = true
            this.showingAvatarCreator = false
            this.showingWorldmap = false
            this.showingImprint = false
        },
        showAvatarCreator() {
            this.showingAuthentication = false
            this.showingAvatarCreator = true
            this.showingWorldmap = false
            this.showingImprint = false
        },
        showWorldmap() {
            this.showingAuthentication = false
            this.showingAvatarCreator = false
            this.showingWorldmap = true
            this.showingImprint = false
        },
        showImprint() {
            this.showingAuthentication = false
            this.showingAvatarCreator = false
            this.showingWorldmap = false
            this.showingImprint = true
        },
    },
})