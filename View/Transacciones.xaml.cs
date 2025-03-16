using Comercial_Estefannny.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SQLite;
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
    public partial class Transacciones : UserControl
    {
        private SQLiteConnection connection;

        public Transacciones()
        {
            InitializeComponent();
          // Asociamos los eventos de cambio de fecha correctamente
        dateTimePickerInicio.SelectedDateChanged += DateTimePicker_SelectedDateChanged;
            dateTimePickerFin.SelectedDateChanged += DateTimePicker_SelectedDateChanged;

            // Mostrar el capital invertido inicialmente con las fechas por defecto
            MostrarCapitalInvertido();
            MostrarGanancia();
            dateTimePickerInicio.SelectedDate = DateTime.Today;
            dateTimePickerFin.SelectedDate = DateTime.Today.AddDays(1);
            FiltrarDatos(null, null); // Cargar datos iniciales
            MostrarVentas(DateTime.Today, DateTime.Today.AddDays(1)); // Mostrar ventas iniciales
        }

        private void DateTimePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Cada vez que se cambie una de las fechas, se actualiza el capital invertido
            MostrarCapitalInvertido();
            MostrarGanancia();
            FiltrarDatos(null, null);

        }
        private void MostrarGanancia()
        {
            DateTime fechaInicio = dateTimePickerInicio.SelectedDate ?? DateTime.Now;
            DateTime fechaFin = dateTimePickerFin.SelectedDate ?? DateTime.Now;
            // Llamamos a ObtenerGanancia con las fechas seleccionadas
            var ganancia = ObtenerGanancia(fechaInicio, fechaFin);
            textBlockGanancia.Text = $"{ganancia:C}";
        }
        private void MostrarCapitalInvertido()
        {
            DateTime fechaInicio = dateTimePickerInicio.SelectedDate ?? DateTime.Now;
            DateTime fechaFin = dateTimePickerFin.SelectedDate ?? DateTime.Now;

            // Llamamos a ObtenerCapitalInvertido con las fechas seleccionadas
            var capitalInvertido = ObtenerCapitalInvertido(fechaInicio, fechaFin);
            textBlockCapitalInvertido.Text = $"{capitalInvertido:C}";
        }

        private decimal ObtenerCapitalInvertido(DateTime fechaInicio, DateTime fechaFin)
        {
            decimal costoTotal = 0;

            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    // Consulta para obtener el costo de los productos vendidos
                    string query = @"
        SELECT SUM(pr.precio_compra * dv.cantidad) 
        FROM detalle_venta dv
        INNER JOIN venta v ON dv.id_venta = v.id_venta
        INNER JOIN producto pr ON dv.id_producto = pr.id_producto
        WHERE v.fecha BETWEEN @fechaInicio AND @fechaFin";

                    using (var command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@fechaInicio", fechaInicio.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@fechaFin", fechaFin.ToString("yyyy-MM-dd"));

                        var result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            costoTotal = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return costoTotal;
        }
        private decimal ObtenerGanancia(DateTime fechaInicio, DateTime fechaFin)
        {
            decimal gananciaTotal = 0;

            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    // Consulta para obtener la ganancia de las ventas dentro del rango de fechas
                    string query = @"
            SELECT SUM((p.precio_venta - dc.precio_compra) * dv.cantidad)
            FROM detalle_venta dv
            INNER JOIN producto p ON dv.id_producto = p.id_producto
            INNER JOIN venta v ON dv.id_venta = v.id_venta
            INNER JOIN detalle_compra dc ON dc.id_producto = p.id_producto
            WHERE v.fecha BETWEEN @fechaInicio AND @fechaFin";

                    using (var command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@fechaInicio", fechaInicio.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@fechaFin", fechaFin.ToString("yyyy-MM-dd"));

                        var result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            gananciaTotal = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al calcular la ganancia: " + ex.Message);
            }

            return gananciaTotal;
        }

        public Transacciones(SQLiteConnection dbConnection)
        {
            connection = dbConnection;
        }

        private SQLiteConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString;
            return new SQLiteConnection(connectionString);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // Verificar si los controles de fecha no son null antes de acceder a ellos
            if (dateTimePickerInicio == null || dateTimePickerFin == null)
            {
                // Puedes manejar el error o simplemente retornar
         
                return;
            }

            DateTime fechaInicio = dateTimePickerInicio.SelectedDate.HasValue ? dateTimePickerInicio.SelectedDate.Value : DateTime.Today;
            DateTime fechaFin = dateTimePickerFin.SelectedDate.HasValue ? dateTimePickerFin.SelectedDate.Value : DateTime.Today.AddDays(1);

            if (radioVentas.IsChecked == true)
            {
                MostrarVentas(fechaInicio, fechaFin);  // Muestra las ventas filtradas
            }
            else if (radioCompras.IsChecked == true)
            {
                MostrarCompras(fechaInicio, fechaFin);  // Muestra las compras filtradas
            }
        }


        private void MostrarVentas(DateTime fechaInicio, DateTime fechaFin)
        {
            ConfigurarColumnasVentas();

            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    var ventas = ObtenerVentasConProductos(fechaInicio, fechaFin); // Pasamos las fechas como parámetros
                    dataGridVentasCompras.ItemsSource = ventas;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar las ventas: " + ex.Message);
            }
        }

        private void MostrarCompras(DateTime fechaInicio, DateTime fechaFin)
        {
            ConfigurarColumnasCompras();

            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    var compras = ObtenerComprasConProductos(fechaInicio, fechaFin); // Pasamos las fechas como parámetros
                    dataGridVentasCompras.ItemsSource = compras;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar las compras: " + ex.Message);
            }
        }


        private void ConfigurarColumnasVentas()
        {
            foreach (var column in dataGridVentasCompras.Columns)
            {
                if (column is DataGridTextColumn textColumn)
                {
                    switch (textColumn.Header.ToString())
                    {
                        case "Nombre":
                            textColumn.Binding = new Binding("NombreEntidad");
                            break;
                        case "Fecha":
                            textColumn.Binding = new Binding("Fecha") { StringFormat = "dd/MM/yyyy" };
                            break;
                        case "Cantidad":
                            textColumn.Binding = new Binding("TotalCantidad");
                            break;
                        case "Total Antes del Descuento":
                            textColumn.Binding = new Binding("TotalAntesDescuento");
                            break;
                        case "Descuento":
                            textColumn.Binding = new Binding("DescuentoTotal");
                            break;
                        case "Total":
                            textColumn.Binding = new Binding("TotalVenta");
                            break;
                    }
                }
            }
        }
        public class VentaCompra
        {
            public string NombreEntidad { get; set; }
            public DateTime Fecha { get; set; }
            public int TotalCantidad { get; set; }
            public decimal TotalAntesDescuento { get; set; }
            public decimal DescuentoTotal { get; set; }
            public decimal TotalVenta { get; set; }
        }

        private void ConfigurarColumnasCompras()
        {
            foreach (var column in dataGridVentasCompras.Columns)
            {
                if (column is DataGridTextColumn textColumn)
                {
                    switch (textColumn.Header.ToString())
                    {
                        case "Nombre":
                            textColumn.Binding = new Binding("NombreEntidad");
                            break;
                        case "Fecha":
                            textColumn.Binding = new Binding("Fecha") { StringFormat = "dd/MM/yyyy" };
                            break;
                        case "Cantidad":
                            textColumn.Binding = new Binding("TotalCantidad");
                            break;
                        case "Total Antes del Descuento":
                            textColumn.Binding = new Binding("TotalAntesDescuento");
                            break;
                        case "Descuento":
                            textColumn.Binding = new Binding("DescuentoTotal");
                            break;
                        case "Total":
                            textColumn.Binding = new Binding("TotalCompra");
                            break;
                    }
                }
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridVentasCompras.SelectedItem is Ventas ventaSeleccionada)
            {
                detalleListView.ItemsSource = ventaSeleccionada.Productos;
            }
            else if (dataGridVentasCompras.SelectedItem is Compra compraSeleccionada)
            {
                detalleListView.ItemsSource = compraSeleccionada.ProductosCompra;
            }

            detalleListView.Visibility = Visibility.Visible;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            detalleListView.Visibility = Visibility.Collapsed;
        }
        private void FiltrarDatos(object sender, SelectionChangedEventArgs e)
        {
            if (dateTimePickerInicio.SelectedDate.HasValue && dateTimePickerFin.SelectedDate.HasValue)
            {
                DateTime fechaInicio = dateTimePickerInicio.SelectedDate.Value;
                DateTime fechaFin = dateTimePickerFin.SelectedDate.Value;

                // Obtener datos filtrados desde la base de datos
                var datosFiltrados = ObtenerVentasConProductos(fechaInicio, fechaFin);

                // Asignar datos al DataGrid
                dataGridVentasCompras.ItemsSource = datosFiltrados;
            }
        }
       
        private List<Ventas> ObtenerVentasConProductos(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Ventas> listaVentas = new List<Ventas>();

            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string query = @"
            SELECT v.id_venta, v.fecha, v.tipo_pago, v.deuda_cliente, c.nombre_cliente, 
                   p.id_producto, p.nombre_producto, dv.cantidad, dv.precio, dv.descuento
            FROM venta v
            INNER JOIN cliente c ON v.id_cliente = c.id_cliente
            INNER JOIN detalle_venta dv ON v.id_venta = dv.id_venta
            INNER JOIN producto p ON dv.id_producto = p.id_producto
            WHERE v.fecha BETWEEN @fechaInicio AND @fechaFin";

                    using (var command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@fechaInicio", fechaInicio.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@fechaFin", fechaFin.ToString("yyyy-MM-dd"));

                        using (var reader = command.ExecuteReader())
                        {
                            Dictionary<int, Ventas> ventasDict = new Dictionary<int, Ventas>();

                            while (reader.Read())
                            {
                                int idVenta = Convert.ToInt32(reader["id_venta"]);

                                if (!ventasDict.ContainsKey(idVenta))
                                {
                                    ventasDict[idVenta] = new Ventas
                                    {
                                        Fecha = Convert.ToDateTime(reader["fecha"]),
                                        Cliente = new ClientesC { Nombre = reader["nombre_cliente"].ToString() },
                                        MetodoPago = reader["tipo_pago"].ToString(),
                                        Productos = new ObservableCollection<ProductoVenta>(),
                                        NombreEntidad = reader["nombre_cliente"].ToString()

                                    };
                                }

                                ProductoVenta producto = new ProductoVenta(
                                    Convert.ToInt32(reader["id_producto"]),
                                    reader["nombre_producto"].ToString(),
                                    Convert.ToInt32(reader["cantidad"]),
                                    Convert.ToDecimal(reader["precio"]),
                                    Convert.ToDecimal(reader["descuento"])
                                );

                                ventasDict[idVenta].Productos.Add(producto);
                            }

                            listaVentas = ventasDict.Values.ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos: " + ex.Message);
            }

            return listaVentas;
        }
        private List<Compra> ObtenerComprasConProductos(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Compra> listaCompras = new List<Compra>();

            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string query = @"
SELECT c.id_compra, c.fecha, c.tipo_pago, p.nombre_proveedor,
       pr.id_producto, pr.nombre_producto, dc.cantidad, 
       dc.precio_compra, dc.descuento, dc.fecha_caducidad
FROM compra c
INNER JOIN proveedor p ON c.id_proveedor = p.id_proveedor
INNER JOIN detalle_compra dc ON c.id_compra = dc.id_compra
INNER JOIN producto pr ON dc.id_producto = pr.id_producto
WHERE c.fecha BETWEEN @fechaInicio AND @fechaFin";


                    using (var command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@fechaInicio", fechaInicio.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@fechaFin", fechaFin.ToString("yyyy-MM-dd"));

                        using (var reader = command.ExecuteReader())
                        {
                            Dictionary<int, Compra> comprasDict = new Dictionary<int, Compra>();

                            while (reader.Read())
                            {
                                int idCompra = Convert.ToInt32(reader["id_compra"]);

                                if (!comprasDict.ContainsKey(idCompra))
                                {
                                    comprasDict[idCompra] = new Compra
                                    {
                                        Fecha = Convert.ToDateTime(reader["fecha"]),
                                        Proveedor = new proveedores { Nombre = reader["nombre_proveedor"].ToString() },
                                        MetodoPago = reader["tipo_pago"].ToString(),
                                        ProductosCompra = new ObservableCollection<ProductoCompra>(),
                                        NombreEntidad = reader["nombre_proveedor"].ToString()
                                    };
                                }
                                ProductoCompra producto = new ProductoCompra(
                                    Convert.ToInt32(reader["id_producto"]),
                                    reader["nombre_producto"].ToString(),
                                    Convert.ToInt32(reader["cantidad"]),
                                    Convert.ToDecimal(reader["precio_compra"]), // Cambio aquí
                                    Convert.ToDecimal(reader["descuento"]) // Cambio aquí
                                );


                                comprasDict[idCompra].ProductosCompra.Add(producto);
                            }

                            listaCompras = comprasDict.Values.ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos: " + ex.Message);
            }

            return listaCompras;
        }

    }
}
