using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.ComponentModel;

namespace AccesoDatos
{
    public class ADEnvio_notas:ConexionBD
    {
        public void Insertar_Envio(Envio_Notas envio)
        {
            using (SqlConnection conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpEnvio_Nota";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "insertar");
                    cmd.Parameters.AddWithValue("id_Envio_Nota", envio.id_Envio_Nota);
                    cmd.Parameters.AddWithValue("Tiponota", envio.Tiponota);
                    cmd.Parameters.AddWithValue("Numnota", envio.Numnota);
                    cmd.Parameters.AddWithValue("Codpredio", envio.Codpredio);
                    cmd.Parameters.AddWithValue("codigo_respuesta", envio.codigo_respuesta);
                    cmd.Parameters.AddWithValue("mensaje_respuesta", envio.mensaje_respuesta);
                    cmd.Parameters.AddWithValue("xml_enviado", envio.xml_enviado);
                    cmd.Parameters.AddWithValue("fecha_envio", DBNull.Value);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}
