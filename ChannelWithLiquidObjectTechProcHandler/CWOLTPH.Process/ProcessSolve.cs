using System;
using CWLOTPH.DataObject;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;

namespace CWLOTPH.Process
{
    public class ProcessSolve : IProcessSolve
    {
        public OutputDataObject Process(InputDataObject inDObj)
        {
            var timer = new Stopwatch();
            timer.Start();

            var filename = OutputForProcessingCore(inDObj);
            CallProcessingCore(filename);                    // Вызов обрабатывающего ядра
            var jsonString = InputProcessingCoreResult(filename+".result");
            var outDataObject = ParseJsonString(jsonString);
            timer.Stop();
            outDataObject.tickCount = Convert.ToInt32(timer.ElapsedMilliseconds);
            return outDataObject;
        }



        private static string OutputForProcessingCore(InputDataObject inDObj)
        {
            var json = JsonConvert.SerializeObject(inDObj);
            const string filename = "data.json";
            if (File.Exists(filename)) File.Delete(filename);
            using (var swt = new StreamWriter(new FileStream(filename,  // Вывод в файл
                FileMode.OpenOrCreate, FileAccess.Write)))
            {
                swt.Write(json);
                swt.Close();
            }
            return filename;
        }

        private static void CallProcessingCore(string filename)
        {
            var process = new System.Diagnostics.Process();

            var startInfo = new ProcessStartInfo
            {
                FileName = AppDomain.CurrentDomain.BaseDirectory +
                           "\\CWLOTPH.ProcessingCore.exe",
                Arguments = filename
            };


            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }

        private static string InputProcessingCoreResult(string filename)
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
        private static OutputDataObject ParseJsonString(string jsonString)
        {
            var outDObj = JsonConvert.DeserializeObject<OutputDataObject>(jsonString);
            return outDObj;
        }
    }

}
