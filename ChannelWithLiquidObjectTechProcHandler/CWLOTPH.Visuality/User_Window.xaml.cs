using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;
using CWLOTPH.DataObject;
using CWLOTPH.DB;

namespace CWLOTPH.Visuality
{
    /// <summary>
    /// Логика взаимодействия для User_Window.xaml
    /// </summary>
    public partial class UserWindow
    {
        public bool CheckFlagStarted = false;
        public bool CheckFlagPaused = false;
        public bool CheckFlagStopped = false;
        ChartControl _chartControl;
        DataTable _dataTable;
        readonly AdditiveData_Window _additiveWindow;

        public UserWindow()
        {
            InitializeComponent();
            ToolTipService.SetShowDuration(mTypeComboBox, 3600000); // Установка времени показа контекстной подсказки
            InitializeChart();
            InitializeDataTable();
            _additiveWindow = new AdditiveData_Window();
        }

        private void InitializeDataTable()
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("Координаты\n по длине\n канала, м");
            _dataTable.Columns.Add("Температура\n в градусах\n Цельсия");
            _dataTable.Columns.Add("Вязкость,\n Па*с");
            _dataTable.Rows.Add(0, 0, 0);
            outputDataGrid.ItemsSource = _dataTable.DefaultView;
        }

        public InputDataObject InputData()
        {
            InputDataObject inpDObj = InputHandler.CheckInput(this);
            if (inpDObj == null)
            {
                MessageWindow.Show("Ошибка ввода данных");
                return null;  
            }
            AddAdditionalDataFromDataBaseIntoInputDataObject(inpDObj);
            return inpDObj;
        }

        private static void AddAdditionalDataFromDataBaseIntoInputDataObject(InputDataObject inpDObj)
        {
            var materialPropertie = AdditiveData_Window.MatType;
            var empiricalVector = AdditiveData_Window.EmpVector;
            // Добавляем в объект
            inpDObj.MaterialProperties = materialPropertie;
            inpDObj.EmpiricalVector = empiricalVector;
            
        }

        public void OutputData(OutputDataObject oDObj)
        {
            productConsumptionTextBox.Text = Math.Round(oDObj.Qk, 2).ToString(CultureInfo.InvariantCulture);
            productTemperatureTextBox.Text = Math.Round(oDObj.Tend, 2).ToString(CultureInfo.InvariantCulture);
            productViscosityTextBox.Text = Math.Round(oDObj.Vend, 5).ToString(CultureInfo.InvariantCulture);
            codePerfomingTimeLabel.Content = "Время выполнения: "+ oDObj.tickCount + " мс";
            _chartControl.DrawCharts(oDObj.lengthCoordinates, oDObj.temperature, oDObj.viscosity);
            FillDataGridTable(oDObj);
        }

        private void FillDataGridTable(OutputDataObject oDObj)
        {
            _dataTable.Rows.Clear();
            for (var i = 0; i < oDObj.lengthCoordinates.Count; i++)
            {
                _dataTable.Rows.Add(Math.Round(oDObj.lengthCoordinates[i],2), 
                    Math.Round(oDObj.temperature[i],2),
                    Math.Round(oDObj.viscosity[i],5));
            }
        }

        private void InitializeChart()
        {
            _chartControl = new ChartControl(UpperChart, LowerChart);
            _chartControl.InitializeStandartChart();
        }


        private void turnButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            // ... A List.
            var mTypes = DbInteraсtion.getAllMaterialTypes();
            var data = mTypes.Select(mtype => mtype.Name).ToList();

            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            if (comboBox == null) return;
            comboBox.ItemsSource = data;

            // ... Make the first item selected.
            comboBox.SelectedIndex = 0;
        }

        private void startExperimentToolstripItem_Click(object sender, RoutedEventArgs e)
        {
            startExperimentToolstripItem.IsEnabled = false;
            stopExperimentToolstripItem.IsEnabled = true;
            pauseExperimentToolstripItem.IsEnabled = true;
            CheckFlagStarted = true;
        }

        private void pauseExperimentToolstripItem_Click(object sender, RoutedEventArgs e)
        {
            pauseExperimentToolstripItem.IsEnabled = false;
            startExperimentToolstripItem.IsEnabled = true;
            CheckFlagPaused = true;
        }

        public void stopExperimentToolstripItem_Click(object sender, RoutedEventArgs e)
        {
            stopExperimentToolstripItem.IsEnabled = false;
            pauseExperimentToolstripItem.IsEnabled = false;
            startExperimentToolstripItem.IsEnabled = true;
            CheckFlagStopped = true;
        }

        private void aboutProgramToolstripItem_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow.Show("Программа CWLOTPH разработана для демонстрации работы расчетного механизма " +
                "технологического процесса. Разработана в 2014 году.");
        }

        private void helpToolstripItem_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\cwlotph.chm");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageWindow.Show("Вы действительно хотите выйти? Сохраните изменения перед выходом. ОК - закрыть приложение");
            if (MessageWindow.Result == MessageWindow.MessageResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void createSettingSetToolstripItem_Click(object sender, RoutedEventArgs e)
        {
            int result = FileSystemInteraction.NewFile();
            if(result==1)// Открыт новый файл
            {
                ClearForm();
            }
        }

        private void ClearForm()
        {
            InitializeChart();
            InitializeDataTable();
            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            lengthTextBox.Clear();
            widthTextBox.Clear();
            heightTextBox.Clear();
            velocityTextBox.Clear();
            temperatureTextBox.Clear();
            productConsumptionTextBox.Clear();
            productTemperatureTextBox.Clear();
            productViscosityTextBox.Clear();
            stepTextBox.Clear();
        }

        private void saveSettingSetToolstripItem_Click(object sender, RoutedEventArgs e)
        {
            InputDataObject inpDObj = InputData();
            if(inpDObj!=null)
            {
                FileSystemInteraction.SaveFile(inpDObj);
            }
        }

        private void saveAsSettingSetToolstripItem_Click(object sender, RoutedEventArgs e)
        {
            InputDataObject inpDObj = InputData();
            if (inpDObj != null)
            {
                FileSystemInteraction.SaveAsFile(inpDObj);
            }
        }


        private void openSettingSetToolstripItem_Click(object sender, RoutedEventArgs e)
        {
            var inpDObj = FileSystemInteraction.OpenFile();
            ClearForm();
            lengthTextBox.Text = inpDObj.GeometryVector.L.ToString(CultureInfo.InvariantCulture);
            widthTextBox.Text = inpDObj.GeometryVector.W.ToString(CultureInfo.InvariantCulture);
            heightTextBox.Text = inpDObj.GeometryVector.H.ToString(CultureInfo.InvariantCulture);
            velocityTextBox.Text = inpDObj.ModeVector.Vu.ToString(CultureInfo.InvariantCulture);
            temperatureTextBox.Text = inpDObj.ModeVector.Tu.ToString(CultureInfo.InvariantCulture);
            stepTextBox.Text = inpDObj.DiscretizationStep.ToString(CultureInfo.InvariantCulture);
        }

        private void mTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var material = DbInteraсtion.getMaterialType(mTypeComboBox.SelectedItem.ToString());
            AdditiveData_Window.setVectors(
                new EmpiricalVector
                {
                alphaU = material.MaterialEmpiricalCoefficients.theHeatTransferCoefficient,
                b = material.MaterialEmpiricalCoefficients.temperatureCoefficientOfViscosity,
                Mu = material.MaterialEmpiricalCoefficients.consistencyIndexMaterial,
                n = material.MaterialEmpiricalCoefficients.indexOfMaterialFlow,
                Tgamma = material.MaterialEmpiricalCoefficients.temperatureReduction
            },
                new DataObject.MaterialProperties
                {
                    Ro = material.MaterialProperties.Density,
                    C = material.MaterialProperties.AverageHeatCapacity,
                    T0 = material.MaterialProperties.MeltingPoint
                });

            SetMaterialComboBoxToolTip(material);
        }

        private void SetMaterialComboBoxToolTip(Material material)
        {
            mTypeComboBox.ToolTip = "Тип материала:\n" +
                "- Имя:                                                               " + material.Name + "\n" +
                "- Марка:                                                           " + material.Stamp + "\n" +
                "- Описание:                                                     " + material.Description + "\n" +
                "Свойства материала:\n" +
                "- Плотность, Кг\u00F7м^3:                                                " + material.MaterialProperties.Density + "\n" +
                "- Cредняя удельная теплоемкость, Дж\u00F7(кг∙\u00B0C):      " + material.MaterialProperties.AverageHeatCapacity + "\n" +
                "- Температура плавления, \u00B0C:                                  " + material.MaterialProperties.MeltingPoint + "\n" +
                "Эмпирические коэффициенты материала:\n" +
                "- Индекс течения:                                                      " + material.MaterialEmpiricalCoefficients.indexOfMaterialFlow + "\n" +
                "- Температура приведения, \u00B0C:                                " + material.MaterialEmpiricalCoefficients.temperatureReduction + "\n" +
                "- Коэффициент теплоотдачи, Вт\u00F7м^2\u00B7\u00B0C:                " + material.MaterialEmpiricalCoefficients.theHeatTransferCoefficient + "\n" +
                "- Коэффициент консистенции материала, Па\u00B7с^n: " + material.MaterialEmpiricalCoefficients.consistencyIndexMaterial + "\n" +
                "- Температурный коэффициент вязкости, 1\u00F7\u00B0C:     " + material.MaterialEmpiricalCoefficients.temperatureCoefficientOfViscosity + "\n";
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            _additiveWindow.Show();
            var material = DbInteraсtion.getMaterialType(mTypeComboBox.SelectedItem.ToString());
            material.MaterialEmpiricalCoefficients = new MaterialEmpiricalCoefficients
            {
                consistencyIndexMaterial = AdditiveData_Window.EmpVector.Mu,
                indexOfMaterialFlow = AdditiveData_Window.EmpVector.n,
                temperatureCoefficientOfViscosity = AdditiveData_Window.EmpVector.b,
                theHeatTransferCoefficient = AdditiveData_Window.EmpVector.alphaU,
                temperatureReduction = AdditiveData_Window.EmpVector.Tgamma
            };
            material.MaterialProperties = new DB.MaterialProperties
            {
                Density = AdditiveData_Window.MatType.Ro,
                AverageHeatCapacity = AdditiveData_Window.MatType.C,
                MeltingPoint = AdditiveData_Window.MatType.T0
            };
            SetMaterialComboBoxToolTip(material);
        }
    }
}
