using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Xml.Serialization;

namespace TSDTest.MAUIXamarin
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        // Храним отсканированный ШК
        public static string Barcode { get; set; }

        private bool reWriteCode = false; // Перезапись ШК (Перезаписываем ШК)

        // Путь корню внешнего хранилища
        public static string externalStorageDirectoryPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;

        // путь до созданной папки
        public static string newDirectoryPath = Path.Combine(externalStorageDirectoryPath, "Scans");

        // Путь до созданного файла
        public static string filePath = Path.Combine(newDirectoryPath, "ScanData.xml");

        public MainPage()
        {
            InitializeComponent();

            ScanningTransmitterTest.Notify += TestNotify;

            // Если папки Scans нет - то создаем
            if (!Directory.Exists(newDirectoryPath))
            {
                Directory.CreateDirectory(newDirectoryPath);
            }
        }

        // Метод в котором получаем ШК
        private void TestNotify(string scanData)
        {
            textScan.Text = scanData; // Выводим надпись на экран ШК

            Barcode = scanData;

            // Метод который начнет работать с XML
            FromToXml(scanData);

        }

        // Метод для высплывающего окна
        async void Alert()
        {
            bool result = await DisplayAlert("Подтвердить действие", $"Штрих-Код: {Barcode} уже был отсканирован\n Cохранить Штрих-Код?", "Да", "Нет");

            await DisplayAlert("Уведомление", "Вы выбрали: " + (result ? "Сохранить ШК" : "Не сохранять"), "OK");

            if (result)
            {
                reWriteCode = true; // Свитч на то, что перезаписываем ШК
                FromToXml(Barcode);
            }
        }

        // Метод для формирования XML
        private void FromToXml(string scanData)
        {
            bool fileExist = File.Exists(filePath);  // Проверка существует ли файл

            if (fileExist)
            {
                var scans = ReadScanLoop(); // получаем наши сканы (Десериализуем)

                // Нужно ли перезаписывать существущий ШК?
                if (!reWriteCode)
                {
                    // Проверка на существующий ШК 
                    foreach (var item in scans)
                    {
                        if (item.Number == scanData)
                        {
                            Alert();
                            return;
                        }
                    }
                }

                // Добавляем в нашу коллекцию новый скан
                scans.Add(
                            new Scan()
                            {
                                Number = scanData,
                                CurrDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")
                            });

                WriteScanLoop(scans); // Пишем XML
            }
            // Если файл не создан
            else
            {
                var Scans = new List<Scan>
                {
                   new Scan()
                   {
                       Number = scanData,
                       CurrDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")
                   }
                };
                WriteScanLoop(Scans);
            }
        }

        // Запись данных в файл XML
        private void WriteScanLoop(List<Scan> value)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Scan>));

            // сохранение массива в файл
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, value);
            }

            reWriteCode = false; // Выключаем перезапись
        }

        // Чтение данных с файла XML (Если файл отсутствует, возвращаем пустой объект)
        private List<Scan> ReadScanLoop()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Scan>));

            bool fileExist = File.Exists(filePath);
            if (fileExist)
            {
                var scaner = new List<Scan>();

                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    scaner = formatter.Deserialize(fs) as List<Scan>;
                }

                return scaner;
            }

            var scan = new List<Scan>();

            return scan; // Возвращаем пустой объект, файл отсутствует
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

