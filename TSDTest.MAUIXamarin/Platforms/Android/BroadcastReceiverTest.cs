using Android.Content;

namespace TSDTest.MAUIXamarin.Platforms.Android {
    public class BroadcastReceiverTest : BroadcastReceiver
    {
        private const string QR_ACTION = "android.intent.ACTION_DECODE_DATA";
        private const string QR_EXTRA = "barcode_string";


        public override void OnReceive(Context context, Intent intent) 
        {
            try
            {
                if (QR_ACTION == intent.Action)
                {
                    if (intent.HasExtra(QR_EXTRA))
                    {
                        string code = intent.GetStringExtra(QR_EXTRA);
                        ScanningTransmitterTest.TransmitScanData(code);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            /*var code = intent.GetStringExtra("scannerdata");
            Console.WriteLine(code);
            ScanningTransmitterTest.TransmitScanData(code);*/
        }
    }
}
