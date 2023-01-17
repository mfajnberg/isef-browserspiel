import { ActorBase } from "./ActorBase";

export class HexTile extends ActorBase {
    constructor(WorldHexVector) {
        super(WorldHexVector)
        this.blocked = false
        this.site = null
    }

    leftClick() {
    }
}
