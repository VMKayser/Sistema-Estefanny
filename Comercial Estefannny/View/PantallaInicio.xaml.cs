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
    public partial class PantallaInicio : System.Windows.Controls.UserControl
    {
        public PantallaInicio()
        {
            InitializeComponent();
            var viewModel = new PantallaInicioC();
            DataContext = viewModel;
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
            // MostrarProductosBajoStock se debe llamar después de que el componente esté cargado
            this.Loaded += (s, e) => MostrarProductosBajoStock(viewModel);
        }

        public class ProductoBajoStock
        {
            public string NombreProducto { get; set; }
            public int Cantidad { get; set; }
        }

        private void MostrarProductosBajoStock(PantallaInicioC viewModel)
        {
            // Buscar el control por nombre si no está generado el campo automáticamente
            var listView = this.FindName("Astock_ListView") as System.Windows.Controls.ListView;
            if (listView == null || viewModel == null)
                return;
            var productosBajoStock = viewModel.ObtenerProductosBajoStock();
            var productosList = new List<ProductoBajoStock>();
            foreach (var producto in productosBajoStock)
            {
                productosList.Add(new ProductoBajoStock
                {
                    NombreProducto = producto.Item1,
                    Cantidad = producto.Item2
                });
            }
            listView.ItemsSource = productosList;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
