using System;
using System.Collections.Generic;

namespace lesoftware.Server.Models;

public partial class ClienteArticulo
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public int ArticuloId { get; set; }

    public DateOnly? Fecha { get; set; }

    public virtual Articulo Articulo { get; set; } = null!;

    public virtual Cliente Cliente { get; set; } = null!;
}
