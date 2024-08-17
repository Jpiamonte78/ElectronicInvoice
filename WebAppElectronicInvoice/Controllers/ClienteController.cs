using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppElectronicInvoice.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Cliente()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Cliente(int id)
        {
            return View(id);
        }
    }
}