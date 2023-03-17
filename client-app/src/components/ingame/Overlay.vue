<script setup>
import { ref, onMounted, nextTick } from 'vue'
import { useUIStore } from '../../stores/UIStore.js'
import { useWorldStore } from '../../stores/WorldStore.js'
import { useGameAssetStore } from '../../stores/GameAssetStore'
import { requestWorldSave } from '../../services/EditorService'
import { Ambience,TileDTOs, Sites3d } from '../../stores/000Singletons'
import { useAuthStore } from '../../stores/AuthStore'
import { usePartyStore } from '../../stores/PartyStore'
import { dispose, loadSitePreview, loadPawn3d } from '../../threejs/ActorManager'
import { HexVector } from '../../classes/HexVector'

const uiStore = useUIStore()
const authStore = useAuthStore()
const worldStore = useWorldStore()
const gameAssetStore = useGameAssetStore()
const partyStore = usePartyStore()
const sites = new TileDTOs()
const sites3d = new Sites3d()
const ambience = new Ambience()

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
            selected.pop()
        }
        selected.push(slots[num])
        slots[num].value.style.borderColor = "rgb(252, 205, 143)"
        worldStore.previewModelURI = gameAssetStore.modelURIs[num]
        loadSitePreview(worldStore)
    }
    else {
        if (selected.includes(num)) {
            slots[num].value.style.borderColor = "rgba(133, 113, 86, 0)"
            for (var i = 0; i < selected.length; i++) {
                if (selected[i] === num) {
                    selected.splice(i, 1)
                }
            }
        }
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
function fixCamera() {
    if (uiStore.editorMode) {
        worldStore.setCameraPosition(
            worldStore.editingAt.getWorldXFromAxialQ(),
            worldStore.editingAt.getWorldZFromAxialR()
        )
        worldStore.orbit.update()
    }
    else {
        const partyVec = new HexVector(partyStore.party.location.Q, partyStore.party.location.R)
        worldStore.setCameraPosition(
            partyVec.getWorldXFromAxialQ(),
            partyVec.getWorldZFromAxialR()
        )
        worldStore.orbit.update()
    }
    console.log()
}

function toggleMusic() {
    if (ambience.music.mute())
        ambience.music.mute(false)
    else {
        ambience.music.mute(true)
    }
}

function toggleSmoothCam() {
    if (worldStore.orbit.enableDamping)
        worldStore.orbit.enableDamping = false
    else {
        worldStore.orbit.enableDamping = true
    }
}

function postLayout() {
    if (uiStore.editorMode)
        requestWorldSave(authStore)
}

function cancelPlacement() {
    if (worldStore.previewModelURI != "HexPreview2.glb" 
        && !worldStore.orbit.isUserInteracting) {
            try {
                dispose(worldStore.preview)
            } catch (error) { 
                console.log(error) 
            }

            worldStore.previewModelURI = "HexPreview2.glb"
            loadSitePreview(worldStore)
            if (selected.length != 0) {
                selected[0].value.style.borderColor = "rgba(133, 113, 86, 0)"
                selected.pop()
            }
            worldStore.preview.visible = false
    }
}

async function setupComponent() {
    if (!uiStore.devMode) {} else {
        partyStore.avatar = { name: "Leito Froste" }
    }
    try {
        if (partyStore.avatar.name === "Eliana Dawnbreak")
            portrait.value.style.backgroundImage = "url('Portrait_Eliana.jpg')"
        if (partyStore.avatar.name === "Leito Froste")
            portrait.value.style.backgroundImage = "url('Portrait_Leito.jpg')"
        if (partyStore.avatar.name === "Marsilio Mirandola")
            portrait.value.style.backgroundImage = "url('Portrait_Marsilio.jpg')"
    } catch (e) {
        console.log(e)
    }

    if (uiStore.editorMode) {
        document.addEventListener('contextmenu', () => { 
            // cancelPlacement() 
        })
        document.addEventListener('keyup', (event) => {
            const key = event.key
            if (key == "Escape" || key == "Backspace" || key == "Delete")
                cancelPlacement()
        })

        for (let slot of slots) {
            slot.value.addEventListener('pointerdown', (e) => {
                if (e.button === 0 && uiStore.editorMode) {
                    gameAssetStore.pointerDownSound.play()
                    slotPressed = slot
                    clickSlot(slots.indexOf(slot))
                }
            })
        }
    }
    else {
        let partyPos = new HexVector(partyStore.party.location.Q, partyStore.party.location.R)
        let partyPosX = partyPos.getWorldXFromAxialQ()
        let partyPosZ = partyPos.getWorldZFromAxialR()
        if (!partyStore.pawn3d)
            loadPawn3d()
        else {
            partyStore.pawn3d.position.x = partyPosX
            partyStore.pawn3d.position.z = partyPosZ
        }
    }   
    }
onMounted(() => {
    console.log("Mounting overlay...")
    setupComponent()

    document.addEventListener("mousedown", dragStart)
    document.addEventListener("mouseup", dragEnd)
    document.addEventListener("mousemove", drag)
    document.addEventListener("mouseleave", dragEnd)

    document.addEventListener("mousemove", moveDynInfoBox)
    fixCamera()
})

const dynInfoBoxRef = ref(null)

const debugPanelRef = ref(null)
var isDragging = false
var currentX = 0
var currentY = 0
var initialX = 0
var initialY = 0
var xOffset = 0
var yOffset = 0

function dragStart(event) {
    if (event.target === debugPanelRef.value) {
        initialX = event.clientX - xOffset
        initialY = event.clientY - yOffset
        isDragging = true
    }
}
function dragEnd() {
    initialX = currentX
    initialY = currentY
    isDragging = false
}
function drag(event) {
    if (isDragging) {
        currentX = event.clientX - initialX
        currentY = event.clientY - initialY
        xOffset = currentX
        yOffset = currentY
        setTranslate(currentX, currentY, debugPanelRef)
    }
}
function setTranslate(xPos, yPos, elementRef) {
    elementRef.value.style.translate = `${xPos}px ${yPos}px`
}

function moveDynInfoBox(event) {
    if (!uiStore.rightClick) {
        try {
            dynInfoBoxRef.value.style.left = event.clientX + 'px'
            dynInfoBoxRef.value.style.top = event.clientY + 'px'
        } catch {}
    }
}

</script>


<template>
    <div id="overlay" @contextmenu.prevent="">
        <div class="clock">
            <span>
                {{ uiStore.currentTime }}
            </span>
            <span class="countdown" v-if="partyStore.traveling">
                {{ uiStore.displayNextUpdateTime }}
            </span>
        </div>

        <div id="slot_panel" 
            @mouseover="uiStore.hoveringOverlay = true" 
            @mouseleave="uiStore.hoveringOverlay = false">
            <div ref="portrait" class="slot portrait" v-show="!uiStore.editorMode"></div>
            <div ref="slot_1" class="slot">
                <span class="slot_text1" v-show="uiStore.editorMode">
                    Wald
                </span>
                <span class="slot_text2" v-show="!uiStore.editorMode">
                    {{ 1 }}
                </span>
            </div>
            <div ref="slot_2" class="slot">
                <span class="slot_text1" v-show="uiStore.editorMode">
                    Banner
                </span>
                <span class="slot_text2" v-show="!uiStore.editorMode">
                    {{ 2 }}
                </span>
            </div>
            <div ref="slot_3" class="slot">
                <span class="slot_text1" v-show="uiStore.editorMode">
                    Haus
                </span>
                <span class="slot_text2" v-show="!uiStore.editorMode">
                    {{ 3 }}
                </span>
            </div>
            <div ref="slot_4" class="slot">
                <span class="slot_text1" v-show="uiStore.editorMode">
                    Zelt
                </span>
                <span class="slot_text2" v-show="!uiStore.editorMode">
                    {{ 4 }}
                </span>
            </div>
            <div ref="slot_5" class="slot">
                <span class="slot_text1" v-show="uiStore.editorMode">
                    Kristall
                </span>
                <span class="slot_text2" v-show="!uiStore.editorMode">
                    {{ 5 }}
                </span>
            </div>
            <div ref="slot_6" class="slot">
                <span class="slot_text1" v-show="uiStore.editorMode">
                    Truhe
                </span>
                <span class="slot_text2" v-show="!uiStore.editorMode">
                    {{ 6 }}
                </span>
            </div>
            <div ref="slot_7" class="slot">
                <span class="slot_text1" v-show="uiStore.editorMode">
                    Baum
                </span>
                <span class="slot_text2" v-show="!uiStore.editorMode">
                    {{ 7 }}
                </span>
            </div>
        </div>
        <p class="resource_panel" v-if="!uiStore.editorMode">
            <span class="resource electrum"></span>
            <label>Elektrum: </label>
            <span class="strong number">{{ partyStore.getElectrum }}</span>
        </p>
        
        <p class="slot_panel_info" v-if="uiStore.editorMode">
            <span v-if="worldStore.previewModelURI === 'HexPreview2.glb'">
                Slot klicken, um ein Objekt zum Platzieren auszuwählen.
            </span>
            <span class="strong" v-if="worldStore.previewModelURI != 'HexPreview2.glb'">
                (Escape/Backspace/Delete zum abbrechen)
            </span>
        </p>
        <div ref="debugPanelRef" class="debug_panel" @mouseover="uiStore.hoveringOverlay = true"
            @mouseleave="uiStore.hoveringOverlay = false">
            ***
            <button class="debug" @click="toggleMusic">♪ Toggle Music ♪</button>
            <button class="debug" @click="toggleSmoothCam">Toggle Smooth Cam</button>
            <button class="debug" v-if="uiStore.editorMode" @click="debug1">debug tileDTOs</button>
            <button class="debug" v-if="uiStore.editorMode" @click="debug2">debug sites3d</button>
            <button class="debug" v-if="uiStore.editorMode" @click="fixCamera">debug camera</button>
            <button class="debug" v-if="uiStore.editorMode" @click="postLayout">Post Layout</button>
        </div>
        <div ref="dynInfoBoxRef" class="dyn_info_box"
            v-show="uiStore.rightClick && worldStore.clickedItem">
            {{uiStore.hoveredName}}
        </div>
        <div id="info_hex">
            <div class="axial" v-if="worldStore.hoveredItem">
                <label class="label_axial">
                    Q:
                </label>
                <span class="span_axial">{{ worldStore.hoveredItem.userData.Q }}</span>
                <label class="label_axial">
                    R:
                </label>
                <span class="span_axial">{{ worldStore.hoveredItem.userData.R }}</span>
            </div>
            <p>Halten und ziehen Sie die <span class="strong">mittlere Maustaste</span> zum bewegen der Kamera</p>
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
    grid-template-rows: 3fr 7fr 12fr 7fr 1fr;
    width: 60rem;
    max-width: 100%;
}

.clock {
    grid-row: 2;
    grid-column: 4/6;

    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    align-self: flex-start;
    justify-self: center;

    padding-top: 3vh;

    font-family: monospace;
    user-select: none;
}

.countdown {
    color: rgba(252, 205, 143, .5);
}

.slot_panel_info {
    position: absolute;
    bottom: 0%;
    /* left: 2vw; */
}

.resource_panel{
    display: flex;
    position: fixed;
    bottom: 15.5%
} .resource_panel > * {
    display: flex;
    align-self: center;
} .resource_panel > .number {
    padding-left: .5rem;
    font-family: monospace;
}.resource {
    height: 2.5rem;
    width: 3rem;
    padding-left: 0%;
    background-image: url("elektrum.png");
    background-repeat: no-repeat;
    background-position: center center;
    background-size: contain;
}

#slot_panel {
    display: flex;
    height: auto;
    position: fixed;
    bottom: calc(4vh + 1rem);

    /* left: 2vw; */
    right: auto;
    background-image: url("leather_texture.jpg");
    border-style: outset;
    border-width: 1px;
    /* border-top-right-radius: 5px; */
    border-color: rgb(133, 113, 86);
}

.portrait {
    /* background-image: url('Portrait_Eliana.jpg'); */
    background-size: cover;
    background-repeat: no-repeat;
    background-position: center center;
}

.slot {
    display: flex;
    align-self: center;
    /* margin: -.5px; */
    width: calc(6vh + 1rem);
    aspect-ratio: 5 / 7;
    /* cursor: pointer; */
    pointer-events: all;
    user-select: none;

    border-style: groove;
    border-width: 1px;
    border-color: rgba(133, 113, 86, 0);
    /* border-top-right-radius: 6px; */

    box-shadow: 0px 0px 7px -3px rgba(133, 113, 86, 0.551) inset;

    /* transition: border-color 0.15s; */

}

.slot:active {
    /* border-style: inset; */
    /* box-shadow: 10px 14px 16px -4px black inset; */
    /* margin-top: 2px; */
}

.slot_text1 {
    display: flex;
    margin: auto;
    margin-left: 3%;
    margin-bottom: 0;
    padding: 3px;
    padding-right: 6px;
    align-self: flex-end;
    font-family: monospace;
    font-size: .8rem;
    color: white;
}

.slot_text2 {
    display: flex;
    margin: auto;
    margin-right: 0;
    margin-bottom: 0;
    padding: 3px;
    padding-right: 6px;
    align-self: flex-end;
    color: rgba(252, 205, 143, .5);
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
    transition: 0s;
}

.debug_panel:active {
    cursor: grabbing;
    transition: 0s;
}

.debug {
    display: flex;
    align-self: center;
    justify-content: center;

    font-size: 1rem;
    cursor: pointer;
}

.inactive {
    text-decoration: line-through;
}


#info_hex {
    position: absolute;
    bottom: 0%;
    right: 0%;
    display: flex;
    flex-direction: column;
    user-select: none;
}

.axial {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 1fr;
    position: absolute;
    right: 0%;
    bottom: 5vh;
    font-size: 1rem;
    line-height: 1.2em;
    width: 50%;
    text-align: right;
}

.label_axial {
    color: rgba(252, 205, 143, .5);
}

.span_axial {
    font-family: monospace;
}

.dyn_info_box {
    padding-left: 2rem;
    padding-right: 2rem;
    padding-top: .2rem;
    padding-bottom: .2rem;
    position: fixed;
    color: white;
    text-shadow: 0rem 0rem .2rem black, 0rem 0rem .2rem black;

    background: linear-gradient(90deg, transparent 0%, black 50%, transparent 100%);
    border-style: double;
    border-color: transparent;
    border-top-color: rgba(252, 205, 143, 1);
    border-bottom-color: rgba(252, 205, 143, .5);
    transition: linear 0s;
}
</style>