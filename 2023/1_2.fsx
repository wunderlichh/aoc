let test_input =
    """two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen"""
        .Split(null)

let rec startNum (input: string) =
    if input.Length = 0             then failwith "Start not found"
    elif input.StartsWith "one"     then 1
    elif input.StartsWith "two"     then 2
    elif input.StartsWith "three"   then 3
    elif input.StartsWith "four"    then 4
    elif input.StartsWith "five"    then 5
    elif input.StartsWith "six"     then 6
    elif input.StartsWith "seven"   then 7
    elif input.StartsWith "eight"   then 8
    elif input.StartsWith "nine"    then 9
    elif System.Char.IsDigit(input[0]) then
        input[0] |> string |> int
    else
        startNum input[1..]

let rec endNum (input: string) =
    if input.Length = 0             then failwith "End not found"
    elif input.EndsWith "one"       then 1
    elif input.EndsWith "two"       then 2
    elif input.EndsWith "three"     then 3
    elif input.EndsWith "four"      then 4
    elif input.EndsWith "five"      then 5
    elif input.EndsWith "six"       then 6
    elif input.EndsWith "seven"     then 7
    elif input.EndsWith "eight"     then 8
    elif input.EndsWith "nine"      then 9
    elif System.Char.IsDigit(input[input.Length - 1]) then
        input[input.Length - 1] |> string |> int
    else if input.Length = 1 then
        endNum ""
    else
        endNum (input.Substring(0, input.Length - 1))

System.IO.File.ReadAllLines "1_1.txt"
|> Array.map (fun l -> sprintf "%d%d" (startNum l) (endNum l) |> int)
|> Array.sum

// 53515
