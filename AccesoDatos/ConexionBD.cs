using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace AccesoDatos
{
    public class ConexionBD
    {
        protected SqlConnection sqlConBD;
        protected SQLiteConnection sqlConIntegrin;

        protected SqlConnection GetConnDB()
        {
            sqlConBD = new SqlConnection( ConfigurationManager.ConnectionStrings["BdElectronicInvoice"].ToString());
            sqlConBD.Open();
            return sqlConBD;
        }

        protected SQLiteConnection GetConnIntegrin(string ruta)
        {

            sqlConIntegrin = new SQLiteConnection("Data Source="+ruta);
            sqlConIntegrin.Open();
            return sqlConIntegrin;
        }
    }
}
