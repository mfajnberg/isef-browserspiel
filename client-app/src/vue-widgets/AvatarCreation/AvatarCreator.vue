<script setup>
import { useCreateAvatarStore } from '../../stores/CreateAvatarStore';

    const store = useCreateAvatarStore()

</script>

<template>
    <div class="creator">
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
