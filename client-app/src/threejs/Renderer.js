import * as THREE from 'three'
import { PCFSoftShadowMap } from 'three'

export function initRenderer(canvas) {
  const _renderer = new THREE.WebGLRenderer({ canvas: canvas, antialias: true });
  _renderer.setSize( window.innerWidth, window.innerHeight );
  _renderer.shadowMap.enabled = true
  _renderer.shadowMap.type = PCFSoftShadowMap
  
  return _renderer
}
  
export function initScene() {

}
