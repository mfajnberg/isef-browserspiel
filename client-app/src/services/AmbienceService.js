import Howler from 'howler';

let instance = null;

export class Ambience {
  constructor() {
    if (instance) {
      return instance;
    }
    instance = this;
    this.music = new Howler.Howl({
        src: 'isef_ambience_1.mp3',
        loop: true,
        volume: .00,
        // mute: true
        preload: true
    })
  }
}