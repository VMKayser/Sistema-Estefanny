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

using System.Collections.ObjectModel;

namespace Comercial_Estefannny.View
{
    public partial class Compras : UserControl
    {
        // Mantener las listas de strings
        public List<string> Marcas { get; set; }
        public List<string> Categorias { get; set; }
        public List<string> Variantes { get; set; }
        public ObservableCollection<Producto> Productos1 { get; set; }
        public ObservableCollection<proveedores> ProveedoresCache { get; set; }
        public ObservableCollection<ProductoCompra> ProductosDeCompra { get; set; }
        private ProductoCompra productoSeleccionadoCompra;
        private Marca selectedMarca;
        private Categoria selectedCategoria;
        private Producto selectedProducto;
        private VarianteC selectedVariante;

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
            ProveedoresCache = new ObservableCollection<proveedores>(proveedores.Obtenerproveedores());
            ProductosDeCompra = new ObservableCollection<ProductoCompra>();
        }

        private void OnProductosDeCompraChanged()
        {
            TextTotalCompra.Text = TotalCompra.ToString("C");
        }

        private void ComboProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void ProductosListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                MessageBox.Show("Seleccione un producto e ingrese el precio para agregar.");
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
                MessageBox.Show("Seleccione un producto de la lista para eliminar.");
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
                MessageBox.Show("Producto actualizado correctamente.");
            }
            else
            {
                MessageBox.Show("Seleccione un producto de la lista para editar.");
            }
        }

        private void BotonRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (ProductosDeCompra.Count == 0)
            {
                MessageBox.Show("No hay productos en la compra.");
                return;
            }

            DateTime fechaCompra = FechaCompra.SelectedDate ?? DateTime.Now;
            string nombreProveedor = (ComboProveedor.SelectedItem as proveedores)?.Nombre;

            if (string.IsNullOrEmpty(nombreProveedor))
            {
                MessageBox.Show("Por favor, seleccione un proveedor.");
                return;
            }

            Compra compras = new Compra(); // Asegúrate de tener acceso a la clase ComprasViewModel
            compras.RegistrarCompra(fechaCompra, nombreProveedor, ProductosDeCompra);

            ProductosDeCompra.Clear();
            OnProductosDeCompraChanged();

            MessageBox.Show("Compra registrada correctamente.");
        }
        private void ComboProducto_FiltrarBusqueda(object sender, KeyEventArgs e)
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
