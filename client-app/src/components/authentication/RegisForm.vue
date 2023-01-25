<script setup>
import { ref, reactive } from 'vue'
import { useAuthStore } from '../../stores/AuthStore.js'
import { useUIStore } from '../../stores/UIStore.js'
import { useGameAssetStore } from '../../stores/GameAssetStore';
import { requestRegis } from '../../services/AuthService';
import { Ambience } from '../../services/Ambience.js'

    const authStore = useAuthStore()
    const uiStore = useUIStore()
    const assetStore = useGameAssetStore()
    const ambience = new Ambience()
    var pwdFocused = ref(false)
    var mailNotifSent = ref(false)

    function switchToLogin() {
        authStore.hideRegisForm()
        authStore.showLoginForm()
    }
    
    // between 6 to 20 chars, at least one numeric, uppercase & lowercase

    function updateValid() {
        authStore.updateValidation()
    }

    async function clickRegis() {
        if (authStore.emailValid && authStore.pwdValid && authStore.repeatValid) {
            await requestRegis(authStore)
            if (authStore.authResponse.status == 200) {
                mailNotifSent.value = true
                ambience.music.play()
                ambience.music.mute(false)
            }
            else {
                console.log(authStore.authResponse)
            }
        }
        else {
            console.log("invalid form input")
        }
        assetStore.pointerDownSound.play()
    }

</script>

<template>
    <div id="registration_form" v-if="!mailNotifSent">
        <div class="item email">
            <label>E-Mail Adresse</label>
            <input @input="authStore.updateValidation()" v-model="authStore.Email" type="text" id="email_address"/> 
            <span v-if="authStore.emailValid" class="valid_input"> ✔ </span>
            <span v-if="!authStore.emailValid" class="invalid_input">⠀</span>
        </div>
        
        <div class="item password">     
            <label> Passwort </label>
            <input @focusin="pwdFocused = true" @focusout="pwdFocused = false" @input="authStore.updateValidation()" v-model="authStore.Password" type="password" id="password"/>
            <span v-if="authStore.pwdValid" class="valid_input"> ✔ </span>
            <span v-if="!authStore.pwdValid && !pwdFocused" class="valid_input"> ⠀ </span>
            <span v-if="!authStore.pwdValid && pwdFocused" class="invalid_input"> 6-20 Zeichen, mind. 1x Zahl, Groß- & Kleinbuchstabe </span>
        </div>
        
        <div class="item repeat" v-if="authStore.showingRegisForm">
            <label> Passwort wiederholen </label>
            <input @input="authStore.updateValidation()" v-model="authStore.repeatedPassword" id="password_repeat" type="password"/>
            <span class="valid_input">⠀</span>
        </div>
    </div>
    <div id="mail_notif_sent" v-if="mailNotifSent">
        Aktiviere dein Konto über den Validierungs-Link in deinem E-Mail Postfach.
        <br/>
        (Es kann teilweise ein paar Minuten dauern, bis die E-Mail ankommt.)
    </div>
    <button @click="clickRegis()" v-if="!mailNotifSent">
        jetzt registrieren
    </button>
    <div v-if="!mailNotifSent">
        Bereits registriert?
        <a @click="switchToLogin" style="cursor: pointer;">Jetzt einloggen!</a>
    </div>
</template>

<style scoped>
    #registration_form {
        display: flex;
        align-items:baseline;
        flex-direction: column;
        z-index: 20;
        width: 300px;
    }
    .item{
        text-align: left;
        display: flex;
        align-items: baseline;
        flex-direction: column;
        padding-left: 15px;
        padding-right: 15px;
        position: relative;
    }
    .invalid_input {
        text-align: left;
        color: red;
        font-size: x-small;
        user-select: none;
    }
    .valid_input {
        text-align: left;
        color:rgb(0, 255, 0);
        font-size: x-small;
        user-select: none;
    }
</style>
