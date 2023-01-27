<script setup>
import { ref, onMounted } from 'vue';
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
    initialX = e.clientX - xOffset;
    initialY = e.clientY - yOffset;

    if (e.target === draggableElement.value) {
        isDragging = true;
    }
}

function dragEnd(e) {
    initialX = currentX;
    initialY = currentY;

    isDragging = false;
}

function drag(e) {
    if (isDragging) {
        currentX = e.clientX - initialX;
        currentY = e.clientY - initialY;

        xOffset = currentX;
        yOffset = currentY;

        setTranslate(currentX, currentY, draggableElement);
    }
}

function setTranslate(xPos, yPos, el) {
    el.value.style.translate = `${xPos}px ${yPos}px`
}

onMounted(() => {
    draggableElement.value.addEventListener("mousedown", dragStart)
    draggableElement.value.addEventListener("mouseup", dragEnd)
    draggableElement.value.addEventListener("mousemove", drag)
    draggableElement.value.addEventListener("mouseleave", dragEnd)
})

</script>

<template>
    <div id="content" ref="draggableElement">
        <RegistrationForm v-if="AuthStore.showingRegisForm"/>  
        <LoginForm v-if="AuthStore.showingLoginForm"/>  
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
    z-index: 100000;
    border-style: double;
    border-color: rgb(252, 205, 143);
    border-width: 1px;

    translate: 20px 20px;
    user-select: none;

}

@media (max-width: 700px) {
    #content {
        display: flex;
        align-items: center;
        justify-content: center;
        flex-direction: column;
        padding: 3em;
        background-color: rgba(0, 0, 0, 0.832);
        z-index: 19;

    }
    @media (max-width: 700px) {
        #content {
            width: 100%;
        }
    }
}
</style>
