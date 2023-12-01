System.IO.File.ReadAllText "1_1.txt"
|> Seq.toList
|> Seq.fold
    (fun f x ->
        match x with
        | '(' -> f + 1
        | ')' -> f - 1
        | _ -> failwith "HÄ")
    0
