using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALEtiquetas.Models
{
    public class Producto
    {
        public int idProducto { get; set; }

        public string nombreProducto { get; set; }

        public decimal Precio { get; set; }

        public string nomSubCategoria { get; set; }

        public int idSubCategoria { get; set; }

        public bool Activo { get; set; }
                   
    }
}
