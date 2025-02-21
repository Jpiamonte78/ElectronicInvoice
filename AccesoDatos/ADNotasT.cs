using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace AccesoDatos
{
    public class ADNotasT:ConexionBD
    {
        public List<NotasT> Consultar_Notas()
        {
            List<NotasT> lnotas = new List<NotasT>();
            using (SqlConnection conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpNotasT";
                    cmd.CommandTimeout = 3600;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Consultar");
                    cmd.Parameters.AddWithValue("Id_nota", DBNull.Value);
                    cmd.Parameters.AddWithValue("ciclo", DBNull.Value);
                    cmd.Parameters.AddWithValue("anio", DBNull.Value);
                    cmd.Parameters.AddWithValue("periodo", DBNull.Value);
                    cmd.Parameters.AddWithValue("numfact", DBNull.Value);
                    cmd.Parameters.AddWithValue("codpredio", DBNull.Value);
                    cmd.Parameters.AddWithValue("valor_mod", DBNull.Value);
                    cmd.Parameters.AddWithValue("fecha", DBNull.Value);
                    try
                    {
                        NotasT nota = new NotasT();
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            nota = new NotasT();
                            nota.id_nota = Convert.ToInt32(dr["Id_nota"]);
                            nota.ciclo = dr["ciclo"].ToString();
                            nota.prefijo = dr["Prefijo"].ToString();
                            nota.anio = dr["anio"].ToString();
                            nota.periodo = dr["periodo"].ToString();
                            nota.fechainiperiodo = dr["fechainiperiodo"].ToString();
                            nota.fechafinperiodo = dr["fechafinperiodo"].ToString();
                            nota.numfact = dr["numfact"].ToString();
                            nota.codpredio = dr["codpredio"].ToString();
                            nota.valor_mod = Convert.ToDecimal(dr["valor_mod"]);
                            nota.Id_Cliente_integrin = Convert.ToInt32(dr["Id_Cliente_Integrin"]);
                            nota.Codpredio = dr["codpredio"].ToString();
                            nota.tipo_identificacion = Convert.ToInt16(dr["tipo_identificacion"]);
                            nota.Identificacion = dr["Identificacion"].ToString().Trim();
                            nota.dv = dr["dv"].ToString();
                            nota.tipo_persona = Convert.ToInt16(dr["tipo_persona"]);
                            nota.Razon_social = dr["Razon_social"].ToString();
                            nota.Nombre_cliente = dr["Nombre_cliente"].ToString();
                            nota.Apellido1_cliente = dr["apellido1_cliente"].ToString();
                            nota.Apellido2_Cliente = dr["apellido2_cliente"].ToString();
                            nota.Direccion_cliente = dr["Direccion_cliente"].ToString();
                            nota.ciudad_cliente = dr["ciudad_cliente"].ToString().Trim();
                            nota.departamento_cliente = dr["departamento_cliente"].ToString().Trim();
                            nota.telefono_cliente = dr["telefono_cliente"].ToString();
                            nota.email_cliente = dr["email_cliente"].ToString();
                            nota.zona_postal = dr["zona_postal"].ToString();
                            nota.ciclo = dr["ciclo"].ToString();
                            nota.uso = dr["uso"].ToString();
                            nota.estrato = dr["estrato"].ToString();
                            nota.Nmedidor = dr["Nmedidor"].ToString();
                            nota.matricula = dr["matricula"].ToString();
                            nota.zona_postal = dr["zona_postal"].ToString();
                            nota.resp_rut = dr["resp_rut"].ToString();
                            nota.tributos = dr["tributos"].ToString();
                            nota.actualizado = Convert.ToBoolean(dr["actualizado"]);
                            nota.nomciudad = dr["nomciudad"].ToString();
                            nota.nomdepto = dr["nomdepto"].ToString();
                            nota.mensaje = dr["mensaje"].ToString();
                            nota.UsoTarifa = dr["UsoTarifa"].ToString();
                            nota.EstratoTarifa = dr["EstratoTarifa"].ToString();
                            nota.Numfactura = dr["Numfactura"].ToString();
                            if (!string.IsNullOrEmpty(dr["fecha_envio"].ToString()))
                                nota.fecha_envio = Convert.ToDateTime(dr["fecha_envio"]);
                            else
                                nota.fecha_envio = null;
                            nota.NumeroNota = Convert.ToInt32(dr["NumeroNota"]);
                            nota.prefijoNota = dr["prefijoNota"].ToString();
                            nota.codigo_respuesta = dr["codigo_respuesta"].ToString();
                            nota.id_Envio_Nota = Convert.ToInt32(dr["id_Envio_Nota"]);
                            lnotas.Add(nota);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Consultar_Notas: " + ex.Message, ex);
                    }
                }
            }
            return lnotas;
        }
    }
}
