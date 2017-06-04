namespace OnlineDictionary

type DataService private() =
    let lookups = ResizeArray<LookupModel>()
    
    static let instance = lazy(DataService())
    static member Instance with get() = instance.Value

    member this.Load() = ()

    member this.Store() = ()

    member this.Clear() =
        lookups.Clear()

    member this.SaveLookup(lookup: LookupModel) = 
        lookups.Insert(0, lookup)

    member this.Lookups with get() = lookups
