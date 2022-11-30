import { defineStore } from 'pinia' 

export const useAuthUIStore = defineStore('AuthUIStore', {
    id: 'AuthUI',
    state: () => ({
        showingRegisForm: false,
        showingLoginForm: false,

        stayLoggedIn: false
    }),
    actions: {

        showRegisForm() {
            this.showingRegisForm = true
        },
        showLoginForm() {
            this.showingLoginForm = true
        },
        hideRegisForm() {
            this.showingRegisForm = false
        },
        hideLoginForm() {
            this.showingLoginForm = false
        },
    },
})