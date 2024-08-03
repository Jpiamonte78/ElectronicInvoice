using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using AccesoDatos;
using System.IO;

namespace WebAppElectronicInvoice.Controllers
{
    public class CargarDatosController : Controller
    {
        // GET: CargarDatos
        public ActionResult CargarDatos()
        {
            return View();
        }

        public ActionResult SubirArchivo(HttpPostedFileBase archivo)
        {
            string _script = "";
            string RutaDb = Server.MapPath("~/DbIntegrin/");
            if (!Directory.Exists(RutaDb))
            {
                Directory.CreateDirectory(RutaDb);
            }
            else
            {
                DirectoryInfo directory = new DirectoryInfo(RutaDb);
                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
            }
            if(archivo!=null)
            {
                string NombreDb = Path.GetFileName(archivo.FileName);
                archivo.SaveAs(RutaDb+NombreDb);
                _script = "<script language='javascript'>" +
                    "window.alert('Guardado Correctamente');" +
                    "window.location.href='/CargarDatos/CargarDatos';" +
                    "</script>";
            }
            return Content(_script);
        }


    }
}