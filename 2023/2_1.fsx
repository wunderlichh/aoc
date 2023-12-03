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


let answer (limit: Color) (games: Game array) : int =
    let checkGame (game: Game) : bool =
        game.colors
        |> Seq.fold (fun s c -> s && c.r <= limit.r && c.g <= limit.g && c.b <= limit.b) true

    games
    |> Seq.choose (fun g -> if checkGame g then Some g else None)
    |> Seq.sumBy (fun g -> g.id)


System.IO.File.ReadAllLines "2_1.txt"
|> Array.map parse
|> answer { r = 12; g = 13; b = 14 }

// 2541
