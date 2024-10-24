﻿using System;
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
        static string RutaDb;
        // GET: CargarDatos
        public ActionResult CargarDatos()
        {
            return View();
        }

        public ActionResult SubirArchivo(HttpPostedFileBase archivo)
        {
            string _script = "";
            RutaDb = new ParametrosGeneralesAD().ConsultarRutaDb();
            if (!Directory.Exists(RutaDb))
            {
                Directory.CreateDirectory(RutaDb);
            }
            else
            {
                DirectoryInfo directory = new DirectoryInfo(RutaDb);
                foreach (FileInfo file in directory.GetFiles())
                {
                    if(!IsFileInUse(file))
                        file.Delete();
                }
            }
            if(archivo!=null)
            {
                string NombreDb = Path.GetFileName(archivo.FileName);
                string rutacompleta = Path.Combine(RutaDb, NombreDb);
                archivo.SaveAs(rutacompleta);
                int cargadosT = CargarFacturas(rutacompleta);
                int cargadosD = CargarDetalles(rutacompleta);
                int cargalect = CargarLecturas(rutacompleta);
                int cargatari = CargarTarifas(rutacompleta);

                if(!IsFileInUse(new FileInfo(rutacompleta)))
                {
                    System.IO.File.Delete(rutacompleta);
                }
                _script = "<script language='javascript'>" +
                    "window.alert('Se han cargado correctamente "+cargadosT.ToString()+" Facturas Totales y "+cargadosD.ToString()+" Detalles ');" +
                    "window.location.href='/CargarDatos/CargarDatos';" +
                    "</script>";
            }
            return Content(_script);
        }

        private static bool IsFileInUse(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }
            return false;
        }

        private int CargarFacturas(string rutadb)
        {
            int reg = 0;
            try
            {
                reg = new ADFacturasT().Consultar_FacturasTotal(rutadb);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reg;
        }

        private int CargarDetalles(string rutadb)
        {
            int reg = 0;
            try
            {
                reg = new ADFacturasD().Consultar_detalles(rutadb);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reg;
        }

        private int CargarLecturas(string rutadb)
        {
            int reg = 0;
            try
            {
                reg = new ADLecturas().Consultar_Lecturas(rutadb);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reg;
        }

        private int CargarTarifas(string rutadb)
        {
            int reg = 0;
            try
            {
                reg = new ADTarifas().Consultar_tarifas(rutadb);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reg;
        }

    }
}