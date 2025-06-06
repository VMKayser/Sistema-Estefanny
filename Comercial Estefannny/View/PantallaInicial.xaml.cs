using System;
using System.Windows;
using System.Windows.Controls;
using Comercial_Estefannny.Services;

namespace Comercial_Estefannny.View
{
    /// <summary>
    /// Lógica de interacción para PantallaInicial.xaml - Versión Espectacular
    /// </summary>
    public partial class PantallaInicial : UserControl
    {
        private readonly SpectacularGradientManager _gradientManager;

        public PantallaInicial()
        {
            try
            {
                // Inicializar el gestor de gradientes espectaculares
                _gradientManager = new SpectacularGradientManager();
                
                // Intentar InitializeComponent 
                InitializeComponent();
                
                // Aplicar gradiente inicial
                ApplySpectacularTheme();
            }
            catch (Exception ex)
            {
                // Log de error básico
                try
                {
                    System.IO.File.WriteAllText(@"d:\pantalla_error_log.txt", 
                        $"{DateTime.Now}: {ex.Message}\n{ex.StackTrace}");
                }
                catch { }
                
                MessageBox.Show($"Error al inicializar PantallaInicial: {ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }        /// <summary>
        /// Aplica el tema espectacular inicial
        /// </summary>
        private void ApplySpectacularTheme()
        {
            try
            {
                // Aplicar gradiente configurado desde el gestor
                var currentGradient = _gradientManager.CurrentGradient;
                if (!string.IsNullOrEmpty(currentGradient))
                {
                    // Aplicar efectos adicionales si es necesario
                    System.Diagnostics.Debug.WriteLine($"Aplicando gradiente: {currentGradient}");
                }
            }
            catch (Exception ex)
            {
                // Error silencioso para no interrumpir la carga
                System.Diagnostics.Debug.WriteLine($"Error aplicando tema: {ex.Message}");
            }
        }

        /// <summary>
        /// Manejador para el botón de configuración
        /// </summary>
        private void ConfigButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Mostrar mensaje de configuración (puede expandirse)
                MessageBox.Show("🔧 Configuración del sistema\n\n" +
                               "Aquí podrás personalizar:\n" +
                               "• Configuraciones generales\n" +
                               "• Preferencias de usuario\n" +
                               "• Configuración de base de datos\n" +
                               "• Opciones de respaldo", 
                               "⚙️ Configuración", 
                               MessageBoxButton.OK, 
                               MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir configuración: {ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Manejador para el botón de temas
        /// </summary>
        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Abrir ventana de configuración de temas
                var themeWindow = new ThemeSettingsWindow();
                var parentWindow = Window.GetWindow(this);
                if (parentWindow != null)
                {
                    themeWindow.Owner = parentWindow;
                }
                themeWindow.ShowDialog();
                
                // Recargar tema después de cerrar la ventana
                ApplySpectacularTheme();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir configuración de temas: {ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Método legacy para compatibilidad
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConfigButton_Click(sender, e);
        }
    }
}
