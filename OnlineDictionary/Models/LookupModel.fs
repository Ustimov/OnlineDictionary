namespace OnlineDictionary

type LookupModel() = 
    let definitions = ResizeArray<DefinitionModel>()

    member val Def: TranslationModel[] = [||] with get, set

    member this.Ok() = this.Def.Length > 0

    member this.Translation
       with get() = if this.Ok() then this.Def.[0].Tr.[0].Text else ""

    member this.Transcription
       with get() = if this.Ok() then this.Def.[0].Ts else ""

    member this.Definitions 
       with get() =
           match definitions.Count with
           | 0 when this.Ok() ->
               for definition in this.Def do
                   let mutable counter = 0
                   for translation in definition.Tr do
                       counter <- counter + 1
                       let definitionModel = DefinitionModel()
                       definitionModel.Number <- counter
                       definitionModel.PartOfSpeech <- translation.Pos
                       let mutable mainTranslation = translation.Text
                       if translation.Gen <> "" then 
                           mainTranslation <- mainTranslation + sprintf " (%s)" translation.Gen
                       definitionModel.Synonyms <-
                           translation.Syn
                           |> Seq.fold (fun state item ->
                               let res = sprintf "%s, %s" state item.Text
                               if item.Gen = "" then res else res + sprintf " (%s)" item.Gen) mainTranslation
                       let mutable means = translation.Mean
                                           |> Seq.fold (fun state item -> sprintf "%s, %s" state item.Text) ""
                       if means.Length > 2 then
                           means <- means.Substring(2)
                           definitionModel.Means <- sprintf "(%s)" means
                       definitionModel.Examples <-
                          translation.Ex
                          |> Seq.fold (fun state item -> sprintf "%s\n\t%s - %s" state item.Text item.Tr.[0].Text) ""
                       if definitionModel.Examples.Length > 2 then
                          definitionModel.Examples <- definitionModel.Examples.Substring(1)
                       definitions.Add(definitionModel)
               definitions
           | _ -> definitions
