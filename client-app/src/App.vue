<script setup>
import { onMounted, ref } from 'vue'
import { useAuthStore } from './stores/AuthStore.js'
import { useUIStore } from './stores/UIStore.js'
import { requestTokenRefresh, LogOut } from './services/AuthService.js'

import Header from './vue-widgets/Header.vue'
import Footer from './vue-widgets/Footer.vue'
import AuthForm from './vue-widgets/Authentication/AuthForm.vue'
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
        <Header class="header"/>
        <div class="content">
            <AuthForm class="auth_form" v-if="UIStore.showingAuthentication"/>
            <AvatarCreator class="avatar_creator" v-if="UIStore.showingAvatarCreator"/>
        </div>
        <Footer/>
    </div>
</template>

<style scoped>
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
</style>
