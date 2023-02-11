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
const assetStore = useGameAssetStore()
const partyStore = usePartyStore()
const worldStore = useWorldStore()
const creatorStore = useCreatorStore()

async function clickSelectAvatar(name) {
    // check response
    await requestPostChoice(authStore, creatorStore, partyStore, name)
    uiStore.editorMode = false
    uiStore.PlayNow(authStore, partyStore, worldStore, assetStore, creatorStore)
}

function playSoundPointerDown() {
    assetStore.pointerDownSound.play()
}
function playSoundPointerUp(event) {
    if (event.button === 0) {
        assetStore.pointerUpSound.play()
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
        <div class="option">
            <div class="portrait portrait-first"></div>
            <div class="statblock statblock-first">
                <span class="name">
                    {{creatorStore.statBlocks[0].Name}} <br/><br/>
                </span>
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
        <div class="option">
            <div class="portrait portrait-second"></div>
            <div class="statblock statblock-second">
                <span class="name">
                    {{creatorStore.statBlocks[1].Name}} <br/><br/>
                </span>
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
        <div class="option">
            <div class="portrait portrait-third"></div>
            <div class="statblock statblock-third">
                <span class="name">
                    {{creatorStore.statBlocks[2].Name}} <br/><br/>
                </span>
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
        bottom: 0%;
        /* align-items: center; */
        justify-content: center;
        position: fixed;
        background: linear-gradient(180deg, rgba(0,0,0,1) 20%, rgba(0,0,0,0) 100%);
        user-select: none;
    }
    .option {
        display: grid;
        padding-top: 5vh;
        padding-bottom: 5vh;
        grid-template-columns: 1fr;
        grid-template-rows: 2fr 1.5fr 3fr .6fr ;
        text-shadow: 0rem 0rem 1rem black;
        /* height: 100%; */
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
    }
    .statblock{
        grid-row: 2;
        display: grid;
        grid-template-columns: 1fr 1fr .5fr .5fr 1fr;
        justify-self: center;
        width: 100%;
        /* max-width: 200px; */
        max-height: 10vh;
        text-align: right;
        font-size: .8rem;
    }.statblock-first{

    }.statblock-second{

    }.statblock-third{

    }.name{
        grid-column: 2/5;
        margin-top: 1rem;
        text-align: center;
        font-size: 1rem;
        font-style: italic;
        color: white;
    }.attribute{
        grid-column: 2;
    }.value{
        grid-column: 3;
        padding-right: 20%;
        color: white;
        text-align: right;
        text-shadow: 0rem 0rem 1rem white;
    }
    .description{
        grid-row: 3;
        margin-top: 10vh;
        max-height: 30vh;
        padding: 0% 5% 0% 5%;
        font-style: italic;
        overflow: auto;
        pointer-events: all;
        color: rgba(252, 205, 143, 0.5);
    }
    .button_select {
        grid-row: 4;
        width: 100%;
        pointer-events: all;
        user-select: none;
        color: rgb(0, 255, 0);
    }
</style>
