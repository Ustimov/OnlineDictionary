namespace OnlineDictionary

open System.ComponentModel
open Xamarin.Forms

type DictionaryPageViewModel(page: Page, networkService: NetworkService, dataService: DataService) as this =
    let event = Event<_, _>()
    let mutable lookup = LookupModel()
    let mutable oldInput = ""
    let mutable isLoading = false
    let mutable fromLanguage = LanguageModel("Английский", "en")
    let mutable toLanguage = LanguageModel("Русcкий", "ru")

    do MessagingCenter.Subscribe<HistoryPage, LookupModel>(this, "ShowHistoryLookup", fun sender lookup -> this.Lookup <- lookup)

    interface INotifyPropertyChanged with
       [<CLIEvent>]
       member this.PropertyChanged = event.Publish
    
    member val Input = "" with get, set

    member this.OldInput
       with get() = oldInput
       and set(value) =
           oldInput <- value
           event.Trigger(this, PropertyChangedEventArgs("OldInput"))

    member this.Lookup
        with get() = lookup
        and set(value) = 
            lookup <- value
            event.Trigger(this, PropertyChangedEventArgs("Lookup"))

    member this.IsLoading
        with get() = isLoading
        and set(value) = 
            isLoading <- value
            event.Trigger(this, PropertyChangedEventArgs("IsLoading"))

    member this.From
       with get() = fromLanguage
       and set(value) =
           fromLanguage <- value
           event.Trigger(this, PropertyChangedEventArgs("From"))

    member this.To
       with get() = toLanguage
       and set(value) =
           toLanguage <- value
           event.Trigger(this, PropertyChangedEventArgs("To"))

    member this.TranslateCommand = 
        Command(fun () -> 
            async {
                this.IsLoading <- true
                let! lookupResult = networkService.Lookup(sprintf "%s-%s" this.From.Code this.To.Code, this.Input)
                this.IsLoading <- false
                this.OldInput <- this.Input
                match lookupResult with
                | None -> do! page.DisplayAlert("Ошибка", "Не удалось получить перевод", "Закрыть") |> Async.AwaitTask
                | Some(lookupModel) ->
                    if lookupModel.Ok() then
                        dataService.SaveLookup(lookupModel)
                    this.Lookup <- lookupModel
            } |> Async.StartImmediate)

    member this.ChangeTranslationDirectionCommand = 
        Command(fun () ->
            let tmp = this.To
            this.To <- this.From
            this.From <- tmp)
