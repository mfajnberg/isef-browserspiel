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