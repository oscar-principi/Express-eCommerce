using Shared.DTOs;

namespace Shared.Interfaces
{
    public interface IGatewayServices
    {
        // Métodos para interactuar con los productos
        Task<IEnumerable<ProductoDTO>> GetProductosAsync();
        Task<ProductoDTO> GetProductoByIdAsync(int id);
        Task AddProductoAsync(ProductoDTO producto);
        Task UpdateProductoAsync(ProductoDTO producto);
        Task DeleteProductoAsync(int id);

        // Métodos para interactuar con imágenes
        Task<IEnumerable<ImagenesProductosDTO>> GetImagenesByProductoIdAsync(int productoId);
        Task AddImagenAsync(ImagenesProductosDTO imagen);
    }
}

