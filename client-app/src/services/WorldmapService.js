export async function fetchGetHexTiles(worldStore) {
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

    await fetch("/api/party/vision", options)
        .then(response => { response.json() })
        .then(data => { 
            worldStore.worldData = data 
            console.log(data)
        })
        .catch(error => { console.log(error) })
}