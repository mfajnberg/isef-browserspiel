<script setup>
import { onMounted } from 'vue'
import { useAuthStore } from './stores/AuthStore.js'
import { useUIStore } from './stores/UIStore.js'
import { requestTokenRefresh } from './services/AuthService.js'

import Header from './vue-widgets/Header.vue'
import Footer from './vue-widgets/Footer.vue'
import Authenticator from './vue-widgets/Authentication/Authenticator.vue'
import AvatarCreator from './vue-widgets/AvatarCreation/AvatarCreator.vue'

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
    <div>
        <Header/>
        <div class="content">
            <Authenticator v-if="UIStore.showingAuthentication"/>
            <AvatarCreator v-if="UIStore.showingAvatarCreator"/>
            <canvas canvas id="adventure-map" class="worldmap" v-show="UIStore.showingWorldmap"></canvas>
        </div>
        <Footer/>
    </div>
</template>

<style scoped>
    .content {
        display: flex;
        width: 100vw;
        height: 75vh;
        background-image: url("twilight_aegis.jpg");
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
    
.worldmap {
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: row;
  position: absolute;
  background-color: rgba(0, 0, 0, 0.851);
  z-index: 1;
}
</style>
