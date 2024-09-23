using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALEtiquetas.Models
{
    public class Impresion
    {
        public int idImpresion { get; set; }
        public int idCodigoBarras { get; set; }
        public int activo { get; set; }
        public bool activo2 { get; set; }
        public int idProducto { get; set; }
        public DateTime fechaAlta { get; set; }
        public string fechaAlta2 { get;set;}
        public int idLogin { get; set; }   
        public int cantidad { get; set; }
        public int impreso { get; set; }
        public bool impreso2 { get; set; }

        public string impreso3 { get; set; }
        public string numcodeBar { get; set; } 
        public string nombrecodeBar { get; set; }
        public string nombreProducto { get; set; }
        public string etiquetaZpl { get; set; }

    }    
}
