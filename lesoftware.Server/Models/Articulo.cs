using System;
using System.Collections.Generic;

namespace lesoftware.Server.Models;

public partial class Articulo
{
    public int Id { get; set; }

    public int Codigo { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public byte[]? Imagen { get; set; }

    public int? Stock { get; set; }

    public virtual ICollection<ArticuloTiendum> ArticuloTienda { get; set; } = new List<ArticuloTiendum>();

    public virtual ICollection<ClienteArticulo> ClienteArticulos { get; set; } = new List<ClienteArticulo>();
}
