import * as THREE from 'three'
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls'
import { PCFSoftShadowMap } from 'three'
import _ from 'lodash';

export function initCameraPawn(canvas, scene, worldStore) {


    const _renderer = new THREE.WebGLRenderer({ canvas: canvas, antialias: true });
    _renderer.setSize(window.innerWidth, window.innerHeight*.75);
    _renderer.shadowMap.enabled = true
    _renderer.shadowMap.type = PCFSoftShadowMap


    const _camera = new THREE.PerspectiveCamera
        (60, window.innerWidth / (window.innerHeight*.75), 0.1, 20000);
    _camera.position.x = 0
    _camera.position.y = 16
    _camera.position.z = 6
    _camera.lookAt(scene.position)

    const _orbit = new OrbitControls(_camera, _renderer.domElement)
    const _pointer = new THREE.Vector2()
    const _raycaster = new THREE.Raycaster()

    var intersects

    window.addEventListener('pointerdown', () => {
    })

    let debouncedFun = _.debounce((event) => {
        _pointer.x = (event.clientX / window.innerWidth) * 2 - 1
        _pointer.y = - (event.offsetY / _renderer.domElement.height) * 2 + 1

        _raycaster.setFromCamera(_pointer, _camera)
        intersects = _raycaster.intersectObjects(worldStore.intersectables)

        if (intersects.length > 0) {
            let hex = intersects[0].object.parent
            worldStore.hoveredItem = hex.userData.wrapper
            if (hex) {
                let vec = new THREE.Vector3();
                vec.setFromMatrixPosition(hex.matrixWorld);
                if (worldStore.worldmap.sites[0])
                worldStore.worldmap.sites[0].scene.position.set(vec.x, vec.y+.1, vec.z)
            }
        }
    }, .00001)

    window.addEventListener('mousemove', debouncedFun)

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