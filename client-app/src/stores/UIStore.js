import { defineStore } from 'pinia' 
import { DateTime, Duration } from 'luxon'
import { Ambience } from './000Singletons.js'
import { requestGetChoices } from '../services/AvatarCreatorService'
import { requestGetHexTiles } from '../services/WorldmapService'
import { loadSitePreview } from '../threejs/ActorManager.js'
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader.js'

export const useUIStore = defineStore('UIStore', {
    id: 'UIStore',
    state: () => ({
        editorMode: false,
        hoveringOverlay: false,
        
        currentTime: "",

        showingAuthentication: false,
        showingAdminPrompt: false,
        showingAvatarCreator: false,
        showingWorldmap: false,
        showingImprint: false,
        showingHome: true,

        fullscreen: false,

        loadingAssets: false,
        loadingProgress: "",

        nextUpdateTime: null, // time object, compared in init loop
        nextUpdateTimeDiff: null,
        displayNextUpdateTime: null,


        devMode: false
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
                    .diff(DateTime.local()).toFormat('hh:mm:ss')

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
            // ambience.music.mute(false)
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
            // // DEBUG-------------------------------***
            // authStore.loginResponse = { status: 200 }
            // authStore.loggedIn = true
            // partyStore.avatar = { name: "Marsilio Mirandola" }
            // this.showingWorldmap = true
            // return
            // authStore.userIsAdmin = true
            if (authStore.loginResponse.status === 200) {
                // fetch assets if not done already
                if (!gameAssetStore.assetsLoaded) {
                    this.loadingAssets = true
                    // if (localStorage.getItem("assets") == undefined) {
                    for (let uri of gameAssetStore.modelURIs) {
                        if (uri == "forest_1.glb")
                        this.loadingProgress = "Wälder aufforsten..."
                        else if (uri == "flag.glb")
                        this.loadingProgress = "Wappen und Siegel beim Heroldsamt melden..."
                        else if (uri == "house.glb")
                        this.loadingProgress = "Siedlerhöfe möblieren..."
                        else if (uri == "tent_field_camp.glb")
                        this.loadingProgress = "Heringe für die Zelte fangen..."
                        else if (uri == "crystals.glb")
                        this.loadingProgress = "Mineralien aus Erathia importieren..."
                        else if (uri == "chest_lp.glb")
                        this.loadingProgress = "Schatztruhen vergraben..."
                        else if (uri == "tree_ancient.glb")
                        this.loadingProgress = "Die alten Götter um Rat bitten..."
                        else if (uri == "HexPreview.glb")
                        this.loadingProgress = "Axiale Koordinatensysteme nachvollziehen..."
                        else if (uri == "HexPreview2.glb")
                        this.loadingProgress = "Weltherrschaftspläne aushacken..."
                        else if (uri == "HexCursor.glb")
                        this.loadingProgress = "Algorithmische Komplexität unterschätzen..."
                        else if (uri == "Arissa.fbx")
                        this.loadingProgress = "Cape ausklopfen und ausschütteln..."
                        else if (uri == "Walking.fbx")
                        this.loadingProgress = "Draußen spazieren gehen..."
                        else if (uri == "Idle.fbx")
                        this.loadingProgress = "Ein Nickerchen machen..."
                        await gameAssetStore.fetchAsset(uri, authStore)
                    }
                        // const fetchPromises = gameAssetStore.modelURIs.map(uri => 
                        //     gameAssetStore.fetchAsset(uri, authStore))
                        // await Promise.all(fetchPromises)
                        // localStorage.setItem("assets", gameAssetStore.assets3d)
                    // }
                    // else {
                    //     gameAssetStore.assets3d = localStorage.getItem("assets")
                    // }
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
                    if (!worldStore.initialized)
                        await worldStore.ACTION(gameAssetStore)
                    if (!this.editorMode) {
                        worldStore.setAbsoluteZeroOffset(
                            authStore.loginResponseData.party.location.Q,
                            authStore.loginResponseData.party.location.R
                        )
                        await requestGetHexTiles(authStore, worldStore)
                        try {
                            worldStore.character.visible = true
                        } catch (e) { }
                    } else { 
                        try {
                            worldStore.character.visible = false
                        } catch (e) { }
                    }
                    worldStore.previewModelURI = "HexPreview2.glb",
                    loadSitePreview(new GLTFLoader(), worldStore, gameAssetStore)
                    this.showWorldmap(worldStore, gameAssetStore)
                }
            }
        },
        
        setEditorMode(bool) { this.editorMode = bool}
    },
})