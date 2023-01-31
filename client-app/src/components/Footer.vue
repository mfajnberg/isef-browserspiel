<script setup>
import { useUIStore } from '../stores/UIStore.js'

const uiStore = useUIStore()

function clickShowImprint() {
    uiStore.showImprint()
}

</script>

<template>
    <div class="footer">
        <div v-show="!uiStore.getShowingAvatarCreator">
        <!-- <div > -->
            <a class="item">v0.1a</a> <span class="separator">|</span>
            <a class="item">About</a> <span class="separator">|</span>
            <a class="item">FAQs</a> <span class="separator">|</span>
            <a class="item">AGBs</a> <span class="separator">|</span>
            <a class="item" @click="clickShowImprint"><strong>Impressum</strong></a>
        </div>
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

        background-color: rgb(0, 0, 0);
        border-top-style: solid;
        border-top-width: 1px;
        border-top-color: rgba(133, 113, 86, 0.551);
        user-select: none;
        transition: border-color 0.25s;
    }.footer:hover {
    border-color: rgb(252, 205, 143);
    }
    .item {
        color: rgb(53, 53, 53);
        cursor: pointer;
    }.separator{
        color: rgb(133, 113, 86);
        padding-left: 8px;
        padding-right: 8px;
    }
</style>
