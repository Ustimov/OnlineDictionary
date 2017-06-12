namespace OnlineDictionary

open Xamarin.Forms

type App() =
    inherit Application(MainPage = RootPage())

    override this.OnStart() =
        async {
            do! DataService.Instance.Load()
        } |> Async.StartImmediate
    
    override this.OnSleep() =
        async {
            do! DataService.Instance.Store()
        } |> Async.StartImmediate