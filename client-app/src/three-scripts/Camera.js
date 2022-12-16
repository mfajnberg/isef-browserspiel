import * as THREE from 'three'
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls'

export function initCameraPawn(renderer, scene, store) {
    const _camera = new THREE.PerspectiveCamera
    ( 60, window.innerWidth / window.innerHeight * .75 , 0.1, 20000 );
    _camera.position.x = -352.21
    _camera.position.y = 927.06
    _camera.position.z = 198.36
    _camera.lookAt(scene.position)

    const _orbit = new OrbitControls( _camera, renderer.domElement )
    const _pointer = new THREE.Vector2()
    const _raycaster = new THREE.Raycaster()
    
    var intersects

    window.addEventListener('mousemove', (event) => {
        _pointer.x = (event.clientX/window.innerWidth) * 2 - 1
        _pointer.y = - (event.clientY/window.innerHeight) * 2 + 1
        _raycaster.setFromCamera(_pointer, _camera)
        intersects = _raycaster.intersectObjects(scene.children)
        
        if(intersects.length > 0) {
            const thing = intersects[0].object
            // if (thing.parent.name == "") { console.log(thing.name) } // axes helper
            // else { 
            store.updateHovered(thing.parent.name) } // regular stuff
        // }
        else {
            store.updateHovered("")
        }
    })

    window.addEventListener('resize', () => {
        _camera.aspect = window.innerWidth / window.innerHeight *.75
        _camera.updateProjectionMatrix()
        renderer.setSize(window.innerWidth, window.innerHeight *.75)
        renderer.render( scene, _camera );
    }, false)
    
    return {camera: _camera, orbit: _orbit, pointer: _pointer, raycaster: _raycaster} 
}