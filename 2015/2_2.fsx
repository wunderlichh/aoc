type Box = { l: int; w: int; h: int }

let parseInput () =
    System.IO.File.ReadAllLines "2_1.txt"
    |> Array.map (fun line -> line.Split("x"))
    |> Array.map (fun input ->
        { l = int input[0]
          w = int input[1]
          h = int input[2] })

let ribbon (b: Box) =
    let dims = [ b.l; b.w; b.h ] |> List.sort
    (b.l * b.w * b.h) + (dims[0] * 2 + dims[1] * 2)

parseInput () |> Array.map ribbon |> Array.sum


// 3783758