using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class identificadoresFacturas
    {
        [XmlElement]
        public int anyo { get; set; }
        [XmlElement]
        public string idFiscalEmisor { get; set; }
        [XmlElement]
        public string numeroDocumento { get; set; }
        [XmlElement]
        public string tipoDocumento { get; set; }
    }
}
