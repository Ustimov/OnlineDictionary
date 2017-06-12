namespace OnlineDictionary

open System
open Xamarin.Forms

type SettingsPageViewModel(page: Page, networkService: NetworkService, dataService: DataService) = 
    let yandexUrl = "http://api.yandex.ru/dictionary/"

    member this.OpenYandexUrlCommand = 
        Command(fun () -> Device.OpenUri(Uri(yandexUrl)))