<script setup>
import { useAuthStore } from '../stores/AuthStore.js'
import { LogOut } from '../services/AuthService.js'
import { useUIStore } from '../stores/UIStore';

    const AuthStore = useAuthStore()
    const UIStore = useUIStore()

    function openAuthForm() {
        UIStore.showAuthentication()
        AuthStore.showLoginForm()
    }

    async function clickLogout(store) {
        LogOut(store)
        UIStore.showWorldmap()
    }

</script>

<template>
    <div class="header">
        <h2 id="logged_out" v-if="!AuthStore.loggedIn">Logged Out {{"..."}}</h2>
        <h2 id="logged_in" v-if="AuthStore.loggedIn">Logged In as {{AuthStore.Email}}</h2>
        <button id="play_now" @click="openAuthForm" v-if="(!UIStore.showingAuthentication)">
            jetzt spielen
        </button>
        <button id="log_out" @click="clickLogout(AuthStore)">
            ausloggen
        </button>

    </div>
</template>

<style scoped>
    .header {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 100vw;
        height: 20vh;
        background-color: rgb(22, 22, 22);

        position: fixed;
        left: 0%;
        top: 0%;
        
        z-index: 20;
    }


    #play_now {
        position: absolute;
        top: 10vh;
    }
    
    #logged_in, #logged_out {
        position: absolute;
        color: white;
        text-shadow: 
            0px 0px 10px black,
            0px 0px 10px black,
            0px 0px 10px black;
        right: 25vw;
        top: 0%;
    } 
    
    #log_out {
        position: absolute;
        color: red;
        position: fixed;
        top: 10vh;
        right: 25vw;
    }    
@media (max-width: 700px) {
    #log_out, #logged_out, #logged_in {
        right: 0%;
    }
}   
</style>
