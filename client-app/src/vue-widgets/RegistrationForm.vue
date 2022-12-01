<script setup>
import { ref } from 'vue'
import { useAuthUIStore } from '../stores/AuthUIStore.js'
import { requestRegis } from '../services/Authentication';

    const store = useAuthUIStore()

    function switchToLogin() {
        store.hideRegisForm()
        store.showLoginForm()
    }

</script>

<template>
    <form class="registration_form">
        <label>E-Mail Adresse</label>
        <input v-model="store.authData.email" type="text" id="email_address"/> 
        <label>Passwort</label>
        <input v-model="store.authData.password" id="password"/>
        <label>Passwort wiederholen</label>
        <input id="password_repeat"/>
        <button v-on:click="requestRegis(store)">Registrieren</button>
        <div>
            Bereits registriert?
            <a @click="switchToLogin" style="cursor: pointer;">Jetzt einloggen!</a>
        </div>
    </form>
</template>

<style scoped>
    .registration_form {
        display: flex;
        width: 50vw;
        height: 100%;
        align-items: center;
        justify-content: center;
        flex-direction: column;
        position: absolute;
        z-index: 2;
    }
    .registration_form > *, .login > * {
        flex-direction: column;
    }
</style>
