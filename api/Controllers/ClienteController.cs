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
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

       
      
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultado = await _clienteRepository.Get();
            return Ok(resultado);
        }
    }
}
