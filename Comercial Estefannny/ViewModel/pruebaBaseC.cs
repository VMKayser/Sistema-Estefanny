using Comercial_Estefannny.ViewModel;
using System;
using System.Configuration;
using System.Data.SQLite;
using System.Windows;
namespace Comercial_Estefannny.ViewModel
{

    public class PruebaBaseC : ViewModelbase
    {
        private SQLiteConnection connection;

        public PruebaBaseC()
        {
            // Constructor
            // Llama al método de conectar
           
        }

        private SQLiteConnection GetConnection()
        {
            // Obtener la cadena de conexión desde el archivo App.config
            string connectionString = ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString;
            return new SQLiteConnection(connectionString);
        }

        // Método para conectar a la base de datos
        public void Conectar()
        {
            try
            {
                connection = GetConnection();
                connection.Open();
                MessageBox.Show("Conectado a la base de datos");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
            }
        }
    }
}