using DALEtiquetas.DALImpresion;
using DALEtiquetas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTLEtiquetas.BTLImpresion
{
    public class BTLImpresion
    {        
        public static Respuesta<List<CodigoBarras>> consultaCodigodeBarras()
        {
            return DALEtiquetas.DALImpresion.DALImpresion.consultaCodigodeBarras();
        }
        public static Respuesta<List<SubCategoria>> consultaSubCategoria() {

            return DALEtiquetas.DALImpresion.DALImpresion.consultaSubCategoria();       
        }
        public static Respuesta<List<Producto>> consultaProducto(Producto objProducto,  int Opcion)
        {
            return DALEtiquetas.DALImpresion.DALImpresion.consultaProducto(objProducto,Opcion);
        }

        public static Respuesta<string> InsertaImpresion(Impresion objImpresion)
        {
            return DALEtiquetas.DALImpresion.DALImpresion.InsertaImpresion(objImpresion);
        }

        public static Respuesta<List<Impresion>> consultaimpresionesGrid()
        {
            return DALEtiquetas.DALImpresion.DALImpresion.consultaimpresionesGrid();        
        }

        public static Respuesta<List<Impresion>> consultaimpresiones()
        {
            return DALEtiquetas.DALImpresion.DALImpresion.consultaimpresiones();
        }

        public static Respuesta<string> eliminaImpresion(Impresion objImpresion)
        {
            return DALEtiquetas.DALImpresion.DALImpresion.eliminaImpresion(objImpresion);
        }

        public static Respuesta<List<Impresion>> consultaimpresion(Impresion objImpresion)
        {           
            return DALEtiquetas.DALImpresion.DALImpresion.consultaimpresion(objImpresion); 
        }
        public static Respuesta<string> actualizacantidadImpresion(Impresion objImpresion)
        {
            return DALEtiquetas.DALImpresion.DALImpresion.actualizacantidadImpresion(objImpresion);
        }
        public static Respuesta<string> actualizaImpresiones(Impresion objImpresion)
        {
            return DALEtiquetas.DALImpresion.DALImpresion.actualizaImpresiones(objImpresion);
        }

        public static Respuesta<string> preparaEtiqueta(Impresion objImpresion)
        {
            return DALEtiquetas.DALImpresion.DALImpresion.preparaEtiqueta(objImpresion);
        }

        public static Respuesta<List<ConsultaCodeBar>> consultacodeBar(string numcodebar) {

            return DALEtiquetas.DALImpresion.DALImpresion.consultacodeBar(numcodebar);
        }
    }
}
