using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces;
using Shared.DTOs;

namespace GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private readonly IGatewayServices _gatewayService;

        public GatewayController(IGatewayServices gatewayService)
        {
            _gatewayService = gatewayService;
        }

        [HttpGet("productos")]
        public async Task<IActionResult> GetProductos()
        {
            var productos = await _gatewayService.GetProductosAsync();
            return Ok(productos);
        }

        [HttpGet("productos/{nombre}")]
        public async Task<IActionResult> GetProductosByNombre(string nombre)
        {
            var productos = await _gatewayService.GetProductosByNombreAsync(nombre);
            if (productos == null || !productos.Any())
                return NotFound($"No se encontraron productos con el nombre {nombre}");
            return Ok(productos);
        }

        [HttpPost("productos")]
        public async Task<IActionResult> AddProducto([FromBody] ProductoDTO producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _gatewayService.AddProductoAsync(producto);
            return CreatedAtAction(nameof(GetProductosByNombre), new { nombre = producto.Nombre }, producto);
        }

        [HttpPut("productos/{nombre}")]
        public async Task<IActionResult> UpdateProducto(string nombre, [FromBody] ProductoDTO producto)
        {
            if (nombre != producto.Nombre)
                return BadRequest("El nombre del producto no coincide.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _gatewayService.UpdateProductoAsync(nombre, producto);
            return NoContent();
        }

        [HttpDelete("productos/{nombre}")]
        public async Task<IActionResult> DeleteProducto(string nombre)
        {
            await _gatewayService.DeleteProductoAsync(nombre);
            return NoContent();
        }

        // Obtener las imágenes de un producto
        [HttpGet("productos/{productoId}/imagenes")]
        public async Task<IActionResult> GetImagenesByProductoId(int productoId)
        {
            var imagenes = await _gatewayService.GetImagenesByProductoIdAsync(productoId);
            return Ok(imagenes);
        }

        // Agregar una imagen a un producto
        [HttpPost("productos/{productoId}/imagenes")]
        public async Task<IActionResult> AddImagen(int productoId, [FromBody] ImagenesProductosDTO imagen)
        {
            if (productoId != imagen.Id)
                return BadRequest("El ID del producto no coincide.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _gatewayService.AddImagenAsync(imagen);
            return Ok(imagen);
        }
    }
}
