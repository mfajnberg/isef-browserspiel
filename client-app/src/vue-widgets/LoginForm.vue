<script setup>
import { ref } from 'vue'
import { useAuthUIStore } from '../stores/AuthUIStore.js'
import { requestLogin } from '../services/Authentication';

    const store = useAuthUIStore()

    function switchToRegis() {
        store.hideLoginForm()
        store.showRegisForm()
    }

    async function LogIn() {
        await requestLogin(store)
    }
</script>

<template>
    <div class="login_form">
        <label>E-Mail Adresse</label>
        <input v-model="store.Email" id="email_address"/> 
        <label>Passwort</label>
        <input v-model="store.Password" type="password" id="password"/>
        <button v-on:click="LogIn()">Einloggen</button>
        <div>
            <label>Angemeldet bleiben</label>
            <input v-model="store.stayLoggedIn" type="checkbox"/>
        </div>
        <div>
            Noch kein Konto?
            <a @click="switchToRegis" style="cursor: pointer;">Jetzt registrieren!</a>
        </div>
    </div>
</template>

<style scoped>
    .login_form {
        display: flex;
        width: 50vw;
        height: 100%;
        align-items: center;
        justify-content: center;
        flex-direction: column;
        position: absolute;
        z-index: 2;
    }
    .login_form > * {
        flex-direction: column;
    }
</style>
