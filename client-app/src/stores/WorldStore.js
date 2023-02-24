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
        hoveredItemPrevious: null,
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
        disposeSingleTileAt(hexVec) {
            const hexes3d = new Hexes3d().buffer
            const sites3d = new Sites3d().buffer
            const tileDTOs = new TileDTOs().buffer

            let hex3dIndex = 0
            for (let hex3d of hexes3d) {
                if (hexVec.Q === hex3d.userData.Q && hexVec.R === hex3d.userData.R) {
                    dispose(hex3d)
                    hexes3d.splice(hex3dIndex, 1)
                    break
                } else {
                    hex3dIndex++
                }
            }
            
            let site3dIndex = 0
            for (let site3d of sites3d) {
                if (hexVec.Q === site3d.userData.hexVector.Q && hexVec.R === site3d.userData.hexVector.R) {
                    dispose(site3d)
                    sites3d.splice(site3dIndex, 1)
                    break
                } else {
                    site3dIndex++
                }
            }

            tileDTOs.splice(tileDTOs.findIndex(
                t => t.AxialCoordinates.Q === hexVec.Q
                && t.AxialCoordinates.R === hexVec.R ), 1)
        },
        disposeAllTiles() {
            const hexes3d = new Hexes3d().buffer
            const sites3d = new Sites3d().buffer
            const tileDTOs = new TileDTOs().buffer
            for (let hex3d of hexes3d) {
                dispose(hex3d)
            }
            for (let site3d of sites3d) {
                dispose(site3d)
            }
            hexes3d.length = 0
            sites3d.length = 0
            tileDTOs.length = 0
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
            // this.camera.lookAt(new Vector3(x, 0, z))
        }
    },
})