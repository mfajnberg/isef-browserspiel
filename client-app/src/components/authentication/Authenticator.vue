<script setup>
import { useAuthStore } from '../../stores/AuthStore.js'
import RegistrationForm from './RegisForm.vue'
import LoginForm from './LoginForm.vue'

const authStore = useAuthStore()

function checkWebGL1() {
    try {
    const canvas = document.createElement( 'canvas' );
    return !! ( window.WebGLRenderingContext && ( canvas.getContext( 'webgl' ) || canvas.getContext( 'experimental-webgl' ) ) );
    } catch ( e ) {
    return false;
    }
}
function checkWebGL2() {
    try {
    const canvas = document.createElement( 'canvas' );
    return !! ( window.WebGLRenderingContext && ( canvas.getContext( 'webgl' ) || canvas.getContext( 'experimental-webgl' ) ) );
    } catch ( e ) {
    return false;
    }
}
</script>

<template>
    <div class="authenticator" ref="draggableElement">
        <div class="disclaimer" v-show="!authStore.mailNotifSent && !authStore.regisFailed">
            <span class="strong">Achtung:</span> <br/>
            Diese Webanwendung verwendet <span class="strong">WebGL</span> <br/> 
            Einige Browser (z.B. LibreWolf) deaktivieren diese Funktion automatisch.
            <br/>
            <br/>
            Falls Ihnen die Spielwelt daher nicht richtig angezeigt wird, können Sie WebGL wie folgt aktivieren:
            <br/>
            <span class="strong">Settings > LibreWolf > Enable WebGL</span>
        </div>

        <div class="form" v-show="!authStore.mailNotifSent && !authStore.regisFailed">
            <RegistrationForm ref="regis" v-if="authStore.showingRegisForm"/>  
            <LoginForm ref="login" v-if="authStore.showingLoginForm"/>  
        </div>

        <div class="validation" v-show="!authStore.mailNotifSent && !authStore.regisFailed">
            <div class="validation_success" v-show="authStore.showingLoginForm && (checkWebGL1() === true || checkWebGL2() === true)">
                WebGL ist aktiviert ✓  <br/>
            </div>
            <div class="validation_fail" v-show="authStore.showingLoginForm && (checkWebGL1() === false && checkWebGL2() === false)">
                WebGL ist deaktiviert !  <br/>
            </div>
            <span class="validation_fail" v-show="authStore.showingLoginForm && authStore.loginFailed">
                Zugangsdaten nicht erkannt . . .  <br/>
            </span>
            <span v-if="authStore.showingRegisForm"> 
                Ihr Passwort braucht <span class="strong">6 bis 20 Zeichen</span>, <br/> mit mindestens einer <span class="strong">Zahl</span>, <br/> sowie <span class="strong">Groß- & Kleinbuchstaben</span> <br/>
            </span>
        </div>

        <div class="after_regis" v-if="authStore.mailNotifSent">
            Aktiviere dein Konto über den Validierungs-Link in deinem E-Mail Postfach,
            <br/>
            um die Registrierung abzuschließen
            <br/><br/>
            (Es kann teilweise ein paar Minuten dauern, bis die E-Mail ankommt.)
        </div>

        <div class="after_regis" v-if="authStore.regisFailed">
            Registrierung fehlgeschlagen. 
            <br/>
            Womöglich existiert bereits ein Konto mit dieser E-Mail Adresse.
            <br/><br/>
            Probiere es gerne zu einem späteren Zeitpunkt erneut
            <br/>
            oder kontaktiere unseren Kundensupport.
        </div>
    </div>

</template>

<style scoped>
.authenticator {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    align-items: center;
    justify-content: center;
    flex-direction: column;
    font-size: .8rem;

    /* z-index: 19; */
    text-shadow: 0rem 0rem 1rem black;
}
.disclaimer {
    /* grid-column: 1; */
    padding: 5%;
}

.form {
    /* grid-column: 2; */
    padding-left: 15%;
    padding-right: 15%;
}
.validation {
    place-self: center;
    text-align: center;
    /* font-size: .6rem; */
    padding-top: 15%;
    padding-bottom: 15%;
}
.validation_success {
    color:rgb(0, 255, 0);
}
.validation_fail {
    color:rgb(255, 0, 0);
}

.after_regis {
    font-size: 1rem;
    text-align: center;
    grid-column: span 3;
}

@media (max-width: 800px) {
    .authenticator {
        grid-template-columns: 1fr;
    }
}
</style>
