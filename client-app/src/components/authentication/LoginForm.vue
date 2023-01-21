<script setup>
import { ref, onMounted } from 'vue';
import { useAuthStore } from '../../stores/AuthStore.js'
import { requestLogin } from '../../services/AuthService';
import { useUIStore } from '../../stores/UIStore';
import { Ambience } from '../../services/howlee.js'

const UIStore = useUIStore()
const store = useAuthStore()
const ambience = new Ambience()

function switchToRegis() {
    store.hideLoginForm()
    store.showRegisForm()
}

async function LogIn() {
    requestLogin(store)
    UIStore.showWorldmap()
    ambience.music.play()
}


function playSoundPointerDown() {
    UIStore.pointerDownSound.play()
}
function playSoundPointerUp() {
    UIStore.pointerUpSound.play()
}

const button_login = ref(null);
onMounted(() => {
    button_login.value.addEventListener('pointerdown', playSoundPointerDown);
    button_login.value.addEventListener('pointerup', playSoundPointerUp);
})        

</script>

<template>
    <div id="login_form">
        <label>E-Mail Adresse</label>
        <input v-model="store.Email" id="email_address" />
        <label>Passwort</label>
        <input v-model="store.Password" type="password" id="password" />
        <button ref="button_login" v-on:click="LogIn()">Einloggen</button>
        <div>
            <label>Angemeldet bleiben</label>
            <input v-model="store.stayLoggedIn" type="checkbox" />
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
