using ProductosAPI;

namespace Shared.Interfaces
{
    public interface IProductosServices
    {
        Task<IEnumerable<Producto>> GetProductosAsync();
        Task<List<Producto>> GetProductosByNombreAsync(string nombre);
        Task AddProductoAsync(Producto producto);
        Task UpdateProductoAsync(string nombre, Producto producto);
        Task DeleteProductoAsync(string nombre);
        Task<IEnumerable<ImagenesProducto>> GetImagenesByProductoIdAsync(int productoId);
        Task AddImagenAsync(ImagenesProducto imagen);
    }
}
