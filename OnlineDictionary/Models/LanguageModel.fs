namespace OnlineDictionary

type LanguageModel(name: string, code: string) =
    member val Name = name with get, set
    member val Code = code with get, set
