using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class NotasD
    {
        public int Id_Detalle_nota { get; set; }
        public string ciclo { get; set; }
        public string anio { get; set; }
        public string periodo { get; set; }
        public string numfac { get; set; }
        public string codpredio { get; set; }
        public string codigo_c { get; set; }
        public string nombre_c { get; set; }
        public decimal valor { get; set; }
        public  DateTime fecha{ get; set; }
        public int cantidad { get; set; }
    }
}
