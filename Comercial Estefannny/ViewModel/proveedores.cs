using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Collections.ObjectModel;
using Comercial_Estefannny.Services;
using System.Configuration;

namespace Comercial_Estefannny.ViewModel
{    public class Proveedores : ViewModelbase
    {
        // Atributos
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public double Deuda { get; set; }
 
        // Caché de proveedores
        public static ObservableCollection<Proveedores> proveedoresCache = new ObservableCollection<Proveedores>();
        private static ExcelImportService _excelImportService = new ExcelImportService();

        public Proveedores()
        {
        }

        // Método para obtener la conexión a la base de datos
        private static SQLiteConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString;
            return new SQLiteConnection(connectionString);
        }

        // Método para obtener los proveedores, usando caché si ya existe
        public static ObservableCollection<Proveedores> Obtenerproveedores()
        {
            // Si el caché está vacío, cargar los datos de la base de datos
            if (!proveedoresCache.Any())
            {
                RefrescarCache();
            }

            // Regresar los proveedores desde el caché en una ObservableCollection
            return proveedoresCache;
        }

        public static ObservableCollection<Proveedores> ObtenerProveedores()
        {
            return Obtenerproveedores();
        }

        // Método para refrescar la caché
        public static void RefrescarCache()
        {
            proveedoresCache.Clear();
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM proveedor", conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var proveedor = new Proveedores
                    {
                        Nombre = reader["nombre_proveedor"].ToString(),
                        Direccion = reader["direccion"].ToString(),
                        Telefono = reader["telefono"].ToString(),
                    };

                    // Añadir proveedor al caché
                    proveedoresCache.Add(proveedor);
                }
            }
        }

        // Método para agregar un proveedor
        public void InsertarProveedor()
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                // Verificar si el proveedor ya existe (puedes verificarlo por nombre o algún otro campo único)
                var checkCmd = new SQLiteCommand("SELECT COUNT(*) FROM proveedor WHERE nombre_proveedor = @nombre", conn);
                checkCmd.Parameters.AddWithValue("@nombre", Nombre);
                var count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    Console.WriteLine("El proveedor con ese nombre ya existe.");
                    return; // Salir del método sin realizar la inserción
                }

                // Insertar el nuevo proveedor
                var cmd = new SQLiteCommand("INSERT INTO proveedor (nombre_proveedor, direccion, telefono) VALUES (@nombre, @direccion, @telefono)", conn);
                cmd.Parameters.AddWithValue("@nombre", Nombre);
                cmd.Parameters.AddWithValue("@direccion", Direccion);
                cmd.Parameters.AddWithValue("@telefono", Telefono);
               
                cmd.ExecuteNonQuery();

                // Agregar el proveedor al caché tras inserción exitosa
                proveedoresCache.Add(this);
            }
        }

        // Método para eliminar un proveedor
        public void EliminarProveedor()
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                // Ejecutar la eliminación en la base de datos
                var cmd = new SQLiteCommand("DELETE FROM proveedor WHERE nombre_proveedor = @nombreProveedor", conn);
                cmd.Parameters.AddWithValue("@nombreProveedor", Nombre);
                cmd.ExecuteNonQuery();
            }

            // Eliminar el proveedor de la caché
            proveedoresCache.Remove(this);
        }

        // Método para actualizar un proveedor
        public void ActualizarProveedor()
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                // Comando SQL para actualizar todos los campos del proveedor
                var cmd = new SQLiteCommand("UPDATE proveedor SET " +
                                             "direccion = @direccion, " +
                                             "telefono = @telefono " +
                                             "WHERE nombre_proveedor = @nombreProveedor", conn);

                // Asignar valores a los parámetros
                cmd.Parameters.AddWithValue("@nombreProveedor", Nombre);
                cmd.Parameters.AddWithValue("@direccion", Direccion);
                cmd.Parameters.AddWithValue("@telefono", Telefono);

                // Ejecutar el comando SQL para actualizar el proveedor
                cmd.ExecuteNonQuery();
            }

            // Actualizar el proveedor en el caché
            var proveedor = proveedoresCache.FirstOrDefault(p => p.Nombre == Nombre);
            if (proveedor != null)
            {
                proveedor.Direccion = Direccion;
                proveedor.Telefono = Telefono;

            }
        }
        public void ImportarProveedoresDesdeExcel(string filePath)
        {
            var proveedores = _excelImportService.ImportarProveedoresDesdeExcel(filePath);

            // Agregar los clientes importados al caché y a la base de datos
            foreach (var proveedor in proveedores)
            {
                proveedor.InsertarProveedor(); // Asumimos que InsertarCliente añade a la base de datos y caché
            }


        }
        public static int ObtenerIdProveedorPorNombre(string nombreProveedor)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                // Comando SQL para obtener el id_proveedor por nombre
                var cmd = new SQLiteCommand("SELECT id_proveedor FROM proveedor WHERE nombre_proveedor = @nombreProveedor", conn);
                cmd.Parameters.AddWithValue("@nombreProveedor", nombreProveedor);

                var result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return -1; // Retorna -1 si no se encuentra el proveedor
                }
            }
        }

    }
}
