namespace OnlineDictionary

open Xamarin.Forms
open Xamarin.Forms.Xaml

type HistoryPage() =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<HistoryPage>)
