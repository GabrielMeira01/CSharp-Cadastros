namespace Cadastros.Models
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public DateTime DataEmissao { get; set; }
        public int CodigoCliente { get; set; }
        public string? NomeCliente { get; set; }
        public float TotalPeso { get; set; }
        public float TotalPedido { get; set; }

    }
}
