export async function requestTokenRefresh(store) {
    const options = {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(store.authData)
    }
    fetch('https://localhost:5002/refresh-token', options).then((response) =>
        response.text().then(function(data) {
            store.token = data
            console.log(store.token)
            store.loggedIn = true
            localStorage.setItem('token', store.token)
        })
    )
}
export async function requestLogin(store) {
    const options = {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(store.authData)
    }
    fetch('https://localhost:5002/login', options).then((response) =>
        response.text().then(function(data) {
            store.token = data
            console.log(store.token)
            store.loggedIn = true
            if (store.stayLoggedIn) {
                localStorage.setItem('token', store.token)
                localStorage.setItem('email', store.authData.email)
            }
            else {
                localStorage.setItem('token', "")
            }
        })
    )
}

export async function requestRegis(store) {
    const options = {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(store.authData)
    }
    const response = await fetch("https://localhost:5002/register", options)
}

export function LogOut(store) {
    store.token = ""
    store.authData.email = ""
    store.authData.password = ""
    localStorage.removeItem('token')
    localStorage.removeItem('email')
    store.loggedIn = false
}