using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Comercial_Estefanny.Services
{
    public class DataService
    {

        // Conexión a la base de datos
        private static DataService _instance;
        private readonly string connectionString = @"Data Source=.\comercial Estefanny base datos.db;Version=3;";
        // Constructor privado para evitar que se creen instancias fuera de la clase
        private DataService() { }

        // Propiedad para acceder a la instancia única
        public static DataService Instance
        {
            get
            {
                // Si la instancia aún no ha sido creada, la crea
                if (_instance == null)
                {
                    _instance = new DataService();
                }
                return _instance;
            }
        }

        // Método para obtener una conexión a la base de datos
        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        // Método para probar la conexión
        public void TestConnection()
        {
            using (SQLiteConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Conexión a la base de datos exitosa.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al conectar a la base de datos: " + ex.Message);
                }
            }
        }

        // ------------------------- Métodos CRUD para Categorías -------------------------

        // Crear una nueva categoría
        public void CrearCategoria(string nombreCategoria)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("INSERT INTO categoria (nombre_categoria) VALUES (@nombre)", conn);
                cmd.Parameters.AddWithValue("@nombre", nombreCategoria);
                cmd.ExecuteNonQuery();
            }
        }

        // Obtener todas las categorías
        public List<string> ObtenerCategorias()
        {
            var categorias = new List<string>();
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT nombre_categoria FROM categoria", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    categorias.Add(reader.GetString(0));
                }
            }
            return categorias;
        }

        // Actualizar una categoría por ID
        public void ActualizarCategoria(int idCategoria, string nuevoNombre)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("UPDATE categoria SET nombre_categoria = @nombre WHERE id_categoria = @id", conn);
                cmd.Parameters.AddWithValue("@nombre", nuevoNombre);
                cmd.Parameters.AddWithValue("@id", idCategoria);
                cmd.ExecuteNonQuery();
            }
        }

        // Eliminar una categoría por ID
        public void EliminarCategoria(int idCategoria)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM categoria WHERE id_categoria = @id", conn);
                cmd.Parameters.AddWithValue("@id", idCategoria);
                cmd.ExecuteNonQuery();
            }
        }


        // ------------------------- Métodos CRUD para Productos -------------------------

        // Crear un nuevo producto
        public void CrearProducto(string nombreProducto, int idCategoria, int idMarca, string variante, string codigoBarras, double precioVenta, double precioCompra, int cantidad)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("INSERT INTO producto (nombre_producto, id_categoria, id_marca, variante, codigo_barras, precio_venta, precio_compra, cantidad) VALUES (@nombre, @categoria, @marca, @variante, @codigo, @venta, @compra, @cantidad)", conn);
                cmd.Parameters.AddWithValue("@nombre", nombreProducto);
                cmd.Parameters.AddWithValue("@categoria", idCategoria);
                cmd.Parameters.AddWithValue("@marca", idMarca);
                cmd.Parameters.AddWithValue("@variante", variante);
                cmd.Parameters.AddWithValue("@codigo", codigoBarras);
                cmd.Parameters.AddWithValue("@venta", precioVenta);
                cmd.Parameters.AddWithValue("@compra", precioCompra);
                cmd.Parameters.AddWithValue("@cantidad", cantidad);
                cmd.ExecuteNonQuery();
            }
        }

        // Obtener todos los productos
        public List<string> ObtenerProductos()
        {
            var productos = new List<string>();
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT nombre_producto FROM producto", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    productos.Add(reader.GetString(0));
                }
            }
            return productos;
        }

        // Actualizar un producto por ID
        public void ActualizarProducto(int idProducto, string nuevoNombre, int idCategoria, int idMarca, string variante, string codigoBarras, double precioVenta, double precioCompra, int cantidad)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("UPDATE producto SET nombre_producto = @nombre, id_categoria = @categoria, id_marca = @marca, variante = @variante, codigo_barras = @codigo, precio_venta = @venta, precio_compra = @compra, cantidad = @cantidad WHERE id_producto = @id", conn);
                cmd.Parameters.AddWithValue("@nombre", nuevoNombre);
                cmd.Parameters.AddWithValue("@categoria", idCategoria);
                cmd.Parameters.AddWithValue("@marca", idMarca);
                cmd.Parameters.AddWithValue("@variante", variante);
                cmd.Parameters.AddWithValue("@codigo", codigoBarras);
                cmd.Parameters.AddWithValue("@venta", precioVenta);
                cmd.Parameters.AddWithValue("@compra", precioCompra);
                cmd.Parameters.AddWithValue("@cantidad", cantidad);
                cmd.Parameters.AddWithValue("@id", idProducto);
                cmd.ExecuteNonQuery();
            }
        }

        // Eliminar un producto por ID
        public void EliminarProducto(int idProducto)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM producto WHERE id_producto = @id", conn);
                cmd.Parameters.AddWithValue("@id", idProducto);
                cmd.ExecuteNonQuery();
            }
        }

        // ------------------------- Métodos CRUD para Clientes -------------------------

        // Crear un nuevo cliente
        public void CrearCliente(string nombreCliente, string direccion, string telefono)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("INSERT INTO cliente (nombre_cliente, direccion, telefono) VALUES (@nombre, @direccion, @telefono)", conn);
                cmd.Parameters.AddWithValue("@nombre", nombreCliente);
                cmd.Parameters.AddWithValue("@direccion", direccion);
                cmd.Parameters.AddWithValue("@telefono", telefono);
                cmd.ExecuteNonQuery();
            }
        }

        // Obtener todos los clientes
        public List<string> ObtenerClientes()
        {
            var clientes = new List<string>();
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT nombre_cliente FROM cliente", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    clientes.Add(reader.GetString(0));
                }
            }
            return clientes;
        }

        // Actualizar un cliente por ID
        public void ActualizarCliente(int idCliente, string nuevoNombre, string nuevaDireccion, string nuevoTelefono)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("UPDATE cliente SET nombre_cliente = @nombre, direccion = @direccion, telefono = @telefono WHERE id_cliente = @id", conn);
                cmd.Parameters.AddWithValue("@nombre", nuevoNombre);
                cmd.Parameters.AddWithValue("@direccion", nuevaDireccion);
                cmd.Parameters.AddWithValue("@telefono", nuevoTelefono);
                cmd.Parameters.AddWithValue("@id", idCliente);
                cmd.ExecuteNonQuery();
            }
        }

        // Eliminar un cliente por ID
        public void EliminarCliente(int idCliente)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM cliente WHERE id_cliente = @id", conn);
                cmd.Parameters.AddWithValue("@id", idCliente);
                cmd.ExecuteNonQuery();
            }
        }

        // ------------------------- Métodos CRUD para Ventas -------------------------

        // Crear una nueva venta
        public void CrearVenta(int idCliente, string codigoVenta, string tipoPago)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("INSERT INTO venta (id_cliente, codigo_venta, tipo_pago) VALUES (@cliente, @codigo, @tipoPago)", conn);
                cmd.Parameters.AddWithValue("@cliente", idCliente);
                cmd.Parameters.AddWithValue("@codigo", codigoVenta);
                cmd.Parameters.AddWithValue("@tipoPago", tipoPago);
                cmd.ExecuteNonQuery();
            }
        }

        // Obtener todas las ventas
        public List<string> ObtenerVentas()
        {
            var ventas = new List<string>();
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT codigo_venta FROM venta", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ventas.Add(reader.GetString(0));
                }
            }
            return ventas;
        }

        // Actualizar una venta por ID
        public void ActualizarVenta(int idVenta, int idCliente, string codigoVenta, string tipoPago)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("UPDATE venta SET id_cliente = @cliente, codigo_venta = @codigo, tipo_pago = @tipoPago WHERE id_venta = @id", conn);
                cmd.Parameters.AddWithValue("@cliente", idCliente);
                cmd.Parameters.AddWithValue("@codigo", codigoVenta);
                cmd.Parameters.AddWithValue("@tipoPago", tipoPago);
                cmd.Parameters.AddWithValue("@id", idVenta);
                cmd.ExecuteNonQuery();
            }
        }

        // Eliminar una venta por ID
        public void EliminarVenta(int idVenta)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM venta WHERE id_venta = @id", conn);
                cmd.Parameters.AddWithValue("@id", idVenta);
                cmd.ExecuteNonQuery();
            }
        }
        // ------------------------- Métodos CRUD para Compras -------------------------

        // Crear una nueva compra
        public void CrearCompra(int idProveedor, string fecha, string tipoPago)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("INSERT INTO compra (id_proveedor, fecha, tipo_pago) VALUES (@proveedor, @fecha, @tipoPago)", conn);
                cmd.Parameters.AddWithValue("@proveedor", idProveedor);
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.Parameters.AddWithValue("@tipoPago", tipoPago);
                cmd.ExecuteNonQuery();
            }
        }

        // Obtener todas las compras
        public List<string> ObtenerCompras()
        {
            var compras = new List<string>();
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT id_compra, fecha, tipo_pago FROM compra", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    compras.Add($"ID: {reader.GetInt32(0)}, Fecha: {reader.GetString(1)}, Tipo de Pago: {reader.GetString(2)}");
                }
            }
            return compras;
        }

        // Actualizar una compra por ID
        public void ActualizarCompra(int idCompra, int idProveedor, string nuevaFecha, string nuevoTipoPago)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("UPDATE compra SET id_proveedor = @proveedor, fecha = @fecha, tipo_pago = @tipoPago WHERE id_compra = @id", conn);
                cmd.Parameters.AddWithValue("@proveedor", idProveedor);
                cmd.Parameters.AddWithValue("@fecha", nuevaFecha);
                cmd.Parameters.AddWithValue("@tipoPago", nuevoTipoPago);
                cmd.Parameters.AddWithValue("@id", idCompra);
                cmd.ExecuteNonQuery();
            }
        }

        // Eliminar una compra por ID
        public void EliminarCompra(int idCompra)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM compra WHERE id_compra = @id", conn);
                cmd.Parameters.AddWithValue("@id", idCompra);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
