using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using Xceed.Wpf.Toolkit;

namespace Comercial_Estefannny.ViewModel
{
    public class PantallaInicioC : ViewModelbase
    {
        private SeriesCollection _seriesCollection;
        private SeriesCollection _productCategorySeries;
        private SeriesCollection _totalSalesPurchasesSeries;
        private bool _isDailyView;
        private int _selectedMonth;
        private SQLiteConnection connection;
        private decimal _ventas;
        private decimal _compras;
        private decimal _ganancias;

        public decimal Ventas
        {
            get { return _ventas; }
            set
            {
                if (_ventas != value)
                {
                    _ventas = value;
                    OnPropertyChanged(); // Notificar al ViewModel sobre el cambio
                }
            }
        }

        public decimal Compras
        {
            get { return _compras; }
            set
            {
                if (_compras != value)
                {
                    _compras = value;
                    OnPropertyChanged(); // Notificar al ViewModel sobre el cambio
                }
            }
        }

        public decimal Ganancias
        {
            get { return _ganancias; }
            set
            {
                if (_ganancias != value)
                {
                    _ganancias = value;
                    OnPropertyChanged(); // Notificar al ViewModel sobre el cambio
                }
            }
        }
        // Constructor
        public PantallaInicioC()
        {
            SelectedMonth = DateTime.Now.Month;
            IsDailyView = true;
            SeriesCollection1 = new SeriesCollection(); // Inicialización de la colección de series
           
            ProductCategorySeries = new SeriesCollection(); // Gráfico de productos por categoría
            TotalSalesPurchasesSeries = new SeriesCollection(); // Gráfico de ventas y compras
            SelectedDate = DateTime.Now.Date;
            LoadGanancias(); // Cargar las ganancias inicialmente
            LoadProductCategoryData(); // Cargar productos por categoría
            LoadTotalSalesPurchases(); // Cargar ventas y compras totales
            ObtenerProductosBajoStock();
        }

        public List<string> Labels { get; set; } = new List<string>();  // Para los labels del eje X
        public string Formatter(object value)
        {
            return string.Format("{0:C}", value);  // Formato de moneda
        }

        // Series de ganancias
        public SeriesCollection SeriesCollection1
        {
            get { return _seriesCollection ?? (_seriesCollection = new SeriesCollection()); }
            set { _seriesCollection = value; OnPropertyChanged(); }
        }

        // Series de productos vendidos por categoría
        public SeriesCollection ProductCategorySeries
        {
            get { return _productCategorySeries ?? (_productCategorySeries = new SeriesCollection()); }
            set { _productCategorySeries = value; OnPropertyChanged(); }
        }

        // Series de total de ventas y compras
        public SeriesCollection TotalSalesPurchasesSeries
        {
            get { return _totalSalesPurchasesSeries ?? (_totalSalesPurchasesSeries = new SeriesCollection()); }
            set { _totalSalesPurchasesSeries = value; OnPropertyChanged(); }
        }

        // Propiedad para la vista diaria
        public bool IsDailyView
        {
            get { return _isDailyView; }
            set
            {
                _isDailyView = value;
                OnPropertyChanged();
                LoadGanancias(); // Cargar las ganancias cada vez que cambie la vista
                LoadProductCategoryData(); // Recargar productos por categoría
                LoadTotalSalesPurchases(); // Recargar ventas y compras totales
            }
        }
      
        // Propiedad para el mes seleccionado
        public int SelectedMonth
        {
            get { return _selectedMonth; }
            set
            {
                if (_selectedMonth != value)
                {
                    _selectedMonth = value;
                    OnPropertyChanged();
                    LoadGanancias(); // Cargar las ganancias nuevamente cuando se cambia el mes
                    LoadProductCategoryData(); // Recargar productos por categoría
                    LoadTotalSalesPurchases(); // Recargar ventas y compras
                }
            }
        }
        private bool _isMonthlyView;
        public bool IsMonthlyView
        {
            get => _isMonthlyView;
            set
            {
                if (_isMonthlyView != value)
                {
                    _isMonthlyView = value;
                    OnPropertyChanged();
                    LoadTotalSalesPurchases(); // Cargar al cambiar la vista
                }
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged();
                    LoadTotalSalesPurchases(); // Cargar al cambiar la fecha
                }
            }
        }
        // Inicializar la propiedad en el constructor del ViewModel



        // Método para cargar las ganancias (ya existente)
        private void LoadGanancias()
        {
            try
            {
                Console.WriteLine($"Mes seleccionado: {SelectedMonth}");
                SeriesCollection1 = new SeriesCollection();
                SeriesCollection1.Clear();  // Limpiar los datos previos
                var ganancias = IsDailyView ? ObtenerGananciasDiarias(SelectedMonth) : ObtenerGananciasMensuales();

                Labels.Clear();  // Limpiar los labels previos
                foreach (var ganancia in ganancias)
                {
                    if (!string.IsNullOrEmpty(ganancia.Item1))  // Verificar que el valor no sea null o vacío
                    {
                        Labels.Add(ganancia.Item1);  // Agregar el día o mes al Label

                        SeriesCollection1.Add(new ColumnSeries
                        {
                            Title = ganancia.Item1,
                            Values = new ChartValues<decimal> { ganancia.Item2 },
                            DataLabels = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar ganancias: {ex.Message}");
            }
        }

        // Método para cargar los productos vendidos por categoría (gráfico de torta)
        private void LoadProductCategoryData()
        {
            try
            {
                ProductCategorySeries = new SeriesCollection();
                ProductCategorySeries.Clear(); // Limpiar datos anteriores
                var productCategories = ObtenerProductosMasVendidos(SelectedMonth);

                if (productCategories == null || productCategories.Count == 0)
                {
                    Console.WriteLine("No se encontraron datos de productos por categoría.");
                    return;
                }

                foreach (var category in productCategories)
                {
                    ProductCategorySeries.Add(new PieSeries
                    {
                        Title = category.Item1,
                        Values = new ChartValues<decimal> { category.Item2 },
                        DataLabels = true
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar productos por categoría: {ex.Message}");
            }
        }

        // Método para cargar ventas y compras totales
        private void LoadTotalSalesPurchases()
        {
            try
            {
                decimal totalVentas = 0;
                decimal totalGanancias = 0;

                if (IsDailyView)
                {
                    // Si la vista es diaria, utiliza la fecha seleccionada en el DateTimePicker directamente
                    var ventasGanancias = ObtenerTotalVentasGanancias(SelectedDate.Date);

                    if (ventasGanancias != null && ventasGanancias.Count > 0)
                    {
                        foreach (var item in ventasGanancias)
                        {
                            Console.WriteLine($"Tipo: {item.Item1}, Monto: {item.Item2}");
                            if (item.Item1 == "Ventas")
                            {
                                totalVentas = item.Item2;
                            }
                            else if (item.Item1 == "Ganancias")
                            {
                                totalGanancias = item.Item2;
                            }
                        }
                    }
                }
                else if (IsMonthlyView)
                {
                    // Si la vista es mensual, utiliza solo el mes y año del DateTimePicker (ignora el día)
                    var ventasGanancias = ObtenerTotalVentasGanancias(new DateTime(SelectedDate.Year, SelectedDate.Month, 1));

                    if (ventasGanancias != null && ventasGanancias.Count > 0)
                    {
                        foreach (var item in ventasGanancias)
                        {
                            if (item.Item1 == "Ventas")
                            {
                                totalVentas = item.Item2;
                            }
                            else if (item.Item1 == "Ganancias")
                            {
                                totalGanancias = item.Item2;
                            }
                        }
                    }
                }

                // Asignar los valores a las propiedades
                Ventas = totalVentas;
                Ganancias = totalGanancias;

                // Actualizar las series para el gráfico
                TotalSalesPurchasesSeries = new SeriesCollection
        {
            new ColumnSeries
            {
                Title = "Ventas",
                Values = new ChartValues<decimal> { totalVentas },
                DataLabels = true
            },
            new ColumnSeries
            {
                Title = "Ganancias",
                Values = new ChartValues<decimal> { totalGanancias },
                DataLabels = true
            }
        };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar ventas y ganancias: {ex.Message}");
            }
        }


        // Obtener ganancias mensuales
        private List<Tuple<string, decimal>> ObtenerGananciasDiarias(int mes)
        {
            var gananciasDiarias = new List<Tuple<string, decimal>>();

            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
                SELECT strftime('%d', venta.fecha) AS Dia, SUM((detalle_venta.precio - producto.precio_compra) * detalle_venta.cantidad) AS Ganancia
                FROM venta
                INNER JOIN detalle_venta ON venta.id_venta = detalle_venta.id_venta
                INNER JOIN producto ON detalle_venta.id_producto = producto.id_producto
                WHERE strftime('%m', venta.fecha) = @Mes
                GROUP BY Dia";

                using (var command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Mes", mes.ToString("D2"));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string dia = reader["Dia"].ToString();
                            decimal ganancia = reader.GetDecimal(reader.GetOrdinal("Ganancia"));
                            gananciasDiarias.Add(new Tuple<string, decimal>(dia, ganancia));
                        }
                    }
                }
            }
            return gananciasDiarias;
        }

        private List<Tuple<string, decimal>> ObtenerGananciasMensuales()
        {
            var gananciasMensuales = new List<Tuple<string, decimal>>();

            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
                SELECT strftime('%m', venta.fecha) AS Mes, SUM((detalle_venta.precio - producto.precio_compra) * detalle_venta.cantidad) AS Ganancia
                FROM venta
                INNER JOIN detalle_venta ON venta.id_venta = detalle_venta.id_venta
                INNER JOIN producto ON detalle_venta.id_producto = producto.id_producto
                GROUP BY Mes";

                using (var command = new SQLiteCommand(query, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string mes = reader["Mes"].ToString();
                            decimal ganancia = reader.GetDecimal(reader.GetOrdinal("Ganancia"));
                            gananciasMensuales.Add(new Tuple<string, decimal>(mes, ganancia));
                        }
                    }
                }
            }
            return gananciasMensuales;
        }

        // Obtener productos vendidos por categoría
        private List<Tuple<string, decimal>> ObtenerProductosPorCategoria(int mes)
        {
            var productosPorCategoria = new List<Tuple<string, decimal>>();

            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
                SELECT categoria.nombre_categoria, SUM(detalle_venta.cantidad) AS TotalVendidos
                FROM venta
                INNER JOIN detalle_venta ON venta.id_venta = detalle_venta.id_venta
                INNER JOIN producto ON detalle_venta.id_producto = producto.id_producto
                INNER JOIN categoria ON producto.id_categoria = categoria.id_categoria
                WHERE strftime('%m', venta.fecha) = @Mes
                GROUP BY categoria.nombre_categoria";

                using (var command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Mes", mes.ToString("D2"));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string categoria = reader["nombre_categoria"].ToString();
                            decimal totalVendidos = reader.GetDecimal(reader.GetOrdinal("TotalVendidos"));
                            productosPorCategoria.Add(new Tuple<string, decimal>(categoria, totalVendidos));
                        }
                    }
                }
            }
            return productosPorCategoria;
        }
        private List<Tuple<string, decimal>> ObtenerProductosMasVendidos(int mes)
        {
            var productosMasVendidos = new List<Tuple<string, decimal>>();

            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
        SELECT producto.nombre_producto, SUM(detalle_venta.cantidad) AS TotalVendidos
        FROM venta
        INNER JOIN detalle_venta ON venta.id_venta = detalle_venta.id_venta
        INNER JOIN producto ON detalle_venta.id_producto = producto.id_producto
        WHERE strftime('%m', venta.fecha) = @Mes
        GROUP BY producto.nombre_producto
        ORDER BY TotalVendidos DESC
        LIMIT 10";
        
        using (var command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Mes", mes.ToString("D2"));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombreProducto = reader["nombre_producto"].ToString();
                            decimal totalVendidos = reader.IsDBNull(reader.GetOrdinal("TotalVendidos")) ? 0 : reader.GetDecimal(reader.GetOrdinal("TotalVendidos"));

                            productosMasVendidos.Add(new Tuple<string, decimal>(nombreProducto, totalVendidos));
                        }
                    }
                }
            }

            return productosMasVendidos;
        }


        // Obtener ventas y compras totales
        private List<Tuple<string, decimal>> ObtenerTotalVentasGanancias(DateTime fecha)
        {
            var ventasGanancias = new List<Tuple<string, decimal>>();

            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT 'Ventas' AS Tipo, SUM(detalle_venta.precio * detalle_venta.cantidad) AS TotalVentas
            FROM venta
            INNER JOIN detalle_venta ON venta.id_venta = detalle_venta.id_venta
            WHERE strftime('%Y-%m-%d', venta.fecha) = @Fecha";

                using (var command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Fecha", fecha.ToString("yyyy-MM-dd"));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tipo = reader["Tipo"].ToString();
                            decimal totalVentas = reader.GetDecimal(reader.GetOrdinal("TotalVentas"));
                            ventasGanancias.Add(new Tuple<string, decimal>(tipo, totalVentas));
                        }
                    }
                }

                // Calculamos las ganancias
                string gananciaQuery = @"
            SELECT SUM((detalle_venta.precio - producto.precio_compra) * detalle_venta.cantidad) AS TotalGanancias
            FROM venta
            INNER JOIN detalle_venta ON venta.id_venta = detalle_venta.id_venta
            INNER JOIN producto ON detalle_venta.id_producto = producto.id_producto
            WHERE strftime('%Y-%m-%d', venta.fecha) = @Fecha";

                using (var command = new SQLiteCommand(gananciaQuery, conn))
                {
                    command.Parameters.AddWithValue("@Fecha", fecha.ToString("yyyy-MM-dd"));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal totalGanancias = reader.GetDecimal(reader.GetOrdinal("TotalGanancias"));
                            ventasGanancias.Add(new Tuple<string, decimal>("Ganancias", totalGanancias));
                        }
                    }
                }
            }

            return ventasGanancias;
        }

        // Método de conexión a SQLite
        private SQLiteConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString;
            return new SQLiteConnection(connectionString);
        }
        public List<Tuple<string, int>> ObtenerProductosBajoStock()
        {
            var productosBajoStock = new List<Tuple<string, int>>();

            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
        SELECT nombre_producto, cantidad
        FROM producto
        WHERE cantidad < 5";

                using (var command = new SQLiteCommand(query, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombreProducto = reader["nombre_producto"].ToString();
                            int cantidad = reader.GetInt32(reader.GetOrdinal("cantidad"));
                            productosBajoStock.Add(new Tuple<string, int>(nombreProducto, cantidad));
                        }
                    }
                }
            }
            return productosBajoStock;
        }

    }
}
