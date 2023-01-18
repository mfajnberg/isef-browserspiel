<script setup>
import { useAuthStore } from '../../stores/AuthStore.js'
import { requestLogin } from '../../services/AuthService';
import { useUIStore } from '../../stores/UIStore';

    const UIStore = useUIStore()
    const store = useAuthStore()

    function switchToRegis() {
        store.hideLoginForm()
        store.showRegisForm()
    }

    async function LogIn() {
        if (store.Email == "dummy") {
            store.loggedIn = true
            store.dummyLogin = true
            UIStore.showWorldmap()
            return
        }
        await requestLogin(store)
        UIStore.showWorldmap()
        

    }
</script>

<template>
    <div id="login_form">
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
    #login_form {
        display: flex;
        height: 100%;
        align-items: center;
        justify-content: center;
        flex-direction: column;
        z-index: 20;
    }
</style>
