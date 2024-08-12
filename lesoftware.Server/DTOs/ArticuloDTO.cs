namespace lesoftware.Server.DTOs
{
    public class ArticuloDTO
    {
        public int Id { get; set; }

        public int Codigo { get; set; }

        public string? Descripcion { get; set; }

        public decimal? Precio { get; set; }

        public byte[]? Imagen { get; set; }

        public int? Stock { get; set; }
    }
}
