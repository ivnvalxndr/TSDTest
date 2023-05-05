using System.Runtime.InteropServices;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using TSDTest.MAUIXamarin.Platforms.Android;


namespace TSDTest.MAUIXamarin 
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity 
    {
        private static string _scanActionName = "android.intent.ACTION_DECODE_DATA"; //_scanActionName - название события, которое мы ловим в шине.
        BroadcastReceiverTest receiver;

        private static string[] PERMISSIONS_STORAGE = 
        {
            Android.Manifest.Permission.ReadExternalStorage,
            Android.Manifest.Permission.WriteExternalStorage
        };

        protected override void OnCreate(Bundle savedInstanceState) 
        {
            base.OnCreate(savedInstanceState);
            receiver = new BroadcastReceiverTest();
        }

        protected override void OnResume() 
        {
            base.OnResume();
            RegisterReceiver(receiver, new IntentFilter(_scanActionName));
        }

        protected override void OnPause() 
        {
            UnregisterReceiver(receiver);
            base.OnPause();
        }

    }
}