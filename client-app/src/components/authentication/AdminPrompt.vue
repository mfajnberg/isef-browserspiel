<script setup>
import { ref, onMounted } from 'vue';
import { useUIStore } from '../../stores/UIStore'
import { usePartyStore } from '../../stores/PartyStore'
import { useGameAssetStore } from '../../stores/GameAssetStore';
import { useWorldStore } from '../../stores/WorldStore';
import { requestGetWorldSliceAdmin } from '../../services/EditorService'
import { useAuthStore } from '../../stores/AuthStore';

const uiStore = useUIStore()
const authStore = useAuthStore()
const worldStore = useWorldStore()
const assetStore = useGameAssetStore()

let qInput = 0
let rInput = 0

async function clickEdit() {
    worldStore.ACTION(assetStore)
    worldStore.setAbsoluteZeroOffset(qInput, rInput)
    await requestGetWorldSliceAdmin(authStore, worldStore)
    let responseStatus = worldStore.response.status
    // // DEBUG------------------------//
    // responseStatus = 200
    // // -.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-
    if (responseStatus === 200) {
        uiStore.editorMode = true
        uiStore.showWorldmap(worldStore)
    }
}

function playSoundPointerDown() { assetStore.pointerDownSound.play() }
function playSoundPointerUp(event) { if (event.button === 0) assetStore.pointerUpSound.play() }
const button_edit = ref(null)
onMounted(() => {
    button_edit.value.addEventListener('pointerdown', playSoundPointerDown);
    button_edit.value.addEventListener('pointerup', playSoundPointerUp);  
    document.addEventListener("mousedown", dragStart)
    document.addEventListener("mouseup", dragEnd)
    document.addEventListener("mousemove", drag)
    document.addEventListener("mouseleave", dragEnd)
})        
const draggableElement = ref(null)
var isDragging = false
var currentX = 0
var currentY = 0
var initialX = 0
var initialY = 0
var xOffset = 0
var yOffset = 0
function dragStart(e) {
    if (e.target === draggableElement.value) {
            initialX = e.clientX - xOffset
            initialY = e.clientY - yOffset
            console.log(e.target)
            isDragging = true
    }
}
function dragEnd() {
    initialX = currentX
    initialY = currentY
    isDragging = false
}
function drag(e) {
    if (isDragging) {
        currentX = e.clientX - initialX
        currentY = e.clientY - initialY
        xOffset = currentX
        yOffset = currentY
        setTranslate(currentX, currentY, draggableElement)
    }
}
function setTranslate(xPos, yPos, el) {
    el.value.style.translate = `${xPos}px ${yPos}px`
}
</script>

<template>
    <div class="admin" ref="draggableElement">
        Herzlich willkommen im Bereich für Admins!<br/><br/>
        Bitte gib die gewünschten Koordinaten zum editieren ein. <br/><br/>
        <span>
            Q: <input v-model="qInput"/>
        </span> <br/>
        <span>
            R: <input v-model="rInput"/>
        </span> <br/>
        <button ref="button_edit" @click="clickEdit()">Welt editieren</button>
    </div>
</template>

<style scoped>
.admin {
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: column;
    padding: 3em;
    background-color: rgba(0, 0, 0, 0.832);

    position: absolute;
    z-index: 100;
    border-style: double;
    border-color: rgb(133, 113, 86);
    border-width: 3px;

    user-select: none;
}
</style>
