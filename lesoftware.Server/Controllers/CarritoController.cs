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
    public class CarritoController: Controller
    {
        private readonly TiendaContext context;
        private readonly IMapper mapper;

        public CarritoController(TiendaContext context, IMapper mapper)
        {
            this.context=context;
            this.mapper=mapper;
        }

        //Agregar producto al carrito
        [HttpPost]
        [Route("carrito")]
        public async Task<ActionResult> carritoArticulo([FromBody] ClienteArticuloDTO carrito)
        {
            //carrito.Fecha = DateOnly;
            var clienteArt = mapper.Map<ClienteArticulo>(carrito);

            context.Add(clienteArt);
            await context.SaveChangesAsync();

            return Ok(clienteArt);

        }

        //Lista de Tienda
        [HttpPost]
        [Route("lista")]
        public async Task<ActionResult<List<ClienteArticuloDTO>>> listaArticulos([FromBody] int clienteId)
        {
            var articulos = await context.ClienteArticulos
                .Where(c => c.ClienteId == clienteId)
                .Include(a => a.Articulo)
                .ToListAsync();

            var carrito = mapper.Map<List<ClienteArticuloDTO>>(articulos);

            return Ok(carrito);
        }
    }
}
