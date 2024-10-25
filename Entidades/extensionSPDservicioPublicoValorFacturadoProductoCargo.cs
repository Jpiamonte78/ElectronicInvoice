using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class extensionSPDservicioPublicoValorFacturadoProductoCargo
    {
        [XmlElement]
        public string razonCargo { get; set; }
        [XmlElement]
        public decimal valorCargo { get; set; }
    }
}
