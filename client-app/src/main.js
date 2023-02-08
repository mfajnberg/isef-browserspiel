import { createApp } from 'vue'
import { createPinia } from 'pinia'

import PrimeVue from 'primevue/config'
import { createManager } from '@vue-youtube/core'

import './style.css'

import App from './App.vue'
import { init } from './threejs/3dScript'


const app = createApp(App)
app.use(createPinia())
app.use(PrimeVue)
app.use(createManager())

app.mount('#app')
init("adventure_map", app)