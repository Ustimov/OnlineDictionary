namespace OnlineDictionary

open Xamarin.Forms
open Xamarin.Forms.Xaml

type OnlineDictionaryPage() =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<OnlineDictionaryPage>)
