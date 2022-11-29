<script setup>
import { useStore } from './stores/ReactiveStore.js'
import Gallery from './vue-widgets/landingpage/Gallery.vue'
import RegistrationForm from './vue-widgets/landingpage/RegistrationForm.vue'
import LoginForm from './vue-widgets/landingpage/LoginForm.vue'

    const store = useStore()
</script>

<template>
    <div class="header">
        <h1 class="game_title">Hello World</h1>
    </div>
    <div class="content">
        <Gallery/>
        <RegistrationForm/>  
        <LoginForm/>  
    </div>
    <div class="footer"></div>
</template>

<style scoped>
    .game_title {
        color: white;
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
