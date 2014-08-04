using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CWLOTPH.Process;
using CWLOTPH.DataObject;
using System.IO;
using System.Windows.Forms;

namespace CWLOTPH.UnitTesting
{
    [TestClass]
    public class ProcessSolveTest
    {
        [TestMethod]
        public void outPutFileTest()
        {
            string json;
            string filename;
            InputDataObject inp = new InputDataObject();

            ProcessSolve pslv = new ProcessSolve();
            pslv.Process(inp);
            filename = System.Reflection.Assembly.GetExecutingAssembly().Location + "data.json";

            using (StreamReader srt = new StreamReader(new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read)))
            {
                json = srt.ReadToEnd(); ;
                srt.Close();
            }
            Assert.AreEqual(json, "");
        }
    }
}
