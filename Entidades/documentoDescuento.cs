using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class documentoDescuento
    {
        [XmlElement]
        public decimal @base { get; set; }
        [XmlElement]
        public decimal porcentaje { get; set; }
        [XmlElement]
        public decimal valor { get; set; }
        [XmlElement]
        public string motivo { get; set; }
    }
}
