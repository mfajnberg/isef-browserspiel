<script setup>
import { ref, onMounted, onBeforeUnmount } from 'vue'
import { useAuthStore } from '../../stores/AuthStore.js'
import { useGameAssetStore } from '../../stores/GameAssetStore';
import { requestRegis } from '../../services/AuthService';

    const authStore = useAuthStore()
    const assetStore = useGameAssetStore()
    let invalidFormSubmit = ref(false)

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
            invalidFormSubmit.value = true
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
    })

onBeforeUnmount(() => {
    authStore.Password = ""
    authStore.repeatedPassword = ""
})
</script>

<template>
    <div id="registration_form">
        <div class="item">
            <label>E-Mail Adresse</label>
            <div class="input_container">
                <input class="form_input" @input="authStore.updateValidation()" v-model="authStore.Email" type="text"/> 
                <span v-if="authStore.emailValid" class="validation valid_input"> 
                    ✓
                </span>
                <span v-if="invalidFormSubmit && !authStore.emailValid" class="validation invalid_input">
                    !
                </span>
            </div>
        </div>
        <div class="item">
            <label> Passwort </label>
            <div class="input_container">     
                <input class="form_input" @input="authStore.updateValidation()" v-model="authStore.Password" type="password"/>
                <span v-if="authStore.pwdValid" class="validation valid_input">
                    ✓
                </span>
                <span v-if="invalidFormSubmit && !authStore.pwdValid" class="validation invalid_input">
                    !
                </span>
            </div>
        </div>
        
        <div class="item">
            <label> Passwort wiederholen </label>
            <div class="input_container">
                <input class="form_input" @input="authStore.updateValidation()" v-model="authStore.repeatedPassword" id="password_repeat" type="password"/>
                <span v-if="authStore.repeatValid" class="validation valid_input">
                    ✓
                </span>
                <span v-if="invalidFormSubmit && !authStore.repeatValid" class="validation invalid_input">
                    !
                </span>
            </div>
        </div>
        <button ref="button_regis" class="button_regis" 
                @click="clickRegis()">
            Jetzt registrieren
        </button>

    </div>
</template>

<style scoped>
    #registration_form {
        display: flex;
        flex-direction: column;
        justify-content: flex-start;
        z-index: 20;
    } 
    label {
        user-select: none;
    }
    .item{
        display: flex;
        flex-direction: column;
        justify-self: center;
        margin-bottom: .8rem;
    } .input_container {
        display: flex;
        align-items: center;
        position: relative;
        width: 10rem;
    } .form_input {
        display: flex;
        width: 9.6rem;
    }.validation {
        width: 100%;
        position: absolute;
        right: -103%;
    }
    .button_regis {
        margin-top: .2rem;
        font-size: 1rem;

    } .button_regis:active + * {
        font-size: 2rem;
    }
    .login_prompt {
        margin-left: auto;
        margin-right: auto;
        margin-top: auto;
        margin-bottom: 10%;
        text-align: center;
    }
    a {
        font-size: 1rem;
    }
    .valid_input {
        left: 10.3rem;
        color:rgb(0, 255, 0);
        user-select: none;
    }
    .invalid_input {
        left: 10.5rem;
        color: rgb(253, 0, 76);
        user-select: none;
    }

    @media (max-width: 800px) {
        button {
            min-width: 50vw;
        }
    }
</style>
