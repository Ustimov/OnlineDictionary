namespace OnlineDictionary

type TranslationModel() = 
    member val Text = "" with get, set
    member val Ts = "" with get, set
    member val Pos = "" with get, set
    member val Tr: TranslationModel[] = [||] with get, set
    member val Syn: TranslationModel[] = [||] with get, set
    member val Mean: TranslationModel[] = [||] with get, set
    member val Ex: TranslationModel[] = [||] with get, set
    member val Gen = "" with get, set
