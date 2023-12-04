type EngineSchematic = char array array
type EngineDimension = { w: int; h: int }
type Vector2 = { x: int; y: int }
type Vector3 = { x: int; y: int; length: int }
type Adjacent = { c: char; pos: Vector2 }

type PartNumber =
    { v: int
      pos: Vector3
      adjacent: Adjacent list }

type FindOneLineFolderState =
    { xoffset: int
      yoffset: int
      incompleteNumber: char list
      partNumbers: PartNumber list }

let findOneLine (dim: EngineDimension) (yoffset: int) (input: char array) =
    let folder (state: FindOneLineFolderState) (c: char) =
        if (System.Char.IsDigit c) && state.xoffset < dim.w - 1 then
            { state with
                incompleteNumber = state.incompleteNumber @ [ c ]
                xoffset = state.xoffset + 1 }
        else if state.incompleteNumber.IsEmpty then
            { state with
                xoffset = state.xoffset + 1 }
        else if (System.Char.IsDigit c) && state.xoffset = dim.w - 1 then
            let numLength = state.incompleteNumber @ [ c ] |> Seq.length

            { state with
                incompleteNumber = []
                partNumbers =
                    state.partNumbers
                    @ [ { v = state.incompleteNumber @ [ c ] |> List.fold (fun s c -> s + string c) "" |> int
                          adjacent = []
                          pos =
                            { x = state.xoffset - numLength + 1
                              y = state.yoffset
                              length = numLength } } ] }
        else
            let numLength = state.incompleteNumber |> Seq.length

            { state with
                incompleteNumber = []
                xoffset = state.xoffset + 1
                partNumbers =
                    state.partNumbers
                    @ [ { v = state.incompleteNumber |> List.fold (fun s c -> s + string c) "" |> int
                          adjacent = []
                          pos =
                            { x = state.xoffset - numLength
                              y = state.yoffset
                              length = numLength } } ] }

    input
    |> Seq.fold
        folder
        { xoffset = 0
          yoffset = yoffset
          incompleteNumber = []
          partNumbers = [] }
    |> fun s -> s.partNumbers

let findPartNumbers (dim: EngineDimension) (input: EngineSchematic) : PartNumber list =
    input
    |> Array.mapi (fun x l -> findOneLine dim x l)
    |> Array.fold (fun s a -> s @ a) []

let findAdjacent (engine: EngineSchematic) (dim: EngineDimension) (number: PartNumber) : Adjacent list =
    let right: Adjacent list =
        if number.pos.x + number.pos.length < dim.w then
            [ { c = engine[number.pos.y][number.pos.x + number.pos.length]
                pos =
                  { x = number.pos.x + number.pos.length
                    y = number.pos.y } } ]
        else
            []

    let left: Adjacent list =
        if number.pos.x - 1 >= 0 then
            [ { c = engine[number.pos.y][number.pos.x - 1]
                pos =
                  { x = number.pos.x - 1
                    y = number.pos.y } } ]
        else
            []

    let above: Adjacent list =
        if number.pos.y = 0 then
            []
        else
            let ra = max (number.pos.x - 1) 0
            let re = min (number.pos.x + number.pos.length) (dim.w - 1)

            engine[number.pos.y - 1][ra..re]
            |> Array.mapi (fun idx c ->
                { c = c
                  pos = { x = ra + idx; y = number.pos.y - 1 } })
            |> Array.toList

    let below: Adjacent list =
        if number.pos.y = dim.h - 1 then
            []
        else
            let ra = max (number.pos.x - 1) 0
            let re = min (number.pos.x + number.pos.length) (dim.w - 1)

            engine[number.pos.y + 1][ra..re]
            |> Array.mapi (fun idx c ->
                { c = c
                  pos = { x = ra + idx; y = number.pos.y + 1 } })
            |> Array.toList

    above @ below @ left @ right |> List.filter (fun c -> c.c <> '.')


let engineSchematic =
    System.IO.File.ReadAllLines("3_1.txt")
    |> Seq.toArray
    |> Seq.map (fun line -> line |> Seq.toArray)
    |> Seq.toArray

let engineDimensions =
    { w = engineSchematic |> Array.head |> Array.length
      h = engineSchematic |> Array.length }

engineSchematic
|> findPartNumbers engineDimensions
|> List.map (fun pn ->
    { pn with
        adjacent = findAdjacent engineSchematic engineDimensions pn })
|> List.filter (fun pn -> pn.adjacent |> List.map (fun a -> a.c) |> List.contains '*')
|> List.groupBy (fun pn -> pn.adjacent)
|> List.map (fun (a, p) -> (a, p |> List.map (fun p' -> p'.v)))
|> List.filter (fun (a, p) -> p.Length = 2)
|> List.map (fun (a, p) -> (p.Head, p.Tail.Head))
|> List.map (fun (a, b) -> a * b)
|> List.sum

// 79613331
