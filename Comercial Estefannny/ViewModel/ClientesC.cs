using Comercial_Estefannny.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SQLite;
using System.Collections.ObjectModel;
using Comercial_Estefanny.Services;    

namespace Comercial_Estefannny.ViewModel
{
    public class ClientesC : ViewModelbase
    {
        // Atributos
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public double Deuda { get; set; }
        private ExcelImportService _excelImportService = new ExcelImportService();
        // Caché de clientes
        public static ObservableCollection<ClientesC> clientesCache = new ObservableCollection<ClientesC>();
        public ClientesC()
        {
        }

        // Método para obtener la conexión a la base de datos
        private static SQLiteConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString;
            return new SQLiteConnection(connectionString);
        }

        // Método para obtener los clientes, usando caché si ya existe
        public static ObservableCollection<ClientesC> ObtenerClientes()
        {
            // Si el caché está vacío, cargar los datos de la base de datos
            if (!clientesCache.Any())
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    var cmd = new SQLiteCommand("SELECT * FROM cliente", conn);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var cliente = new ClientesC
                        {
                            Nombre = reader["nombre_cliente"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            Deuda = Convert.ToDouble(reader["deuda"])
                        };

                        // Añadir cliente al caché
                        clientesCache.Add(cliente);
                    }
                }
            }

            // Regresar los clientes desde el caché en una ObservableCollection
            return new ObservableCollection<ClientesC>(clientesCache);
        }

        // Método para agregar un cliente
        public void InsertarCliente()
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                // Verificar si el cliente ya existe (puedes verificarlo por nombre o algún otro campo único)
                var checkCmd = new SQLiteCommand("SELECT COUNT(*) FROM cliente WHERE nombre_cliente = @nombre", conn);
                checkCmd.Parameters.AddWithValue("@nombre", Nombre);
                var count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    Console.WriteLine("El cliente con ese nombre ya existe.");
                    return; // Salir del método sin realizar la inserción
                }

                // Insertar el nuevo cliente
                var cmd = new SQLiteCommand("INSERT INTO cliente (nombre_cliente, direccion, telefono, deuda) VALUES (@nombre, @direccion, @telefono, @deuda)", conn);
                cmd.Parameters.AddWithValue("@nombre", Nombre);
                cmd.Parameters.AddWithValue("@direccion", Direccion);
                cmd.Parameters.AddWithValue("@telefono", Telefono);
                cmd.Parameters.AddWithValue("@deuda", Deuda);
                cmd.ExecuteNonQuery();

                // Agregar el cliente al caché tras inserción exitosa
                clientesCache.Add(this);
            }
        }

        public void EliminarCliente()
        {
            using (var conn = new SQLiteConnection(@"Data Source=.\comercial Estefanny base datos.db;Version=3;"))
            {
                conn.Open();

                // Ejecutar la eliminación en la base de datos
                var cmd = new SQLiteCommand("DELETE FROM cliente WHERE nombre_cliente = @nombreCliente", conn);
                cmd.Parameters.AddWithValue("@nombreCliente", Nombre);
                cmd.ExecuteNonQuery();
            }

            // Eliminar el cliente de la caché
            clientesCache.Remove(this);
        }

        public void ActualizarCliente()
        {
            using (var conn = new SQLiteConnection(@"Data Source=.\comercial Estefanny base datos.db;Version=3;"))
            {
                conn.Open();

                // Comando SQL para actualizar todos los campos del cliente
                var cmd = new SQLiteCommand("UPDATE cliente SET " +
                                             "direccion = @direccion, " +
                                             "telefono = @telefono, " +
                                             "deuda = @deuda " +
                                             "WHERE nombre_cliente = @nombreCliente", conn);

                // Asignar valores a los parámetros
                cmd.Parameters.AddWithValue("@nombreCliente", Nombre);
                cmd.Parameters.AddWithValue("@direccion", Direccion);
                cmd.Parameters.AddWithValue("@telefono", Telefono);
                cmd.Parameters.AddWithValue("@deuda", Deuda);

                // Ejecutar el comando SQL para actualizar el cliente
                cmd.ExecuteNonQuery();
            }

            // Actualizar el cliente en el caché
            var cliente = clientesCache.FirstOrDefault(c => c.Nombre == Nombre);
            if (cliente != null)
            {
                cliente.Direccion = Direccion;
                cliente.Telefono = Telefono;
                cliente.Deuda = Deuda;
            }
        }
        public void ImportarClientesDesdeExcel(string filePath)
        {
            var clientes = _excelImportService.ImportarClientesDesdeExcel(filePath);

            // Agregar los clientes importados al caché y a la base de datos
            foreach (var cliente in clientes)
            {
                cliente.InsertarCliente(); // Asumimos que InsertarCliente añade a la base de datos y caché
            }

           
        }
        // Función para obtener el ID del cliente por nombre
        public static int ObtenerIdClientePorNombre(string nombreCliente)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                // Comando SQL para obtener el id_cliente por nombre
                var cmd = new SQLiteCommand("SELECT id_cliente FROM cliente WHERE nombre_cliente = @nombreCliente", conn);
                cmd.Parameters.AddWithValue("@nombreCliente", nombreCliente);

                var result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return -1; // Retorna -1 si no se encuentra el cliente
                }
            }
        }


    }
}
