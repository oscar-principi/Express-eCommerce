using Microsoft.EntityFrameworkCore;
using ProductosAPI.Data;
using Shared.DTOs;
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
                .Include(p => p.Categoria)         
                .ToListAsync();
        }


        public async Task<List<Producto>> GetProductosByNombreAsync(string nombre)
        {
            return await _context.Productos
                .Include(p => p.ImagenesProductos)
                .Where(p => p.Nombre.Contains(nombre))
                .ToListAsync();
        }

        public async Task AddProductoAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductoAsync(string nombre, Producto producto)
        {
            var productoExistente = await _context.Productos
                .FirstOrDefaultAsync(p => p.Nombre == nombre);

            if (productoExistente == null)
                throw new KeyNotFoundException($"No se encontró un producto con el nombre {nombre}");

            productoExistente.Nombre = producto.Nombre;
            productoExistente.Descripcion = producto.Descripcion;
            productoExistente.Precio = producto.Precio;
            productoExistente.Stock = producto.Stock;

            _context.Productos.Update(productoExistente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductoAsync(string nombre)
        {
            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.Nombre == nombre);

            if (producto == null)
                throw new KeyNotFoundException($"No se encontró un producto con el nombre {nombre}");

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
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
