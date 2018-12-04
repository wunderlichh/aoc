open System.Collections.Generic
(*
--- Part Two ---

Confident that your list of box IDs is complete, you're ready to find the boxes full of prototype fabric.

The boxes will have IDs which differ by exactly one character at the same position in both strings. 
For example, given the following box IDs:

abcde
fghij
klmno
pqrst
fguij
axcye
wvxyz

The IDs abcde and axcye are close, but they differ by two characters (the second and fourth). 
However, the IDs fghij and fguij differ by exactly one character, the third (h and u). 
Those must be the correct boxes.

What letters are common between the two correct box IDs? (In the example above, this is 
found by removing the differing character from either ID, producing fgij.)
*)


let input = System.IO.File.ReadAllLines "2018\\2.input.txt"

let testInput = "abcde
fghij
klmno
pqrst
fguij
axcye
wvxyz"

(* 
Muss noch gefixt werden    
let common (s: string) (s': string) =
    let allOthers = s' |> List.ofSeq
    s
    |> List.ofSeq
    |> List.map (fun c -> List.tryFind ((=) c) allOthers)
    |> List.choose id
    |> List.fold (fun sa c -> sa + string c) ""
*)

let diff (s: string) (s': string) =
    Seq.fold2 (fun acc sl sr -> if sl <> sr then acc + 1 else acc) 0 s s'

let rec asw ids all =
    match ids with 
    | [id] -> 
        match List.tryFind (fun i -> diff i id = 1) all with
        | Some e -> (id, e)
        | None -> failwith "Not found"
    | id::tail -> 
        match List.tryFind (fun i -> diff i id = 1) all with
        | Some e -> (id, e)
        | None -> asw tail all
    | [] -> failwith "List empty"

let ids = input |> Array.toList

let answer = asw ids ids

// bvnfawcnyoeyudzrpgsleimtkj Zwei fast geliche ID's
// bvnfawcnyoeyudzrpgsldimtkj

// bvnfawcnyoeyudzrpgslimtkj Ohne e/d 
// bvnfawcnyoeyudzrpgslimtkj