using DALEtiquetas.Models;
using DALEtiquetas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace BTLEtiquetas.BTLSeguridad
{
    public class BTLSeguridad
    {
        public static respLogin loginAutenticacion(DALEtiquetas.Models.Login objLogin)
        {
         return   DALSeguridad.loginAutenticacion(objLogin);
        }
    }
}
