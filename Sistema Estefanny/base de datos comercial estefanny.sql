-- Crear la base de datos
CREATE DATABASE comercial_estefanny;
GO

-- Usar la base de datos creada
USE comercial_estefanny;
GO

-- Tabla Categorias
CREATE TABLE Categorias (
    CategoriaID INT PRIMARY KEY IDENTITY(1,1),
    NombreCategoria NVARCHAR(50) COLLATE Latin1_General_CI_AS NOT NULL,
    Codigo VARCHAR(5) UNIQUE NOT NULL
);
GO

-- Tabla Marcas
CREATE TABLE Marcas (
    MarcaID INT PRIMARY KEY IDENTITY(1,1),
    NombreMarca NVARCHAR(50) COLLATE Latin1_General_CI_AS NOT NULL
);
GO

-- Tabla Productos
CREATE TABLE Productos (
    ProductoID INT PRIMARY KEY IDENTITY(1,1),
    CodigoProducto VARCHAR(10) UNIQUE NOT NULL,
    CategoriaID INT NOT NULL,
    MarcaID INT NOT NULL,
    NombreProducto NVARCHAR(100) COLLATE Latin1_General_CI_AS NOT NULL,
    Variante NVARCHAR(50) COLLATE Latin1_General_CI_AS,
    CodigoBarras VARCHAR(50) UNIQUE,
    PrecioVenta DECIMAL(10, 2) NOT NULL,
    PrecioCompra DECIMAL(10, 2),
    FechaCaducidad DATE,
    Stock INT DEFAULT 0,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CategoriaID) REFERENCES Categorias(CategoriaID),
    FOREIGN KEY (MarcaID) REFERENCES Marcas(MarcaID)
);
GO

-- Tabla Clientes
CREATE TABLE Clientes (
    ClienteID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) COLLATE Latin1_General_CI_AS NOT NULL,
    Direccion NVARCHAR(255) COLLATE Latin1_General_CI_AS,
    Telefono NVARCHAR(15) COLLATE Latin1_General_CI_AS,
    DeudaTotal DECIMAL(10, 2) DEFAULT 0
);
GO

-- Tabla Proveedores
CREATE TABLE Proveedores (
    ProveedorID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) COLLATE Latin1_General_CI_AS NOT NULL,
    Direccion NVARCHAR(255) COLLATE Latin1_General_CI_AS,
    Telefono NVARCHAR(15) COLLATE Latin1_General_CI_AS,
    DeudaTotal DECIMAL(10, 2) DEFAULT 0
);
GO

-- Tabla Compras
CREATE TABLE Compras (
    CompraID INT PRIMARY KEY IDENTITY(1,1),
    ProveedorID INT NOT NULL,
    FechaCompra DATETIME DEFAULT GETDATE(),
    TipoPago NVARCHAR(20) COLLATE Latin1_General_CI_AS NOT NULL,
    TotalCompra DECIMAL(10, 2) NOT NULL,
    DescuentoTotal DECIMAL(10, 2) DEFAULT 0,
    FOREIGN KEY (ProveedorID) REFERENCES Proveedores(ProveedorID)
);
GO

-- Tabla DetalleCompras
CREATE TABLE DetalleCompras (
    DetalleCompraID INT PRIMARY KEY IDENTITY(1,1),
    CompraID INT NOT NULL,
    ProductoID INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioCompra DECIMAL(10, 2) NOT NULL,
    FechaCaducidad DATE,
    Descuento DECIMAL(10, 2) DEFAULT 0,
    FOREIGN KEY (CompraID) REFERENCES Compras(CompraID) ON DELETE CASCADE,
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID)
);
GO

-- Tabla Ventas
CREATE TABLE Ventas (
    VentaID INT PRIMARY KEY IDENTITY(1,1),
    CodigoVenta VARCHAR(10) UNIQUE NOT NULL,
    ClienteID INT NOT NULL,
    FechaVenta DATETIME DEFAULT GETDATE(),
    TipoPago NVARCHAR(20) COLLATE Latin1_General_CI_AS NOT NULL,
    TotalVenta DECIMAL(10, 2) NOT NULL,
    DescuentoTotal DECIMAL(10, 2) DEFAULT 0,
    FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID)
);
GO

-- Tabla DetalleVentas
CREATE TABLE DetalleVentas (
    DetalleVentaID INT PRIMARY KEY IDENTITY(1,1),
    VentaID INT NOT NULL,
    ProductoID INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10, 2) NOT NULL,
    Descuento DECIMAL(10, 2) DEFAULT 0,
    FOREIGN KEY (VentaID) REFERENCES Ventas(VentaID) ON DELETE CASCADE,
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID)
);
GO

-- Tabla Kardex
CREATE TABLE Kardex (
    KardexID INT PRIMARY KEY IDENTITY(1,1),
    ProductoID INT NOT NULL,
    FechaMovimiento DATETIME DEFAULT GETDATE(),
    TipoMovimiento NVARCHAR(20) COLLATE Latin1_General_CI_AS NOT NULL, -- 'entrada' o 'salida'
    Cantidad INT NOT NULL,
    ReferenciaID INT, -- ID de referencia para compras o ventas
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID)
);
GO
