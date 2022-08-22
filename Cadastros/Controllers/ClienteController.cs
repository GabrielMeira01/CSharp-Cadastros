using Cadastros.Models;
using Cadastros.Service;
using Microsoft.AspNetCore.Mvc;

namespace Cadastros.Controllers
{
    public class ClienteController : Controller
    {
        private IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public IActionResult Cliente()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObterCliente()
        {
            try
            {
                var retorno = await _clienteService.ObterCliente();
                return View("_Cliente", retorno); 
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult AdicionarCliente()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarCliente(ClienteDTO cliente)
        {
            try
             {
                var retorno = await _clienteService.AdicionarCliente(cliente);
                if (retorno)
                {
                    return RedirectToAction("Cliente");
                }
                else
                {
                    return RedirectToAction("AdicionarCliente");
                }
            }
             catch(Exception)
            {
                return RedirectToAction("AdicionarCliente");
            }
        } 

        [HttpGet]
        public async Task<IActionResult> EditarCliente(int id)
        {
            var retorno = await _clienteService.ObterClientePorId(id);
            return View("EditarCliente", retorno);
        }

        [HttpPost]
        public async Task<IActionResult> EditarCliente(ClienteDTO cliente)
        {
            try
            {
                var retorno = await _clienteService.EditarCliente(cliente);
                if (retorno)
                {
                    return RedirectToAction("Cliente");
                }
                else
                {
                    return RedirectToAction("EditarCliente");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("EditarCliente");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirCliente(int id)
        {
            try
            {
                var retorno = await _clienteService.ExcluirCliente(id);
                if (retorno)
                {
                    return RedirectToAction("Cliente");
                }
                else
                {
                    return RedirectToAction("Cliente");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Cliente");
            }
        }
    }
}