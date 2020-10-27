using System;
using Xamarin.Essentials;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.Security;

namespace GUI.Droid
{
    [Activity(Label = "GUI", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);


            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            LoadApplication(new App());
            InitPerms();
        }

        private async void InitPerms()
        {
            var camStatus = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.Camera>();
            var torchStatus = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.Flashlight>();

            if(camStatus != PermissionStatus.Granted)
            {
                if (await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.Camera>() != PermissionStatus.Granted)
                    throw new Exception("camera needs to become disabled");
            }

            if(torchStatus != PermissionStatus.Granted)
            {
                if (await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.Flashlight>() != PermissionStatus.Granted)
                    throw new Exception("camera needs to become disabled");
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            ZXing.Net.Mobile.Forms.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}