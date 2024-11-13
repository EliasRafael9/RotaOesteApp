using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjetoRotaOeste.Dtos;
using ProjetoRotaOeste.Models;
using ProjetoRotaOeste.Repositories;
using System.Linq;
using ProjetoRotaOeste.DTOs;


namespace ProjetoRotaOeste.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ClienteController(IClienteRepository clienteRepository, IUsuarioRepository usuarioRepository, IPerguntaRepository perguntaRepository)
        {
            _clienteRepository = clienteRepository;
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCliente(int id)
        {
            var cliente = await _clienteRepository.GetClienteByIdAsync(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpGet ("all")]
        public async Task<IActionResult> GetAllClientes()
        {
            var clientes = await _usuarioRepository.GetAllClientesAsync();
            return Ok(clientes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCliente([FromBody] Cliente cliente)
        {
            await _clienteRepository.AddClienteAsync(cliente);
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.IdCliente }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.IdCliente) return BadRequest();
            await _clienteRepository.UpdateClienteAsync(cliente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            await _clienteRepository.DeleteClienteAsync(id);
            return NoContent();
        }
    }
}
