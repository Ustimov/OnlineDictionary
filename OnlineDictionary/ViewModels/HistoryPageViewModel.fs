namespace OnlineDictionary

open System.Collections.ObjectModel
open Xamarin.Forms

type HistoryPageViewModel(page: Page, networkService: NetworkService, dataService: DataService) =
    let lookups = ObservableCollection<LookupModel>()

    member this.ClearCommand = 
        Command(fun () ->
            lookups.Clear()
            dataService.Clear())

    member this.RefreshHistory() = 
        lookups.Clear()
        for lookup in dataService.Lookups do
            lookups.Add(lookup)

    member this.Lookups with get() = lookups
