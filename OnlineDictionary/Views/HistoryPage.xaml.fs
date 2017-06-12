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

    member this.ListViewItemSelected(sender: obj, args: SelectedItemChangedEventArgs) =
        if args.SelectedItem <> null then
           let lookup = args.SelectedItem :?> LookupModel
           MessagingCenter.Send(this, "ShowHistoryLookup", lookup)
           (sender :?> ListView).SelectedItem <- null

