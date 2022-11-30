import { defineStore } from 'pinia' 

export const useAuthUIStore = defineStore('AuthUIStore', {
    id: 'AuthUI',
    state: () => ({
        showingRegisForm: false,
        showingLoginForm: false,
    }),
    actions: {

        showRegisForm() {
            this.showingRegisForm = true
            console.log(this.showingRegisForm)
        },
        showLoginForm() {
            this.showingLoginForm = true
            console.log(this.showingLoginForm)
        },
        hideRegisForm() {
            this.showingRegisForm = false
            console.log(this.showingRegisForm)
        },
        hideLoginForm() {
            this.showingLoginForm = false
            console.log(this.showingLoginForm)
        },
    },
})