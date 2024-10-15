using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data;
using System.Configuration;

namespace AccesoDatos
{
    public class ADParametrosGenerales:ConexionBD
    {
        public List<Responsabilidades> Consultar_Responsabilidades()
        {
            List<Responsabilidades> lresp = new List<Responsabilidades>();
            using (SqlConnection conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpParametrosGenerales";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Responsabilidades_Rut");
                    cmd.Parameters.AddWithValue("rutadb", DBNull.Value);
                    try
                    {
                        Responsabilidades res = new Responsabilidades();
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            res = new Responsabilidades();
                            res.codigo = dr["codigo"].ToString();
                            res.descripcion = dr["descripcion"].ToString();
                            lresp.Add(res);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Consultar_Responsabilidades: " + ex.Message, ex);
                    }
                    finally { 
                        conn.Close();
                    }
                }
            }
            return lresp;
        }
        public List<Tributos> consultarTributos()
        {
            List<Tributos> ltributos = new List<Tributos>();
            using (SqlConnection conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpParametrosGenerales";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Tributos");
                    cmd.Parameters.AddWithValue("rutadb", DBNull.Value);
                    try
                    {
                        Tributos trib = new Tributos();
                        var dr = cmd.ExecuteReader();
                        while (dr.Read()) {
                            trib = new Tributos();
                            trib.codigo = dr["codigo"].ToString() ;
                            trib.nombre = dr["nombre"].ToString();
                            trib.descripcion = dr["descripcion"].ToString();
                            ltributos.Add(trib);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("consultarTributos: "+ex.Message, ex );
                    } 
                    finally { conn.Close(); }
                }
            }
            return ltributos;
        }
    }
}
