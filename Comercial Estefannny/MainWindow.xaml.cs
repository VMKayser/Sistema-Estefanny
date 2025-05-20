using System;
using System.Windows;

namespace Comercial_Estefannny
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); // No eliminar, necesario para WPF
            Console.WriteLine("Bienvenido a Comercial Estefanny");
        }
    }
}