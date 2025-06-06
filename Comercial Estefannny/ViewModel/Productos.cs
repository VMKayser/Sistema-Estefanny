using Comercial_Estefannny.Services;
using DocumentFormat.OpenXml.VariantTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;

namespace Comercial_Estefannny.ViewModel
{    public class Producto
    {
        public int Id { get; set; }  // Agregado para Entity Framework
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int IdCategoria { get; set; }
        public int IdMarca { get; set; }
        public string Variante { get; set; }
        public string CodigoBarras { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public string NombreCategoria { get; set; }
        public string NombreMarca { get; set; }
        private static ExcelImportService _excelImportService = new ExcelImportService();
        public static ObservableCollection<Producto> productosCache = new ObservableCollection<Producto>();

        private static SQLiteConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString;
            return new SQLiteConnection(connectionString);
        }

        public void InsertarProducto()
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var checkCmd = new SQLiteCommand("SELECT COUNT(*) FROM producto WHERE nombre_producto = @nombre AND variante = @variante", conn);
                checkCmd.Parameters.AddWithValue("@nombre", NombreProducto);
                checkCmd.Parameters.AddWithValue("@variante", Variante);
                var count = Convert.ToInt32(checkCmd.ExecuteScalar());
                if (count > 0)
                {
                    Console.WriteLine("El producto con ese nombre y variante ya existe.");
                    return;
                }
                var cmd = new SQLiteCommand("INSERT INTO producto (nombre_producto, id_categoria, id_marca, variante, codigo_barras, precio_venta, precio_compra, cantidad) VALUES (@nombre, @categoria, @marca, @variante, @codigo, @precioVenta, @precioCompra, @cantidad)", conn);
                cmd.Parameters.AddWithValue("@nombre", NombreProducto);
                cmd.Parameters.AddWithValue("@categoria", IdCategoria);
                cmd.Parameters.AddWithValue("@marca", IdMarca);
                cmd.Parameters.AddWithValue("@variante", Variante);
                cmd.Parameters.AddWithValue("@codigo", CodigoBarras);
                cmd.Parameters.AddWithValue("@precioVenta", PrecioVenta);
                cmd.Parameters.AddWithValue("@precioCompra", PrecioCompra);
                cmd.Parameters.AddWithValue("@cantidad", Cantidad);
                cmd.ExecuteNonQuery();
                var categoriaCmd = new SQLiteCommand("SELECT nombre_categoria FROM categoria WHERE id_categoria = @categoria", conn);
                categoriaCmd.Parameters.AddWithValue("@categoria", IdCategoria);
                NombreCategoria = categoriaCmd.ExecuteScalar()?.ToString();
                var marcaCmd = new SQLiteCommand("SELECT nombre_marca FROM marca WHERE id_marca = @marca", conn);
                marcaCmd.Parameters.AddWithValue("@marca", IdMarca);
                NombreMarca = marcaCmd.ExecuteScalar()?.ToString();
                productosCache.Add(this);
            }
        }

        public static ObservableCollection<Producto> ObtenerProductos()
        {
            if (!productosCache.Any())
            {
                RefrescarCache();
            }
            return productosCache;
        }

        public static void RefrescarCache()
        {
            productosCache.Clear();
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT p.id_producto, p.nombre_producto, p.id_categoria, p.id_marca, p.variante, p.codigo_barras, p.precio_venta, p.precio_compra, p.cantidad, c.nombre_categoria, m.nombre_marca FROM producto p JOIN categoria c ON p.id_categoria = c.id_categoria JOIN marca m ON p.id_marca = m.id_marca", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    productosCache.Add(new Producto
                    {
                        IdProducto = reader.GetInt32(0),
                        NombreProducto = reader.GetString(1),
                        IdCategoria = reader.GetInt32(2),
                        IdMarca = reader.GetInt32(3),
                        Variante = reader.GetString(4),
                        CodigoBarras = reader.GetString(5),
                        PrecioVenta = reader.GetDecimal(6),
                        PrecioCompra = reader.GetDecimal(7),
                        Cantidad = reader.GetInt32(8),
                        NombreCategoria = reader.GetString(9),
                        NombreMarca = reader.GetString(10)
                    });
                }
            }
        }

        public static List<Producto> FiltrarProductos(string textoBusqueda)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT id_producto, nombre_producto, id_categoria, id_marca, variante, codigo_barras, precio_venta, precio_compra, cantidad FROM producto WHERE nombre_producto LIKE @busqueda", conn);
                cmd.Parameters.AddWithValue("@busqueda", $"%{textoBusqueda}%");
                var productosFiltrados = new List<Producto>();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    productosFiltrados.Add(new Producto
                    {
                        IdProducto = reader.GetInt32(0),
                        NombreProducto = reader.GetString(1),
                        IdCategoria = reader.GetInt32(2),
                        IdMarca = reader.GetInt32(3),
                        Variante = reader.GetString(4),
                        CodigoBarras = reader.GetString(5),
                        PrecioCompra = reader.GetDecimal(7),
                        PrecioVenta = reader.GetDecimal(6),
                        Cantidad = reader.GetInt32(8)
                    });
                }
                return productosFiltrados;
            }
        }
        public void EliminarProducto()
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM producto WHERE id_producto = @idProducto", conn);
                cmd.Parameters.AddWithValue("@idProducto", IdProducto);
                cmd.ExecuteNonQuery();
            }
            productosCache.Remove(this);
        }

        public void ActualizarProducto()
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("UPDATE producto SET nombre_producto = @nombreProducto, id_categoria = @idCategoria, id_marca = @idMarca, variante = @variante, codigo_barras = @codigoBarras, precio_venta = @precioVenta, precio_compra = @precioCompra, cantidad = @cantidad WHERE id_producto = @idProducto", conn);
                cmd.Parameters.AddWithValue("@idProducto", IdProducto);
                cmd.Parameters.AddWithValue("@nombreProducto", NombreProducto);
                cmd.Parameters.AddWithValue("@idCategoria", IdCategoria);
                cmd.Parameters.AddWithValue("@idMarca", IdMarca);
                cmd.Parameters.AddWithValue("@variante", Variante);
                cmd.Parameters.AddWithValue("@codigoBarras", CodigoBarras);
                cmd.Parameters.AddWithValue("@precioCompra", PrecioCompra);
                cmd.Parameters.AddWithValue("@precioVenta", PrecioVenta);
                cmd.Parameters.AddWithValue("@cantidad", Cantidad);
                cmd.ExecuteNonQuery();
            }
        }

        public static bool ExisteProductoConCategoria(int idCategoria)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT COUNT(*) FROM producto WHERE id_categoria = @idCategoria", conn);
                cmd.Parameters.AddWithValue("@idCategoria", idCategoria);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        public static bool ExisteProductoConMarca(int idMarca)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT COUNT(*) FROM producto WHERE id_marca = @idMarca", conn);
                cmd.Parameters.AddWithValue("@idMarca", idMarca);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        public static bool ExisteProductoConVariante(int idVariante)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT COUNT(*) FROM producto WHERE variante = @idVariante", conn);
                cmd.Parameters.AddWithValue("@idVariante", idVariante);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        public static void ImportarProductosDesdeExcel(string filePath)
        {
            var productos = _excelImportService.ImportarProductosDesdeExcel(filePath);
            foreach (var producto in productos)
            {
                producto.InsertarProducto();
            }
        }
    }
}
