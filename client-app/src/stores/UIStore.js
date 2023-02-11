import { defineStore } from 'pinia' 
import { DateTime } from 'luxon'
import { Ambience } from './000Singletons.js'
import { requestGetChoices } from '../services/AvatarCreatorService'
import { requestGetHexTiles } from '../services/WorldmapService'

export const useUIStore = defineStore('UIStore', {
    id: 'UIStore',
    state: () => ({
        editorMode: false,
        
        currentTime: "",

        showingAuthentication: false,
        showingAdminPrompt: false,
        showingAvatarCreator: false,
        showingWorldmap: false,
        showingImprint: false,
        showingHome: true,

        nextUpdate: null // time object, compared in init loop
    }),
    getters: {
        getShowingAuthentication: (state) => state.showingAuthentication,
        getShowingAdminPrompt:(state) => state.showingAdminPrompt,
        getShowingAvatarCreator:(state) => state.showingAvatarCreator,
        getShowingWorldmap:(state) => state.showingWorldmap,
        getShowingImprint:(state) => state.showingImprint,
        getShowingHome:(state) => state.showingHome,
        getIsEditorMode:(state) => state.editorMode
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
        showWorldmap(worldStore, assetStore) {
            if (!worldStore.initialized)
                worldStore.ACTION(assetStore)

            this.showingHome = false
            this.showingAuthentication = false
            this.showingAdminPrompt = false
            this.showingAvatarCreator = false
            this.showingWorldmap = true
            this.showingImprint = false

            // var header = document.getElementById("header")
            // header.style.background = "linear-gradient(180deg, rgba(0,0,0,0) 0%, rgba(0,0,0,1) 100%)"       
            const ambience = new Ambience()
            if (!ambience.music.playing()){
                ambience.music.play()
            }
            ambience.music.mute(false)
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

        async PlayNow(authStore, partyStore, worldStore, assetStore, creatorStore) {
            // // DEBUG-------------------------------***
            // authStore.loginResponse = { status: 200 }
            // authStore.loggedIn = true
            // authStore.userIsAdmin = true
            // partyStore.avatar = {
            //     name: "Marsilio Mirandola"
            // }
            // -.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-
            if (authStore.loginResponse.status === 200) {
                if (authStore.userIsAdmin == true && !this.getShowingAdminPrompt) {
                    console.log("Admin status detected...")
                    this.showAdminPrompt()
                }
                else if (partyStore.avatar === null) {
                    console.log("No avatar yet...")
                    await requestGetChoices(authStore, creatorStore)
                    this.showAvatarCreator()
                }
                else  {
                    if (!worldStore.initialized)
                        await worldStore.ACTION(assetStore)
                    if (!this.editorMode)
                        worldStore.preview = null
                        await requestGetHexTiles(authStore, worldStore)
                    
                    this.showWorldmap(worldStore, assetStore)
                }
            }
        },
        setEditorMode(bool) { this.editorMode = bool}
    },
})