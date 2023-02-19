import { LoadingScreens } from "../stores/000Singletons"
import { useAuthStore } from "../stores/AuthStore"
import { useGameAssetStore } from "../stores/GameAssetStore"
import { useUIStore } from "../stores/UIStore"

export async function requestGetAsset(uri) {
    await fetch(`api/asset/get?name=${uri}`, {
        headers: {
            Authorization: `Bearer ${useAuthStore().token}`,
            credentials: 'include',
            "Content-Type": "application/octet-stream"
        }
    })
    .then(async (response) => 
        await response.arrayBuffer())
    .then(async arrayBuffer => {
        useGameAssetStore().assets3d.push({
            name: uri, 
            data: arrayBuffer, 
        })
        let isAssetFamiliar = false
        for (let element of new LoadingScreens().texts) {
            if (element.name === uri) {
                useUIStore().loadingProgress = element.text
                isAssetFamiliar = true
                break
            }
        }
        if (!isAssetFamiliar)
            useUIStore().loadingProgress = "neue Spielinhalte werden geladen..."
    })
}