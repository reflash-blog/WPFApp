using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CWLOTPH.DataObject;
using CWLOTPH.Process;
using System.Threading;

namespace CWLOTPH.UnitTesting
{
    [TestClass]
    public class ExperimentTest
    {
        [TestMethod]
        public void ExperimentWorkTest()
        {
            Experiment experiment = Experiment.InitExperiment(null);
            experiment.StartExperiment();
            Assert.AreEqual(Experiment.State.Null, experiment.GetState);
            Thread.Sleep(0);
            Assert.AreEqual(Experiment.State.Ended, experiment.GetState);
        }
    }
}
