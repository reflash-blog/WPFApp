using System.IO;
using Newtonsoft.Json;
using CWLOTPH.DataObject;

namespace CWLOTPH.Visuality
{
    public class FileSystemInteraction
    {
        private static string _currentlyOpenedFile = "Untitled.ctph";

        public static void SaveFile(InputDataObject inpDObj)
        {
            if (inpDObj == null) return;
            if(_currentlyOpenedFile != "Untitled.ctph")
            {
                OutputObjectToFile(inpDObj, _currentlyOpenedFile);
            }
            else
            {
                SaveAsFile(inpDObj);
            }
        }

        public static void SaveAsFile(InputDataObject inpDObj)
        {
            var sdg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "Document",
                DefaultExt = ".ctph",
                Filter = "CWLOTPH Data (.ctph)|*.ctph"
            };

            // Show save file dialog box
            var result = sdg.ShowDialog();

            // Process save file dialog box results
            if (result != true) return;
            // Save document
            var filename = sdg.FileName;
            OutputObjectToFile(inpDObj, filename);
            _currentlyOpenedFile = filename;
        }

        public static InputDataObject OpenFile()
        {
            InputDataObject inpDObj = null;
            if(_currentlyOpenedFile=="Untitled.ctph")
            {
                inpDObj = openFileDialogHandler();
            }else{
                MessageWindow.Show("Вы действительно хотите открыть другой файл? Предыдущие данные будут утеряны. ОК- открыть файл");
                if(MessageWindow.Result == MessageWindow.MessageResult.Ok)
                {
                    inpDObj = openFileDialogHandler();
                }
            }

            return inpDObj;
        }

        private static InputDataObject openFileDialogHandler()
        {
            var opfd = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "Document",
                DefaultExt = ".ctph",
                Filter = "CWLOTPH Data (.ctph)|*.ctph"
            };

            // Show save file dialog box
            var result = opfd.ShowDialog();

            // Process save file dialog box results
            if (result != true) return null;
            // Save document
            var filename = opfd.FileName;
            var inpDObj = JsonConvert.DeserializeObject<InputDataObject>(InputObjectFromFile(filename));
            _currentlyOpenedFile = filename;
            return inpDObj;
        }

        public static int NewFile()
        {
            if(_currentlyOpenedFile != "Untitled.ctph")
            {
                MessageWindow.Show("Вы действительно хотите открыть другой файл? Предыдущие данные будут утеряны. ОК- открыть файл");
                if (MessageWindow.Result != MessageWindow.MessageResult.Ok) return 0;
                _currentlyOpenedFile = "Untitled.ctph";
                return 1;
            }
            _currentlyOpenedFile = "Untitled.ctph";
            return 1;
        }

        private static void OutputObjectToFile(InputDataObject inDObj, string filename)
        {
            if (File.Exists(filename)) File.Delete(filename);
            var json = JsonConvert.SerializeObject(inDObj);
            using (var swt = new StreamWriter(new FileStream(filename,  // Вывод в файл
                FileMode.OpenOrCreate, FileAccess.Write)))
            {
                swt.Write(json);
                swt.Close();
            }
        }

        private static string InputObjectFromFile(string filename)
        {
            string json;
            using (var srd = new StreamReader(new FileStream(filename,  // Вывод в файл
                FileMode.Open, FileAccess.Read)))
            {
                json = srd.ReadToEnd();
                srd.Close();
            }
            return json;
        }
    }
}
