import { defineStore } from 'pinia' 
import { DateTime, Duration } from 'luxon'
import { Ambience } from './000Singletons.js'
import { requestGetChoices } from '../services/AvatarCreatorService'
import { requestPartyVision } from '../services/WorldmapService'
import { loadHexCursor, loadSitePreview } from '../threejs/ActorManager.js'
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader.js'

export const useUIStore = defineStore('UIStore', {
    id: 'UIStore',
    state: () => ({
        devMode: false,

        showingHome: true,
        showingAuthentication: false,
        showingAdminPrompt: false,
        showingAvatarCreator: false,
        showingWorldmap: false,
        showingImprint: false,
        
        loadingAssets: false,
        loadingProgress: "",

        fullscreen: false,
        editorMode: false,
        
        currentTime: "",
        nextUpdateTime: null, // time object, compared in init loop
        nextUpdateTimeDiff: null,
        displayNextUpdateTime: null,
        
        hoveringOverlay: false,
        hoveredName:"",
        rightClick: false,
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
        updateCountdown() {
            try {
                this.displayNextUpdateTime = this.nextUpdateTime
                    .diff(DateTime.local())
                    // .plus({seconds: 1})
                    .toFormat('hh:mm:ss')

                // this.displayNextUpdateTime = Duration
                //     .fromObject(nextUpdateTimeDiff)
                //     .toFormat('hh:mm:ss')
            } catch (e) {
                console.log(e)
            }
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

            const ambience = new Ambience()
            if (!ambience.music.playing()){
                ambience.music.play()
            }
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

        async PlayNow(authStore, partyStore, worldStore, gameAssetStore, creatorStore) {
            if (authStore.loginResponse.status === 200) {

                // fetch assets if not done already
                if (!gameAssetStore.assetsLoaded) {
                    this.loadingAssets = true
                    for (let uri of gameAssetStore.modelURIs) {
                        await gameAssetStore.fetchAsset(uri)
                    }
                    for (let asset of gameAssetStore.assets3d) {
                        if (asset.name == "HexBase.glb")
                            gameAssetStore.hexModel = asset.data
                    }                    
                    gameAssetStore.assetsLoaded = true
                    this.loadingAssets = false
                }

                // admin just logged in
                if (authStore.userIsAdmin == true && !this.getShowingAdminPrompt) {
                    this.showAdminPrompt()
                }

                // new player just logged in || new admin wants to play
                else if (partyStore.avatar === null) {
                    await requestGetChoices(authStore, creatorStore)
                    this.showAvatarCreator()
                }

                // existing player logged in or admin wants to play
                else {
                    if (!this.editorMode) {
                        await requestPartyVision(
                            partyStore.party.location.Q,
                            partyStore.party.location.R
                        )
                        try {
                            partyStore.pawn3d.visible = true
                        } catch (e) { }
                    } else { 
                        try {
                            partyStore.pawn3d.visible = false
                        } catch (e) { }
                    }
                    loadHexCursor()
                    worldStore.previewModelURI = "3dCursorCross.glb"
                    loadSitePreview(worldStore)
                    worldStore.initialized = true
                    this.showWorldmap(worldStore, gameAssetStore)
                }
            }
        },
        
        setEditorMode(bool) { this.editorMode = bool}
    },
})