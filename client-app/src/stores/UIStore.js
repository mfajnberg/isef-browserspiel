import { defineStore } from 'pinia' 
import { DateTime } from 'luxon'
import { Ambience } from './000Singletons.js'
import { requestGetChoices } from '../services/AvatarCreatorService'
import { requestGetHexTiles } from '../services/WorldmapService'
import { loadSitePreview } from '../threejs/ActorManager.js'
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader.js'

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

        loadingAssets: false,
        loadingProgress: "",

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

        async PlayNow(authStore, partyStore, worldStore, gameAssetStore, creatorStore) {
            // // DEBUG-------------------------------***
            // authStore.loginResponse = { status: 200 }
            // authStore.loggedIn = true
            // authStore.userIsAdmin = true
            // partyStore.avatar = {
            //     name: "Marsilio Mirandola"
            // }
            if (authStore.loginResponse.status === 200) {
                // fetch assets if not done already
                if (!gameAssetStore.assetsLoaded) {
                    this.loadingAssets = true
                    for (let uri of gameAssetStore.modelURIs) {
                        if (uri == "forest_1.glb")
                        this.loadingProgress = "Wälder aufforsten..."
                        else if (uri == "flag.glb")
                        this.loadingProgress = "Wappen und Siegel beim Heroldsamt melden..."
                        else if (uri == "house.glb")
                        this.loadingProgress = "Siedlerhöfe möblieren..."
                        else if (uri == "tent_field_camp.glb")
                        this.loadingProgress = "Heringe für die Zelte besorgen..."
                        else if (uri == "crystals.glb")
                        this.loadingProgress = "Mineralien aus Erathia importieren..."
                        else if (uri == "chest_lp.glb")
                        this.loadingProgress = "Schatztruhen vergraben..."
                        else if (uri == "tree_ancient.glb")
                        this.loadingProgress = "Die alten Götter anbeten..."
                        await gameAssetStore.fetchAsset(uri, authStore)
                    }
                    await gameAssetStore.fetchAsset("Arissa.fbx", authStore)
                    await gameAssetStore.fetchAsset("Idle.fbx", authStore)
                    await gameAssetStore.fetchAsset("Walking.fbx", authStore)
                    console.log(gameAssetStore.assets3d)
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
                else  {
                    if (!worldStore.initialized)
                        await worldStore.ACTION(gameAssetStore)
                    if (!this.editorMode) {
                        await requestGetHexTiles(authStore, worldStore)
                        worldStore.previewModelURI = "HexPreview.glb"
                        // loadSitePreview(new GLTFLoader(), worldStore, gameAssetStore)
                    }
                    this.showWorldmap(worldStore, gameAssetStore)
                }
            }
        },
        
        setEditorMode(bool) { this.editorMode = bool}
    },
})