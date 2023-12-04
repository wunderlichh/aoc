let input = System.IO.File.ReadAllLines "4_1.txt" |> Array.toList

let parseLine (input: string) =
    let numbers = input.[input.IndexOf(":") + 1 ..].Trim()

    let winningNumbers =
        numbers[.. numbers.IndexOf("|") - 1].Trim().Split(" ")
        |> Array.toList
        |> List.filter (fun s -> s.Length > 0)
        |> List.map int

    let userNumbers =
        numbers[numbers.IndexOf("|") + 1 ..].Trim().Split(" ")
        |> Array.toList
        |> List.filter (fun s -> s.Length > 0)
        |> List.map int

    (winningNumbers, userNumbers)

let wins (numbers: int list * int list) =
    snd numbers |> List.filter (fun s -> fst numbers |> List.contains s)

input
|> List.map parseLine
|> List.map wins
|> List.map (fun s -> pown 2 (s.Length - 1))
|> List.sum

// 25231