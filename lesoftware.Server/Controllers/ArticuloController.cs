using AutoMapper;
using lesoftware.Server.DTOs;
using lesoftware.Server.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace lesoftware.Server.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : Controller
    {
        private readonly TiendaContext context;
        private readonly IMapper mapper;
       
        public ArticuloController(TiendaContext context, IMapper mapper)
        {
            this.context=context;
            this.mapper=mapper;
        }

        //Lista de Tienda
        [HttpPost]
        [Route("lista")]
        public async Task<ActionResult<List<ArticuloDTO>>> listaArticulos()
        {
            var articulos = await context.Articulos
                .ToListAsync();


            return mapper.Map<List<ArticuloDTO>>(articulos);
        }
        //Tienda por id
        [HttpPost]
        [Route("articulo")]
        public async Task<ActionResult<ArticuloDTO>> cliente([FromBody] int id)
        {

            var articulo = await context.Articulos
                .FirstOrDefaultAsync(a => a.Id == id);

            if (articulo == null)
            {
                return BadRequest("Articulo no encontrado");
            }


            return mapper.Map<ArticuloDTO>(articulo);
        }
        //Agregar tienda a la bd
        [HttpPost]
        [Route("agregar")]
        public async Task<ActionResult<ArticuloDTO>> agregarTienda([FromBody] ArticuloDTO articuloDTO)
        {
            var articulo = mapper.Map<Articulo>(articuloDTO);

            context.Add(articulo);
            await context.SaveChangesAsync();

            return mapper.Map<ArticuloDTO>(articulo);
        }
        //Actualizar tienda
        [HttpPost]
        [Route("editar")]
        public async Task<ActionResult> editarCliente([FromBody] ArticuloDTO articuloDTO)
        {
            var articulo = await context.Articulos.FirstOrDefaultAsync(a => a.Id == articuloDTO.Id);

            if (articulo == null)
            {
                return BadRequest("Articulo no encontrado");
            }

            articulo.Descripcion = articuloDTO.Descripcion is null ? articulo.Descripcion : articuloDTO.Descripcion;
            articulo.Precio = articuloDTO.Precio is null ? articulo.Precio : articuloDTO.Precio;
            articulo.Imagen = articuloDTO.Imagen is null ? articulo.Imagen : articuloDTO.Imagen;
            articulo.Stock = articuloDTO.Stock is null ? articulo.Stock : articuloDTO.Stock;

            var articuloUpdate = mapper.Map<Articulo>(articulo);

            context.Update(articuloUpdate);
            await context.SaveChangesAsync();

            return Ok(articulo);
        }

        //Eliminar tienda de la bd
        [HttpPost]
        [Route("eliminar")]
        public async Task<ActionResult> eliminarTienda([FromBody] ArticuloDTO articuloDTO)
        {
            var articulo = await context.Articulos.FirstOrDefaultAsync(c => c.Id == articuloDTO.Id);

            if (articulo == null)
            {
                return BadRequest("Articulo no encontrado");
            }

            context.Articulos.Remove(articulo);
            await context.SaveChangesAsync();

            return Ok("Articulo eliminado exitosamente");
        }

       

    }
}
