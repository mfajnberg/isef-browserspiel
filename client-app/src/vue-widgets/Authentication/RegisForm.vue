<script setup>
import { ref } from 'vue'
import { useAuthStore } from '../../stores/AuthStore.js'
import { requestRegis } from '../../services/AuthService';

    const store = useAuthStore()

    function switchToLogin() {
        store.hideRegisForm()
        store.showLoginForm()
    }
    
    // between 6 to 20 chars, at least one numeric, uppercase & lowercase
    var validPwdPattern=  /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/;
    var validEmailPattern= /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;

    function updateValidation() {
        if (store.Email.match(validEmailPattern)) {
            store.emailValid = true
        }
        else {
            store.emailValid = false
        }
        if (store.Password.match(validPwdPattern)) {
            store.pwdValid = true
        }
        else { // not valid
            store.pwdValid = false
        }
        if (store.Password == store.repeatedPassword) {
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
    <div id="registration_form">
        <label>E-Mail Adresse</label>
        <label v-if="!store.emailValid" class="invalid_input">
            keine valide E-Mail Adresse
        </label>
        <input @input="updateValidation()" v-model="store.Email" type="text" id="email_address"/> 
        <label>
            Passwort
        </label>
        <label v-if="!store.pwdValid" class="invalid_input"> 
            6-20 Zeichen, mind. 1x Zahl, Groß- & Kleinbuchstabe
        </label>
        <input @input="updateValidation()" v-model="store.Password" type="password" id="password"/>
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
    #registration_form {
        display: flex;
        height: 100%;
        align-items: center;
        justify-content: center;
        flex-direction: column;
        z-index: 20;
    }
    .invalid_input {
        color: red;
    }
</style>
