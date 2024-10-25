using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Entidades
{
    [Serializable]
    public class documentocondicionPago
    {
        [XmlElement]
        public string formaPago { get; set; }
        [XmlElement]
        public string medioPago { get; set; }
        [XmlElement]
        public string entidadBancaria { get; set; }
        [XmlElement]
        public string numeroCuenta { get; set; }
        [XmlElement]
        public string beneficiario { get; set; }
        [XmlElement]
        public string fechaPago { get; set; }
    }
}
