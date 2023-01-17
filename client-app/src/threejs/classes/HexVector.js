export class HexVector {
  constructor(axialQ, axialR) {
    this.Q = axialQ
    this.R = axialR
  }

  equals(other) {
    if (!(other instanceof HexVector)) {
      return false
    }
    return this.Q === other.Q && this.R === other.R
  }

  getWorldXFromAxialQ() {
    let x = 50 * (Math.sqrt(3) * this.Q + Math.sqrt(3) / 2 * this.R)
    return x
  }
  getWorldZFromAxialR() {
    let z = 50 * (3. / 2 * this.R)
    return z
  }

  neighbors() {
    return [
      new HexVector(this.Q + 1, this.R),
      new HexVector(this.Q + 1, this.R - 1),
      new HexVector(this.Q, this.R - 1),
      new HexVector(this.Q - 1, this.R),
      new HexVector(this.Q - 1, this.R + 1),
      new HexVector(this.Q, this.R + 1)
    ];
  }

  axialDistance(other) {
    let dQ = Math.abs(this.Q - other.Q);
    let dR = Math.abs(this.R - other.R);
    let dS = Math.abs(-this.Q - this.R + other.Q + other.R);

    return Math.max(dQ, dR, dS);
  }

  setByCardinal(direction, length){
    switch (direction) {
      case (direction == "NE"):
        this.Q = (this.Q + 1*length)
        this.R = (this.R - 1*length)
        return
      case (direction == "E"):
        this.Q = (this.Q + 1*length)
        return
      case (direction == "SE"):
        this.R = (this.R + 1*length)
        return
      case (direction == "SW"):
        this.Q = (this.Q - 1*length)
        this.R = (this.R + 1*length)
        return
      case (direction == "W"):
        this.Q = (this.Q - 1*length)
        return
      case (direction == "NW"):
        this.R = (this.R - 1*length)
        return
    }
  }

  // rewrite as numeric array look-up thingy to use "by" parameter
  static turnDirectionClockwise(from, by) {
    switch (from) {
      case "NE":
        return "SE"
        case "E":
          return "SW"
        case "SE":
          return "W"
        case "SW":
          return "NW"
        case "W":
          return "NE"
        case"NW":
          return "E"
    }
  }

  static getUnitVector(dir) {
    switch (dir) {
      case "NE":
          return new HexVector(1, -1)
        case "E":
          return new HexVector(1, 0)
        case "SE":
          return new HexVector(0, 1)
        case "SW":
          return new HexVector(-1, 1)
        case "W":
          return new HexVector(-1, 0)
        case "NW":
          return new HexVector(0, -1)
    }
  }

  makeUnitVec(dir) {
    if (dir === "NE") {
      this.Q = 1
      this.R = -1
    } else if (dir === "E") {
      this.Q = 1
      this.R = 0
    } else if (dir === "SE") {
      this.Q = 0
      this.R = 1
    } else if (dir === "SW") {
      this.Q = -1
      this.R = 1
    } else if (dir === "W") {
      this.Q = -1
      this.R = 0
    } else if (dir === "NW") {
      this.Q = 0
      this.R = -1
    }
  }

}


