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
    public class ADEnvio_Factura:ConexionBD
    {
       public List<Envio_Factura> Consultar_Envio(string Numfactura,string Codpredio)
       {
            List<Envio_Factura> lenvios = new List<Envio_Factura>();
            using (SqlConnection conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpEnvio_Factura";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Consultar");
                    cmd.Parameters.AddWithValue("id_Envio_Factura", DBNull.Value);
                    cmd.Parameters.AddWithValue("Numfactura", Numfactura);
                    cmd.Parameters.AddWithValue("Codpredio", Codpredio);
                    cmd.Parameters.AddWithValue("codigo_respuesta", DBNull.Value);
                    cmd.Parameters.AddWithValue("mensaje_respuesta", DBNull.Value);
                    cmd.Parameters.AddWithValue("xml_enviado", DBNull.Value);
                    cmd.Parameters.AddWithValue("fecha_envio", DBNull.Value);
                    try
                    {
                        Envio_Factura envio = new Envio_Factura();
                        var dr = cmd.ExecuteReader();
                        while(dr.Read())
                        {
                            envio = new Envio_Factura();
                            envio.id_Envio_Factura = Convert.ToInt32(dr["id_Envio_Factura"]);
                            envio.Numfactura = dr["Numfactura"].ToString();
                            envio.Codpredio = dr["Codpredio"].ToString();
                            envio.codigo_respuesta = dr["codig_respuesta"].ToString();
                            envio.mensaje_respuesta = dr["mensaje_respuesta"].ToString();
                            envio.xml_enviado = dr["xml_enviado"].ToString();
                            lenvios.Add(envio);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return lenvios;
       }

        public void insertar_respuesta(Envio_Factura envio)
        {
            using (SqlConnection conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpEnvio_Factura";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "insertar");
                    cmd.Parameters.AddWithValue("id_Envio_Factura", envio.id_Envio_Factura);
                    cmd.Parameters.AddWithValue("Numfactura", envio.Numfactura);
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

        public List<Envio_Factura> consultar_fecha(DateTime fecha)
        {
            List<Envio_Factura> lenvios = new List<Envio_Factura>();
            using (SqlConnection conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpEnvio_Factura";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Consultar_Fecha");
                    cmd.Parameters.AddWithValue("id_Envio_Factura", DBNull.Value);
                    cmd.Parameters.AddWithValue("Numfactura", DBNull.Value);
                    cmd.Parameters.AddWithValue("Codpredio", DBNull.Value);
                    cmd.Parameters.AddWithValue("codigo_respuesta", DBNull.Value);
                    cmd.Parameters.AddWithValue("mensaje_respuesta", DBNull.Value);
                    cmd.Parameters.AddWithValue("xml_enviado", DBNull.Value);
                    cmd.Parameters.AddWithValue("fecha_envio", fecha);
                    try
                    {
                        Envio_Factura envio = new Envio_Factura();
                        var dr = cmd.ExecuteReader();
                        while(dr.Read())
                        {
                            envio = new Envio_Factura();
                            envio.id_Envio_Factura = Convert.ToInt32(dr["id_Envio_Factura"]);
                            envio.Numfactura = dr["Numfactura"].ToString();
                            envio.Codpredio = dr["Codpredio"].ToString();
                            envio.fecha_envio = Convert.ToDateTime(dr["fecha_envio"]);
                            envio.codigo_respuesta = dr["codigo_respuesta"].ToString();
                            envio.mensaje_respuesta = dr["mensaje_respuesta"].ToString() ;
                            envio.fecha = Convert.ToDateTime(dr["fecha"]);
                            envio.valor_total = Convert.ToDecimal(dr["valor_total"]);
                            lenvios.Add(envio);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Consultar_fecha: "+ex.Message,ex);
                    }
                }
            }
            return lenvios;
        }
    }
}
