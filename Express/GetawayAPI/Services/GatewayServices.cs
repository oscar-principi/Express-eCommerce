// GatewayAPI/Services/GatewayService.cs

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.DTOs;
using Shared.Interfaces;

namespace GatewayAPI.Services
{
    public class GatewayServices : IGatewayServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public GatewayServices(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"] ?? throw new ArgumentNullException("ApiSettings:BaseUrl", "Base URL must be defined in configuration.");
            _baseUrl = _baseUrl.EndsWith("/") ? _baseUrl : $"{_baseUrl}/";
        }

        public async Task<IEnumerable<ProductoDTO>> GetProductosAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<ProductoDTO>>($"{_baseUrl}productos");
            return response ?? new List<ProductoDTO>();
        }

        public async Task<ProductoDTO> GetProductoByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ProductoDTO>($"{_baseUrl}productos/{id}");
            return response ?? new ProductoDTO();
        }

        public async Task AddProductoAsync(ProductoDTO producto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}productos", producto);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateProductoAsync(ProductoDTO producto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}productos/{producto.Id}", producto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}productos/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<ImagenesProductosDTO>> GetImagenesByProductoIdAsync(int productoId)
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<ImagenesProductosDTO>>($"{_baseUrl}productos/{productoId}/imagenes");
            return response ?? new List<ImagenesProductosDTO>();
        }

        public async Task AddImagenAsync(ImagenesProductosDTO imagen)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}productos/{imagen.Id}/imagenes", imagen);
            response.EnsureSuccessStatusCode();
        }
    }
}
