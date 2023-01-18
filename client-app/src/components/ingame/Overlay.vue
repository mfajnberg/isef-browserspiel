<script setup>
import { useUIStore } from '../../stores/UIStore.js'
import { useWorldStore } from '../../stores/WorldStore.js'

    const UIStore = useUIStore()
    const worldStore = useWorldStore()

</script>


<template>
    <div id="overlay">
        <!--top_left_info-->
        <div id="clock">{{UIStore.currentTime}}</div>
        <!--top_right_info-->
        <div id="avatar"><img id="portrait" src="Portrait_Eliana.jpg"/></div>
        <div id="slots">
            <div class="b_slot">slot 1</div>
            <div class="b_slot">slot 2</div>
            <div class="b_slot">slot 3</div>
            <div class="b_slot">slot 4</div>
            <div class="b_slot">slot 5</div>
            <div class="b_slot">slot 6</div>
            <div class="b_slot">slot 7</div>
        </div>
        <div id="info_site">(site info)</div>
        <div id="actions">
            <div class="b_action">action 1</div>
            <div class="b_action">action 2</div>
            <div class="b_action">action 3</div>
            <div class="b_action">action 4</div>
        </div>
        <div id="info_hex">{{worldStore.hoveredItem.axial}}</div>
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
} .b_slot {
    align-self: center;
    margin: 1vw;

    cursor: pointer;
    pointer-events: all;
    user-select: none;
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
} .b_action {
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
