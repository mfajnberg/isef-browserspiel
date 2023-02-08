<script setup>
import { ref, onMounted } from 'vue'
import { usePlayer } from '@vue-youtube/core'
import { useUIStore } from './stores/UIStore.js'
import { useAuthStore } from './stores/AuthStore.js'
import { usePartyStore } from './stores/PartyStore'
import { requestTokenRefresh } from './services/AuthService.js'

import Header from './components/Header.vue'
import Footer from './components/Footer.vue'
import Imprint from './components/Imprint.vue'
import Authenticator from './components/authentication/Authenticator.vue'
import AdminPrompt from './components/authentication/AdminPrompt.vue'
import AvatarCreator from './components/ingame/AvatarCreator.vue'
import Overlay from './components/ingame/Overlay.vue'

    // let image1 = new Image()
    // image1.src = "assets/Portrait_Eliana.jpg"
    // let image2 = new Image()
    // image2.src = "assets/Portrait_Leito.jpg"
    // let image3 = new Image()
    // image3.src = "assets/Portrait_Marsilio.jpg"

    const authStore = useAuthStore()
    const uiStore = useUIStore()
    const partyStore = usePartyStore()

    const videoId = ref('PmdVSxLBZjU');
    const youtube = ref();

    const { onReady } = usePlayer(videoId, youtube, {
    cookie: false,
    playerVars: {
        mute: 0
    },
        width: "100%",
        height: "100%"
    });

    onReady((event) => {
        // event.target.playVideo();
    });

    onMounted(() => {
        if (localStorage.token != null) {
            authStore.Email = localStorage.Email
            requestTokenRefresh(authStore, partyStore)
        }
    })

</script>

<template>
    <div id="background">
        <Header/>
        <div class="content">
            <div id="welcome" v-show="uiStore.showingHome">
                <h2 class="grid_span_2">Willkommen</h2>
                <div class="welcome_item">
                    <h3 class="welcome_item_heading">in Averroes, ...</h3>
                    <p>
                        ... einer Welt voller Gefahren und Abenteuer. 
                        <br/><br/>
                        Dich erwartet ein beispielloses Spielerlebnis, 
                        mit Elementen aus klassischen Strategie- und Rollenspielen.
                        <br/><br/> 
                        ✓ Erkunde legendäre Stätten.
                        <br/>
                        ✓ Sammle Einfluss und Berühmtheit.
                        <br/>
                        ✓ Kämpfe, wofür es sich für dich zu kämpfen lohnt.
                    </p>
                </div>
                <div class="welcome_item vid">
                    <div id="vid" ref="youtube"/>
                </div>
                <div class="welcome_item grid_span_2">
                    <h3 class="welcome_item_heading">Schon sehr bald ...</h3>
                    <p>
                        ... ist diese App Teil einer integrierten Spielwelt – 
                        bist du bereit frischen Wind in ein wachsendes Team zu bringen? <br/>
                        Für die Arbeit am Schwesterprojekt sind nämlich noch diverse <a class="anchor_inline">Stellen</a> offen. 
                    </p>
                </div>
            </div>
            <Authenticator v-show="uiStore.getShowingAuthentication"/>
            <AdminPrompt v-if="uiStore.getShowingAdminPrompt"/>
            <AvatarCreator id="avatar_creator" v-if="uiStore.getShowingAvatarCreator"/>
            <canvas canvas id="adventure_map" v-show="uiStore.getShowingWorldmap"></canvas>
            <Overlay id="overlay" v-if="uiStore.getShowingWorldmap"/>
            <Imprint v-if="uiStore.getShowingImprint"/>
        </div>
        <Footer/>
    </div>
</template>

<style scoped>

#background {
    position: fixed;
    height: 100vh;
    width: 100vw;
    left: 0;
    top: 0;

    background-image: url("twilight_aegis.jpg");
        background-repeat: no-repeat;
        background-attachment: fixed;
        background-position: center;
        background-size: cover;
}

    .content {
        display: flex;
        width: 100%;
        height: 85vh;

        position: relative;
        left: 0%;
        top: 10vh;

        align-items: center;
        justify-content: center;
    }
    h2{
        display: flex;
        justify-content: center;
        align-items: center;
        height: 75%;
        color: white;
        text-shadow: 0rem 0rem 1rem black;

    }
    #welcome {
        display: grid;
        height: 85%;
        gap: 2rem;
        grid-template-columns: 1fr 1fr ;
        grid-template-rows: 1fr 6fr 3fr ;
        max-width: 60rem;
        text-align: left;
        user-select: none;
        
    } .welcome_item {
        display: flex;
        flex-direction: column;
        justify-content: flex-start;
        align-content: center;
        padding: 0rem 2rem 0rem 2rem;
        border-style: outset;
        border-width: 1px;
        border-color: rgba(133, 113, 86, 0.5) ;
        text-shadow: 0rem 0rem 1rem black;
        /* transition: .5s; */
        cursor: default;
    } .welcome_item:hover {
        /* border-color: rgba(252, 205, 143, 100); */
        /* border-style: inset; */
        /* color: white; */
    } .vid {
        border-style: none;
        padding: 0;
    } .welcome_item_heading {
        color: white;
    } .grid_span_2 {
        grid-column: span 2;
        text-align: right;
    } .anchor_inline {
        display: inline;
    }

    #adventure_map {
    background-color: rgba(0, 0, 0, 0.851);
    z-index: 10;
    }

    #overlay {
        position: absolute;
        height: 100%;
        z-index: 11;
        pointer-events:none;
    }

    #avatar_creator {
        position: fixed;
        z-index: 21;
        width: 100%;
        height: 100%;
        pointer-events:none;
    }

</style>
