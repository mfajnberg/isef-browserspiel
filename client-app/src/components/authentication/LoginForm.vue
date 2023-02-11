<script setup>
import { ref, onMounted } from 'vue'
import { useUIStore } from '../../stores/UIStore'
import { useAuthStore } from '../../stores/AuthStore.js'
import { usePartyStore } from '../../stores/PartyStore';
import { useGameAssetStore } from '../../stores/GameAssetStore'
import { requestLogin } from '../../services/AuthService'
import { useWorldStore } from '../../stores/WorldStore';
import { useCreatorStore } from '../../stores/AvatarCreatorStore';

const uiStore = useUIStore()
const authStore = useAuthStore()
const partyStore = usePartyStore()
const worldStore = useWorldStore()
const assetStore = useGameAssetStore()
const creatorStore = useCreatorStore()

function switchToRegis() {
    authStore.hideLoginForm()
    authStore.showRegisForm()
}

// bind this to an Enter key event on the login widget
async function LogInEnter() {
    assetStore.pointerDownSound.play()
    await LogIn()
}

async function LogIn() {
    await requestLogin(authStore, partyStore)
    // if (true) {
    if (authStore.loginResponse.ok) {
        assetStore.pointerUpSound.play()
        authStore.loginFailed = false
        console.log("Loading scene & initial actors ...")

        await uiStore.PlayNow(authStore, partyStore, worldStore, assetStore, creatorStore)
    }
    else {
        authStore.loginFailed = true
    }
    authStore.Password = ""
}

function playSoundPointerDown() {
    assetStore.pointerDownSound.play()
}
function playSoundPointerUp() {
    assetStore.pointerUpSound.play()
}

const button_login = ref(null)
onMounted(() => {
    button_login.value.addEventListener('pointerdown', playSoundPointerDown);
    button_login.value.addEventListener('pointerup', playSoundPointerUp);
})        

</script>

<template>
    <div id="login_form">
        <div class="item">
            <label>E-Mail Adresse</label>
            <input v-model="authStore.Email" id="email_address" @keyup.enter.native="LogInEnter"/>
        </div>
        <div class="item">
            <label>Passwort</label>
            <input v-model="authStore.Password" type="password" id="password" @keyup.enter.native="LogInEnter"/>
        </div>
        <button id="btn_login" ref="button_login" v-on:click="LogIn(worldStore, assetStore)">Einloggen</button>
        <div>
            <label>Angemeldet bleiben</label>
            <input v-model="authStore.stayLoggedIn" type="checkbox" />
        </div>
        <div>
            Noch kein Konto?
            <a @click="switchToRegis" style="cursor: pointer;">Jetzt registrieren!</a>
        </div>
        <span class="invalid_input abs" v-if="authStore.loginFailed">
            Zugangsdaten nicht erkannt . . .
        </span>
    </div>
</template>

<style scoped>
#login_form {
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: column;
    z-index: 20;
} .item {
    display: flex;
    flex-direction: column;
    margin:.5rem;
}
#btn_login {
    margin-top: 1rem;
    margin-bottom: 1rem;
}
.invalid_input {
        text-align: left;
        color: red;
        font-size: x-small;
        font-size: 1vh;
        user-select: none;
    }.abs {
        /* display: none; */
        padding-top: 3px;
        margin-bottom: -27px;
    }
</style>
