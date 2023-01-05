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
        align-items: center;
        justify-content: center;
        flex-direction: column;
        padding: 3em;
        background-color: rgba(0, 0, 0, 0.832);
        z-index: 19;
        border-radius: 6px;
    }
    @media (max-width: 700px) {
        #content {
            width: 100%;
        }
    }
</style>
