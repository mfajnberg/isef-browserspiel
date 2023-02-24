<script setup>
import { ref, onMounted } from 'vue';
import { useUIStore } from '../../stores/UIStore'
import { usePartyStore } from '../../stores/PartyStore'
import { useGameAssetStore } from '../../stores/GameAssetStore';
import { useWorldStore } from '../../stores/WorldStore';
import { requestGetWorldSliceAdmin } from '../../services/EditorService'
import { useAuthStore } from '../../stores/AuthStore';
import { useCreatorStore } from '../../stores/AvatarCreatorStore';
import { Worldmap } from '../../classes/Worldmap';
import { HexVector } from '../../classes/HexVector';

const uiStore = useUIStore()
const authStore = useAuthStore()
const worldStore = useWorldStore()
const assetStore = useGameAssetStore()
const partyStore = usePartyStore()
const creatorStore = useCreatorStore()

let qInput = 0
let rInput = 0

async function clickEdit() {
    uiStore.setEditorMode(true)
    worldStore.editingAt = new HexVector(qInput, rInput)
    await requestGetWorldSliceAdmin(
        worldStore.editingAt.Q,
        worldStore.editingAt.R
    )

    // let hexVec = new HexVector(qInput, rInput)
    // worldStore.moveCamera(hexVec.getWorldXFromAxialQ, hexVec.getWorldZFromAxialR)
    
    await uiStore.PlayNow(authStore, partyStore, worldStore, assetStore, creatorStore)
}

async function clickPlay() {
    uiStore.setEditorMode(false)
    await uiStore.PlayNow(authStore, partyStore, worldStore, assetStore, creatorStore) 
}

function playSoundPointerDown() { assetStore.pointerDownSound.play() }
function playSoundPointerUp(event) { if (event.button === 0) assetStore.pointerUpSound.play() }
const button_edit = ref(null)
const button_play = ref(null)
onMounted(() => {
    button_edit.value.addEventListener('pointerdown', playSoundPointerDown)
    button_play.value.addEventListener('pointerdown', playSoundPointerDown)
    button_edit.value.addEventListener('pointerup', playSoundPointerUp)
    button_play.value.addEventListener('pointerup', playSoundPointerUp)
})        
</script>

<template>
    <div class="admin">
        <button ref="button_play" @click="clickPlay" class="button_play"> Als Spieler fortfahren </button>
        <div class="world_edit_form">
                <span>oder Koordinaten eingeben:</span>
                <div>
                    <span>
                        <label class="label_coordinate">Q</label><input class="input_coordinate" v-model="qInput"/>
                    </span>
                    <span>
                        <label class="label_coordinate">R</label><input class="input_coordinate" v-model="rInput"/>
                    </span>
                </div>
        </div>
        <button ref="button_edit" @click="clickEdit()" class="button_edit">und die Welt editieren.</button>
    </div>
</template>

<style scoped>
.admin {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    grid-template-rows: 1fr 1fr 1fr;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    /* padding: 2rem; */
    height: 100%;
    width: 60rem;
    max-width: 100%;

    z-index: 100;
    
    font-size: .8rem;
    text-align: center;
    text-shadow: 0rem 0rem 1rem black;
    background-color: rgba(0, 0, 0, 0.832);

    border-style: outset;
    border-width: 1px;
    border-color: rgba(133, 113, 86, 0.5) ;
    border-width: 1px;

    user-select: none;
} 
h3, p {
    grid-column: span 3;
}
span {
    display: flex;
    align-items: center;
    justify-content: right;
} 
button {
    font-size: 1rem;
}
.button_play {
    grid-column: 1;
    grid-row: 2;
    width: 12rem;
    align-self: center;
    justify-self: center;
} 
.world_edit_form {
    grid-column: 2;
    grid-row: 2;
    display: flex;
    flex-direction: column;
    align-items: center;
} .label_coordinate {
    display: flex;
    justify-content: center;
    margin-left: .5rem;
    color: white;
} .input_coordinate {
    display: flex;
    margin-left: .5rem;
    width: 4rem;
    max-width: 10vw;
    height: 1rem;
    text-align: center;
}
.button_edit {
    grid-column: 3;
    grid-row: 2;
    width: 12rem;
    align-self: center;
    justify-self: center;
}
.other {
    grid-row: 3;
    padding-left: 15%;
    padding-right: 15%;
}
</style>
