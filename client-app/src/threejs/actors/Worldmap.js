import { HexVector } from "../HexVector"

export class Worldmap {
    constructor() {
        this.hexGrid = []
        this.sites = []
    }

    findHex(axial) {
        for (let hex of this.hexGrid) {
            if (axial.Q == hex.axial.Q) {
                if (axial.R == hex.axial.R) {
                    return hex
                }
            }
        }
        return null
    }

    static makeHexGridVectors(radius) {
        let result = []
        let directions = ["NE", "E", "SE", "SW", "W", "NW",]

        let vecCurrent = new HexVector(0, 0)
        result.push(new HexVector(0, 0))

        let currentRing = 1
        while (currentRing <= radius) {
            directions.forEach(dir => {
                let unit = new HexVector()
                
                unit.makeUnitVec(dir)
                vecCurrent.Q = unit.Q * currentRing
                vecCurrent.R = unit.R * currentRing
                result.push(new HexVector(vecCurrent.Q, vecCurrent.R))
                
                let dirTurned = HexVector.turnDirectionClockwise(dir, 2)
                unit.makeUnitVec(dirTurned)
                let vecTurned = new HexVector()
                for (let i = 1; i < currentRing; i++) {
                    vecTurned.Q = unit.Q * i + vecCurrent.Q
                    vecTurned.R = unit.R * i + vecCurrent.R
                    result.push(new HexVector(vecTurned.Q, vecTurned.R))
                }
            })
            currentRing++
        }
        return result
    }
}