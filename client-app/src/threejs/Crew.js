import * as THREE from 'three'
import _ from 'lodash';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls'
import { useGameAssetStore } from '../stores/GameAssetStore'
import { useUIStore } from '../stores/UIStore'
import { spawnActor } from './ActorManager';
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader';
import { HexVector } from './HexVector';

export function initCameraPawn(canvas, scene, worldStore) {

    const assetStore = useGameAssetStore()
    const uiStore = useUIStore()

    const _renderer = new THREE.WebGLRenderer({ canvas: canvas, antialias: true })
    _renderer.setSize(window.innerWidth, window.innerHeight*.85)
    _renderer.shadowMap.enabled = true
    _renderer.shadowMap.type = THREE.PCFSoftShadowMap


    const _camera = new THREE.PerspectiveCamera
        (60, window.innerWidth / (window.innerHeight*.85), 0.1, 20000)
    _camera.position.x = 0
    _camera.position.y = 10
    _camera.position.z = 14
    _camera.lookAt(scene.position)
    
    const _orbit = new OrbitControls(_camera, _renderer.domElement)
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

        if (!worldStore.hoveredItem.userData.free) {
            console.log(`hex ${qHov}|${rHov} already occupied`)
            return
        }
        assetStore.placeObjectSound.play()
        worldStore.sitesBuffer.push({
            AxialCoordinates: {
                Q: qHov,
                R: rHov
            },
            SiteType: 100
        })
        spawnActor(new GLTFLoader(), scene, worldStore, new HexVector(qHov, rHov), worldStore.previewUrl)
    })

    window.addEventListener('pointerdown', (e) => {
        if (e.button === 0 && uiStore.showingWorldmap && worldStore.hoveredItem) {
            if (uiStore.editorMode && worldStore.preview && worldStore.objectSnapped) {
                placeActor()
            }
        }
    })

    let updateWorldCursor = _.debounce((event) => {
        _pointer.x = (event.clientX / window.innerWidth) * 2 - 1
        _pointer.y = - (event.offsetY / _renderer.domElement.height) * 2 + 1

        _raycaster.setFromCamera(_pointer, _camera)
        intersects = _raycaster.intersectObjects(worldStore.hexes3d)

        if (uiStore.showingWorldmap && intersects.length > 0) {
            let hex = intersects[0].object.parent
            let vec = new THREE.Vector3();
            const point = intersects[ 0 ].point;
            if (worldStore.preview) {
                worldStore.preview.position.set(point.x, point.y, point.z)
                worldStore.preview.visible = true
            }
            vec.setFromMatrixPosition(hex.matrixWorld)
            worldStore.cursor.visible = true
            worldStore.cursor.position.set(point.x, point.y, point.z)
            worldStore.hoveredItem = hex
            if (worldStore.preview 
                && hex.userData.free
                && vec.distanceTo(point) < .82) {
                    worldStore.preview.position.set(vec.x, vec.y, vec.z)
                    worldStore.preview.visible = true
                    worldStore.objectSnapped = true
                }
            else {
                worldStore.objectSnapped = false
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

    function getHalfwayPoints(origin) {}
    function getCornerVectors(origin) {}

    window.addEventListener('mousemove', updateWorldCursor)

    window.addEventListener('resize', () => {
        _camera.aspect = window.innerWidth / (window.innerHeight * .85)
        _camera.updateProjectionMatrix()
        _renderer.setSize(window.innerWidth, window.innerHeight * .85)
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