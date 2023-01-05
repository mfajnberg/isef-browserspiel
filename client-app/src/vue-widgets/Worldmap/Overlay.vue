<script setup>
import { useUIStore } from '../../stores/UIStore.js'
import { useWorldmapStore } from '../../stores/WorldmapStore.js'

    const UIStore = useUIStore()
    const WorldmapStore = useWorldmapStore
</script>


<template>
    <div id="">
        <div id="clock">
            {{UIStore.currentTime}}
        </div>
    </div>
</template>


<style scoped>

#clock {
    position: relative;
    top: 5%;
    font-family: monospace;
    pointer-events: all;
}

</style>
