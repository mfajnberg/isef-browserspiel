import * as THREE from 'three'
import { useStore } from '../stores/ReactiveStore'
import { initRenderer } from './Renderer'
import { initLights } from './Lights'
import { initCameraPawn as initCameraPawn } from './Camera'
import { initActors } from './Actors'
// import { GUI } from 'dat.gui'

export async function init(canvasDomId) {

  /* setup */
  var piniaStore, canvas, renderer, scene, lights, cameraPawn, cameraPawn
  piniaStore = useStore()
  canvas = document.getElementById(canvasDomId)
  renderer = initRenderer(canvas)
  renderer.setSize( window.innerWidth, window.innerHeight * .75 );
  scene = new THREE.Scene()
  lights = initLights(scene)
  cameraPawn = initCameraPawn(renderer, scene, piniaStore)
  initActors(scene, piniaStore) 

  /* dev help */
  /* const AH = new THREE.AxesHelper(100000)
  AH.name = 'axes helper'
  scene.add(AH)
  const _gui = new GUI()
  const cameraFolder = _gui.addFolder('Camera')
  cameraFolder.add(cameraPawn.camera.position, 'x', -400, 400)
  cameraFolder.add(cameraPawn.camera.position, 'y', -400, 400)
  cameraFolder.add(cameraPawn.camera.position, 'z', -400, 400)
  document.getElementById("dev-gui-container").appendChild(_gui.domElement)
  */

  function run() {
    requestAnimationFrame( run )
 
    try { 
      piniaStore.THREE_sites[0].rotation.y += (0.001 * piniaStore.count * piniaStore.count) 
    } catch(e){}

    cameraPawn.orbit.update()
    renderer.render( scene, cameraPawn.camera )
  }
  run()
}