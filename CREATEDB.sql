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
Descripcion VARCHAR(100) NOT NULL
)
go

-- Marcas
CREATE TABLE Marcas(
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, -- campos autonumericos
Descripcion VARCHAR(100) NOT NULL
)
GO

-- Clientes
CREATE TABLE Clientes(
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, -- campos autonumericos
Nombre VARCHAR(100) NOT NULL,
Apellido VARCHAR(100) NOT NULL,
Email VARCHAR(150) UNIQUE, -- para evitar duplicados de mails
Telefono VARCHAR(50)
)
go

-- Proveedores
CREATE TABLE Proveedores(
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, -- campos autonumericos
Nombre VARCHAR(100),
RazonSocial VARCHAR(200) NOT NULL,
CUIT VARCHAR(13) NOT NULL UNIQUE, -- no puede haber dos clientes con mismo CUIT
Email VARCHAR(150),
Telefono VARCHAR(50)
)
go

-- Perfiles
CREATE TABLE Perfiles(
Id INT PRIMARY KEY NOT NULL,
Descripcion VARCHAR(50) NOT NULL
)
GO

-- Creacion de tablas con dependencias

-- Usuarios
CREATE TABLE Usuarios(
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, -- autonumerico
Username VARCHAR(100) NOT NULL UNIQUE, -- debe ser unico
Password VARCHAR(100) NOT NULL,
IdPerfil INT NOT NULL FOREIGN KEY REFERENCES Perfiles(Id),
Activo BIT NOT NULL DEFAULT 1
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
PorcentajeGanancia DECIMAL(4,2) NOT NULL DEFAULT 30.00 -- porcentaje por defecto sera del 30%
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
