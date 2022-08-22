using Cadastros.Models;
using Cadastros.Service;
using Microsoft.AspNetCore.Mvc;

namespace Cadastros.Controllers
{
    public class ProdutoController : Controller
    {
        private IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public IActionResult Produto()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObterProduto()
        {
            try
            {
                var retorno = await _produtoService.ObterProduto();
                return View("_Produto", retorno); 
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult AdicionarProduto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarProduto(ProdutoDTO Produto)
        {
            try
             {
                var retorno = await _produtoService.AdicionarProduto(Produto);
                if (retorno)
                {
                    return RedirectToAction("Produto");
                }
                else
                {
                    return RedirectToAction("AdicionarProduto");
                }
            }
             catch(Exception)
            {
                return RedirectToAction("AdicionarProduto");
            }
        } 

        [HttpGet]
        public async Task<IActionResult> EditarProduto(int id)
        {
            var retorno = await _produtoService.ObterProdutoPorId(id);
            return View("EditarProduto", retorno);
        }

        [HttpPost]
        public async Task<IActionResult> EditarProduto(ProdutoDTO Produto)
        {
            try
            {
                 var retorno = await _produtoService.EditarProduto(Produto);
                if (retorno)
                {
                    return RedirectToAction("Produto");
                }
                else
                {
                    return RedirectToAction("EditarProduto");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("EditarProduto");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirProduto(int id)
        {
            try
            {
                var retorno = await _produtoService.ExcluirProduto(id);
                if (retorno)
                {
                    return RedirectToAction("Produto");
                }
                else
                {
                    return RedirectToAction("Produto");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Produto");
            }
        }
    }
}