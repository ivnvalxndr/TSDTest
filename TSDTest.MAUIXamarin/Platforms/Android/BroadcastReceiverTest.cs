using Android.Content;

namespace TSDTest.MAUIXamarin.Platforms.Android {
    public class BroadcastReceiverTest : BroadcastReceiver {

        public override void OnReceive(Context context, Intent intent) {
            var code = intent.GetStringExtra("scannerdata");
            Console.WriteLine(code);
            ScanningTransmitterTest.TransmitScanData(code);
        }
    }
}
