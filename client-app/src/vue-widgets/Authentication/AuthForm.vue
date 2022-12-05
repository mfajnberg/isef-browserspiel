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
        position:absolute;
    }
</style>
