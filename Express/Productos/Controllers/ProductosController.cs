using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces;
using System.Text.Json.Serialization;
using System.Text.Json;
using Shared.DTOs;
using AutoMapper;

namespace ProductosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosServices _productosServices;
        private readonly IMapper _mapper;

        public ProductosController(IProductosServices productosServices, IMapper mapper)
        {
            _productosServices = productosServices;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            var productos = await _productosServices.GetProductosAsync();

            var productosDTO = _mapper.Map<List<ProductoDTO>>(productos);

            return new JsonResult(productosDTO, new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                MaxDepth = 16 
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductoById(int id)
        {
            var producto = await _productosServices.GetProductoByIdAsync(id);
            if (producto == null)
                return NotFound($"No se encontró un producto con el ID {id}");
            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> AddProducto([FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productosServices.AddProductoAsync(producto);
            return CreatedAtAction(nameof(GetProductoById), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] Producto producto)
        {
            if (id != producto.Id)
                return BadRequest("El ID del producto no coincide.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productosServices.UpdateProductoAsync(producto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            await _productosServices.DeleteProductoAsync(id);
            return NoContent();
        }

        [HttpGet("{productoId}/imagenes")]
        public async Task<IActionResult> GetImagenesByProductoId(int productoId)
        {
            var imagenes = await _productosServices.GetImagenesByProductoIdAsync(productoId);
            return Ok(imagenes);
        }

        [HttpPost("{productoId}/imagenes")]
        public async Task<IActionResult> AddImagen(int productoId, [FromBody] ImagenesProducto imagen)
        {
            if (productoId != imagen.IdProducto)
                return BadRequest("El ID del producto no coincide.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productosServices.AddImagenAsync(imagen);
            return Ok(imagen);
        }
    }
}
