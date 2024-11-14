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
                    cmd.CommandTimeout = 1800;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Consultar_pendientes");
                    cmd.Parameters.AddWithValue("id_cliente_integrin", DBNull.Value);
                    cmd.Parameters.AddWithValue("codpredio", DBNull.Value);
                    cmd.Parameters.AddWithValue("tipo_identificacion", DBNull.Value);
                    cmd.Parameters.AddWithValue("Identificacion", DBNull.Value);
                    cmd.Parameters.AddWithValue("dv", DBNull.Value);
                    cmd.Parameters.AddWithValue("tipo_persona", DBNull.Value);
                    cmd.Parameters.AddWithValue("razon_social", DBNull.Value);
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
                    cmd.Parameters.AddWithValue("tipo_identificacion", DBNull.Value);
                    cmd.Parameters.AddWithValue("Identificacion", DBNull.Value);
                    cmd.Parameters.AddWithValue("dv", DBNull.Value);
                    cmd.Parameters.AddWithValue("tipo_persona", DBNull.Value);
                    cmd.Parameters.AddWithValue("razon_social", DBNull.Value);
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

        public ClienteIntegrin ConsultarporCodpredio(string codpredio)
        {
            ClienteIntegrin cliente = new ClienteIntegrin();
            using (SqlConnection conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpCliente_integrin";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Consultar_Codpredio");
                    cmd.Parameters.AddWithValue("id_cliente_integrin", DBNull.Value);
                    cmd.Parameters.AddWithValue("codpredio", codpredio);
                    cmd.Parameters.AddWithValue("tipo_identificacion", DBNull.Value);
                    cmd.Parameters.AddWithValue("Identificacion", DBNull.Value);
                    cmd.Parameters.AddWithValue("dv", DBNull.Value);
                    cmd.Parameters.AddWithValue("tipo_persona", DBNull.Value);
                    cmd.Parameters.AddWithValue("razon_social", DBNull.Value);
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
                            cliente.nomciudad = dr["nomciudad"].ToString();
                            cliente.nomdepto = dr["nomdepto"].ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("ConsultarUnCliente: " + ex.Message, ex);
                    }
                }
            }
            return cliente;
        }

        public void Insertar_cliente(ClienteIntegrin cli)
        {
            using (SqlConnection conn = GetConnDB())
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpCliente_integrin";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Insertar");
                    cmd.Parameters.AddWithValue("id_cliente_integrin", DBNull.Value);
                    cmd.Parameters.AddWithValue("codpredio", cli.Codpredio);
                    cmd.Parameters.AddWithValue("tipo_identificacion", cli.tipo_identificacion);
                    cmd.Parameters.AddWithValue("Identificacion", cli.Identificacion);
                    cmd.Parameters.AddWithValue("dv", cli.dv);
                    cmd.Parameters.AddWithValue("tipo_persona", cli.tipo_persona);
                    cmd.Parameters.AddWithValue("razon_social", cli.Razon_social);
                    cmd.Parameters.AddWithValue("nombre_cliente", cli.Nombre_cliente);
                    cmd.Parameters.AddWithValue("apellido1_cliente", cli.Apellido1_cliente);
                    cmd.Parameters.AddWithValue("apellido2_cliente", cli.Apellido2_Cliente);
                    cmd.Parameters.AddWithValue("direccion_cliente", cli.Direccion_cliente);
                    cmd.Parameters.AddWithValue("ciudad_cliente", cli.ciudad_cliente);
                    cmd.Parameters.AddWithValue("departamento_cliente", cli.departamento_cliente);
                    cmd.Parameters.AddWithValue("telefono_cliente", cli.telefono_cliente);
                    cmd.Parameters.AddWithValue("email_cliente", cli.email_cliente);
                    cmd.Parameters.AddWithValue("ciclo", cli.ciclo);
                    cmd.Parameters.AddWithValue("uso", cli.uso);
                    cmd.Parameters.AddWithValue("Estrato", cli.estrato);
                    cmd.Parameters.AddWithValue("Nmedidor", cli.Nmedidor);
                    cmd.Parameters.AddWithValue("Matricula", cli.matricula);
                    cmd.Parameters.AddWithValue("zona_postal", cli.zona_postal);
                    cmd.Parameters.AddWithValue("resp_rut", cli.resp_rut);
                    cmd.Parameters.AddWithValue("tributos", cli.tributos);
                    cmd.Parameters.AddWithValue("actualizado", cli.actualizado);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Insertar_cliente: "+ex.Message,ex);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }
        
        public void Actualizar_cliente(ClienteIntegrin cli)
        {
            using (SqlConnection conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpCliente_integrin";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Actualizar");
                    cmd.Parameters.AddWithValue("id_cliente_integrin", cli.Id_Cliente_integrin);
                    cmd.Parameters.AddWithValue("codpredio", cli.Codpredio);
                    cmd.Parameters.AddWithValue("tipo_identificacion", cli.tipo_identificacion);
                    cmd.Parameters.AddWithValue("Identificacion", cli.Identificacion);
                    cmd.Parameters.AddWithValue("dv", string.IsNullOrEmpty(cli.dv) ? "" : cli.dv.Trim());
                    cmd.Parameters.AddWithValue("tipo_persona", cli.tipo_persona);
                    cmd.Parameters.AddWithValue("razon_social",string.IsNullOrEmpty(cli.Razon_social)?"":cli.Razon_social);
                    cmd.Parameters.AddWithValue("nombre_cliente", string.IsNullOrEmpty(cli.Nombre_cliente)?"":cli.Nombre_cliente);
                    cmd.Parameters.AddWithValue("apellido1_cliente",string.IsNullOrEmpty(cli.Apellido1_cliente)?"":cli.Apellido1_cliente);
                    cmd.Parameters.AddWithValue("apellido2_cliente", string.IsNullOrEmpty(cli.Apellido2_Cliente)?"":cli.Apellido2_Cliente);
                    cmd.Parameters.AddWithValue("direccion_cliente", string.IsNullOrEmpty(cli.Direccion_cliente) ? "" : cli.Direccion_cliente);
                    cmd.Parameters.AddWithValue("ciudad_cliente", string.IsNullOrEmpty(cli.ciudad_cliente) ? "" : cli.ciudad_cliente);
                    cmd.Parameters.AddWithValue("departamento_cliente", cli.departamento_cliente);
                    cmd.Parameters.AddWithValue("telefono_cliente", cli.telefono_cliente);
                    cmd.Parameters.AddWithValue("email_cliente", string.IsNullOrEmpty(cli.email_cliente) ? "" : cli.email_cliente);
                    cmd.Parameters.AddWithValue("ciclo", cli.ciclo);
                    cmd.Parameters.AddWithValue("uso", cli.uso);
                    cmd.Parameters.AddWithValue("Estrato", cli.estrato);
                    cmd.Parameters.AddWithValue("Nmedidor", string.IsNullOrEmpty(cli.Nmedidor) ? "" : cli.Nmedidor);
                    cmd.Parameters.AddWithValue("Matricula", string.IsNullOrEmpty(cli.matricula)?"":cli.matricula);
                    cmd.Parameters.AddWithValue("zona_postal",string.IsNullOrEmpty(cli.zona_postal)?"":cli.zona_postal);
                    cmd.Parameters.AddWithValue("resp_rut", cli.resp_rut);
                    cmd.Parameters.AddWithValue("tributos", cli.tributos);
                    cmd.Parameters.AddWithValue("actualizado", cli.actualizado);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Actualizar_cliente: " + ex.Message, ex);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public List<ClienteIntegrin> ConsultarTodos()
        {
            List<ClienteIntegrin> lcliente = new List<ClienteIntegrin>();
            using (SqlConnection conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpCliente_integrin";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 3600;
                    cmd.Parameters.AddWithValue("accion", "Consultar_todos");
                    cmd.Parameters.AddWithValue("id_cliente_integrin", DBNull.Value);
                    cmd.Parameters.AddWithValue("codpredio", DBNull.Value);
                    cmd.Parameters.AddWithValue("tipo_identificacion", DBNull.Value);
                    cmd.Parameters.AddWithValue("Identificacion", DBNull.Value);
                    cmd.Parameters.AddWithValue("dv", DBNull.Value);
                    cmd.Parameters.AddWithValue("tipo_persona", DBNull.Value);
                    cmd.Parameters.AddWithValue("razon_social", DBNull.Value);
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
                        while (dr.Read())
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
                            cliente.ciudad_cliente = dr["ciudad_cliente"].ToString().Trim();
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
                        throw new ApplicationException("ConsultarTodos: " + ex.Message, ex);
                    }
                }
            }
            return lcliente;
        }
    }
}
