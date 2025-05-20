using Comercial_Estefanny.Services;
using DocumentFormat.OpenXml.VariantTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;

namespace Comercial_Estefannny.ViewModel
{
    public class Producto
    {
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

        private ExcelImportService _excelImportService = new ExcelImportService();

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

                // Verificar si el producto con el mismo nombre y variante ya existe
                var checkCmd = new SQLiteCommand("SELECT COUNT(*) FROM producto WHERE nombre_producto = @nombre AND variante = @variante", conn);
                checkCmd.Parameters.AddWithValue("@nombre", NombreProducto);
                checkCmd.Parameters.AddWithValue("@variante", Variante);
                var count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    Console.WriteLine("El producto con ese nombre y variante ya existe.");
                    return; // Salir del método sin realizar la inserción
                }

                // Insertar el nuevo producto
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

                // Obtener y asignar los nombres de la categoría y la marca para el producto insertado
                var categoriaCmd = new SQLiteCommand("SELECT nombre_categoria FROM categoria WHERE id_categoria = @categoria", conn);
                categoriaCmd.Parameters.AddWithValue("@categoria", IdCategoria);
                NombreCategoria = categoriaCmd.ExecuteScalar()?.ToString();

                var marcaCmd = new SQLiteCommand("SELECT nombre_marca FROM marca WHERE id_marca = @marca", conn);
                marcaCmd.Parameters.AddWithValue("@marca", IdMarca);
                NombreMarca = marcaCmd.ExecuteScalar()?.ToString();

                // Agregar el producto al caché tras inserción exitosa
                productosCache.Add(this);
            }
        }



        public static ObservableCollection<Producto> ObtenerProductos()
        {
            if (!productosCache.Any())
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    var cmd = new SQLiteCommand("SELECT p.id_producto, p.nombre_producto, p.id_categoria, p.id_marca, p.variante, p.codigo_barras, p.precio_venta, p.precio_compra, p.cantidad, c.nombre_categoria, m.nombre_marca " +
                                                "FROM producto p " +
                                                "JOIN categoria c ON p.id_categoria = c.id_categoria " +
                                                "JOIN marca m ON p.id_marca = m.id_marca", conn);
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

            return productosCache;
        }

        public static ObservableCollection<Producto> ObtenerProductos1()
        {
            if (!productosCache.Any())
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    var cmd = new SQLiteCommand("SELECT p.id_producto, p.nombre_producto, p.id_categoria, p.id_marca, p.variante, p.codigo_barras, p.precio_venta, p.precio_compra, p.cantidad, c.nombre_categoria, m.nombre_marca " +
                                                "FROM producto p " +
                                                "JOIN categoria c ON p.id_categoria = c.id_categoria " +
                                                "JOIN marca m ON p.id_marca = m.id_marca", conn);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        productosCache.Add(new Producto
                        {
                            IdProducto = reader.GetInt32(0),
                            NombreProducto = reader.GetString(5),
                            IdCategoria = reader.GetInt32(1),
                            IdMarca = reader.GetInt32(2),
                            Variante = reader.GetString(3),
                            PrecioVenta = reader.GetDecimal(4),
                            PrecioCompra = reader.GetDecimal(5),
                            Cantidad = reader.GetInt32(6),
            
                        });
                    }
                }
            }

            return productosCache;
        }
        public static List<Producto> FiltrarProductos(string textoBusqueda)
        {
            using (var conn =GetConnection())
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
            using (var conn =GetConnection())
            {
                conn.Open();

                // Ejecuta la eliminación en la base de datos
                var cmd = new SQLiteCommand("DELETE FROM producto WHERE id_producto = @idProducto", conn);
                cmd.Parameters.AddWithValue("@idProducto", IdProducto);
                cmd.ExecuteNonQuery();
            }

            // Eliminar el producto de la caché
            productosCache.Remove(this);
        }


        public void ActualizarProducto()
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                // Comando SQL para actualizar todos los campos del producto
                var cmd = new SQLiteCommand("UPDATE producto SET " +
                                            "nombre_producto = @nombreProducto, " +
                                            "id_categoria = @idCategoria, " +
                                            "id_marca = @idMarca, " +
                                            "variante = @variante, " +
                                            "codigo_barras = @codigoBarras, " +
                                            "precio_venta = @precioVenta, " +
                                            "precio_compra = @precioCompra, " +
                                            "cantidad = @cantidad " +
                                            "WHERE id_producto = @idProducto", conn);

                // Asignar valores a los parámetros
                cmd.Parameters.AddWithValue("@idProducto", IdProducto);
                cmd.Parameters.AddWithValue("@nombreProducto", NombreProducto);
                cmd.Parameters.AddWithValue("@idCategoria", IdCategoria);
                cmd.Parameters.AddWithValue("@idMarca", IdMarca);
                cmd.Parameters.AddWithValue("@variante", Variante);
                cmd.Parameters.AddWithValue("@codigoBarras", CodigoBarras);
                cmd.Parameters.AddWithValue("@precioCompra", PrecioCompra);
                cmd.Parameters.AddWithValue("@precioVenta", PrecioVenta);
                cmd.Parameters.AddWithValue("@cantidad", Cantidad);

                // Ejecutar el comando SQL para actualizar el producto
                cmd.ExecuteNonQuery();
            }
        }
        public static bool ExisteProductoConMarca(string marca)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT COUNT(*) FROM producto WHERE id_marca = (SELECT id_marca FROM marca WHERE nombre_marca = @marca)", conn);
                cmd.Parameters.AddWithValue("@marca", marca);

                long count = (long)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        public static bool ExisteProductoConCategoria(string categoria)
        {
            using (var conn =GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT COUNT(*) FROM producto WHERE id_categoria = (SELECT id_categoria FROM categoria WHERE nombre_categoria = @categoria)", conn);
                cmd.Parameters.AddWithValue("@categoria", categoria);

                long count = (long)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        public static bool ExisteProductoConVariante(string variante)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT COUNT(*) FROM producto WHERE variante = @variante", conn);
                cmd.Parameters.AddWithValue("@variante", variante);

                long count = (long)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public void ImportarProductosDesdeExcel(string filePath)
        {
            // Importar productos desde el archivo Excel
            var productosImportados = _excelImportService.ImportarProductosDesdeExcel(filePath);

            // Agregar los productos importados a la colección
            foreach (var producto in productosImportados)
            {
                string nombreMarca = producto.NombreMarca; // Asumimos que 'producto' tiene una propiedad 'NombreMarca'
                string nombreCategoria = producto.NombreCategoria; // Similar para la categoría
                string variante = producto.Variante; // Y para la variante

                // Verificar si la marca ya existe
                if (!Marca.ExisteMarca(nombreMarca))
                {
                    Marca.InsertarMarca(nombreMarca);
                }

                // Verificar si la categoría ya existe
                if (!Categoria.ExisteCategoria(nombreCategoria))
                {
                    Categoria nuevaCategoria = new Categoria();
                    nuevaCategoria.CrearCategoria(nombreCategoria);
                }

                // Verificar si la variante ya existe
                if (!VarianteC.ExisteVarianteC(variante))
                {
                    VarianteC nuevaVariante = new VarianteC();
                    nuevaVariante.CrearVarianteC(variante);
                }

                // Verificar que la función Obtiene el Id correcto
                int idMarca = Marca.ObtenerIdPorNombre(nombreMarca);
                if (idMarca == 0)
                {
                    Console.WriteLine("ID de marca no encontrado para: " + nombreMarca);
                }

                int idCategoria = Categoria.ObtenerIdPorNombre(nombreCategoria);
                if (idCategoria == 0)
                {
                    Console.WriteLine("ID de categoría no encontrado para: " + nombreCategoria);
                }


                // Ahora asociamos estos valores al producto y lo insertamos
                producto.IdMarca = idMarca;
                producto.IdCategoria = idCategoria;
                producto.InsertarProducto();
            }
        }

    }
}
