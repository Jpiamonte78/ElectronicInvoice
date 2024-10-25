using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class documentodatoAdicional
    {
        [XmlElement]
        public int numDato { get; set; }
        [XmlElement]
        public string valorDato { get; set; }
    }
}
