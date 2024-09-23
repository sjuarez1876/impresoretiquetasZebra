using DALEtiquetas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebEtiquetas.Controllers
{
    [Authorize]
    public class ConsultaImpresionController : Controller
    {
        // GET: ConsultaImpresion
       
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult consultacodeBar(string numcodebar)
        {
            Respuesta<List<ConsultaCodeBar>> lsconscodeBar = BTLEtiquetas.BTLImpresion.BTLImpresion.consultacodeBar(numcodebar);            
            return Json(new  { jslsconscodebar = lsconscodeBar.Value, respconscodbar = lsconscodeBar, JsonRequestBehavior.AllowGet });
        }

        //public ActionResult Logout()
        //{
        //    FormsAuthentication.SignOut();

        //    return RedirectToAction("Login", "Login");
        //}
    }
}