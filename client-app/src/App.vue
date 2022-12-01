<script setup>
import { ref } from 'vue'
import { useAuthUIStore } from './stores/AuthUIStore.js'
import { LogOut } from './services/Authentication.js'

import Gallery from './vue-widgets/Gallery.vue'
import RegistrationForm from './vue-widgets/RegistrationForm.vue'
import LoginForm from './vue-widgets/LoginForm.vue'
import Worldmap from './vue-widgets/Worldmap.vue'

    const AuthUIStore = useAuthUIStore()
</script>

<template>
    <div class="header">
        <h1 class="logged_out" v-if="!AuthUIStore.loggedIn">Logged Out {{"..."}}</h1>
        <h1 class="logged_in" v-if="AuthUIStore.loggedIn">Logged In as {{"user x"}}</h1>
        <button @click="(AuthUIStore.showLoginForm)" v-if="(!AuthUIStore.showingRegisForm && !AuthUIStore.showingLoginForm)" class="play_now">
            jetzt spielen
        </button>
        <button class="log_out" @click="LogOut(AuthUIStore)" v-if="!AuthUIStore.loggedIn">
            ausloggen
        </button>             
        <button class="change_lang" @click="">
            Sprache ändern
        </button>        
    </div>
    <div class="content">
        <Worldmap/>
        <Gallery/>
        <RegistrationForm v-if="AuthUIStore.showingRegisForm"/>  
        <LoginForm v-if="AuthUIStore.showingLoginForm"/>  
    </div>
    <div class="footer">
        <a>v0.1a</a> |
        <a>Jobs</a> |
        <a>FAQs</a> |
        <a>Terms</a> |
        <a>Imprint</a>
    </div>
</template>

<style scoped>
    .logged_in {
        color: white;
    }
    .play_now {
        color: rgb(0, 255, 0);
    }    
    .log_out {
        color: red;
        position: fixed;
        top: 15.5vh;
        right: 25vw;
    }    
    .change_lang {
        color: yellow;
        position: fixed;
        top: 0%;
        right: 25vw;
    }
    .header {
        width: 100vw;
        height: 20vh;
        background-color: rgb(0, 0, 0);
        position: fixed;
        left: 0%;
        top: 0%;
    }    
    .content {
        display: flex;
        width: 100vw;
        height: 75vh;
        background-image: url(twilight_aegis.jpg);
        background-repeat: no-repeat;
        background-attachment: fixed;
        background-position: center;
        background-size: cover;
        position: absolute;
        left: 0%;
        top: 20vh;

        align-items: center;
        justify-content: center;
    }
    .footer {
        display: flex;
        width: 100vw;
        height: 5vh;
        background-color: black;
        position: fixed;
        left: 0%;
        bottom: 0%;
        justify-content: center;
        align-items: center;
        white-space: pre-wrap;
    }
    .footer > * {
        color: grey;
        cursor: pointer;
    }
</style>
