<script setup>
import { useAvatarCreationStore } from '../../stores/AvatarCreationStore';

import Preview from './Preview.vue';
import Options from './Options.vue';
import Estate from './Estate.vue';

    const store = useAvatarCreationStore()

</script>

<template>
    <div class="creator">
        <Preview/>
        <Options/>
        <Estate/>
    </div>
</template>

<style scoped>
    .creator {
        display: flex;
        width: 100%;
        height: 100%;
        align-items: center;
        justify-content: center;
        flex-direction: row;
        position: absolute;
        background-color: rgba(0, 0, 0, 0.851);
        z-index: 1;
    }
    
</style>
