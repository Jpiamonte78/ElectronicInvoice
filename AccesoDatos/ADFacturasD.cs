using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Entidades;
using System.Data.SqlClient;
using System.Data;

namespace AccesoDatos
{
    public class ADFacturasD:ConexionBD
    {
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
                    detalle.valor = Convert.ToDecimal(dr["valor"].ToString().Replace('.',','));
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
                        reg += Convert.ToInt32(cmd.ExecuteScalar());
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
    }
}
