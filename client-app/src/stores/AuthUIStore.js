import { defineStore } from 'pinia' 

export const useAuthUIStore = defineStore('AuthUIStore', {
    id: 'AuthUI',
    state: () => ({
        
        showingRegisForm: false,
        showingLoginForm: false,
        
        newsletter: false,
        stayLoggedIn: false,
        
        loggedIn: false,
        token: "",
        
        credentials: {
            email: "",
            password: "",
        }
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
        }
    },
})