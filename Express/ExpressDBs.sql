
/****** Object:  Database [ExpressDBProductos]  ******/

CREATE DATABASE ExpressDBProductos
GO

USE ExpressDBProductos
GO

CREATE TABLE Categorias (
    Id INT IDENTITY(1,1) NOT NULL,
    NombreCategoria NVARCHAR(255) NOT NULL,
	CONSTRAINT PK_ID_Categoria PRIMARY KEY (Id)
);

CREATE TABLE Productos (
	Id INT IDENTITY(1,1) NOT NULL,
	Nombre NVARCHAR(100) NOT NULL,
	Descripcion NVARCHAR(500) NOT NULL,
	Categoria INT NOT NULL,
	Precio DECIMAL(18, 2) NOT NULL,
	Stock INT NOT NULL,
    CONSTRAINT PK_ID_Producto PRIMARY KEY (ID),
	CONSTRAINT FK_Producto_Categoria FOREIGN KEY (Categoria) REFERENCES Categorias(Id) ON DELETE CASCADE
 )

CREATE TABLE ImagenesProductos ( 
	Id INT IDENTITY(1,1) NOT NULL,
	IdProducto INT NOT NULL,
	UrlImagen NVARCHAR(255) NOT NULL,
	Descripcion NVARCHAR(255) NOT NULL,
    CONSTRAINT PK_ID_Imagen PRIMARY KEY (Id),
	CONSTRAINT FK_Imagenes_Productos FOREIGN KEY (IdProducto) REFERENCES Productos(Id) ON DELETE CASCADE
)


--Insertar categorias
INSERT INTO Categorias (NombreCategoria) VALUES
('Electrónica'),
('Electrodomésticos'),
('Computación'),
('Celulares y Telefonía'),
('Hogar, Muebles y Jardín'),
('Moda y Accesorios'),
('Calzado'),
('Indumentaria'),
('Belleza, Cuidado Personal y Salud'),
('Deportes y Fitness'),
('Juguetes y Juegos'),
('Bebés'),
('Automóviles y Motos'),
('Repuestos y Accesorios para Vehículos'),
('Construcción y Ferretería'),
('Herramientas y Maquinarias'),
('Alimentos y Bebidas'),
('Libros, Revistas y Comics'),
('Música, Películas y Series'),
('Instrumentos Musicales'),
('Industria y Oficinas'),
('Arte y Manualidades'),
('Jardinería'),
('Viajes y Turismo')

-- Insertar productos
INSERT INTO Productos (Nombre, Descripcion, Categoria, Precio, Stock) VALUES
('Laptop Gamer X1', 'Laptop de alto rendimiento para gaming', 1, 100000, 50),
('Smartphone Pro Max', 'Teléfono inteligente con cámara avanzada', 4, 83000, 30),
('Auriculares Inalámbricos', 'Auriculares Bluetooth con cancelación de ruido', 1, 3000, 200),
('Monitor 4K Ultra HD', 'Monitor de alta resolución para trabajo y gaming', 1, 15000, 60),
('Teclado Mecánico RGB', 'Teclado con retroiluminación RGB personalizable', 1, 22000, 80),
('Mouse Gamer', 'Mouse óptico con alta precisión y botones programables', 1, 75000, 40),
('Silla Ergonómica', 'Silla de oficina con soporte lumbar y ajustable', 5, 12000, 90),
('Router WiFi 6', 'Router de última generación con gran alcance', 1, 34000, 70),
('Smartwatch Fitness', 'Reloj inteligente con monitoreo de salud', 1, 5000, 120),
('Tarjeta Gráfica RTX', 'GPU de alto rendimiento para gaming y diseño', 1, 19000, 55),
('Disco SSD NVMe', 'Unidad de almacenamiento ultrarrápida', 1, 89000, 20),
('Cámara Web Full HD', 'Cámara con resolución 1080p para videollamadas', 1, 67000, 45),
('Micrófono Profesional', 'Micrófono condensador para streaming y podcast', 1, 26000, 95),
('Impresora Multifunción', 'Impresora con escáner y conectividad WiFi', 2, 31000, 65),
('Tablet Gráfica', 'Tableta con lápiz digital para diseño', 3, 87000, 35),
('Parlantes Bluetooth', 'Altavoces inalámbricos con sonido envolvente', 1, 43000, 85),
('Consola de Videojuegos', 'Última generación con gráficos avanzados', 1, 98000, 25),
('Cargador Rápido USB-C', 'Cargador de alta potencia para dispositivos', 1, 12000, 110),
('Fuente de Poder 750W', 'Fuente de alimentación certificada para PC', 1, 1500, 250),
('Hub USB 3.0', 'Expansión de puertos USB para múltiples dispositivos', 1, 29000, 75);



-- Generar imágenes para productos
DECLARE @i INT = 1;
WHILE @i <= 20
BEGIN
    INSERT INTO ImagenesProductos (IdProducto, UrlImagen, Descripcion) VALUES
    (@i, '/Imagenes/ImagenesProductos/1.jpg', CONCAT('Imagen 1 del Producto ', @i)),
    (@i, '/Imagenes/ImagenesProductos/2.jpg', CONCAT('Imagen 2 del Producto ', @i)),
    (@i, '/Imagenes/ImagenesProductos/3.jpg', CONCAT('Imagen 3 del Producto ', @i));
    SET @i = @i + 1;
END;

