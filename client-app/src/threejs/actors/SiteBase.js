import { ActorBase } from "./ActorBase";

export class SiteBase {
    constructor() {
        this.model = null // ArrayBuffer
        this.location = null // HexTile
        this.blocks = [] // HexVector[], relative to location.axial
        // actions
    }
}
