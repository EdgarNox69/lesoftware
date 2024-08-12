using AutoMapper;
using lesoftware.Server.DTOs;
using lesoftware.Server.Models;
using System.Runtime.InteropServices;

namespace lesoftware.Server.Utilities
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ClienteDTO, Cliente>().ReverseMap();
            CreateMap<TiendaDTO, Tiendum>().ReverseMap();
            CreateMap<ArticuloDTO, Articulo>().ReverseMap();
        }
    }
}
