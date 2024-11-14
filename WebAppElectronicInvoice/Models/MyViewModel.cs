using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppElectronicInvoice.Models
{
    public class MyViewModel
    {
        public IEnumerable<ClienteIntegrin> Pendientes { get; set; }
        public IEnumerable<ClienteIntegrin> Clientes { get; set; }
    }
}