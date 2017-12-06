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
*)

let sCoordinates (s: int) : (int * int) =
    if s = 1 then (0, 0)
    else 
        let r = if s = 1 then 1
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
            if x1 < x2 then 
                b >= x1 && b <= x2
            else
                b >= x2 && b <= x1

        let rindWidth = (2 * r) + 1
        let ringHalf = rindWidth / 2
        let biggstSquare = rindWidth * rindWidth
        let corners = [|biggstSquare; biggstSquare - (2 * ringHalf); biggstSquare - (4 * ringHalf); biggstSquare - (6 * ringHalf)|]

        if Seq.exists ((=) s) corners then (ringHalf, ringHalf)
        else
            let middleDistancePoints = Seq.map (fun m -> biggstSquare - if m = 0 then ringHalf else ringHalf + (m * (rindWidth - 1))) [0..3]
            
            let mdpX = [|Seq.min middleDistancePoints; (Seq.min middleDistancePoints) + 2 * (rindWidth - 1)|]
            let mdpY = [|Seq.max middleDistancePoints; (Seq.max middleDistancePoints) - 2 * (rindWidth - 1)|]
            
            let closestMdpX = if abs (s - (Seq.min mdpY)) < abs (s - (Seq.max mdpY)) then Seq.min mdpY else Seq.max mdpY
            let distanceX = if Seq.exists (fun corner -> between s closestMdpX corner) corners then ringHalf else abs (s - closestMdpX) 
            
            let closestMdpY = if abs (s - (Seq.min mdpX)) < abs (s - (Seq.max mdpX)) then Seq.min mdpX else Seq.max mdpX        
            let distanceY = if Seq.exists (fun corner -> between s closestMdpY corner) corners then ringHalf else abs (s - closestMdpY) 

            (distanceX, distanceY)

let manhattanDistance (a1, a2)  (b1, b2) = 
    abs (a1 - b1) + abs (a2 - b2)

let md00 = manhattanDistance (0, 0)

let steps square =
    square |> sCoordinates |> md00

printfn "Data from square %d is carried %d steps" 1 (steps 1)
printfn "Data from square %d is carried %d steps" 12 (steps 12)
printfn "Data from square %d is carried %d steps" 23 (steps 23)

printfn "#####################################################"
printfn "      steps (368078) = %d" (steps 368078)
printfn "#####################################################"
