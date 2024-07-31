using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace AccesoDatos
{
    public class ConexionBD
    {
        protected SqlConnection sqlConBD;
        protected SqliteConnection sqlConIntegrin;

        protected SqlConnection GetConnDB()
        {
            sqlConBD = new SqlConnection( ConfigurationManager.ConnectionStrings["BdElectronicInvoice"].ToString());
            sqlConBD.Open();
            return sqlConBD;
        }

        protected SqliteConnection GetConnIntegrin(string ruta)
        {
            sqlConIntegrin = new SqliteConnection(ruta);
            sqlConIntegrin.Open();
            return sqlConIntegrin;
        }
    }
}
