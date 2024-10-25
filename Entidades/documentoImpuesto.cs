using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class documentoImpuesto
    {
        [XmlElement]
        public decimal baseImpuesto { get; set; }
        [XmlElement] 
        public decimal porcImpuesto { get; set;}
        [XmlElement]
        public decimal valorImpuesto { get; set; }
        [XmlElement]
        public string codImpuesto { get; set; }
        [XmlElement]
        public string nombreImpuesto { get; set; }
    }
}
