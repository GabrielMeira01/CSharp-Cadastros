namespace Cadastros.Models
{
    public class ClienteDTO
    {
        public int Codigo { get; set; }
        public string? Nome { get; set; }
        public string? CEP { get; set; }
        public string? Logradouro { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? UF { get; set; }
        public int CodigoIBGE { get; set; }
    }
}
