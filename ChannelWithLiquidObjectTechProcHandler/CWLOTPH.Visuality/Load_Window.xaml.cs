using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;

namespace CWLOTPH.Visuality
{
    /// <summary>
    /// Логика взаимодействия для Load_Window.xaml
    /// </summary>
    public partial class Load_Window : Window
    {

        private static Load_Window LOADGui = null;                              // Форма Загрузочного окна
        Thread PBARHandlerThread = null;

        private Load_Window()
        {
            InitializeComponent();
        }


        // // // //
        // public static void Initialize()
        // 
        // Функция инициализирует работу загрузочной формы
        // // // //
        public static Load_Window Initialize()
        {
            LOADGui = new Load_Window();                                        // Создаем объект формы
            LOADGui.PBARHandlerThread = new Thread(LOADGui.progressBarHandler);  // Запускаем поток контроля прогресс бара
            return LOADGui;                                           //
        }

        // // // //
        // private static void ShowLoadGUI()
        // 
        // Функция отображает загрузочную форму
        // // // //
        public void ShowLoadGUI()
        {
            try
            {
                LOADGui.PBARHandlerThread.Start();
                // Если вызывается из другого потока
                LOADGui.ShowDialog();
            }
            catch (ThreadAbortException) { }                                    // Отлавливаем исключение при отключении потока
        }

        // // // //
        // public void progressBarHandler()
        // 
        // Функция обрабатывает прогресс бар
        // // // //
        public void progressBarHandler()
        {
            progressBar.Dispatcher.BeginInvoke(new ThreadStart(delegate()
            {
                Thread.Sleep(1000);  
                progressBar.Maximum = 100;                                      // Устанавливаем максимальное значение прогресс бара
            }));

            for (int loadCounter = 100; loadCounter >= 0; loadCounter--)    // Цикл на 100 шагов ~ %
            {
                System.Threading.Thread.Sleep(20);                          // Паузим на 20 мс
                progressBar.Dispatcher.BeginInvoke(new ThreadStart(delegate()
            {
                progressBar.Value = 100 - loadCounter;                          // Присваиваем следующее значение/Изменяем значение прогресс бара
            }));
                DispatcherHelper.DoEvents();
            }

            LOADGui.Dispatcher.BeginInvoke(new ThreadStart(delegate()
            {
                LOADGui.Close();
            }));
        }
    }
}
