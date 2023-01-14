export class ActorBase {
    constructor(object3d) {
        this.object3d = object3d;
        this.axial = worldHexVector
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
