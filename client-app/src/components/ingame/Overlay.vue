<script setup>
import { ref, onMounted } from 'vue';
import { useUIStore } from '../../stores/UIStore.js'
import { useWorldStore } from '../../stores/WorldStore.js'
import { useGameAssetStore } from '../../stores/GameAssetStore';

    const uiStore = useUIStore()
    const worldStore = useWorldStore()
    const assetStore = useGameAssetStore()

    const cursorURLs = ['tree_cluster_fir_1.glb', 'tower_medieval.glb', 'crystals.glb', 'crystals.glb', 'crystals.glb', 'crystals.glb', 'crystals.glb']

    function playClickSoundPointerDown() {
        
        assetStore.pointerDownSound.play()
    }
    function playClickSoundPointerUp() {
        assetStore.pointerUpSound.play()
    }
    
    const slot_1 = ref(null);
    const slot_2 = ref(null);
    const slot_3 = ref(null);
    const slot_4 = ref(null);
    const slot_5 = ref(null);
    const slot_6 = ref(null);
    const slot_7 = ref(null);    
    const action_1 = ref(null);
    const action_2 = ref(null);
    const action_3 = ref(null);
    const action_4 = ref(null);

    const slots = [slot_1, slot_2, slot_3, slot_4, slot_5, slot_6, slot_7];
    const actions = [action_1, action_2, action_3, action_4];

    let selected = [];
    function clickSlot(num) {
        if (uiStore.editorMode) {
            worldStore.changedCursorURL = true
            worldStore.cursorURL = cursorURLs[num]
        }
        if (selected.includes(num)) {
            slots[num].value.style.borderColor = "rgba(133, 113, 86, 0)";
            for (var i = 0; i < selected.length; i++) {
                if (selected[i] === num) {
                    selected.splice(i, 1);
                }
            }
        }
        else {
            selected.push(num)
            slots[num].value.style.borderColor = "rgb(252, 205, 143)";
        }
        selectedPrevious = num
    }

    let slotPressed, actionPressed
    onMounted(() => {
        for (let slot of slots) {
            slot.value.addEventListener('pointerdown', (e) => {
                if (e.button === 0) {
                    assetStore.pointerDownSound.play()
                    slotPressed = slot
                    clickSlot(slots.indexOf(slot))
                }
            })
            slot.value.addEventListener('pointerup', (e) => {
                if (e.button === 0 && slotPressed === slot) {
                    assetStore.pointerUpSound.play()
                }
            })
        }
        for (let action of actions) {
            action.value.addEventListener('pointerdown', (e) => {
                if (e.button === 0) {
                    assetStore.pointerDownSound.play()
                    actionPressed = action
                }
            })
            action.value.addEventListener('pointerup', (e) => {
                if (e.button === 0 && actionPressed === action) {
                    assetStore.pointerUpSound.play()
                }
            })
        }
    })

</script>


<template>
    <div id="overlay">
        <!--top_left_info-->
        <div id="clock">{{uiStore.currentTime}}</div>
        <!--top_right_info-->
        <div id="slot_panel">
            <div id="portrait" src="Portrait_Eliana.jpg"></div>
            <div class="slot" ref="slot_1">slot 1</div>
            <div class="slot" ref="slot_2">slot 2</div>
            <div class="slot" ref="slot_3">slot 3</div>
            <div class="slot" ref="slot_4">slot 4</div>
            <div class="slot" ref="slot_5">slot 5</div>
            <div class="slot" ref="slot_6">slot 6</div>
            <div class="slot" ref="slot_7">slot 7</div>
        </div>
        <div id="info_site">(site info)</div>
        <div id="action_panel">
            <div class="action" ref="action_1">action 1</div>
            <div class="action" ref="action_2">action 2</div>
            <div class="action" ref="action_3">action 3</div>
            <div class="action" ref="action_4">action 4</div>
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


#slot_panel{
    grid-row: 4;
    grid-column: 1/4;
    display: flex;
    height: 130px;
    background-image: url('leather_texture.jpg');
    border-style: solid;
    border-width: 1px;
    border-color: rgb(133, 113, 86);
} #portrait {
    background-image: url('Portrait_Eliana.jpg');
    background-size: cover;
    background-repeat: no-repeat;
    background-position: center center;

    align-self: center;
    width: 100px;
    height: 130px;

    user-select: none;
    pointer-events: all;
    cursor: pointer;
} .slot {
    align-self: center;
    margin: 1px;
    width: 100px;
    height: 130px;

    cursor: pointer;
    pointer-events: all;
    user-select: none;

    border-style:solid;
    border-width: 1px;
    border-color: rgba(133, 113, 86, 0);
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

#action_panel {
    grid-row: 3 / 5;
    grid-column: 8;
    display: flex;
    flex-direction: column;
} .action {
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
