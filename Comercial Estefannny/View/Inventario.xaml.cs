using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Lógica de interacción para Inventario.xaml
    /// </summary>
    public partial class Inventario : System.Windows.Controls.UserControl
    {
        public int Cantidad { get; set; } = 0; // Valor por defecto
        public string CodigoBarras { get; set; } = "Sin código"; // Valor por defecto
        decimal CapitalInvertido;
        private List<string> marcas;
        private List<string> categorias;
        private List<string> variantes;
        private ICollectionView productosView;
        public ObservableCollection<Producto> productos { get; set; } = new ObservableCollection<Producto>();
        public Inventario()
        {
            InitializeComponent();

            marcas = Marca.ObtenerMarcas();
            categorias = Categoria.ObtenerCategorias();
            variantes = VarianteC.ObtenerVarianteCs();

            MarcaComboBox.ItemsSource = marcas;
            CategoriaComboBox.ItemsSource = categorias;
            VarianteComboBox.ItemsSource = variantes;

            // Obtener los productos de la base de datos
            CargarProductos();

            // Asignar la colección de productos a un ICollectionView para habilitar el filtrado
            productosView = CollectionViewSource.GetDefaultView(productos);
            ProductosListView.ItemsSource = productosView;
            CapitalInvertido= ObtenerCapitalInvertido();
        }
        private void CargarProductos()
        {
            // Refrescar la caché de productos y actualizar la colección
            Producto.RefrescarCache();
            productos.Clear();
            foreach (var producto in Producto.productosCache)
            {
                productos.Add(producto);
            }
            // No es necesario reasignar ItemsSource ni limpiar manualmente
        }

        private void MarcaComboBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string textoBusqueda = MarcaComboBox.Text.ToLower();

            // Filtrar las marcas que contienen el texto ingresado
            var marcasFiltradas = marcas.Where(m => m.ToLower().Contains(textoBusqueda)).ToList();

            // Actualizar el ItemsSource con las marcas filtradas
            MarcaComboBox.ItemsSource = marcasFiltradas;



            // Mantener el desplegable abierto mientras se escribe
            MarcaComboBox.IsDropDownOpen = true;
        }
        private void MarcaComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            MarcaComboBox.IsDropDownOpen = true;
        }



        private void CategoriaComboBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string textoBusqueda = CategoriaComboBox.Text.ToLower();

            var categoriasFiltradas = categorias.Where(c => c.ToLower().Contains(textoBusqueda)).ToList();

            // Establecer los elementos filtrados solo si hay resultados
            if (categoriasFiltradas.Any())
            {
                CategoriaComboBox.ItemsSource = categoriasFiltradas;
            }
            else
            {
                CategoriaComboBox.ItemsSource = categorias; // Restablecer a la lista completa si no hay resultados
            }

            CategoriaComboBox.IsDropDownOpen = true;
        }
        private void CategoriaComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CategoriaComboBox.IsDropDownOpen = true;
        }
        private void VarianteComboBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string textoBusqueda = VarianteComboBox.Text.ToLower();

            var variantesFiltradas = variantes.Where(v => v.ToLower().Contains(textoBusqueda)).ToList();

            // Establecer los elementos filtrados solo si hay resultados
            if (variantesFiltradas.Any())
            {
                VarianteComboBox.ItemsSource = variantesFiltradas;
            }
            else
            {
                VarianteComboBox.ItemsSource = variantes; // Restablecer a la lista completa si no hay resultados
            }

            VarianteComboBox.IsDropDownOpen = true; // Mantener abierto el desplegable mientras se escribe
        }
        private void VarianteComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            VarianteComboBox.IsDropDownOpen = true;
        }
        private void ProductoComboBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string textoBusqueda = ProductoComboBox.Text.ToLower();

            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                // Filtrar productos por nombre
                var productosFiltrados = Producto.FiltrarProductos(textoBusqueda);

                // Actualizar el ItemsSource con los nombres de productos filtrados
                ProductoComboBox.ItemsSource = productosFiltrados.Select(p => p.NombreProducto).ToList();
            }
            else
            {
                // Si no hay texto, restablecer a la lista completa
                ProductoComboBox.ItemsSource = Producto.ObtenerProductos().Select(p => p.NombreProducto).ToList();
            }

            ProductoComboBox.IsDropDownOpen = true;
        }

        private void ProductoComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ProductoComboBox.IsDropDownOpen = true;
        }



        private void BotonAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            // Obtener valores de los ComboBox y los TextBox
            string nombreProducto = ProductoComboBox.Text;

            // Convertir los precios a decimal
            decimal precioCompra = NumericPrecioCompra.Value ?? 0m;
            decimal precioVenta = NumericPrecioVenta.Value ?? 0m;

            // Validar que el nombre del producto y los precios no estén vacíos
            if (string.IsNullOrWhiteSpace(nombreProducto))
            {
                System.Windows.MessageBox.Show("El nombre del producto es obligatorio.");
                return; // Salir del método si el nombre del producto está vacío
            }

            if (precioCompra <= 0 || precioVenta <= 0)
            {
                System.Windows.MessageBox.Show("Los precios deben ser mayores a cero.");
                return; // Salir del método si los precios no son válidos
            }

            // Crear una instancia del producto con valores predeterminados para Cantidad y Código de Barras
            Producto nuevoProducto = new Producto
            {
                NombreProducto = nombreProducto,
                Cantidad = 0,            // Valor predeterminado para cantidad
                CodigoBarras = "Sin código", // Valor predeterminado para código de barras
                PrecioCompra = precioCompra,
                PrecioVenta = precioVenta
            };

            // Insertar el nuevo producto en la base de datos
            nuevoProducto.InsertarProducto();

            // Actualizar la lista de productos en la interfaz de usuario
            CargarProductos();

            // Notificar al usuario y limpiar los campos
            System.Windows.MessageBox.Show("Producto agregado correctamente.");

            // Limpiar campos después de agregar el producto
            ProductoComboBox.Text = string.Empty;
            NumericPrecioCompra.Value = null;
            NumericPrecioVenta.Value = null;
        }




        
        private void ProductosListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verifica si se ha seleccionado un ítem
            if (ProductosListView.SelectedItem != null)
            {
                // Obtiene el producto seleccionado (asegúrate de que Producto sea el tipo correcto)
                Producto productoSeleccionado = (Producto)ProductosListView.SelectedItem;

                // Actualiza los ComboBox con los valores del producto
                MarcaComboBox.SelectedItem = productoSeleccionado.NombreMarca;
                CategoriaComboBox.SelectedItem = productoSeleccionado.NombreCategoria;
                VarianteComboBox.SelectedItem = productoSeleccionado.Variante;
                ProductoComboBox.Text = productoSeleccionado.NombreProducto; // Si el ComboBox tiene valores numéricos, puedes asignar la cantidad, precio de compra, etc.
                                                                             // Si el ComboBox tiene valores numéricos, puedes asignar la cantidad, precio de compra, etc.

                NumericPrecioCompra.Value = productoSeleccionado.PrecioCompra;
                NumericPrecioVenta.Value = productoSeleccionado.PrecioVenta;
                TextCodigoBarra1.Text = productoSeleccionado.CodigoBarras;
            }
        }

        private void BotonEliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MarcaComboBox.Text) || !string.IsNullOrEmpty(CategoriaComboBox.Text) || !string.IsNullOrEmpty(VarianteComboBox.Text))
            {
                // Eliminar producto según marca, categoría y variante seleccionados en ComboBox
                if (!string.IsNullOrEmpty(MarcaComboBox.Text) &&
                    !string.IsNullOrEmpty(CategoriaComboBox.Text) &&
                    !string.IsNullOrEmpty(VarianteComboBox.Text) &&
                    ProductosListView.SelectedItem != null)
                {
                    Producto productoSeleccionado = (Producto)ProductosListView.SelectedItem;

                    // Llamamos al método de eliminación del producto
                    productoSeleccionado.EliminarProducto();

                    System.Windows.MessageBox.Show("Producto eliminado correctamente.");
                    CargarProductos(); // Actualizar la lista completa
                }
                else
                {
                    // Eliminar según el ComboBox específico seleccionado
                    if (!string.IsNullOrEmpty(MarcaComboBox.Text))
                    {
                        string marcaSeleccionada = MarcaComboBox.Text;

                        if (!Producto.ExisteProductoConMarca(Marca.ObtenerIdPorNombre(marcaSeleccionada)))
                        {
                            Marca.EliminarMarcaPorNombre(marcaSeleccionada);
                            marcas.Remove(marcaSeleccionada);
                            MarcaComboBox.ItemsSource = null;
                            MarcaComboBox.ItemsSource = marcas;
                            System.Windows.MessageBox.Show("Marca eliminada correctamente.");
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("La marca está siendo utilizada en productos. No se puede eliminar.");
                        }
                    }

                    if (!string.IsNullOrEmpty(CategoriaComboBox.Text))
                    {
                        string categoriaSeleccionada = CategoriaComboBox.Text;

                        if (!Producto.ExisteProductoConCategoria(Categoria.ObtenerIdPorNombre(categoriaSeleccionada)))
                        {
                            Categoria.EliminarCategoriaPorNombre(categoriaSeleccionada);
                            categorias.Remove(categoriaSeleccionada);
                            CategoriaComboBox.ItemsSource = null;
                            CategoriaComboBox.ItemsSource = categorias;

                            System.Windows.MessageBox.Show("Categoría eliminada correctamente.");
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("La categoría está siendo utilizada en productos. No se puede eliminar.");
                        }
                    }

                    if (!string.IsNullOrEmpty(VarianteComboBox.Text))
                    {
                        string varianteSeleccionada = VarianteComboBox.Text;

                        if (!Producto.ExisteProductoConVariante(VarianteC.ObtenerIdPorNombre(varianteSeleccionada)))
                        {
                            VarianteC.EliminarVarianteCPorNombre(varianteSeleccionada);
                            variantes.Remove(varianteSeleccionada);
                            VarianteComboBox.ItemsSource = null;
                            VarianteComboBox.ItemsSource = variantes;

                            System.Windows.MessageBox.Show("VarianteC eliminada correctamente.");
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("La variante está siendo utilizada en productos. No se puede eliminar.");
                        }
                    }
                    CargarProductos(); // Actualizar la lista completa
                }
            }
            else
            {
                if (ProductosListView.SelectedItem != null)
                {
                    Producto productoSeleccionado = (Producto)ProductosListView.SelectedItem;

                    // Llamamos al método de eliminación del producto
                    productoSeleccionado.EliminarProducto();

                    System.Windows.MessageBox.Show("Producto eliminado correctamente.");
                    CargarProductos(); // Actualizar la lista completa
                }
                else
                {
                    System.Windows.MessageBox.Show("Por favor, selecciona un producto para eliminar.");
                }
            }
        }
        private void BotonEditarProducto_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si hay un producto seleccionado
            if (ProductosListView.SelectedItem != null)
            {
                // Obtener el producto seleccionado
                Producto productoSeleccionado = (Producto)ProductosListView.SelectedItem;

                // Obtener los nuevos valores de los ComboBox y campos de texto
                string nuevoNombreProducto = ProductoComboBox.Text;
                string nuevoNombreMarca = MarcaComboBox.Text;
                string nuevoNombreCategoria = CategoriaComboBox.Text;
                string nuevoNombreVarianteC = VarianteComboBox.Text;
                decimal nuevoPrecioCompra = NumericPrecioCompra.Value ?? 0m;
                decimal nuevoPrecioVenta = NumericPrecioVenta.Value ?? 0m;

                // Actualizar los campos del producto
                productoSeleccionado.NombreProducto = nuevoNombreProducto;
                productoSeleccionado.NombreMarca = nuevoNombreMarca;
                productoSeleccionado.NombreCategoria = nuevoNombreCategoria;
                productoSeleccionado.Variante = nuevoNombreVarianteC;
                productoSeleccionado.PrecioCompra = nuevoPrecioCompra;
                productoSeleccionado.PrecioVenta = nuevoPrecioVenta;

                // Obtener los IDs de marca, categoría y variante
                productoSeleccionado.IdMarca = Marca.ObtenerIdPorNombre(nuevoNombreMarca);
                productoSeleccionado.IdCategoria = Categoria.ObtenerIdPorNombre(nuevoNombreCategoria);

                // Llamar al método de actualización para reflejar los cambios en la base de datos
                productoSeleccionado.ActualizarProducto();

                // Refrescar la lista de productos para ver los cambios en la interfaz de usuario
                CargarProductos();

                // Notificar al usuario
                System.Windows.MessageBox.Show("Producto actualizado correctamente.");
            }
            else
            {
                System.Windows.MessageBox.Show("Por favor, selecciona un producto para editar.");
            }
        }
        private void TextBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (productosView != null)
            {
                // Aplicar el filtro en base al texto ingresado en el TextBox
                productosView.Filter = item =>
                {
                    var producto = item as Producto;
                    if (producto != null)
                    {
                        // Compara el nombre del producto con el texto del buscador, ignorando mayúsculas/minúsculas
                        return producto.NombreProducto.ToLower().Contains(TextBuscador.Text.ToLower());
                    }
                    return false;
                };
            }
        }

        private void BotonImportarDatos_Click(object sender, RoutedEventArgs e)
        {
            CargarProductos();
            // Crear un cuadro de diálogo de archivo para seleccionar el archivo Excel
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Archivos de Excel (*.xlsx)|*.xlsx",
                Title = "Seleccionar archivo Excel"
            };

            // Mostrar el cuadro de diálogo y obtener el resultado
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;  // Ruta del archivo seleccionado

                // Llamar al método ImportarClientesDesdeExcel del ViewModel
                try
                {
                    // Asumimos que tienes un método en tu ViewModel para importar desde Excel
                    Producto.ImportarProductosDesdeExcel(filePath);

                    System.Windows.MessageBox.Show("Productos importados exitosamente.");

                    // Actualizar la lista de clientes después de la importación
                    CargarProductos();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Error al importar los datos: {ex.Message}");
                }
            }
        }
        public decimal ObtenerCapitalInvertido()
        {
            decimal capitalInvertido = productos.Sum(p => p.Cantidad * p.PrecioCompra);
            textBlockCapitalInvertido.Text = $"{capitalInvertido:C}";
            return capitalInvertido;
        }


    }
}







