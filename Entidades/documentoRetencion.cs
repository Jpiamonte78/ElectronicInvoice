using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Entidades
{
    [Serializable]
    public class documentoRetencion
    {
        [XmlElement]
        public decimal baseRetencion { get; set; }
        [XmlElement]
        public decimal porcRetencion { get; set; }
        [XmlElement]
        public decimal valorRetencion { get; set; }
        [XmlElement]
        public string codRetencion { get; set; }
    }
}
