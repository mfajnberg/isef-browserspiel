import { defineStore } from 'pinia' 

export const useAuthStore = defineStore('AuthStore', {
    id: 'AuthStore',
    state: () => ({
        
        showingRegisForm: false,
        showingLoginForm: false,
        
        stayLoggedIn: false,
        newsletter: false,
        
        loggedIn: false,
        token: "",
        
        Email: "",
        Password: "",

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