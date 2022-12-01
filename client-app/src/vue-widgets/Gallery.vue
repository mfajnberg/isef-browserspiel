<script setup>
import { useAuthUIStore } from '../stores/AuthUIStore.js'

    const AuthUI = useAuthUIStore()
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
        background-color: rgba(0, 0, 0, 0.851);
        z-index: 1;
    }
</style>
