using Comercial_Estefannny.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SQLite;
using System.Collections.ObjectModel;
using Comercial_Estefannny.Services;

namespace Comercial_Estefannny.ViewModel
{    public class ClientesC : ViewModelbase
    {
        // Atributos
        public int Id { get; set; }  // Agregado para Entity Framework
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public double Deuda { get; set; }
        private static ExcelImportService _excelImportService = new ExcelImportService();

        // Métodos CRUD y utilidades
        public void InsertarCliente()
        {
            using (var conn = new System.Data.SQLite.SQLiteConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString))
            {
                conn.Open();
                var cmd = new System.Data.SQLite.SQLiteCommand("INSERT INTO cliente (nombre_cliente, direccion, telefono, deuda) VALUES (@nombre, @direccion, @telefono, @deuda)", conn);
                cmd.Parameters.AddWithValue("@nombre", Nombre);
                cmd.Parameters.AddWithValue("@direccion", Direccion);
                cmd.Parameters.AddWithValue("@telefono", Telefono);
                cmd.Parameters.AddWithValue("@deuda", Deuda);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarCliente()
        {
            using (var conn = new System.Data.SQLite.SQLiteConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString))
            {
                conn.Open();
                var cmd = new System.Data.SQLite.SQLiteCommand("DELETE FROM cliente WHERE nombre_cliente = @nombre", conn);
                cmd.Parameters.AddWithValue("@nombre", Nombre);
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarCliente()
        {
            using (var conn = new System.Data.SQLite.SQLiteConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString))
            {
                conn.Open();
                var cmd = new System.Data.SQLite.SQLiteCommand("UPDATE cliente SET direccion = @direccion, telefono = @telefono, deuda = @deuda WHERE nombre_cliente = @nombre", conn);
                cmd.Parameters.AddWithValue("@nombre", Nombre);
                cmd.Parameters.AddWithValue("@direccion", Direccion);
                cmd.Parameters.AddWithValue("@telefono", Telefono);
                cmd.Parameters.AddWithValue("@deuda", Deuda);
                cmd.ExecuteNonQuery();
            }
        }

        public static ObservableCollection<ClientesC> ObtenerClientes()
        {
            var clientes = new ObservableCollection<ClientesC>();
            using (var conn = new System.Data.SQLite.SQLiteConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString))
            {
                conn.Open();
                var cmd = new System.Data.SQLite.SQLiteCommand("SELECT nombre_cliente, direccion, telefono, deuda FROM cliente", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    clientes.Add(new ClientesC
                    {
                        Nombre = reader["nombre_cliente"].ToString(),
                        Direccion = reader["direccion"].ToString(),
                        Telefono = reader["telefono"].ToString(),
                        Deuda = Convert.ToDouble(reader["deuda"])
                    });
                }
            }
            return clientes;
        }

        public static void ImportarClientesDesdeExcel(string filePath)
        {
            var clientes = _excelImportService.ImportarClientesDesdeExcel(filePath);
            foreach (var cliente in clientes)
            {
                cliente.InsertarCliente();
            }
        }

        public static int ObtenerIdClientePorNombre(string nombreCliente)
        {
            using (var conn = new System.Data.SQLite.SQLiteConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString))
            {
                conn.Open();
                var cmd = new System.Data.SQLite.SQLiteCommand("SELECT id_cliente FROM cliente WHERE nombre_cliente = @nombre", conn);
                cmd.Parameters.AddWithValue("@nombre", nombreCliente);
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
