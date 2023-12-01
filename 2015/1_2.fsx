let rec findStep p f (i: char list) =
    if f = -1 then
        p
    else
        match i[p] with
        | '(' -> findStep (p + 1) (f + 1) i
        | ')' -> findStep (p + 1) (f - 1) i
        | _ -> failwith ""

System.IO.File.ReadAllText "1_1.txt" |> Seq.toList |> findStep 0 0

// 1783
