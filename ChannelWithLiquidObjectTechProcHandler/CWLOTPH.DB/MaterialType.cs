namespace CWLOTPH.DB
{
    public class MaterialType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Stamp { get; set; }

        public double ConsistencyIndexMaterial { get; set; }
        public double IndexOfMaterialFlow { get; set; }
        public double TemperatureReduction { get; set; }
        public double TheHeatTransferCoefficient { get; set; }
        public double TemperatureCoefficientOfViscosity { get; set; }

        public double Density { get; set; }
        public double MeltingPoint { get; set; }
        public double AverageHeatCapacity { get; set; }
    }
}
