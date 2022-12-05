import { defineStore } from 'pinia' 

export const useUIStore = defineStore('UIStore', {
    id: 'UIStore',
    state: () => ({
        
        showingAuthentication: false,
        showingAvatarCreator: false,
    }),
    actions: {

        showAuthentication() {
            this.showingAuthentication = true
        },
        showAvatarCreator() {
            this.showingAvatarCreator = true
        },
        hideAuthentication() {
            this.showingAuthentication = false
        },
        hideAvatarCreator() {
            this.showingAvatarCreator = false
        },
    },
})