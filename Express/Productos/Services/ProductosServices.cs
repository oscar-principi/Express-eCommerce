using Microsoft.EntityFrameworkCore;
using ProductosAPI.Data;
using Shared.Interfaces;

namespace ProductosAPI.Services
{
    public class ProductosServices : IProductosServices
    {
        private readonly ProductosDbContext _context;

        public ProductosServices(ProductosDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {
            return await _context.Productos
                .Include(p => p.ImagenesProductos) 
                .ToListAsync();
        }

        public async Task<Producto?> GetProductoByIdAsync(int id)
        {
            return await _context.Productos
                .Include(p => p.ImagenesProductos) 
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProductoAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductoAsync(Producto producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductoAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ImagenesProducto>> GetImagenesByProductoIdAsync(int productoId)
        {
            return await _context.ImagenesProductos
                .Where(img => img.IdProducto == productoId)
                .ToListAsync();
        }

        public async Task AddImagenAsync(ImagenesProducto imagen)
        {
            await _context.ImagenesProductos.AddAsync(imagen);
            await _context.SaveChangesAsync();
        }
    }
}
