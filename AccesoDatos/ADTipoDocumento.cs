using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data;


namespace AccesoDatos
{
    public class ADTipoDocumento:ConexionBD
    {
        public List<TipoDocumento> tipoDocumentos()
        {
            List<TipoDocumento> ltipodocumento = new List<TipoDocumento>();
            using (SqlConnection conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpTipoDocumento";
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        TipoDocumento tipo = new TipoDocumento();
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            tipo = new TipoDocumento();
                            tipo.codigo = Convert.ToInt32(dr["codigo"]);
                            tipo.Descripcion = dr["descripcion"].ToString();
                            ltipodocumento.Add(tipo);   
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("tipoDocumentos: " + ex.Message, ex);
                    }
                    finally { 
                        conn.Close();
                    }
                }
            }
            return ltipodocumento;
        }
    }
}
