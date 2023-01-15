<script setup>
import { onMounted } from 'vue'
import { useAuthStore } from './stores/AuthStore.js'
import { useUIStore } from './stores/UIStore.js'
import { requestTokenRefresh } from './services/AuthService.js'

import Header from './vue-widgets/Header.vue'
import Footer from './vue-widgets/Footer.vue'
import Authenticator from './vue-widgets/Authentication/Authenticator.vue'
import AvatarCreator from './vue-widgets/AvatarCreation/AvatarCreator.vue'
import Imprint from './vue-widgets/Imprint.vue'
import Overlay from './vue-widgets/Worldmap/Overlay.vue'

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
    <div id="bg">
        <Header/>
        <div class="content">
            <!-- <img src="/border1.png" class="border" id="b4"> -->
            <Authenticator v-show="UIStore.showingAuthentication"/>
            <AvatarCreator v-if="UIStore.showingAvatarCreator"/>
            <canvas canvas id="adventure-map" class="worldmap" v-show="UIStore.showingWorldmap"></canvas>
            <Imprint v-if="UIStore.showingImprint"/>
            <Overlay v-if="UIStore.showingWorldmap" id="overlay"/>
        </div>
        <Footer/>
    </div>
</template>

<style scoped>
    .content {
        display: flex;
        width: 100vw;
        height: 75vh;

        position: absolute;
        left: 0%;
        top: 20vh;

        align-items: center;
        justify-content: center;
    }
    
.worldmap {
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: row;
  position: relative;
  background-color: rgba(0, 0, 0, 0.851);
  z-index: 10;
}

#overlay {
    position: absolute;
    height: 100%;
    width: 100%;
    z-index: 11;
    pointer-events:none;
}

.border {
        position:fixed;
        top: 18vh;
        z-index: 21;
        user-select: none;
        -webkit-touch-callout: none
    }

#bg {
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

</style>
