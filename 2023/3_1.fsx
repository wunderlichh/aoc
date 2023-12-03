let test_input =
    """467..114..
...*......
..35...633
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598.."""

type EngineSchematic = char array array
type EngineDimension = { w: int; h: int }
type Vector3 = { x: int; y: int; length: int }

type PartNumber =
    { v: int
      pos: Vector3
      adjacent: char list }

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

let findAdjacent (engine: EngineSchematic) (dim: EngineDimension) (number: PartNumber) : char list =
    let right =
        if number.pos.x + number.pos.length < dim.w then
            [ engine[number.pos.y][number.pos.x + number.pos.length] ]
        else
            []

    let left =
        if number.pos.x - 1 >= 0 then
            [ engine[number.pos.y][number.pos.x - 1] ]
        else
            []

    let above =
        if number.pos.y = 0 then
            []
        else
            let ra = max (number.pos.x - 1) 0
            let re = min (number.pos.x + number.pos.length) (dim.w - 1)
            engine[number.pos.y - 1][ra..re] |> Array.toList

    let below =
        if number.pos.y = dim.h - 1 then
            []
        else
            let ra = max (number.pos.x - 1) 0
            let re = min (number.pos.x + number.pos.length) (dim.w - 1)
            engine[number.pos.y + 1][ra..re] |> Array.toList

    above @ below @ left @ right |> List.filter (fun c -> c <> '.')

let partNumbers =
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
    |> List.filter (fun pn -> not pn.adjacent.IsEmpty)

let answer = partNumbers |> List.sumBy (fun pn -> pn.v)


// 543867
