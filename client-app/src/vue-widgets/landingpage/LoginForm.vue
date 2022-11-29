<script setup>
import { useStore } from '../../stores/ReactiveStore.js'

    const store = useStore()
</script>

<template>
    <form class="login_form">
        <label>E-Mail Adresse</label>
        <input id="email_address"/> 
        <label>Passwort</label>
        <input id="password"/>
        <button v-on:click="store.toggleRegForm()">Login</button>
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
        z-index: 0;
    }
    .login_form > * {
        flex-direction: column;
    }
</style>
