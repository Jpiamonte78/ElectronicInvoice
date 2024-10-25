using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class documentoAdjunto
    {
        [XmlElement]
        public string nombreFichero { get; set; }
        [XmlElement]
        public string contenidoFichero { get; set; }
        [XmlElement]
        public string indPdfPrincipal { get; set; }
    }
}
