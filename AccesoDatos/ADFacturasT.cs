using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SQLite;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.Core.Metadata.Edm;

namespace AccesoDatos
{
    public class ADFacturasT:ConexionBD
    {
        public int Consultar_FacturasTotal(string ruta)
        {
            int reg = 0;
            List<FacturasT> lfacturas = new List<FacturasT>();
            SQLiteDataReader dr;
            SQLiteConnection sqlcon = new SQLiteConnection();
            try
            {
                sqlcon = GetConnIntegrin(ruta);
                string query = "select ciclo,anio, periodo,numfact,codpredio,valor_neto,fecha,codigobarras,terminal,atraso from BdFacturasT where valor_neto<>0 and terminal is NOT NULL";
                SQLiteCommand cmd = new SQLiteCommand(query, sqlcon);
                dr = cmd.ExecuteReader();
                FacturasT factura = new FacturasT();
                DateTime fechaant = DateTime.Now;
                string feclimant = "";
                string feclim = "";
                string ciclo = "";
                while (dr.Read())
                {
                    fechaant = (!string.IsNullOrEmpty(dr["terminal"].ToString())) ? Convert.ToDateTime(dr["fecha"]) : fechaant;
                    if (dr["codigobarras"].ToString().Contains("(") && dr["codigobarras"].ToString().Length > 25)
                    {
                        feclimant = dr["codigobarras"].ToString().Substring(dr["codigobarras"].ToString().Length - 8);
                        feclim = dr["codigobarras"].ToString().Substring(dr["codigobarras"].ToString().Length - 8);
                    }
                    else
                    {
                        feclim = feclimant;
                    }
                    ciclo = dr["ciclo"].ToString();
                    factura = new FacturasT();
                    factura.ciclo = dr["ciclo"].ToString();
                    factura.anio = Convert.ToInt32(dr["anio"]);
                    factura.periodo = dr["periodo"].ToString();
                    factura.numfact = dr["numfact"].ToString();
                    factura.codpredio = dr["codpredio"].ToString();
                    factura.valor_total = Convert.ToDecimal(dr["valor_neto"]);
                    factura.fecha = fechaant;
                    factura.fecha_limite = Convert.ToDateTime(feclim.Substring(4, 4) + '-' + feclim.Substring(2, 2) + '-' + feclim.Substring(0, 2));
                    factura.atraso = Convert.ToInt16(dr["atraso"]);
                    lfacturas.Add(factura);
                }
                reg += InsertarFacturasT(lfacturas);
                new ADperiodosCiclo().consultarperiodosCiclo(ciclo, ruta);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Se ha presentado un error en Consultar_FacturasTotal "+ ex.Message,ex);
            }
            finally
            {
                sqlcon.Close();
            }
            return reg;
        }

        private int InsertarFacturasT(List<FacturasT> lfacturaT)
        {
            int reg = 0;
            List<FacturasT> factintegrin = lfacturaT;
            SqlConnection sqlconn = GetConnDB();
            foreach(FacturasT fact in factintegrin)
            {
                using (SqlCommand cmd = new SqlCommand("SpFacturasT", sqlconn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "insertar");
                    cmd.Parameters.AddWithValue("id_factura", DBNull.Value);
                    cmd.Parameters.AddWithValue("ciclo", fact.ciclo);
                    cmd.Parameters.AddWithValue("anio", fact.anio);
                    cmd.Parameters.AddWithValue("periodo", fact.periodo);
                    cmd.Parameters.AddWithValue("numfact", fact.numfact);
                    cmd.Parameters.AddWithValue("codpredio", fact.codpredio);
                    cmd.Parameters.AddWithValue("valor_total", fact.valor_total);
                    cmd.Parameters.AddWithValue("fecha", fact.fecha);
                    cmd.Parameters.AddWithValue("fecha_limite", fact.fecha_limite);
                    cmd.Parameters.AddWithValue("atraso", fact.atraso);
                    try
                    {
                        reg += Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Se ha presentado un error en InsertarFacturasT: " + ex.Message, ex);
                    }
                }
            }
            sqlconn.Close();

            return reg;
        }

        public List<FacturasT> Consultar_Facturas()
        {
            List<FacturasT> lfacturas = new List<FacturasT>();
            using (SqlConnection conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpFacturasT";
                    cmd.CommandTimeout = 3600;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Consultar");
                    cmd.Parameters.AddWithValue("id_factura", DBNull.Value);
                    cmd.Parameters.AddWithValue("ciclo", DBNull.Value);
                    cmd.Parameters.AddWithValue("anio", DBNull.Value);
                    cmd.Parameters.AddWithValue("periodo", DBNull.Value);
                    cmd.Parameters.AddWithValue("numfact", DBNull.Value);
                    cmd.Parameters.AddWithValue("codpredio", DBNull.Value);
                    cmd.Parameters.AddWithValue("valor_total", DBNull.Value);
                    cmd.Parameters.AddWithValue("fecha", DBNull.Value);
                    cmd.Parameters.AddWithValue("fecha_limite", DBNull.Value);
                    cmd.Parameters.AddWithValue("atraso", DBNull.Value);
                    try
                    {
                        FacturasT fact = new FacturasT();
                        var dr = cmd.ExecuteReader();
                        while(dr.Read())
                        {
                            fact = new FacturasT();
                            fact.id_factura = Convert.ToInt32(dr["id_factura"]);
                            fact.ciclo = dr["ciclo"].ToString();
                            fact.Prefijo = dr["Prefijo"].ToString();
                            fact.anio = Convert.ToInt32(dr["anio"]);
                            fact.periodo = dr["periodo"].ToString();
                            fact.fechainiperiodo = dr["fechainiperiodo"].ToString();
                            fact.fechafinperiodo = dr["fechafinperiodo"].ToString();
                            fact.numfact = dr["numfact"].ToString();
                            fact.codpredio = dr["codpredio"].ToString();
                            fact.valor_total = Convert.ToDecimal(dr["valor_total"]);
                            fact.fecha = Convert.ToDateTime(dr["fecha"]);
                            fact.fecha_limite = Convert.ToDateTime(dr["fecha_limite"]);
                            fact.Id_Cliente_integrin = Convert.ToInt32(dr["Id_Cliente_Integrin"]);
                            fact.Codpredio = dr["codpredio"].ToString();
                            fact.tipo_identificacion = Convert.ToInt16(dr["tipo_identificacion"]);
                            fact.Identificacion = dr["Identificacion"].ToString().Trim();
                            fact.dv = dr["dv"].ToString();
                            fact.tipo_persona = Convert.ToInt16(dr["tipo_persona"]);
                            fact.Razon_social = dr["Razon_social"].ToString();
                            fact.Nombre_cliente = dr["Nombre_cliente"].ToString();
                            fact.Apellido1_cliente = dr["apellido1_cliente"].ToString();
                            fact.Apellido2_Cliente = dr["apellido2_cliente"].ToString();
                            fact.Direccion_cliente = dr["Direccion_cliente"].ToString();
                            fact.ciudad_cliente = dr["ciudad_cliente"].ToString().Trim();
                            fact.departamento_cliente = dr["departamento_cliente"].ToString().Trim();
                            fact.telefono_cliente = dr["telefono_cliente"].ToString();
                            fact.email_cliente = dr["email_cliente"].ToString();
                            fact.zona_postal = dr["zona_postal"].ToString();
                            fact.ciclo = dr["ciclo"].ToString();
                            fact.uso = dr["uso"].ToString();
                            fact.estrato = dr["estrato"].ToString();
                            fact.Nmedidor = dr["Nmedidor"].ToString();
                            fact.matricula = dr["matricula"].ToString();
                            fact.zona_postal = dr["zona_postal"].ToString();
                            fact.resp_rut = dr["resp_rut"].ToString();
                            fact.tributos = dr["tributos"].ToString();
                            fact.actualizado = Convert.ToBoolean(dr["actualizado"]);
                            fact.nomciudad = dr["nomciudad"].ToString();
                            fact.nomdepto = dr["nomdepto"].ToString();
                            fact.mensaje = dr["mensaje"].ToString();
                            fact.UsoTarifa = dr["UsoTarifa"].ToString();
                            fact.EstratoTarifa = dr["EstratoTarifa"].ToString();
                            fact.atraso = Convert.ToInt16(dr["atraso"]);
                            lfacturas.Add(fact);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Consultar_Facturas: "+ex.Message,ex);
                    }
                }
            }
            return lfacturas;
        }
    }
}
