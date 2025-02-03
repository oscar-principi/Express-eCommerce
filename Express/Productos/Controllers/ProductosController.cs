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

        [HttpGet("{nombre}")]
        public async Task<IActionResult> GetProductosByNombre(string nombre)
        {
            var productos = await _productosServices.GetProductosByNombreAsync(nombre);
            var productosDTO = _mapper.Map<List<ProductoDTO>>(productos);

            return new JsonResult(productosDTO, new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                MaxDepth = 16
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddProducto([FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productosServices.AddProductoAsync(producto);
            return CreatedAtAction(nameof(GetProductosByNombre), new { nombre = producto.Nombre }, producto);
        }

        [HttpPut("{nombre}")]
        public async Task<IActionResult> UpdateProducto(string nombre, [FromBody] Producto producto)
        {
            if (nombre != producto.Nombre)
                return BadRequest("El nombre del producto no coincide.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productosServices.UpdateProductoAsync(nombre, producto);
            return NoContent();
        }

        [HttpDelete("{nombre}")]
        public async Task<IActionResult> DeleteProducto(string nombre)
        {
            await _productosServices.DeleteProductoAsync(nombre);
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
