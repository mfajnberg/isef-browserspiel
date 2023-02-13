<script setup>
import { ref, onMounted } from 'vue'
import { LogOut } from '../services/AuthService.js'
import { useAuthStore } from '../stores/AuthStore.js'
import { usePartyStore } from '../stores/PartyStore';
import { useUIStore } from '../stores/UIStore.js'
import { useGameAssetStore } from '../stores/GameAssetStore'
import { useWorldStore } from '../stores/WorldStore';
import { useCreatorStore } from '../stores/AvatarCreatorStore';
import { Ambience } from '../stores/000Singletons.js'

    const uiStore = useUIStore()
    const authStore = useAuthStore()
    const partyStore = usePartyStore()
    const assetStore = useGameAssetStore()
    const worldStore = useWorldStore()
    const creatorStore = useCreatorStore()
    const ambience = new Ambience()

    async function clickPlay() {
        if (authStore.loggedIn === false) {
            authStore.mailNotifSent = false
            authStore.regisFailed = false
            uiStore.showAuthentication()
            authStore.showLoginForm()
        }
        else {
            uiStore.setEditorMode(false)
            await uiStore.PlayNow(authStore, partyStore, worldStore, assetStore, creatorStore) 
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
        authStore.updateValidation()
        ambience.music.mute(true)
    }

    function playSoundPointerDown() {
        assetStore.pointerDownSound.play()
    }
    function playSoundPointerUp(event) {
        if (event.button === 0) {
            assetStore.pointerUpSound.play()
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
    <div id="header_content">
        <!-- <div id="3js_gui"></div>
        <h3 id="logged_in" v-if="authStore.loggedIn">
            Eingeloggt: {{authStore.Email}}
        </h3> -->
        
        <button id="button_play" 
            @click="clickPlay" 
            ref="button_play"
            v-show="!uiStore.showingAuthentication && !uiStore.showingWorldmap && !uiStore.showingAvatarCreator">
            Jetzt spielen
        </button>

        <button id="button_home" 
            @click="clickHome()"
            ref="button_home"
            v-show="!uiStore.showingHome">
            Home
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
        
        z-index: 22;
    }
    @media (max-width: 800px) {
        button {
            top: 0%;
            /* margin-top: 5vh; */
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
    }

    #button_play {
        position: absolute;
        justify-self: center;
        /* color: rgb(252, 205, 143); */
    } #button_play:hover {
        text-shadow: 0rem 0rem 1rem white;
        color: rgb(0, 255, 0);
    } #button_play:active {
        color: white;
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
    }    
/* @media (max-width: 700px) {
    #button_logout, #logged_in {
        right: 0%;
    }
    #button_home {
        left: 0%;
    }
}    */
</style>
