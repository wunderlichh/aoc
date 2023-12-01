open System.Text.RegularExpressions

let test_input =
    """two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen"""
        .Split(null)

let numbers input =
    seq {
        for m in Regex.Matches(input, @"(\d|one|two|three|four|five|six|seven|eight|nine)") do
            yield m.Value
    }

let (|Int|_|) str =
    match System.Int32.TryParse(str: string) with
    | (true, int) -> Some(int)
    | _ -> None

let numeric number =
    match number with
    | "one" -> 1
    | "two" -> 2
    | "three" -> 3
    | "four" -> 4
    | "five" -> 5
    | "six" -> 6
    | "seven" -> 7
    | "eight" -> 8
    | "nine" -> 9
    | Int x -> x
    | _ -> failwith "invalid"

let output =
    System.IO.File.ReadAllLines "1_1.txt"
    // test_input
    |> Array.map numbers
    |> Array.map (Seq.map numeric)
    |> Array.map (fun n -> (n |> Seq.head), (n |> Seq.last))
    |> Array.map (fun t -> sprintf "%d%d" (fst t) (snd t))
    |> Array.map int
// |> Seq.map (fun n -> sprintf "%d%d" (n |> Seq.head) (n |> Seq.last))
// |> Seq.map int

// 1: 54388
// 53_519 --> WRONG TOO HIGH
printfn "OUT %A" (output |> Array.sum)
