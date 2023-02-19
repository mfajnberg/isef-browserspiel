<script setup>
import { ref, onMounted } from 'vue'
import { usePlayer } from '@vue-youtube/core'
import { useUIStore } from './stores/UIStore.js'
import { useAuthStore } from './stores/AuthStore.js'
import { usePartyStore } from './stores/PartyStore'
import { useGameAssetStore } from './stores/GameAssetStore'
import { requestTokenRefresh } from './services/AuthService.js'

import Header from './components/Header.vue'
import Footer from './components/Footer.vue'
import Imprint from './components/Imprint.vue'
import Authenticator from './components/authentication/Authenticator.vue'
import AdminPrompt from './components/authentication/AdminPrompt.vue'
import AvatarCreator from './components/ingame/AvatarCreator.vue'
import Overlay from './components/ingame/Overlay.vue'

    const authStore = useAuthStore()
    const uiStore = useUIStore()
    const partyStore = usePartyStore()
    const gameAssetStore = useGameAssetStore()

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
            <div class="welcome" v-show="uiStore.getShowingHome || uiStore.getShowingAuthentication">
                <h2 class="welcome_heading grid_span_2">Willkommen in Averroes,</h2>
                <div class="welcome_sub_container grid_span_2">
                    <div class="welcome_item" v-show="uiStore.getShowingHome && !uiStore.loadingAssets">
                        <p>
                            ... einer Welt voller Gefahren und Abenteuer. 
                            <br/><br/>
                            Dich erwartet ein beispielloses Spielerlebnis, 
                            mit Elementen aus klassischen Strategie- und Rollenspielen.
                            <br/><br/> 
                            ✓ Erkunde legendäre Stätten.
                            <br/><br/>
                            ✓ Sammle Einfluss und Berühmtheit.
                            <br/><br/>
                            ✓ Kämpfe, wofür es sich für dich zu kämpfen lohnt.
                        </p>
                    </div>
                    <div class="welcome_item vid" v-show="uiStore.getShowingHome && !uiStore.loadingAssets">
                        <div id="vid" ref="youtube"/>
                    </div>
    
                    <div class="welcome_item grid_span_2" v-show="uiStore.getShowingAuthentication && !uiStore.loadingAssets">
                        <Authenticator/>
                    </div>
                    <div class="loading welcome_item" v-if="(uiStore.getShowingHome || uiStore.getShowingAuthentication) && uiStore.loadingAssets">
                        <h3 class="heading_loading">Inhalte werden geladen...</h3>
                        <span class="text_transparent">{{uiStore.loadingProgress}}</span>
                    </div>
                </div>

                <div class="roadmap welcome_item grid_span_2" v-show="uiStore.getShowingHome || uiStore.showingAuthentication">
                    <h3 class="welcome_item_heading">Roadmap</h3>
                    <p>
                        Bald wird diese App Teil einer integrierten Spielwelt.
                        Bist du bereit frischen Wind in ein wachsendes Team zu bringen? <br/>
                        Für die Arbeit am Schwesterprojekt sind diverse <a class="anchor_inline">Stellen</a> offen. 
                    </p>
                </div>
            </div>
            <AdminPrompt v-if="uiStore.getShowingAdminPrompt"/>
            <AvatarCreator id="avatar_creator" v-if="uiStore.getShowingAvatarCreator"/>
            <canvas canvas id="adventure_map" v-show="uiStore.getShowingWorldmap"></canvas>
            <Overlay id="overlay" v-if="uiStore.getShowingWorldmap"/>
            <Imprint v-if="uiStore.getShowingImprint"/>
        </div>
        <Footer v-if="!uiStore.showingWorldmap"/>
    </div>
</template>

<style scoped>

#background {
    display: flex;
    justify-content: center;
    position: fixed;
    height: 100vh;
    height: 100svh;
    width: 100vw;
    width: 100svw;
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
        height: auto; 

        /* position: relative;
        left: 0%; */
        /* top: 10vh; */

        align-items: center;
        justify-content: center;
    }
    h2{
        margin-top: 5vh;
        /* position: absolute; */
        display: flex;
        justify-content: start;
        align-items: center;
        /* height: 75%; */
        color: white;
        text-shadow: 0rem 0rem 1rem black;

    }
    .welcome {
        display: grid;
        height: 85%;
        grid-row-gap: 2rem;
        grid-template-columns: 1fr;
        grid-template-rows: 1fr 5fr 3fr ;
        width: 100%;
        max-width: 60rem;
        text-align: left;
        /* user-select: none; */
        
    } .welcome_item {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-content: center;
        padding: 0rem 2rem 0rem 2rem;
        border-style: outset;
        border-width: 1px;
        border-color: rgba(133, 113, 86, 0.5) ;
        text-shadow: 0rem 0rem 1rem black;
        background-color: rgba(0, 0, 0, 0.852);
        /* transition: .5s; */
        cursor: default;
    } .welcome_item:hover {
        /* border-color: rgba(252, 205, 143, 100); */
        /* border-style: inset; */
        /* color: white; */
    } .vid {
        /* border-style: none; */
        padding: 0;
    } .roadmap {
        font-size: .8rem;
        text-align: center;
    } .welcome_item_heading {
        color: white;
        height: 0rem;
        text-align: center;
    } .welcome_sub_container {
        display: grid;
        height: 100%;
        width: 100%;
        grid-template-columns: 1fr 1fr;
        grid-template-rows: 1fr;
        grid-column-gap: 2rem;
    }.grid_span_2 {
        grid-column: span 2;
        /* text-align: justify; */
    } .anchor_inline {
        display: inline;
        cursor:help;
    }
    @media (max-width: 800px) {
        #background {
            overflow: scroll;
            margin-bottom: 5vh;
        }
        .content {
            /* height: auto; */
            
        }
        .welcome {
            grid-template-columns: 1fr;
            grid-template-rows: 1fr 1fr 1fr;
            /* height: auto; */
        }
        .welcome_heading {
            justify-content: center;
        }
        .welcome_sub_container {
            grid-template-columns: 1fr;
        }
        /* .grid_span_2 {
            grid-column: 1;
        } */
        .vid {
            height: 200%;
            margin-top: 5vh;
            grid-row: 2;
        }
        .roadmap {
            margin-top: 30vh;
        }
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

    .loading {
    display: grid;
    grid-template-rows: 1fr .5fr .5fr 1fr;
    width: 60rem;
    height: 100%;
    padding: 0%;
    align-self: center;
    justify-self: center;
    align-items: center;
    justify-items: center;
} .heading_loading {
    grid-row: 2;
    display: flex;
    align-self: flex-start;
    justify-content: center;
    font-family: 'Fondamento';
    max-width: 100vw;
    } .text_transparent {
        grid-row: 3;
        display: flex;
        font-size: .8rem;
        color: rgba(252, 205, 143, 0.5)
    }
</style>
