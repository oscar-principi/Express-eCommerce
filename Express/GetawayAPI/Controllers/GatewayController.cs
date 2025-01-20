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

        [HttpGet("productos/{id}")]
        public async Task<IActionResult> GetProductoById(int id)
        {
            var producto = await _gatewayService.GetProductoByIdAsync(id);
            if (producto == null)
                return NotFound($"No se encontró un producto con el ID {id}");
            return Ok(producto);
        }

        [HttpPost("productos")]
        public async Task<IActionResult> AddProducto([FromBody] ProductoDTO producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _gatewayService.AddProductoAsync(producto);
            return CreatedAtAction(nameof(GetProductoById), new { id = producto.Id }, producto);
        }

        [HttpPut("productos/{id}")]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] ProductoDTO producto)
        {
            if (id != producto.Id)
                return BadRequest("El ID del producto no coincide.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _gatewayService.UpdateProductoAsync(producto);
            return NoContent();
        }

        [HttpDelete("productos/{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            await _gatewayService.DeleteProductoAsync(id);
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
