import { createApp } from 'vue'
import { createPinia } from 'pinia'

import './style.css'

import App from './App.vue'
import { init } from './threejs/init'


const app = createApp(App)
const pinia = createPinia()

app.use(pinia)
app.mount('#app')
init("adventure_map", app)