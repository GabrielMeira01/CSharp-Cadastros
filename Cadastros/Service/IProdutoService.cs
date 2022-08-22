﻿using Cadastros.Models;

namespace Cadastros.Service
{
    public interface IProdutoService
    {
        Task<bool> AdicionarProduto(ProdutoDTO Produto);
        Task<bool> EditarProduto(ProdutoDTO Produto);
        Task<bool> ExcluirProduto(int id);
        Task<List<ProdutoDTO>> ObterProduto();
        Task<ProdutoDTO> ObterProdutoPorId(int id);
    }
}