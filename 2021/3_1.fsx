let input =
    System.IO.File.ReadAllLines "3_input.txt"
    |> Array.map (fun line ->
        line.ToCharArray()
        |> Array.map (fun c -> (int c) - 48))

/// finds the most common bit from the input at position 'pos'
let mostCommonBit pos (input: int array array) =
    let length = input |> Array.length
    let ones = input |> Array.sumBy (fun e -> e.[pos])
    ones > length / 2

let leastCommonBit pos input = not (mostCommonBit pos input)

let boolToInt b =
    match b with
    | true -> 1
    | false -> 0

/// convertes an array of zero and ones to a decimal number
let rec binaryToDecimal x b =
    match b with
    | [||] -> x
    | _ ->
        let m = b |> Array.head
        let p = (b |> Array.length) - 1
        let x' = (boolToInt m) * (pown 2 p) + x
        binaryToDecimal x' (Array.tail b)

let computeBy bitFunction input =
    [ 0 .. 11 ]
    |> Seq.map (fun i -> input |> bitFunction i)
    |> Array.ofSeq
    |> binaryToDecimal 0

let gammaRate = input |> computeBy mostCommonBit
let epsilonRate = input |> computeBy leastCommonBit
let powerConsumption = gammaRate * epsilonRate

printfn "gamma rate = %d, epsilon rate = %d, power consumption = %d" gammaRate epsilonRate powerConsumption
// gamma rate = 3516, epsilon rate = 579, power consumption = 2035764
