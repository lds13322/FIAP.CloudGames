namespace SharedIntegration
{
    public class SensorDataMessage
    {
        // Adicionamos o = string.Empty; no final
        public string TalhaoId { get; set; } = string.Empty; 
        public double Temperatura { get; set; }
        public double Umidade { get; set; }
        public double Precipitacao { get; set; }
        public DateTime DataHora { get; set; }
    }
}