import * as THREE from 'three'
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls'
import { PCFSoftShadowMap } from 'three'

export function initCameraPawn(canvas, scene, store) {


    const _renderer = new THREE.WebGLRenderer({ canvas: canvas, antialias: true });
    _renderer.setSize( window.innerWidth, window.innerHeight );
    _renderer.shadowMap.enabled = true
    _renderer.shadowMap.type = PCFSoftShadowMap


    const _camera = new THREE.PerspectiveCamera
    ( 60, window.innerWidth / (window.innerHeight *.75), 0.1, 20000 );
    _camera.position.x = -352.21
    _camera.position.y = 927.06
    _camera.position.z = 198.36
    _camera.lookAt(scene.position)

    const _orbit = new OrbitControls( _camera, _renderer.domElement )
    const _pointer = new THREE.Vector2()
    const _raycaster = new THREE.Raycaster()
    
    var intersects

    window.addEventListener('pointerdown', () => {
    })

    window.addEventListener('mousemove', (event) => {
        _pointer.x = (event.clientX/window.innerWidth) * 2 - 1
        _pointer.y = - (event.offsetY/_renderer.domElement.height) * 2 + 1

        _raycaster.setFromCamera(_pointer, _camera)
        intersects = _raycaster.intersectObjects(store.intersectables)
        
        if(intersects.length > 0) {
            let thing = intersects[0].object
            store.updateHovered(thing) 
            if (store.hoveredItem.object3d) {
                let worldPos = new THREE.Vector3();
                worldPos.setFromMatrixPosition( store.hoveredItem.object3d.scene.matrixWorld );
                store.worldmap.sites[0].scene.position.set(worldPos.x,worldPos.y+40,worldPos.z)
            }         
        }
        // }
        else {
            store.updateHovered(null)
        }
    })

    window.addEventListener('resize', () => {
        _camera.aspect = window.innerWidth / (window.innerHeight *.75)
        _camera.updateProjectionMatrix()
        _renderer.setSize(window.innerWidth, window.innerHeight *.75)
        _renderer.render( scene, _camera );
    }, false)
    
    return {renderer: _renderer, camera: _camera, orbit: _orbit, pointer: _pointer, raycaster: _raycaster} 
}