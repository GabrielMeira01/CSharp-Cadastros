using System.Text.RegularExpressions;
using Cadastros.Models;
using Cadastros.Repository;

namespace Cadastros.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<bool> AdicionarCliente(ClienteDTO cliente)
        {
            var model = new ClienteDTO()
            {
                Nome = cliente.Nome,
                Bairro = cliente.Bairro,
                CEP = cliente.CEP.Replace("-", ""),
                Cidade = cliente.Cidade,
                Codigo = cliente.Codigo,
                CodigoIBGE = cliente.CodigoIBGE,
                Complemento = cliente.Complemento ?? "",
                Logradouro = cliente.Logradouro,
                UF = cliente.UF
            };
            return await _clienteRepository.AdicionarCliente(model);
        }


        public async Task<bool> EditarCliente(ClienteDTO cliente)
        {
            var model = new ClienteDTO()
            {
                Nome = cliente.Nome,
                Bairro = cliente.Bairro,
                CEP = cliente.CEP.Replace("-", ""),
                Cidade = cliente.Cidade,
                Codigo = cliente.Codigo,
                CodigoIBGE = cliente.CodigoIBGE,
                Complemento = cliente.Complemento ?? "",
                Logradouro = cliente.Logradouro,
                UF = cliente.UF
            };
            return await _clienteRepository.EditarCliente(model);
        }

        public async Task<bool> ExcluirCliente(int id)
        {
            return await _clienteRepository.ExcluirCliente(id);
        }

        public async Task<List<ClienteDTO>> ObterCliente()
        {
            return await _clienteRepository.ObterCliente();
        }

        public async Task<ClienteDTO> ObterClientePorId(int id)
        {
            return await _clienteRepository.ObterClientePorId(id);
        }
    }
}
