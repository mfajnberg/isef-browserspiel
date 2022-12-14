import * as THREE from 'three'
import { useWorldmapStore } from '../stores/WorldmapStore'
import { useGameAssetStore } from '../stores/GameAssetStore'
import { useUIStore } from '../stores/UIStore'
import { initRenderer } from './Renderer'
import { initLights } from './Lights'
import { initCameraPawn as initCameraPawn } from './Camera'
import { initActors } from './InitActors'
// import { GUI } from 'dat.gui'

export async function init(canvasDomId) {

  /* setup */
  var worldStore = useWorldmapStore()
  var assetStore = useGameAssetStore()
  var clockStore = useUIStore()
  var canvas, renderer, scene, lights, cameraPawn, cameraPawn
  canvas = document.getElementById(canvasDomId)
  renderer = initRenderer(canvas)
  renderer.setSize( window.innerWidth, window.innerHeight * .75 );
  scene = new THREE.Scene()
  lights = initLights(scene)
  cameraPawn = initCameraPawn(renderer, scene, worldStore)
  
  initActors(scene, worldStore, assetStore) 

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
    clockStore.getCurrentTime()
    try { 
      worldStore.THREE_sites[0].rotation.y += (0.001 * worldStore.count * worldStore.count) 
    } catch(e){}

    cameraPawn.orbit.update()
    renderer.render( scene, cameraPawn.camera )
  }
  run()
}