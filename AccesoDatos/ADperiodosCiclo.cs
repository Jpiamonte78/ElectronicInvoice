using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace AccesoDatos
{
    public class ADperiodosCiclo:ConexionBD
    {
        public int consultarperiodosCiclo(string ciclo,string ruta)
        {
            int reg = 0;
            periodosCiclo periodos = new periodosCiclo();
            SQLiteDataReader dr;
            SQLiteConnection sqlcon = new SQLiteConnection();
            try
            {
                sqlcon = GetConnIntegrin(ruta);
                string query = "select periodo,anio,nomperiodo,observaciones,notificaciones from BdEmpresa";
                SQLiteCommand cmd = new SQLiteCommand(query, sqlcon);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    periodos = new periodosCiclo();
                    periodos.ciclo = ciclo;
                    periodos.periodo = dr["periodo"].ToString();
                    periodos.anio = dr["anio"].ToString();
                    periodos.nomperiodo = dr["nomperiodo"].ToString();
                    periodos.observaciones = dr["observaciones"].ToString();
                    periodos.notificaciones = dr["notificaciones"].ToString();
                }
                reg = InsertarperiodosCiclo(periodos);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Se ha presentado un error en consultarperiodosCiclo " + ex.Message, ex);
            }
            finally
            {
                sqlcon.Close();
            }
            return reg;
        }

        private int InsertarperiodosCiclo(periodosCiclo periodos)
        {
            int reg = 0;
            SqlConnection sqlconn = GetConnDB();

            using (SqlCommand cmd = new SqlCommand("SpPeriodosCiclo", sqlconn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("accion", "Insertar");
                cmd.Parameters.AddWithValue("ciclo", periodos.ciclo);
                cmd.Parameters.AddWithValue("anio", periodos.anio);
                cmd.Parameters.AddWithValue("periodo", periodos.periodo);
                cmd.Parameters.AddWithValue("nomperiodo", periodos.nomperiodo);
                cmd.Parameters.AddWithValue("observaciones", periodos.observaciones);
                cmd.Parameters.AddWithValue("notificaciones", periodos.notificaciones);
                try
                {
                    reg += cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Se ha presentado un error en InsertarperiodosCiclo: " + ex.Message, ex);
                }
                finally
                {
                    sqlconn.Close();
                }
            }

            return reg;
        }

        public periodosCiclo consultarperiodosCiclo(string ciclo, string periodo, string anio) 
        {
            periodosCiclo periodos = new periodosCiclo();
            SqlConnection sqlconn = GetConnDB();

            using (SqlCommand cmd = new SqlCommand("SpPeriodosCiclo", sqlconn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("accion", "Consultar");
                cmd.Parameters.AddWithValue("ciclo", ciclo);
                cmd.Parameters.AddWithValue("anio", anio);
                cmd.Parameters.AddWithValue("periodo", periodo);
                cmd.Parameters.AddWithValue("nomperiodo", DBNull.Value);
                cmd.Parameters.AddWithValue("observaciones", DBNull.Value);
                cmd.Parameters.AddWithValue("notificaciones", DBNull.Value);
                try
                {
                    
                    var dr = cmd.ExecuteReader();
                    while(dr.Read())
                    {
                        periodos = new periodosCiclo();
                        periodos.ciclo = dr["ciclo"].ToString();
                        periodos.anio = dr["anio"].ToString();
                        periodos.periodo = dr["periodo"].ToString();
                        periodos.nomperiodo = dr["nomperiodo"].ToString();
                        periodos.observaciones = dr["observaciones"].ToString();
                        periodos.notificaciones = dr["notificaciones"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Se ha presentado un error en InsertarperiodosCiclo: " + ex.Message, ex);
                }
                finally
                {
                    sqlconn.Close();
                }
            }
            return periodos;
        }
    }
}
