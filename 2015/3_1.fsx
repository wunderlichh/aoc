let folder (current: (int * int)) (dir: char) =
    let next =
        match dir with
        | '^' -> (fst current, snd current + 1)
        | 'v' -> (fst current, snd current - 1)
        | '<' -> (fst current - 1, snd current)
        | '>' -> (fst current + 1, snd current)
        | _ -> failwith "unknown"

    (current, next)


let (visited, current) =
    System.IO.File.ReadAllText "3_1.txt" |> Seq.toList |> List.mapFold folder (0, 0)

visited @ [ current ] |> List.distinct |> List.length

// 2572
