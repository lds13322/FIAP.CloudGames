namespace FarmService.Models
{
    public class Farm
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string ProdutorEmail { get; set; } = string.Empty;
        public List<Talhao> Talhoes { get; set; } = new();
    }

    public class Talhao
    {
        public string Id { get; set; } = string.Empty; // Ex: "Talhao-Sul-01"
        public string Cultura { get; set; } = string.Empty; // Ex: "Soja", "Milho"
        public double AreaHectares { get; set; }
    }
}