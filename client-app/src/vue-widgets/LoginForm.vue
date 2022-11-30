<script setup>
import { useAuthUIStore } from '../stores/AuthUIStore.js'

    const AuthUI = useAuthUIStore()

    function swithToRegisForm() {
        AuthUI.hideLoginForm()
        AuthUI.showRegisForm()
    }
</script>

<template>
    <form class="login_form">
        <label>E-Mail Adresse</label>
        <input id="email_address"/> 
        <label>Passwort</label>
        <input id="password"/>
        <button v-on:click="">Einloggen</button>
        Noch kein Konto?
        <a @click="swithToRegisForm">Jetzt registrieren!</a>
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
        background-color: rgb(62, 62, 62);
        z-index: 2;
    }
    .login_form > * {
        flex-direction: column;
    }
</style>
