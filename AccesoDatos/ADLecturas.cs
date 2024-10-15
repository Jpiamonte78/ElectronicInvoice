using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace AccesoDatos
{
    public  class ADLecturas:ConexionBD
    {
        CultureInfo culture = new CultureInfo("en-US");
        public int Consultar_Lecturas(string ruta)
        {
            int reg = 0;
            List<Lectura> lLecturas = new List<Lectura>();
            SQLiteDataReader dr;
            SQLiteConnection sqlcon = new SQLiteConnection();
            try
            {
                sqlcon = GetConnIntegrin(ruta);
                string query = "select ciclo,anio, periodo,codigo,lect_ant,lect_act,consumo,promedio,fecha_lect from BdLecturas where fecha_lect is not NULL";
                SQLiteCommand cmd = new SQLiteCommand(query, sqlcon);
                dr = cmd.ExecuteReader();
                Lectura lectura = new Lectura();
                while (dr.Read())   
                {
                    string fecha = dr["fecha_lect"].ToString();
                    string formatof = "ddd MMM dd yyyy HH:mm:ss 'GMT'K '(hora estándar de Colombia)'";
                    lectura = new Lectura();
                    lectura.ciclo = dr["ciclo"].ToString();
                    lectura.anio = Convert.ToInt32(dr["anio"]);
                    lectura.periodo = dr["periodo"].ToString();
                    lectura.codigo_p = dr["codigo"].ToString();
                    lectura.lect_anterior = Convert.ToInt32(dr["lect_ant"]);
                    lectura.lect_actual = Convert.ToInt32(dr["lect_act"]);
                    lectura.consumo = Convert.ToInt32(dr["consumo"]);
                    lectura.consumo_promedio = Convert.ToInt32(dr["promedio"]);
                    lectura.fecha_lectura = DateTime.ParseExact(fecha, formatof, CultureInfo.InvariantCulture);
                    lLecturas.Add(lectura);
                }
                reg = InsertarLecturas(lLecturas);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Se ha presentado un error en Consultar_Lecturas " + ex.Message, ex);
            }
            finally
            {
                sqlcon.Close();
            }
            return reg;
        }

        private int InsertarLecturas(List<Lectura> llecturas)
        {
            int reg = 0;
            List<Lectura> lectintegrin = llecturas;
            SqlConnection sqlconn = GetConnDB();
            foreach (Lectura lect in lectintegrin)
            {
                using (SqlCommand cmd = new SqlCommand("SpLecturas", sqlconn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Insertar");
                    cmd.Parameters.AddWithValue("id_lecturas", DBNull.Value);
                    cmd.Parameters.AddWithValue("ciclo", lect.ciclo);
                    cmd.Parameters.AddWithValue("anio", lect.anio);
                    cmd.Parameters.AddWithValue("periodo",lect.periodo);
                    cmd.Parameters.AddWithValue("codigo_p", lect.codigo_p);
                    cmd.Parameters.AddWithValue("lect_actual", lect.lect_actual);
                    cmd.Parameters.AddWithValue("lect_anterior", lect.lect_anterior);
                    cmd.Parameters.AddWithValue("consumo", lect.consumo);
                    cmd.Parameters.AddWithValue("consumo_promedio", lect.consumo_promedio);
                    cmd.Parameters.AddWithValue("fecha_lectura", lect.fecha_lectura);
                    try
                    {
                        reg += cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Se ha presentado un error en InsertarLecturas: " + ex.Message, ex);
                    }
                }
            }
            sqlconn.Close();

            return reg;
        }
    }
}
