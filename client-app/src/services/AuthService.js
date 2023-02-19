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
                const data = await response.json()
                authStore.loginResponseData = data
                authStore.token = data.accessToken
                authStore.userIsAdmin = data.isAdmin
                partyStore.party = data.party
                partyStore.avatar = data.avatar
                if (authStore.stayLoggedIn)
                    localStorage.setItem('token', authStore.token)
                authStore.loggedIn = true
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
                const data = await response.json()
                authStore.loginResponseData = data
                authStore.token = data.accessToken
                authStore.userIsAdmin = data.isAdmin
                partyStore.party = data.party
                partyStore.avatar = data.avatar
                if (authStore.stayLoggedIn)
                    localStorage.setItem('token', authStore.token)
                authStore.loggedIn = true
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