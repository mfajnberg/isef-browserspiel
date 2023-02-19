import * as THREE from 'three'
import { useWorldStore } from '../stores/WorldStore'
import { useUIStore } from '../stores/UIStore'
import { initCameraPawn } from './Crew'
import { usePartyStore } from '../stores/PartyStore'

export async function init(canvasDomId) {

  /* setup */
  const worldStore = useWorldStore()
  const uiStore = useUIStore()
  const partyStore = usePartyStore()
  var crew, canvas, renderer, scene, lights
  canvas = document.getElementById(canvasDomId)
  scene = new THREE.Scene()
  crew = initCameraPawn(canvas, scene, worldStore)
  renderer = crew.renderer
  lights = crew.lights
  scene.add(lights[0]) // directional
  scene.add(lights[1]) // ambient
  worldStore.scene = scene

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
      if (!partyStore.traveling) { }
      else {
        uiStore.updateCountdown()
        const displaceVector = new THREE.Vector3().subVectors(partyStore.goal.position, partyStore.start.position)
        const velocityVector = displaceVector.divideScalar(.7) // das ist aktuell kompletter quatsch
        const distanceVector = velocityVector.clone().multiplyScalar(clock.getDelta())
        partyStore.pawn3d.position.add(distanceVector)
        worldStore.moveCamera(distanceVector.x, distanceVector.z)
      }
    }
    uiStore.updateClock()

    renderer.render(scene, crew.camera)
    requestAnimationFrame(run)
  }

  run()
}