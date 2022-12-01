<script setup>
import { ref } from 'vue'
import { useAuthUIStore } from '../stores/AuthUIStore.js'
import { requestLogin } from '../services/Authentication';

    const AuthUI = useAuthUIStore()

    function switchToRegis() {
        AuthUI.hideLoginForm()
        AuthUI.showRegisForm()
    }

    function LogIn() {
        requestLogin(AuthUI)
        if (AuthUI.stayLoggedIn) {
            localStorage.setItem('token', AuthUI.token)
        }
    }
</script>

<template>
    <form class="login_form">
        <label>E-Mail Adresse</label>
        <input v-model="AuthUI.credentials.email" id="email_address"/> 
        <label>Passwort</label>
        <input v-model="AuthUI.credentials.password" id="password"/>
        <button v-on:click="LogIn()">Einloggen</button>
        <div>
            <label>Angemeldet bleiben</label>
            <input v-model="AuthUI.stayLoggedIn" type="checkbox"/>
        </div>
        <div>
            Noch kein Konto?
            <a @click="switchToRegis">Jetzt registrieren!</a>
        </div>
    </form>
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
