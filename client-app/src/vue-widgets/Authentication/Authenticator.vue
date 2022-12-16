<script setup>
import { useAuthStore } from '../../stores/AuthStore.js'

import RegistrationForm from './RegisForm.vue'
import LoginForm from './LoginForm.vue'

    const AuthStore = useAuthStore()
</script>

<template>
    <div id="content">
        <RegistrationForm v-if="AuthStore.showingRegisForm"/>  
        <LoginForm v-if="AuthStore.showingLoginForm"/>  
    </div>
</template>

<style scoped>
    #content {
        display: flex;
        width: 50vw;
        height: 100%;
        align-items: center;
        justify-content: center;
        flex-direction: column;
        background-color: rgba(0, 0, 0, 0.832);
        z-index: 19;
    }
</style>
