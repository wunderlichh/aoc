(*
--- Day 2: Inventory Management System ---

You stop falling through time, catch your breath, and check the screen on the device. 
"Destination reached. Current Year: 1518. Current Location: North Pole Utility Closet 83N10." 
You made it! Now, to find those anomalies.

Outside the utility closet, you hear footsteps and a voice. "...I'm not sure either. But now that 
so many people have chimneys, maybe he could sneak in that way?" Another voice responds, "Actually, 
we've been working on a new kind of suit that would let him fit through tight spaces like that. But, 
I heard that a few days ago, they lost the prototype fabric, the design plans, everything! Nobody on 
the team can even seem to remember important details of the project!"

"Wouldn't they have had enough fabric to fill several boxes in the warehouse? They'd be stored together, 
so the box IDs should be similar. Too bad it would take forever to search the warehouse for two similar 
box IDs..." They walk too far away to hear any more.

Late at night, you sneak to the warehouse - who knows what kinds of paradoxes you could cause if you were 
discovered - and use your fancy wrist device to quickly scan every box and produce a list of the likely 
candidates (your puzzle input).

To make sure you didn't miss any, you scan the likely candidate boxes again, counting the number that have 
an ID containing exactly two of any letter and then separately counting those with exactly three of any letter. 
You can multiply those two counts together to get a rudimentary checksum and compare it to what your device predicts.

For example, if you see the following box IDs:

    abcdef contains no letters that appear exactly two or three times.
    bababc contains two a and three b, so it counts for both.
    abbcde contains two b, but no letter appears exactly three times.
    abcccd contains three c, but no letter appears exactly two times.
    aabcdd contains two a and two d, but it only counts once.
    abcdee contains two e.
    ababab contains three a and three b, but it only counts once.

Of these box IDs, four of them contain a letter which appears exactly twice, and three of them contain 
a letter which appears exactly three times. Multiplying these together produces a checksum of 4 * 3 = 12.

What is the checksum for your list of box IDs?

Your puzzle answer was 6696.
*)

let input = "bvhfawknyoqsudzrpgslecmtkj
bpufawcnyoqxldzrpgsleimtkj
bvhfawcnyoqxqdzrplsleimtkf
bvhoagcnyoqxudzrpgsleixtkj
bxvfgwcnyoqxudzrpgsleimtkj
bvqfawcngoqxudzrpgsleiktkj
bvhfawcnmoqxuyzrpgsleimtkp
bvheawcnyomxsdzrpgsleimtkj
bcdfawcnyoqxudzrpgsyeimtkj
bvhpawcnyoqxudzrpgsteimtkz
bxhfawcnyozxudzrpgsleimtoj
bvhfdwcnyozxudzrposleimtkj
bvpfawcnyotxudzrpgsleimtkq
bvhfpwccyoqxudzrpgslkimtkj
bvhfawcnyoqxudirpgsreimtsj
bvhfawcnyoqxudzppgbleemtkj
bvhzawcnyoqxudqrpgslvimtkj
bvhfawclyoqxudirpgsleimtka
bvhgawfnyoqxudzrpguleimtkj
bvhfazcnytqxudzrpgslvimtkj
bvhfawcnygxxudzrpgjleimtkj
bxhfawcnyoqxudzipgsleimtxj
bvhptwcnyoqxudzrpgsleimtmj
bzhfawcgyooxudzrpgsleimtkj
bvhjlwcnyokxudzrpgsleimtkj
bvhfawcnyoqxudbrmgslesmtkj
bvhfawcnysixudzwpgsleimtkj
bvhflwcnymqxxdzrpgsleimtkj
bvifawcnyoyxudzrpgsleimtvj
bvhfawcnyofxudlrpgsheimtkj
bvhbawcmyoqxudzrpggleimtkj
bhhxgwcnyoqxudzrpgsleimtkj
bvhfawgnyoqxbdzrpgsleimfkj
bvhfawcnyoqxudcrngsleimykj
bvhfawcnyofxudzrpgslebgtkj
bvhfaocnybqxudzapgsleimtkj
bvhxawcnyodxudzrpfsleimtkj
bchfawcnyoqxudrrtgsleimtkj
bvhfawcqyoqxudzdpgsltimtkj
bvhfawknyoqxudzrpnsleimtbj
cihfawcnyoqxudirpgsleimtkj
bvlfawpnyoqxudzrpgslgimtkj
bulfawcnyoqbudzrpgsleimtkj
bvhfajcnyoqkudzrpgsoeimtkj
bvhrakcnyoqxudzrpgsleimjkj
bvbftwcnyoqxuvzrpgsleimtkj
bvhfhwcnyoqxudzrpgslelmtbj
bvhyawcntoqxudzrpgsleimtuj
xvhuawcnyoqxuqzrpgsleimtkj
pvhfawcnyoqxudzdpglleimtkj
bvhfawsnyoqxudzrpgvlefmtkj
bvhfawcnyoqxudzrpgepeiwtkj
bvhfawcnyoqxudzrphsleittkr
dvhfawcnyoqxudzrpkslzimtkj
bvhfawpnyoqxudzrpgmlcimtkj
bvhsawcnyzqxudzrpgsaeimtkj
bdhfawcnyoqxudzrpasleiwtkj
bvhfawbnyoqxpdbrpgsleimtkj
mvhfawwnyoqxujzrpgsleimtkj
bvafawcnyoyxudzrpgsleidtkj
bvhyawcnyoqxudztpgzleimtkj
besfawcnyoqxudzrpgsleimdkj
bvhfawcnyoqxudrrpgsjeimjkj
xvhfkwcnyoqxudzcpgsleimtkj
bvhfawcnyeqdudzrpgzleimtkj
bvhfuwcnybqxudzrpgsleimttj
lvhfawcnyoqhudzdpgsleimtkj
bvhfawcnyoqxudzrpgslevwtnj
bvhfadcnzoqxxdzrpgsleimtkj
bvsfawcnyoqxpdzrpgileimtkj
bzhfaycnyoqxudzrpgsxeimtkj
bwhfdwcnyoqxudzrpgsleimtkz
bvhfawcnyoqxudzrpgsjlimtkm
bvhfawcnyoqxudsrwgsleimtlj
bbhfalynyoqxudzrpgsleimtkj
bvhfawcnyeqxudzrpglleimtkr
bvhfawnnboqxurzrpgsleimtkj
yvhfawcnyoqxudzrpgslzimtpj
bvhfjwcnyoqxqdxrpgsleimtkj
bthfawcnyoqfudzrpgslhimtkj
bvhfawchuoqxudzqpgsleimtkj
bvhfawcndoqxudzrugsleimrkj
bvhfawcnnoqxjdzrpgsleidtkj
bvhpawcnyoqkudzrpgsleimtzj
bvhfaiinyoqxudzopgsleimtkj
bvhfawcnyxqxuizrigsleimtkj
bvnfawcnyoqxudzqpgsleimbkj
bvnfawcnyoeyudzrpgsleimtkj
bvhfawcnyoqxudarpgsieimtoj
bthcawcnyoqxudlrpgsleimtkj
bvhfnwcnyozxudzrpgsleomtkj
bpwfawcnyoqxudzrpgskeimtkj
bvhfapcnyoqxudnrpgsxeimtkj
bvhfdwcnyoqxubzrxgsleimtkj
fvhfawcnyoqxjdzrpgsleirtkj
bvhfawcneoqxudzrvzsleimtkj
bvhaawcnyoqxudzrpgsleimtex
bvhfawcnyojxudvrpgsleimckj
bvlfawcnyoqxddzrpgsleimtko
bvhfawclfoqxudzrpgsleiktkj
bvhfawciyobxudzrpgkleimtkj
bvhfpwcnyoqxudzrpgsqeimtkd
bvhyawcnyyqxudzrkgsleimtkj
bvhfawcncoqxudzrphsaeimtkj
bvhfawmnyoqxudzrpgifeimtkj
bvhfawcjyoqxudzjpgszeimtkj
bohfawcnwoqxudzrpgsleimwkj
bvhfaucnyoqxudzrpgfluimtkj
bvhfawlnyoqgudzrpgwleimtkj
bmhfawcnyoqxndzrpgsleymtkj
bvhfawcngoqxudzrpzxleimtkj
bihfawcnyoqxudrrpgsleimokj
lvhfawcnylqxudzrpgsleintkj
bvhfawcnyoqvugzrqgsleimtkj
bvhfawcnyoqxudzgpgslqimtij
bvhfawcnyoqludzrpgslnimtcj
hvhfawcnyolxudzrpgsmeimtkj
nvhfawcdkoqxudzrpgsleimtkj
bvhfawcnyoqxkdzrggsneimtkj
bvhfawnnyoqxudzrpgqleibtkj
bvhfawyuyoqxudzrhgsleimtkj
wvhfbwcnyoqxtdzrpgsleimtkj
bvhfawcnyoqxedzzpgoleimtkj
bvhfawcnioqxunzrpgsleimtnj
bvhfawctyoqxudzrpgsldkmtkj
bvhfawcnyonxudzrpgsleitpkj
bvefawcnyoqaudzhpgsleimtkj
bvhfawcnyxqxudzrpgslelmbkj
bvhfamrnyoqxudzrpgsleimgkj
bvhfaqcnyoqxudzrpgsaeimekj
bvhfawcnyoqcidzrpgsleimvkj
bvhfawcnnorxudzrpgsmeimtkj
bvroawcnyoqxudzrpgsleiwtkj
bvhfwwcnyoqxudzrpaslewmtkj
bvsfawcnyoqxudzcpgszeimtkj
bkhfmwcnyoqjudzrpgsleimtkj
bvtfawcnyoqxudzrcgslecmtkj
bvhfawcnypzxudzrpgsleimtkv
bvhfawcnyoqzudzrfgtleimtkj
bvhpawcnyoqxudhrpgsleimtko
tvhfawcnyoqxudzxpfsleimtkj
bvhfawccyofxudzrpqsleimtkj
bvnfawtnyoqxuzzrpgsleimtkj
bvhfamcnuwqxudzrpgsleimtkj
bvhfawcfyoqxudjrpgsleimrkj
bvhpalcnyoqxudzrpgslexmtkj
bvhfawcnjsqxudzlpgsleimtkj
bvhfafcnioqxydzrpgsleimtkj
bvzfawcnyxqxudzgpgsleimtkj
bvhzawcnyoqxudzrpgslewctkj
bvhiawcnhoqrudzrpgsleimtkj
bvhfawcnyoqxuszrggslenmtkj
bvhfowcnyoqxudzrptseeimtkj
behfawfnyoqxudzrpgsleimlkj
lvhfawcnyoqxudsrpgvleimtkj
bvhfawnnyaqxudzrpgsqeimtkj
lvhfawcnfoqxvdzrpgsleimtkj
svhxawcnyoqxudzrpqsleimtkj
bvhfawqnfoqxudzrpgsleimkkj
bvhfafcnyoqcudzrpgsleimtcj
bvhfyfcntoqxudzrpgsleimtkj
bvhfpwcnyoqxudzrpgsleimumj
bvhfawccyoqxudzrqgrleimtkj
bvhfawqnyoqxudzbpgsleimkkj
bvhflwcnyoqxudzrpxsleemtkj
bvhfawcnyoqxuezrpgslehrtkj
bvhfawceyoqxudzrpgsleimswj
bvhfawcncohgudzrpgsleimtkj
bahfawcnyoqxgdzrpgsleamtkj
yvhfawcnyoqxudzrppslrimtkj
fvhfawcmyoqxudzrpgskeimtkj
bvylawsnyoqxudzrpgsleimtkj
bvhfswcnyyqxedzrpgsleimtkj
fvrfawcnyoqxudzrpgzleimtkj
bvhfawcnyoqxuvzrpgslermtks
bvhkawccyoqxudzcpgsleimtkj
bvhfaobnyoqxudzrprsleimtkj
bvbfawcnyoqxudirpgsleimhkj
bvhfawcnyoqxudzvpgsueimtgj
bvhxawcnyoqxudzrpgsleimtgi
svhfawcjyoqxuszrpgsleimtkj
bvnfawcnyoeyudzrpgsldimtkj
bvhfawcnyoqxuhzrpgsleimcki
bvhfvwcnyoqxudzizgsleimtkj
bvhfapznyohxudzrpgsleimtkj
bvhfaelnyosxudzrpgsleimtkj
xvhfawcnmoqxuhzrpgsleimtkj
bjhfawcnyaqxutzrpgsleimtkj
bvhfawcnyohxudzrpgslgnmtkj
bvhfawcnyoqxudzrppsreimtkx
fvhfapcnyoqyudzrpgsleimtkj
qvhfafcnyoqxudorpgsleimtkj
bvhfawcnyoqxedzrwgsleimtvj
bvhfawgnyoqxudzupgqleimtkj
bvhfowctyoqxudzrpgbleimtkj
bvhwawcnyoqxudzapgslvimtkj
bvhfadcnyoqxudzrugsleimtuj
bvhfawcnyosxudzlpgsleamtkj
bvhfawcnywqxuqzrpgsloimtkj
bvhfawcnyoqxumzrpgvlfimtkj
bvhfawcgyoqxbdzrpgsleomtkj
bvhfahcnyoqwudzrfgsleimtkj
gvbfawcnyrqxudzrpgsleimtkj
svhfawcnyoqxudlrpgsleimtkx
avhfafcnyoqxuhzrpgsleimtkj
bvhfawcsyoqxuazrpgsleimtej
bvofawcnyoqxudzrpgsteimtkf
bvhfajcnyoqxudzqpgszeimtkj
bvhfawcsyoqxudzrmgsleiktkj
mvhfawcnyoqxudzrpgkluimtkj
bvhfawcnhoqxudzrpgslwhmtkj
bmhaawsnyoqxudzrpgsleimtkj
bvhfawcnyoqxudzhpgsleimhyj
bvhfxwcnyoqxsdzypgsleimtkj
bvhpawcyyoqxuczrpgsleimtkj
bvomawcnyovxudzrpgsleimtkj
bvhfawcnjvqxudzrpgsleimtkt
nvhfawcnyqqxudzrpgsleittkj
bvhiawcnyzqxudzrpysleimtkj
bvhdawcnyoqxukzrpgsleimtuj
bvhfawcnyyxxudzrpgslzimtkj
hvhfawcnyoqxudzupgslemmtkj
byhfawknyoqxudzrpgsleimtkb
bvhfawcnyoqxudzrpasleihakj
bvafahcnyaqxudzrpgsleimtkj
bkhfawcnyoqxudzrpgllepmtkj
bghfawcnycqxuzzrpgsleimtkj
bvhfawcnyoqxudzrbgeleimtkl
bvhfascnyoqgudzrpgsveimtkj
bvhfawnnyoqxudzrpgsleimtdl
bvhqawcnyoqxudzrpgsleimgrj
bvhsawdwyoqxudzrpgsleimtkj
bvhfawcnyoqxudzrpgaleipttj
bvhfawcnrlqxudzrbgsleimtkj
bvhfdwcnyoqxudzqpcsleimtkj
bvhfawcnyoqxudzopgslexmokj
bvhfawcoyoqxudzrpghlewmtkj
bvhfozcnykqxudzrpgsleimtkj
bvhfawcnyoqxuvzrpgslrimtkr
bvhfrncnyoqrudzrpgsleimtkj
bvhfawcnyocxuizrpgslefmtkj
bvhfawywyoqxudzrpgsleimxkj
bvhfawcnyoqxugzrpgslrimtij
bvtfawcnyoqxudzcpgsleimtfj
bvhfawcnyoqxuzzspgsleimtkz
bvhfawcnzoqxvdzrpgslsimtkj
bvhfzwcnyoqxudzrpgslenmhkj
bvhfkccnyoqxudzrpgzleimtkj
bvhfawcnyoqzudzrpgslhimwkj
bzhfawvnyooxudzrpgsleimtkj"

let testInput = "abcdef
bababc
abbcde
abcccd
aabcdd
abcdee
ababab"

let occurrences (s: string) (c: char) =
    s
    |> List.ofSeq
    |> List.fold (fun acc c' -> if c' = c then acc + 1 else acc) 0

let calc (state: int * int) (input: string) =
    let ts = 
        input 
        |> List.ofSeq
        |> List.fold (fun acc char -> 
            let occ = occurrences input char
            if occ = 2 then (fst acc + 1, snd acc)
            elif occ = 3 then (fst acc, snd acc + 1)
            else acc
            ) (0, 0)

    ((if fst ts > 0 then fst state + 1 else fst state), 
     (if snd ts > 0 then snd state + 1 else snd state))

input.Split( '\r', '\n')
|> Seq.fold calc (0,0)
|> (fun occ -> fst occ * snd occ)