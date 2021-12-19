let input =
    System.IO.File.ReadAllLines "3_input.txt"
    |> Array.map (fun line ->
        line.ToCharArray()
        |> Array.map (fun c -> (int c) - 48))

let mcb pos (input: int array array) =
    let ones =
        input
        |> Array.filter (fun e -> e.[pos] = 1)
        |> Array.length

    let zeros =
        input
        |> Array.filter (fun e -> e.[pos] = 0)
        |> Array.length

    match ones, zeros with
    | a, b when a < b -> 0
    | a, b when b < a -> 1
    | a, b when b = a -> 1
    | _ -> failwith "dude"

let lcb pos (input: int array array) =
    match mcb pos input with
    | 1 -> 0
    | 0 -> 1
    | _ -> failwith "dude"

let rec binaryToDecimal x b =
    match b with
    | [||] -> x
    | _ ->
        let m = b |> Array.head
        let p = (b |> Array.length) - 1
        let x' = m * (pown 2 p) + x
        binaryToDecimal x' (Array.tail b)

let rec find fn p (input: int array array) =
    match input with
    | [| x |] -> x
    | _ ->
        find
            fn
            (p + 1)
            (input
             |> Array.filter (fun t -> t.[p] = fn p input))

let oxygenGeneratorRating = input |> find mcb 0 |> binaryToDecimal 0
let co2ScrubberRating = input |> find lcb 0 |> binaryToDecimal 0

printfn "oxygen generator rating %A" oxygenGeneratorRating
printfn "CO2 scrubber rating %A" co2ScrubberRating
printfn "life support rating %A" (oxygenGeneratorRating * co2ScrubberRating)

// oxygen generator rating 3311
// CO2 scrubber rating 851
// life support rating 2817661
