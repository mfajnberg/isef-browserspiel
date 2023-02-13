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

            if (authStore.regisResponse.status == 200) {
                authStore.regisFailed = false
                authStore.mailNotifSent = true
                authStore.showingLoginForm = false
                authStore.showingRegisForm = false
            }
            else {
                authStore.regisFailed = true
                authStore.updateValidation()
                authStore.showingLoginForm = false
                authStore.showingRegisForm = false
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
        <div class="item_container">
            <label>E-Mail Adresse</label>
            <div class="form_item">
                <input class="form_input" @input="authStore.updateValidation()" v-model="authStore.Email" type="text" id="email_address"/> 
                <span v-if="authStore.emailValid" class="validation valid_input"> 
                    ✓
                </span>
            </div>
        </div>
        <div class="item_container">
            <label> Passwort </label>
            <div class="form_item">     
                <input class="form_input" @focusin="pwdFocused = true" @focusout="pwdFocused = false" @input="authStore.updateValidation()" v-model="authStore.Password" type="password" id="password"/>
                <span v-if="authStore.pwdValid" class="validation valid_input">✓</span>
            </div>
        </div>
        
        <div class="item_container">
            <label> Passwort wiederholen </label>
            <div class="form_item" v-if="authStore.getShowingRegisForm">
                <input class="form_input" @input="authStore.updateValidation()" v-model="authStore.repeatedPassword" id="password_repeat" type="password"/>
                <span v-if="authStore.repeatValid" class="validation valid_input">
                    ✓
                </span>
            </div>
        </div>
        <button class="button_regis" ref="button_regis" @click="clickRegis()" v-if="!mailNotifSent && !regisFailed">
        Jetzt registrieren
        </button>
        <span class="center_stuff">
            Bereits registriert? <br/>
            <a @click="switchToLogin" style="cursor: pointer;">Jetzt einloggen!</a>
        </span>
    </div>
</template>

<style scoped>
    #registration_form {
        display: flex;
        align-items: stretch;
        flex-direction: column;
        padding: 5%;
        z-index: 20;
    }
    .item_container{
        display: flex;
        flex-direction: column;
        margin-top: .8rem;
    } .form_item {
        display: flex;
        align-items: center;
        position: relative;
    } .item_container {
        align-self: center;
    } .form_input {
        display: flex;
        width: 10rem;
    }.validation {
        width: 100%;
        position: absolute;
        right: -103%;
    }
    .button_regis {
        font-size: 1rem;
        display: block;
        margin-right: auto;
        margin-top: 1rem;
        margin-bottom: 1rem;
        margin-left: auto;

    }
    .center_stuff {
        margin-left: auto;
        margin-right: auto;
        text-align: center;
    }
    .valid_input {
        text-align: left;
        color:rgb(0, 255, 0);
        user-select: none;
    }
    .invalid_input {
        text-align: left;
        color: red;
        user-select: none;
    }

    @media (max-width: 800px) {
        button {
            min-width: 50vw;
        }
    }
</style>
