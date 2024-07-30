using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class FacturasT
    {
        public int id_factura { get; set; }
        public string ciclo { get; set; }
        public int anio { get; set; }
        public string periodo { get; set; }
        public string numfact { get; set; }
        public string codpredio { get; set; }
        public decimal valor_total { get; set; }
        public DateTime fecha { get; set; }
        public DateTime fecha_limite { get; set; }
    }
}
