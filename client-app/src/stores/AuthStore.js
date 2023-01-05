import { defineStore } from 'pinia' 

export const useAuthStore = defineStore('AuthStore', {
    id: 'AuthStore',
    state: () => ({
        dummyLogin: false,
        
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
            this.showingLoginForm = false
        },
        showLoginForm() {
            this.showingRegisForm = false
            this.showingLoginForm = true
        },
        hideRegisForm() {
            this.showingRegisForm = false
            this.repeatedPassword = ""
        },
        hideLoginForm() {
            this.showingLoginForm = false
        }
    },
})