using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALEtiquetas.Models
{
    public class respLogin
    {
        public string codigoRespuesta { get; set; }

        public string mensaje {  get; set; }

        public string nombreClase { get; set; }

        public string nombreMetodo  { get; set; }


        public List<Login> lslogin { get; set; }

    }
}
