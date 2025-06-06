using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.SQLite;
using System.Configuration;

namespace Comercial_Estefannny.ViewModel
{
    public class Compra : ViewModelbase
    {
        public ObservableCollection<Proveedores> Proveedores { get; set; }
        public ObservableCollection<ProductoCompra> ProductosCompra { get; set; }
        private Proveedores _proveedor;
        private SQLiteConnection connection;
        public string NombreEntidad { get; set; }
        // Propiedad de proveedor que se enlaza al ComboBox
        public Proveedores Proveedor
        {
            get => _proveedor;
            set
            {
                _proveedor = value;
                OnPropertyChanged(nameof(Proveedor));
            }
        }

        public Compra(SQLiteConnection dbConnection)
        {
            connection = dbConnection;
        }

        // Constructor
        public Compra()
        {
            // Obtener todos los proveedores desde la base de datos
            Proveedores = new ObservableCollection<Proveedores>(ViewModel.Proveedores.Obtenerproveedores());
            ProductosCompra = new ObservableCollection<ProductoCompra>();

            Fecha = DateTime.Now;
            MetodoPago = "Efectivo"; // Valor predeterminado
            MetodosDePago = new ObservableCollection<string> { "Efectivo", "Transferencia", "Tarjeta" };
        }        // Propiedades
        public DateTime Fecha { get; set; }
        
        // Propiedad para binding del DatePicker
        public DateTime FechaCompra 
        { 
            get => Fecha; 
            set 
            { 
                Fecha = value; 
                OnPropertyChanged(nameof(FechaCompra));
                OnPropertyChanged(nameof(Fecha));
            } 
        }
        
        public string MetodoPago { get; set; }
        public ObservableCollection<string> MetodosDePago { get; set; }

        // Propiedades para cálculos de compra
        public decimal TotalCompra
        {
            get
            {
                return ProductosCompra.Sum(p => p.Total); // Total de todos los productos en la compra
            }
        }

        public decimal TotalCantidad => ProductosCompra.Sum(p => p.Cantidad);

        public decimal DescuentoTotal
        {
            get
            {
                return ProductosCompra.Sum(p => p.Descuento); // Total de los descuentos aplicados
            }
        }

        public decimal TotalAntesDescuento
        {
            get
            {
                return ProductosCompra.Sum(p => p.Cantidad * p.Precio); // Total sin descuento
            }
        }

        private SQLiteConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString;
            return new SQLiteConnection(connectionString);
        }

        // Método para registrar la compra en la base de datos
        public void RegistrarCompra(DateTime fechaCompra, string nombreProveedor, ObservableCollection<ProductoCompra> productosCompra)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                // Obtener el id_proveedor utilizando el nombre del proveedor
                int idProveedor = ViewModel.Proveedores.ObtenerIdProveedorPorNombre(nombreProveedor);

                // Verificar si se encontró el proveedor
                if (idProveedor == -1)
                {
                    Console.WriteLine("Proveedor no encontrado.");
                    return; // Salir si no se encuentra el proveedor
                }

                // Insertar la compra en la tabla 'compra'
                var cmdCompra = new SQLiteCommand("INSERT INTO compra (id_proveedor, fecha) VALUES (@id_proveedor, @fecha)", conn);
                cmdCompra.Parameters.AddWithValue("@id_proveedor", idProveedor);
                cmdCompra.Parameters.AddWithValue("@fecha", fechaCompra.ToString("yyyy-MM-dd HH:mm:ss"));
                cmdCompra.ExecuteNonQuery();

                // Obtener el ID de la compra recién insertada
                var idCompra = (int)conn.LastInsertRowId;

                // Insertar los productos de la compra en la tabla 'detalle_compra' y actualizar inventario
                foreach (var producto in productosCompra)
                {
                    // Insertar en detalle_compra
                    var cmdDetalle = new SQLiteCommand("INSERT INTO detalle_compra (id_compra, id_producto, cantidad, precio_compra, descuento) VALUES (@id_compra, @id_producto, @cantidad, @precio_compra, @descuento)", conn);
                    cmdDetalle.Parameters.AddWithValue("@id_compra", idCompra);
                    cmdDetalle.Parameters.AddWithValue("@id_producto", producto.IdProducto);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", producto.Cantidad);
                    cmdDetalle.Parameters.AddWithValue("@precio_compra", producto.Precio);
                    cmdDetalle.Parameters.AddWithValue("@descuento", producto.Descuento);
                    cmdDetalle.ExecuteNonQuery();

                    // Actualizar la cantidad en la tabla 'producto' (sumar la cantidad comprada)
                    var cmdActualizarCantidad = new SQLiteCommand("UPDATE producto SET cantidad = cantidad + @cantidad WHERE id_producto = @id_producto", conn);
                    cmdActualizarCantidad.Parameters.AddWithValue("@cantidad", producto.Cantidad);
                    cmdActualizarCantidad.Parameters.AddWithValue("@id_producto", producto.IdProducto);
                    cmdActualizarCantidad.ExecuteNonQuery();
                }
            }
        }

        // Método para obtener las compras desde la base de datos
        public static ObservableCollection<Compra> ObtenerCompras(SQLiteConnection connection)
        {
            var compras = new ObservableCollection<Compra>();

            // Consulta para obtener las compras con el proveedor
            string queryCompras = @"
                SELECT c.id_compra, c.fecha, p.nombre_proveedor
                FROM compra c
                INNER JOIN proveedor p ON c.id_proveedor = p.id_proveedor";

            using (var cmd = new SQLiteCommand(queryCompras, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var compra = new Compra
                    {
                        Fecha = DateTime.Parse(reader["fecha"].ToString()),
                        Proveedor = new Proveedores { Nombre = reader["nombre_proveedor"].ToString() },
                        ProductosCompra = new ObservableCollection<ProductoCompra>()
                    };

                    int idCompra = Convert.ToInt32(reader["id_compra"]);
                    string queryProductos = @"
                        SELECT p.id_producto, p.nombre_producto, dc.cantidad, dc.precio_compra, dc.descuento
                        FROM detalle_compra dc
                        INNER JOIN producto p ON dc.id_producto = p.id_producto
                        WHERE dc.id_compra = @idCompra";

                    using (var cmdProducto = new SQLiteCommand(queryProductos, connection))
                    {
                        cmdProducto.Parameters.AddWithValue("@idCompra", idCompra);
                        using (var readerProducto = cmdProducto.ExecuteReader())
                        {
                            while (readerProducto.Read())
                            {
                                var producto = new ProductoCompra(
                                    Convert.ToInt32(readerProducto["id_producto"]),
                                    readerProducto["nombre_producto"].ToString(),
                                    Convert.ToInt32(readerProducto["cantidad"]),
                                    Convert.ToDecimal(readerProducto["precio_compra"]),
                                    Convert.ToDecimal(readerProducto["descuento"])
                                );
                                compra.ProductosCompra.Add(producto);
                            }
                        }
                    }

                    compras.Add(compra);
                }
            }

            return compras;
        }

        public static ObservableCollection<Compra> ObtenerCompras()
        {
            var compras = new ObservableCollection<Compra>();
            using (var conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM compra", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    compras.Add(new Compra
                    {
                        Fecha = DateTime.Parse(reader["fecha"].ToString()),
                        MetodoPago = reader["tipo_pago"].ToString(),
                        // Completa con los campos necesarios
                    });
                }
            }
            return compras;
        }
    }
}
