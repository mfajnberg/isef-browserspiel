export class ActorBase {
    constructor() {
        this.object3d = null
        this.hexTile = null
    }

    leftClick() {
    }

    rightClick() {
    }

    moveAbsGrid(hexVector) {
        // ...
        this.axial = hexVector
    }

    moveAbsWorld(x, y, z) {
    }

    moveRelGrid(hexVector) {
    }

    moveRelWorld(x, y, z) {
    }
}
