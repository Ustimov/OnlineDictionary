namespace OnlineDictionary

open PCLStorage
open Newtonsoft.Json

type DataService private() =
    let lookups = ResizeArray<LookupModel>()
    let fileName = "history"
    
    static let instance = lazy(DataService())
    static member Instance with get() = instance.Value

    member this.Load() = 
        async {
            let rootFolder = FileSystem.Current.LocalStorage
            let! existanceCheck = rootFolder.CheckExistsAsync(fileName) |> Async.AwaitTask
            if existanceCheck = ExistenceCheckResult.FileExists then
                let! file = rootFolder.GetFileAsync(fileName) |> Async.AwaitTask
                let! content = file.ReadAllTextAsync() |> Async.AwaitTask
                let history = JsonConvert.DeserializeObject<ResizeArray<LookupModel>>(content)
                lookups.AddRange(history)
        }

    member this.Store() = 
        async {
            let rootFolder = FileSystem.Current.LocalStorage
            let! file = rootFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting) |> Async.AwaitTask
            do! file.WriteAllTextAsync(JsonConvert.SerializeObject(lookups)) |> Async.AwaitTask
        }

    member this.Clear() =
        lookups.Clear()

    member this.SaveLookup(lookup: LookupModel) = 
        lookups.Insert(0, lookup)

    member this.Lookups with get() = lookups
