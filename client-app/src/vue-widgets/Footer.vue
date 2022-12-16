<script setup>

</script>

<template>
    <div class="footer">
        <a class="item">v0.1a</a> |
        <a class="item">About</a> |
        <a class="item">FAQs</a> |
        <a class="item">AGBs</a> |
        <a class="item">Impressum</a>
    </div>
</template>

<style scoped>
    .footer {
        display: flex;
        width: 100vw;
        height: 5vh;
        background-color: black;
        position: fixed;
        left: 0%;
        bottom: 0%;
        justify-content: center;
        align-items: center;
        white-space: pre-wrap;
    }
    .item {
        color: grey;
        cursor: pointer;
    }
</style>
