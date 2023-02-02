import { createApp } from 'vue'
import { createPinia } from 'pinia'
import PrimeVue from 'primevue/config'

import './style.css'

import App from './App.vue'
import { init } from './threejs/3dScript'


const app = createApp(App)
const pinia = createPinia()

app.use(PrimeVue)
app.use(pinia)
app.mount('#app')
init("adventure_map", app)