namespace OnlineDictionary

type DefinitionModel() = 
    member val PartOfSpeech = "" with get, set
    member val Number = -1 with get, set
    member val Synonyms = "" with get, set
    member val Means = "" with get, set
    member val Examples = "" with get, set
