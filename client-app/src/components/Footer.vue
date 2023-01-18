<script setup>
import { useUIStore } from '../stores/UIStore.js'

const UIStore = useUIStore()

</script>

<template>
    <div class="footer">
        <a class="item">v0.1a</a> |
        <a class="item">About</a> |
        <a class="item">FAQs</a> |
        <a class="item">AGBs</a> |
        <a class="item" @click="UIStore.showImprint"><strong>Impressum</strong></a>
    </div>
</template>

<style scoped>
    .footer {
        display: flex;
        justify-content: center;
        align-items: center;
        white-space: pre-wrap;
        height: 5vh;
        width: 100vw;

        position: absolute;
        left: 0%;
        bottom: 0%;
        
        font-size: 14px;

        background-color: rgb(22, 22, 22);
    }
    .item {
        color: grey;
        cursor: pointer;
    }
</style>
