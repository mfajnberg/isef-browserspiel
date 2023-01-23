<script setup>
import { ref, onMounted } from 'vue'
import { useAuthStore } from '../../stores/AuthStore.js'
import { useUIStore } from '../../stores/UIStore'
import { useGameAssetStore } from '../../stores/GameAssetStore'
import { requestLogin } from '../../services/AuthService'
import { Ambience } from '../../services/Ambience.js'

const authStore = useAuthStore()
const uiStore = useUIStore()
const assetStore = useGameAssetStore()
const ambience = new Ambience()

function switchToRegis() {
    authStore.hideLoginForm()
    authStore.showRegisForm()
}

async function LogIn() {
    requestLogin(authStore)
    uiStore.showWorldmap()
    ambience.music.play()
}


function playSoundPointerDown() {
    assetStore.pointerDownSound.play()
}
function playSoundPointerUp() {
    assetStore.pointerUpSound.play()
}

const button_login = ref(null)
const login_form = ref(null)
onMounted(() => {
    button_login.value.addEventListener('pointerdown', playSoundPointerDown);
    button_login.value.addEventListener('pointerup', playSoundPointerUp);
})        

</script>

<template>
    <div id="login_form" ref="login_form">
        <label>E-Mail Adresse</label>
        <input v-model="authStore.Email" id="email_address" @keyup.enter.native="LogIn"/>
        <label>Passwort</label>
        <input v-model="authStore.Password" type="password" id="password" @keyup.enter.native="LogIn"/>
        <button id="btn_login" ref="button_login" v-on:click="LogIn()">Einloggen</button>
        <div>
            <label>Angemeldet bleiben</label>
            <input v-model="authStore.stayLoggedIn" type="checkbox" />
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
