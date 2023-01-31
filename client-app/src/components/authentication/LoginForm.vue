<script setup>
import { ref, onMounted } from 'vue'
import { useUIStore } from '../../stores/UIStore'
import { useAuthStore } from '../../stores/AuthStore.js'
import { usePartyStore } from '../../stores/PartyStore';
import { useCreatorStore } from '../../stores/AvatarCreatorStore.js';
import { useGameAssetStore } from '../../stores/GameAssetStore'
import { requestLogin } from '../../services/AuthService'
import { Ambience } from '../../services/AmbienceService.js'
import { fetchGetChoices } from '../../services/AvatarCreatorService';

const uiStore = useUIStore()
const authStore = useAuthStore()
const partyStore = usePartyStore()
const creatorStore = useCreatorStore()
const assetStore = useGameAssetStore()
const ambience = new Ambience()

function switchToRegis() {
    authStore.hideLoginForm()
    authStore.showRegisForm()
}

async function LogIn() {
    await requestLogin(authStore, partyStore, creatorStore)

    // DEBUG
    authStore.responseStatus = 200
    authStore.userIsAdmin = false
    partyStore.avatar = null

    if (authStore.responseStatus === 200) {
        if (partyStore.avatar === null && !authStore.userIsAdmin) {
            console.log("login successful, no avatar and no admin status...")
            await creatorStore.fetchAvatarCreationChoices()
            uiStore.showAvatarCreator()
        }

        else if (authStore.userIsAdmin == true) {
            console.log("login successful, admin status detected...")
            uiStore.showAdminPrompt()
        }
        
        else  {
            console.log("login successful, loading worldmap...")
            uiStore.showWorldmap()
            ambience.music.play()
        }
    }
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
        <label>E-Mail Adresse</label>
        <input v-model="authStore.Email" id="email_address" @keyup.enter.native="LogIn"/>
        <label>Passwort</label>
        <input v-model="authStore.Password" type="password" id="password" @keyup.enter.native="LogIn"/>
        <span v-if="!authStore.responseStatus === 404" class="invalid_input"> 
            Die eingegebenen Daten stimmen mit keinem Nutzerkonto überein...
        </span>
        <span v-if="!authStore.responseStatus === 422" class="invalid_input"> 
            Bitte aktiviere deinen Account über den E-Mail Bestätigungslink.
        </span>
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

.invalid_input {
        text-align: left;
        color: red;
        font-size: x-small;
        user-select: none;
    }
</style>
