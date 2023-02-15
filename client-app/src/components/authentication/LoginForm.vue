<script setup>
import { ref, onMounted, onBeforeUnmount } from 'vue'
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
const gameAssetStore = useGameAssetStore()
const creatorStore = useCreatorStore()

function switchToRegis() {
    authStore.hideLoginForm()
    authStore.showRegisForm()
}

// bind this to an Enter key event on the login widget
async function LogInEnter() {
    gameAssetStore.pointerDownSound.play()
    await LogIn()
}

async function LogIn() {
    await requestLogin(authStore, partyStore)
    // if (true) {
    if (authStore.loginResponse.ok) {
        gameAssetStore.pointerUpSound.play()
        authStore.loginFailed = false
        console.log("Loading scene & initial actors ...")

        await uiStore.PlayNow(authStore, partyStore, worldStore, gameAssetStore, creatorStore)
    }
    else {
        authStore.loginFailed = true
    }
}

function playSoundPointerDown() {
    gameAssetStore.pointerDownSound.play()
}
function playSoundPointerUp() {
    gameAssetStore.pointerUpSound.play()
}

const button_login = ref(null)
onMounted(() => {
    button_login.value.addEventListener('pointerdown', playSoundPointerDown);
})        


onBeforeUnmount( () => {
    authStore.Password = ""
    authStore.loginFailed = false
})

</script>

<template>
    <div id="login_form" v-if="!uiStore.loadingAssets">
        <h3 class="strong">Login</h3>
        <div class="item">
            <label class="item_label">E-Mail Adresse</label>
            <input class="form_input" v-model="authStore.Email" id="email_address" @keyup.enter.native="LogInEnter"/>
        </div>
        <div class="item">
            <label class="item_label">Passwort</label>
            <input class="form_input" v-model="authStore.Password" type="password" id="password" @keyup.enter.native="LogInEnter"/>
        </div>
        <div class="stay_logged_in">
            <label class="item_label">Angemeldet bleiben?</label>
            <input class="checkbox" v-model="authStore.stayLoggedIn" type="checkbox" />
        </div>
        <button class="button_login" ref="button_login" v-on:click="LogIn(worldStore, gameAssetStore)">Einloggen</button>
        <div class="register_prompt">
            Noch kein Konto?
            <a @click="switchToRegis" style="cursor: pointer;">Jetzt registrieren!</a>
        </div>
    </div>
    <div v-if="uiStore.loadingAssets" class="loading">
        <h3>Inhalte werden geladen...</h3>
    </div>
</template>

<style scoped>
#login_form {
    display: grid;
    align-items: stretch;
    justify-content: center;
    flex-direction: column;
    /* padding: 5%; */
    z-index: 20;
} h3 {
    font-family: 'fondamento';
    text-align: center;
    margin-bottom: 1rem;
}
.item {
    display: flex;
    margin-bottom: .8rem;
    flex-direction: column;
    align-self: center;
    justify-self: center;
    padding-left: 5%;
    padding-right: 5%;
    width: 10rem;
} .item_label {
} 
.form_input {

    /* width: 10rem; */
}
.button_login {
    display: flex;
    justify-content: center;
    justify-self: center;
    margin-top: .8rem;
    margin-bottom: .8rem;
    margin-left: auto;
    margin-right: auto;
    font-size: 1rem;
}
.stay_logged_in {
    display: flex;
    align-self: center;
    /* margin-top: 2%; */
    margin-right: auto;
    margin-left: auto;
} .checkbox {
    margin-left: .7rem;
    width: 1rem;
    height: 1rem;
    /* color: black; */
}
.register_prompt {
    margin-left: auto;
    margin-right: auto;
}
.invalid_input {
        text-align: left;
        color: red;
        /* font-size: .8rem; */
        line-height: 150%;
        user-select: none;
    }.abs {
        /* display: none; */
        padding-top: 3px;
        margin-bottom: -27px;
    }
.loading {
    text-align: center;
    /* font-family: 'Marcellus'; */
}
</style>
