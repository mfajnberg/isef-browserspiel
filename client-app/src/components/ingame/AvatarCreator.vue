<script setup>
import { useCreatorStore } from '../../stores/AvatarCreatorStore.js';

const creatorStore = useCreatorStore()
</script>

<template>
    <div class="creator">
        <div class="option">
            <div class="portrait portrait-first"></div>
            <div class="statblock statblock-first">
                Verstand: {{creatorStore.statBlocks[0].Intellect}} <br/>
                Disziplin: {{creatorStore.statBlocks[0].Discipline}} <br/>
                Stärke: {{creatorStore.statBlocks[0].Power}} <br/>
                Geschick: {{creatorStore.statBlocks[0].Agility}} <br/>
                Luzidität: {{creatorStore.statBlocks[0].Lucidity}} <br/>
                Charisma: {{creatorStore.statBlocks[0].Charisma}} <br/>
            </div>
            <button class="select">
                Avatar wählen
            </button>
        </div> 
        <div class="option">
            <div class="portrait portrait-second"></div>
            <div class="statblock statblock-second">
                Verstand: {{creatorStore.statBlocks[1].Intellect}} <br/>
                Disziplin: {{creatorStore.statBlocks[1].Discipline}} <br/>
                Stärke: {{creatorStore.statBlocks[1].Power}} <br/>
                Geschick: {{creatorStore.statBlocks[1].Agility}} <br/>
                Luzidität: {{creatorStore.statBlocks[1].Lucidity}} <br/>
                Charisma: {{creatorStore.statBlocks[1].Charisma}} <br/>
            </div>
            <button class="select">
                Avatar wählen
            </button>
        </div>
        <div class="option">
            <div class="portrait portrait-third"></div>
            <div class="statblock statblock-third">
                Verstand: {{creatorStore.statBlocks[2].Intellect}} <br/>
                Disziplin: {{creatorStore.statBlocks[2].Discipline}} <br/>
                Stärke: {{creatorStore.statBlocks[2].Power}} <br/>
                Geschick: {{creatorStore.statBlocks[2].Agility}} <br/>
                Luzidität: {{creatorStore.statBlocks[2].Lucidity}} <br/>
                Charisma: {{creatorStore.statBlocks[2].Charisma}} <br/>
            </div>
            <button class="select">
                Avatar wählen
            </button>
        </div>
    </div>
</template>

<style scoped>
    .creator {
        display: grid;
        grid-template-columns: 1fr 1fr 1fr ;
        grid-template-rows: 1fr;
        width: 60vw;
        height: 80vh;
        bottom: 0%;
        align-items: center;
        justify-content: center;
        flex-direction: row;
        position: absolute;
        background: linear-gradient(180deg, rgba(0,0,0,1) 20%, rgba(0,0,0,0) 100%);
        z-index: 30;
    }
    .option {
        display: grid;
        grid-template-columns: 1fr;
        grid-template-rows: 3fr 6fr 1fr ;
        height: 100%;
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
    }.portrait-first {
        background-image: url('Portrait_Eliana.jpg');
    }.portrait-second {
        background-image: url('Portrait_Leito.jpg');
    }.portrait-third {
        background-image: url('Portrait_Marsilio.jpg');
    }
    .statblock{
        grid-row: 2;
        
    }.statblock-first{

    }.statblock-second{

    }.statblock-third{

    }
    .select {
        grid-row: 3;    
        pointer-events: all;
        user-select: none;
    }
</style>
