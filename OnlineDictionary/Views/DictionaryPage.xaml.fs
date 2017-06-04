namespace OnlineDictionary

open Xamarin.Forms
open Xamarin.Forms.Xaml

type DictionaryPage() as this =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<DictionaryPage>)
    do base.BindingContext <- DictionaryPageViewModel(this, NetworkService(), DatabaseService())
