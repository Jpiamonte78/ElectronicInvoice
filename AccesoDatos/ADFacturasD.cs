using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Entidades;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace AccesoDatos
{
    public class ADFacturasD:ConexionBD
    {
        CultureInfo culture = new CultureInfo("en-US");
        public int Consultar_detalles(string ruta)
        {
            int reg = 0;
            List<FacturasD> ldetalle = new List<FacturasD>();
            SQLiteDataReader dr;
            SQLiteConnection con = new SQLiteConnection();
            try
            {
                con = GetConnIntegrin(ruta);
                string query = "select a.ciclo,a.anio,a.periodo,a.numfac,a.codpredio,a.codigo_c,b.nombre_c,a.valor from BdFacturasD a inner join BdConceptos b on a.codigo_c=b.codigo_c";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                dr = cmd.ExecuteReader();
                FacturasD detalle = new FacturasD();
                while (dr.Read())
                {
                    detalle = new FacturasD();
                    detalle.ciclo = dr["ciclo"].ToString();
                    detalle.anio = Convert.ToInt32(dr["anio"]);
                    detalle.periodo = dr["periodo"].ToString();
                    detalle.numfac = dr["numfac"].ToString();
                    detalle.codpredio = dr["codpredio"].ToString();
                    detalle.codigo_c = dr["codigo_c"].ToString();
                    detalle.nombre_c = dr["nombre_c"].ToString();
                    detalle.valor = Convert.ToDecimal(dr["valor"],culture);
                    ldetalle.Add(detalle);
                }
                reg = InsertarFacturasD(ldetalle);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Se ha presentado error en Consultar_detalles: " + ex.Message, ex);
            }
            finally
            {
                con.Close();
            }
            return reg;
        }

        private int InsertarFacturasD(List<FacturasD> lfacturaD)
        {
            int reg = 0;
            List<FacturasD> factintegrin = lfacturaD;
            SqlConnection sqlconn = GetConnDB();
            foreach (FacturasD fact in factintegrin)
            {
                using (SqlCommand cmd = new SqlCommand("[SpFacturasD]", sqlconn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "insertar");
                    cmd.Parameters.AddWithValue("id_facturaD", DBNull.Value);
                    cmd.Parameters.AddWithValue("ciclo", fact.ciclo);
                    cmd.Parameters.AddWithValue("anio", fact.anio);
                    cmd.Parameters.AddWithValue("periodo", fact.periodo);
                    cmd.Parameters.AddWithValue("numfac", fact.numfac);
                    cmd.Parameters.AddWithValue("codpredio", fact.codpredio);
                    cmd.Parameters.AddWithValue("codigo_c", fact.codigo_c);
                    cmd.Parameters.AddWithValue("nombre_c",fact.nombre_c);
                    cmd.Parameters.AddWithValue("valor", fact.valor);
                    try
                    {
                        reg += cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Se ha presentado un error en InsertarFacturasD: " + ex.Message, ex);
                    }
                }
            }
            sqlconn.Close();

            return reg;
        }

        public List<FacturasD> Consultar_Detalle(string ciclo, string periodo, int anio, string codpredio)
        {
            List<FacturasD> ldetalle = new List<FacturasD>();
            using (var conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpFacturasD";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Consultar");
                    cmd.Parameters.AddWithValue("id_facturaD", DBNull.Value);
                    cmd.Parameters.AddWithValue("ciclo", ciclo);
                    cmd.Parameters.AddWithValue("anio", anio);
                    cmd.Parameters.AddWithValue("periodo", periodo);
                    cmd.Parameters.AddWithValue("numfac", DBNull.Value);
                    cmd.Parameters.AddWithValue("codpredio", codpredio);
                    cmd.Parameters.AddWithValue("codigo_c", DBNull.Value);
                    cmd.Parameters.AddWithValue("nombre_c", DBNull.Value);
                    cmd.Parameters.AddWithValue("valor", DBNull.Value);
                    try
                    {
                        FacturasD Dfact = new FacturasD();
                        var dr = cmd.ExecuteReader();
                        while(dr.Read())
                        {
                            Dfact = new FacturasD();
                            Dfact.ciclo = dr["ciclo"].ToString();
                            Dfact.periodo = dr["periodo"].ToString();
                            Dfact.anio = Convert.ToInt32(dr["anio"]);
                            Dfact.codpredio = dr["codpredio"].ToString();
                            Dfact.codigo_c = dr["codigo_c"].ToString();
                            Dfact.nombre_c = dr["nombre_c"].ToString();
                            Dfact.valor = Convert.ToDecimal(dr["valor"]);
                            Dfact.consumo = Convert.ToInt32(dr["consumo"]);
                            ldetalle.Add(Dfact);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Consultar_Detalle: "+ex.Message,ex);
                    }
                }
            }
            return ldetalle;
        }
    }
}
