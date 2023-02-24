<script setup>
import { ref, onMounted } from 'vue';
import { requestPostChoice } from '../../services/AvatarCreatorService';
import { useAuthStore } from '../../stores/AuthStore';
import { useCreatorStore } from '../../stores/AvatarCreatorStore.js';
import { useGameAssetStore } from '../../stores/GameAssetStore';
import { useUIStore } from '../../stores/UIStore';
import { useWorldStore } from '../../stores/WorldStore';
import { usePartyStore } from '../../stores/PartyStore';

const uiStore = useUIStore()
const authStore = useAuthStore()
const gameAssetStore = useGameAssetStore()
const partyStore = usePartyStore()
const worldStore = useWorldStore()
const creatorStore = useCreatorStore()

async function clickSelectAvatar(name) {
    // check response
    await requestPostChoice(authStore, creatorStore, partyStore, name)
    uiStore.editorMode = false
    uiStore.PlayNow(authStore, partyStore, worldStore, gameAssetStore, creatorStore)
}

function playSoundPointerDown() {
    gameAssetStore.pointerDownSound.play()
}
function playSoundPointerUp(event) {
    if (event.button === 0) {
        gameAssetStore.pointerUpSound.play()
    }
}

const select_first = ref(null); 
const select_second = ref(null);
const select_third = ref(null);

onMounted(() => {
    select_first.value.addEventListener('pointerdown', playSoundPointerDown);
    select_first.value.addEventListener('pointerup', playSoundPointerUp);
    select_second.value.addEventListener('pointerdown', playSoundPointerDown);
    select_second.value.addEventListener('pointerup', playSoundPointerUp);
    select_third.value.addEventListener('pointerdown', playSoundPointerDown);
    select_third.value.addEventListener('pointerup', playSoundPointerUp);
})        

</script>

<template>
    <div class="creator">
        <h3>Wähle deinen Avatar...</h3>
        <div class="option" ref="option_one">
            <div class="portrait portrait-first"></div>
            <span class="name">
                {{creatorStore.statBlocks[0].Name}} <br/><br/>
            </span>
            <div class="statblock statblock-first">
                <span class="attribute">Verstand: </span>
                <span class="value">{{creatorStore.statBlocks[0].Intellect}} </span>
                <span class="attribute">Disziplin: </span>
                <span class="value">{{creatorStore.statBlocks[0].Discipline}} </span>
                <span class="attribute">Stärke: </span>   
                <span class="value">{{creatorStore.statBlocks[0].Power}} </span>
                <span class="attribute">Geschick: </span> 
                <span class="value">{{creatorStore.statBlocks[0].Agility}} </span>
                <span class="attribute">Luzidität: </span>
                <span class="value">{{creatorStore.statBlocks[0].Lucidity}} </span>
                <span class="attribute">Charisma: </span> 
                <span class="value">{{creatorStore.statBlocks[0].Charisma}} </span>
            </div>
            <p class="description">
                {{ creatorStore.statBlocks[0].Description }}
            </p>
            <button class="button_select" ref="select_first" @click="clickSelectAvatar('Eliana Dawnbreak')">
                ✓
            </button>
        </div> 
        <div class="option" ref="option_two">
            <div class="portrait portrait-second"></div>
            <span class="name">
                {{creatorStore.statBlocks[1].Name}} <br/><br/>
            </span>
            <div class="statblock statblock-second">
                <span class="attribute">Verstand: </span>
                <span class="value">{{creatorStore.statBlocks[1].Intellect}} </span>
                <span class="attribute">Disziplin: </span> 
                <span class="value">{{creatorStore.statBlocks[1].Discipline}} </span>
                <span class="attribute">Stärke: </span>
                <span class="value">{{creatorStore.statBlocks[1].Power}} </span>
                <span class="attribute">Geschick: </span>
                <span class="value">{{creatorStore.statBlocks[1].Agility}} </span>
                <span class="attribute">Luzidität: </span>
                <span class="value">{{creatorStore.statBlocks[1].Lucidity}} </span>
                <span class="attribute">Charisma: </span>
                <span class="value">{{creatorStore.statBlocks[1].Charisma}} </span>
            </div>
            <p class="description">
                {{ creatorStore.statBlocks[1].Description }}
            </p>
            <button class="button_select" ref="select_second" @click="clickSelectAvatar('Leito Froste')">
                ✓
            </button>
        </div>
        <div class="option" ref="option_three">
            <div class="portrait portrait-third"></div>
            <span class="name">
                {{creatorStore.statBlocks[2].Name}} <br/><br/>
            </span>
            <div class="statblock statblock-third">
                <span class="attribute">Verstand: </span>
                <span class="value">{{creatorStore.statBlocks[2].Intellect}} </span>
                <span class="attribute">Disziplin: </span>
                <span class="value">{{creatorStore.statBlocks[2].Discipline}} </span>
                <span class="attribute">Stärke: </span>
                <span class="value">{{creatorStore.statBlocks[2].Power}} </span>
                <span class="attribute">Geschick: </span>
                <span class="value">{{creatorStore.statBlocks[2].Agility}} </span>
                <span class="attribute">Luzidität: </span>
                <span class="value">{{creatorStore.statBlocks[2].Lucidity}} </span>
                <span class="attribute">Charisma: </span>
                <span class="value">{{creatorStore.statBlocks[2].Charisma}} </span>
            </div>
            <p class="description">
                {{ creatorStore.statBlocks[2].Description }}
            </p>
            <button class="button_select" ref="select_third" @click="clickSelectAvatar('Marsilio Mirandola')">
                ✓
            </button>
        </div>
    </div>
</template>

<style scoped>
    .creator {
        display: grid;
        grid-template-columns: 1fr 1fr 1fr ;
        grid-template-rows: 1fr;
        align-items: center;
        justify-content: center;
        position: fixed;
        background: linear-gradient(180deg, rgba(0,0,0,1) 20%, rgba(0,0,0,0) 100%);
        user-select: none;

    } .creator > * { 
        pointer-events: all;
    }
    h3 {
        justify-self: center;
        align-self: center;
        line-height: 0%;
        position: fixed;
        top: 2%;
        color: rgba(252, 205, 143, .5);
        font-family: 'fondamento';
    }
    .option {
        display: grid;
        padding-top: 15%;
        padding-left: 5%;
        padding-right: 5%;
        padding-bottom: 10%;
        grid-template-columns: 1fr;
        grid-template-rows: 2fr .3fr 1.5fr 3fr .5fr ;
        text-shadow: 0rem 0rem 1rem black;
        /* height: 100%; */
    } 
    .option:hover > .description {
        color: rgb(252, 205, 143);
        transition: 1s;
    }.option:hover > .name {
        border-bottom-color: white;
        transition: 1s;
    }.option:hover > .button_select {
        border-color: rgb(252, 205, 143);
        transition: 1s;
    }
    .portrait {
    grid-row: 1;
    background-repeat: no-repeat;
    background-position: center center;
    align-self: center;
    justify-self: center;
    width: 100%;
    height: 100%;
    user-select: none;
    /* border-style: outset;
    border-width: 1px;
    border-color: rgba(133, 113, 86, 0.5)  */
    }.portrait-first {
        background-image: url('Portrait_Eliana.jpg');
    }.portrait-second {
        background-image: url('Portrait_Leito.jpg');
    }.portrait-third {
        background-image: url('Portrait_Marsilio.jpg');
    }.name{
        grid-row: 2;
        margin-top: 1rem;
        margin-bottom: 1rem;
        text-align: center;
        display: flex;
        width: fit-content;
        height: 1.2rem;
        justify-self: center;
        padding: .2rem;
        font-size: 1rem;
        font-family: 'fondamento';
        font-style: italic;
        color: white;
        border-style: double;
        border-color: transparent;
        transition: 1s;
    }
    .statblock{
        grid-row: 3;
        display: grid;
        grid-template-columns: 1fr .25fr .75fr;
        justify-self: center;
        width: 50%;
        /* max-width: 200px; */
        max-height: 10%;
        text-align: right;
        font-size: .8rem;
    }
    .attribute{
        grid-column: 1;
    }.value{
        grid-column: 2;
        /* padding-right: 20%; */
        color: white;
        text-align: right;
        text-shadow: 0rem 0rem 1rem white;
    }
    .description{
        font-family: 'fondamento';
        grid-row: 4;
        margin-top: 10%;
        /* max-height: 30%; */
        padding: 0% 5% 0% 5%;
        font-style: italic;
        overflow: auto;
        color: rgba(252, 205, 143, 0.5);
        transition: 1s;
    }
    .button_select {
        position: fixed;
        justify-self: center;
        bottom: 1%;
        grid-row: 5;
        width: 70%;
        height: 7%;
        pointer-events: all;
        user-select: none;
        color: rgb(0, 255, 0);

        transition: 1s;
    }
</style>
