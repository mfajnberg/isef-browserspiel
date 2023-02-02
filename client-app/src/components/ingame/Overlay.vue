<script setup>
import { ref, onMounted } from 'vue'
import { useUIStore } from '../../stores/UIStore.js'
import { useWorldStore } from '../../stores/WorldStore.js'
import { useGameAssetStore } from '../../stores/GameAssetStore'
import { requestWorldSave } from '../../services/EditorService'

    const uiStore = useUIStore()
    const worldStore = useWorldStore()
    const assetStore = useGameAssetStore()

    const previewModelUrls = 
        ['forest_1.glb', 'cliffs.glb', 'house.glb', 
        'tent_field_camp.glb', 'crystals.glb', 
        'chest_lp.glb', 'crystals.glb']
    
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



    const slots = [slot_1, slot_2, slot_3, slot_4, slot_5, slot_6, slot_7]
    const actions = [action_1, action_2, action_3, action_4]

    let selected = [];
    function clickSlot(num) {
        if (uiStore.editorMode) {
            if (selected.length != 0) {
                selected[0].value.style.borderColor = "rgba(133, 113, 86, 100)"
                selected[0].value.style.borderStyle = "groove"
                selected.pop()
            }
            selected.push(slots[num])
            slots[num].value.style.borderColor = "rgb(252, 205, 143)"
            worldStore.changedPreviewURL = true
            worldStore.previewUrl = previewModelUrls[num]
        }
        else {
            if (selected.includes(num)) {
                slots[num].value.style.borderColor = "rgba(133, 113, 86, 100)"
                for (var i = 0; i < selected.length; i++) {
                    if (selected[i] === num) {
                        selected.splice(i, 1)
            }   }   }
            else {
                selected.push(num)
                slots[num].value.style.borderColor = "rgb(252, 205, 143)"
            }
        }
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
        }
        document.addEventListener('pointerup', (e) => {
            if (e.button === 0) {
                try {
                    slotPressed.value.style.borderStyle = "outset"
                }
                catch (e) {}
            }
        })
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

    function debug2() {
        console.log(worldStore.sitesBuffer)
    }
</script>


<template>
    <div id="overlay">
        <!--top_left_info-->
        <div id="clock">{{uiStore.currentTime}}</div>
        <!--top_right_info-->
        <div id="slot_panel">
            <div id="portrait" src="Portrait_Eliana.jpg"></div>
            <div class="slot" ref="slot_1">
                <span class="slot_text">
                    {{1}}
                </span>
            </div>
            <div class="slot" ref="slot_2">
                <span class="slot_text">
                    {{2}}
                </span>
            </div>
            <div class="slot" ref="slot_3">
                <span class="slot_text">
                    {{3}}
                </span>
            </div>
            <div class="slot" ref="slot_4">
                <span class="slot_text">
                    {{4}}
                </span>
            </div>
            <div class="slot" ref="slot_5">
                <span class="slot_text">
                    {{5}}
                </span>
            </div>
            <div class="slot" ref="slot_6">
                <span class="slot_text">
                    {{6}}
                </span>
            </div>
            <div class="slot" ref="slot_7">
                <span class="slot_text">
                    {{7}}
                </span>
            </div>
        </div>
        <div id="action_panel">
            <button class="action" ref="action_1" @click="requestWorldSave(worldStore)">Post Layout</button>
            <button class="action" ref="action_2" @click="debug2">(action 2)</button>
            <button class="action" ref="action_3">(action 3)</button>
            <button class="action" ref="action_4">(action 4)</button>
        </div>
        <div id="info_hex">{{worldStore.getHoveredName}}</div>
    </div>
</template>


<style scoped>

#overlay {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 1fr 1fr 1fr 1fr 1fr;
    grid-template-rows: 3fr 7fr 12fr 7fr 1fr ;
    width: 100%;
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
    display: flex;
    height: 130px;
    position: fixed;
    bottom: 5vh;
    left: 0%;
    right: auto;
    font-style: italic;
    background-image: url('leather_texture.jpg');
    border-style: groove;
    border-width: 1px;
    /* border-top-right-radius: 5px; */
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
    display:flex;
    align-self: center;
    /* margin: -.5px; */
    width: 100px;
    height: 130px;

    cursor: pointer;
    pointer-events: all;
    user-select: none;

    border-style: groove;
    border-width: 1px;
    border-color: rgba(133, 113, 86, 100);
    /* border-top-right-radius: 6px; */
    
    box-shadow: 0px 0px 7px -3px rgba(133, 113, 86, 0.551) inset;

    /* transition: border-color 0.15s; */

} .slot:active {
    border-style: inset;
    /* box-shadow: 10px 14px 16px -4px black inset; */
    margin-top: 2px;
}.slot_text{
    display: flex;
    margin: auto;
    margin-right: 0;
    margin-bottom: 0;
    padding: 3px;
    padding-right: 6px;
    align-self: flex-end;
    /* color: white; */
}
#action_panel {
    grid-row: 3 / 5;
    grid-column: 8;
    display: flex;
    flex-direction: column;
} .action {
    display: flex; /* for now */
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
    position: fixed;
    right: 0%;
    bottom: 5vh;

    user-select: none;
}

</style>
