export function AxialToWorldXY(Q, R) {
    const x = 50 * (Math.sqrt(3) * Q + Math.sqrt(3)/2 * R)
    const y = 50 * (3./2 * R)
    return {x, y}
}