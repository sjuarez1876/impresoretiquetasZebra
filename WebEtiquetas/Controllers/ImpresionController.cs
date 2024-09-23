using DALEtiquetas.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebEtiquetas.Helpers;

namespace WebEtiquetas.Controllers
{
    [Authorize]
    public class ImpresionController : Controller
    {
        // GET: Impresion
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize]
        public ActionResult Impresion()
        {
            return View();
        }


        [HttpPost]
        public JsonResult consultasubCategoria()
        {
            Respuesta<List<SubCategoria>> lsSubcategoria = BTLEtiquetas.BTLImpresion.BTLImpresion.consultaSubCategoria();

            return Json(new { jsonsubCategoria = lsSubcategoria.Value, jsonRespuesta = lsSubcategoria, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult consultaCodigoBarras()
        {
           Respuesta<List<CodigoBarras>> lscodigobarras = BTLEtiquetas.BTLImpresion.BTLImpresion.consultaCodigodeBarras();

            return Json(new { objsoncodigoBarras = lscodigobarras.Value, jsonRespuesta = lscodigobarras, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult ConsultaProductosPorSubcategoria(int idsubcategoria)
        {            
           Respuesta<List<Producto>> lsProductos = new Respuesta<List<Producto>>();
            Producto objProducto = new Producto();
            objProducto.idSubCategoria = idsubcategoria;

            if (objProducto.idSubCategoria == 0)
            {
                lsProductos = BTLEtiquetas.BTLImpresion.BTLImpresion.consultaProducto(objProducto, 2);
            }
            else
            {
                lsProductos = BTLEtiquetas.BTLImpresion.BTLImpresion.consultaProducto(objProducto, 1);
            }

          
            return Json(new { data = lsProductos.Value, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult InsertaImpresion(Impresion objImpresion)
        {
            Respuesta<string> respimpresionAgrega = new Respuesta<string>();

            objImpresion.idLogin = 2;
            respimpresionAgrega = BTLEtiquetas.BTLImpresion.BTLImpresion.InsertaImpresion(objImpresion);
           
            return Json(new { respimpresionAgrega = respimpresionAgrega, JsonRequestBehavior.AllowGet });

        }

        [HttpPost]
        public JsonResult actualizacantidadImpresion(Impresion objImpresion)
        {
            Respuesta<string> respimpresionActualiza = new Respuesta<string>();
            respimpresionActualiza = BTLEtiquetas.BTLImpresion.BTLImpresion.actualizacantidadImpresion(objImpresion);

            return Json(new { respimpresionActualiza = respimpresionActualiza, JsonRequestBehavior.AllowGet });
        }


        [HttpPost]
        public JsonResult consultaproductosImpresionGrid()
        {
          Respuesta<List<Impresion>> lsrespImpresion = new Respuesta<List<Impresion>>();
          List<Impresion> lsimpresion = new List<Impresion>();
            lsrespImpresion = BTLEtiquetas.BTLImpresion.BTLImpresion.consultaimpresionesGrid();

            if (lsrespImpresion.codigoRespuesta == "0000") {
                lsimpresion = lsrespImpresion.Value.ToList();
            }

            if (lsrespImpresion.codigoRespuesta == "0001") {

                lsimpresion = lsrespImpresion.Value.ToList();
            }

            if (lsrespImpresion.codigoRespuesta == "1000") {
                lsimpresion = null;
            }

            //var strlist = lsrespImpresion.Value.ToList();

            return Json(new { data = lsimpresion, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult imprimeEtiquetas()
        {
          Respuesta<List<Impresion>> lsImpresion = new Respuesta<List<Impresion>>();
            Respuesta<string> objrespImp = new Respuesta<string>();
            Impresion objimpresion = new Impresion();
            bool statusPrinter = false;
            string strNombreImpresora = "ZDesigner GC420t";
            string strcodebar = "";
            string etiquetacode128 = "";
            int cantidad = 0;
            int countEtiquetas = 0;
            int countProductos = 0;
            string strcodigo = "";
            string strmensaje = "";
            string strnombremetodo = "";
            string strnombreclase = "";
            int idImpresion = 0;
           
            try
            {               
                EstatusPrinter objEP = new EstatusPrinter();
                statusPrinter = objEP.EstaEnLineaLaImpresora(strNombreImpresora);

                if (statusPrinter == true)
                {
                    lsImpresion = BTLEtiquetas.BTLImpresion.BTLImpresion.consultaimpresiones();
                  
                    if (lsImpresion.Value.Count > 0)
                    {
                        foreach (var etiquetas in lsImpresion.Value)
                        {
                            countProductos++;
                            cantidad = etiquetas.cantidad;
                           
                            for (var i = 0; i < cantidad; i++)
                            {
                                idImpresion = etiquetas.idImpresion;
                                strcodebar = etiquetas.numcodeBar.ToString();
                                etiquetacode128 = etiquetas.etiquetaZpl.ToString().Replace("[numcodebar]", strcodebar);
                                RawPrinterHelper.SendStringToPrinter(strNombreImpresora, etiquetacode128);
                                objimpresion.impreso = 1;
                                objimpresion.idImpresion = idImpresion;

                                objrespImp = BTLEtiquetas.BTLImpresion.BTLImpresion.actualizaImpresiones(objimpresion);
                                countEtiquetas++;
                                objrespImp.mensaje =  "Se imprimieron: " + countEtiquetas + " etiqueta(s) de: " + countProductos +  " producto(s) ";

                            }

                        }
                    }

                }
                else
                {
                    objrespImp.codigoRespuesta = "0002";
                    objrespImp.mensaje = "La impresora no se encuentra disponible favor de verificar";
                }

            }
            catch (Exception ex) {

                
                strmensaje = "Mensaje: " + ex.Message
                                        + "Fuente: " + ex.Source
                                        + "Metodo: " + ex.TargetSite
                                        + "Rastreo" + ex.StackTrace;
                strcodigo = "1000";
                strnombremetodo = "imprimeEtiquetas";
                strnombreclase = "ImpresionController";
                objrespImp.mensaje = strmensaje + "|" + "  code de barr: " + strcodebar + "|" + "IdImpresion: " + idImpresion;
                objrespImp.nombreMetodo = strnombremetodo;
                objrespImp.nombreClase = strnombreclase ;
                objrespImp.codigoRespuesta = strcodigo;
                

            }
           
            return Json(new { jsonRespuesta = objrespImp, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult preparaEtiqueta(Impresion objimpresion)
        {
            Respuesta<string> objrespImp = new Respuesta<string>();
            objimpresion.impreso = 0;
            objrespImp = BTLEtiquetas.BTLImpresion.BTLImpresion.preparaEtiqueta(objimpresion);

            return Json(new { jsonRespuesta = objrespImp ,existeImpreso = objrespImp.Value,JsonRequestBehavior.AllowGet });
        }


        [HttpPost]
        public JsonResult eliminaImpresion(Impresion objImpresion)
        {
            Respuesta<string> respimpresionelimina = new Respuesta<string>();

            objImpresion.activo = 0;
            objImpresion.idLogin = 2;
            respimpresionelimina = BTLEtiquetas.BTLImpresion.BTLImpresion.eliminaImpresion(objImpresion);

            return Json(new { jsonRespuesta = respimpresionelimina, JsonRequestBehavior.AllowGet });

        }

        //public ActionResult Logout()
        //{
        //    FormsAuthentication.SignOut();

        //    return RedirectToAction("Login", "Login");
        //}

    }
}

//Modal
//https://www.aspsnippets.com/Articles/4036/Bootstrap-5-Open-Show-Modal-Popup-Window-using-jQuery/