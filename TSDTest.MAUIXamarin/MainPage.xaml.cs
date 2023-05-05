using System.Runtime.InteropServices;
//using System.IO;

namespace TSDTest.MAUIXamarin {
    public partial class MainPage : ContentPage 
    {
        int count = 0;

        private string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public MainPage() 
        {
            InitializeComponent();
            ScanningTransmitterTest.Notify += TestNotify;
        }

        // Метод который выводит отсканированный ШК
        private void TestNotify(string scanData) 
        {
            textScan.Text = scanData;
            //SaveToCSVFile(scanData);
            SaveFileToExternalStorage(scanData);
        }


        public async Task SaveFileToExternalStorage(string data)
        {
            var storageStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();
            if (storageStatus == PermissionStatus.Granted)
            {
                var file = Path.Combine(FileSystem.CacheDirectory, "data.txt");
                var fileStream = new StreamWriter(file);
                //fileStream.WriteLine(data);
                await fileStream.WriteLineAsync(data.Replace(",", ";"));
                fileStream.Close();

                var externalStorage = FileSystem.AppDataDirectory;
                var destination = Path.Combine(externalStorage, "data.txt");

                File.Copy(file, destination, true);

                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(destination)
                });
            }
        }


        // Метод который пишет в CsvFile
        //public static void SaveToCSVFile(string data)
        public async Task SaveToCSVFile(string data)
        {

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "data.txt");

            //string filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "count.txt");

            // Проверяем, что файл по указанному пути существует
            if (!File.Exists(filePath))
            {
                // Если файл не существует, создаем его
                File.Create(filePath).Close();
            }
            /*else
            {
                // запрашиваем разрешение на перезапись
                bool isRewrited = await DisplayAlert("Подтверждение", "Файл уже существует, перезаписать его?", "Да",
                    "Нет");
                if (isRewrited == false) return;
            }*/

            // Открываем файл для записи
            using (StreamWriter writer = File.AppendText(filePath))
            {
                // Записываем строку в файл, разделяя ее значениями ";"
                //writer.WriteLine(data.Replace(",", ";"));
                await writer.WriteLineAsync(data.Replace(",", ";"));
            }


            //--------------------------
            /*// Определение пути файла и его имени
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "data.csv");

            // Открытие файла на запись
            using (var writer = new StreamWriter(path))
            {
                // Запись строки в файл
                writer.WriteLine(data);
            }*/
        }

        private void OnCounterClicked(object sender, EventArgs e) 
        {
            count++;

            //SaveToCSVFile("keeek");


            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        // реализуем обработчик события скана
        /*public void ScanCallback(object sender, EventArgs e)
        {
            // преобразуем данные в строку
            //string barcode = Marshal.PtrToStringAnsi(data, length);

            // использование строки-штрихкода в своих целях
            // ...

            //Scan.Text = barcode;

            ScanningTransmitterTest.Notify += TestNotify;

            SemanticScreenReader.Announce(Scan.Text);

            //Console.WriteLine(barcode);
        }*/
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
