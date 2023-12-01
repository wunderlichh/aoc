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
let n c =
    match c with
    | 'o' -> Some [ ("one", 1) ]
    | 't' -> Some [ ("two", 2); ("three", 3) ]
    | 'f' -> Some [ ("four", 4); ("five", 5) ]
    | 's' -> Some [ ("six", 6); ("seven", 7) ]
    | 'e' -> Some [ ("eight", 8) ]
    | 'n' -> Some [ ("nine", 9) ]
    | x when System.Char.IsDigit x -> Some [ (x |> string |> int |> string, x |> string |> int) ]
    | _ -> None

let cf (s: string) (candidates: (string * int) list) =
    match candidates |> List.filter (fun c -> s.StartsWith(fst c)) with
    | [ x ] -> Some x
    | _ -> None


let rec numbers (line: string) (nums: int list) : int list =
    //printfn "NUMS %A - %A" line nums

    if line.Length = 0 then
        nums
    else
        match n line[0] with
        | Some c ->
            match cf line c with
            | Some(s, n) -> numbers (line.Substring(s.Length)) (nums @ [ n ] )
            | _ -> numbers (line.Substring(1)) nums

        | None -> numbers (line.Substring(1)) nums

let mapNum line = numbers line [] 

let result =
    // test_input
    System.IO.File.ReadAllLines "1_1.txt"
    |> Array.map mapNum
    |> Array.map (fun t -> sprintf "%d%d" (List.head t) (List.last t))
    |> Array.map int
    |> Array.sum

// 53519 TOO FUCKING HIGH