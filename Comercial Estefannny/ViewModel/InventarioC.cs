using Comercial_Estefanny.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercial_Estefannny.ViewModel
{
    public class InventarioC : ViewModelbase
    {
        private ObservableCollection<Producto> _productos;
        public ObservableCollection<Producto> Productos
        {
            get { return _productos; }
            set
            {
                _productos = value;
                OnPropertyChanged(nameof(Productos));
            }
        }

        public InventarioC()
        {
            // Inicializa la colección de productos
            Productos = new ObservableCollection<Producto>();
            
        }

   

        // Método para agregar un nuevo producto
        public void AgregarProducto(int idCategoria, string nombreProducto, decimal precioVenta, decimal precioCompra, int cantidad)
        {
            // Crear nuevo producto
            var nuevoProducto = new Producto
            {
                IdCategoria = idCategoria,
                NombreProducto = nombreProducto,
                PrecioVenta = precioVenta,
                PrecioCompra = precioCompra,
                Cantidad = cantidad
            };

            // Aquí iría el código para insertar el producto en la base de datos

            // Si todo está bien, agregarlo a la colección para actualizar la vista
            Productos.Add(nuevoProducto);
            OnPropertyChanged(nameof(Productos));
        }

        // Método para eliminar un producto
        public void EliminarProducto(Producto producto)
        {
            // Aquí iría el código para eliminar el producto de la base de datos

            // Si todo está bien, eliminarlo de la colección para actualizar la vista
            Productos.Remove(producto);
            OnPropertyChanged(nameof(Productos));
        }

        // Método para editar un producto
        public void EditarProducto(int idProducto, string nombreProducto, decimal precioVenta, decimal precioCompra, int cantidad)
        {
            var producto = Productos.FirstOrDefault(p => p.IdProducto == idProducto);
            if (producto != null)
            {
                // Actualizar las propiedades del producto
                producto.NombreProducto = nombreProducto;
                producto.PrecioVenta = precioVenta;
                producto.PrecioCompra = precioCompra;
                producto.Cantidad = cantidad;

                // Aquí iría el código para actualizar el producto en la base de datos

                OnPropertyChanged(nameof(Productos));
            }
        }

        // Método para importar productos desde un archivo Excel
    
    }
}
