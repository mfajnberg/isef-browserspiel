import { defineStore } from 'pinia' 
import { requestPartyVision } from '../services/WorldmapService'
import { dispose, playAnim } from '../threejs/ActorManager'
import { TileDTOs, Sites3d, Hexes3d } from './000Singletons'
import { useGameAssetStore } from './GameAssetStore'
import { usePartyStore } from './PartyStore'
import { Vector3 } from 'three'

export const useWorldStore = defineStore('WorldStore', {
    id: 'WorldStore',
    state: () => ({
        initialized: false,
        
        scene: null,
        camera: null,
        orbit: null,
        
        objectSnapped: false,
        hoveredItem: null,
        clickedItem: null,
        preview: null,
        cursor: null,
        
        previewModelURI: "",

        response: null,

        animationMixer: null,

        editingAt: null
    }),
    getters: {
    },
    actions: {

        disposeAll() {
            for (let hex of new Hexes3d().buffer) {
                dispose(hex)
            }
            new Hexes3d().buffer.length = 0
            const tiles = new TileDTOs()
            const sites3d = new Sites3d()
            for (let obj3d of sites3d.buffer) {
                dispose(obj3d)
            }
            tiles.buffer.length = 0
            sites3d.buffer.length = 0
        },

        async movePawn() {
            const partyStore = usePartyStore()
            partyStore.traveling = false
            const gameAssetStore = useGameAssetStore()
            await requestPartyVision(
                useWorldStore().clickedItem.userData.Q, 
                useWorldStore().clickedItem.userData.R
            )
            playAnim(this, "Idle.fbx")
            gameAssetStore.pointerUpSound.play()
            partyStore.start = partyStore.goal
        },

        moveCamera(x, z) {
            this.camera.position.x += x
            this.camera.position.z += z
            this.orbit.target.x += x
            this.orbit.target.z += z
        },
        setCameraPosition(x, z) {
            this.orbit.target.x = x
            this.orbit.target.z = z
            this.camera.position.x = x
            this.camera.position.z = z
            this.camera.lookAt(new Vector3(x, 0, z))
            this.orbit.update()
        }
    },
})