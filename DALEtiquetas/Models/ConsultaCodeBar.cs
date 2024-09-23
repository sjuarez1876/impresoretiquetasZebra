using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALEtiquetas.Models
{
    public class ConsultaCodeBar
    {
        public int idProducto { get; set; }
        public string nombreProducto { get; set; }
        public decimal Precio { get; set; }
        public string numcodeBar { get; set; }
	    public string nomSubcategoria { get; set; }
        public string nomCategoria { get; set; }


    }
}
