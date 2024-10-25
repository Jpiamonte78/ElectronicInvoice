using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Entidades
{
    [Serializable]
    public class extensionSPDservicioPublicosuscriptor
    {
        [XmlElement]
        public string nombre { get; set; }
        [XmlElement]
        public string direccionPostal { get; set; }
        [XmlElement]
        public string direccionEntrega { get; set; }
        [XmlElement]
        public string ciudad { get; set; }
        [XmlElement]
        public string departamento { get; set; }
        [XmlElement]
        public string pais { get; set; }
        [XmlElement]
        public string tipoEstrato { get; set; }
        [XmlElement]
        public string email { get; set; }
    }
}
