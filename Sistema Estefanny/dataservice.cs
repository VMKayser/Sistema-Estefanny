using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SQLite;

namespace Comercial_Estefannny.ViewModel
{
    public class DataService
    {
        private readonly string connectionString = @"Data Source=.\comercial Estefanny base datos.db;Version=3;";

        public void TestConnection()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
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

        // Otros métodos CRUD
    }
}
