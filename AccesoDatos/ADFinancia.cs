using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class ADFinancia:ConexionBD
    {
        CultureInfo culture = new CultureInfo("en-US");

        public int Consultar_financia(string ruta)
        {
            int reg = 0;
            List<Financia> lfinancia = new List<Financia>();
            SQLiteDataReader dr;
            SQLiteConnection con = new SQLiteConnection();
            try
            {
                string query = "select ciclo, anio,periodo,codigo_p,codigo_c,valor_c,valor_cu,cuotas,cuotas_pa from BdFinancia where cuotas_pa>0";
                using (con = GetConnIntegrin(ruta))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                    {
                        dr = cmd.ExecuteReader();
                        Financia financia = new Financia();
                        while (dr.Read())
                        {
                            financia = new Financia();
                            financia.ciclo = dr["ciclo"].ToString();
                            financia.anio = dr["anio"].ToString();
                            financia.periodo = dr["periodo"].ToString();
                            financia.codigo_p = dr["codigo_p"].ToString();
                            financia.codigo_c = dr["codigo_c"].ToString();
                            financia.valor_c = Convert.ToDecimal(dr["valor_c"]);
                            financia.valor_cu = Convert.ToDecimal(dr["valor_cu"]);
                            financia.cuotas = Convert.ToInt16(dr["cuotas"]);
                            financia.cuotas_pa = Convert.ToInt16(dr["cuotas_pa"]);
                            lfinancia.Add(financia);
                        }
                        reg = insertar_Financia(lfinancia);
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                throw new ApplicationException("Se ha presentado error en Consultar_detalles: " + ex.Message, ex);
            }
            return reg;
        }

        private int insertar_Financia(List<Financia> lfinancia)
        {
            int reg = 0;
            List<Financia> financia = lfinancia;
            SqlConnection sqlconn = GetConnDB();
            foreach (Financia finan in financia)
            {
                using (SqlCommand cmd = new SqlCommand("SpFinancia", sqlconn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "insertar");
                    cmd.Parameters.AddWithValue("ciclo", finan.ciclo);
                    cmd.Parameters.AddWithValue("anio", finan.anio);
                    cmd.Parameters.AddWithValue("periodo", finan.periodo);
                    cmd.Parameters.AddWithValue("codigo_p", finan.codigo_p);
                    cmd.Parameters.AddWithValue("codigo_c", finan.codigo_c);
                    cmd.Parameters.AddWithValue("valor_c", finan.valor_c);
                    cmd.Parameters.AddWithValue("valor_cu", finan.valor_cu);
                    cmd.Parameters.AddWithValue("cuotas", finan.cuotas);
                    cmd.Parameters.AddWithValue("cuotas_pa", finan.cuotas_pa);
                    try
                    {
                        reg += cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Se ha presentado un error en insertar_Financia: " + ex.Message, ex);
                    }
                }
            }
            sqlconn.Close();
            return reg;
        }

        public List<Financia> Consultar_Financiacion(string ciclo, string codigop)
        {
            List<Financia> lfinancia = new List<Financia>();
            using (SqlConnection conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpFinancia";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "consultar");
                    cmd.Parameters.AddWithValue("ciclo", ciclo);
                    cmd.Parameters.AddWithValue("anio", DBNull.Value);
                    cmd.Parameters.AddWithValue("periodo", DBNull.Value);
                    cmd.Parameters.AddWithValue("codigo_p", codigop);
                    cmd.Parameters.AddWithValue("codigo_c", DBNull.Value);
                    cmd.Parameters.AddWithValue("valor_c", DBNull.Value);
                    cmd.Parameters.AddWithValue("valor_cu", DBNull.Value);
                    cmd.Parameters.AddWithValue("cuotas", DBNull.Value);
                    cmd.Parameters.AddWithValue("cuotas_pa", DBNull.Value);
                    try
                    {
                        Financia finan = new Financia();
                        var dr = cmd.ExecuteReader();
                        while(dr.Read())
                        {
                            finan = new Financia();
                            finan.ciclo = dr["ciclo"].ToString();
                            finan.anio = dr["anio"].ToString();
                            finan.periodo = dr["periodo"].ToString();
                            finan.codigo_p = dr["codigo_p"].ToString();
                            finan.codigo_c = dr["codigo_c"].ToString();
                            finan.valor_c = Convert.ToDecimal(dr["valor_c"]);
                            finan.valor_cu = Convert.ToDecimal(dr["valor_cu"]);
                            finan.cuotas = Convert.ToInt16(dr["cuotas"]);
                            finan.cuotas_pa = Convert.ToInt16(dr["cuotas_pa"]);
                            lfinancia.Add(finan);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Consultar financiacion: "+ex.Message,ex);
                    }
                }
            }
            return lfinancia;
        }
    }
}
