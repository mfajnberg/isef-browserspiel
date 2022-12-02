<script setup>
import { ref } from 'vue'
import { useAuthUIStore } from '../stores/AuthUIStore.js'
import { requestRegis } from '../services/Authentication';

    const store = useAuthUIStore()

    function switchToLogin() {
        store.hideRegisForm()
        store.showLoginForm()
    }
    
    // between 6 to 20 chars, at least one numeric, uppercase & lowercase
    var validPwdPattern=  /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/;
    var validEmailPattern= /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;

    function updateValidation() {
        if (store.authData.email.match(validEmailPattern)) {
            store.emailValid = true
        }
        else {
            store.emailValid = false
        }
        if (store.authData.password.match(validPwdPattern)) {
            store.pwdValid = true
        }
        else { // not valid
            store.pwdValid = false
        }
        if (store.authData.password == store.repeatedPassword) {
            store.pwdRepeat = true
        }
        else { // no repeat
            store.pwdRepeat = false
        }
    }

    function tryRequestRegis() {
        if (store.emailValid && store.pwdValid && store.pwdRepeat) {
            requestRegis(store)
        }
        else {
            console.log("invalid form input")
        }
    }

</script>

<template>
    <div class="registration_form">
        <label>E-Mail Adresse</label>
        <label v-if="!store.emailValid" class="invalid_input">
            keine valide E-Mail Adresse
        </label>
        <input @input="updateValidation()" v-model="store.authData.email" type="text" id="email_address"/> 
        <label>
            Passwort
        </label>
        <label v-if="!store.pwdValid" class="invalid_input"> 
            6-20 Zeichen, mind. 1x Zahl, Groß- & Kleinbuchstabe
        </label>
        <input @input="updateValidation()" v-model="store.authData.password" type="password" id="password"/>
        <label>
            Passwort wiederholen
        </label>
        <label v-if="!store.pwdRepeat" class="invalid_input">
            !!!
        </label>
        <input @input="updateValidation()" v-model="store.repeatedPassword" id="password_repeat" type="password"/>
        <button v-on:click="tryRequestRegis()">
            jetzt registrieren
        </button>
        <div>
            Bereits registriert?
            <a @click="switchToLogin" style="cursor: pointer;">Jetzt einloggen!</a>
        </div>
    </div>
</template>

<style scoped>
    .registration_form {
        display: flex;
        width: 50vw;
        height: 100%;
        align-items: center;
        justify-content: center;
        flex-direction: column;
        position: absolute;
        z-index: 2;
    }
    .registration_form > *, .login > * {
        flex-direction: column;
    }
    .invalid_input {
        color: red;
    }
</style>
