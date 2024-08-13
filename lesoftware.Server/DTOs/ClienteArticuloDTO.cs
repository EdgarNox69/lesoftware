using lesoftware.Server.Models;

namespace lesoftware.Server.DTOs
{
    public class ClienteArticuloDTO
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public int ArticuloId { get; set; }

        public DateOnly? Fecha { get; set; }

        public virtual ArticuloDTO Articulo { get; set; } = null!;
    }
}
