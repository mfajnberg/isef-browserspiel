import Howler from 'howler'

let _instanceAmbience = null
let _instanceSites = null
let _instanceSites3d = null

export class Ambience {
  constructor() {
    if (_instanceAmbience) {
      return _instanceAmbience
    }
    _instanceAmbience = this
    this.music = new Howler.Howl({
        src: 'isef_ambience_1.mp3',
        loop: true,
        volume: .1,
        mute: false,
        preload: true
    })
  }
}

export class Sites {
  constructor() {
    if (_instanceSites) {
      return _instanceSites
    }
    _instanceSites = this
    this.buffer = []
  }
}

export class Sites3d {
  constructor() {
    if (_instanceSites3d) {
      return _instanceSites3d
    }
    _instanceSites3d = this
    this.buffer = []
  }
}