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
        repeatValid: false,

        // between 6 to 20 chars, at least one numeric, uppercase & lowercase
        validPwdPattern:  /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/,
        validEmailPattern: /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/,

        regisResponse: null,
        loginResponse: null,
        userIsAdmin: false
    }),
    getters: {
        getShowingRegisForm:(state) => state.showingRegisForm,
        getShowingLoginForm:(state) => state.showingLoginForm,
    },
    actions: {

        setResponse(response) {
            this.response = response
        },
        
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
            this.Password = ""
        },
        hideLoginForm() {
            this.showingLoginForm = false
            this.Password = ""
        },
        
        updateValidation() {
            if (this.Email.match(this.validEmailPattern)) {
                this.emailValid = true
            }
            else {
                this.emailValid = false
            }
            if (this.Password.match(this.validPwdPattern)) {
                this.pwdValid = true
            }
            else { // not valid
                this.pwdValid = false
            }
            if (this.Password == this.repeatedPassword && this.Password != "") {
                this.repeatValid = true
            }
            else { // no repeat
                this.repeatValid = false
            }
        }

    },
})