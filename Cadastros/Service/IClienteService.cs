using Cadastros.Models;

namespace Cadastros.Service
{
    public interface IClienteService
    {
        Task<bool> AdicionarCliente(ClienteDTO cliente);
        Task<bool> EditarCliente(ClienteDTO cliente);
        Task<bool> ExcluirCliente(int id);
        Task<List<ClienteDTO>> ObterCliente();
        Task<ClienteDTO> ObterClientePorId(int id);
    }
}