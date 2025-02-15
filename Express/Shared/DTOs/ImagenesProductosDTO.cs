﻿using ProductosAPI;

namespace Shared.DTOs
{
    public class ImagenesProductosDTO
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public string UrlImagen { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public virtual Producto IdProductoNavigation { get; set; } = null!;
    }
}
