<script setup>
import { ref } from 'vue'
import { useAuthUIStore } from './stores/AuthUIStore.js'

import Gallery from './vue-widgets/Gallery.vue'
import RegistrationForm from './vue-widgets/RegistrationForm.vue'
import LoginForm from './vue-widgets/LoginForm.vue'
import Worldmap from './vue-widgets/Worldmap.vue'

    const AuthUI = useAuthUIStore()
</script>

<template>
    <div class="header">
        <h1 class="game_title">Hello World</h1>
        <button @click="(AuthUI.showLoginForm)"
                v-if="(!AuthUI.showingRegisForm && !AuthUI.showingLoginForm)" 
                class="play_now">
            jetzt spielen
        </button>
    </div>
    <div class="content">
        <Worldmap/>
        <Gallery/>
        <RegistrationForm v-if="AuthUI.showingRegisForm"/>  
        <LoginForm v-if="AuthUI.showingLoginForm"/>  
    </div>
    <div class="footer"></div>
</template>

<style scoped>
    .game_title {
        color: white;
    }
    .play_now {
        color: red;
        font-style: italic;
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
        background-color: rgb(109, 109, 109);
        position: absolute;
        left: 0%;
        top: 20vh;

        align-items: center;
        justify-content: center;
    }
    .footer {
        width: 100vw;
        height: 5vh;
        background-color: black;
        position: fixed;
        left: 0%;
        bottom: 0%;
    }
</style>
