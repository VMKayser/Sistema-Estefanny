using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercial_Estefannny.ViewModel
{
    public class ProductoCompra : INotifyPropertyChanged
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }

        public string Variante { get; set; }
        public decimal Total => (Cantidad * Precio)-Descuento;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ProductoCompra(int idProducto, string nombreProducto, int cantidad, decimal precio, decimal descuento)
        {
            IdProducto = idProducto;
            NombreProducto = nombreProducto;
            Cantidad = cantidad;
            Precio = precio;
            Descuento = descuento;
        }
    }

}
