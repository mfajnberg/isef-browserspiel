export async function requestRegis(worldStore) {
    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `bearer ${localStorage.getItem("token")}`
        },
        credentials: 'include',
        body: JSON.stringify(worldStore.hexTileBuffer)
    }
    
    console.log(options.body)

    await fetch("/api/admin/world/init", options)
        .then(response => { response.json() })
        .then(data => { console.log(data) })
        .catch(error => { console.log(error) })
}