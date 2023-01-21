<script setup>
import { Howl } from 'howler';
import { Ambience } from '../services/howlee.js'
import { useAuthStore } from '../stores/AuthStore.js'
import { LogOut } from '../services/AuthService.js'
import { useUIStore } from '../stores/UIStore.js';
import { ref, onMounted } from 'vue';

    const AuthStore = useAuthStore()
    const UIStore = useUIStore()
    const ambience = new Ambience()


    function playGame() {
        if (!AuthStore.loggedIn) {
            UIStore.showAuthentication()
            AuthStore.showLoginForm()
        }
        else {
            UIStore.showWorldmap()
            ambience.music.play()
        }
    }

    function clickLogout(store) {
        LogOut(store)
        UIStore.showHome()
        ambience.music.pause()
    }
    
    function clickHome() {
        UIStore.showHome()
        AuthStore.hideRegisForm()
        AuthStore.hideLoginForm()
        ambience.music.pause()
    }

    function playSoundPointerDown() {
        UIStore.pointerDownSound.play()
    }
    function playSoundPointerUp() {
        UIStore.pointerUpSound.play()
    }

    const button_play = ref(null);
    const button_home = ref(null);
    const button_logout = ref(null);
    onMounted(() => {
        button_play.value.addEventListener('pointerdown', playSoundPointerDown);
        button_play.value.addEventListener('pointerup', playSoundPointerUp);
        button_home.value.addEventListener('pointerdown', playSoundPointerDown);
        button_home.value.addEventListener('pointerup', playSoundPointerUp);
        button_logout.value.addEventListener('pointerdown', playSoundPointerDown);
        button_logout.value.addEventListener('pointerup', playSoundPointerUp);
    })        


</script>

<template>
    <div id="header">
        <div id="3js_gui"></div>
        <h3 id="logged_in" v-if="AuthStore.loggedIn">
            Eingeloggt: {{AuthStore.Email}}
        </h3>
        
        <button id="button_play" 
            @click="playGame" 
            ref="button_play"
            v-show="!UIStore.showingAuthentication && !UIStore.showingWorldmap">
            jetzt spielen
        </button>

        <button id="button_home" @click="clickHome()" ref="button_home">
            home
        </button>

        <button id="button_logout" ref="button_logout" v-show="AuthStore.loggedIn" @click="clickLogout(AuthStore)">
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
        height: 20vh;
        
        background: rgba(0,0,0,0);

        position: fixed;
        left: 0%;
        top: 0%;
        
        z-index: 20;
    }


    #button_home {
        position: absolute;
        left: 25vw;
        top: 10vh;
        color: white;
    }

    #button_play {
        position: absolute;
        top: 10vh;
    }

    #logged_in {
        position: absolute;
        right: 25vw;
        top: 0%;

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
        top: 10vh;
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
