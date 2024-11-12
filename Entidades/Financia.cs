using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Financia
    {
        public string ciclo { get; set; }
        public string anio { get; set; }
        public string periodo { get; set; }
        public string codigo_p { get; set; }
        public string codigo_c { get; set; }
        public decimal valor_c { get; set; }
        public decimal valor_cu { get; set; }
        public int cuotas { get; set; }
        public int cuotas_pa { get; set; }
    }
}
