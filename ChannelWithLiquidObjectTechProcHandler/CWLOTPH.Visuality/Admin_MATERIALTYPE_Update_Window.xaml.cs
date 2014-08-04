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
using CWLOTPH.DB;

namespace CWLOTPH.Visuality
{
    /// <summary>
    /// Логика взаимодействия для Admin_MATERIALTYPE_Update_Window.xaml
    /// </summary>
    public partial class Admin_MATERIALTYPE_Update_Window : Window
    {
        public Admin_MATERIALTYPE_Update_Window(string materialTypeName)
        {
            InitializeComponent();
            InitializeMaterialType(materialTypeName);                       // Инициализируем поля типа материала
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
        private void InitializeMaterialType(string materialTypeName)        // По имени типа материала заполняем текстовые поля
        {
            materialTypeTextBox.Text = materialTypeName;
            Material _mType = DbInteraсtion.getMaterialType(materialTypeName);
            densityTextBox.Text = _mType.MaterialProperties.Density.ToString();
            averageSpecificHeatCapacityTextBox.Text = _mType.MaterialProperties.AverageHeatCapacity.ToString();
            meltingTemperatureTextBox.Text = _mType.MaterialProperties.MeltingPoint.ToString();

            stampTextBox.Text = _mType.Stamp.ToString();
            descriptionTextBox.Text = _mType.Description.ToString();

            indexOfMaterialTextBox.Text = _mType.MaterialEmpiricalCoefficients.indexOfMaterialFlow.ToString();
            temperatureReductionTemperatureTextBox.Text = _mType.MaterialEmpiricalCoefficients.temperatureReduction.ToString();
            theHeatTransferCoefficientSpecificHeatCapacityTextBox.Text = _mType.MaterialEmpiricalCoefficients.theHeatTransferCoefficient.ToString();
            consistencyIndexMaterialTextBox.Text = _mType.MaterialEmpiricalCoefficients.consistencyIndexMaterial.ToString();
            temperatureCoefficientOfViscosityTemperatureTextBox.Text = _mType.MaterialEmpiricalCoefficients.temperatureCoefficientOfViscosity.ToString();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(averageSpecificHeatCapacityTextBox.Text!=""&densityTextBox.Text!=""  // Если поля не пустые
                    & meltingTemperatureTextBox.Text != "")
                {
                    Material _mType = new Material();                           // Создаем экземпляр класса типа материала
                    _mType.MaterialProperties = new MaterialProperties();
                    _mType.MaterialEmpiricalCoefficients = new MaterialEmpiricalCoefficients();

                    _mType.ID = DbInteraсtion.getMaterialType(materialTypeTextBox.Text).ID;        // Присваиваем ID
                    _mType.Name = materialTypeTextBox.Text;                             // Устанавливаем имя
                    _mType.MaterialProperties.Density = Convert.ToDouble(densityTextBox.Text);             // Устанавливаем плотность
                    _mType.MaterialProperties.AverageHeatCapacity = Convert.ToDouble(averageSpecificHeatCapacityTextBox.Text); // Устанавливаем ср. удел. теплоемкость
                    _mType.MaterialProperties.MeltingPoint = Convert.ToDouble(meltingTemperatureTextBox.Text);                 // Устанавливаем темп. плавления
                    _mType.Description = descriptionTextBox.Text;
                    _mType.Stamp = stampTextBox.Text;
                    _mType.MaterialEmpiricalCoefficients.consistencyIndexMaterial = Convert.ToDouble(consistencyIndexMaterialTextBox.Text);
                    _mType.MaterialEmpiricalCoefficients.indexOfMaterialFlow = Convert.ToDouble(indexOfMaterialTextBox.Text);
                    _mType.MaterialEmpiricalCoefficients.temperatureCoefficientOfViscosity = Convert.ToDouble(temperatureCoefficientOfViscosityTemperatureTextBox.Text);
                    _mType.MaterialEmpiricalCoefficients.temperatureReduction = Convert.ToDouble(temperatureReductionTemperatureTextBox.Text);
                    _mType.MaterialEmpiricalCoefficients.theHeatTransferCoefficient = Convert.ToDouble(theHeatTransferCoefficientSpecificHeatCapacityTextBox.Text);

                    MessageWindow.Show(string.Format("Вы действительно хотите изменить тип материала {0}", // Проверка на необходимость изменений
                    _mType.Name) + 
                    "Нажмите ОК, для подтверждения или крестик в правом верхнем углу экрана для отмены");
                    if (MessageWindow.Result == MessageWindow.MessageResult.Ok)                           // Если ОК
                    {
                        DbInteraсtion.updateMaterialType(_mType);                                           // Обновляем
                        MessageWindow.Show(string.Format("Тип материала {0} успешно изменен",              // Уведомляем пользователя
                        _mType.Name));                                                        
                    }
                    Close();
                }
            }catch(FormatException)
            {
                MessageWindow.Show("Неправильный формат ввода");                                            // Отлавливаем неправильный формат ввода
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
