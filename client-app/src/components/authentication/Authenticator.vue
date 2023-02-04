<script setup>
import { useAuthStore } from '../../stores/AuthStore.js'
import RegistrationForm from './RegisForm.vue'
import LoginForm from './LoginForm.vue'

const authStore = useAuthStore()
</script>

<template>
    <div id="authenticator" ref="draggableElement">
        <RegistrationForm ref="regis" v-if="authStore.showingRegisForm"/>  
        <LoginForm ref="login" v-if="authStore.showingLoginForm"/>  
    </div>
</template>

<style scoped>
#authenticator {
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: column;
    padding: 3em;
    background-color: rgba(0, 0, 0, 0.832);

    position: absolute;
    z-index: 100;
    border-style: double;
    border-color: rgb(133, 113, 86);
    border-width: 3px;

    user-select: none;

}

@media (max-width: 700px) {
    #content {
        display: flex;
        align-items: center;
        justify-content: center;
        flex-direction: column;
        padding: 3em;
        z-index: 19;

    }
    @media (max-width: 700px) {
        #content {
            width: 100%;
        }
    }
}
</style>
