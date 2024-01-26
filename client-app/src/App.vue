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
import Account from './components/Account.vue'
import Authenticator from './components/authentication/Authenticator.vue'
import AdminPrompt from './components/authentication/AdminPrompt.vue'
import AvatarCreator from './components/ingame/AvatarCreator.vue'
import Overlay from './components/ingame/Overlay.vue'
import About from './components/About.vue'

    const authStore = useAuthStore()
    const uiStore = useUIStore()
    const partyStore = usePartyStore()

    const videoId = ref('EEqhHsvarus');
    const youtube = ref();

    const { onReady } = usePlayer(videoId, youtube, {
    cookie: false,
    playerVars: {
        mute: 0
    },
        width: "100%",
        height: "100%"
    });

    onMounted(() => {
        if (localStorage.token != null) {
            authStore.Email = localStorage.Email
            requestTokenRefresh(authStore, partyStore)
        }
    })

</script>

<template>
    <div id="screen_too_small">
        Die mobile Version von IS:EF ist bereits in Planung.
        <br/>
        <br/>
        Um mitzuspielen, wechseln Sie bitte auf ein anderes Gerät.
    </div>
    <div id="background">
        <Header/>
        <div class="content">
            <div class="welcome" v-show="!uiStore.getShowingWorldmap && !uiStore.getShowingAvatarCreator">
                <div class="welcome_heading grid_span_2">
                    <h2 v-if="!uiStore.getShowingAdminPrompt">Willkommen in Averroes,</h2>
                    <h2 v-if="uiStore.getShowingAdminPrompt">Herzlich willkommen, Admin!</h2>
                </div>
                <div class="welcome_sub_container grid_span_2">
                    <div class="welcome_item synopsis" v-show="uiStore.getShowingHome && !uiStore.loadingAssets">
                        <h3 class="welcome_item_heading heading_synopsis">... einer Welt voller Gefahren und Abenteuer. </h3>
                        <p>
                            <span class="check_mark">✓</span> Erkunde legendäre Stätten
                            <br/><br/>
                            <span class="check_mark">✓</span> Sammle Einfluss und Reichtümer
                            <br/><br/>
                            <span class="check_mark">✓</span> Entscheide, wofür es sich zu kämpfen lohnt
                        </p>
                    </div>
                    <div class="welcome_item vid" v-show="uiStore.getShowingHome && !uiStore.loadingAssets">
                        <div id="vid" ref="youtube"/>
                    </div>
    
                    <div class="grid_span_2 admin" v-if="uiStore.getShowingAdminPrompt">
                        <AdminPrompt/>
                    </div>
    
                    <div class="loading welcome_item" v-if="uiStore.loadingAssets">
                        <h3 class="heading_loading">Inhalte werden geladen...</h3>
                        <span class="text_transparent">{{uiStore.loadingProgress}}</span>
                    </div>
                </div>
                
                <div class="welcome_item grid_span_2 authenticator" v-show="uiStore.getShowingAuthentication && !uiStore.loadingAssets">
                    <Authenticator/>
                </div>

                <div class="roadmap welcome_item grid_span_2" v-show="uiStore.getShowingHome || uiStore.showingAdminPrompt || uiStore.loadingAssets">
                    <!-- <h3 class="welcome_item_heading">Roadmap</h3> -->
                    <p>
                        Diese App wurde im Rahmen einer Prüfungsleistung an der IU International University of Applied Sciences entwickelt.
                    </p>
                </div>

                <div class="socials">
                    <a class="soc_item discord" href="https://discord.gg/uzFgCCCKE6" target="_blank"></a>
                    <a class="soc_item youtube" href="https://www.youtube.com/@autogenes" target="_blank"></a>
                    <a class="soc_item twitch" href="https://www.twitch.tv/mjboazy" target="_blank"></a>
                </div>

                <div class="grid_span_2 imprint" v-if="uiStore.getShowingAbout">
                    <About/>
                </div>
                <div class="grid_span_2 imprint" v-if="uiStore.getShowingAccount">
                    <Account/>
                </div>
                <div class="grid_span_2 imprint" v-if="uiStore.getShowingImprint">
                    <Imprint/>
                </div>
            </div>
            <AvatarCreator id="avatar_creator" v-if="uiStore.getShowingAvatarCreator"/>
            <canvas canvas id="adventure_map" v-show="uiStore.getShowingWorldmap"></canvas>
            <Overlay id="overlay" v-if="uiStore.getShowingWorldmap"/>
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

    background-image: url("/twilight_aegis.jpg");
        background-repeat: no-repeat;
        background-attachment: fixed;
        background-position: center;
        background-size: cover;
}
#screen_too_small {
    display: none;
    justify-content: center;
    position: fixed;
    height: 100vh;
    height: 100svh;
    width: 100vw;
    width: 100svw;
    left: 0;
    top: 0;

    background-color: black;
    z-index: 99999
}
@media (pointer: coarse) {
    #screen_too_small {
        display: flex;
        align-items: center;
    }
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
        grid-template-rows: 1fr 4.5fr 3fr 1fr ;
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
        font-size: 1rem;
        text-shadow: 0rem 0rem 1rem black;
        background-color: rgba(0, 0, 0, 0.852);
        /* background: linear-gradient(90deg, transparent 0%, rgba(0, 0, 0, 0.852) 25%, rgba(0, 0, 0, 0.852) 75%, transparent 100%); */
        /* transition: .5s; */
        cursor: default;
    }
    .synopsis {
        justify-content: space-evenly;
    }.check_mark {
        display: inline-flex;
        color: rgb(0, 255, 0);
        font-size: 1.4rem;
        padding-right: .1rem;
    }.weak {
        color: white;
    } .vid {
        /* border-style: none; */
        padding: 0;
        background-color: transparent;
        border-style: double;
        border-color: transparent;
        border-width: unset;
        border-top-color: rgba(252, 205, 143, 1);
        border-bottom-color: rgba(252, 205, 143, .5);
    } .roadmap {
        /* font-size: .8rem; */
        text-align: center;
    } .welcome_item_heading {
        margin-top: 0%;
        color: white;
        font-size: 1rem;
        height: 0rem;
        text-align: center;
    }  .heading_synopsis {
        margin-bottom: 0%;
        text-align: left;
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
    }
    .authenticator, .imprint {
        grid-row: 2 / 4;
        /* display: grid; */
    } .admin {
        grid-row: 1;
    }
    .socials {
        display: flex;
        justify-self: right;
        height: 2rem;

        flex-direction: row;
        justify-content: right;

        position: absolute;
        bottom: 10vh;
    }
    .soc_item {
        display: flex;
        align-self: center;
        justify-self: center;

        position: absolute;

        width: 1.7rem;
        height: 1.7rem;

        background-repeat: no-repeat;
        background-size: contain;
        background-position: center;
    }
    .discord {
        right: 6rem;
        background-image: url("/discord_blurple.svg");
        background-image: url("/discord_blurple.svg");
    } .discord:hover{
        background-image: url("/discord.svg");
    }
    .twitch {
        right: 0rem;
        background-image: url("/TwitchGlitchPurple.svg");
    } .twitch:hover {
        background-image: url("/TwitchGlitchWhite.svg");
    }
    .youtube {
        right: 3rem;
        background-image: url("/YouTube_color.svg");
    } .youtube:hover {
        background-image: url("/YouTube_light.svg");
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
