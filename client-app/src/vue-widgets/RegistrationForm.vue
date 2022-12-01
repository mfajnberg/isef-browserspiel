<script setup>
import { ref } from 'vue'
import { useAuthUIStore } from '../stores/AuthUIStore.js'
import { requestRegis } from '../services/Authentication';

    const AuthUI = useAuthUIStore()

    function switchToLogin() {
        AuthUI.hideRegisForm()
        AuthUI.showLoginForm()
    }

</script>

<template>
    <form class="registration_form">
        <label>E-Mail Adresse</label>
        <input v-model="AuthUI.authData.email" type="text" id="email_address"/> 
        <label>Passwort</label>
        <input v-model="AuthUI.authData.password" id="password"/>
        <label>Passwort wiederholen</label>
        <input id="password_repeat"/>
        <button v-on:click="requestRegis(AuthUI)">Registrieren</button>
        <div>
            <label>Zum Newsletter anmelden und einen kostenlosen Esel erhalten</label>
            <input v-model="AuthUI.newsletter" type="checkbox"/>
        </div>
        <div>
            Bereits registriert?
            <a @click="switchToLogin">Jetzt einloggen!</a>
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
