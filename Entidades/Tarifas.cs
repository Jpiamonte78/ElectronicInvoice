using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Tarifas
    {
        public int id_tarifa { get; set; }
        public string ciclo { get; set; }
        public int anio { get; set; }
        public string periodo { get; set; }
        public string uso { get; set; }
        public string estrato { get; set; }
        public decimal Cargo_fijo { get; set; }
        public decimal Consumo { get; set; }
    }
}
