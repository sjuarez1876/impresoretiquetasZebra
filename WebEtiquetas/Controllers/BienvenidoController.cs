using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebEtiquetas.Controllers
{
    [Authorize]
    public class BienvenidoController : Controller
    {
        // GET: Bienvenido
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult Logout()
        //{
        //    FormsAuthentication.SignOut();

        //    return RedirectToAction("Login", "Login");
        //}
    }
}