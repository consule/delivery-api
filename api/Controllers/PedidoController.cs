using api.Repositories;
using api.Repositories.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;
        public PedidoController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

       
      
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultado = await _pedidoRepository.GetPedidosAsync();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoPorIDAsync(int Id)
        {
            var resultado = await _pedidoRepository.GetPedidoPorIDAsync(Id);
            return Ok(resultado);
        }

        [HttpGet]
        [Route("nomeCliente/{nomeCliente}")]
        public async Task<IActionResult> GetPedidosPorNomeClienteAsync(string nomeCliente)
        {
            var result = await _pedidoRepository.GetPedidosPorNomeClienteAsync(nomeCliente.ToUpper());
            return Ok(result);
        }

        [HttpGet]
        [Route("nomeClienteEntregue/{nomeCliente}")]
        public async Task<IActionResult> GetPedidosPorNomeClienteEntregue(string nomeCliente)
        {
            var result = await _pedidoRepository.GetPedidosPorNomeClienteEntregue(nomeCliente.ToUpper());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pedido ped)
        {
            if (this.ClienteExiste(ped.IdCliente) && this.ProdutoExiste(ped.IdProduto))
            {
                var resultado = await _pedidoRepository.PostPedidoAsync(ped);
                if (resultado != null)
                {
                    return Ok(new { mensagem = "Pedido Registrado" });
                }
                
            }
            return Ok(new { mensagem = "Pedido NÃO Registrado" });
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int Id, Pedido ped)
        {
            var resultado = await _pedidoRepository.MarcaPedidoComoEntregue(Id, ped);
            if (resultado != null)
            {
                return Ok(new { mensagem = "Entrega Registrada" });
            }
            return Ok(new { mensagem = "Entrega NÃO Registrada" });
        }
        [HttpGet]
        [Route("entregues")]
        public async Task<IActionResult> GetPedidosEntregues()
        {
            var resultado = await _pedidoRepository.GetPedidoEntregues();
            return Ok(resultado);
        }
        protected bool ClienteExiste(int IdCliente)
        {
            return true;
        }

        protected bool ProdutoExiste(int IdProduto)
        {
            return true;
        }
    }
}
