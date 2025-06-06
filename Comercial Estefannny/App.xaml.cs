using System;
using System.Windows;

namespace Comercial_Estefannny
{
    /// <summary>
    /// Aplicación WPF principal del Sistema Comercial Estefannny
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar la aplicación: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }
    }
}