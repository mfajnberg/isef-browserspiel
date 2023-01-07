<script setup>
import { useUIStore } from '../../stores/UIStore.js'
import { useGameAssetStore } from '../../stores/GameAssetStore.js'

    const UIStore = useUIStore()
    const assetStore = useGameAssetStore()
</script>


<template>
    <div>
        <div id="clock">
            {{UIStore.currentTime}}
        </div>
        <button class="button_debug" @click="assetStore.loadModel()">
            debug fn1
        </button>
    </div>
</template>


<style scoped>

#clock {
    position: relative;
    top: 5%;
    font-family: monospace;
    pointer-events: all;
    user-select: none;
}

.button_debug{
    position: relative;
    top: 20vh;
    pointer-events: all;
}

</style>
