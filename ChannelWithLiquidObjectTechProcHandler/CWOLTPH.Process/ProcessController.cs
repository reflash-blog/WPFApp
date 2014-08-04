using System;
using System.Threading.Tasks;
using System.Threading;
using CWLOTPH.Visuality;
using CWLOTPH.DataObject;

namespace CWLOTPH.Process
{
    public class ProcessController
    {
        IExperiment experiment = null;
        UserWindow uWindow = null;
        Thread processControllerCycleThread;

        public void initProcessController()
        {
            uWindow = new UserWindow();
            uWindow.Show();
            processControllerCycleThread = new Thread(processControllerCycle);
            processControllerCycleThread.SetApartmentState(ApartmentState.STA);
            processControllerCycleThread.Start();
        }

        private void processControllerCycle()
        {
            bool isLoaded = true;
            uWindow.Dispatcher.Invoke(new Action(delegate()
            {
                isLoaded = uWindow.IsLoaded;
            }));

            while (isLoaded)
            {
                // Проверка на старт
                if (uWindow.CheckFlagStarted)
                {
                    input();
                    uWindow.CheckFlagStarted = false;
                }
                // Проверка на паузу
                if (uWindow.CheckFlagPaused)
                {
                    if (experiment != null)
                    {
                        experiment.PauseExperiment();
                    }
                    else
                    {
                        MessageWindow.Show("Ошибка эксперимент №5235838. Завершение эксперимента");
                    }
                    uWindow.CheckFlagPaused = false;
                }
                // Проверка на остановку
                if (uWindow.CheckFlagStopped)
                {
                    if (experiment != null)
                    {
                        experiment.StopExperiment();
                    }
                    else
                    {
                        MessageWindow.Show("Ошибка эксперимент №4245676. Завершение эксперимента");
                    }
                    uWindow.CheckFlagStopped = false;
                }

                if (experiment != null)
                {
                    // Проверка состояния эксперимента
                    if (experiment.GetState == Experiment.State.Ended)
                    {
                        output();
                        experiment.GetState = Experiment.State.Null;
                    }
                }

                try
                {
                    uWindow.Dispatcher.Invoke(new Action(delegate()
                {
                    isLoaded = uWindow.IsLoaded;
                }));
                }catch(TaskCanceledException)  // Если окно уже закрыто, мы не можем получить доступ к объекту
                {
                    isLoaded = false;
                }
            }
        }

        private void input()
        {
            InputDataObject _inpDObj = null;
            uWindow.Dispatcher.Invoke(new Action(delegate()
            {
                _inpDObj = uWindow.InputData();      // Получаем объект
            }));
            if (_inpDObj != null)
            {
                experiment = Experiment.InitExperiment(_inpDObj);    // Создаем новый эксперимент
                experiment.StartExperiment();                        // Стартуем эксперимент
            }
            else
            {
                uWindow.CheckFlagStarted = false;                   // Отключаем запуск
                StartStopButtonsInvokeDispatchHandler();
                
            }
        }

        private void output()
        {
            uWindow.Dispatcher.Invoke(new Action(delegate()
            {
                uWindow.OutputData(experiment.GetOutdataObject);
            }));
            experiment.StopExperiment();
            StartStopButtonsInvokeDispatchHandler();
        }

        private void StartStopButtonsInvokeDispatchHandler()
        {
            uWindow.Dispatcher.Invoke(new Action(delegate()
            {
                uWindow.stopExperimentToolstripItem_Click(null, null); // Вызываем переключение активных элементов
            }));
        }
    }
}
