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
    /// Логика взаимодействия для Admin_MATERIALTYPE_Add_Window.xaml
    /// </summary>
    public partial class Admin_MATERIALTYPE_Add_Window : Window
    {
        public Admin_MATERIALTYPE_Add_Window()
        {
            InitializeComponent();
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

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (materialTypeTextBox.Text != "" & meltingTemperatureTextBox.Text != "" &
                    densityTextBox.Text != "" & averageSpecificHeatCapacityTextBox.Text != "")     // Проверка на не пустые поля
                {
                    Material _mType = new Material();                                      // Создаем экземпляр объекта типа материала
                    _mType.MaterialEmpiricalCoefficients = new MaterialEmpiricalCoefficients();
                    _mType.MaterialProperties = new MaterialProperties();
                    _mType.Name = materialTypeTextBox.Text;                                        // Присваиваем имя
                    _mType.Stamp = stampTextBox.Text;                                              // Марка материала
                    _mType.Description = descriptionTextBox.Text;                                  // Описание материала
                    _mType.MaterialEmpiricalCoefficients.consistencyIndexMaterial = Convert.ToDouble(consistencyIndexMaterialTextBox.Text); // Присваиваем эмпирические коэффициэнты
                    _mType.MaterialEmpiricalCoefficients.indexOfMaterialFlow = Convert.ToDouble(indexOfMaterialTextBox.Text);
                    _mType.MaterialEmpiricalCoefficients.temperatureCoefficientOfViscosity = Convert.ToDouble(temperatureCoefficientOfViscosityTemperatureTextBox.Text);
                    _mType.MaterialEmpiricalCoefficients.temperatureReduction = Convert.ToDouble(temperatureReductionTemperatureTextBox.Text);
                    _mType.MaterialEmpiricalCoefficients.theHeatTransferCoefficient = Convert.ToDouble(theHeatTransferCoefficientSpecificHeatCapacityTextBox.Text); 
                    _mType.MaterialProperties.MeltingPoint = Convert.ToDouble(meltingTemperatureTextBox.Text);        // Присваиваем температуру плавления
                    _mType.MaterialProperties.Density = Convert.ToDouble(densityTextBox.Text);                        // Присваиваем плотность
                    _mType.MaterialProperties.AverageHeatCapacity = Convert.ToDouble(averageSpecificHeatCapacityTextBox.Text); // Присваиваем среднюю удельную теплоемкость
                    DbInteraсtion.addMaterialType(_mType);                                         // Добавляем материал в БД
                    MessageWindow.Show(string.Format("Тип материала {0} успешно добавлен в базу данных", // Вызываем сообщение для пользователя
                        _mType.Name));
                    clearTextBoxes();                                                              // Очищаем поля для ввода
                }
            }
            catch (FormatException) {
                MessageWindow.Show(" Неправильный формат ввода ");                                // Отлавливаем неправильный ввод данных
            }
        }

        private void clearTextBoxes()
        {
            materialTypeTextBox.Text = "";                                        // Очищаем 4 текстовых поля
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

        private void exitButton_Click(object sender, RoutedEventArgs e)           // Handler для кнопки выхода
        {
            Close();        
        }
    }
}
