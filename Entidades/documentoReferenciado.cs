using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Entidades
{
    [Serializable]
    public class documentoReferenciado
    {
        [XmlElement]
        public string numDocumentoRef { get; set; }
        [XmlElement]
        public string uuidDocumentoRef { get; set; }
        [XmlElement]
        public string fechaDocumentoRef { get; set; }

    }
}
