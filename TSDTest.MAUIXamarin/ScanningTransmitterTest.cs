namespace TSDTest.MAUIXamarin; 
public static class ScanningTransmitterTest
{
    public delegate void ScanHandler(string scanData);

    public static event ScanHandler Notify;
    public static void TransmitScanData(string scanData) 
    {
        Notify?.Invoke(scanData);
    }
}
  