using Cadastros.Models;

namespace Cadastros.Repository
{
    public interface IClienteRepository
    {
        Task<bool> AdicionarCliente(ClienteDTO cliente);
        Task<bool> EditarCliente(ClienteDTO cliente);
        Task<bool> ExcluirCliente(int id);
        Task<List<ClienteDTO>> ObterCliente();
        Task<ClienteDTO> ObterClientePorId(int id);
    }
}