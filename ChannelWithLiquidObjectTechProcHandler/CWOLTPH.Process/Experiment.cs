using System.Threading;
using CWLOTPH.DataObject;

namespace CWLOTPH.Process
{
    public class Experiment : CWLOTPH.Process.IExperiment
    {
        readonly Thread _experimentThread;                                       // Поток эксперимента
        State _state;                                                   // Состояние эксперимента
        readonly InputDataObject _indataObject;                                  // Входные данные эксперимента
        OutputDataObject _outdataObject;                                // Выходные данные эксперимента

        static Experiment _experiment = null;
        // // // // // 
        // public enum State
        //
        // enum показывает текущее состояние эксперимента
        //
        // NULL - пустой эксперимент
        // PROCESSING - в стадии обработки
        // PAUSED - пауза
        // ENDED - завершен, можно получить outdataObject из буффера
        // // // // //
        public enum State
        {
            Null,Processing,Paused,Ended
        }

        private Experiment(InputDataObject inDataObject)
        {
            _indataObject = inDataObject;                              // Ввод объекта данных    
            _experimentThread = new Thread(ExperimentCycle);           // Инициализация потока
            _state = State.Null;                                       // Состояние NULL
            _outdataObject = null;
        }
        // // // // // 
        // public void initExperiment(InputDataObject inDataObject)
        //
        // Функция инициализирует новый эксперимент
        //
        // // // // //
        public static Experiment InitExperiment(InputDataObject inDataObject)
        {
            if(_experiment==null)
            {
                _experiment = new Experiment(inDataObject);
            }

            return _experiment;
        }

        // // // // // 
        // public void startExperiment()
        //
        // Функция стартует новый эксперимент
        //
        // // // // //
        public void StartExperiment()
        {
            if(_state==State.Null)
            {
                _experimentThread.Start();                            // Старт потока
            }else
            {
#pragma warning disable 618
                _experimentThread.Resume();                           // Продолжение работы потока
#pragma warning restore 618
            }
        }

        // // // // // 
        // public void experimentCycle()
        //
        // Функция цикла эксперимента
        //
        // // // // //
        private void ExperimentCycle()
        {
            IProcessSolve procSolve = new ProcessSolve();            // Создаем объект решения
            _outdataObject = procSolve.Process(_indataObject);        // Получаем решение
            _state = State.Ended;                                    // ENDED
        }

        // // // // // 
        // public void pauseExperiment()
        //
        // Функция паузы эксперимента
        //
        // // // // //
        public void PauseExperiment()
        {
            if(_state == State.Processing)
            {
                _state = State.Paused;                               // PAUSED
#pragma warning disable 618
                _experimentThread.Suspend();                         // Пауза потока
#pragma warning restore 618
            }
        }

        // // // // // 
        // public void stopExperiment()
        //
        // Функция остановки эксперимента
        //
        // // // // //
        public void StopExperiment()
        {
            if (_state == State.Processing | _state == State.Paused | _state == State.Ended)
            {
                try
                {
                    _state = State.Null;                            // NULL
                    _experimentThread.Abort();                      // Остановка потока
                    _experiment = null;
                }
                catch (ThreadAbortException) { }
            }
        }

        // // // // // 
        // public OutputDataObject getOutdataObject
        //
        // Свойство получения выходного объекта
        //
        // // // // //
        public OutputDataObject GetOutdataObject
        {
            get { return _outdataObject; }
            set { _outdataObject = value; }
        }

        // // // // // 
        // public State getState
        //
        // Свойство получения состояния эксперимента
        //
        // // // // //
        public State GetState
        {
            get { return _state; }
            set { _state = value; }
        }
    }
}
