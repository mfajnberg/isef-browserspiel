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
    /* padding-top: 2rem;
    padding-bottom: 2rem; */
    /* background-color: rgba(0, 0, 0, 0.832); */
    /* height: 100%; */
    /* width: 100%; */

    /* position: absolute; */
    z-index: 100;
    /* border-style: none;
    border-color: rgb(252, 205, 143); */
    /* border-style: outset;
    border-width: 1px;
    border-color: rgba(133, 113, 86, 0.5) ; */
    text-shadow: 0rem 0rem 1rem black;

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
