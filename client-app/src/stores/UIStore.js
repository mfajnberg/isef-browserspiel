import { defineStore } from 'pinia' 
import { DateTime } from 'luxon'

export const useUIStore = defineStore('UIStore', {
    id: 'UIStore',
    state: () => ({
        
        showingAuthentication: false,
        showingAvatarCreator: false,
        showingWorldmap: false,
        showingImprint: false,
        currentTime: ""
    }),
    actions: {
        getCurrentTime() {
            this.currentTime = DateTime.local().toLocaleString(DateTime.TIME_24_WITH_SECONDS)
        },
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
            var header = document.getElementById("header")
            header.style.background = "linear-gradient(180deg, rgba(0,0,0,0) 0%, rgba(0,0,0,1) 100%)"       
        },
        showImprint() {
            this.showingAuthentication = false
            this.showingAvatarCreator = false
            this.showingWorldmap = false
            this.showingImprint = true
            var header = document.getElementById("header")
            header.style.background = "rgba(0,0,0,0)"
        },
        showHome() {
            this.showingAuthentication = false
            this.showingAvatarCreator = false
            this.showingWorldmap = false
            this.showingImprint = false
            var header = document.getElementById("header")
            header.style.background = "rgba(0,0,0,0)"
        },
    },
})