import * as THREE from 'three'

export function initLights(scene) {
    const _lights = []
    const _dirLight = new THREE.DirectionalLight ( 0xffffff, 1 )
    _dirLight.position.set( -250, 500, 250 )
    _dirLight.castShadow = true
    _dirLight.shadow.mapSize.width = 1024*2
    _dirLight.shadow.mapSize.height = 1024*2
    let side = 500
    _dirLight.shadow.camera.top = side
    _dirLight.shadow.camera.bottom = -side
    _dirLight.shadow.camera.left = side
    _dirLight.shadow.camera.right = -side
    _dirLight.shadow.camera.far = 20000
    scene.add( _dirLight )
    const _ambLight = new THREE.AmbientLight()
    _ambLight.intensity = .4
    scene.add(_ambLight)

    _lights.push(_dirLight, _ambLight)
    return _lights
}