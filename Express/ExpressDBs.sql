
/****** Object:  Database [ExpressDBProductos]  ******/

CREATE DATABASE ExpressDBProductos
GO

USE ExpressDBProductos
GO

CREATE TABLE Productos (
	Id int IDENTITY(1,1) NOT NULL,
	Nombre nvarchar(100) NOT NULL,
	Descripcion nvarchar(500) NOT NULL,
	Precio decimal(18, 2) NOT NULL,
	Stock int NOT NULL,
    CONSTRAINT PK_ID_Producto PRIMARY KEY (ID)
 )

CREATE TABLE ImagenesProductos ( 
	Id int IDENTITY(1,1) NOT NULL,
	IdProducto int NOT NULL,
	UrlImagen nvarchar(255) NOT NULL,
	Descripcion nvarchar(255) NOT NULL,
    CONSTRAINT PK_ID_Imagen PRIMARY KEY (Id),
	CONSTRAINT FK_Imagenes_Productos FOREIGN KEY (IdProducto) REFERENCES Productos(Id)
)


-- Insertar productos
INSERT INTO Productos (Nombre, Descripcion, Precio, Stock) VALUES
('Producto 1', 'Descripción del Producto 1', 10.99, 50),
('Producto 2', 'Descripción del Producto 2', 20.99, 30),
('Producto 3', 'Descripción del Producto 3', 15.99, 20);


-- Insertar imágenes para el Producto 1
INSERT INTO ImagenesProductos (IdProducto, UrlImagen, Descripcion) VALUES
(1, '/Imagenes/ImagenesProductos/1.jpg', 'Imagen 1 del Producto 1'),
(1, '/Imagenes/ImagenesProductos/2.jpg', 'Imagen 2 del Producto 1'),
(1, '/Imagenes/ImagenesProductos/3.jpg', 'Imagen 3 del Producto 1');

-- Insertar imágenes para el Producto 2
INSERT INTO ImagenesProductos (IdProducto, UrlImagen, Descripcion) VALUES
(2, '/Imagenes/ImagenesProductos/1.jpg', 'Imagen 1 del Producto 2'),
(2, '/Imagenes/ImagenesProductos/2.jpg', 'Imagen 2 del Producto 2'),
(2, '/Imagenes/ImagenesProductos/3.jpg', 'Imagen 3 del Producto 2');

-- Insertar imágenes para el Producto 3
INSERT INTO ImagenesProductos (IdProducto, UrlImagen, Descripcion) VALUES
(3, '/Imagenes/ImagenesProductos/1.jpg', 'Imagen 1 del Producto 3'),
(3, '/Imagenes/ImagenesProductos/2.jpg', 'Imagen 2 del Producto 3'),
(3, '/Imagenes/ImagenesProductos/3.jpg', 'Imagen 3 del Producto 3');


