using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades;
using System.Data.SQLite;


namespace AccesoDatos
{
    public class ADClienteIntegrin:ConexionBD
    {
        public List<ClienteIntegrin> ConsultarPendientes()
        {
            List<ClienteIntegrin> lcliente = new List<ClienteIntegrin>();
            using (SqlConnection conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpCliente_integrin";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Consultar_pendientes");
                    cmd.Parameters.AddWithValue("id_cliente_integrin", DBNull.Value);
                    cmd.Parameters.AddWithValue("codpredio", DBNull.Value);
                    cmd.Parameters.AddWithValue("Identificacion", DBNull.Value);
                    cmd.Parameters.AddWithValue("nombre_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("apellido1_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("apellido2_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("direccion_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("ciudad_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("departamento_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("telefono_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("email_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("ciclo", DBNull.Value);
                    cmd.Parameters.AddWithValue("uso", DBNull.Value);
                    cmd.Parameters.AddWithValue("Estrato", DBNull.Value);
                    cmd.Parameters.AddWithValue("Nmedidor", DBNull.Value);
                    cmd.Parameters.AddWithValue("Matricula", DBNull.Value);
                    cmd.Parameters.AddWithValue("zona_postal", DBNull.Value);
                    cmd.Parameters.AddWithValue("resp_rut", DBNull.Value);
                    cmd.Parameters.AddWithValue("tributos", DBNull.Value);
                    cmd.Parameters.AddWithValue("actualizado", DBNull.Value);
                    try
                    {
                        ClienteIntegrin cliente = new ClienteIntegrin();
                        var dr = cmd.ExecuteReader();
                        while(dr.Read())
                        {
                            cliente = new ClienteIntegrin();
                            cliente.Id_Cliente_integrin = Convert.ToInt32(dr["Id_Cliente_Integrin"]);
                            cliente.Codpredio = dr["codpredio"].ToString();
                            cliente.tipo_identificacion = Convert.ToInt16(dr["tipo_identificacion"]);
                            cliente.Identificacion = dr["Identificacion"].ToString();
                            cliente.dv = dr["dv"].ToString();
                            cliente.tipo_persona = Convert.ToInt16(dr["tipo_persona"]);
                            cliente.Nombre_cliente = dr["nombre_cliente"].ToString();
                            cliente.Apellido1_cliente = dr["apellido1_cliente"].ToString();
                            cliente.Apellido2_Cliente = dr["apellido2_cliente"].ToString();
                            cliente.Direccion_cliente = dr["direccion_cliente"].ToString();
                            cliente.ciudad_cliente= dr["ciudad_cliente"].ToString().Trim();
                            cliente.departamento_cliente = dr["departamento_cliente"].ToString().Trim();
                            cliente.telefono_cliente = dr["telefono_cliente"].ToString();
                            cliente.email_cliente = dr["email_cliente"].ToString();
                            cliente.ciclo = dr["ciclo"].ToString();
                            cliente.uso = dr["uso"].ToString();
                            cliente.estrato = dr["estrato"].ToString();
                            cliente.Nmedidor = dr["Nmedidor"].ToString();
                            cliente.matricula = dr["matricula"].ToString();
                            cliente.zona_postal = dr["zona_postal"].ToString();
                            cliente.resp_rut = dr["resp_rut"].ToString();
                            cliente.tributos = dr["tributos"].ToString();
                            cliente.actualizado = Convert.ToBoolean(dr["actualizado"]);
                            lcliente.Add(cliente);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("ConsultarPendientes: "+ex.Message,ex);
                    }
                }
            }

            return lcliente;
        }

        public ClienteIntegrin ConsultarUnCliente(int idclienteintegrin)
        {
            ClienteIntegrin cliente = new ClienteIntegrin();
            using (SqlConnection conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpCliente_integrin";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Consultar_uno");
                    cmd.Parameters.AddWithValue("id_cliente_integrin", idclienteintegrin);
                    cmd.Parameters.AddWithValue("codpredio", DBNull.Value);
                    cmd.Parameters.AddWithValue("Identificacion", DBNull.Value);
                    cmd.Parameters.AddWithValue("nombre_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("apellido1_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("apellido2_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("direccion_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("ciudad_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("departamento_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("telefono_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("email_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("ciclo", DBNull.Value);
                    cmd.Parameters.AddWithValue("uso", DBNull.Value);
                    cmd.Parameters.AddWithValue("Estrato", DBNull.Value);
                    cmd.Parameters.AddWithValue("Nmedidor", DBNull.Value);
                    cmd.Parameters.AddWithValue("Matricula", DBNull.Value);
                    cmd.Parameters.AddWithValue("zona_postal", DBNull.Value);
                    cmd.Parameters.AddWithValue("resp_rut", DBNull.Value);
                    cmd.Parameters.AddWithValue("tributos", DBNull.Value);
                    cmd.Parameters.AddWithValue("actualizado", DBNull.Value);

                    try
                    {
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            cliente = new ClienteIntegrin();
                            cliente.Id_Cliente_integrin = Convert.ToInt32(dr["Id_Cliente_Integrin"]);
                            cliente.Codpredio = dr["codpredio"].ToString();
                            cliente.tipo_identificacion = Convert.ToInt16(dr["tipo_identificacion"]);
                            cliente.Identificacion = dr["Identificacion"].ToString();
                            cliente.dv = dr["dv"].ToString();
                            cliente.tipo_persona = Convert.ToInt16(dr["tipo_persona"]);
                            cliente.Razon_social = dr["Razon_social"].ToString();
                            cliente.Nombre_cliente = dr["Nombre_cliente"].ToString();
                            cliente.Apellido1_cliente = dr["apellido1_cliente"].ToString();
                            cliente.Apellido2_Cliente = dr["apellido2_cliente"].ToString();
                            cliente.Direccion_cliente = dr["Direccion_cliente"].ToString();
                            cliente.ciudad_cliente = dr["ciudad_cliente"].ToString().Trim();
                            cliente.departamento_cliente = dr["departamento_cliente"].ToString().Trim();
                            cliente.telefono_cliente = dr["telefono_cliente"].ToString();
                            cliente.email_cliente = dr["email_cliente"].ToString();
                            cliente.zona_postal = dr["zona_postal"].ToString();
                            cliente.ciclo = dr["ciclo"].ToString();
                            cliente.uso = dr["uso"].ToString();
                            cliente.estrato = dr["estrato"].ToString();
                            cliente.Nmedidor = dr["Nmedidor"].ToString();
                            cliente.matricula = dr["matricula"].ToString();
                            cliente.zona_postal = dr["zona_postal"].ToString();
                            cliente.resp_rut = dr["resp_rut"].ToString();
                            cliente.tributos = dr["tributos"].ToString();
                            cliente.actualizado = Convert.ToBoolean(dr["actualizado"]);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("ConsultarUnCliente: "+ex.Message,ex);
                    }
                }
            }
            return cliente;
        }

        
    }
}
