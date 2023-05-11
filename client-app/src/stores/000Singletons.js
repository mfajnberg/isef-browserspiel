import Howler from 'howler'

let _instanceTileDTOs = null
let _instanceSites3d = null
let _instanceHexes3d = null
let _instanceLoadingScreens = null
let _instanceAmbience = null

// this is more like a buffer of HexTileDTOs, actually......
export class TileDTOs {
  constructor() {
    if (_instanceTileDTOs) {
      return _instanceTileDTOs
    }
    _instanceTileDTOs = this
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

export class Hexes3d {
  constructor() {
    if (_instanceHexes3d) {
      return _instanceHexes3d
    }
    _instanceHexes3d = this
    this.buffer = []
  }
}

export class LoadingScreens {
  constructor() {
    if (_instanceLoadingScreens) {
      return _instanceLoadingScreens
    }
    _instanceLoadingScreens = this
    this.texts = [
      {
        name: "forest_1.glb",
        text: "Wälder aufforsten..."
      },
      {
        name: "flag.glb",
        text: "Wappen und Siegel beim Heroldsamt melden..."
      },
      {
        name: "house.glb",
        text: "Siedlerhöfe möblieren..."
      },
      {
        name: "tent_field_camp.glb",
        text: "Heringe für die Zelte fangen..."
      },
      {
        name: "crystals.glb",
        text: "Mineralien aus Erathia importieren..."
      },
      {
        name: "chest_lp.glb",
        text: "Schatztruhen vergraben..."
      },
      {
        name: "tree_ancient.glb",
        text: "Die alten Götter um Rat bitten..."
      },
      {
        name: "3dCursorArrow.glb",
        text: "Axiale Koordinatensysteme nachvollziehen..."
      },
      {
        name: "3dCursorArrow.glb",
        text: "Alles wieder von vorne machen müssen..."
      },
      {
        name: "Arissa.fbx",
        text: "Cape ausklopfen und ausschütteln..."
      },
      {
        name: "Walking.fbx",
        text: "Draußen spazieren gehen..."
      },
      {
        name: "Idle.fbx",
        text: "Ein Nickerchen machen..."
      },
    ]
  }
}

export class Ambience {
  constructor() {
    if (_instanceAmbience) {
      return _instanceAmbience
    }
    _instanceAmbience = this
    this.music = new Howler.Howl({
        src: 'music.mp3',
        loop: true,
        volume: .14,
        mute: false,
        preload: true
    })
  }
}
