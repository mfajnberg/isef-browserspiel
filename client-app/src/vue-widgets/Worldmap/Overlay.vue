<script setup>
import { useUIStore } from '../../stores/UIStore.js'

    const UIStore = useUIStore()
</script>


<template>
    <div>
        <div id="clock">
            {{UIStore.currentTime}}
        </div>
        <button class="button_debug" @click="">
            debug fn1
        </button>
        <button class="button_debug" @click="">
            debug fn2
        </button>
        <button class="button_debug" @click="">
            debug fn3
        </button>
        <button class="button_debug" @click="">
            debug fn4
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
