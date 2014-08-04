using CWLOTPH.DataObject;
namespace CWLOTPH.Process
{
    interface IExperiment
    {
        Experiment.State GetState { get; set; }
        OutputDataObject GetOutdataObject { get; set; }
        void PauseExperiment();
        void StartExperiment();
        void StopExperiment();
    }
}
