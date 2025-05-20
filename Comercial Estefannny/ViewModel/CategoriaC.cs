using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Comercial_Estefannny.ViewModel
{
    public class Categoria : ViewModelbase
    {
        // Propiedades de la clase Categoria
        private int _idCategoria;
        public int IdCategoria
        {
            get => _idCategoria;
            set
            {
                _idCategoria = value;
                OnPropertyChanged(nameof(IdCategoria));
            }
        }

        private string _nombreCategoria;
        public string NombreCategoria
        {
            get => _nombreCategoria;
            set
            {
                _nombreCategoria = value;
                OnPropertyChanged(nameof(NombreCategoria));
            }
        }

        // Constructor
        public Categoria() { }

        public Categoria(int idCategoria, string nombreCategoria)
        {
            IdCategoria = idCategoria;
            NombreCategoria = nombreCategoria;
        }

        // Método para obtener la conexión a la base de datos
        private static SQLiteConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString;
            return new SQLiteConnection(connectionString);
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

        // Obtener una categoría por su ID
        public static Categoria ObtenerCategoriaPorId(int idCategoria)
        {
            using (var conn = new SQLiteConnection(@"Data Source=.\comercial Estefanny base datos.db;Version=3;"))
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT id_categoria, nombre_categoria FROM categoria WHERE id_categoria = @id", conn);
                cmd.Parameters.AddWithValue("@id", idCategoria);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Categoria(reader.GetInt32(0), reader.GetString(1));
                }

                return null; // No se encontró la categoría
            }
        }

        // Actualizar una categoría por ID
        // Añadir método para actualizar categoría por nombre
        public static void ActualizarCategoriaPorNombre(string nombreActual, string nuevoNombre)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("UPDATE categoria SET nombre_categoria = @nuevoNombre WHERE nombre_categoria = @nombreActual", conn);
                cmd.Parameters.AddWithValue("@nuevoNombre", nuevoNombre);
                cmd.Parameters.AddWithValue("@nombreActual", nombreActual);
                cmd.ExecuteNonQuery();
            }
        }

        // Añadir método para obtener ID de categoría por nombre
        public static int ObtenerIdPorNombre(string nombreCategoria)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT id_categoria FROM categoria WHERE nombre_categoria = @nombre", conn);
                cmd.Parameters.AddWithValue("@nombre", nombreCategoria);

                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1; // -1 si no se encontró
            }
        }

        // Añadir método para verificar existencia de una categoría por nombre
        public static bool ExisteCategoria(string nombreCategoria)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT COUNT(*) FROM categoria WHERE nombre_categoria = @nombre", conn);
                cmd.Parameters.AddWithValue("@nombre", nombreCategoria);

                long count = (long)cmd.ExecuteScalar();
                return count > 0;
            }
        }


        // ------------------------- Métodos para obtener todas las categorías -------------------------

        private static List<string> categoriasCache = new List<string>();

        // Método para obtener todas las categorías desde la base de datos
        public static List<string> ObtenerCategorias()
        {
            // Si la lista está vacía, la cargamos desde la base de datos
            if (!categoriasCache.Any())
            {
                CargarCategoriasDesdeDB();
            }

            return categoriasCache;
        }

        // Método para cargar las categorías desde la base de datos
        private static void CargarCategoriasDesdeDB()
        {
            categoriasCache.Clear();  // Limpiar la lista de categorías en memoria

            using (var conn =GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT nombre_categoria FROM categoria", conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    categoriasCache.Add(reader.GetString(0));
                }
            }
        }
        //metodo obtener categoria por nombre
       
        // Método para eliminar una categoría por ID
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
        public static void EliminarCategoriaPorNombre(string nombreCategoria)
        {
            using (var conn = new SQLiteConnection(@"Data Source=.\comercial Estefanny base datos.db;Version=3;"))
            {
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM categoria WHERE nombre_categoria = @nombre", conn);
                cmd.Parameters.AddWithValue("@nombre", nombreCategoria);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
