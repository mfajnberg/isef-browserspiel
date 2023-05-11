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

// bind this to an Enter key event on the login widget
async function LogInEnter() {
    gameAssetStore.pointerDownSound.play()
    await LogIn()
}

async function LogIn() {
    await requestLogin(authStore, partyStore)
    if (authStore.loginResponse.ok) {
        authStore.loginFailed = false
        await uiStore.PlayNow(authStore, partyStore, worldStore, gameAssetStore, creatorStore)
        gameAssetStore.pointerUpSound.play()
    }
    else {
        authStore.loginFailed = true
    }
}

function playSoundPointerDown() {
    gameAssetStore.pointerDownSound.play()
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
        <div class="item">
            <label class="item_label">E-Mail Adresse</label>
            <input class="form_input" v-model="authStore.Email" type="text" autofill="email" @keyup.enter.native="LogInEnter"/>
        </div>
        <div class="item">
            <label class="item_label">Passwort</label>
            <input class="form_input" v-model="authStore.Password" type="password" autofill="password" @keyup.enter.native="LogInEnter"/>
        </div>
        <div class="stay_logged_in">
            <label class="item_label">Angemeldet bleiben?</label>
            <input class="checkbox" v-model="authStore.stayLoggedIn" type="checkbox" />
        </div>
        <button ref="button_login" class="button_login"
                v-on:click="LogIn(worldStore, gameAssetStore)">
            Einloggen
        </button>
    </div>
</template>

<style scoped>
#login_form {
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    z-index: 20;
} 
.item {
    display: flex;
    flex-direction: column;
    align-self: center;
    justify-self: center;
    margin-bottom: .8rem;
    width: 10rem;
} .item_label {
    user-select: none;
} 
.form_input {

    /* width: 10rem; */
}
.button_login {
    margin-top: .2rem;
    font-size: 1rem;
}
.stay_logged_in {
    display: flex;
    align-self: center;
    align-items: center;
    height: 2.725rem;
    margin-right: auto;
    margin-left: auto;
    margin-bottom: .8rem;
} .checkbox {
    margin-left: .7rem;
    width: 1rem;
    height: 1rem;
    /* color: black; */
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
</style>
