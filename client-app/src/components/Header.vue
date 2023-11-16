<script setup>
import { ref, onMounted } from 'vue'
import { LogOut } from '../services/AuthService.js'
import { useAuthStore } from '../stores/AuthStore.js'
import { usePartyStore } from '../stores/PartyStore'
import { useUIStore } from '../stores/UIStore.js'
import { useGameAssetStore } from '../stores/GameAssetStore'
import { useWorldStore } from '../stores/WorldStore'
import { useCreatorStore } from '../stores/AvatarCreatorStore'
import { Ambience } from '../stores/000Singletons.js'
import { loadHexCursor, loadSitePreview, spawnSite } from '../threejs/ActorManager';
import { HexVector } from '../classes/HexVector'
import { requestPartyVision } from '../services/WorldmapService'
import { requestGetChoices } from '../services/AvatarCreatorService'

    const uiStore = useUIStore()
    const authStore = useAuthStore()
    const partyStore = usePartyStore()
    const gameAssetStore = useGameAssetStore()
    const worldStore = useWorldStore()
    const creatorStore = useCreatorStore()
    const ambience = new Ambience()

    async function clickPlay() {
        uiStore.playButtonShouldShow = false
        
        if (uiStore.devMode) {

            // await requestGetChoices(authStore, creatorStore)
            // uiStore.showAvatarCreator()
            // return

            // uiStore.editorMode = true
            await gameAssetStore.devFetchHexBase()
            partyStore.party = { 
                location: {
                    Q: 0,
                    R: 0
                }
            }
            await requestPartyVision(0, 0) // broken due to being POST to local json
            uiStore.showWorldmap(worldStore, gameAssetStore)
            spawnSite(worldStore, gameAssetStore, new HexVector(1,0), 201)
            loadHexCursor()
            loadSitePreview(worldStore)
            worldStore.initialized = true
            return
        }

        if (authStore.loggedIn === false) {
            authStore.mailNotifSent = false
            authStore.regisFailed = false
            uiStore.showAuthentication()
            authStore.showLoginForm()
        }
        else {
            uiStore.setEditorMode(false)
            await uiStore.PlayNow(authStore, partyStore, worldStore, gameAssetStore, creatorStore) 
        }
    }

    function clickLogout() {
        LogOut(authStore, partyStore)
        uiStore.showHome()
        ambience.music.stop()
    }
    
    function clickHome() {
        uiStore.showHome()
        authStore.hideRegisForm()
        authStore.hideLoginForm()
        authStore.mailNotifSent = false
        authStore.regisFailed = false
        try {
            authStore.updateValidation()
        } catch (error) {}
        ambience.music.mute(true)
    }
    
    var elem = document.documentElement
    function toggleFullscreen() {
        if (!document.fullscreenElement) {
            if (elem.requestFullscreen) {
                elem.requestFullscreen()
            } else if (elem.webkitRequestFullscreen) { /* Safari */
                elem.webkitRequestFullscreen()
            } else if (elem.msRequestFullscreen) { /* IE11 */
                elem.msRequestFullscreen()
            }
            uiStore.fullscreen = true
        }
        else {
            if (document.exitFullscreen) {
                document.exitFullscreen()
            } else if (document.webkitExitFullscreen) { /* Safari */
                document.webkitExitFullscreen()
            } else if (document.msExitFullscreen) { /* IE11 */
                document.msExitFullscreen()
            }
            uiStore.fullscreen = false
        }
    }

    function playSoundPointerDown() {
        gameAssetStore.pointerDownSound.play()
    }
    function playSoundPointerUp(event) {
        if (event.button === 0) {
            gameAssetStore.pointerUpSound.play()
        }
    }

    const button_play = ref(null);
    const button_home = ref(null);
    const button_logout = ref(null);
    onMounted(() => {
        button_play.value.addEventListener('pointerdown', playSoundPointerDown);
        button_play.value.addEventListener('pointerup', playSoundPointerUp);
        button_home.value.addEventListener('pointerdown', playSoundPointerDown);
        button_logout.value.addEventListener('pointerdown', playSoundPointerDown);
    })        


</script>

<template>
    <div id="header">
    <div id="header_content" v-show="!uiStore.loadingAssets && !uiStore.showingAvatarCreator">
        
        <button id="button_home" 
            @click="clickHome()"
            ref="button_home"
            v-show="!uiStore.showingHome">
            Home
        </button>

        <button id="button_fullscreen"
            @click="toggleFullscreen"
            v-show="uiStore.showingWorldmap">
            Toggle Fullscreen
        </button>

        <button id="button_play" 
            @click="clickPlay" 
            ref="button_play"
            v-show="uiStore.showingHome || uiStore.showingImprint || uiStore.showingAbout || uiStore.playButtonShouldShow">
            Jetzt spielen
        </button>
    
        <button id="button_logout" ref="button_logout" v-show="authStore.loggedIn" @click="clickLogout()">
            Ausloggen
        </button>
    </div>
    </div>


</template>

<style scoped>
    #header {
        display: grid;
        align-items: center;
        justify-content: center;

        width: 100vw;
        height: 10vh;
        
        background: rgba(0,0,0,0);

        position: fixed;
        left: auto;
        right: auto;

        pointer-events: none;
        
        z-index: 22;
    }
    @media (max-width: 800px) {
        button {
            top: 0%;
            background-color: black;
        }

    }
    #header_content {
        display: grid;
        justify-self: center;
        align-items: center;
        justify-content: center;

        width: 100vw;
        max-width: 60rem;
        height: 10vh;
        
        background: rgba(0,0,0,0);

        position: fixed;
        left: auto;
        right: auto;
        
        z-index: 22;
    }


    #button_home {
        position: absolute;
        justify-self: left;
        /* color: white; */
        pointer-events: all;
    }
    
    #button_play {
        position: absolute;
        top: 12vh;
        justify-self: center;
        color: rgb(252, 205, 143);
        transition: .25s;
        pointer-events: all;
    } #button_play:hover {
        text-shadow: 0rem 0rem 1rem white;
        color: rgb(0, 255, 0);
    } #button_play:active {
        transition: 0s;
        color: white;
    }
    
    #button_fullscreen {
        pointer-events: all;
    }
    
    #logged_in {
        position: absolute;
        justify-self: right;
        
        color: white;
        text-shadow: 
        0px 0px 10px black,
        0px 0px 10px black,
        0px 0px 10px black;
        user-select: none;
    } 
    
    #button_logout {
        position: absolute;
        justify-self: right;

        color: red;
        pointer-events: all;
    }    
    @media (max-width: 1124px) {
        #button_play {
            top: initial;
        }
    }   
    </style>
