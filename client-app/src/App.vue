<script setup>
import { onMounted } from 'vue'
import { useAuthStore } from './stores/AuthStore.js'
import { useUIStore } from './stores/UIStore.js'
import { requestTokenRefresh } from './services/AuthService.js'

import Header from './vue-widgets/Header.vue'
import Footer from './vue-widgets/Footer.vue'
import Authenticator from './vue-widgets/Authentication/Authenticator.vue'
import Imprint from './vue-widgets/Imprint.vue'
import AvatarCreator from './vue-widgets/InGame/AvatarCreator.vue'
import Overlay from './vue-widgets/InGame/Overlay.vue'

    const AuthStore = useAuthStore()
    const UIStore = useUIStore()
    onMounted(() => {
        if (localStorage.token != null) {
            AuthStore.Email = localStorage.Email
            requestTokenRefresh(AuthStore)
        }
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
