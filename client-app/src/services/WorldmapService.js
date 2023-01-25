export async function requestStuff(worldStore) {
    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem("token")}`
        },
        credentials: 'include',
        body: JSON.stringify(worldStore.sitesBuffer)
    }

    console.log(localStorage.getItem("token"))
    console.log(options.body)

    await fetch("/api/world/init", options)
        .then(response => { response.json() })
        .then(data => { console.log(data) })
        .catch(error => { console.log(error) })
}