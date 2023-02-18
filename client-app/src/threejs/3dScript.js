import * as THREE from 'three'
import { useWorldStore } from '../stores/WorldStore'
import { useUIStore } from '../stores/UIStore'
import { initCameraPawn } from './Crew'
import { loadSitePreview } from './ActorManager'
import { GUI } from 'dat.gui'
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader'

export async function init(canvasDomId) {

  /* setup */
  var worldStore = useWorldStore()
  var uiStore = useUIStore()
  var crew, canvas, renderer, scene, lights
  const gltfLoader = new GLTFLoader()
  canvas = document.getElementById(canvasDomId)
  scene = new THREE.Scene()
  crew = initCameraPawn(canvas, scene, worldStore)
  renderer = crew.renderer
  lights = crew.lights
  scene.add(lights[0]) // directional
  scene.add(lights[1]) // ambient
  worldStore.setScene(scene)

  // initActors(scene, worldStore, assetStore)

  /* dev help */
  // const AH = new THREE.AxesHelper(100000)
  // AH.name = 'axes helper'
  // scene.add(AH)
  // const _gui = new GUI()
  // const cameraFolder = _gui.addFolder('Camera')
  // cameraFolder.add(crew.camera.position, 'x', -400, 400)
  // cameraFolder.add(crew.camera.position, 'y', -400, 400)
  // cameraFolder.add(crew.camera.position, 'z', -400, 400)
  // document.getElementById("3js_gui").appendChild(_gui.domElement)

  const clock = new THREE.Clock()

  function run() {
    if (uiStore.showingWorldmap) {
      try {
        worldStore.animationMixer.update(clock.getDelta())
      } catch (error) { 
        // console.(error) 
      }
      try {
        worldStore.preview.rotation.y += .01
      } catch (error) { 
        // console.log(error) 
      }
      if (worldStore.traveling)
        uiStore.updateCountdown()
    }
    uiStore.updateClock()

    renderer.render(scene, crew.camera)
    requestAnimationFrame(run)
  }

  run()
}