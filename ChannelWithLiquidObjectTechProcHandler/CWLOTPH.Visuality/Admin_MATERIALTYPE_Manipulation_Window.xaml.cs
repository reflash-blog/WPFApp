using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CWLOTPH.DB;

namespace CWLOTPH.Visuality
{
    /// <summary>
    /// Логика взаимодействия для Admin_MATERIALTYPE_Manipulation_Window.xaml
    /// </summary>
    public partial class AdminMaterialtypeManipulationWindow : Window
    {
        public AdminMaterialtypeManipulationWindow()
        {
            InitializeComponent();
            RefreshMaterialNameComboBox();            // Обновляем записи в комбо боксе 
            setMaterialTooltips();
        }


        private void setMaterialTooltips()
        {
            this.densityTextBox.ToolTip = "Кг\u00F7м^3";
            this.averageSpecificHeatCapacityTextBox.ToolTip = "Дж\u00F7(кг∙\u00B0C)";
            this.meltingTemperatureTextBox.ToolTip = "\u00B0C";

            this.temperatureReductionTemperatureTextBox.ToolTip = "\u00B0C";
            this.theHeatTransferCoefficientSpecificHeatCapacityTextBox.ToolTip = "Вт\u00F7м^2\u00B7\u00B0C";
            this.consistencyIndexMaterialTextBox.ToolTip = "Па\u00B7с^n";
            this.temperatureCoefficientOfViscosityTemperatureTextBox.ToolTip = "1\u00F7\u00B0C";
        }

        private void RefreshMaterialNameComboBox()
        {
            var _mTypeList = DbInteraсtion.getAllMaterialTypes();      // Получаем все материалы из БД
            var _mTypeNamesList = _mTypeList.Select(mType => mType.Name).ToList();                        // Список строк
            materialTypeComboBox.ItemsSource = _mTypeNamesList;                       // Привязка данных с комбо боксом
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            string _mTypeName = materialTypeComboBox.SelectedItem.ToString();           // Имя выбранного типа
            if (_mTypeName == "") return;
            var admMtUpdWin = new Admin_MATERIALTYPE_Update_Window(_mTypeName);  // Создаем форму изменения материала
            admMtUpdWin.ShowInTaskbar = false;                                                               // Не показывать в панели задач
            admMtUpdWin.ShowDialog();                                                                        // Вызываем форму
            clearTextBoxes();                                                                                   // Очищаем текстовые поля
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

            Material _mType = DbInteraсtion.getMaterialType(materialTypeComboBox.SelectedItem.ToString());   // Получаем тип материала
            MessageWindow.Show(string.Format("Вы действительно хотите удалить тип материала {0}",              // Запрос на удаление у пользователя
            _mType.Name)+"Нажмите ОК, для подтверждения или крестик в правом верхнем углу экрана для отмены");
            if(MessageWindow.Result==MessageWindow.MessageResult.Ok)                                          // Если нажато ОК
            {
                DbInteraсtion.deleteMaterialType(_mType);                                                        // Удалить тип материала
                MessageWindow.Show(string.Format("Тип материала {0} успешно удален из базы данных",             // Уведомление об удалении
                _mType.Name));
                clearTextBoxes();                                                                                // Очистка текстовых полей
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void materialTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (materialTypeComboBox.SelectedIndex < 0) return;
            Material _mType = DbInteraсtion.getMaterialType(materialTypeComboBox.SelectedItem.ToString()); // Получаем тип материала
            averageSpecificHeatCapacityTextBox.Text = _mType.MaterialProperties.AverageHeatCapacity.ToString();                   // Записываем среднюю удельную теплоемкость в поле
            densityTextBox.Text = _mType.MaterialProperties.Density.ToString();                                                   // Записываем плотность в поле
            meltingTemperatureTextBox.Text = _mType.MaterialProperties.MeltingPoint.ToString();                                   // Записываем температуру плавления в поле

            stampTextBox.Text = _mType.Stamp;
            descriptionTextBox.Text = _mType.Description;

            indexOfMaterialTextBox.Text = _mType.MaterialEmpiricalCoefficients.indexOfMaterialFlow.ToString();
            temperatureReductionTemperatureTextBox.Text = _mType.MaterialEmpiricalCoefficients.temperatureReduction.ToString();
            theHeatTransferCoefficientSpecificHeatCapacityTextBox.Text = _mType.MaterialEmpiricalCoefficients.theHeatTransferCoefficient.ToString();
            consistencyIndexMaterialTextBox.Text = _mType.MaterialEmpiricalCoefficients.consistencyIndexMaterial.ToString();
            temperatureCoefficientOfViscosityTemperatureTextBox.Text = _mType.MaterialEmpiricalCoefficients.temperatureCoefficientOfViscosity.ToString();
        }
        private void clearTextBoxes()
        {
            materialTypeComboBox.SelectedIndex = -1;                                      // Очищаем текстовые поля
            averageSpecificHeatCapacityTextBox.Text = "";
            meltingTemperatureTextBox.Text = "";
            densityTextBox.Text = "";
            consistencyIndexMaterialTextBox.Text = "";
            temperatureReductionTemperatureTextBox.Text = "";
            temperatureCoefficientOfViscosityTemperatureTextBox.Text = "";
            descriptionTextBox.Text = "";
            indexOfMaterialTextBox.Text = "";
            stampTextBox.Text = "";
            theHeatTransferCoefficientSpecificHeatCapacityTextBox.Text = "";
        }
    }
}
