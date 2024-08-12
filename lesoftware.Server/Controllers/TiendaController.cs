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
    public class TiendaController : Controller
    {
        private readonly TiendaContext context;
        private readonly IMapper mapper;

        public TiendaController(TiendaContext context, IMapper mapper)
        {
            this.context=context;
            this.mapper=mapper;
        }

        //Lista de Tienda
        [HttpPost]
        [Route("lista")]
        public async Task<ActionResult<List<TiendaDTO>>> listaTiendas()
        {
            var tiendas = await context.Tienda
                .ToListAsync();


            return mapper.Map<List<TiendaDTO>>(tiendas);
        }
        //Tienda por id
        [HttpPost]
        [Route("tienda")]
        public async Task<ActionResult<TiendaDTO>> cliente([FromBody] int id)
        {

            var tienda = await context.Tienda
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tienda == null)
            {
                return BadRequest("Tienda no encontrada");
            }


            return mapper.Map<TiendaDTO>(tienda);
        }
        //Agregar tienda a la bd
        [HttpPost]
        [Route("agregar")]
        public async Task<ActionResult<TiendaDTO>> agregarTienda([FromBody] TiendaDTO tiendaDTO)
        {
            var tienda = mapper.Map<Tiendum>(tiendaDTO);

            context.Add(tienda);
            await context.SaveChangesAsync();

            return mapper.Map<TiendaDTO>(tienda);
        }
        //Actualizar tienda
        [HttpPost]
        [Route("editar")]
        public async Task<ActionResult> editarCliente([FromBody] TiendaDTO tiendaDTO)
        {
            var tienda = await context.Tienda.FirstOrDefaultAsync(t => t.Id == tiendaDTO.Id);

            if (tienda == null)
            {
                return BadRequest("Tienda no encontrada");
            }

            tienda.Sucursal = tiendaDTO.Sucursal is null ? tienda.Sucursal : tiendaDTO.Sucursal;
            tienda.Direccion = tiendaDTO.Direccion is null ? tienda.Direccion : tiendaDTO.Direccion;

            var tiendaUpdate = mapper.Map<Tiendum>(tienda);

            context.Update(tiendaUpdate);
            await context.SaveChangesAsync();

            return Ok(tienda);
        }

        //Eliminar tienda de la bd
        [HttpPost]
        [Route("eliminar")]
        public async Task<ActionResult> eliminarTienda([FromBody] TiendaDTO tiendaDTO)
        {
            var tienda = await context.Tienda.FirstOrDefaultAsync(c => c.Id == tiendaDTO.Id);

            if (tienda == null)
            {
                return BadRequest("Tienda no encontrado");
            }

            context.Tienda.Remove(tienda);
            await context.SaveChangesAsync();

            return Ok("Tienda eliminada exitosamente");
        }

    }
}
