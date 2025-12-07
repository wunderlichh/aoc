let greatest (s: string) =
    let sArr = s.ToCharArray() |> Seq.map (string >> int) |> List.ofSeq |> List.indexed

    let findGreatestIdx s x =
        let xOld = snd s
        let xNew = snd x
        if xNew > xOld then x else s

    let n1 = Seq.fold findGreatestIdx (0, 0) sArr

    if fst n1 + 1 = sArr.Length then
        let n2 =
            sArr[.. fst n1 - 1]
            |> List.map snd
            |> List.indexed
            |> Seq.fold findGreatestIdx (0, 0)

        string (snd n2) + string (snd n1)
    else
        let n2 =
            sArr[fst n1 + 1 ..]
            |> List.map snd
            |> List.indexed
            |> Seq.fold findGreatestIdx (0, 0)

        string (snd n1) + string (snd n2)


let testInput =
    "987654321111111
811111111111119
234234234234278
818181911112111"

let testAnswer = testInput.Split(null) |> Array.map (greatest >> int) |> Array.sum

let answer =
    System.IO.File.ReadAllLines "2025/3_1.txt"
    |> Array.map (greatest >> int)
    |> Array.sum
// 16858
