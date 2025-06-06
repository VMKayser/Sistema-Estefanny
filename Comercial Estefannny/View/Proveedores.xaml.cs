﻿using System;
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
using ViewModelProveedores = Comercial_Estefannny.ViewModel.Proveedores; // Alias for ViewModel

namespace Comercial_Estefannny.View
{
    public partial class Proveedores : System.Windows.Controls.UserControl
    {
        private ICollectionView ProveedoresView;
        public ObservableCollection<ViewModelProveedores> ProveedoresList { get; set; } = new ObservableCollection<ViewModelProveedores>();

        public Proveedores()
        {
            InitializeComponent();

            // Cargar proveedores y configurar la vista para habilitar filtrado
            CargarProveedores();
            ProveedoresView = CollectionViewSource.GetDefaultView(ProveedoresList);
            ProveedoresListView.ItemsSource = ProveedoresView;
        }

        private void CargarProveedores()
        {
            // Limpiar la colección actual
            ProveedoresList.Clear();

            // Obtener los proveedores de la base de datos y agregarlos a la colección
            foreach (var proveedor in ViewModelProveedores.Obtenerproveedores()) // Use alias and correct static call
            {
                ProveedoresList.Add(proveedor);
            }

            // Asignar la colección al ListView
            ProveedoresListView.ItemsSource = ProveedoresList;
        }

        // Método para agregar un proveedor
        private void BotonAgregarProveedor_Click(object sender, RoutedEventArgs e)
        {
            string nombreProveedor = TextNombreProveedor.Text;
            string direccion = TextDireccionProveedor.Text;
            string telefono = TextTelefonoProveedor.Text;

            // Validar que el nombre esté ingresado
            if (string.IsNullOrEmpty(nombreProveedor))
            {
                System.Windows.MessageBox.Show("Por favor, ingrese el nombre del proveedor.");
                return;
            }

            // Crear un objeto Proveedor con los datos ingresados
           ViewModelProveedores nuevoProveedor = new ViewModelProveedores // Use alias
            {
                Nombre = nombreProveedor,
                Direccion = direccion,
                Telefono = telefono,
            };

            // Llamar al método del ViewModel para insertar el proveedor en la base de datos
            nuevoProveedor.InsertarProveedor();

            System.Windows.MessageBox.Show("Proveedor agregado exitosamente.");

            // Actualizar la lista de proveedores
            CargarProveedores();
            LimpiarCampos();
        }

        // Método para eliminar un proveedor
        private void BotonEliminarProveedor_Click(object sender, RoutedEventArgs e)
        {
            string nombreProveedor = TextNombreProveedor.Text;

            // Validar que el nombre esté ingresado
            if (string.IsNullOrEmpty(nombreProveedor))
            {
                System.Windows.MessageBox.Show("Por favor, ingrese el nombre del proveedor a eliminar.");
                return;
            }

            // Llamada al ViewModel para eliminar el proveedor
            var proveedor = ProveedoresList.FirstOrDefault(p => p.Nombre == nombreProveedor);
            if (proveedor != null)
            {
                proveedor.EliminarProveedor();
                System.Windows.MessageBox.Show("Proveedor eliminado exitosamente.");
            }
            else
            {
                System.Windows.MessageBox.Show("Proveedor no encontrado.");
            }

            // Actualizar la lista de proveedores
            CargarProveedores();
            LimpiarCampos();
        }

        // Método para editar un proveedor
        private void BotonEditarProveedor_Click(object sender, RoutedEventArgs e)
        {
            string nombreProveedor = TextNombreProveedor.Text;
            string nuevaDireccion = TextDireccionProveedor.Text;
            string nuevoTelefono = TextTelefonoProveedor.Text;

            // Validar que el nombre esté ingresado
            if (string.IsNullOrEmpty(nombreProveedor))
            {
                System.Windows.MessageBox.Show("Por favor, ingrese el nombre del proveedor.");
                return;
            }

            // Llamada al ViewModel para actualizar el proveedor
            var proveedor = ProveedoresList.FirstOrDefault(p => p.Nombre == nombreProveedor);
            if (proveedor != null)
            {
                proveedor.Direccion = nuevaDireccion;
                proveedor.Telefono = nuevoTelefono;

                proveedor.ActualizarProveedor();  // Método en tu ViewModel para actualizar el proveedor

                System.Windows.MessageBox.Show("Proveedor actualizado exitosamente.");
            }
            else
            {
                System.Windows.MessageBox.Show("Proveedor no encontrado.");
            }

            // Actualizar la lista de proveedores
            CargarProveedores();
            LimpiarCampos();
        }

        // Limpiar los campos de texto
        private void LimpiarCampos()
        {
            TextNombreProveedor.Clear();
            TextDireccionProveedor.Clear();
            TextTelefonoProveedor.Clear();
        }

        // Método para manejar la selección de un proveedor en el ListView
        private void ProveedoresListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProveedoresListView.SelectedItem != null)
            {
                ViewModelProveedores proveedorSeleccionado = (ViewModelProveedores)ProveedoresListView.SelectedItem; // Use alias

                // Rellenar los campos con los datos del proveedor seleccionado
                TextNombreProveedor.Text = proveedorSeleccionado.Nombre;
                TextDireccionProveedor.Text = proveedorSeleccionado.Direccion;
                TextTelefonoProveedor.Text = proveedorSeleccionado.Telefono;
            }
        }

        private void BotonImportarDatosProveedor_Click(object sender, RoutedEventArgs e)
        {
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
                    var proveedoresviewModel = new ViewModelProveedores(); // Use alias
                    proveedoresviewModel.ImportarProveedoresDesdeExcel(filePath);

                    System.Windows.MessageBox.Show("Proveedores importados exitosamente.");

                    // Actualizar la lista de clientes después de la importación
                    CargarProveedores();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Error al importar los datos: {ex.Message}");
                }
            }

        }
    }
}
