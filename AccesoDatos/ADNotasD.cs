using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace AccesoDatos
{
    public class ADNotasD:ConexionBD
    {
        public List<NotasD> Consultar_Detalle(string ciclo, string periodo, string anio, string codpredio,string numfac)
        {
            List<NotasD> ldetalle = new List<NotasD>();
            using (var conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpNotasD";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Consultar");
                    cmd.Parameters.AddWithValue("Id_Detalle_nota", DBNull.Value);
                    cmd.Parameters.AddWithValue("ciclo", ciclo);
                    cmd.Parameters.AddWithValue("anio", anio);
                    cmd.Parameters.AddWithValue("periodo", periodo);
                    cmd.Parameters.AddWithValue("numfac", numfac);
                    cmd.Parameters.AddWithValue("codpredio", codpredio);
                    cmd.Parameters.AddWithValue("codigo_c", DBNull.Value);
                    cmd.Parameters.AddWithValue("nombre_c", DBNull.Value);
                    cmd.Parameters.AddWithValue("valor", DBNull.Value);
                    cmd.Parameters.AddWithValue("fecha", DBNull.Value);
                    cmd.Parameters.AddWithValue("cantidad", DBNull.Value);
                    try
                    {
                        NotasD Dnota = new NotasD();
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            Dnota = new NotasD();
                            Dnota.Id_Detalle_nota = Convert.ToInt32(dr["Id_Detalle_nota"]);
                            Dnota.numfac = dr["numfac"].ToString();
                            Dnota.ciclo = dr["ciclo"].ToString();
                            Dnota.periodo = dr["periodo"].ToString();
                            Dnota.anio = dr["anio"].ToString();
                            Dnota.codpredio = dr["codpredio"].ToString();
                            Dnota.codigo_c = dr["codigo_c"].ToString();
                            Dnota.nombre_c = dr["nombre_c"].ToString();
                            Dnota.valor = Convert.ToDecimal(dr["valor"]);
                            Dnota.fecha = Convert.ToDateTime(dr["fecha"]);
                            Dnota.cantidad = Convert.ToInt32(dr["cantidad"]);
                            ldetalle.Add(Dnota);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Consultar_Detalle: " + ex.Message, ex);
                    }
                }
            }
            return ldetalle;
        }
    }
}
