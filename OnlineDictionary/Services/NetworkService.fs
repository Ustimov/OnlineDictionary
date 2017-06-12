namespace OnlineDictionary

open Newtonsoft.Json
open System.Net
open System.Net.Http

type NetworkService private() = 
    let apiKey = "API_KEY"
    let baseUrl = "https://dictionary.yandex.net/api/v1/dicservice.json"
    let client = new HttpClient()

    static let mutable instance = lazy(NetworkService())
    static member Instance with get() = instance.Value

    member this.Lookup(lang: string, text: string) = 
        async {
            try
                let! response = client.GetAsync(sprintf "%s/lookup?key=%s&lang=%s&text=%s&ui=ru" baseUrl apiKey lang text) |> Async.AwaitTask
                match response.StatusCode with
                | HttpStatusCode.OK ->
                    let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
                    let translation = JsonConvert.DeserializeObject<LookupModel>(content)
                    return Some(translation)
                | _ -> return None
            with
            | _ -> return None
        }
    
   