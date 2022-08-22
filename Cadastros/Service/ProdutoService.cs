using Cadastros.Models;
using Cadastros.Repository;

namespace Cadastros.Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<bool> AdicionarProduto(ProdutoDTO Produto)
        {

            return await _produtoRepository.AdicionarProduto(Produto);
        }

        public async Task<bool> EditarProduto(ProdutoDTO Produto)
        {
            return await _produtoRepository.EditarProduto(Produto);
        }

        public async Task<bool> ExcluirProduto(int id)
        {
            return await _produtoRepository.ExcluirProduto(id);
        }

        public async Task<List<ProdutoDTO>> ObterProduto()
        {
            return await _produtoRepository.ObterProduto();
        }

        public async Task<ProdutoDTO> ObterProdutoPorId(int id)
        {
            return await _produtoRepository.ObterProdutoPorId(id);
        }
    }
}
