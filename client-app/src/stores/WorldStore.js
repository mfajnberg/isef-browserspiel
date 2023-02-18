import { defineStore } from 'pinia' 
import { requestGetWorldSliceAdmin } from '../services/EditorService'
import { requestGetHexTiles } from '../services/WorldmapService'
import { initActors, dispose, playAnim } from '../threejs/ActorManager'
import { Sites, Sites3d } from './000Singletons'
import { useAuthStore } from './AuthStore'
import { useGameAssetStore } from './GameAssetStore'

export const useWorldStore = defineStore('WorldStore', {
    id: 'WorldStore',
    state: () => ({
        initialized: false,

        absoluteZeroOffset: {
            Q: 0, 
            R: 0
        },
        
        scene: null,
        camera: null,
        hexes3d: [],
        sites3d: [],
        character: null,
        
        objectSnapped: false,
        hoveredItem: null,
        preview: null,
        cursor: null,
        
        previewModelURI: "",

        response: null,

        animationMixer: null,

        traveling: false
    }),
    getters: {
        getScene: (state) => state.scene,
        getHexes3d: (state) => state.hexes3d,
        getSites: (state) => state.sites3d,
        getSitesBuffer: (state) => state.sitesBuffer,
        getHoveredName: (state) => {
            if (state.hoveredItem != null) {
                return state.hoveredItem.name
            }
            return ""
            },
        getAbsoluteZeroOffset: (state) => state.absoluteZeroOffset
    },
    actions: {
        setAbsoluteZeroOffset(q, r) {
            this.absoluteZeroOffset.Q = q
            this.absoluteZeroOffset.R = r
        },

        async fetchWorldData() {
            await requestGetHexTiles(this)
        },
        setScene(scene) {
            this.scene = scene
        },
        async ACTION(assetStore) {
            if (!this.initialized) {
                await initActors(this, assetStore)
                this.initialized = true
            }
        },
        disposeAll() {
            const sites3d = new Sites3d()
            for (let obj3d of sites3d.buffer) {
                for (let hex of this.hexes3d) {
                    if (hex.userData.Q === obj3d.userData.hexVector.Q &&
                        hex.userData.R === obj3d.userData.hexVector.R) {
                            hex.userData.isBlocked = false
                        }
                    }
                    obj3d.userData.hexVector = null
                    dispose(obj3d)
                }
            sites3d.buffer = []
            
            const sites = new Sites()
            for (let site of sites.buffer) {
                        site.SiteType = 0
            }
        },
        async movePawn() {
            this.traveling = false
            const gameAssetStore = useGameAssetStore()
            await requestGetHexTiles(useAuthStore(), this)
            playAnim(this, gameAssetStore, "Idle.fbx")
            gameAssetStore.pointerUpSound.play()
        }
    },
})