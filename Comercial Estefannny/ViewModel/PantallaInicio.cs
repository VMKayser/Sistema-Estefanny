using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Comercial_Estefannny.ViewModel
{
    /// <summary>
    /// ViewModel moderno para PantallaInicio con source generators de .NET 8.0
    /// </summary>
    public partial class PantallaInicioC : ViewModelbase
    {
        private readonly ILogger<PantallaInicioC>? _logger;        // Source generators automáticamente generan las propiedades y notificaciones
        [ObservableProperty]
        private ISeries[] _seriesCollection = Array.Empty<ISeries>();        [ObservableProperty]
        private ISeries[] _productCategorySeries = Array.Empty<ISeries>();
        
        [ObservableProperty]
        private ISeries[] _totalSalesPurchasesSeries = Array.Empty<ISeries>();
        
        [ObservableProperty]
        private bool _isDailyView;

        [ObservableProperty]
        private bool _isMonthlyView;

        [ObservableProperty]
        private int _selectedMonth;

        [ObservableProperty]
        private decimal _ventas;

        [ObservableProperty]
        private decimal _compras;

        [ObservableProperty]
        private decimal _ganancias;

        [ObservableProperty]
        private DateTime _selectedDate;

        [ObservableProperty]
        private List<string> _labels = new();

        public PantallaInicioC(ILogger<PantallaInicioC>? logger = null)
        {
            _logger = logger;
            _selectedMonth = DateTime.Now.Month;
            _isDailyView = true;
            _selectedDate = DateTime.Now.Date;
            
            // Inicializar con gráficos modernos de LiveChartsCore
            InitializeModernCharts();
            
            // Cargar datos de forma asíncrona
            _ = Task.Run(async () =>
            {
                await LoadGananciasAsync();
                await LoadProductCategoryDataAsync();
                await LoadTotalSalesPurchasesAsync();
                await ObtenerProductosBajoStockAsync();
            });
        }

        // Formatter moderno para .NET 8.0
        public Func<double, string> Formatter => value => value.ToString("C", System.Globalization.CultureInfo.CurrentCulture);

        private void InitializeModernCharts()
        {
            _seriesCollection = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = new double[] { 200, 300, 500, 400, 600 },
                    Fill = null,
                    Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 3 },
                    GeometryFill = new SolidColorPaint(SKColors.Blue),
                    GeometryStroke = new SolidColorPaint(SKColors.White) { StrokeThickness = 2 }
                }
            };

            _productCategorySeries = new ISeries[]
            {
                new PieSeries<double> { Values = new double[] { 30 }, Name = "Electrónicos" },
                new PieSeries<double> { Values = new double[] { 20 }, Name = "Ropa" },
                new PieSeries<double> { Values = new double[] { 10 }, Name = "Hogar" }
            };

            _totalSalesPurchasesSeries = new ISeries[]
            {
                new ColumnSeries<double> 
                { 
                    Values = new double[] { 1000, 800, 1200 }, 
                    Name = "Ventas",
                    Fill = new SolidColorPaint(SKColors.Green)
                },
                new ColumnSeries<double> 
                { 
                    Values = new double[] { 600, 500, 700 }, 
                    Name = "Compras",
                    Fill = new SolidColorPaint(SKColors.Orange)
                }
            };

        }        // Métodos para manejar cambios de propiedades con source generators
        partial void OnIsDailyViewChanged(bool oldValue, bool newValue)
        {
            LoadGanancias(); // Cargar las ganancias cada vez que cambie la vista
            LoadProductCategoryData(); // Recargar productos por categoría
            LoadTotalSalesPurchases(); // Recargar ventas y compras totales
        }
        
        partial void OnSelectedMonthChanged(int oldValue, int newValue)
        {
            LoadGanancias(); // Cargar las ganancias nuevamente cuando se cambia el mes
            LoadProductCategoryData(); // Recargar productos por categoría
            LoadTotalSalesPurchases(); // Recargar ventas y compras
        }
        
        partial void OnSelectedDateChanged(DateTime oldValue, DateTime newValue)
        {
            LoadTotalSalesPurchases(); // Cargar al cambiar la fecha
        }

        // Inicializar la propiedad en el constructor del ViewModel
        // Método para cargar las ganancias (ya existente)
        private void LoadGanancias()
        {
            try
            {
                Console.WriteLine($"Mes seleccionado: {_selectedMonth}");
                var ganancias = _isDailyView ? ObtenerGananciasDiarias(_selectedMonth) : ObtenerGananciasMensuales();

                _labels.Clear();  // Limpiar los labels previos
                var seriesData = new List<double>();
                
                foreach (var ganancia in ganancias)
                {
                    if (!string.IsNullOrEmpty(ganancia.Item1))  // Verificar que el valor no sea null o vacío
                    {
                        _labels.Add(ganancia.Item1);  // Agregar el día o mes al Label
                        seriesData.Add((double)ganancia.Item2);
                    }
                }

                // Actualizar la serie con LiveChartsCore
                _seriesCollection = new ISeries[]
                {
                    new ColumnSeries<double>
                    {
                        Values = seriesData,
                        Name = "Ganancias",
                        Fill = new SolidColorPaint(SKColors.Blue)
                    }
                };
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
                var productCategories = ObtenerProductosMasVendidos(_selectedMonth);

                if (productCategories == null || productCategories.Count == 0)
                {
                    Console.WriteLine("No se encontraron datos de productos por categoría.");
                    return;
                }

                var pieSeries = new List<ISeries>();
                foreach (var category in productCategories)
                {
                    pieSeries.Add(new PieSeries<double>
                    {
                        Values = new double[] { (double)category.Item2 },
                        Name = category.Item1
                    });
                }

                _productCategorySeries = pieSeries.ToArray();
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

                if (_isDailyView)
                {
                    // Si la vista es diaria, utiliza la fecha seleccionada en el DateTimePicker directamente
                    var ventasGanancias = ObtenerTotalVentasGanancias(_selectedDate.Date);

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
                else if (_isMonthlyView)
                {
                    // Si la vista es mensual, utiliza solo el mes y año del DateTimePicker (ignora el día)
                    var ventasGanancias = ObtenerTotalVentasGanancias(new DateTime(_selectedDate.Year, _selectedDate.Month, 1));

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
                _ventas = totalVentas;
                _ganancias = totalGanancias;

                // Actualizar las series para el gráfico con LiveChartsCore
                _totalSalesPurchasesSeries = new ISeries[]
                {
                    new ColumnSeries<double>
                    {
                        Values = new double[] { (double)totalVentas },
                        Name = "Ventas",
                        Fill = new SolidColorPaint(SKColors.Green)
                    },
                    new ColumnSeries<double>
                    {
                        Values = new double[] { (double)totalGanancias },
                        Name = "Ganancias",
                        Fill = new SolidColorPaint(SKColors.Blue)
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
            }            return ventasGanancias;
        }

        // Método de conexión a SQLite
        private SQLiteConnection GetConnection()
        {
            try
            {
                // Primero intentar cargar desde ConfigurationManager
                var connectionConfig = ConfigurationManager.ConnectionStrings["Cadena"];
                string connectionString = null;
                
                if (connectionConfig != null && !string.IsNullOrEmpty(connectionConfig.ConnectionString))
                {
                    connectionString = connectionConfig.ConnectionString;
                }
                else
                {
                    // Fallback: usar ruta directa si ConfigurationManager no funciona
                    string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "comercial_Estefanny_base_datos.db");
                    if (File.Exists(dbPath))
                    {
                        connectionString = $"Data Source={dbPath};Version=3;";
                    }
                    else
                    {
                        // Intentar con el nombre original
                        dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "comercial Estefanny base datos.db");
                        if (File.Exists(dbPath))
                        {
                            connectionString = $"Data Source={dbPath};Version=3;";
                        }
                        else
                        {
                            throw new FileNotFoundException("No se encontró el archivo de base de datos. Verifique que 'comercial_Estefanny_base_datos.db' esté en el directorio de la aplicación.");
                        }
                    }
                }
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("No se pudo obtener una cadena de conexión válida.");
                }
                
                var connection = new SQLiteConnection(connectionString);
                
                // Verificar si la base de datos necesita inicialización
                try
                {
                    connection.Open();
                    string checkQuery = "SELECT COUNT(*) FROM categoria";
                    using (var cmd = new SQLiteCommand(checkQuery, connection))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count == 0)
                        {
                            connection.Close();
                            InicializarBaseDatosConDatosMuestra(connection);
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    // Si hay error al verificar, simplemente continuar
                    System.Diagnostics.Debug.WriteLine($"No se pudo verificar el estado de la base de datos: {ex.Message}");
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }
                
                return new SQLiteConnection(connectionString);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener la conexión a la base de datos: {ex.Message}", ex);
            }
        }public List<Tuple<string, int>> ObtenerProductosBajoStock()
        {
            var productosBajoStock = new List<Tuple<string, int>>();

            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string query = @"
            SELECT nombre_producto, cantidad
            FROM producto
            WHERE cantidad < 5";

                    using (var command = new SQLiteCommand(query, conn))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombreProducto = reader["nombre_producto"].ToString() ?? string.Empty;
                            int cantidad = reader.GetInt32(reader.GetOrdinal("cantidad"));
                            productosBajoStock.Add(new Tuple<string, int>(nombreProducto, cantidad));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error and return empty list to prevent crashes
                System.Diagnostics.Debug.WriteLine($"Error en ObtenerProductosBajoStock: {ex.Message}");
                Console.WriteLine($"Error en ObtenerProductosBajoStock: {ex.Message}");
            }
            
            return productosBajoStock;
        }

        // Métodos asíncronos modernos para .NET 8.0
        private async Task LoadGananciasAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    // Lógica de carga de ganancias
                    var connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"]?.ConnectionString;
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        connectionString = "Data Source=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "comercial_Estefanny_base_datos.db");
                    }

                    using var connection = new SQLiteConnection(connectionString);
                    connection.Open();
                    
                    // Consulta optimizada para .NET 8.0
                    string query = @"
                        SELECT 
                            COALESCE(SUM(total), 0) as Ventas,
                            (SELECT COALESCE(SUM(total), 0) FROM compras) as Compras
                        FROM ventas";
                    
                    using var command = new SQLiteCommand(query, connection);
                    using var reader = command.ExecuteReader();
                    
                    if (reader.Read())
                    {
                        _ventas = Convert.ToDecimal(reader["Ventas"]);
                        _compras = Convert.ToDecimal(reader["Compras"]);
                        _ganancias = _ventas - _compras;
                    }
                });

                _logger?.LogInformation("Ganancias cargadas: Ventas={Ventas}, Compras={Compras}, Ganancias={Ganancias}", 
                    _ventas, _compras, _ganancias);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error cargando ganancias");
            }
        }

        private async Task LoadProductCategoryDataAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    // Lógica moderna para cargar datos de categorías
                    _logger?.LogInformation("Cargando datos de categorías de productos");
                });
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error cargando datos de categorías");
            }
        }

        private async Task LoadTotalSalesPurchasesAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    // Lógica moderna para cargar ventas y compras totales
                    _logger?.LogInformation("Cargando ventas y compras totales");
                });
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error cargando ventas y compras totales");
            }
        }

        private async Task ObtenerProductosBajoStockAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    // Lógica moderna para productos bajo stock
                    _logger?.LogInformation("Verificando productos bajo stock");
                });
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error verificando productos bajo stock");
            }
        }

        // Comandos modernos con source generators
        [RelayCommand]
        private async Task RefreshDataAsync()
        {
            await LoadGananciasAsync();
            await LoadProductCategoryDataAsync();
            await LoadTotalSalesPurchasesAsync();
            await ObtenerProductosBajoStockAsync();
        }

        [RelayCommand]
        private void ToggleView()
        {
            _isDailyView = !_isDailyView;
            _ = Task.Run(async () => await RefreshDataAsync());
        }

        private void InicializarBaseDatosConDatosMuestra(SQLiteConnection connection)
        {
            try
            {
                connection.Open();
                
                // Verificar si ya hay datos
                string checkQuery = "SELECT COUNT(*) FROM categoria";
                using (var cmd = new SQLiteCommand(checkQuery, connection))
                {
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        return; // Ya hay datos, no necesitamos inicializar
                    }
                }
                
                // Insertar datos de muestra
                string insertData = @"
                    -- Insertar categorías
                    INSERT INTO categoria (nombre_categoria, codigo) VALUES 
                    ('Electrónicos', 'ELEC001'),
                    ('Ropa', 'ROPA001'),
                    ('Hogar', 'HOGAR001'),
                    ('Deportes', 'DEP001'),
                    ('Alimentación', 'ALIM001');
                    
                    -- Insertar marcas
                    INSERT INTO marca (nombre_marca, codigo) VALUES 
                    ('Samsung', 'SAM001'),
                    ('Nike', 'NIKE001'),
                    ('Adidas', 'ADI001'),
                    ('Sony', 'SONY001'),
                    ('Apple', 'APP001');
                    
                    -- Insertar productos
                    INSERT INTO producto (nombre_producto, precio, cantidad, id_categoria, id_marca, codigo_barras) VALUES 
                    ('Smartphone Galaxy S24', 899.99, 15, 1, 1, '123456789012'),
                    ('Zapatillas Running', 129.99, 25, 4, 2, '123456789013'),
                    ('Auriculares Bluetooth', 199.99, 8, 1, 4, '123456789014'),
                    ('Camiseta Deportiva', 39.99, 50, 2, 3, '123456789015'),
                    ('iPad Pro', 1099.99, 5, 1, 5, '123456789016'),
                    ('Lámpara LED', 45.99, 12, 3, 1, '123456789017'),
                    ('Proteína en Polvo', 89.99, 20, 5, 2, '123456789018');
                    
                    -- Insertar clientes
                    INSERT INTO cliente (nombre, direccion, telefono) VALUES 
                    ('Juan Pérez', 'Av. Principal 123', '555-0001'),
                    ('María García', 'Calle Secundaria 456', '555-0002'),
                    ('Carlos López', 'Jr. Los Olivos 789', '555-0003'),
                    ('Ana Martínez', 'Av. Central 321', '555-0004'),
                    ('Luis Rodríguez', 'Calle Nueva 654', '555-0005');
                    
                    -- Insertar proveedores
                    INSERT INTO proveedor (nombre, direccion, telefono) VALUES 
                    ('TechSupplier S.A.', 'Zona Industrial 100', '555-1001'),
                    ('SportGoods Inc.', 'Av. Comercial 200', '555-1002'),
                    ('HomeDecor Ltd.', 'Calle Empresarial 300', '555-1003'),
                    ('FoodDistributor Co.', 'Jr. Mayorista 400', '555-1004');
                    
                    -- Insertar algunas ventas de muestra
                    INSERT INTO venta (fecha, id_cliente, total) VALUES 
                    (date('now', '-30 days'), 1, 899.99),
                    (date('now', '-25 days'), 2, 169.98),
                    (date('now', '-20 days'), 3, 1099.99),
                    (date('now', '-15 days'), 4, 129.99),
                    (date('now', '-10 days'), 5, 45.99),
                    (date('now', '-5 days'), 1, 239.98),
                    (date('now', '-2 days'), 2, 89.99);
                    
                    -- Insertar detalles de venta
                    INSERT INTO detalle_venta (id_venta, id_producto, cantidad, precio_unitario) VALUES 
                    (1, 1, 1, 899.99),
                    (2, 2, 1, 129.99),
                    (2, 4, 1, 39.99),
                    (3, 5, 1, 1099.99),
                    (4, 2, 1, 129.99),
                    (5, 6, 1, 45.99),
                    (6, 3, 1, 199.99),
                    (6, 4, 1, 39.99),
                    (7, 7, 1, 89.99);
                    
                    -- Insertar algunas compras de muestra
                    INSERT INTO compra (fecha, id_proveedor, total) VALUES 
                    (date('now', '-35 days'), 1, 4500.00),
                    (date('now', '-28 days'), 2, 2000.00),
                    (date('now', '-21 days'), 3, 800.00),
                    (date('now', '-14 days'), 4, 1200.00);
                    
                    -- Insertar detalles de compra
                    INSERT INTO detalle_compra (id_compra, id_producto, cantidad, precio_unitario) VALUES 
                    (1, 1, 10, 450.00),
                    (2, 2, 20, 65.00),
                    (2, 4, 30, 20.00),
                    (3, 6, 15, 25.00),
                    (3, 3, 5, 120.00),
                    (4, 7, 25, 48.00);
                ";
                
                // Dividir y ejecutar comandos
                string[] commands = insertData.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                
                foreach (string commandText in commands)
                {
                    string trimmedCommand = commandText.Trim();
                    if (!string.IsNullOrEmpty(trimmedCommand) && !trimmedCommand.StartsWith("--"))
                    {
                        using (var cmd = new SQLiteCommand(trimmedCommand, connection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                
                MessageBox.Show("Base de datos inicializada con datos de muestra.", "Inicialización", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar la base de datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
