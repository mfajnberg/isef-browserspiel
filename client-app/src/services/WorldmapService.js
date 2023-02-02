export async function requestGetHexTiles(authStore, worldStore) {
    const options = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${authStore.token}`
        },
        credentials: 'include'
    }

    await fetch("/api/party/vision", options)
    // await fetch("/api/admin/world/get.json")
        .then(response => { 
            return response.json() 
        })
        .then(data => { 
            const loader = new GLTFLoader()
            data.forEach(element => {
                console.log(element)
                if (element.site){
                    if (element.site.type === 100) {
                    spawnSite(loader, worldStore.scene, worldStore, new HexVector(element.Q, element.R), 'forest_1.glb')
                    }
                }
            })
        })
        .catch(error => { console.log(error) })
}