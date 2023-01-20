<script setup>
import { onMounted } from 'vue'
import { Howler } from 'howler'
import { useAuthStore } from './stores/AuthStore.js'
import { useUIStore } from './stores/UIStore.js'
import { requestTokenRefresh } from './services/AuthService.js'

import Header from './components/Header.vue'
import Footer from './components/Footer.vue'
import Imprint from './components/Imprint.vue'
import Overlay from './components/ingame/Overlay.vue'
import AvatarCreator from './components/ingame/AvatarCreator.vue'
import Authenticator from './components/authentication/Authenticator.vue'

    const AuthStore = useAuthStore()
    const UIStore = useUIStore()
    onMounted(() => {
        if (localStorage.token != null) {
            AuthStore.Email = localStorage.Email
            requestTokenRefresh(AuthStore)
        }
        Howler.debug = true 
    })
</script>

<template>
    <div id="background">
        <Header/>
        <div class="content">
            <!-- <img src="/border1.png" class="border" id="b4"> -->
            <Authenticator v-show="UIStore.showingAuthentication"/>
            <AvatarCreator v-if="UIStore.showingAvatarCreator"/>
            <canvas canvas id="adventure_map" v-show="UIStore.showingWorldmap"></canvas>
            <Imprint v-if="UIStore.showingImprint"/>
            <Overlay v-if="UIStore.showingWorldmap" id="overlay"/>
        </div>
        <Footer/>
    </div>
</template>

<style scoped>

#background {
    position: fixed;
    height: 100vh;
    width: 100vw;
    left: 0;
    top: 0;

    background-image: url("twilight_aegis.jpg");
        background-repeat: no-repeat;
        background-attachment: fixed;
        background-position: center;
        background-size: cover;
}

    .content {
        display: flex;
        width: 100%;
        height: 75vh;

        position: relative;
        left: 0%;
        top: 20vh;

        align-items: center;
        justify-content: center;
    }
    
    #adventure_map {
    background-color: rgba(0, 0, 0, 0.851);
    z-index: 10;
    }

    #overlay {
        position: absolute;
        height: 100%;
        z-index: 11;
        pointer-events:none;
    }

</style>
