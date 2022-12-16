import { defineStore } from 'pinia' 

export const useUIStore = defineStore('UIStore', {
    id: 'UIStore',
    state: () => ({
        
        showingAuthentication: false,
        showingAvatarCreator: false,
        showingWorldmap: false,
    }),
    actions: {

        showAuthentication() {
            this.showingAuthentication = true
            this.showingAvatarCreator = false
            this.showingWorldmap = false
        },
        showAvatarCreator() {
            this.showingAuthentication = false
            this.showingAvatarCreator = true
            this.showingWorldmap = false
        },
        showWorldmap() {
            this.showingAuthentication = false
            this.showingAvatarCreator = false
            this.showingWorldmap = true
        },
    },
})