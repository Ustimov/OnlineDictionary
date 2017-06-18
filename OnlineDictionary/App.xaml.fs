namespace OnlineDictionary

open Microsoft.Azure.Mobile
open Microsoft.Azure.Mobile.Analytics
open Microsoft.Azure.Mobile.Crashes
open Xamarin.Forms

type App() =
    inherit Application(MainPage = RootPage())

    do MobileCenter.Start("API_KEY", typeof<Analytics>, typeof<Crashes>);

    override this.OnStart() =
        async {
            do! DataService.Instance.Load()
        } |> Async.StartImmediate
    
    override this.OnSleep() =
        async {
            do! DataService.Instance.Store()
        } |> Async.StartImmediate