using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Comercial_Estefannny.ViewModel;

namespace Comercial_Estefannny.View
{
    /// <summary>
    /// Lógica de interacción para PantallaInicio.xaml
    /// </summary>
    public partial class PantallaInicio : UserControl
    {
        public PantallaInicio()
        {
            InitializeComponent();
            DataContext = new PantallaInicioC();
            // Establecer el directorio de datos
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);

            MostrarProductosBajoStock();


        }
        public class ProductoBajoStock
        {
            public string NombreProducto { get; set; }
            public int Cantidad { get; set; }
        }
        private void MostrarProductosBajoStock()
        {
            var BajoStock = new PantallaInicioC();
            var productosBajoStock = BajoStock.ObtenerProductosBajoStock();
            var productosList = new List<ProductoBajoStock>();

            foreach (var producto in productosBajoStock)
            {
                productosList.Add(new ProductoBajoStock
                {
                    NombreProducto = producto.Item1,
                    Cantidad = producto.Item2
                });
            }

            // Asignar la lista al ItemsSource del ListView
            Astock_ListView.ItemsSource = productosList;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
