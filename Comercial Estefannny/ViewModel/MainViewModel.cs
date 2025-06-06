using FontAwesome.Sharp;
using System.Windows.Input;
using Comercial_Estefannny.ViewModel;
using Comercial_Estefannny.View;

namespace Comercial_Estefannny.ViewModel
{
    public class MainViewModel : ViewModelbase
    {
        // Campos privados
        private object _currentView = new PantallaInicio();
        private string _caption = "Inicio";
        private IconChar _icon = IconChar.Home;

        // Propiedades públicas
        public object CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(nameof(CurrentView)); }
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
            // Vista por defecto            ExecuteVentanaInicial(null!);
        }

        // Métodos de cambio de vista
        private void ExecuteVentanaPruebaBase(object obj)
        {
            // Crear instancia de la vista correspondiente
            CurrentView = new Pruebabase();
            Caption = "Prueba Base";
            Icon = IconChar.Home;
        }

        private void ExecuteVentanaTransacciones(object obj)
        {
            CurrentView = new Transacciones();
            Caption = "Transacciones";
            Icon = IconChar.ExchangeAlt;
        }        private void ExecuteVentanaProveedores(object obj)
        {
            CurrentView = new View.Proveedores();
            Caption = "Proveedores";
            Icon = IconChar.Truck;
        }

        private void ExecuteVentanaCompras(object obj)
        {
            CurrentView = new Compras();
            Caption = "Compras";
            Icon = IconChar.ShoppingBag;
        }

        private void ExecuteVentanaInventario(object obj)
        {
            CurrentView = new Inventario();
            Caption = "Inventario";
            Icon = IconChar.Warehouse;
        }

        private void ExecuteVentanaClientes(object obj)
        {
            CurrentView = new Clientes();
            Caption = "Clientes";
            Icon = IconChar.Users;
        }        private void ExecuteVentanaVentas(object obj)
        {
            CurrentView = new VentasView();
            Caption = "Ventas";
            Icon = IconChar.ShoppingCart;
        }

        private void ExecuteVentanaInicial(object obj)
        {
            CurrentView = new PantallaInicio();
            Caption = "Inicio";
            Icon = IconChar.Home;
        }
    }
}
