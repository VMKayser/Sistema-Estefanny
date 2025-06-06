using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FontAwesome.Sharp;
using Comercial_Estefannny.ViewModel;

namespace Comercial_Estefannny
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // Configurar el DataContext con el ViewModel principal
            DataContext = new MainViewModel();
            
            // Configuraciones de la ventana
            this.WindowState = WindowState.Normal;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
            // Inicializar estado de la ventana después de que se cargue
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Configurar el nombre del usuario
            var userNameText = this.FindName("UserNameText") as TextBlock;
            if (userNameText != null)
            {
                userNameText.Text = "Administrador";
            }
        }

        #region Event Handlers

        /// <summary>
        /// Permite arrastrar la ventana desde la barra superior
        /// </summary>
        private void TopBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                if (e.ClickCount == 2)
                {
                    // Doble clic: alternar entre maximizado y normal
                    ToggleMaximizeRestore();
                }
                else
                {
                    // Un clic: permitir arrastrar la ventana
                    this.DragMove();
                }
            }
        }

        /// <summary>
        /// Alterna entre estado maximizado y normal
        /// </summary>
        private void ToggleMaximizeRestore()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        #endregion

        #region Menu Item Handlers

        private void MenuItem_MiPerfil_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Función Mi Perfil - En desarrollo", "Mi Perfil", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_Configuracion_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Función Configuración - En desarrollo", "Configuración", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_Temas_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var themeWindow = new Comercial_Estefannny.View.ThemeSettingsWindow();
                themeWindow.Owner = this;
                themeWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir configuración de temas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem_CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("¿Está seguro que desea cerrar sesión?", "Cerrar Sesión", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        #endregion
    }
}