import * as THREE from 'three'
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls'
import * as ORBIT from 'three/examples/jsm/controls/OrbitControls'
import _ from 'lodash';
import { useGameAssetStore } from '../stores/GameAssetStore';
import { useUIStore } from '../stores/UIStore'

export function initCameraPawn(canvas, scene, worldStore) {

    const assetStore = useGameAssetStore()
    const uiStore = useUIStore()

    const _renderer = new THREE.WebGLRenderer({ canvas: canvas, antialias: true });
    _renderer.setSize(window.innerWidth, window.innerHeight*.75);
    _renderer.shadowMap.enabled = true
    _renderer.shadowMap.type = THREE.PCFSoftShadowMap


    const _camera = new THREE.PerspectiveCamera
        (60, window.innerWidth / (window.innerHeight*.75), 0.1, 20000);
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
    console.log(_orbit)
    _orbit.update()

    const _pointer = new THREE.Vector2()
    const _raycaster = new THREE.Raycaster()

    var intersects

    let placeActor = _.debounce((event) => {
        
    })

    window.addEventListener('pointerdown', (e) => {
        if (e.button === 0 && uiStore.showingWorldmap && worldStore.objectSnapped) {
            assetStore.placeObjectSound.play()
        }
    })

    let updateWorldCursor = _.debounce((event) => {
        _pointer.x = (event.clientX / window.innerWidth) * 2 - 1
        _pointer.y = - (event.offsetY / _renderer.domElement.height) * 2 + 1

        _raycaster.setFromCamera(_pointer, _camera)
        intersects = _raycaster.intersectObjects(worldStore.intersectables)

        if (uiStore.showingWorldmap && intersects.length > 0) {
            let hex = intersects[0].object.parent
            let vec = new THREE.Vector3();
            const point = intersects[ 0 ].point;
            if (worldStore.cursor) {
                worldStore.cursor.position.set(point.x, point.y, point.z)
                worldStore.cursor.visible = true
            }
            vec.setFromMatrixPosition(hex.matrixWorld);
            if (worldStore.cursor && vec.distanceTo(point) < .83) {
                    worldStore.hoveredItem = hex.userData.wrapper
                    worldStore.cursor.position.set(vec.x, vec.y, vec.z)
                    worldStore.cursor.visible = true
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
                worldStore.objectSnapped = false
            }
            catch (e) {
            }
        }
    }, 1)

    function getHalfwayPoints(origin) {}
    function getCornerVectors(origin) {}

    window.addEventListener('mousemove', updateWorldCursor)

    window.addEventListener('resize', () => {
        _camera.aspect = window.innerWidth / (window.innerHeight * .75)
        _camera.updateProjectionMatrix()
        _renderer.setSize(window.innerWidth, window.innerHeight * .75)
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