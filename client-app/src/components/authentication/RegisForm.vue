<script setup>
import { ref, onMounted } from 'vue'
import { useAuthStore } from '../../stores/AuthStore.js'
import { useGameAssetStore } from '../../stores/GameAssetStore';
import { requestRegis } from '../../services/AuthService';

    const authStore = useAuthStore()
    const assetStore = useGameAssetStore()
    var pwdFocused = ref(false)
    var mailNotifSent = ref(false)
    var regisFailed = ref(false)

    function switchToLogin() {
        authStore.hideRegisForm()
        authStore.showLoginForm()
        authStore.updateValidation()
    }
    
    async function clickRegis() {
        if (authStore.emailValid && authStore.pwdValid && authStore.repeatValid) {
            await requestRegis(authStore)

            if (authStore.regisResponse.status == 200) 
                mailNotifSent.value = true
            else {
                regisFailed.value = true
                authStore.updateValidation()
            }
        }
        else {
            console.log("invalid form input")
        }
    }

function playSoundPointerDown() {
    assetStore.pointerDownSound.play()
}
function playSoundPointerUp() {
    assetStore.pointerUpSound.play()
}

const button_regis = ref(null)
onMounted(() => {
    button_regis.value.addEventListener('pointerdown', playSoundPointerDown);
    button_regis.value.addEventListener('pointerup', playSoundPointerUp);
})    
</script>

<template>
    <div id="registration_form" v-if="!mailNotifSent && !regisFailed">
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
        
        <div class="item repeat" v-if="authStore.getShowingRegisForm">
            <label> Passwort wiederholen </label>
            <input @input="authStore.updateValidation()" v-model="authStore.repeatedPassword" id="password_repeat" type="password"/>
            <span class="valid_input">⠀</span>
        </div>
    </div>
    <div id="mail_notif_sent" v-if="mailNotifSent">
        Aktiviere dein Konto über den Validierungs-Link in deinem E-Mail Postfach,
        <br/>
        um die Registrierung abzuschließen
        <br/><br/>
        (Es kann teilweise ein paar Minuten dauern, bis die E-Mail ankommt.)
    </div>
    <div id="regis_failed" v-if="regisFailed">
        Registrierung fehlgeschlagen. 
        <br/>
        Womöglich existiert bereits ein Konto mit dieser E-Mail Adresse.
        <br/><br/>
        Probier es gerne zu einem späteren Zeitpunkt erneut
        <br/>
        oder kontaktiere unseren Kundensupport.
    </div>
    <button ref="button_regis" @click="clickRegis()" v-if="!mailNotifSent && !regisFailed">
        Jetzt registrieren
    </button>
    <div>
        <br/>
        Bereits registriert?
        <a @click="switchToLogin" style="cursor: pointer;">Jetzt einloggen!</a>
    </div>
</template>

<style scoped>
    #registration_form {
        /* display: flex; */
        align-items: center;
        flex-direction: column;
        z-index: 20;
        width: 300px;
    }
    .item{
        text-align: left;
        display: flex;
        /* align-items: baseline; */
        flex-direction: column;
        padding-left: 15px;
        padding-right: 15px;
        position: relative;
    }
    .invalid_input {
        text-align: left;
        color: red;
        font-size: x-small;
        font-size: 1vh;
        user-select: none;
    }
    .valid_input {
        text-align: left;
        color:rgb(0, 255, 0);
        font-size: x-small;
        font-size: 1vh;
        user-select: none;
    }
</style>
