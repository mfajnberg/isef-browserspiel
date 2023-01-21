<script setup>
import { ref, onMounted } from 'vue';
import { useUIStore } from '../../stores/UIStore.js'
import { useWorldStore } from '../../stores/WorldStore.js'

    const UIStore = useUIStore()
    const worldStore = useWorldStore()

    const cursorURLs = ['tree_cluster_fir_1.glb', 'tower_medieval.glb']

    function clickSlot(num) {
        if (UIStore.editorMode) {
            worldStore.changedCursorURL = true
            worldStore.cursorURL = cursorURLs[num]
        }
    }
    
    function playClickSoundPointerDown() {
        UIStore.pointerDownSound.play()
    }
    function playClickSoundPointerUp() {
        UIStore.pointerUpSound.play()
    }
    
    const button_slot_1 = ref(null);
    const button_slot_2 = ref(null);
    const button_slot_3 = ref(null);
    const button_slot_4 = ref(null);
    const button_slot_5 = ref(null);
    const button_slot_6 = ref(null);
    const button_slot_7 = ref(null);    
    const button_action_1 = ref(null);
    const button_action_2 = ref(null);
    const button_action_3 = ref(null);
    const button_action_4 = ref(null);
    onMounted(() => {
        button_slot_1.value.addEventListener('pointerdown', playClickSoundPointerDown);
        button_slot_1.value.addEventListener('pointerup', playClickSoundPointerUp);
        button_slot_2.value.addEventListener('pointerdown', playClickSoundPointerDown);
        button_slot_2.value.addEventListener('pointerup', playClickSoundPointerUp);
        button_slot_3.value.addEventListener('pointerdown', playClickSoundPointerDown);
        button_slot_3.value.addEventListener('pointerup', playClickSoundPointerUp);
        button_slot_4.value.addEventListener('pointerdown', playClickSoundPointerDown);
        button_slot_4.value.addEventListener('pointerup', playClickSoundPointerUp);
        button_slot_5.value.addEventListener('pointerdown', playClickSoundPointerDown);
        button_slot_5.value.addEventListener('pointerup', playClickSoundPointerUp);
        button_slot_6.value.addEventListener('pointerdown', playClickSoundPointerDown);
        button_slot_6.value.addEventListener('pointerup', playClickSoundPointerUp);
        button_slot_7.value.addEventListener('pointerdown', playClickSoundPointerDown);
        button_slot_7.value.addEventListener('pointerup', playClickSoundPointerUp);
        button_action_1.value.addEventListener('pointerdown', playClickSoundPointerDown);
        button_action_1.value.addEventListener('pointerup', playClickSoundPointerUp);
        button_action_2.value.addEventListener('pointerdown', playClickSoundPointerDown);
        button_action_2.value.addEventListener('pointerup', playClickSoundPointerUp);
        button_action_3.value.addEventListener('pointerdown', playClickSoundPointerDown);
        button_action_3.value.addEventListener('pointerup', playClickSoundPointerUp);
        button_action_4.value.addEventListener('pointerdown', playClickSoundPointerDown);
        button_action_4.value.addEventListener('pointerup', playClickSoundPointerUp);
    })

</script>


<template>
    <div id="overlay">
        <!--top_left_info-->
        <div id="clock">{{UIStore.currentTime}}</div>
        <!--top_right_info-->
        <div id="avatar"><img id="portrait" src="Portrait_Eliana.jpg"/></div>
        <div id="slots">
            <div class="button_slot" ref="button_slot_1" @click="clickSlot(0)">slot 1</div>
            <div class="button_slot" ref="button_slot_2" @click="clickSlot(1)">slot 2</div>
            <div class="button_slot" ref="button_slot_3">slot 3</div>
            <div class="button_slot" ref="button_slot_4">slot 4</div>
            <div class="button_slot" ref="button_slot_5">slot 5</div>
            <div class="button_slot" ref="button_slot_6">slot 6</div>
            <div class="button_slot" ref="button_slot_7">slot 7</div>
        </div>
        <div id="info_site">(site info)</div>
        <div id="actions">
            <div class="button_action" ref="button_action_1">action 1</div>
            <div class="button_action" ref="button_action_2">action 2</div>
            <div class="button_action" ref="button_action_3">action 3</div>
            <div class="button_action" ref="button_action_4">action 4</div>
        </div>
        <div id="info_hex">{{worldStore.getHovered}}</div>
    </div>
</template>


<style scoped>

#overlay {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 1fr 1fr 1fr 1fr 1fr;
    grid-template-rows: 3fr 7fr 12fr 7fr 1fr ;
}

#clock {
    grid-row: 1;
    grid-column: 4 / 6;
    align-self:flex-start;
    justify-self: center;

    font-family: monospace;
    user-select: none;
}
#avatar {
    grid-row: 4 / 6;
    grid-column: 1;
    align-self: center;
} #portrait {
    width: 6vw;
    height: 6vw;
    /* min-width: 100px;
    min-height: 100px; */

    user-select: none;
    pointer-events: all;
    cursor: pointer;
}

#slots{
    grid-row: 4 / 6;
    grid-column: 2/4;
    display: flex;
} .button_slot {
    align-self: center;
    margin: .5vw;
    padding: 1vw;

    cursor: pointer;
    pointer-events: all;
    user-select: none;
    background-image: url('leather_texture.jpg');

    border-style:solid;
    border-width: 2px;
    border-color: rgb(133, 113, 86);
}

#info_site {
    grid-row: 4 / 6;
    grid-column: 6 / 9;
    align-self: center;

    font-style: italic;
} @media (max-width: 700px) {
    #info_site {
        display: none;
    }
}

#actions {
    grid-row: 3 / 5;
    grid-column: 8;
    display: flex;
    flex-direction: column;
} .button_action {
    align-self: center;
    margin: 15px;

    pointer-events:all;
    cursor: pointer;
    user-select: none;
} @media (max-width: 700px) {
    #actions {
        display: none;
 }
}


#info_hex {
    grid-row: 5;
    grid-column: 1 / 9;

    user-select: none;
}

</style>
