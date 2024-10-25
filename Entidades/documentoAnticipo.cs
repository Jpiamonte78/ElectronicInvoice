using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Entidades
{
    [Serializable]
    public class documentoAnticipo
    {
        [XmlElement]
        public decimal valorAnticipo { get; set; }
        [XmlElement]
        public string fechaRecepcionAnticipo { get; set; }
        [XmlElement]
        public string fechaRealizacionAnticipo { get; set; }
        [XmlElement]
        public string instruccionesAnticipo { get; set; }
    }
}
