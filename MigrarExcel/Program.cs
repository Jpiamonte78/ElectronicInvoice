using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using ExcelDataReader;
using System.Data;

namespace MigrarExcel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string excelFilePath = @"C:\Users\jjpc5\Downloads\Clientes (1).xlsx";
            string connectionString = "Data Source=PC-JAVIER\\DEVELOPER;Initial Catalog=DbFacturaElectronica;user id=sa; password= Superu1";

            // Leer datos desde Excel
            DataTable dataTable = ReadExcelFile(excelFilePath);

            // Migrar datos a SQL Server
            MigrateDataToSqlServer(dataTable, connectionString);
        }
        static DataTable ReadExcelFile(string path)
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                    return result.Tables[0]; // Obteniendo la primera tabla de Excel
                }
            }
        }

        static void MigrateDataToSqlServer(DataTable dataTable, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "dbo.clientesWO";
                    bulkCopy.ColumnMappings.Add("Tipo_de_Identificacion", "Tipo_de_Identificacion");
                    bulkCopy.ColumnMappings.Add("Identificacion", "Identificacion");
                    bulkCopy.ColumnMappings.Add("Digito_de_verificacion", "Digito_de_verificacion");
                    bulkCopy.ColumnMappings.Add("Codigo_tercero", "Codigo_tercero");
                    bulkCopy.ColumnMappings.Add("Email", "Email");
                    bulkCopy.ColumnMappings.Add("Propiedades", "Propiedades");
                    bulkCopy.ColumnMappings.Add("Tipo_de_Contribuyente", "Tipo_de_Contribuyente");
                    bulkCopy.ColumnMappings.Add("tipo_persona", "tipo_persona");
                    bulkCopy.WriteToServer(dataTable);
                }
            }
        }
    }

    
}
