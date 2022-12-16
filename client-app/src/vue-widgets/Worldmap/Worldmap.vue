<script setup>
import { useStore } from '../../stores/ReactiveStore';


    const store = useStore()

</script>

<template>
    <div class="worldmap">
    </div>
</template>

<style scoped>
    .worldmap {
        display: flex;
        width: 100vw;
        height: 75vh;
        align-items: center;
        justify-content: center;
        flex-direction: row;
        position: absolute;
        background-color: rgba(0, 0, 0, 0.851);
        z-index: 1;
    }
    
</style>
