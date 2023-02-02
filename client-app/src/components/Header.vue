<script setup>
import { Ambience } from '../services/AmbienceService.js'
import { useAuthStore } from '../stores/AuthStore.js'
import { usePartyStore } from '../stores/PartyStore';
import { useUIStore } from '../stores/UIStore.js'
import { useGameAssetStore } from '../stores/GameAssetStore'
import { LogOut } from '../services/AuthService.js'
import { ref, onMounted } from 'vue'

    const uiStore = useUIStore()
    const authStore = useAuthStore()
    const partyStore = usePartyStore()
    const assetStore = useGameAssetStore()

    const ambience = new Ambience()


    function clickPlay() {
        if (!authStore.loggedIn) {
            uiStore.showAuthentication()
            authStore.showLoginForm()
        }
        else {
            uiStore.showWorldmap()
            if (!ambience.music.playing()) {
                ambience.music.play()
            } ambience.music.mute(false)
        }
    }

    function clickLogout() {
        LogOut(authStore, partyStore)
        uiStore.showHome()
        ambience.music.pause()
    }
    
    function clickHome() {
        uiStore.showHome()
        authStore.hideRegisForm()
        authStore.hideLoginForm()
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
        // button_home.value.addEventListener('pointerup', playSoundPointerUp);
        button_logout.value.addEventListener('pointerdown', playSoundPointerDown);
        // button_logout.value.addEventListener('pointerup', playSoundPointerUp);
    })        


</script>

<template>
    <div id="header">
        <div id="3js_gui"></div>
        <h3 id="logged_in" v-if="authStore.loggedIn">
            Eingeloggt: {{authStore.Email}}
        </h3>
        
        <button id="button_play" 
            @click="clickPlay" 
            ref="button_play"
            v-show="!uiStore.showingAuthentication && !uiStore.showingWorldmap && !uiStore.showingAvatarCreator">
            jetzt spielen
        </button>

        <button id="button_home" 
            @click="clickHome()"
            ref="button_home"
            v-show="!uiStore.showingHome">
            home
        </button>

        <button id="button_logout" ref="button_logout" v-show="authStore.loggedIn" @click="clickLogout()">
            ausloggen
        </button>

    </div>
</template>

<style scoped>
    #header {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 100vw;
        height: 10vh;
        
        background: rgba(0,0,0,0);

        position: fixed;
        left: 0%;
        top: 0%;
        
        z-index: 20;
    }


    #button_home {
        position: absolute;
        left: 25vw;
        top: 7vh;
        color: white;
    }

    #button_play {
        position: absolute;
        top: 7vh;
    }

    #logged_in {
        position: absolute;
        right: 25vw;
        /* top: 0%; */

        color: white;
        text-shadow: 
            0px 0px 10px black,
            0px 0px 10px black,
            0px 0px 10px black;
    } 
    
    #button_logout {
        position: absolute;
        color: red;
        position: fixed;
        top: 7vh;
        right: 25vw;
    }    
@media (max-width: 700px) {
    #button_logout, #logged_in {
        right: 0%;
    }
    #button_home {
        left: 0%;
    }
}   
</style>
