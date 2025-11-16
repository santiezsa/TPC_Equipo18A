-- Crea la base de datos
use master
go
create database BBDD_TPC_P3
go
use BBDD_TPC_P3
go

-- Categorias
CREATE TABLE Categorias(
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, -- campos autonumericos
Descripcion VARCHAR(100) NOT NULL,
Activo BIT NOT NULL DEFAULT 1
)
go

-- Marcas
CREATE TABLE Marcas(
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, -- campos autonumericos
Descripcion VARCHAR(100) NOT NULL,
Activo BIT NOT NULL DEFAULT 1
)
GO

-- Clientes
CREATE TABLE Clientes(
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, -- campos autonumericos
Nombre VARCHAR(100) NOT NULL,
Apellido VARCHAR(100) NOT NULL,
Email VARCHAR(150) UNIQUE, -- para evitar duplicados de mails
Telefono VARCHAR(50),
Direccion VARCHAR(255),
Activo BIT NOT NULL DEFAULT 1
)
go

-- Proveedores
CREATE TABLE Proveedores(
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, -- campos autonumericos
Nombre VARCHAR(100),
RazonSocial VARCHAR(200) NOT NULL,
CUIT VARCHAR(13) NOT NULL UNIQUE, -- no puede haber dos clientes con mismo CUIT
Email VARCHAR(150),
Telefono VARCHAR(50),
Activo BIT NOT NULL DEFAULT 1
)
go

-- Perfiles
CREATE TABLE Perfiles(
Id INT PRIMARY KEY NOT NULL,
Descripcion VARCHAR(50) NOT NULL,
Activo BIT NOT NULL DEFAULT 1
)
GO

-- Creacion de tablas con dependencias

-- Usuarios
CREATE TABLE Usuarios(
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, -- autonumerico
Username VARCHAR(100) NOT NULL UNIQUE, -- debe ser unico
Password VARCHAR(100) NOT NULL,
IdPerfil INT NOT NULL FOREIGN KEY REFERENCES Perfiles(Id),
Activo BIT NOT NULL DEFAULT 1,
)
go

-- Productos
CREATE TABLE Productos(
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, --autonumerico
Codigo VARCHAR(50) UNIQUE,
Nombre VARCHAR(150) NOT NULL,
Descripcion VARCHAR(500),
IdMarca INT FOREIGN KEY REFERENCES Marcas(Id),
IdCategoria INT FOREIGN KEY REFERENCES Categorias(Id),
StockActual INT NOT NULL DEFAULT 0, -- cuando se crea el producto se asume que tiene 0 unidades
StockMinimo INT NOT NULL DEFAULT 5, -- el stock minimo del prod es 5
PorcentajeGanancia DECIMAL(5,2) NOT NULL DEFAULT 30.00, -- porcentaje por defecto sera del 30%
Activo BIT NOT NULL DEFAULT 1
)
go

-- Tabla para conectar los productos con proveedores
CREATE TABLE Productos_x_Proveedores(
IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(Id),
IdProveedor INT NOT NULL FOREIGN KEY REFERENCES Proveedores(Id),
PRIMARY KEY (IdProducto, IdProveedor) -- PK Compuesta
)
GO



----------------------- PENDIENTE LAS TABLAS CORE -----------------------
CREATE TABLE Compras(
Id INT PRIMARY KEY IDENTITY(1,1),
IdProveedor INT NOT NULL FOREIGN KEY REFERENCES Proveedores(Id),
IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id), -- para registrar que usuario efectuo la compra
Fecha DATETIME NOT NULL DEFAULT GETDATE(),
Total DECIMAL (18,2) NOT NULL,
Activo BIT NOT NULL DEFAULT 1 -- para poder anular compra
)
go

CREATE TABLE DetalleCompra(
Id INT PRIMARY KEY IDENTITY(1,1),
IdCompra INT NOT NULL FOREIGN KEY REFERENCES Compras(Id),
IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(Id),
Cantidad INT NOT NULL,
PrecioUnitario DECIMAL(18,2) NOT NULL
)
go

CREATE TABLE Ventas(
Id INT PRIMARY KEY IDENTITY(1,1),
NumeroFactura VARCHAR(50) NOT NULL UNIQUE,
IdCliente INT NOT NULL FOREIGN KEY REFERENCES Clientes(Id),
IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id), -- para registrar que usuario efectuo la venta
Fecha DATETIME NOT NULL DEFAULT GETDATE(),
Total DECIMAL(18,2) NOT NULL,
Activo BIT NOT NULL DEFAULT 1
)
GO

CREATE TABLE DetalleVenta(
Id INT PRIMARY KEY IDENTITY(1,1),
IdVenta INT NOT NULL FOREIGN KEY REFERENCES Ventas(Id),
IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(Id),
Cantidad INT NOT NULL,
PrecioUnitario DECIMAL (18,2) NOT NULL
)
go

CREATE TABLE MovimientosStock (
Id INT PRIMARY KEY IDENTITY(1,1),
IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(Id),
IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id),
Fecha DATETIME NOT NULL DEFAULT GETDATE(),
Cantidad INT NOT NULL,
EsIngreso BIT NOT NULL, -- 1. ingreso = positivo 2. egreso = rotura/robo
Motivo VARCHAR(200) NOT NULL
)
GO


----------------------- INSERT DE DATOS PARA PERFILES Y USUARIOS -----------------------
INSERT INTO Perfiles(Id, Descripcion) VALUES (1, 'Administrador'), (2, 'Vendedor')

INSERT INTO Usuarios(Username, Password, IdPerfil) VALUES ('admin', 'Aa123456', 1)