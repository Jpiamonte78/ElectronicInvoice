﻿using System;
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
        public decimal mvjm { get; set; }
        public decimal dv1 { get; set; }
        public decimal cm { get; set; }
        public decimal tm { get; set; }
        public decimal gm { get; set; }
        public decimal poder_c { get; set; }
        public decimal pleno_mvjm { get; set; }
        public decimal neto_mvjm { get; set; }
        public decimal subs_contrib { get; set; }
        public decimal cons_prom_subs { get; set; }
        public decimal factor_correccion { get; set; }
    }
}
