using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
// using System.Windows.Controls; // Eliminado para evitar ambigüedad
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Comercial_Estefannny.ViewModel;
using ViewModelProveedores = Comercial_Estefannny.ViewModel.Proveedores; // Alias for ViewModel.Proveedores

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Comercial_Estefannny.View
{
    public partial class Compras : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {
        // Mantener las listas de strings
        public List<string> Marcas { get; set; }
        public List<string> Categorias { get; set; }
        public List<string> Variantes { get; set; }
        public ObservableCollection<Producto> Productos1 { get; set; }
        public ObservableCollection<ViewModelProveedores> ProveedoresCache { get; set; } // Use alias
        public ObservableCollection<ProductoCompra> ProductosDeCompra { get; set; }
        private ProductoCompra productoSeleccionadoCompra;

        private decimal _cantidad;
        public decimal Cantidad
        {
            get => _cantidad;
            set { _cantidad = value; OnPropertyChanged(nameof(Cantidad)); }
        }

        private decimal _precioVenta;
        public decimal PrecioVenta
        {
            get => _precioVenta;
            set { _precioVenta = value; OnPropertyChanged(nameof(PrecioVenta)); }
        }

        private decimal _descuento;
        public decimal Descuento
        {
            get => _descuento;
            set { _descuento = value; OnPropertyChanged(nameof(Descuento)); }
        }

        private DateTime? _fecha;
        public DateTime? Fecha
        {
            get => _fecha;
            set { _fecha = value; OnPropertyChanged(nameof(Fecha)); }
        }

        private string _proveedor;
        public string Proveedor
        {
            get => _proveedor;
            set { _proveedor = value; OnPropertyChanged(nameof(Proveedor)); }
        }

        private ProductoCompra _productoSeleccionado;
        public ProductoCompra productoSeleccionado
        {
            get => _productoSeleccionado;
            set { _productoSeleccionado = value; OnPropertyChanged(nameof(productoSeleccionado)); }
        }

        public decimal TotalAPagar
        {
            get { return ProductosDeCompra?.Sum(p => p.Total) ?? 0; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public decimal TotalCompra
        {
            get
            {
                return ProductosDeCompra?.Sum(p => p.Total) ?? 0;
            }
        }

        public Compras()
        {
            InitializeComponent();

            DataContext = this; // Setting ViewModel to DataContext

            // Cargar datos de la base de datos
            Marcas = Marca.ObtenerMarcas();
            Categorias = Categoria.ObtenerCategorias();
            Variantes = VarianteC.ObtenerVarianteCs();
            Productos1 = new ObservableCollection<Producto>(Producto.ObtenerProductos());
            // Corrected to use ViewModel.Proveedores.Obtenerproveedores() which returns ObservableCollection<ViewModel.Proveedores>
            ProveedoresCache = ViewModel.Proveedores.Obtenerproveedores(); 
            ProductosDeCompra = new ObservableCollection<ProductoCompra>();
        }

        private void OnProductosDeCompraChanged()
        {
            TextTotalCompra.Text = TotalCompra.ToString("C");
        }

        private void ComboProducto_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ComboProducto.SelectedItem is Producto productoSeleccionado)
            {
                ComboMarca.SelectedItem = Marcas.FirstOrDefault(m => m.Equals(productoSeleccionado.NombreMarca));
                ComboCategoria.SelectedItem = Categorias.FirstOrDefault(c => c.Equals(productoSeleccionado.NombreCategoria));
                ComboVariante.SelectedItem = Variantes.FirstOrDefault(v => v.Equals(productoSeleccionado.Variante));

                DecimalPrecio.Value = productoSeleccionado.PrecioCompra;

                Console.WriteLine($"Producto seleccionado: {productoSeleccionado.NombreProducto}, Precio de compra: {productoSeleccionado.PrecioCompra}");
            }
        }

        private void ProductosListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ProductosListView.SelectedItem is ProductoCompra productoSeleccionado)
            {
                ComboProducto.SelectedItem = Productos1.FirstOrDefault(p => p.IdProducto == productoSeleccionado.IdProducto);
                ComboVariante.SelectedItem = Variantes.FirstOrDefault(v => v.Equals(productoSeleccionado.Variante));
                DecimalCantidad.Value = productoSeleccionado.Cantidad;
                DecimalPrecio.Value = productoSeleccionado.Precio;

                this.productoSeleccionadoCompra = productoSeleccionado;
            }
        }

        private void BotonAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (ComboProducto.SelectedItem is Producto selectedProducto && DecimalPrecio.Value.HasValue)
            {

                decimal descuento = DecimalDescuento?.Value ?? 0;

                decimal cantidad = DecimalCantidad?.Value ?? 1;
                string variante = ComboVariante.SelectedItem?.ToString();
                string categoria = ComboCategoria.SelectedItem?.ToString();
                string marca = ComboMarca.SelectedItem?.ToString();

                var nuevoProductoCompra = new ProductoCompra(
                    selectedProducto.IdProducto,
                    selectedProducto.NombreProducto,
                    (int)cantidad,
                    DecimalPrecio.Value.Value,
                     descuento
                )
                {
                    Variante = variante
                };

                ProductosDeCompra.Add(nuevoProductoCompra);
                OnProductosDeCompraChanged();
            }
            else
            {
                System.Windows.MessageBox.Show("Seleccione un producto e ingrese el precio para agregar.");
            }
        }

        private void BotonEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (ProductosListView.SelectedItem is ProductoCompra productoSeleccionado)
            {
                ProductosDeCompra.Remove(productoSeleccionado);
                OnProductosDeCompraChanged();
            }
            else
            {
                System.Windows.MessageBox.Show("Seleccione un producto de la lista para eliminar.");
            }
        }

        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            if (productoSeleccionadoCompra != null)
            {
                productoSeleccionadoCompra.Cantidad = (int)DecimalCantidad.Value;
                productoSeleccionadoCompra.Precio = DecimalPrecio.Value.Value;
                productoSeleccionadoCompra.Variante = ComboVariante.SelectedItem?.ToString();

                ProductosListView.ItemsSource = null;
                ProductosListView.ItemsSource = ProductosDeCompra;
                OnProductosDeCompraChanged();
                System.Windows.MessageBox.Show("Producto actualizado correctamente.");
            }
            else
            {
                System.Windows.MessageBox.Show("Seleccione un producto de la lista para editar.");
            }
        }

        private void BotonRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (ProductosDeCompra.Count == 0)
            {
                System.Windows.MessageBox.Show("No hay productos en la compra.");
                return;
            }

            DateTime fechaCompra = FechaCompra.SelectedDate ?? DateTime.Now;
            // Cast to ViewModel.Proveedores using the alias
            string nombreProveedor = (ComboProveedor.SelectedItem as ViewModelProveedores)?.Nombre; 

            if (string.IsNullOrEmpty(nombreProveedor))
            {
                System.Windows.MessageBox.Show("Por favor, seleccione un proveedor.");
                return;
            }

            Compra compras = new Compra(); // Asegúrate de tener acceso a la clase ComprasViewModel
            compras.RegistrarCompra(fechaCompra, nombreProveedor, ProductosDeCompra);

            ProductosDeCompra.Clear();
            OnProductosDeCompraChanged();

            System.Windows.MessageBox.Show("Compra registrada correctamente.");
        }
        private void ComboProducto_FiltrarBusqueda(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string filtro = ComboProducto.Text.ToLower();

            if (string.IsNullOrWhiteSpace(filtro))
            {
                ComboProducto.ItemsSource = Productos1; // Mostrar todos los productos si el campo está vacío
            }
            else
            {
                ComboProducto.ItemsSource = Productos1
                    .Where(p => p.NombreProducto.ToLower().Contains(filtro)) // Buscar en cualquier parte del nombre
                    .ToList();
            }

            ComboProducto.IsDropDownOpen = true; // Mantener el ComboBox abierto mientras filtra
        }
        private void ComboProducto_GotFocus(object sender, RoutedEventArgs e)
        {
            // Abre el dropdown automáticamente cuando el ComboBox recibe el foco
            ComboProducto.IsDropDownOpen = true;
        }
    }
}
