<script setup>
import { useUIStore } from '../../stores/UIStore.js'
import { useWorldmapStore } from '../../stores/WorldmapStore.js'

    const UIStore = useUIStore()
    const worldmapStore = useWorldmapStore()
</script>


<template>
    <div id="overlay">
        <!--top_left_info-->
        <div id="clock">{{UIStore.currentTime}}</div>
        <!--top_right_info-->
        <div id="avatar"></div>
        <div id="slots">
            <div class="slot">slot-1-data</div>
            <div class="slot">slot-2-data</div>
            <div class="slot">slot-3-data</div>
            <div class="slot">slot-4-data</div>
            <div class="slot">slot-5-data</div>
            <div class="slot">slot-6-data</div>
            <div class="slot">slot-7-data</div>
        </div>
        <div id="info_site">site info</div>
        <button class="b_party_action"></button>
        <button class="b_party_action"></button>
        <button class="b_party_action"></button>
        <button class="b_party_action"></button>
        <div id="info_hex">{{worldmapStore.hoveredItemName}}</div>
    </div>
</template>


<style scoped>

#overlay {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 1fr;
    grid-template-rows: 3fr 7fr 4fr 1fr ;
}

#clock {
    position: absolute;
    align-self:flex-start;
    justify-self:center;
    font-family: monospace;
    pointer-events: all;
    user-select: none;
}

#info_hex {
    grid-row: 4;
    grid-column: 2 / 4;
    user-select: none;
}
#slots{
    grid-row: 3;
    grid-column: 2;
}
#info_site {
    grid-row: 3;
    grid-column: 3;
}
.b_party_action {
    grid-row: 3;
    grid-column: 4;
}
.button_debug{
    position: relative;
    top: 20vh;
    pointer-events: all;
}


</style>
