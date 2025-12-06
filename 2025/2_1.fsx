type Id = int64

// String in the format of "a-b" to int tuple of a * b
let stoidr (str: string) =
    match str.Split "-" with
    | [| a; b |] -> int64 a, int64 b
    | _ -> failwith (sprintf "Invalid ID Range: '%s'" str)

// An Id is invalid if its symmetrical eq 55 (5 twice), 6464 (64 twice), and 123123 (123 twice)
let isInvalid (id: Id) =
    let idStr = string id
    let len = idStr.Length
    let fstHalf = idStr.Substring(0, len / 2)
    let sndHalf = idStr.Substring(len / 2)
    fstHalf = sndHalf

let invalidIds (range: Id * Id) =
    [ (fst range) .. (snd range) ] |> Seq.filter isInvalid

// Parse input strings, find invalid ids, flatten seq and sum all ids
let algo = Seq.map stoidr >> Seq.map invalidIds >> Seq.concat >> Seq.sum

let testInput =
    "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124"

let testAnswer = testInput.Split(",") |> algo
// 1227775554L

let input = System.IO.File.ReadAllText "2025/2_1.txt"

let answer = input.Split(",") |> algo
// 53420042388L
