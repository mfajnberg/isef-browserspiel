import * as THREE from 'three'
import _ from 'lodash';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls'
import { useGameAssetStore } from '../stores/GameAssetStore'
import { useUIStore } from '../stores/UIStore'
import { playAnim, spawnSite } from './ActorManager';
import { HexVector } from '../classes/HexVector';
import { requestTravel } from '../services/WorldmapService';
import { DateTime } from 'luxon';
import {Hexes3d, TileDTOs } from '../stores/000Singletons';
import { usePartyStore } from '../stores/PartyStore';

export function initCameraPawn(canvas, scene, worldStore) {
    const gameAssetStore = useGameAssetStore()
    const uiStore = useUIStore()
    const partyStore = usePartyStore()

    const _renderer = new THREE.WebGLRenderer({ canvas: canvas, antialias: true })
    _renderer.setSize(window.innerWidth, window.innerHeight)
    _renderer.shadowMap.enabled = true
    _renderer.shadowMap.type = THREE.PCFSoftShadowMap

    const _camera = new THREE.PerspectiveCamera
        (60, window.innerWidth / (window.innerHeight), 0.1, 20000)
    worldStore.camera = _camera
    _camera.position.x = 0
    _camera.position.y = 10
    _camera.position.z = 14
    _camera.lookAt(scene.position)
    
    const _orbit = new OrbitControls(_camera, _renderer.domElement)
    worldStore.orbit = _orbit
    _orbit.mouseButtons = { RIGHT: THREE.MOUSE.ROTATE, LEFT: THREE.MOUSE.PAN }
    _orbit.enablePan = false
    _orbit.enableZoom = true
    _orbit.target.set(0, 0, 0)
    const distance = _camera.position.distanceTo( scene.position )
    _orbit.minDistance = _orbit.maxDistance = distance
    const angle = Math.atan2(_camera.position.y - scene.position.y, distance)
    _orbit.minPolarAngle = _orbit.maxPolarAngle = angle
    _orbit.update()

    const _pointer = new THREE.Vector2()
    const _raycaster = new THREE.Raycaster()

    var intersects

    let placeActor = _.debounce((event) => {
        let qHov = worldStore.hoveredItem.userData.Q
        let rHov = worldStore.hoveredItem.userData.R

        if (worldStore.hoveredItem.userData.isBlocked) {
            console.log(`hex ${qHov}|${rHov} already occupied`)
            return
        }
        spawnSite(worldStore, gameAssetStore, new HexVector(qHov, rHov))
        gameAssetStore.placeObjectSound.play()
    })

    window.addEventListener('pointerdown', async (e) => {
        if (e.button === 0 && uiStore.showingWorldmap && worldStore.hoveredItem && worldStore.objectSnapped) {
            worldStore.clickedItem = worldStore.hoveredItem
            if (uiStore.editorMode 
                && !uiStore.hoveringOverlay
                && worldStore.previewModelURI != "HexPreview2.glb") 
                    placeActor()
            
            else if (!uiStore.editorMode && !partyStore.traveling) {

                // normalize using relative party world offset
                const qClicked = worldStore.clickedItem.userData.Q
                const rClicked = worldStore.clickedItem.userData.R
                const qRelative = qClicked - partyStore.party.location.Q
                const rRelative = rClicked - partyStore.party.location.R
                partyStore.travelOK = false

                let closeBoi = false
                if (qRelative == 1 && rRelative == 0) {
                    closeBoi = true
                    partyStore.pawn3d.rotation.y = 0.523599 + (1.0472 * 7 )
                }
                else if (qRelative == 0 && rRelative == 1) {
                    closeBoi = true
                    partyStore.pawn3d.rotation.y = 0.523599 + (1.0472 * 6 )
                }
                else if (qRelative == -1 && rRelative == 1) {
                    closeBoi = true
                    partyStore.pawn3d.rotation.y = 0.523599 + (1.0472 * 5 )
                }
                else if (qRelative == -1 && rRelative == 0) {
                    closeBoi = true
                    partyStore.pawn3d.rotation.y = 0.523599 + (1.0472 * 4 )
                }
                else if (qRelative == 0 && rRelative == -1) {
                    closeBoi = true
                    partyStore.pawn3d.rotation.y = 0.523599 + (1.0472 * 3 )
                }
                else if (qRelative == 1 && rRelative == -1) {
                    closeBoi = true
                    partyStore.pawn3d.rotation.y = 0.523599 + (1.0472 * 2 )
                }
                if (closeBoi) {
                    gameAssetStore.placeObjectSound.play()
                    await requestTravel(qClicked, rClicked)
                    if (partyStore.travelOK) {
                        partyStore.goal = worldStore.clickedItem
                        partyStore.traveling = true
                        playAnim(worldStore, "Walking.fbx")
                        setTimeout(worldStore.movePawn, 5000) // cb
                        uiStore.nextUpdateTime = DateTime.local().plus({
                            seconds: 5
                        })
                    }
                    
                }
            }
        }
    })

    let updateWorldCursor = _.debounce((event) => {
        _pointer.x = (event.clientX / window.innerWidth) * 2 - 1
        _pointer.y = - (event.clientY / window.innerHeight) * 2 + 1

        _raycaster.setFromCamera(_pointer, _camera)
        intersects = _raycaster.intersectObjects(new Hexes3d().buffer)
        if (worldStore.cursor && uiStore.showingWorldmap && intersects.length > 0) {
            let hex = intersects[0].object.parent
            let vec = new THREE.Vector3();
            const point = intersects[ 0 ].point;
            vec.setFromMatrixPosition(hex.matrixWorld)
            worldStore.cursor.visible = true
            worldStore.cursor.position.set(point.x, point.y, point.z)
            worldStore.hoveredItem = hex

            if (worldStore.preview 
                && !partyStore.traveling
                && !hex.userData.isBlocked
                && vec.distanceTo(point) < .82) {
                    worldStore.preview.position.set(vec.x, vec.y, vec.z)
                    worldStore.preview.visible = true
                    worldStore.objectSnapped = true
                }
                else {
                worldStore.preview.visible = false
                worldStore.objectSnapped = false
            }
            
            if (worldStore.hoveredItem.userData.Q == partyStore.party.location.Q && worldStore.hoveredItem.userData.R == partyStore.party.location.R) {
                worldStore.preview.visible = false
            } else {
                const distance = Math.max(
                    Math.abs(partyStore.party.location.Q - worldStore.hoveredItem.userData.Q), 
                    Math.abs(partyStore.party.location.R - worldStore.hoveredItem.userData.R), 
                    Math.abs(partyStore.party.location.Q + partyStore.party.location.R - worldStore.hoveredItem.userData.Q - worldStore.hoveredItem.userData.R)
                )
                if (!uiStore.editorMode && distance != 1) {
                    worldStore.preview.visible = false
                }
            }
        }
        else {
            worldStore.hoveredItem = null
            try {
                worldStore.cursor.visible = false
                worldStore.preview.visible = false
                worldStore.objectSnapped = false
            }
            catch (e) { }
        }
    }, .01)
    canvas.addEventListener('mousemove', (event) => {
        if (worldStore.initialized)
            updateWorldCursor(event)
    })

    window.addEventListener('resize', () => {
        _camera.aspect = window.innerWidth / window.innerHeight
        _camera.updateProjectionMatrix()
        _renderer.setSize(window.innerWidth, window.innerHeight)
        _renderer.render(scene, _camera);
    }, false)

    const _lights = []
    const _dirLight = new THREE.DirectionalLight ( 0xffffff, 1 )
    _dirLight.position.set( -25, 50, 25 )
    _dirLight.castShadow = true
    _dirLight.shadow.mapSize.width = 1024*2
    _dirLight.shadow.mapSize.height = 1024*2
    let side = 10
    _dirLight.shadow.camera.top = side
    _dirLight.shadow.camera.bottom = -side
    _dirLight.shadow.camera.left = side +5
    _dirLight.shadow.camera.right = -side -5
    _dirLight.shadow.camera.far = 2000
    const _ambLight = new THREE.AmbientLight()
    _ambLight.intensity = .45
    _lights.push(_dirLight, _ambLight)

    return { renderer: _renderer, camera: _camera, lights: _lights, orbit: _orbit, pointer: _pointer, raycaster: _raycaster }
}