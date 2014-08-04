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
using CWLOTPH.DataObject;

namespace CWLOTPH.Visuality
{
    /// <summary>
    /// Логика взаимодействия для Admin_MATERIALTYPE_Add_Window.xaml
    /// </summary>
    public partial class AdditiveData_Window : Window
    {
        static EmpiricalVector empVector;
        static MaterialProperties matType;


        public AdditiveData_Window()
        {
            InitializeComponent();
        }

        public static void setVectors(EmpiricalVector empVector, MaterialProperties matType)
        {
            AdditiveData_Window.empVector = empVector;
            AdditiveData_Window.matType = matType;
        }
        private void exitButton_Click(object sender, RoutedEventArgs e)           // Handler для кнопки выхода
        {
            this.Visibility = Visibility.Hidden;       
        }

        public static EmpiricalVector EmpVector
        {
            get { return AdditiveData_Window.empVector; }
            set { AdditiveData_Window.empVector = value; }
        }

        public static MaterialProperties MatType
        {
            get { return AdditiveData_Window.matType; }
            set { AdditiveData_Window.matType = value; }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try {
                AdditiveData_Window.MatType.Ro = Convert.ToDouble(densityTextBox.Text);
                AdditiveData_Window.MatType.C = Convert.ToDouble(averageSpecificHeatCapacityTextBox.Text);
                AdditiveData_Window.MatType.T0 = Convert.ToDouble(meltingTemperatureTextBox.Text);

                AdditiveData_Window.EmpVector.alphaU = Convert.ToDouble(theHeatTransferCoefficientSpecificHeatCapacityTextBox.Text);
                AdditiveData_Window.EmpVector.b = Convert.ToDouble(temperatureCoefficientOfViscosityTemperatureTextBox.Text);
                AdditiveData_Window.EmpVector.Mu = Convert.ToDouble(consistencyIndexMaterialTextBox.Text);
                AdditiveData_Window.EmpVector.n = Convert.ToDouble(indexOfMaterialTextBox.Text);
                AdditiveData_Window.EmpVector.Tgamma = Convert.ToDouble(temperatureReductionTemperatureTextBox.Text);
                this.Visibility = Visibility.Hidden;
            }
            catch (FormatException) { MessageWindow.Show("Неверный формат ввода"); }
        }

        public void Show()
        {
            this.consistencyIndexMaterialTextBox.Text = AdditiveData_Window.EmpVector.Mu.ToString();
            this.indexOfMaterialTextBox.Text = AdditiveData_Window.EmpVector.n.ToString();
            this.temperatureCoefficientOfViscosityTemperatureTextBox.Text = AdditiveData_Window.EmpVector.b.ToString();
            this.temperatureReductionTemperatureTextBox.Text = AdditiveData_Window.EmpVector.Tgamma.ToString();
            this.theHeatTransferCoefficientSpecificHeatCapacityTextBox.Text = AdditiveData_Window.EmpVector.alphaU.ToString();

            this.densityTextBox.Text = AdditiveData_Window.MatType.Ro.ToString();
            this.averageSpecificHeatCapacityTextBox.Text = AdditiveData_Window.MatType.C.ToString();
            this.meltingTemperatureTextBox.Text = AdditiveData_Window.MatType.T0.ToString();

            this.ShowDialog();
        }
    }
}
