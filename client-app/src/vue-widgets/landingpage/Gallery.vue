<script setup>
import { useStore } from '../../stores/ReactiveStore.js'

    const store = useStore()
</script>

<template>
    <div class="gallery"></div>
</template>

<style scoped>
    .gallery {
        display: flex;
        width: 50vw;
        height: 100%;
        align-items: center;
        justify-content: center;
        flex-direction: column;
        position: absolute;
        background-color: rgb(62, 62, 62);
        z-index: 0;
    }
</style>
