using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercial_Estefannny.ViewModel
{
    public class VarianteC : ViewModelbase
    {
        // Propiedades de la clase VarianteC
        private int _idVarianteC;
        public int IdVarianteC
        {
            get => _idVarianteC;
            set
            {
                _idVarianteC = value;
                OnPropertyChanged(nameof(IdVarianteC));
            }
        }

        private string _nombreVarianteC;
        public string NombreVarianteC
        {
            get => _nombreVarianteC;
            set
            {
                _nombreVarianteC = value;
                OnPropertyChanged(nameof(NombreVarianteC));
            }
        }

        // Constructor
        public VarianteC() { }

        public VarianteC(int idVarianteC, string nombreVarianteC)
        {
            IdVarianteC = idVarianteC;
            NombreVarianteC = nombreVarianteC;
        }

        // Método para obtener la conexión a la base de datos
        private static SQLiteConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString;
            return new SQLiteConnection(connectionString);
        }

        // ------------------------- Métodos CRUD para VarianteCs -------------------------

        // Crear una nueva variante
        public void CrearVarianteC(string nombreVarianteC)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("INSERT INTO variantes (nombre_variante) VALUES (@nombre)", conn);
                cmd.Parameters.AddWithValue("@nombre", nombreVarianteC);
                cmd.ExecuteNonQuery();
            }
        }

        // Obtener una variante por su ID
        public static VarianteC ObtenerVarianteCPorId(int idVarianteC)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT id_variante, nombre_variante FROM variantes WHERE id_variante = @id", conn);
                cmd.Parameters.AddWithValue("@id", idVarianteC);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new VarianteC(reader.GetInt32(0), reader.GetString(1));
                }

                return null; // No se encontró la variante
            }
        }

        // Actualizar una variante por ID
        public static void ActualizarVarianteC(int idVarianteC, string nuevoNombre)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("UPDATE variantes SET nombre_variante = @nombre WHERE id_variante = @id", conn);
                cmd.Parameters.AddWithValue("@nombre", nuevoNombre);
                cmd.Parameters.AddWithValue("@id", idVarianteC);
                cmd.ExecuteNonQuery();
            }
        }

  

        // ------------------------- Métodos para obtener todas las variantes -------------------------

        private static List<string> variantesCache = new List<string>();

        // Método para obtener todas las variantes desde la base de datos
        public static List<string> ObtenerVarianteCs()
        {
            // Si la lista está vacía, la cargamos desde la base de datos
            if (!variantesCache.Any())
            {
                CargarVarianteCsDesdeDB();
            }

            return variantesCache;
        }

        // Método para cargar las variantes desde la base de datos
        private static void CargarVarianteCsDesdeDB()
        {
            variantesCache.Clear();  // Limpiar la lista de variantes en memoria

            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT id_variante, nombre_variante FROM variantes", conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    variantesCache.Add(reader.GetString(1));
                }
            }
        }
        public static bool ExisteVarianteC(string nombreVarianteC)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT COUNT(*) FROM variantes WHERE nombre_variante = @nombreVarianteC", conn);
                cmd.Parameters.AddWithValue("@nombreVarianteC", nombreVarianteC);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
        public static void EliminarVarianteC(int idVarianteC)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM variantes WHERE id_variante = @id", conn);
                cmd.Parameters.AddWithValue("@id", idVarianteC);
                cmd.ExecuteNonQuery();
            }
        }
        public static void EliminarVarianteCPorNombre(string nombreVarianteC)
        {
            using (var conn = new SQLiteConnection(@"Data Source=.\comercial Estefanny base datos.db;Version=3;"))
            {
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM variantes WHERE nombre_variante = @nombre", conn);
                cmd.Parameters.AddWithValue("@nombre", nombreVarianteC);
                cmd.ExecuteNonQuery();
            }
        }
        // Añadir método para actualizar variante por nombre
        public static void ActualizarVarianteCPorNombre(string nombreActual, string nuevoNombre)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("UPDATE variantes SET nombre_variante = @nuevoNombre WHERE nombre_variante = @nombreActual", conn);
                cmd.Parameters.AddWithValue("@nuevoNombre", nuevoNombre);
                cmd.Parameters.AddWithValue("@nombreActual", nombreActual);
                cmd.ExecuteNonQuery();
            }
        }

        // Añadir método para obtener ID de variante por nombre
        public static int ObtenerIdPorNombre(string nombreVarianteC)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT id_variante FROM variantes WHERE nombre_variante = @nombre", conn);
                cmd.Parameters.AddWithValue("@nombre", nombreVarianteC);

                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1; // -1 si no se encontró
            }
        }


    }

}

