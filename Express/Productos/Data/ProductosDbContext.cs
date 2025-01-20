using Microsoft.EntityFrameworkCore;

namespace ProductosAPI.Data;

public partial class ProductosDbContext : DbContext
{
    public ProductosDbContext()
    {
    }

    public ProductosDbContext(DbContextOptions<ProductosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ImagenesProducto> ImagenesProductos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:ExpressDBProductos");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ImagenesProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ID_Imagen");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.UrlImagen).HasMaxLength(255);

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ImagenesProductos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Imagenes_Productos");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ID_Producto");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
