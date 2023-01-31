<script setup>
import { ref, onMounted } from 'vue';
import { useUIStore } from '../../stores/UIStore'
import { usePartyStore } from '../../stores/PartyStore'
import { useGameAssetStore } from '../../stores/GameAssetStore';

const uiStore = useUIStore()
const partyStore = usePartyStore()
const assetStore = useGameAssetStore()

function playSoundPointerDown() {
    assetStore.pointerDownSound.play()
}
function playSoundPointerUp(event) {
    if (event.button === 0)
        assetStore.pointerUpSound.play()
}

const button_edit = ref(null)
onMounted(() => {
    button_edit.value.addEventListener('pointerdown', playSoundPointerDown);
    button_edit.value.addEventListener('pointerup', playSoundPointerUp);
})        

</script>

<template>
    <div class="admin">
        Herzlich willkommen im Bereich für Admins!<br/><br/>
        Bitte gib die gewünschten Koordinaten zum editieren ein. <br/><br/>
        <span>Q: <input v-model="partyStore.coordinates.Q"/> <br/></span>
        <span>R: <input v-model="partyStore.coordinates.R"/></span> <br/>
        <button ref="button_edit">Welt editieren</button>
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
