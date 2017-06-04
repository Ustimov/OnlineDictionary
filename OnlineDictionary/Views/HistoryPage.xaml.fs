namespace OnlineDictionary

open Xamarin.Forms
open Xamarin.Forms.Xaml

type HistoryPage() as this =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<HistoryPage>)
    let viewModel = HistoryPageViewModel(this, NetworkService.Instance, DataService.Instance)
    do base.BindingContext <- viewModel

    override this.OnAppearing() = 
        viewModel.RefreshHistory()
