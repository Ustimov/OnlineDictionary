namespace OnlineDictionary

open Xamarin.Forms
open Xamarin.Forms.Xaml

type SettingsPage() as this =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<SettingsPage>)
    do base.BindingContext <- SettingsPageViewModel(this, NetworkService.Instance, DataService.Instance)
