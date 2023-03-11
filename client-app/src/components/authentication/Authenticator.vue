<script setup>
import { useAuthStore } from '../../stores/AuthStore.js'
import RegistrationForm from './RegisForm.vue'
import LoginForm from './LoginForm.vue'
import { ref } from 'vue';

const authStore = useAuthStore()

const regisRef = ref(null)
const loginRef = ref(null)

function checkWebGL1() {
    const canvas = document.createElement( 'canvas' );
    let enabled = !! ( window.WebGLRenderingContext && ( canvas.getContext( 'webgl' ) || canvas.getContext( 'experimental-webgl' ) ) )
    if (enabled) {
        return true
    }
    else {
        return false
    }
}
function checkWebGL2() {
    const canvas = document.createElement( 'canvas' );
    let enabled = !! ( window.WebGL2RenderingContext && canvas.getContext( 'webgl2' ) );
    if (enabled) {
        return true
    }
    else {
        return false
    }
}

function switchForms() {
    if (authStore.showingLoginForm) 
    {
        authStore.hideLoginForm()
        authStore.showRegisForm()
        regisRef.value.style.left = "0%"
        loginRef.value.style.right = "100%"
        authStore.updateValidation()
    } 
    else if (authStore.showingRegisForm) 
    {
        authStore.hideRegisForm()
        authStore.showLoginForm()
        regisRef.value.style.left = "100%"
        loginRef.value.style.right = "0%"
        authStore.updateValidation()
    }
}
</script>

<template> 
    <div class="authenticator">
        <h3 class="strong" v-if="authStore.showingLoginForm">Login</h3>
        <h3 class="strong" v-if="authStore.showingRegisForm">Registrierung</h3>
        <p class="disclaimer" v-show="!authStore.mailNotifSent && !authStore.regisFailed">
            <span class="strong">Achtung:</span> <br/>
            <p>
                Diese App verwendet WebGL. <br/> <br/> 
                Einige Browser deaktivieren diese Funktion automatisch.
                <br/><br/>
                Wir empfehlen <a class="opera" href="https://www.opera.com/gx" target="_blank">Opera GX</a> für ein angenehmes Spielerlebnis im Browser.
            </p>
            <!-- <br/><br/>
            WebGL unter LibreWolf aktivieren:
            <br/>
            <span class="strong"> > 'Settings'  <br/> > 'LibreWolf' <br/> > 'Enable WebGL' <br/> > Browser neu starten</span>  -->
        </p>

        <div class="form_container" 
            v-if="authStore.showingLoginForm || authStore.showingRegisForm"
            v-show="!authStore.mailNotifSent && !authStore.regisFailed">
            <div ref="regisRef" class="form regis">
                <RegistrationForm/>  
            </div>
            <div ref="loginRef" class="form login">
                <LoginForm/>  
            </div>
        </div>
        
        <p class="prompt" @click="switchForms">
            <span v-if="authStore.showingLoginForm">
                Noch kein Konto? <br/>
            </span>
            <a v-if="authStore.showingLoginForm">
                Jetzt registrieren!
            </a>
            <a v-if="authStore.showingRegisForm">
                Zurück
            </a>
        </p>

        <div class="validation" v-show="!authStore.mailNotifSent && !authStore.regisFailed">
            <div class="validation_suboptimal" v-show="authStore.showingLoginForm && (checkWebGL1() === true && checkWebGL2() === false)">
                Ihr Browser verwendet eine veraltete Version von WebGL. Das Spielerlebnis könnte dadurch beeinträchtigt werden.  <br/>
            </div>
            <div class="validation_success" v-show="authStore.showingLoginForm && (checkWebGL2() === true)">
                WebGL ist aktiviert ✓  <br/>
            </div>
            <div class="validation_fail" v-show="authStore.showingLoginForm && (checkWebGL1() === false && checkWebGL2() === false)">
                WebGL ist deaktiviert. <br/> Bitte verwenden Sie einen anderen Browser. <br/>
            </div>
            <span class="validation_fail" v-show="authStore.showingLoginForm && authStore.loginFailed">
                Zugangsdaten nicht erkannt . . .  <br/>
            </span>
            <span v-if="authStore.showingRegisForm"> 
                Das Passwort braucht <span class="strong">6 bis 20 Zeichen</span>, <br/> mindestens eine <span class="strong">Zahl</span>, <br/> <span class="strong">Groß- & Kleinbuchstaben</span> <br/>
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
    grid-template-rows: .3fr 1fr .3fr;
    align-items: center;
    justify-content: center;
    flex-direction: column;

    font-size: .8rem;

    /* z-index: 19; */
    text-shadow: 0rem 0rem 1rem black;
}
h3 {
    position: relative;
    top: 10vh;
    grid-column: 2;
    grid-row: 1;
    font-family: 'fondamento';
    text-align: center;
}
.disclaimer {
    grid-column: 1;
    grid-row: 1 / 4;
    padding: 7.5%;
    padding-right: 10%;
    color: rgba(252, 205, 143, .5); 
    font-size: 1rem;
    text-align: right;
    transition: 1s;
} .disclaimer > p {
    margin-top: 0%;
    text-align: right;
}.opera {
    color: rgb(255, 0, 76);
}
 .opera:hover {
    color: white;
} .disclaimer:hover {
    color: rgba(252, 205, 143, 1);
    transition: 1s;
} .disclaimer > span {
    justify-self: center;
}
.form_container {
    display: flex;
    height: 35vh;
    align-items: end;
    position: relative;
    border-style: solid;
    border-width: .75rem;
    border-bottom-width: 1rem;
    border-top-width: 2rem;
    /* border-bottom-left-radius: 2.5rem;
    border-top-right-radius: 2.5rem; */
    border-top-right-radius: 50%;
    border-top-left-radius: 50%;
    /* border-right-width: .2rem; */
    /* border-left-color: rgba(252, 205, 143, .5); */
    /* border-left-color: rgba(252, 205, 143, 1); */
    border-color: rgba(0, 0, 0, 1);
    border-bottom-color: transparent;
    border-top-color: transparent;
    overflow: hidden;
    transition: border-color 1s;
}
.form {
    display: flex;
    flex-direction: column;
    align-items: center;
    
    width: 100%;
    /* height: 100%; */

    position: absolute;
    transition: ease-out .25s;

} .regis {
    left: 100%;
} .login {
    right: 0%;
}
.prompt {
    align-self: center;
    justify-self: center;
    margin-top: auto;
    margin-bottom: 10%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;

    width: 10rem;
    height: 5rem;
    padding-right: 6%;
    padding-left: 6%;

    text-align: center;
    color: rgba(252, 205, 143);

    user-select: none;
    transition: 1.5s;
} .prompt:hover {
    cursor: pointer;
    transition: 1.5s;
} .prompt:active {
    transition: .1s;
    letter-spacing: initial;
} 
.prompt > a {
    /* font-size: 1rem; */
    color: white;
    transition: .2s;
} .prompt:hover > a {
    letter-spacing: .05rem;
    transition: .5s;
    /* text-shadow: 0rem 0rem 1rem white; */
} .prompt:active > a {
    /* letter-spacing: initial; */
    transition: .2s;
    /* border-radius: 0%; */
}
.validation {
    grid-column: 3;
    grid-row: 1 / 4;
    text-align: left;
    padding: 7.5%;
    padding-left: 10%;
}
.validation_success {
    color:rgb(0, 255, 0);
}
.validation_fail {
    color:rgb(255, 0, 76);
}
.validation_suboptimal {
    color:rgb(255, 255, 0);
}

.after_regis {
    font-size: 1rem;
    text-align: center;
    grid-column: span 3;
    grid-row: 2;
    padding-bottom: 10%;
}

@media (max-width: 800px) {
    .authenticator {
        grid-template-columns: 1fr;
    }
}
</style>
