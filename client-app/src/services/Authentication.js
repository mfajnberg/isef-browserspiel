export async function requestTokenRefresh(store) {
    const options = {
        method: 'POST',
        headers: {
            "Content-Type": "application/json",
        },
        credentials: 'include',
        body: JSON.stringify({
            Email: store.Email,
            Password: store.Password
        })
    }
    fetch('/api/user/token-refresh', options).then((response) =>
        response.text().then(function(data) {
            if (response.ok) {
                store.token = data
                store.loggedIn = true
                localStorage.setItem('token', store.token)
            }
        })
    )
}
export async function requestLogin(store) {
    const options = {
        method: 'POST',
        headers: {
            "Content-Type": "application/json",
        },
        credentials: 'include',
        body: JSON.stringify({
            Email: store.Email,
            Password: store.Password
        })
    }
    fetch('/api/user/login', options).then((response) =>
        response.text().then(function(data) {
            store.token = data
            console.log(store.token)
            store.loggedIn = true
            if (store.stayLoggedIn) {
                localStorage.setItem('token', store.token)
                localStorage.setItem('Email', store.Email)
            }
            else {
                localStorage.removeItem('Email')
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
        credentials: 'include',
        body: JSON.stringify({
            Email: store.Email,
            Password: store.Password
        })
    }
    const response = await fetch("/api/user/register", options)
    console.log(response.text())
}

export function LogOut(store) {
    store.token = ""
    store.Email = ""
    store.Password = ""
    localStorage.removeItem('token')
    localStorage.removeItem('Email')
    store.loggedIn = false
}