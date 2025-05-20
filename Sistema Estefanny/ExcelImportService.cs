using ClosedXML.Excel;
using System.Collections.Generic;
using System.IO;

namespace Comercial_Estefanny.Services
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
    }
}
