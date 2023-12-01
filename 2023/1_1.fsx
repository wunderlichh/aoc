let test_input =
    """1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet"""
        .Split(null)

let calibration_value (s: string) =
    let digits' = s |> Seq.toList |> Seq.filter System.Char.IsDigit
    sprintf "%c%c" (digits' |> Seq.head) (digits' |> Seq.last)

let output =
    System.IO.File.ReadAllLines "1_1.txt"
    |> Seq.map (calibration_value >> int)
    |> Seq.sum

// 54388
printfn "%A" output
