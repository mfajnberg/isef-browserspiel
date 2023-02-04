<script setup>
import { onMounted } from 'vue'
import { Howler } from 'howler'
import { useAuthStore } from './stores/AuthStore.js'
import { useUIStore } from './stores/UIStore.js'
import { requestTokenRefresh } from './services/AuthService.js'

import Header from './components/Header.vue'
import Footer from './components/Footer.vue'
import Imprint from './components/Imprint.vue'
import Authenticator from './components/authentication/Authenticator.vue'
import AdminPrompt from './components/authentication/AdminPrompt.vue'
import AvatarCreator from './components/ingame/AvatarCreator.vue'
import Overlay from './components/ingame/Overlay.vue'
import { usePartyStore } from './stores/PartyStore'

    let image1 = new Image()
    image1.src = "assets/Portrait_Eliana.jpg"
    image1.onload = function() {
        console.log("loading Eliana's portrait...")
    }

    let image2 = new Image()
    image2.src = "assets/Portrait_Eliana.jpg"
    image2.onload = function() {
        console.log("loading Leito's portrait...")
    }

    let image3 = new Image()
    image3.src = "assets/Portrait_Eliana.jpg"
    image3.onload = function() {
        console.log("loading Marsilio's portrait...")
    }

    const authStore = useAuthStore()
    const uiStore = useUIStore()
    const partyStore = usePartyStore()
    onMounted(() => {
        if (localStorage.token != null) {
            authStore.Email = localStorage.Email
            requestTokenRefresh(authStore, partyStore)
        }
    })
</script>

<template>
    <div id="background">
        <Header/>
        <div class="content">
            <Authenticator v-show="uiStore.getShowingAuthentication"/>
            <AdminPrompt v-if="uiStore.getShowingAdminPrompt"/>
            <AvatarCreator id="avatar_creator" v-if="uiStore.getShowingAvatarCreator"/>
            <canvas canvas id="adventure_map" v-show="uiStore.getShowingWorldmap"></canvas>
            <Overlay id="overlay" v-if="uiStore.getShowingWorldmap"/>
            <Imprint v-if="uiStore.getShowingImprint"/>
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
        height: 85vh;

        position: relative;
        left: 0%;
        top: 10vh;

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

    #avatar_creator {
        position: fixed;
        z-index: 21;
        width: 100%;
        height: 100%;
        pointer-events:none;
    }

</style>
