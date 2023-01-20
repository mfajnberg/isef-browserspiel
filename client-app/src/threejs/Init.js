import * as THREE from 'three'
import { useWorldStore } from '../stores/WorldStore'
import { useGameAssetStore } from '../stores/GameAssetStore'
import { useUIStore } from '../stores/UIStore'
import { initCameraPawn } from './Crew'
import { initActors, loadWorldCursor } from './ActorManager'
import { GUI } from 'dat.gui'
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader'

export async function init(canvasDomId, vue) {

  /* setup */
  var worldStore = useWorldStore()
  var assetStore = useGameAssetStore()
  var clockStore = useUIStore()
  var crew, canvas, renderer, scene, lights
  const gltfLoader = new GLTFLoader()
  canvas = document.getElementById(canvasDomId)
  scene = new THREE.Scene()
  crew = initCameraPawn(canvas, scene, worldStore)
  renderer = crew.renderer
  lights = crew.lights
  scene.add(lights[0]) // directional
  scene.add(lights[1]) // ambient
  
  initActors(scene, gltfLoader, worldStore, assetStore) 

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
  

  function run() {
    requestAnimationFrame(run)

    if (worldStore.changedCursorURL === true) {
      loadWorldCursor(worldStore.cursorURL, gltfLoader, scene, worldStore)
      worldStore.changedCursorURL = false
    }

    clockStore.getCurrentTime()
    try {
      // worldStore.worldmap.sites[0].scene.rotation.y += (0.01) 
    } catch (e) { }

    renderer.render(scene, crew.camera)
  }
  run()
}