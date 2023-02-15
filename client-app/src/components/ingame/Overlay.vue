<script setup>
import { ref, onMounted } from 'vue'
import { useUIStore } from '../../stores/UIStore.js'
import { useWorldStore } from '../../stores/WorldStore.js'
import { useGameAssetStore } from '../../stores/GameAssetStore'
import { requestWorldSave } from '../../services/EditorService'
import { Ambience, Sites, Sites3d } from '../../stores/000Singletons'
import { useAuthStore } from '../../stores/AuthStore'
import { usePartyStore } from '../../stores/PartyStore'
import { loadSitePreview } from '../../threejs/ActorManager'
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader'

    const uiStore = useUIStore()
    const authStore = useAuthStore()
    const worldStore = useWorldStore()
    const gameAssetStore = useGameAssetStore()
    const partyStore = usePartyStore()
    const sites = new Sites()
    const sites3d = new Sites3d()
    const ambience = new Ambience()
    const loader = new GLTFLoader()

    const portrait = ref(null)
    const slot_1 = ref(null);
    const slot_2 = ref(null);
    const slot_3 = ref(null);
    const slot_4 = ref(null);
    const slot_5 = ref(null);
    const slot_6 = ref(null);
    const slot_7 = ref(null);
    
    const slots = [slot_1, slot_2, slot_3, slot_4, slot_5, slot_6, slot_7]
    let slotPressed
    let selected = [];
    function clickSlot(num) {
        if (uiStore.editorMode) {
            if (selected.length != 0) {
                selected[0].value.style.borderColor = "rgba(133, 113, 86, 0)"
                // selected[0].value.style.borderStyle = "groove"
                selected.pop()
            }
            selected.push(slots[num])
            slots[num].value.style.borderColor = "rgb(252, 205, 143)"
            worldStore.previewModelURI = gameAssetStore.modelURIs[num]
            loadSitePreview(loader, worldStore, gameAssetStore)
        }
        else {
            if (selected.includes(num)) {
                slots[num].value.style.borderColor = "rgba(133, 113, 86, 0)"
                // selected[0].value.style.borderStyle = "groove"
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

    function debug1() {
        console.log(sites.buffer)
    }
    function debug2() {
        console.log(sites3d.buffer)
    }
    function muteMusic() {
        ambience.music.mute(true)
    }
    function unmuteMusic() {
        ambience.music.mute(false)
    }
    
    onMounted(() => {
        console.log("Mounting overlay...")

        try {
            if (partyStore.avatar.name === "Eliana Dawnbreak")
                portrait.value.style.backgroundImage = "url('assets/Portrait_Eliana.jpg')"
            if (partyStore.avatar.name === "Leito Froste")
                portrait.value.style.backgroundImage = "url('assets/Portrait_Leito.jpg')"
            if (partyStore.avatar.name === "Marsilio Mirandola")
                portrait.value.style.backgroundImage = "url('assets/Portrait_Marsilio.jpg')"
        } catch (e) {}

        for (let slot of slots) {
            slot.value.addEventListener('pointerdown', (e) => {
                if (e.button === 0 && uiStore.editorMode) {
                    gameAssetStore.pointerDownSound.play()
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
    <div id="overlay"  @contextmenu.prevent="">
        <div id="clock">{{uiStore.currentTime}}</div>
        <div id="slot_panel">
            <div id="portrait" ref="portrait" v-if="!uiStore.editorMode"></div>
            <div class="slot" ref="slot_1">
                <span class="slot_text1" v-if="uiStore.editorMode">
                    Wald
                </span>                
                <span class="slot_text2" v-if="!uiStore.editorMode">
                    {{1}}
                </span>
            </div>
            <div class="slot" ref="slot_2">
                <span class="slot_text1" v-if="uiStore.editorMode">
                    Banner
                </span>
                <span class="slot_text2" v-if="!uiStore.editorMode">
                    {{2}}
                </span>
            </div>
            <div class="slot" ref="slot_3">
                <span class="slot_text1" v-if="uiStore.editorMode">
                    Haus
                </span>
                <span class="slot_text2" v-if="!uiStore.editorMode">
                    {{3}}
                </span>
            </div>
            <div class="slot" ref="slot_4">
                <span class="slot_text1" v-if="uiStore.editorMode">
                    Zelt
                </span>
                <span class="slot_text2" v-if="!uiStore.editorMode">
                    {{4}}
                </span>
            </div>
            <div class="slot" ref="slot_5">
                <span class="slot_text1" v-if="uiStore.editorMode">
                    Kristall
                </span>
                <span class="slot_text2" v-if="!uiStore.editorMode">
                    {{5}}
                </span>
            </div>
            <div class="slot" ref="slot_6">
                <span class="slot_text1" v-if="uiStore.editorMode">
                    Truhe
                </span>
                <span class="slot_text2" v-if="!uiStore.editorMode">
                    {{6}}
                </span>
            </div>
            <div class="slot" ref="slot_7">
                <span class="slot_text1" v-if="uiStore.editorMode">
                    Baum
                </span>
                <span class="slot_text2" v-if="!uiStore.editorMode">
                    {{7}}
                </span>
            </div>
        </div>
        <p class="slot_panel_info" v-if="uiStore.editorMode">Slot klicken, um eine platzierbare Stätte zu wählen.</p>
        <div class="debug_panel" ref="draggableElement">
            *debug panel*
            <button class="debug" @click="muteMusic">♪ Mute Music ♪</button>
            <button class="debug" @click="unmuteMusic">♪ Unmute ♪</button>
            <button class="debug" @click="debug1">debug sites</button>
            <button class="debug" @click="debug2">debug sites3d</button>
            <button class="debug inactive" @click="">Post Layout</button>
            <!-- <button class="debug" @click="requestWorldSave(authStore)">Post Layout</button> -->
        </div>
        <div id="info_hex">
            <div class="axial" v-if="worldStore.hoveredItem">
                <label class="label_axial">
                    Q: 
                </label>
                <span class="span_axial">{{worldStore.hoveredItem.userData.Q}}</span>
                <label class="label_axial">
                    R: 
                </label>
                <span class="span_axial">{{worldStore.hoveredItem.userData.R}}</span>
            </div>
            <p>Ziehen Sie die rechte Maustaste, um die Kamera zu bewegen.</p>
        </div>
    </div>
</template>

<style scoped>
p {
    font-size: .8rem;
    user-select: none;
}
#overlay {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 1fr 1fr 1fr 1fr 1fr;
    grid-template-rows: 3fr 7fr 12fr 7fr 1fr ;
    width: 60rem;
    max-width: 100%;
}
#clock {
    padding-top: 4vh;
    grid-row: 1;
    grid-column: 4/6;
    align-self:flex-start;
    justify-self: center;

    font-family: monospace;
    user-select: none;
}
.slot_panel_info {
    position: absolute;
    bottom: 0%;
    /* left: 2vw; */
}
#slot_panel{
    display: flex;
    height: 130px;
    position: fixed;
    bottom: 5vh;
    /* left: 2vw; */
    right: auto;
    font-style: italic;
    background-image: url('leather_texture.jpg');
    border-style: outset;
    border-width: 1px;
    /* border-top-right-radius: 5px; */
    border-color: rgb(133, 113, 86);
} #portrait {
    /* background-image: url('Portrait_Eliana.jpg'); */
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

    /* cursor: pointer; */
    pointer-events: all;
    user-select: none;

    border-style: groove;
    border-width: 1px;
    border-color: rgba(133, 113, 86, 0);
    /* border-top-right-radius: 6px; */
    
    box-shadow: 0px 0px 7px -3px rgba(133, 113, 86, 0.551) inset;

    /* transition: border-color 0.15s; */

} .slot:active {
    /* border-style: inset; */
    /* box-shadow: 10px 14px 16px -4px black inset; */
    /* margin-top: 2px; */
}.slot_text1{
    display: flex;
    margin: auto;
    margin-left: 3%;
    margin-bottom: 0;
    padding: 3px;
    padding-right: 6px;
    align-self: flex-end;
    /* color: white; */
}.slot_text2{
    display: flex;
    margin: auto;
    margin-right: 0;
    margin-bottom: 0;
    padding: 3px;
    padding-right: 6px;
    align-self: flex-end;
    /* color: white; */
}
.debug_panel {
    grid-row: 3 / 5;
    grid-column: 8;
    display: flex;
    flex-direction: column;
    position: absolute;

    font-size: .8rem;
    color: rgba(252, 205, 143, .5);

    pointer-events: all;
    user-select: none;
    cursor: grab;

    background-color: rgba(0, 0, 0, 0.852);
    border-top-style: dotted;
    border-width: 1px;
    border-color: 1px;
    border-color: rgb(252, 205, 143);
} .debug_panel:active {
    cursor: grabbing;
}
 .debug {
    display: flex;
    align-self: center;
    justify-content: center;

    font-size: 1rem;
    cursor: pointer;
} .inactive {
    text-decoration: line-through;
}


#info_hex {
    position: absolute;
    bottom: 0%;
    right: 0%;
    display: flex;
    flex-direction: column;
    user-select: none;
} .axial {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 1fr;
    position: absolute;
    right: 0%;
    bottom: 5vh;
    font-size: 1rem;
    line-height: 1.2em;
    width: 50%;
    text-align: right;
} .label_axial {
    /* padding-left: 3rem; */
}
.span_axial {
    font-family: monospace;
}

</style>
