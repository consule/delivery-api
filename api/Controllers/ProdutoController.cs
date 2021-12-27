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
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _pedidoRepository;
        public ProdutoController(IProdutoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }
     
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultado = await _pedidoRepository.GetAll();
            return Ok(resultado);
        }
    }
}
