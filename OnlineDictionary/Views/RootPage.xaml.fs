namespace OnlineDictionary

open Xamarin.Forms
open Xamarin.Forms.Xaml

type RootPage() as this =
    inherit TabbedPage()
    let _ = base.LoadFromXaml(typeof<RootPage>)
    do MessagingCenter.Subscribe<LookupModel>(this, "ShowHistoryLookup", fun _ -> this.CurrentPage <- this.Children.[0])
