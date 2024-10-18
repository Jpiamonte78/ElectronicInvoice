using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Lectura
    {
        public int Id_Lecturas { get; set; }
        public string ciclo { get; set; }
        public string periodo { get; set; }
        public int anio { get; set; }
        public string codigo_p { get; set; }
        public decimal lect_actual { get; set; }
        public decimal lect_anterior { get; set; }
        public decimal consumo { get; set; }
        public decimal consumo_promedio { get; set; }
        public DateTime fecha_lectura { get; set; }
    }
}
