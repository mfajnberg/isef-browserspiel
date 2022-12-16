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
        <h1 id="logged_out" v-if="!AuthStore.loggedIn">Logged Out {{"..."}}</h1>
        <h1 id="logged_in" v-if="AuthStore.loggedIn">Logged In as {{AuthStore.Email}}</h1>
        <button @click="openAuthForm" v-if="(!UIStore.showingAuthentication)">
            jetzt spielen
        </button>
        <button id="log_out" @click="clickLogout(AuthStore)">
            ausloggen
        </button>             
        <button id="change_lang" @click="">
            Sprache ändern
        </button>        
    </div>
</template>

<style scoped>
    .header {
        width: 100vw;
        height: 20vh;
        background-color: rgb(22, 22, 22);
        position: fixed;
        left: 0%;
        top: 0%;
    }    
    #logged_in {
        color: white;
    } 
    #log_out {
        color: red;
        position: fixed;
        top: 15.5vh;
        right: 25vw;
    }    
    #change_lang {
        color: yellow;
        position: fixed;
        top: 0%;
        right: 25vw;
    }
</style>
