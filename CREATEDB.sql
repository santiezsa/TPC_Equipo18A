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

