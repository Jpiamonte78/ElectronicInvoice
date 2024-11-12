using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccesoDatos;
using Entidades;

namespace WebAppElectronicInvoice.Controllers
{
    public class Envio_FacturaController : Controller
    {
        // GET: Envio_Factura
        public ActionResult Envio_Documentos(DateTime fecha)
        {
            List<Envio_Factura> lenvios = new List<Envio_Factura>();
            try
            {
                lenvios = new ADEnvio_Factura().consultar_fecha(fecha);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrión un error en la consulta: " + ex.Message;
            }
            return PartialView("Envio_Documentos",lenvios);
        }
    }
}