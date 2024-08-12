using System;
using System.Collections.Generic;

namespace lesoftware.Server.Models;

public partial class ArticuloTiendum
{
    public int Id { get; set; }

    public int ArticuloId { get; set; }

    public int TiendaId { get; set; }

    public DateOnly? Fecha { get; set; }

    public virtual Articulo Articulo { get; set; } = null!;

    public virtual Tiendum Tienda { get; set; } = null!;
}
