import { defineStore } from 'pinia' 

export const useAuthUIStore = defineStore('AuthUIStore', {
    id: 'AuthUI',
    state: () => ({
        
        showingRegisForm: false,
        showingLoginForm: false,
        
        stayLoggedIn: false,
        newsletter: false,
        
        loggedIn: false,
        token: "",
        
        authData: {
            email: "",
            password: "",
        },
        repeatedPassword: "",

        emailValid: false,
        pwdValid: false,
        pwdRepeat: false,

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