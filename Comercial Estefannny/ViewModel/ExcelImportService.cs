using ClosedXML.Excel;
using System.Collections.Generic;
using System.IO;
using Comercial_Estefannny.ViewModel;
using System.Linq;
using Comercial_Estefannny.View;

namespace Comercial_Estefannny.Services
{
    public class ExcelImportService
    {
        public List<ClientesC> ImportarClientesDesdeExcel(string filePath)
        {
            var clientes = new List<ClientesC>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); // Primer hoja del archivo Excel
                foreach (var row in worksheet.RangeUsed().RowsUsed().Skip(1)) // Saltar encabezados
                {
                    var cliente = new ClientesC
                    {
                        Nombre = row.Cell(1).GetString(),
                        Direccion = row.Cell(2).GetString(),
                        Telefono = row.Cell(3).GetString(),
                        Deuda = row.Cell(4).GetDouble(),
                    };
                    clientes.Add(cliente);
                }
            }
            return clientes;
        }

        public List<Comercial_Estefannny.ViewModel.Proveedores> ImportarProveedoresDesdeExcel(string filePath)
        {
            var proveedoresList = new List<Comercial_Estefannny.ViewModel.Proveedores>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); // Primer hoja del archivo Excel
                foreach (var row in worksheet.RangeUsed().RowsUsed().Skip(1)) // Saltar encabezados
                {
                    var proveedor = new Comercial_Estefannny.ViewModel.Proveedores
                    {
                        Nombre = row.Cell(1).GetString(),
                        Direccion = row.Cell(2).GetString(),
                        Telefono = row.Cell(3).GetString(),
                    };
                    proveedoresList.Add(proveedor);
                }
            }

            return proveedoresList;
        }

        public List<Producto> ImportarProductosDesdeExcel(string filePath)
        {
            var productos = new List<Producto>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); // Primer hoja del archivo Excel
                foreach (var row in worksheet.RangeUsed().RowsUsed().Skip(1)) // Saltar encabezados
                {
                    var producto = new Producto
                    {
                        NombreProducto = row.Cell(1).GetString(),
                        NombreMarca = row.Cell(2).GetString(),
                        NombreCategoria = row.Cell(3).GetString(),
                        Variante = row.Cell(4).GetString(),
                        CodigoBarras = row.Cell(5).GetString(),
                        PrecioCompra = row.Cell(6).GetValue<decimal>(),
                        PrecioVenta = row.Cell(7).GetValue<decimal>(),
                        Cantidad = row.Cell(8).IsEmpty() ? 0 : row.Cell(8).GetValue<int>(),
                    };
                    productos.Add(producto);
                }
            }

            return productos;
        }

    }
}
