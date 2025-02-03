using ProductosAPI;
using Shared.DTOs;

namespace Shared.Interfaces
{
    public interface IGatewayServices
    {
        // Métodos para interactuar con los productos
        Task<IEnumerable<ProductoDTO>> GetProductosAsync();
        Task<List<ProductoDTO>> GetProductosByNombreAsync(string nombre);
        Task AddProductoAsync(ProductoDTO producto);
        Task UpdateProductoAsync(string nombre, ProductoDTO producto);
        Task DeleteProductoAsync(string nombre);

        // Métodos para interactuar con imágenes
        Task<IEnumerable<ImagenesProductosDTO>> GetImagenesByProductoIdAsync(int productoId);
        Task AddImagenAsync(ImagenesProductosDTO imagen);
    }
}

