<script setup>
import { useAuthUIStore } from '../stores/AuthUIStore.js'

    const AuthUI = useAuthUIStore()
</script>

<template>
    <form class="registration_form">
        <label>E-Mail Adresse</label>
        <input id="email_address"/> 
        <label>Passwort</label>
        <input id="password"/>
        <label>Passwort wiederholen</label>
        <input id="password_repeat"/>
        <button v-on:click="">Registrieren</button>
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
        background-color: rgb(62, 62, 62);
        z-index: 2;
    }
    .registration_form > *, .login > * {
        flex-direction: column;
    }
</style>
