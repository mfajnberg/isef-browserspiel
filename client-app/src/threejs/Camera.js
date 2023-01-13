import * as THREE from 'three'
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls'

export function initCameraPawn(renderer, scene, store) {
    const _camera = new THREE.PerspectiveCamera
    ( 60, renderer.domElement.width / renderer.domElement.height, 0.1, 20000 );
    _camera.position.x = -352.21
    _camera.position.y = 927.06
    _camera.position.z = 198.36
    _camera.lookAt(scene.position)

    // const _orbit = new OrbitControls( _camera, renderer.domElement )
    const _pointer = new THREE.Vector2()
    const _raycaster = new THREE.Raycaster()
    
    var intersects

    window.addEventListener('click', () => {
        var object = store.hoveredItem
        if (object) {
            var worldPos = new THREE.Vector3();
            worldPos.setFromMatrixPosition( object.matrixWorld );
            store.sites[0].scene.position.set(worldPos.x,worldPos.y+40,worldPos.z)
        }         
    })

    window.addEventListener('mousemove', (event) => {
        _pointer.x = (event.clientX/window.innerWidth) * 2 - 1
        _pointer.y = - (event.offsetY/renderer.domElement.height) * 2 + 1

        _raycaster.setFromCamera(_pointer, _camera)
        intersects = _raycaster.intersectObjects(
            store.hexTiles.map(tile => {return tile.scene}))
        
        if(intersects.length > 0) {
            const thing = intersects[0].object
            store.updateHovered(thing) 
        }
        // }
        else {
            store.updateHovered(null)
        }
    })

    window.addEventListener('resize', () => {
        _camera.aspect = window.innerWidth / (window.innerHeight *.75)
        _camera.updateProjectionMatrix()
        renderer.setSize(window.innerWidth, window.innerHeight *.75)
        renderer.render( scene, _camera );
    }, false)
    
    return {camera: _camera, pointer: _pointer, raycaster: _raycaster} 
}