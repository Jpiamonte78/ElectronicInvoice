using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public class ParametrosGeneralesAD:ConexionBD
    {
        public string ConsultarRutaDb()
        {
            string ruta = "";
            SqlConnection con = GetConnDB();
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "SpParametrosGenerales";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("accion", "Consulta");
                cmd.Parameters.AddWithValue("rutadb", DBNull.Value);
                try
                {
                    ruta = cmd.ExecuteScalar().ToString();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("ConsultaRutaDb: "+ex.Message,ex);
                }
                finally
                {
                    con.Close();
                }
            }
            return ruta;
        }

        public void ActualizarRuta(string ruta)
        {
            using (SqlConnection con = GetConnDB())
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SpParametrosGenerales";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Actualizar");
                    cmd.Parameters.AddWithValue("rutadb", ruta);
                    try
                    {
                        cmd.ExecuteNonQuery();  
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("ActualizarRuta: "+ex.Message,ex);
                    }
                    finally { con.Close(); }
                }
            }
        }
    }
}
