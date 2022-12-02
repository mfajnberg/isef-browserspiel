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
            "Content-Type": "application/json",
        },
        credentials: 'include',
        body: JSON.stringify({
            Email: store.Email,
            Password: store.Password
        })
    }
    fetch('https://localhost:5002/login', options).then((response) =>
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
    fetch("https://localhost:5002/register", options).then((response) =>
        console.log(response.text())
    )
}

export function LogOut(store) {
    store.token = ""
    store.Email = ""
    store.Password = ""
    localStorage.removeItem('token')
    localStorage.removeItem('Email')
    store.loggedIn = false
}