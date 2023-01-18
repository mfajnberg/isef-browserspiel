<script setup>
import { useAuthStore } from '../stores/AuthStore.js'
import { LogOut } from '../services/AuthService.js'
import { useUIStore } from '../stores/UIStore';

    const AuthStore = useAuthStore()
    const UIStore = useUIStore()

    function playNow() {
        if (!AuthStore.loggedIn) {
            UIStore.showAuthentication()
            AuthStore.showLoginForm()
        }
        else {
            UIStore.showWorldmap()
        }
    }

    function clickLogout(store) {
        LogOut(store)
        UIStore.showHome()
    }
    
    function clickHome() {
        UIStore.showHome()
        AuthStore.hideRegisForm()
        AuthStore.hideLoginForm()
    }

</script>

<template>
    <div id="header">
        <div id="3js_gui"></div>
        <h3 id="logged_in" v-if="AuthStore.loggedIn">
            Eingeloggt: {{AuthStore.Email}}
        </h3>
        
        <button id="play_now" 
            @click="playNow" 
            v-if="!UIStore.showingAuthentication && !UIStore.showingWorldmap">
            jetzt spielen
        </button>

        <button id="home" @click="clickHome()">
            home
        </button>

        <button id="log_out" v-if="AuthStore.loggedIn" @click="clickLogout(AuthStore)">
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


    #home {
        position: absolute;
        left: 25vw;
        top: 10vh;
        color: white;
    }

    #play_now {
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
    
    #log_out {
        position: absolute;
        color: red;
        position: fixed;
        top: 10vh;
        right: 25vw;
    }    
@media (max-width: 700px) {
    #log_out, #logged_in {
        right: 0%;
    }
    #home {
        left: 0%;
    }
}   
</style>
