namespace OnlineDictionary

open System.ComponentModel
open System.Collections.ObjectModel
open Xamarin.Forms

type DictionaryPageViewModel(page: Page, networkService: NetworkService, databaseService: DatabaseService) =
    let event = Event<_, _>()
    let mutable lookup = LookupModel()
    let mutable oldInput = ""
    //let mutable text = ""
    //let mutable transcription = ""
    //let mutable translation = ""
    //let definitions = ObservableCollection<TranslationModel>()

    //let setTranslation() =
    //    if lookup.Def.Length > 0 then
    //        this.Text <- lookup.Def.[0].Text
    //        this.Transcription <- lookup.Def.[0].Ts
    //        definitions.Clear()
    //        for definition in lookup.Def do
    //            definitions.Add(definition)

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
  
    //member this.Text
    //   with get() = text
    //   and set(value) = 
    //       text <- value
    //       event.Trigger(this, PropertyChangedEventArgs("Text"))
    
    //member this.Transcription
    //   with get() = transcription
    //   and set(value) =
    //       transcription <- value
    //       event.Trigger(this, PropertyChangedEventArgs("Transcription"))

    //member this.Translation
    //   with get() = translation
    //   and set(value) =
    //       translation <- value
    //       event.Trigger(this, PropertyChangedEventArgs("Translation"))

    //member this.Definitions = definitions

    member this.TranslateCommand = 
        Command(fun () -> 
            async {
                this.OldInput <- this.Input
                let! lookupResult = networkService.Lookup("en-ru", this.Input)
                match lookupResult with
                | None -> do! page.DisplayAlert("Ошибка", "Не удалось получить перевод", "Закрыть") |> Async.AwaitTask
                | Some(lookupModel) -> this.Lookup <- lookupModel//; setTranslation()
            } |> Async.StartImmediate)
