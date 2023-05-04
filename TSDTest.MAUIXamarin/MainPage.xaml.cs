namespace TSDTest.MAUIXamarin {
    public partial class MainPage : ContentPage 
    {
        int count = 0;

        public MainPage() 
        {
            InitializeComponent();
            ScanningTransmitterTest.Notify += TestNotify;
        }

        private void TestNotify(string scanData) 
        {
            textScan.Text = scanData;
            //textScan.Text = "hello";
        }
        private void OnCounterClicked(object sender, EventArgs e) 
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}



/*
// импортируем функции из библиотеки
[DllImport("NativeInterface.dll", EntryPoint = "StartScanner")]
public static extern void StartScanner(IntPtr handle, NativeScanCallback callback);

[DllImport("NativeInterface.dll", EntryPoint = "StopScanner")]
public static extern void StopScanner();

// определяем делегат для обработки события скана
public delegate void ScanCallbackDelegate(IntPtr data, int length);

// реализуем обработчик события скана
public void ScanCallback(IntPtr data, int length)
{
    // преобразуем данные в строку
    string barcode = Marshal.PtrToStringAnsi(data, length);

    // использование строки-штрихкода в своих целях
    // ...
}

// запускаем сканирование при запуске приложения
StartScanner(this.Handle, new NativeScanCallback(ScanCallback));

// останавливаем сканирование при закрытии приложения
private void Form1_FormClosing(object sender, FormClosingEventArgs e)
{
    StopScanner();
}
*/
