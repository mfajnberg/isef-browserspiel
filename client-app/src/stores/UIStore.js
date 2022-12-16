import { defineStore } from 'pinia' 

export const useUIStore = defineStore('UIStore', {
    id: 'UIStore',
    state: () => ({
        
        showingAuthentication: false,
        showingAvatarCreator: false,
        showingWorldmap: true,
    }),
    actions: {

        showAuthentication() {
            this.showingAvatarCreator = false
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
        showAuth() {

        }
    },
})