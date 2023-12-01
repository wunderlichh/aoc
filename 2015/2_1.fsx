type Box = { l: int; w: int; h: int }

let parseInput () =
    System.IO.File.ReadAllLines "2_1.txt"
    |> Array.map (fun line -> line.Split("x"))
    |> Array.map (fun input ->
        { l = int input[0]
          w = int input[1]
          h = int input[2] })

let area b =
    2 * b.l * b.w
    + 2 * b.w * b.h
    + 2 * b.h * b.l
    + (min (min (b.l * b.w) (b.w * b.h)) (b.h * b.l))

parseInput () |> Array.map area |> Array.sum

// 1588178
