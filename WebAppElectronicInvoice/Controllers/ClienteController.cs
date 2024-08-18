using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccesoDatos;
using Entidades;

namespace WebAppElectronicInvoice.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Cliente()
        {
            List<ClienteIntegrin> lclientespendientes = new List<ClienteIntegrin>();
            lclientespendientes = new ADClienteIntegrin().ConsultarPendientes();
            return View(lclientespendientes);
        }
        [HttpGet]
        public ActionResult EditCliente(int id)
        {
            ClienteIntegrin cliente = new ClienteIntegrin();
            cliente = new ADClienteIntegrin().ConsultarUnCliente(id);
            cliente.ltipodocumento = new ADTipoDocumento().tipoDocumentos();    
            return View(cliente);
        }
    }
}