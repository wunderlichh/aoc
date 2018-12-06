(*
--- Day 3: No Matter How You Slice It ---

The Elves managed to locate the chimney-squeeze prototype fabric for Santa's suit (thanks 
to someone who helpfully wrote its box IDs on the wall of the warehouse in the middle of the night). 
Unfortunately, anomalies are still affecting them - nobody can even agree on how to cut the fabric.

The whole piece of fabric they're working on is a very large square - at least 1000 inches on each side.

Each Elf has made a claim about which area of fabric would be ideal for Santa's suit. 
All claims have an ID and consist of a single rectangle with edges parallel to the edges 
of the fabric. Each claim's rectangle is defined as follows:

The number of inches between the left edge of the fabric and the left edge of the rectangle.
The number of inches between the top edge of the fabric and the top edge of the rectangle.
The width of the rectangle in inches.
The height of the rectangle in inches.
A claim like #123 @ 3,2: 5x4 means that claim ID 123 specifies a rectangle 3 inches from the 
left edge, 2 inches from the top edge, 5 inches wide, and 4 inches tall. Visually, it claims 
the square inches of fabric represented by # (and ignores the square inches of fabric represented by .) 
in the diagram below:

...........
...........
...#####...
...#####...
...#####...
...#####...
...........
...........
...........
The problem is that many of the claims overlap, causing two or more claims to cover part of 
the same areas. For example, consider the following claims:

#1 @ 1,3: 4x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2
Visually, these claim the following areas:

........
...2222.
...2222.
.11XX22.
.11XX22.
.111133.
.111133.
........

The four square inches marked with X are claimed by both 1 and 2. (Claim 3, while adjacent 
to the others, does not overlap either of them.)

If the Elves all proceed with their own plans, none of them will have enough fabric. How many 
square inches of fabric are within two or more claims?

Your puzzle answer was 109143.
*)

type Position = {
    Top: int;
    Left: int
}

type Claim = {
    Id: int;
    Width: int;
    Height: int;
    Position: Position
}

let extract (s: string) (separator: char) =
    match s.Split(separator) with
    | [|a; b|] -> (int a, int b)
    | _ -> failwith "argument" 

let parseClaim (input: string) =
    let aIdx = input.IndexOf "@"
    let cIdx = input.IndexOf ":"
    let idPart = input.[1..(aIdx - 1)]
    let loPart = input.[(aIdx + 2)..(cIdx - 1)]
    let siPart = input.[cIdx + 1..]
    let location = extract loPart ','
    let size     = extract siPart 'x'
    {
        Id = int idPart;
        Width = fst size;
        Height = snd size;
        Position = {
            Top = snd location;
            Left = fst location;
        }
    }

let markPosition (map: int[,]) claim =
    for i in [0..(claim.Height - 1)] do
        for j in [0..(claim.Width - 1)] do
            let x = i + claim.Position.Top
            let y = j + claim.Position.Left  
            map.[x, y] <- map.[x, y] + 1
    map

let sumOverlapp (map: int[,]) =
    let mutable state = 0
    for x in 0 .. Array2D.length1 map - 1 do
        for y in 0 .. Array2D.length2 map - 1 do
            state <- if map.[x, y] >= 2 then state + 1 else state
    state

System.IO.File.ReadAllLines "2018\\3.input.txt"
|> Seq.map parseClaim
|> Seq.fold markPosition (Array2D.init 2000 2000 (fun _ _ -> 0)) 
|> sumOverlapp