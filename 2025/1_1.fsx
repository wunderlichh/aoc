type Dial = { pos: int; zeros: int }

let rotate clicks (fn: int -> int -> int) (dial: Dial) =
    let pos' = (100 + (fn dial.pos clicks)) % 100
    let zeros' = if pos' = 0 then dial.zeros + 1 else dial.zeros

    { dial with pos = pos'; zeros = zeros' }

let (|Left|Right|) (s: string) =
    match s[0] with
    | 'L' -> Left(int (s.Substring 1))
    | 'R' -> Right(int (s.Substring 1))
    | _ -> failwith "Unknown rotation direction"

let folder (dial: Dial) =
    function
    | Left n -> rotate n (-) dial
    | Right n -> rotate n (+) dial

let testAnswer =
    [| "L68"; "L30"; "R48"; "L5"; "R60"; "L55"; "L1"; "L99"; "R14"; "L82" |]
    |> Seq.fold folder { pos = 50; zeros = 0 }

let realAnswer =
    System.IO.File.ReadAllLines "2025/1_1.txt"
    |> Seq.fold folder { pos = 50; zeros = 0 }

//      P    C
//(100 + (50 - 68)) % 100
//(100 + (0 - 5)) % 100
//(100 + (95 + 60)) % 100
