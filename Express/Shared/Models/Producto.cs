﻿using System;
using System.Collections.Generic;

namespace ProductosAPI;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int Categoria { get; set; }

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public virtual Categoria CategoriaNavigation { get; set; } = null!;

    public virtual ICollection<ImagenesProducto> ImagenesProductos { get; set; } = new List<ImagenesProducto>();
}
