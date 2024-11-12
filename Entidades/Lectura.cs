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
        public int consumo1 { get; set; }
        public int consumo2 { get; set; }
        public int consumo3 { get; set; }
        public int consumo4 { get; set; }
        public int consumo5 { get; set; }
        public int consumo6 { get; set; }
    }
}
