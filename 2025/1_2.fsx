type Direction =
    | R
    | L

type Instruction = { d: Direction; c: int }

// State of the dial during while executing the instructions
type State = { p: int; z: int; w: int }

let initalState = { p = 50; z = 0; w = 0 }

let LL = 0 // Lower Limit
let UL = 99 // Upper Limit

// Get the next position based on the current position and the direction we're moving.
let nextInSeq d p =
    match d with
    | R -> if p + 1 > UL then LL else p + 1
    | L -> if p - 1 < LL then UL else p - 1

// Generates the seq of elements the instruction is passing.
// For example: p = 50, c = 5, d = R -> [51, 52, 53, 54, 55]
//              p = 50, c = 2, d = L -> [49, 48]
let generateSeq d (p: int) (c: int) =
    let moved = nextInSeq d

    let folder (s: int list * int) (x: int) =
        let p = snd s
        let u = fst s
        let p' = moved p
        u @ [ p' ], p'

    [ 1..c ] |> List.fold folder ([], p) |> fst

// Rotate the dial. Calculates the new state from the previous one an the instruction given.
let rotate s i =
    let rs = generateSeq i.d s.p i.c // Seq of elements describing the rotation [50; 51; 52; ...]
    let np = rs |> List.last // The last element of the rotation seq (rs) is the position this instruction landed on
    let pZero = np = 0 // true if the instruction landed on the zero position

    // Calculate the round-trips or wraps around the dial.
    // The amount of wraps is equal to the number of zeros in the rotation seq BUT there is an edge-case when np eq. true.
    // This is the reason i reverse the sequence, skip one and search for zeros in the other elements.
    let wraps =
        rs |> List.rev |> List.tail |> List.filter (fun c -> c = 0) |> List.length

    { p = np
      z = if pZero then s.z + 1 else s.z
      w = s.w + wraps }

let parseInstruction (str: string) =
    match str.[0] with
    | 'R' -> { d = R; c = int <| str.Substring 1 }
    | 'L' -> { d = L; c = int <| str.Substring 1 }
    | _ -> failwith "parseInstruction error"

let testInput =
    [| "L68"; "L30"; "R48"; "L5"; "R60"; "L55"; "L1"; "L99"; "R14"; "L82" |]
    |> Seq.map parseInstruction
    |> Seq.fold rotate initalState

let input =
    System.IO.File.ReadAllLines "2025/1_1.txt"
    |> Seq.map parseInstruction
    |> Seq.fold rotate initalState

// Answer Nr 2
// z      w
// 1147 + 5642
