type Color = { r: int; g: int; b: int }
type Game = { id: int; colors: Color list }

let parseColor (color: Color) (input: string) =
    match input.Split(" ") with
    | [| x; "red" |] -> { color with r = int x }
    | [| x; "green" |] -> { color with g = int x }
    | [| x; "blue" |] -> { color with b = int x }
    | _ -> failwith (sprintf "parseColor Error: %A" input)

let parse (input: string) : Game =
    let idLen = input.Length - input.Substring(input.IndexOf(":")).Length - 5
    let id = input.Substring(5, idLen) |> int

    let colors =
        input.Substring(input.IndexOf(":") + 2).Split(";")
        |> Array.map (fun s -> s.Split(","))
        |> Array.map (fun s -> s |> Array.map (fun d -> d.Trim()))
        |> Array.map (fun s -> s |> Array.fold parseColor { r = 0; g = 0; b = 0 })
        |> Array.toList

    { id = id; colors = colors }


let power (game: Game) : int =
    let color =
        game.colors
        |> Seq.fold
            (fun s c ->
                { s with
                    r = (max s.r c.r)
                    g = (max s.g c.g)
                    b = (max s.b c.b) })
            { r = 0; g = 0; b = 0 }

    color.r * color.g * color.b

System.IO.File.ReadAllLines "2_1.txt"
|> Array.map parse
|> Array.map power
|> Array.sum

// 66016
