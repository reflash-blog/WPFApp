using System.Collections.Generic;
using System.Windows.Controls.DataVisualization.Charting;

namespace CWLOTPH.Visuality
{
    public class ChartControl
    {
        private readonly LineSeries _firstChart;                    // График для первого Chart
        private readonly LineSeries _secondChart;                   // График для второго Chart LineSeries - по точкам, линейный

        public ChartControl(LineSeries firstChart, LineSeries secondChart)
        {
            _firstChart = firstChart;
            _secondChart = secondChart;
        }
        public void InitializeStandartChart()                  // Заполнение стандартными данными
        {
            var valueList = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(10, 60),
                new KeyValuePair<int, int>(20, 20),
                new KeyValuePair<int, int>(30, 50),
                new KeyValuePair<int, int>(40, 30),
                new KeyValuePair<int, int>(50, 40)
            };

            _firstChart.DataContext = valueList;
            _secondChart.DataContext = valueList;
        }

        public void DrawCharts(List<double> lengthCoordinates,List<double> temperature, List<double> viscosity)
        {
            var temperatureValueList = new List<KeyValuePair<double, double>>();
            var viscosityValueList = new List<KeyValuePair<double, double>>();

            for (var i = 0; i < lengthCoordinates.Count;i++ )
            {
                temperatureValueList.Add(new KeyValuePair<double, double>(lengthCoordinates[i], temperature[i]));
                viscosityValueList.Add(new KeyValuePair<double, double>(lengthCoordinates[i], viscosity[i]));
            }

            _firstChart.DataContext = temperatureValueList;
            _secondChart.DataContext = viscosityValueList;
        }
    }
}
