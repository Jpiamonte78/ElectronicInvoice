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
                    try
                    {
                        ClienteIntegrin cliente = new ClienteIntegrin();
                        var dr = cmd.ExecuteReader();
                        while(dr.Read())
                        {
                            cliente = new ClienteIntegrin();
                            cliente.Id_Cliente_integrin = Convert.ToInt32(dr["Id_Cliente_Integrin"]);
                            cliente.Codpredio = dr["codpredio"].ToString();
                            cliente.Identificacion = dr["Identificacion"].ToString();
                            cliente.nombres = dr["nombres"].ToString();
                            cliente.apellido_1 = dr["apellido_1"].ToString();
                            cliente.apellido_2 = dr["apellido_2"].ToString();
                            cliente.direccion = dr["direccion"].ToString();
                            cliente.ciudad = dr["ciudad"].ToString();
                            cliente.departamento = dr["departamento"].ToString();
                            cliente.telefono = dr["telefono"].ToString();
                            cliente.email = dr["email"].ToString();
                            cliente.tipo_identificacion = Convert.ToInt32(dr["tipo_identificacion"]);
                            cliente.dv = Convert.ToInt32(dr["dv"]);
                            cliente.razon_social = dr["razon_social"].ToString();
                            cliente.tipo_persona = Convert.ToInt32(dr["tipo_persona"]);
                            cliente.zona_postal = dr["zona_postal"].ToString();
                            cliente.ciclo = dr["ciclo"].ToString();
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
                    try
                    {
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            string[] ident = dr["Identificacion"].ToString().Split('-');
                            cliente = new ClienteIntegrin();
                            cliente.Id_Cliente_integrin = Convert.ToInt32(dr["Id_Cliente_Integrin"]);
                            cliente.Codpredio = dr["codpredio"].ToString();
                            cliente.identificacion = Convert.ToInt32(ident[0]);
                            cliente.nombres = dr["nombres"].ToString();
                            cliente.apellido_1 = dr["apellido_1"].ToString();
                            cliente.apellido_2 = dr["apellido_2"].ToString();
                            cliente.direccion = dr["direccion"].ToString();
                            cliente.ciudad = dr["ciudad"].ToString();
                            cliente.departamento = dr["departamento"].ToString();
                            cliente.telefono = dr["telefono"].ToString();
                            cliente.email = dr["email"].ToString();
                            cliente.tipo_identificacion = Convert.ToInt32(dr["tipo_identificacion"]);
                            cliente.dv = ident.Length > 1 ? Convert.ToInt32(ident[1]):0;
                            cliente.razon_social = dr["razon_social"].ToString();
                            cliente.tipo_persona = Convert.ToInt32(dr["tipo_persona"]);
                            cliente.zona_postal = dr["zona_postal"].ToString();
                            cliente.ciclo = dr["ciclo"].ToString();
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
