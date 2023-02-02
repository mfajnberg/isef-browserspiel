import { fetchGetChoices } from "./AvatarCreatorService"

export async function requestTokenRefresh(authStore) {
    const options = {
        method: 'POST',
        headers: {
            "Content-Type": "application/json",
        },
        credentials: 'include'
    }
    fetch('/api/user/token-refresh', options).then((response) =>
        response.text().then(function(data) {
            if (response.ok) {
                authStore.token = data
                authStore.loggedIn = true
                localStorage.setItem('token', authStore.token)
                // make sure that gameplay data is delivered at some point as well (probably)
            }
            else {
                console.log(response)
            }
        })
    )
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
    await fetch('/api/user/login', options).then(response => {       
        authStore.setResponse(response)
        if (response.status !== 200) {
            console.log(response.text())
            // DISPLAY ERROR MSG IN UI
        }
        else {
            response.json().then(LoginResponseDTO => {
                console.log(LoginResponseDTO)
                authStore.loggedIn = true
                authStore.token = LoginResponseDTO.accessToken
                authStore.userIsAdmin = LoginResponseDTO.isAdmin
                partyStore.avatar = LoginResponseDTO.avatar
                if (authStore.stayLoggedIn) {
                    localStorage.setItem('token', authStore.token)
                    localStorage.setItem('Email', authStore.Email)
                }
                else {
                    localStorage.removeItem('Email')
                }
            })
        }
    })
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