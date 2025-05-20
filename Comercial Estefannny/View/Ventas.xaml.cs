using Comercial_Estefannny.ViewModel;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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






namespace Comercial_Estefannny.View
{
    public partial class VentasView : UserControl
    {
        // Mantener las listas de strings
        public List<string> Marcas { get; set; }
        public List<string> Categorias { get; set; }
        public List<string> Variantes { get; set; }
        public ObservableCollection<Producto> Productos1 { get; set; }
        public ObservableCollection<ClientesC> clientesCache { get; set; }
        public ObservableCollection<ProductoVenta> ProductosDeVenta { get; set; }
        private ProductoVenta productoSeleccionado;
        // Variables de los ComboBox seleccionados
        private Marca selectedMarca;
        private Categoria selectedCategoria;
        private Producto selectedProducto;
        private VarianteC selectedVariante;
        public decimal TotalAPagar
        {
            get
            {
                return ProductosDeVenta?.Sum(p => p.Total) ?? 0;
            }
        }

        
        public VentasView()
        {
            InitializeComponent();

            // Asignar el DataContext a sí mismo para que los ComboBox puedan enlazar las propiedades directamente.
            DataContext = this; // Setting ViewModel to DataContext


            // Cargar datos de la base de datos
            Marcas = Marca.ObtenerMarcas(); // Lista de marcas (string)
            Categorias = Categoria.ObtenerCategorias(); // Lista de categorías (string)
            Variantes = VarianteC.ObtenerVarianteCs(); // Lista de variantes (string)
            Productos1 = new ObservableCollection<Producto>(Producto.ObtenerProductos()); // Lista de productos
            clientesCache = new ObservableCollection<ClientesC>(ClientesC.ObtenerClientes()); // Lista de clientes
                                                                                              // Inicializar la colección de productos
            ProductosDeVenta = new ObservableCollection<ProductoVenta>();
            // Asignar la colección a la propiedad ItemsSource de la ListView
           
        }
        private void OnProductosDeVentaChanged()
        {
            TextTotalApagar.Text = TotalAPagar.ToString("C");
        }

        private void ComboProducto_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            ComboProducto.IsDropDownOpen = true; // Mantiene el ComboBox abierto mientras escribes
        }


        // Función que se ejecuta cuando se selecciona un producto
        private void ComboProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboProducto.SelectedItem is Producto productoSeleccionado)
            {
                // Actualizar ComboMarca, ComboCategoria, y ComboVariante con los datos del producto seleccionado.
                ComboMarca.SelectedItem = Marcas.FirstOrDefault(m => m.Equals(productoSeleccionado.NombreMarca));
                ComboCategoria.SelectedItem = Categorias.FirstOrDefault(c => c.Equals(productoSeleccionado.NombreCategoria));
                ComboVariante.SelectedItem = Variantes.FirstOrDefault(v => v.Equals(productoSeleccionado.Variante));

                // Asignar el precio de venta al DecimalUpDown
                DecimalPrecio.Value = productoSeleccionado.PrecioVenta;

                // Mensaje en consola que muestra que se seleccionó un producto
                Console.WriteLine($"Producto seleccionado: {productoSeleccionado.NombreProducto}, Precio de venta: {productoSeleccionado.PrecioVenta}");
            }
        }

        private void ProductosListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductosListView.SelectedItem is ProductoVenta productoSeleccionado)
            {
                // Llenar los campos con los valores actuales del producto seleccionado
                ComboProducto.SelectedItem = Productos1.FirstOrDefault(p => p.IdProducto == productoSeleccionado.IdProducto);
                ComboVariante.SelectedItem = Variantes.FirstOrDefault(v => v.Equals(productoSeleccionado.Variante));
                DecimalCantidad.Value = productoSeleccionado.Cantidad;
                DecimalDescuento.Value = productoSeleccionado.Descuento;
                DecimalPrecio.Value = productoSeleccionado.Precio;

                // Asignar el producto seleccionado a la variable 'productoSeleccionado' para su edición
                this.productoSeleccionado = productoSeleccionado;
            }
        }


        // Otros métodos de los botones (puedes agregar tu lógica aquí)
        private void BotonAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (ComboProducto.SelectedItem is Producto selectedProducto && DecimalPrecio.Value.HasValue)
            {
                decimal descuento = DecimalDescuento?.Value ?? 0;
                decimal cantidad = DecimalCantidad?.Value ?? 1;

                string variante = ComboVariante.SelectedItem?.ToString();
                string categoria = ComboCategoria.SelectedItem?.ToString();
                string marca = ComboMarca.SelectedItem?.ToString();

                var nuevoProducto = new ProductoVenta(
                    selectedProducto.IdProducto,
                    selectedProducto.NombreProducto,
                    (int)cantidad,
                    DecimalPrecio.Value.Value,
                    descuento
                )
                {
                    Variante = variante
                };

                ProductosDeVenta.Add(nuevoProducto);
                OnProductosDeVentaChanged();
            }
            else
            {
                MessageBox.Show("Seleccione un producto e ingrese el precio para agregar.");
            }
        }



        private void BotonEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si hay un producto seleccionado
            if (ProductosListView.SelectedItem is ProductoVenta productoSeleccionado)
            {
                // Eliminar el producto seleccionado de la lista
                ProductosDeVenta.Remove(productoSeleccionado);
                OnProductosDeVentaChanged();
            }
            else
            {
                MessageBox.Show("Seleccione un producto de la lista para eliminar.");
            }
        }


        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            if (productoSeleccionado != null)
            {
                // Actualizar las propiedades del producto seleccionado
                productoSeleccionado.Cantidad = (int)DecimalCantidad.Value;
                productoSeleccionado.Descuento = DecimalDescuento.Value ?? 0;
                productoSeleccionado.Precio = DecimalPrecio.Value.Value;
                productoSeleccionado.Variante = ComboVariante.SelectedItem?.ToString();

                // Forzar la actualización del ListView
                ProductosListView.ItemsSource = null;
                ProductosListView.ItemsSource = ProductosDeVenta;
                OnProductosDeVentaChanged();
                MessageBox.Show("Producto actualizado correctamente.");
            }
            else
            {
                MessageBox.Show("Seleccione un producto de la lista para editar.");
            }
        }





        private void BotonRegistrar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si hay productos para registrar
            if (ProductosDeVenta.Count == 0)
            {
                MessageBox.Show("No hay productos en la venta.");
                return;
            }

            // Obtener los datos necesarios para la venta
            DateTime fechaVenta = FechaVenta.SelectedDate ?? DateTime.Now;
            string nombreCliente = (ComboCliente.SelectedItem as ClientesC)?.Nombre;
            string tipoPago = ((ComboTipoDePago.SelectedItem as ComboBoxItem)?.Content as TextBlock)?.Text;


            // Verificar si el cliente y el tipo de pago están seleccionados
            if (string.IsNullOrEmpty(nombreCliente))
            {
                // Si no hay cliente seleccionado, asignar "Cliente Genérico" por defecto
                nombreCliente = "Cliente Genérico";

                // También puedes asegurarte de que el ComboBox tenga "Cliente Genérico" seleccionado
                ComboCliente.SelectedItem = clientesCache.FirstOrDefault(c => c.Nombre == nombreCliente);
            }

            if (string.IsNullOrEmpty(tipoPago))
            {
                MessageBox.Show("Por favor, seleccione un tipo de pago.");
                return;
            }

            // Verificar que el cliente y el tipo de pago están correctamente seleccionados
            if (string.IsNullOrEmpty(nombreCliente) || string.IsNullOrEmpty(tipoPago))
            {
                MessageBox.Show("Por favor, seleccione un cliente y un tipo de pago.");
                return;
            }

            // Llamar al método RegistrarVenta pasando los productos de la venta
            Ventas ventas = new Ventas(); // Asegúrate de tener acceso a la clase Ventas
            ventas.RegistrarVenta(fechaVenta, nombreCliente, tipoPago, ProductosDeVenta);

            // Limpiar la lista de productos de venta después de registrar
            ProductosDeVenta.Clear();
            OnProductosDeVentaChanged();

            MessageBox.Show("Venta registrada correctamente.");
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void ComboProducto_GotFocus(object sender, RoutedEventArgs e)
        {
            // Abre el dropdown automáticamente cuando el ComboBox recibe el foco
            ComboProducto.IsDropDownOpen = true;
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


    }
}