import { HexVector } from './HexVector';
import { PriorityQueue } from '@datastructures-js/priority-queue'
import { Hexes3d } from '../stores/000Singletons';

export class Worldmap {
    constructor() {
        this.hexGrid = []
        this.sites = []
    }

    static findHex(axial) {
        for (let hex of new Hexes3d().buffer) {
            if (axial.Q == hex.userData.Q) {
                if (axial.R == hex.userData.R) {
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

    static findPathAStar(start, end, worldStore) {
        let frontier = []
        let processed = []
        frontier.push(start)
        
        while (frontier.length > 0) {
            let candidate = frontier[0]
            
            if (candidate.name === end.name) { 
                break 
            }
            
            let candidateIndex = frontier.findIndex(hex =>
                hex.userData.FCost < candidate.userData.FCost ||
                hex.userData.FCost === candidate.userData.FCost
                && hex.userData.HCost < candidate.userData.HCost )
            
            console.log(candidateIndex)
            if (candidateIndex != -1)
                candidate = frontier[candidateIndex]

            frontier.splice(candidateIndex, 1)
            processed.push(candidate)
            
            let neighbors = this.getFreeNeighbors(candidate, worldStore)
            for (let neighbor of neighbors) {
                if ((processed.findIndex(hex => 
                    hex.userData.Q === candidate.userData.Q
                    && hex.userData.R === candidate.userData.R)) != -1) {
                        continue
                }
                let frontierNeighborIndex = frontier.findIndex(hex => 
                    hex.userData.Q === neighbor.userData.Q
                    && hex.userData.R === neighbor.userData.R)
                
                let newGCost = candidate.userData.GCost + 1

                if (newGCost < neighbor.userData.GCost || frontierNeighborIndex === -1) {
                    neighbor.userData.GCost = newGCost
                    neighbor.userData.HCost = this.getDistance(neighbor, goal)
                    neighbor.userData.FCost = neighbor.userData.GCost + neighbor.userData.HCost
                    neighbor.userData.CameFrom = candidate
                    if (frontierNeighborIndex === -1) {
                        frontier.push(neighbor)
                    }
                }
            }
        }
        let path = []
        let node = end
        while (node.userData.name != start.userData.name) {
            path.push(node)
            node = node.userData.CameFrom
        }
        return path
    }


    static getFreeNeighbors(ofHex, worldStore) {
        let result = []
        for (let vec of 
                new HexVector(ofHex.userData.Q, ofHex.userData.R)
                .getNeighborVectors()) {
            for (let hex of new Hexes3d().buffer) {
                if (hex.userData.Q === vec.Q
                    && hex.userData.R === vec.R
                    && !hex.userData.isBlocked) {
                        result.push(hex)
                }
            }
        }
        return result
    }

}

export function siteTypeToURI(code) {
    switch (code) {
        case 101:
            return "forest_1.glb"
        case 201:
            return "flag.glb"
        case 202:
            return "house.glb"
        case 203:
            return "tent_field_camp.glb"
        case 204:
            return "crystals.glb"
        case 205:
            return "chest_lp.glb"
        case 206:
            return "tree_ancient.glb"

    }
}

export function siteTypeToInfoText(code) {
    switch (code) {
        case 101:
            return "Verbotener Wald"
        case 201:
            return "Sammelpunkt"
        case 202:
            return "Gehöft"
        case 203:
            return "Feldlager"
        case 204:
            return "Kristalle"
        case 205:
            return "Schatztruhe"
        case 206:
            return "Baum-Ent"

    }
}
export function URIToSiteType(URI) {
    switch (URI) {
        case "forest_1.glb":
            return 101
        case "flag.glb":
            return 201
        case "house.glb":
            return 202
        case "tent_field_camp.glb":
            return 203
        case "crystals.glb":
            return 204
        case "chest_lp.glb":
            return 205
        case "tree_ancient.glb":
            return 206

    }
}