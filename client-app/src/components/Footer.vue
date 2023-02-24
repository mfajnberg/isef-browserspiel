<script setup>
import { useUIStore } from '../stores/UIStore.js'

const uiStore = useUIStore()

function clickImprint() {
    if (!uiStore.showingImprint) {
        uiStore.showImprint()
    } else {
        uiStore.showHome()
    }
}

</script>

<template>
    <div class="footer" v-show="!uiStore.getShowingAvatarCreator">
        <div class="items">
        <!-- <div > -->
            <span class="item version"><span class="number">v0.1</span> pre-alpha</span> <span class="separator">|</span>
            <a class="item">About</a> <span class="separator">|</span>
            <a class="item">FAQs</a> <span class="separator">|</span>
            <a class="item">AGBs</a> <span class="separator">|</span>
            <a class="item implemented" @click="clickImprint"><strong>Impressum</strong></a>
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

        position: fixed;
        left: 0%;
        bottom: 0%;
        
        font-size: 14px;

        background-color: rgb(0, 0, 0);
        border-top-style: outset;
        border-top-width: 1px;
        border-top-color: rgba(133, 113, 86, 0.551);
        user-select: none;
        transition: 0.25s;
    }
    .items {
        width: 100%;
    }
    @media (max-width: 800px) {
        .footer{
            /* bottom: -5%; */
        }
    }
    .item {
        color: rgb(66, 66, 66);
        transition: 1s;
    }.separator{
        color: rgba(133, 113, 86, 0);
        padding-left: 8px;
        padding-right: 8px;
    } 
    .implemented{
        cursor: pointer;
        color: rgb(106, 106, 106);
        border-style: double;
        border-width: 3px;
        border-color: transparent;
        transition: .1s;
        /* text-decoration: underline; */
    } .implemented:hover {
        border-bottom-color: white;
        transition: .2s;
    }
    .version {
        font-family: 'fondamento';
        color: rgb(133, 113, 86);
    } .number {
        font-family: monospace;
    }

    .items:hover > .implemented {
        color: white;
        /* letter-spacing: 1px; */
    }
</style>
