using BTLEtiquetas.BTLSeguridad;
using DALEtiquetas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;


namespace WebEtiquetas.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult Login() {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult autentica(DALEtiquetas.Models.Login login) {

            respLogin objRespSeguridad = new respLogin();
           
            string strMensaje = "";
            ViewData["Message"] = "";

            if (ModelState.IsValid) { 

                objRespSeguridad = BTLSeguridad.loginAutenticacion(login);

            if (objRespSeguridad.codigoRespuesta == "0000") {

                    FormsAuthentication.SetAuthCookie(objRespSeguridad.lslogin[0].nomusuario, false);

                    return RedirectToAction("Index","Bienvenido");
            }

            if (objRespSeguridad.codigoRespuesta == "0001") {
                strMensaje = "Los datos que usted escribio son incorrectos!!!";
                ViewData["Message"] = strMensaje;
            }
            if (objRespSeguridad.codigoRespuesta == "1000") {
                ViewData["Message"] = objRespSeguridad.mensaje
                                    + " " + objRespSeguridad.codigoRespuesta
                                    + " " + objRespSeguridad.nombreClase
                                    + " " + objRespSeguridad.nombreMetodo;
            }
        }          

            return View("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}

//Referencias:
//https://learn.microsoft.com/es-es/aspnet/mvc/overview/older-versions/getting-started-with-aspnet-mvc3/cs/adding-validation-to-the-model