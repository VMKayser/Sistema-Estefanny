using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.SQLite;
using System.Configuration;
using DocumentFormat.OpenXml.Bibliography;

namespace Comercial_Estefannny.ViewModel
{
    public class Ventas : ViewModelbase
    {
        // Propiedades
        public ObservableCollection<ClientesC> Clientes { get; set; }
        public ObservableCollection<ProductoVenta> Productos { get; set; }
        public ObservableCollection<string> MetodosDePago { get; set; }
        private InventarioC inventario;
        private ClientesC _cliente;
        private SQLiteConnection connection;
       public string NombreEntidad { get; set; }
        private bool _esCredito;


        public ClientesC Cliente
        {
            get => _cliente;
            set { _cliente = value; OnPropertyChanged(nameof(Cliente)); }
        }

        public bool EsCredito
        {
            get => _esCredito;
            set { _esCredito = value; OnPropertyChanged(nameof(EsCredito)); }
        }

        public DateTime Fecha { get; set; }
        public string MetodoPago { get; set; }

        // Constructores
        public Ventas(SQLiteConnection dbConnection)
        {
            connection = dbConnection;
        }

        public Ventas()
        {
            Clientes = new ObservableCollection<ClientesC>(ClientesC.ObtenerClientes());
            
            Productos = new ObservableCollection<ProductoVenta>();
            Fecha = DateTime.Now;
            MetodoPago = "Efectivo";
            MetodosDePago = new ObservableCollection<string> { "Efectivo", "Crédito", "QR" };

        }

        private SQLiteConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString;
            return new SQLiteConnection(connectionString);
        }

        // Métodos para manipular productos en la venta
        public void AgregarProducto(int idProducto, string nombreProducto, int cantidad, decimal precio, decimal descuento)
        {
            var nuevoProductoVenta = new ProductoVenta(idProducto, nombreProducto, cantidad, precio, descuento);
            Productos.Add(nuevoProductoVenta);
            OnPropertyChanged(nameof(Productos));
        }

        public void EliminarProducto(ProductoVenta producto)
        {
            if (Productos.Contains(producto))
            {
                Productos.Remove(producto);
            }
        }

        public void EditarProducto(ProductoVenta producto, int cantidad, decimal precio, decimal descuento)
        {
            var productoExistente = Productos.FirstOrDefault(p => p.IdProducto == producto.IdProducto);
            if (productoExistente != null)
            {
                productoExistente.Cantidad = cantidad;
                productoExistente.Precio = precio;
                productoExistente.Descuento = descuento;
                OnPropertyChanged(nameof(Productos));
            }
        }

        // Métodos de filtrado
        public void FiltrarClientes(string textoBusqueda)
        {
            var clientesFiltrados = ClientesC.ObtenerClientes()
                                             .Where(c => c.Nombre.ToLower().Contains(textoBusqueda.ToLower()))
                                             .ToList();
            Clientes = new ObservableCollection<ClientesC>(clientesFiltrados);
            OnPropertyChanged(nameof(Clientes));
        }

        // Propiedades calculadas
        public decimal TotalVenta => Productos.Sum(p => p.Total);
        public decimal TotalCantidad => Productos.Sum(p => p.Cantidad);
        public decimal DescuentoTotal => Productos.Sum(p => p.Descuento);
        public decimal TotalAntesDescuento => Productos.Sum(p => p.Cantidad * p.Precio);

        // Registro de venta
        public void RegistrarVenta(DateTime fechaVenta, string nombreCliente, string tipoPago, ObservableCollection<ProductoVenta> productosVenta)
        {

            using (var conn = GetConnection())
            {
                conn.Open();
                int idCliente = ClientesC.ObtenerIdClientePorNombre(nombreCliente);
                if (idCliente == -1) return;

                var cmdVenta = new SQLiteCommand("INSERT INTO venta (id_cliente, fecha, tipo_pago, deuda_cliente) VALUES (@id_cliente, @fecha, @tipo_pago, @deuda_cliente)", conn);
                cmdVenta.Parameters.AddWithValue("@id_cliente", idCliente);
                cmdVenta.Parameters.AddWithValue("@fecha", fechaVenta.ToString("yyyy-MM-dd HH:mm:ss"));
                cmdVenta.Parameters.AddWithValue("@tipo_pago", tipoPago);
                cmdVenta.Parameters.AddWithValue("@deuda_cliente", 0);
                cmdVenta.ExecuteNonQuery();

                int idVenta = (int)conn.LastInsertRowId;
                foreach (var producto in productosVenta)
                {
                    var cmdDetalle = new SQLiteCommand("INSERT INTO detalle_venta (id_venta, id_producto, cantidad, precio, descuento) VALUES (@id_venta, @id_producto, @cantidad, @precio, @descuento)", conn);
                    cmdDetalle.Parameters.AddWithValue("@id_venta", idVenta);
                    cmdDetalle.Parameters.AddWithValue("@id_producto", producto.IdProducto);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", producto.Cantidad);
                    cmdDetalle.Parameters.AddWithValue("@precio", producto.Precio);
                    cmdDetalle.Parameters.AddWithValue("@descuento", producto.Descuento);
                    cmdDetalle.ExecuteNonQuery();
                    var cmdActualizarCantidad = new SQLiteCommand("UPDATE producto SET cantidad = cantidad-@cantidad WHERE id_producto = @id_producto", conn);
                    cmdActualizarCantidad.Parameters.AddWithValue("@cantidad", producto.Cantidad);
                    cmdActualizarCantidad.Parameters.AddWithValue("@id_producto", producto.IdProducto);
                    cmdActualizarCantidad.ExecuteNonQuery();
                }

                if (tipoPago == "Crédito")
                {
                    decimal deudaTotal = productosVenta.Sum(p => p.Total);
                    var updateDeudaCmd = new SQLiteCommand("UPDATE cliente SET deuda = deuda + @deuda WHERE id_cliente = @id_cliente", conn);
                    updateDeudaCmd.Parameters.AddWithValue("@deuda", deudaTotal);
                    updateDeudaCmd.Parameters.AddWithValue("@id_cliente", idCliente);
                    updateDeudaCmd.ExecuteNonQuery();
                }
                
            }
        }

        // Obtención de ventas
        public static ObservableCollection<Ventas> ObtenerVentas(SQLiteConnection connection)
        {
            var ventas = new ObservableCollection<Ventas>();

            // Consulta para obtener las ventas con los detalles del cliente
            string queryVentas = @"
    SELECT v.id_venta, v.fecha, v.tipo_pago, v.deuda_cliente, c.nombre_cliente 
    FROM venta v
    INNER JOIN cliente c ON v.id_cliente = c.id_cliente";

            using (var cmd = new SQLiteCommand(queryVentas, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var venta = new Ventas
                    {
                        Fecha = DateTime.Parse(reader["fecha"].ToString()),
                        Cliente = new ClientesC { Nombre = reader["nombre_cliente"].ToString() },
                        MetodoPago = reader["tipo_pago"].ToString(),
                        Productos = new ObservableCollection<ProductoVenta>()
                    };

                    int idVenta = Convert.ToInt32(reader["id_venta"]);

                    // Consulta para obtener los productos relacionados con la venta
                    string queryProductos = @"
            SELECT p.id_producto, p.nombre_producto, dv.cantidad, dv.precio, dv.descuento
            FROM detalle_venta dv
            INNER JOIN producto p ON dv.id_producto = p.id_producto
            WHERE dv.id_venta = @idVenta";

                    using (var cmdProducto = new SQLiteCommand(queryProductos, connection))
                    {
                        cmdProducto.Parameters.AddWithValue("@idVenta", idVenta);
                        using (var readerProducto = cmdProducto.ExecuteReader())
                        {
                            while (readerProducto.Read())
                            {
                                var producto = new ProductoVenta(
                                    Convert.ToInt32(readerProducto["id_producto"]),
                                    readerProducto["nombre_producto"].ToString(),
                                    Convert.ToInt32(readerProducto["cantidad"]),
                                    Convert.ToDecimal(readerProducto["precio"]),
                                    Convert.ToDecimal(readerProducto["descuento"])
                                );
                                venta.Productos.Add(producto);
                            }
                        }
                    }

                    ventas.Add(venta);
                }
            }

            return ventas;
        }



    }
}
