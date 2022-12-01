export async function requestLogin(store) {
    const options = {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(store.authData)
    }
    
    console.log(options.body)

    const response = await fetch("https://localhost:5002/login", options)

    console.log(response.json())
    
    if (response.ok) {
        store.token = response.data
    }
}

export async function requestRegis(store) {
    const options = {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(store.authData)
    }
    console.log(options.body)
    const response = await fetch("https://localhost:5002/register", options)
    console.log(response.json())
}

export function LogOut(store) {
    store.token = ""
    store.authData.email = ""
    store.authData.password = ""
    localStorage.removeItem('token')
    store.loggedIn = false
}