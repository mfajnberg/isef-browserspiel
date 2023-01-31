import { defineStore } from 'pinia' 
import { DateTime } from 'luxon'
import { Howl } from 'howler'

export const useUIStore = defineStore('UIStore', {
    id: 'UIStore',
    state: () => ({
        editorMode: true,
        
        currentTime: "",

        showingAuthentication: false,
        showingAdminPrompt: false,
        showingAvatarCreator: false,
        showingWorldmap: false,
        showingImprint: false,
        showingHome: true

    }),
    getters: {
        getShowingAuthentication: (state) => state.showingAuthentication,
        getShowingAdminPrompt:(state) => state.showingAdminPrompt,
        getShowingAvatarCreator:(state) => state.showingAvatarCreator,
        getShowingWorldmap:(state) => state.showingWorldmap,
        getShowingImprint:(state) => state.showingImprint,
        getShowingHome:(state) => state.showingHome
    },
    actions: {
        updateClock() {
            this.currentTime = DateTime.local().toLocaleString(DateTime.TIME_24_WITH_SECONDS)
        },
        showHome() {
            this.showingHome = true
            this.showingAuthentication = false
            this.showingAdminPrompt = false
            this.showingAvatarCreator = false
            this.showingWorldmap = false
            this.showingImprint = false
            var header = document.getElementById("header")
            header.style.background = "rgba(0,0,0,0)"
        },
        showAuthentication() {
            this.showingHome = false
            this.showingAuthentication = true
            this.showingAdminPrompt = false
            this.showingAvatarCreator = false
            this.showingWorldmap = false
            this.showingImprint = false
        },
        showAdminPrompt() {
            this.showingHome = false
            this.showingAuthentication = false
            this.showingAdminPrompt = true
            this.showingAvatarCreator = false
            this.showingWorldmap = false
            this.showingImprint = false
        },
        showAvatarCreator() {
            this.showingHome = false
            this.showingAuthentication = false
            this.showingAdminPrompt = false
            this.showingAvatarCreator = true
            this.showingWorldmap = false
            this.showingImprint = false
        },
        showWorldmap() {
            this.showingHome = false
            this.showingAuthentication = false
            this.showingAdminPrompt = false
            this.showingAvatarCreator = false
            this.showingWorldmap = true
            this.showingImprint = false
            var header = document.getElementById("header")
            header.style.background = "linear-gradient(180deg, rgba(0,0,0,0) 0%, rgba(0,0,0,1) 100%)"       
        },
        showImprint() {
            this.showingHome = false
            this.showingAuthentication = false
            this.showingAdminPrompt = false
            this.showingAvatarCreator = false
            this.showingWorldmap = false
            this.showingImprint = true
            var header = document.getElementById("header")
            header.style.background = "rgba(0,0,0,0)"
        },
    },
})