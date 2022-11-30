export async function requestLogin(credentials) {
    const options = {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(credentials)
    }
    console.log(options.body)
    const response = await fetch("https://localhost:5001/login", options)
    console.log(response.json())
}

export async function requestRegis(credentials) {
    const options = {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(credentials)
    }
    console.log(options.body)
    const response = await fetch("https://localhost:5001/register", options)
    console.log(response.json())
}