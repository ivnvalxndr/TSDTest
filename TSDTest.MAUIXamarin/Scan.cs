/*
using System.Xml;

namespace TSDTest.MAUIXamarin
{
    public static class Scan
    {
        public string Number { get; set; }

        public string CurrDate { get; set; }

        public Scan(string data)
        {
            this.Number = data;
            this.CurrDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
        }

        public void SaveToXml(string data)
        {
           // DateTime currentTime = DateTime.Now; // Текущее время

           // string currtime = currentTime.ToString("yyyy-MM-dd hh:mm:ss.fff"); // Строка с форматом времени

            if (Android.OS.Environment.ExternalStorageDirectory != null)
            {
                string filePath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath,
                    "data.xml");

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;

                // Проверяем, что файл по указанному пути существует
                if (!File.Exists(filePath))
                {
                    using (XmlWriter writer = XmlWriter.Create(filePath, settings))
                    {
                        // начинаем запись XML-кода
                        writer.WriteStartDocument();
                        writer.WriteStartElement("XMLScaner");

                        writer.WriteStartElement("scan");

                        // Номер штрих Кода
                        writer.WriteStartAttribute("number");
                        writer.WriteString(data);
                        writer.WriteEndAttribute();

                        // Дата
                        writer.WriteStartAttribute("date");
                        writer.WriteString(currtime);
                        writer.WriteEndAttribute();

                        // заканчиваем запись XML-кода
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                        writer.WriteEndDocument();

                    }
                }
                else
                {
                    XmlDocument xDoc = new XmlDocument();

                    xDoc.Load(filePath);

                    // создание нового элемента и добавление его к корневому элементу
                    XmlElement xRoot = xDoc.DocumentElement;

                    // создаем новый элемент
                    // XmlElement newScanElem = xDoc.CreateElement("scan");

                    // создаем новый элемент person
                    XmlElement newScanElem = xDoc.CreateElement("scan");

                    // создаем атрибут number
                    XmlAttribute numberAttr = xDoc.CreateAttribute("number");
                    //XmlAttribute dateAttr = xDoc.CreateAttribute("date");
                    //XmlText dataText = xDoc.CreateTextNode(data);

                    //numberAttr.AppendChild(dataText);

                    newScanElem.Attributes.Append(numberAttr);
                    //newScanElem.Attributes.Append(dateAttr);

                    xRoot.AppendChild(newScanElem);
                    //xDoc.AppendChild(newScanElem);#2#

                    // xRoot.InsertAfter(newScanElem, );

                    xDoc.AppendChild(xRoot);

                    // сохранение изменений в файл
                    xDoc.Save(filePath);
                }
            }
        }
    }
}
*/
