using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comercial_Estefanny.Services;

namespace Comercial_Estefannny.ViewModel
{
    public class Marca : ViewModelbase
    {

        private int _idMarca;
        public int IdMarca
        {
            get => _idMarca;
            set
            {
                _idMarca = value;
                OnPropertyChanged(nameof(IdMarca));
            }
        }

        private string _nombreMarca;
        public string NombreMarca
        {
            get => _nombreMarca;
            set
            {
                _nombreMarca = value;
                OnPropertyChanged(nameof(NombreMarca));
            }
        }

        // Constructor
        public Marca() { }

        public Marca(int idMarca, string nombreMarca)
        {
            IdMarca = idMarca;
            NombreMarca = nombreMarca;
        }

        // Método para obtener la conexión a la base de datos
        private static SQLiteConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString;
            return new SQLiteConnection(connectionString);
        }


        // ------------------------- Método para Insertar Marca -------------------------

        // Insertar una nueva marca
        public static void InsertarMarca(string nombreMarca)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("INSERT INTO marca (nombre_marca) VALUES (@nombre)", conn);
                cmd.Parameters.AddWithValue("@nombre", nombreMarca);
                cmd.ExecuteNonQuery();
            }
        }

        // ------------------------- Método para Obtener Marcas -------------------------

        private static List<string> marcasCache = new List<string>();

        // Método para obtener todas las marcas desde la base de datos
        public static List<string> ObtenerMarcas()
        {
            // Si la lista está vacía, la cargamos desde la base de datos
            if (!marcasCache.Any())
            {
                CargarMarcasDesdeDB();
            }

            return marcasCache;
        }

        // Método para cargar las marcas desde la base de datos
        private static void CargarMarcasDesdeDB()
        {
            marcasCache.Clear();  // Limpiar la lista de marcas en memoria

            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT nombre_marca FROM marca", conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    marcasCache.Add(reader.GetString(0));
                }
            }
        }



        // Añadir método para actualizar marca por nombre
        public static void ActualizarMarcaPorNombre(string nombreActual, string nuevoNombre)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("UPDATE marca SET nombre_marca = @nuevoNombre WHERE nombre_marca = @nombreActual", conn);
                cmd.Parameters.AddWithValue("@nuevoNombre", nuevoNombre);
                cmd.Parameters.AddWithValue("@nombreActual", nombreActual);
                cmd.ExecuteNonQuery();
            }
        }

        // Añadir método para obtener ID de marca por nombre
        public static int ObtenerIdPorNombre(string nombreMarca)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT id_marca FROM marca WHERE nombre_marca = @nombre", conn);
                cmd.Parameters.AddWithValue("@nombre", nombreMarca);

                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1; // -1 si no se encontró
            }
        }

        // Añadir método para verificar existencia de una marca por nombre
        public static bool ExisteMarca(string nombreMarca)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT COUNT(*) FROM marca WHERE nombre_marca = @nombre", conn);
                cmd.Parameters.AddWithValue("@nombre", nombreMarca);

                long count = (long)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public static void EliminarMarca(int idMarca)
        {
            using (var conn = new SQLiteConnection(@"Data Source=.\comercial Estefanny base datos.db;Version=3;"))
            {
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM marca WHERE id_marca = @id", conn);
                cmd.Parameters.AddWithValue("@id", idMarca);
                cmd.ExecuteNonQuery();
            }
        }
        public static void EliminarMarcaPorNombre(string nombreMarca)
        {
            using (var conn = new SQLiteConnection(@"Data Source=.\comercial Estefanny base datos.db;Version=3;"))
            {
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM marca WHERE nombre_marca = @nombre", conn);
                cmd.Parameters.AddWithValue("@nombre", nombreMarca);
                cmd.ExecuteNonQuery();
            }
        }
       


    }
}





