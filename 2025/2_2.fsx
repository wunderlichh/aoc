type Id = int64

// String in the format of "a-b" to int tuple of a * b
let stoidr (str: string) =
    match str.Split "-" with
    | [| a; b |] -> int64 a, int64 b
    | _ -> failwith (sprintf "Invalid ID Range: '%s'" str)

// Returns true if the id split into n chunks are all the same.
let nSym (n: int) (id: string) =
    let idStr = string id
    let idArr = idStr.ToCharArray()
    let idLen = idArr |> Seq.length

    idArr |> Seq.chunkBySize (idLen / n) |> Seq.distinct |> Seq.length = 1


// Retuns true if the id has any symmetry (from 2 up to id digits count).
let sym (id: Id) =
    let idStr = string id
    let idLen = idStr.Length

    Seq.map (fun n -> nSym n idStr) [ 2..idLen ] |> Seq.contains true

let invalidIds (range: Id * Id) =
    [ (fst range) .. (snd range) ] |> Seq.filter sym

// Parse input strings, find invalid ids, flatten seq and sum all ids
let algo = Seq.map stoidr >> Seq.map invalidIds >> Seq.concat >> Seq.sum

let testInput =
    "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124"

let testAnswer = testInput.Split(",") |> algo
// 4174379265L

let input = System.IO.File.ReadAllText "2025/2_1.txt"

let answer = input.Split(",") |> algo
// 69553832684L
