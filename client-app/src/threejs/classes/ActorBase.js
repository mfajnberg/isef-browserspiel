export class ActorBase {
    constructor(hexVector) {
        this.axial = hexVector
        this.object3d = null
        this.displayName = ""
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
