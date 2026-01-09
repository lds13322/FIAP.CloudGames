namespace FIAP.CloudGames.Pagamentos.Worker.Dtos
{
    public class PedidoDeCompraIniciadoDto
    {
        public Guid UsuarioId { get; set; }
        public Guid JogoId { get; set; }
        public decimal Valor { get; set; }
        public DateTime Timestamp { get; set; }
    }
}