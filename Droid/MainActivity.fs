namespace OnlineDictionary.Droid
open System

open Android.App
open Android.Content
open Android.Content.PM
open Android.Runtime
open Android.Views
open Android.Widget
open Android.OS

type Resources = OnlineDictionary.Droid.Resource

[<Activity (Label = "Словарь", Icon = "@drawable/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = (ConfigChanges.ScreenSize ||| ConfigChanges.Orientation))>]
type MainActivity() =
    inherit Xamarin.Forms.Platform.Android.FormsAppCompatActivity()
    override this.OnCreate (bundle: Bundle) =
        MainActivity.TabLayoutResource <- Resources.Layout.Tabbar
        MainActivity.ToolbarResource <- Resources.Layout.Toolbar

        base.OnCreate (bundle)

        Xamarin.Forms.Forms.Init (this, bundle)

        this.LoadApplication (new OnlineDictionary.App ())

