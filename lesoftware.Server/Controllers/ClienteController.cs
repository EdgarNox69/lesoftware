using AutoMapper;
using lesoftware.Server.DTOs;
using lesoftware.Server.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lesoftware.Server.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly TiendaContext context;
        private readonly IMapper mapper;

        public ClienteController(TiendaContext context, IMapper mapper)
        {
            this.context=context;
            this.mapper=mapper;
        }

        //Lista de clientes
        [HttpPost]
        [Route("lista")]
        public async Task<ActionResult<List<ClienteDTO>>> listaClientes()
        {
            var clientes = await context.Clientes
                .ToListAsync();


            return mapper.Map<List<ClienteDTO>>(clientes);
        }
        //Cliente por id
        [HttpPost]
        [Route("cliente")]
        public async Task<ActionResult<ClienteDTO>> cliente([FromBody] int id)
        {
            
            var cliente = await context.Clientes
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
            {
                return BadRequest("Cliente no encontrado");
            }


            return mapper.Map<ClienteDTO>(cliente);
        }
        //Agregar cliente a la bd
        [HttpPost]
        [Route("agregar")]
        public async Task<ActionResult<ClienteDTO>> agregarCliente([FromBody] ClienteDTO clienteDTO)
        {
            var cliente = mapper.Map<Cliente>(clienteDTO);
            
            context.Add(cliente);
            await context.SaveChangesAsync();

            return mapper.Map<ClienteDTO>(cliente);
        }
        //Actualizar cliente
        [HttpPost]
        [Route("editar")]
        public async Task<ActionResult> editarCliente([FromBody] ClienteDTO clienteDTO)
        {
            var cliente = await context.Clientes.FirstOrDefaultAsync(c => c.Id == clienteDTO.Id);

            if (cliente == null)
            {
                return BadRequest("Cliente no encontrado");
            }

            cliente.Nombre = clienteDTO.Nombre is null ? cliente.Nombre : clienteDTO.Nombre;
            cliente.Apellidos = clienteDTO.Apellidos is null ? cliente.Apellidos : clienteDTO.Apellidos;
            cliente.Direccion = clienteDTO.Direccion is null ? cliente.Direccion : clienteDTO.Direccion;
            var clienteUpdate = mapper.Map<Cliente>(cliente);

            context.Update(clienteUpdate);
            await context.SaveChangesAsync();

            return Ok(cliente);
        }

        //Eliminar cliente de la bd
        [HttpPost]
        [Route("eliminar")]
        public async Task<ActionResult> eliminarCliente([FromBody] ClienteDTO clienteDTO)
        {
            var cliente = await context.Clientes.FirstOrDefaultAsync(c => c.Id == clienteDTO.Id);

            if (cliente == null)
            {
                return BadRequest("Cliente no encontrado");
            }

            context.Clientes.Remove(cliente);
            await context.SaveChangesAsync();

            return Ok("Cliente eliminado exitosamente");
        }
    }
}
