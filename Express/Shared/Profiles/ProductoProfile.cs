using AutoMapper;
using Shared.DTOs;
using ProductosAPI;

namespace Shared.Profiles
{
    // Asegúrate de heredar de Profile para usar CreateMap
    public class ProductoProfile : Profile
    {
        public ProductoProfile()
        {
            // Definimos el mapeo entre Producto y ProductoDTO
            CreateMap<Producto, ProductoDTO>()
                .ForMember(dest => dest.ImagenesProductos, opt => opt.MapFrom(src => src.ImagenesProductos));

            // Mapeo simple de ImagenesProducto a ImagenesProductosDTO
            CreateMap<ImagenesProducto, ImagenesProductosDTO>()
                .ForMember(dest => dest.IdProductoNavigation, opt => opt.Ignore()); 
        }
    }
}

