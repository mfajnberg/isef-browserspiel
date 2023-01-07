<script setup>
import { ref, onMounted } from 'vue'
import { useAuthStore } from '../../stores/AuthStore.js'

import RegistrationForm from './RegisForm.vue'
import LoginForm from './LoginForm.vue'

const AuthStore = useAuthStore()

const draggableElement = ref(null)
var isDragging = false
var currentX = 0
var currentY = 0
var initialX = 0
var initialY = 0
var xOffset = 0
var yOffset = 0

function dragStart(e) {
    initialX = e.target.clientX - xOffset;
    initialY = e.target.clientY - yOffset;
    if (e.target === draggableElement.value) {
        isDragging = true;
    }
    console.log("drag start")
}

function dragEnd(e) {
    initialX = currentX;
    initialY = currentY;

    isDragging = false;
    console.log("stopping drag")
}

function drag(e) {
    if (isDragging) {
        currentX = e.clientX - initialX;
        currentY = e.clientY - initialY;
        console.log(e.clientX)
        console.log(e.clientY)

        xOffset = currentX;
        yOffset = currentY;

        setTranslate(currentX, currentY, draggableElement);
        console.log("dragging")
    }
}

function setTranslate(xPos, yPos, el) {
    el.value.style.transform.translate = "translate(" + xPos + "px, " + yPos + "px)";
}

onMounted(() => {
    draggableElement.value.addEventListener("mousedown", dragStart)
    draggableElement.value.addEventListener("mouseup", dragEnd)
    draggableElement.value.addEventListener("mousemove", drag)
})

</script>

<template>
    <div id="content" ref="draggableElement">
        <RegistrationForm v-if="AuthStore.showingRegisForm" />
        <LoginForm v-if="AuthStore.showingLoginForm" />
    </div>
</template>

<style scoped>
#content {
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: column;
    padding: 3em;
    background-color: rgba(0, 0, 0, 0.832);

    position: absolute;
    z-index: 19;
    border-radius: 6px;
}

@media (max-width: 700px) {
    #content {
        width: 100%;
    }
}
</style>
