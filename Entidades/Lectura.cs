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
        public int lect_actual { get; set; }
        public int lect_anterior { get; set; }
        public int consumo { get; set; }
        public int consumo_promedio { get; set; }
        public DateTime fecha_lectura { get; set; }
    }
}
