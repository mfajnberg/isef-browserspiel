import { fetchGetChoices } from "./AvatarCreatorService"

export async function requestTokenRefresh(authStore) {
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
    fetch('/api/user/token-refresh', options).then((response) =>
        response.text().then(function(data) {
            if (response.ok) {
                authStore.token = data
                authStore.loggedIn = true
                localStorage.setItem('token', authStore.token)
            }
        })
    )
}
export async function requestLogin(authStore, partyStore, creatorStore) {
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
    await fetch('/api/user/login', options).then((response) =>
        response.text().then(data => {
            authStore.authResponse = response
            authStore.responseStatus = response.status
            if (response.status === 200) {
                authStore.loggedIn = true
                authStore.token = data.AccessToken
                authStore.userIsAdmin = data.IsAdmin
                if (authStore.stayLoggedIn) {
                    localStorage.setItem('token', authStore.token)
                    localStorage.setItem('Email', authStore.Email)
                }
                else {
                    localStorage.removeItem('Email')
                }
                partyStore.avatar = data.avatar
            }
        })
    )
}

export async function requestRegis(authStore) {
    const options = {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
        credentials: 'include',
        body: JSON.stringify({
            Email: authStore.Email,
            Password: authStore.Password
        })
    }
    await fetch("/api/user/register", options)
    .then((response) => {
    })
    
}

export function LogOut(authStore, partyStore) {
    authStore.token = ""
    authStore.Email = ""
    authStore.Password = ""
    authStore.emailValid = false
    authStore.pwdValid = false
    authStore.repeatValid = false
    localStorage.removeItem('token')
    localStorage.removeItem('Email')
    authStore.loggedIn = false
    authStore.dummyLogin = false
    partyStore.avatar = null
}