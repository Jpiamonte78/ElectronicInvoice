using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class extensionSPDservicioPublicoValorFacturadoProductoLecturaContador
    {
        [XmlElement]
        public string datosMedidor { get; set; }
        [XmlElement]
        public string fechaLecturaAnterior { get; set; }
        [XmlElement]
        public decimal unidadesLecturaAnterior { get; set; }
        [XmlElement]
        public string unidadMedidaAnterior { get; set; }
        [XmlElement]
        public string fechaLecturaActual { get; set; }
        [XmlElement]
        public decimal unidadesLecturaActual { get; set; }
        [XmlElement]
        public string unidadMedidaActual { get; set; }
        [XmlElement]
        public string metodoLectura { get; set; }
        [XmlElement]
        public string duracionServicio { get; set; }
        [XmlElement]
        public string unidadMedidaServicio { get; set; }
    }
}
