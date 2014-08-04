using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using CWLOTPH.DB;

namespace CWLOTPH.Visuality
{
    /// <summary>
    /// Логика взаимодействия для Admin_Window.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            FillDataGrids();                   // Заполняем таблицы
        }


        private void FillDataGrids()
        {
            var _userList = DbInteraсtion.getAllUserRecords();                   // Заполняем список пользователей
            List<Material> _materialTypeList = DbInteraсtion.getAllMaterialTypes(); // Заполняем список типов материалов
            userRecordsDataGrid.ItemsSource = _userList;                                // Привязка пользователей
            List<MaterialType> _mtypeList = new List<MaterialType>();
            foreach (Material _material in _materialTypeList)
            {
                var _mType = new MaterialType();
                _mType.Id = _material.ID;
                _mType.Name = _material.Name;
                _mType.Stamp = _material.Stamp;
                _mType.MeltingPoint = _material.MaterialProperties.MeltingPoint;
                _mType.Density = _material.MaterialProperties.Density;
                _mType.AverageHeatCapacity = _material.MaterialProperties.AverageHeatCapacity;
                _mType.ConsistencyIndexMaterial = _material.MaterialEmpiricalCoefficients.consistencyIndexMaterial;
                _mType.IndexOfMaterialFlow = _material.MaterialEmpiricalCoefficients.indexOfMaterialFlow;
                _mType.TemperatureCoefficientOfViscosity = _material.MaterialEmpiricalCoefficients.temperatureCoefficientOfViscosity;
                _mType.TemperatureReduction = _material.MaterialEmpiricalCoefficients.temperatureReduction;
                _mType.TheHeatTransferCoefficient = _material.MaterialEmpiricalCoefficients.theHeatTransferCoefficient;
                _mtypeList.Add(_mType);
            }
            materialTypesDataGrid.ItemsSource = _mtypeList;
        }

        private void turnButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;  // Handler для кнопки сворачивания
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();                   // Handler для перемещения окна
        }

        private void createMaterialTypeToolStripItem_Click(object sender, RoutedEventArgs e)
        {
            Admin_MATERIALTYPE_Add_Window adm_MT_add_win = new Admin_MATERIALTYPE_Add_Window();
            adm_MT_add_win.ShowInTaskbar = false; // Не показывать в панели задач
            adm_MT_add_win.ShowDialog();
        }

        private void updateMaterialTypeToolStripItem_Click(object sender, RoutedEventArgs e)
        {
            AdminMaterialtypeManipulationWindow adm_MT_upd_win = new AdminMaterialtypeManipulationWindow();
            adm_MT_upd_win.ShowInTaskbar = false; // Не показывать в панели задач
            adm_MT_upd_win.ShowDialog();
        }

        private void createUserRecordToolStripItem_Click(object sender, RoutedEventArgs e)
        {
            Admin_USER_Add adm_U_add_win = new Admin_USER_Add();
            adm_U_add_win.ShowInTaskbar = false; // Не показывать в панели задач
            adm_U_add_win.ShowDialog();
        }

        private void updateUserRecordToolStripItem_Click(object sender, RoutedEventArgs e)
        {
            Admin_USER_Manipulation_Window adm_U_add_win = new Admin_USER_Manipulation_Window();
            adm_U_add_win.ShowInTaskbar = false; // Не показывать в панели задач
            adm_U_add_win.ShowDialog();
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            FillDataGrids();
        }
    }
}
