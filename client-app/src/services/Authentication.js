export async function requestLogin(store) {
    const options = {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(store.credentials)
    }
    
    console.log(options.body)

    const response = await fetch("https://localhost:5001/login", options)

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
        body: JSON.stringify(store.credentials)
    }
    console.log(options.body)
    const response = await fetch("https://localhost:5001/register", options)
    console.log(response.json())
}

export function LogOut(store) {
    store.token = ""
    store.credentials.email = ""
    store.credentials.password = ""
    localStorage.removeItem('token')
    store.loggedIn = false
}