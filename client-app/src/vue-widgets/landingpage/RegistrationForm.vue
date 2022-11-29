<script setup>
import { useStore } from '../../stores/ReactiveStore.js'

    const store = useStore()
</script>

<template>
    <form class="registration_form" v-if="store.reg_bool">
        <label>E-Mail Adresse</label>
        <input id="email_address"/> 
        <label>Passwort</label>
        <input id="password"/>
        <label>Passwort wiederholen</label>
        <input id="password_repeat"/>
        <button v-on:click="store.toggleRegForm()">Registrieren</button>
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
        z-index: 1;
    }
    .registration_form > *, .login > * {
        flex-direction: column;
    }
</style>
