(*
--- Day 3: Spiral Memory ---

You come across an experimental new kind of memory stored on an infinite two-dimensional grid.

Each square on the grid is allocated in a spiral pattern starting at
a location marked 1 and then counting up while spiraling outward. For
example, the first few squares are allocated like this:

17  16  15  14  13
18   5   4   3  12
19   6   1   2  11
20   7   8   9  10
21  22  23---> ...

While this is very space-efficient (no squares are skipped),
requested data must be carried back to square 1 (the location
of the only access port for this memory system) by programs
that can only move up, down, left, or right. They always take
the shortest path: the Manhattan Distance between the location
of the data and square 1.

For example:

Data from square 1 is carried 0 steps, since it's at the access port.
Data from square 12 is carried 3 steps, such as: down, left, left.
Data from square 23 is carried only 2 steps: up twice.
Data from square 1024 must be carried 31 steps.

How many steps are required to carry the data from the square
identified in your puzzle input all the way to the access port?

Your puzzle input is 368078.

Your puzzle answer was 371.

The first half of this puzzle is complete! It provides one gold star: *

--- Part Two ---

As a stress test on the system, the programs here clear the grid and then
store the value 1 in square 1. Then, in the same allocation order as shown
above, they store the sum of the values in all adjacent squares, including diagonals.

So, the first few squares' values are chosen as follows:

Square 1 starts with the value 1.
Square 2 has only one adjacent filled square (with value 1), so it also stores 1.
Square 3 has both of the above squares as neighbors and stores the sum of their values, 2.
Square 4 has all three of the aforementioned squares as neighbors and stores the sum of their values, 4.
Square 5 only has the first and fourth squares as neighbors, so it gets the value 5.
Once a square is written, its value does not change. Therefore, 
the first few squares would receive the following values:

147  142  133  122   59
304    5    4    2   57
330   10    1    1   54
351   11   23   25   26
362  747  806--->   ...

What is the first value written that is larger than your puzzle input?

Your puzzle input is still 368078.

*)

let ring (s: int) : int = 
    if s = 1 
    then 1
    else  
        let mutable ring = 0
        let mutable border = 3
        let mutable sqareCount = 1

        while sqareCount < s do
            sqareCount <- sqareCount + (2 * border + 2 * (border - 2))
            border <- border + 2
            ring <- ring + 1

        ring

let between x1 x2 b =
    if x1 < x2 
    then b >= x1 && b <= x2
    else b >= x2 && b <= x1

let mdp (bs: int) (rh: int) (rw: int) : int seq = 
    Seq.map (fun m -> bs - if m = 0 then rh else rh + (m * (rw - 1))) [0..3]

let sCoordinates (s: int) : (int * int) =
    if s = 1 then (0, 0)
    else 
        let r = ring s
        let ringWidth = (2 * r) + 1
        let ringHalf = ringWidth / 2
        let biggstSquare = ringWidth * ringWidth
        let corners = [|biggstSquare; biggstSquare - (2 * ringHalf); biggstSquare - (4 * ringHalf); biggstSquare - (6 * ringHalf)|]

        if Seq.exists ((=) s) corners 
        then (ringHalf, ringHalf)
        else
            let middleDistancePoints = mdp biggstSquare ringHalf ringWidth
            
            let mdpX = [|Seq.min middleDistancePoints; (Seq.min middleDistancePoints) + 2 * (ringWidth - 1)|]
            let mdpY = [|Seq.max middleDistancePoints; (Seq.max middleDistancePoints) - 2 * (ringWidth - 1)|]
            
            let closestMdpX = if abs (s - (Seq.min mdpY)) < abs (s - (Seq.max mdpY)) then Seq.min mdpY else Seq.max mdpY
            let distanceX = if Seq.exists (fun corner -> between s closestMdpX corner) corners then ringHalf else abs (s - closestMdpX) 
            
            let closestMdpY = if abs (s - (Seq.min mdpX)) < abs (s - (Seq.max mdpX)) then Seq.min mdpX else Seq.max mdpX        
            let distanceY = if Seq.exists (fun corner -> between s closestMdpY corner) corners then ringHalf else abs (s - closestMdpY) 

            (distanceX, distanceY)

type Quadrant = 
    | TopLeft
    | TopRight
    | BottomLeft
    | BottomRight

let sQuadrant (s: int) : Quadrant =
    let r = ring s
    let ringWidth = (2 * r) + 1
    let ringHalf = ringWidth / 2
    let biggstSquare = ringWidth * ringWidth

    let middleDistancePoints = mdp biggstSquare ringHalf ringWidth

    let right = Seq.min middleDistancePoints
    let bottom = Seq.max middleDistancePoints
    let left = Seq.min middleDistancePoints + 2 * (ringWidth - 1)
    let top = Seq.max middleDistancePoints - 2 * (ringWidth - 1)
    
    if (between right top s) then TopRight
    else if (between left top s) then TopLeft
    else if (between left bottom s) then BottomLeft
    else BottomRight


let memorySize = 21
let memory: int [,] = Array2D.init memorySize memorySize (fun _ _ -> 0)
let center = memorySize / 2
memory.[center, center] <- 1

let setNext (position: (int * int)) (memory: int [,]) : int =
    let (x, y) = position        
    let newItem = 
        Seq.sum 
            [|
                memory.[x    , y + 1]  // 1. Right
                memory.[x - 1, y + 1]  // 2. Top Right
                memory.[x - 1, y    ]  // 3. Top
                memory.[x - 1, y - 1]  // 4. Top Left
                memory.[x    , y - 1]  // 5. Left
                memory.[x + 1, y - 1]  // 6. Bottom Left
                memory.[x + 1, y    ]  // 7. Bottom
                memory.[x + 1, y + 1]  // 8. Bottom Right
            |]

    memory.[x, y] <- newItem
    newItem

let next xCoordinate yCoordinate : Unit = 
    let next = setNext (xCoordinate, yCoordinate) memory

    if next > 368078
    then printfn "FOUND: %d" next

let step v = 
    printfn "STEP: %d" v

    let (x, y) = sCoordinates v
    printfn "COORDINATES: (X: %d, Y: %d)" x y
    
    let quadrant = sQuadrant v
    printfn "QUADRANT: %A" quadrant

    match quadrant with
    | TopRight -> next (center - y) (center + x)
    | TopLeft -> next (center - y) (center - x)
    | BottomLeft -> next (center + y) (center - x)
    | BottomRight -> next (center + y) (center + x)

    printfn "%A \n" memory

Seq.iter step [2..memorySize*memorySize]