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
    public partial class Clientes : UserControl
    {
        private ICollectionView ClientesView;
        public ObservableCollection<ClientesC> ClientesList { get; set; }= new ObservableCollection<ClientesC>();

        public Clientes()
        {

            InitializeComponent();

            // Asignar la colección de productos a un ICollectionView para habilitar el filtrado
            CargarClientes();
            ClientesView = CollectionViewSource.GetDefaultView(ClientesList);
            ClientesListView.ItemsSource = ClientesView;
        }
        private void CargarClientes()
        {
            // Limpiar la colección actual
            ClientesList.Clear();

            // Obtener los productos de la base de datos y agregar a la colección
            foreach (var cliente in ClientesC.ObtenerClientes())
            {
                ClientesList.Add(cliente);
            }

            // Asignar la colección al ListView
            ClientesListView.ItemsSource = ClientesList;
        }
        // Método para agregar cliente
        // Método para agregar un cliente
        private void BotonAgregarCliente_Click(object sender, RoutedEventArgs e)
        {
            string nombreCliente = TextNombreCliente.Text;
            string direccion = TextDireccionCliente.Text;
            string telefono = TextTelefonoCliente.Text;
            double deuda;

            // Validar que el nombre esté ingresado
            if (string.IsNullOrEmpty(nombreCliente))
            {
                MessageBox.Show("Por favor, ingrese el nombre del cliente.");
                return;
            }

            // Convertir deuda en un valor por defecto si no está ingresada
            if (!double.TryParse(TextDeudaCliente.Text, out deuda))
            {
                deuda = 0.0;  // Si no se ingresa una deuda, se asigna 0 por defecto
            }

            // Crear un objeto Cliente con los datos ingresados
            ClientesC nuevoCliente = new ClientesC
            {
                Nombre = nombreCliente,
                Direccion = direccion,
                Telefono = telefono,
                Deuda = deuda
            };

            // Llamar al método del ViewModel para insertar el cliente en la base de datos
            nuevoCliente.InsertarCliente();  // Asumimos que tienes este método en tu ViewModel

            MessageBox.Show("Cliente agregado exitosamente.");

            // Actualizar la lista de clientes
            CargarClientes();
            LimpiarCampos();
        }


        private void BotonEliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            string nombreCliente = TextNombreCliente.Text;

            // Validar que el nombre esté ingresado
            if (string.IsNullOrEmpty(nombreCliente))
            {
                MessageBox.Show("Por favor, ingrese el nombre del cliente a eliminar.");
                return;
            }

            // Llamada al ViewModel para eliminar el cliente
            var cliente = ClientesList.FirstOrDefault(c => c.Nombre == nombreCliente);
            if (cliente != null)
            {
                cliente.EliminarCliente();  // Método en tu ViewModel que elimina el cliente
                MessageBox.Show("Cliente eliminado exitosamente.");
            }
            else
            {
                MessageBox.Show("Cliente no encontrado.");
            }

            // Actualizar la lista de clientes
            CargarClientes();
            // Limpiar los campos de texto después de eliminar
            LimpiarCampos();
        }

        private void BotonEditarCliente_Click(object sender, RoutedEventArgs e)
{
    string nombreCliente = TextNombreCliente.Text;
    string nuevaDireccion = TextDireccionCliente.Text;
    string nuevoTelefono = TextTelefonoCliente.Text;
    double nuevaDeuda;

    // Validar que el nombre esté ingresado
    if (string.IsNullOrEmpty(nombreCliente))
    {
        MessageBox.Show("Por favor, ingrese el nombre del cliente.");
        return;
    }

    // Convertir deuda en un valor por defecto si no está ingresada
    if (!double.TryParse(TextDeudaCliente.Text, out nuevaDeuda))
    {
        nuevaDeuda = 0.0;  // Si no se ingresa una deuda, se asigna 0 por defecto
    }

    // Llamada al ViewModel para actualizar el cliente
    var cliente = ClientesList.FirstOrDefault(c => c.Nombre == nombreCliente);
    if (cliente != null)
    {
        cliente.Direccion = nuevaDireccion;
        cliente.Telefono = nuevoTelefono;
        cliente.Deuda = nuevaDeuda;

        cliente.ActualizarCliente();  // Método en tu ViewModel para actualizar el cliente

        MessageBox.Show("Cliente actualizado exitosamente.");
    }
    else
    {
        MessageBox.Show("Cliente no encontrado.");
    }

    // Actualizar la lista de clientes
    CargarClientes();
    // Limpiar los campos de texto después de editar
    LimpiarCampos();
}


        // Limpiar los campos de texto
        private void LimpiarCampos()
        {
            TextNombreCliente.Clear();
            TextDireccionCliente.Clear();
            TextTelefonoCliente.Clear();
            TextDeudaCliente.Clear();
        }
        private void ClientesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verificar si se ha seleccionado un cliente
            if (ClientesListView.SelectedItem != null)
            {
                // Obtener el cliente seleccionado
                ClientesC clienteSeleccionado = (ClientesC)ClientesListView.SelectedItem;

                // Rellenar los campos con los datos del cliente
                TextNombreCliente.Text = clienteSeleccionado.Nombre;
                TextDireccionCliente.Text = clienteSeleccionado.Direccion;
                TextTelefonoCliente.Text = clienteSeleccionado.Telefono;
                TextDeudaCliente.Text = clienteSeleccionado.Deuda.ToString();
            }
        }

        private void BotonImportarDatosCliente_Click(object sender, RoutedEventArgs e)
        {
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
                    var clientesViewModel = new ClientesC();
                    clientesViewModel.ImportarClientesDesdeExcel(filePath);

                    MessageBox.Show("Clientes importados exitosamente.");

                    // Actualizar la lista de clientes después de la importación
                    CargarClientes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al importar los datos: {ex.Message}");
                }
            }
        }
    }
}
