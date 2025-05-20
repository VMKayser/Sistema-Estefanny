using FontAwesome.Sharp;
using System.Windows.Input;

namespace Comercial_Estefannny.ViewModel
{
    public class MainViewModel : ViewModelbase
    {
        // Campos privados
        private ViewModelbase _currentChildView;
        private string _caption;
        private IconChar _icon;

        // Propiedades públicas
        public ViewModelbase CurrentChildView
        {
            get => _currentChildView;
            set { _currentChildView = value; OnPropertyChanged(nameof(CurrentChildView)); }
        }
        public string Caption
        {
            get => _caption;
            set { _caption = value; OnPropertyChanged(nameof(Caption)); }
        }
        public IconChar Icon
        {
            get => _icon;
            set { _icon = value; OnPropertyChanged(nameof(Icon)); }
        }

        // Comandos
        public ICommand VentanaInicial { get; set; }
        public ICommand VentanaClientes { get; set; }
        public ICommand VentanaInventario { get; set; }
        public ICommand VentanaCompras { get; set; }
        public ICommand VentanaVentas { get; set; }
        public ICommand VentanaProveedores { get; set; }
        public ICommand VentanaTransacciones { get; set; }
        public ICommand VentanaPruebaBase { get; set; }

        public MainViewModel()
        {
            VentanaInicial = new ViewModelCommand(ExecuteVentanaInicial);
            VentanaVentas = new ViewModelCommand(ExecuteVentanaVentas);
            VentanaClientes = new ViewModelCommand(ExecuteVentanaClientes);
            VentanaInventario = new ViewModelCommand(ExecuteVentanaInventario);
            VentanaCompras = new ViewModelCommand(ExecuteVentanaCompras);
            VentanaProveedores = new ViewModelCommand(ExecuteVentanaProveedores);
            VentanaTransacciones = new ViewModelCommand(ExecuteVentanaTransacciones);
            VentanaPruebaBase = new ViewModelCommand(ExecuteVentanaPruebaBase);
            // Vista por defecto
            ExecuteVentanaInicial(null);
        }

        // Métodos de cambio de vista
        private void ExecuteVentanaPruebaBase(object obj)
        {
            CurrentChildView = new pruebaBaseC();
            Caption = "Prueba Base";
            Icon = IconChar.Home;
        }
        private void ExecuteVentanaTransacciones(object obj)
        {
            CurrentChildView = new TransaccionesC();
            Caption = "Transacciones";
            Icon = IconChar.ExchangeAlt;
        }
        private void ExecuteVentanaProveedores(object obj)
        {
            CurrentChildView = new proveedores();
            Caption = "Proveedores";
            Icon = IconChar.Truck;
        }
        private void ExecuteVentanaCompras(object obj)
        {
            CurrentChildView = new Compra();
            Caption = "Compras";
            Icon = IconChar.ShoppingCart;
        }
        private void ExecuteVentanaInventario(object obj)
        {
            CurrentChildView = new InventarioC();
            Caption = "Inventario";
            Icon = IconChar.Warehouse;
        }
        private void ExecuteVentanaClientes(object obj)
        {
            CurrentChildView = new ClientesC();
            Caption = "Clientes";
            Icon = IconChar.User;
        }
        private void ExecuteVentanaVentas(object obj)
        {
            CurrentChildView = new Ventas();
            Caption = "Ventas";
            Icon = IconChar.ShoppingCart;
        }
        private void ExecuteVentanaInicial(object obj)
        {
            CurrentChildView = new PantallaInicioC();
            Caption = "Inicio";
            Icon = IconChar.Home;
        }
    }
}
