using ProductosAPI;

namespace Shared.Interfaces
{
    public interface IProductosServices
    {
        Task<IEnumerable<Producto>> GetProductosAsync();
        Task<Producto?> GetProductoByIdAsync(int id);
        Task AddProductoAsync(Producto producto);
        Task UpdateProductoAsync(Producto producto);
        Task DeleteProductoAsync(int id);
        Task<IEnumerable<ImagenesProducto>> GetImagenesByProductoIdAsync(int productoId);
        Task AddImagenAsync(ImagenesProducto imagen);
    }
}
