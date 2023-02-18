import { useWorldStore } from "../stores/WorldStore"

export async function requestTokenRefresh(authStore, partyStore) {
    const options = {
        method: 'POST',
        headers: {
            "Content-Type": "application/json",
        },
        credentials: 'include'
    }
    await fetch('/api/user/token-refresh', options)
        .then(async response => {       
            authStore.loginResponse = response
            if (!response.ok) {
                console.log(response.text())
            }
            else { 
                authStore.loginResponseData = await response.json()
                authStore.token = authStore.loginResponseData.accessToken
                authStore.userIsAdmin = authStore.loginResponseData.isAdmin
                partyStore.avatar = authStore.loginResponseData.avatar
                if (authStore.stayLoggedIn)
                    localStorage.setItem('token', authStore.token)
                authStore.loggedIn = true
                useWorldStore().setAbsoluteZeroOffset(
                    authStore.loginResponseData.party.location.Q,
                    authStore.loginResponseData.party.location.R
                )
            }
        })
}

export async function requestLogin(authStore, partyStore) {
    const options = {
        method: 'POST',
        headers: {
            "Content-Type": "application/json",
        },
        credentials: 'include',
        body: JSON.stringify({
            Email: authStore.Email,
            Password: authStore.Password
        })
    }
    await fetch('/api/user/login', options)
        .then(async response => {       
            authStore.loginResponse = response
            if (!response.ok) {
                console.log(response.text())
            }
            else { 
                authStore.loginResponseData = await response.json()
                authStore.token = authStore.loginResponseData.accessToken
                authStore.userIsAdmin = authStore.loginResponseData.isAdmin
                partyStore.avatar = authStore.loginResponseData.avatar
                if (authStore.stayLoggedIn) {
                    localStorage.setItem('token', authStore.token)
                }
                authStore.loggedIn = true
                useWorldStore().setAbsoluteZeroOffset(
                    authStore.loginResponseData.party.location.Q,
                    authStore.loginResponseData.party.location.R
                )
            }
        })
}

export async function requestRegis(authStore) {
    const options = {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            Email: authStore.Email,
            Password: authStore.Password
        })
    }
    await fetch("/api/user/register", options)
    .then(response => {
        authStore.regisResponse = response
        authStore.Password = ""
        authStore.repeatedPassword = ""
        authStore.updateValidation()
        console.log(response.text())
    })
}

export function LogOut(authStore, partyStore) {
    partyStore.avatar = null
    authStore.token = ""
    authStore.Email = ""
    authStore.Password = ""
    authStore.updateValidation()
    localStorage.removeItem('token')
    authStore.loggedIn = false
}