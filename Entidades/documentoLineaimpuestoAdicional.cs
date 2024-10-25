using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class documentoLineaimpuestoAdicional
    {
        [XmlElement]
        public decimal baseImpuestoAdicional { get; set; }
        [XmlElement]
        public decimal porcImpuestoAdicional { get; set; }
        [XmlElement]
        public decimal valorImpuestoAdicional { get; set; }
        [XmlElement]
        public string codImpuestoAdicional { get; set; }
    }
}
