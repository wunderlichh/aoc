let parseCard (input: string) =
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
    snd numbers
    |> List.filter (fun s -> fst numbers |> List.contains s)
    |> (fun x -> (x.Length, 1))

let rec multiplyCards (pos: int) (cards: (int * int) list) =
    if (pos >= cards.Length) then
        cards
    else
        let (wins, count) = cards.[pos]

        let cards' =
            cards
            |> List.mapi (fun idx (wins', count') ->
                if idx >= pos + 1 && idx <= pos + wins then
                    (wins', count' + count)
                else
                    (wins', count'))

        multiplyCards (pos + 1) cards'

System.IO.File.ReadAllLines "4_1.txt"
|> Array.toList
|> List.map parseCard
|> List.map wins
|> multiplyCards 0
|> List.map snd
|> List.sum

// 9721255
