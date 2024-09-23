using DALEtiquetas.Models;
using DALEtiquetas.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DALEtiquetas.DALImpresion
{
    public class DALImpresion
    {
        public static string cnnstrConexion = cnnConexionDB.ConnectionString;

        public static Respuesta<List<Producto>> consultaProducto(Producto objProducto, int Opcion)
        {
            DataSet dsResult = new DataSet();
           Respuesta<List<Producto>> lsProductos = new Respuesta<List<Producto>>();
       
            string strcodigo = "";
            string strmensaje = "";
            string strnombremetodo = "";
            string strnombreclase = "";
            string strQuery = "";
            try
            {
                if (Opcion == 1) {
                    //Por sub categoria
                     strQuery = "SELECT  P.ID_PRODUCTO AS ID_PRODUCTO, P.NOMBRE_PRODUCTO AS NOMBRE_PRODUCTO " +
                                      ",P.PRECIO AS PRECIO, SC.NOM_SUBCATEGORIA  AS NOM_SUBCATEGORIA, P.ACTIVO AS ACTIVO "
                                      + "  FROM TBL_PRODUCTO  P INNER JOIN TBL_SUB_CATEGORIA SC "
                                      + "  ON SC.ID_SUB_CATEGORIA = P.ID_SUB_CATEGORIA "
                                      + " WHERE P.ID_SUB_CATEGORIA = " + objProducto.idSubCategoria + " ORDER BY P.NOMBRE_PRODUCTO";


                }
                else if (Opcion == 2){
                    //todo
                     strQuery = "SELECT  P.ID_PRODUCTO AS ID_PRODUCTO, P.NOMBRE_PRODUCTO AS NOMBRE_PRODUCTO " +
                                        ",P.PRECIO AS PRECIO, SC.NOM_SUBCATEGORIA  AS NOM_SUBCATEGORIA, P.ACTIVO AS ACTIVO "
                                        + "  FROM TBL_PRODUCTO  P INNER JOIN TBL_SUB_CATEGORIA SC "
                                        + "  ON SC.ID_SUB_CATEGORIA = P.ID_SUB_CATEGORIA "
                                        + " ORDER BY P.NOMBRE_PRODUCTO ";
                }

                dsResult = utileriasSql.EjecutaTextDataSet(strQuery, cnnstrConexion);
                
                    lsProductos.Value = (dsResult.Tables[0].AsEnumerable()
                        .Select(datarow => new Producto()
                        {
                            idProducto = datarow.Field<int>("ID_PRODUCTO"),
                            nombreProducto = datarow.Field<string>("NOMBRE_PRODUCTO"),
                            Precio = datarow.Field<decimal>("PRECIO"),
                            nomSubCategoria = datarow.Field<string>("NOM_SUBCATEGORIA"),
                            Activo = datarow.Field<bool>("ACTIVO"),
                        })).ToList();

                if (lsProductos.Value.Count > 0)
                {
                    strcodigo = "0000";
                    strmensaje = "OK";
                    lsProductos.codigoRespuesta = strcodigo;
                    lsProductos.mensaje = strmensaje;
                }
                else {
                    strcodigo = "0001";
                    strmensaje = "No existen registros para la consulta (productos)";
                    lsProductos.codigoRespuesta = strcodigo;
                    lsProductos.mensaje = strmensaje;

                }
                               
            }
            catch (Exception ex)
            {
                strmensaje = "Mensaje: " + ex.Message
                                       + "Fuente: " + ex.Source
                                       + "Metodo: " + ex.TargetSite
                                       + "Rastreo" + ex.StackTrace;
                strcodigo = "1000";
                strnombremetodo = "consultaProducto";
                strnombreclase = "DALImpresion";
                lsProductos.mensaje = strmensaje;
                lsProductos.nombreMetodo = strnombremetodo;
                lsProductos.nombreClase = strnombreclase;
                lsProductos.codigoRespuesta = strcodigo;
            }
            return lsProductos;
        }
       
        public static Respuesta<List<CodigoBarras>> consultaCodigodeBarras()
        {
            DataSet dsResult = new DataSet();
           Respuesta<List<CodigoBarras>> lsCodBarr = new Respuesta<List<CodigoBarras>>();
         
            string strcodigo = "";
            string strmensaje = "";
            string strnombremetodo = "";
            string strnombreclase = "";

            try
            {

                string strQuery = "SELECT ID_CODIGO_BARRAS, NOMBRE_CODE_BAR ,ETIQUETA_ZPL FROM TBL_CODIGO_BARRAS  WHERE ACTIVO = 1"
                                   + " ORDER BY NOMBRE_CODE_BAR";

                dsResult = utileriasSql.EjecutaTextDataSet(strQuery, cnnstrConexion);

                    lsCodBarr.Value = (dsResult.Tables[0].AsEnumerable()
                        .Select(datarow => new CodigoBarras()
                        {
                          ID_CODIGO_BARRAS = datarow.Field<int>("ID_CODIGO_BARRAS"),
                          NOMBRE_CODE_BAR = datarow.Field<string>("NOMBRE_CODE_BAR"),
                          ETIQUETA_ZPL = datarow.Field<string>("ETIQUETA_ZPL"),                          
                        })).ToList();

                if (lsCodBarr.Value.Count > 0)
                {

                    strcodigo = "0000";
                    strmensaje = "OK";

                    lsCodBarr.codigoRespuesta = strcodigo;
                    lsCodBarr.mensaje = strmensaje;

                }
                else {

                    strcodigo = "0001";
                    strmensaje = "No existen registros para la consulta (Código de Barras)";
                    lsCodBarr.codigoRespuesta = strcodigo;
                    lsCodBarr.mensaje = strmensaje;

                }
            }
            catch (Exception ex)
            {
                strmensaje = "Mensaje: " + ex.Message
                                       + "Fuente: " + ex.Source
                                       + "Metodo: " + ex.TargetSite
                                       + "Rastreo" + ex.StackTrace;
                strcodigo = "1000";
                strnombremetodo = "consultaCodigodeBarras";
                strnombreclase = "DALImpresion";
                lsCodBarr.mensaje = strmensaje;
                lsCodBarr.nombreMetodo = strnombremetodo;
                lsCodBarr.nombreClase = strnombreclase;
                lsCodBarr.codigoRespuesta = strcodigo;

            }
            return lsCodBarr;
        }

        public static Respuesta<List<SubCategoria>> consultaSubCategoria()
        {
            DataSet dsResult = new DataSet();
           Respuesta<List<SubCategoria>> lsSubCategoria = new Respuesta<List<SubCategoria>>();
           
            string strcodigo = "";
            string strmensaje = "";
            string strnombremetodo = "";
            string strnombreclase = "";
            try
            {

                string strQuery = "SELECT  ID_SUB_CATEGORIA, NOM_SUBCATEGORIA FROM TBL_SUB_CATEGORIA" + " ORDER BY NOM_SUBCATEGORIA";

                dsResult = utileriasSql.EjecutaTextDataSet(strQuery, cnnstrConexion);

                    lsSubCategoria.Value = (dsResult.Tables[0].AsEnumerable()
                        .Select(datarow => new SubCategoria()
                        {
                           ID_SUB_CATEGORIA = datarow.Field<int>("ID_SUB_CATEGORIA"),
                           NOM_SUBCATEGORIA = datarow.Field<string>("NOM_SUBCATEGORIA"),
                        })).ToList();

                if (lsSubCategoria.Value.Count > 0)
                {
                    strcodigo = "0000";
                    strmensaje = "OK";

                    lsSubCategoria.codigoRespuesta = strcodigo;
                    lsSubCategoria.mensaje = strmensaje;
                }
                else{
                    strcodigo = "0001";
                    strmensaje = "No existen registros para la consulta (Sub Categoria)";

                    lsSubCategoria.codigoRespuesta = strcodigo;
                    lsSubCategoria.mensaje = strmensaje;
                }
            }
            catch (Exception ex)
            {
                strmensaje = "Mensaje: " + ex.Message
                                    + "Fuente: " + ex.Source
                                    + "Metodo: " + ex.TargetSite
                                    + "Rastreo" + ex.StackTrace;
                strcodigo = "1000";
                strnombremetodo = "consultaSubCategoria";
                strnombreclase = "DALImpresion";
                lsSubCategoria.mensaje = strmensaje;
                lsSubCategoria.nombreMetodo = strnombremetodo;
                lsSubCategoria.nombreClase = strnombreclase;
                lsSubCategoria.codigoRespuesta = strcodigo;

            }
            return lsSubCategoria;
        }
  
        public static Respuesta<List<Impresion>> consultaimpresionesGrid()
        {
          DataSet dsResult = new DataSet();
          Respuesta<List<Impresion>> lsImpresion = new Respuesta<List<Impresion>>();
         

            string strQuery = "";
            try
            {
               
                     strQuery = "SELECT I.ID_IMPRESION,CB.NOMBRE_CODE_BAR,I.ID_PRODUCTO,I.ACTIVO,P.NOMBRE_PRODUCTO" +
                        ",CONVERT(VARCHAR(10), I.FECHA_ALTA, 103) + ' ' + convert(VARCHAR(8), I.FECHA_ALTA, 14) AS FECHA_ALTA" +
                        ",I.CANTIDAD,I.NUM_CODE_BAR,CASE WHEN I.IMPRESO = 0 THEN 'PREPARADO' WHEN I.IMPRESO = 1 THEN 'IMPRESO' END AS IMPRESO " +
                        "FROM TBL_IMPRESION I INNER JOIN TBL_PRODUCTO P ON I.ID_PRODUCTO = P.ID_PRODUCTO INNER JOIN TBL_CODIGO_BARRAS CB " +
                        "ON CB.ID_CODIGO_BARRAS = I.ID_CODIGO_BARRAS ORDER BY I.IMPRESO, I.FECHA_ALTA DESC;";

               

                dsResult = utileriasSql.EjecutaTextDataSet(strQuery, cnnstrConexion);

                    lsImpresion.Value = (dsResult.Tables[0].AsEnumerable()
                        .Select(datarow => new Impresion()
                        {
                            idImpresion = datarow.Field<int>("ID_IMPRESION"),
                            nombrecodeBar = datarow.Field<string>("NOMBRE_CODE_BAR"),
                            activo2 = datarow.Field<bool>("ACTIVO"),
                            impreso3 = datarow.Field<string>("IMPRESO"),
                            nombreProducto = datarow.Field<string>("NOMBRE_PRODUCTO"),
                            fechaAlta2 = datarow.Field<string>("FECHA_ALTA"),
                            cantidad = datarow.Field<int>("CANTIDAD"),
                            numcodeBar = datarow.Field<string>("NUM_CODE_BAR"),
                            idProducto = datarow.Field<int>("ID_PRODUCTO")
                        })).ToList();

                if (lsImpresion.Value.Count > 0) {

                    lsImpresion.codigoRespuesta = "0000";
                    lsImpresion.mensaje = "OK";
                }
                else {

                    lsImpresion.codigoRespuesta = "0001";
                    lsImpresion.mensaje = "No existen registros para la consulta (productos)";
                   
                }
            }
            catch (Exception ex)
            {
                lsImpresion.mensaje = "Mensaje: " + ex.Message
                                    + "Fuente: " + ex.Source
                                    + "Metodo: " + ex.TargetSite
                                    + "Rastreo" + ex.StackTrace;
                lsImpresion.codigoRespuesta = "consultaProducto";
                lsImpresion.codigoRespuesta = "DALImpresion";
                lsImpresion.codigoRespuesta = "1000";
            }
            return lsImpresion;
        }

        public static Respuesta<List<Impresion>> consultaimpresiones()
        {
            DataSet dsResult = new DataSet();
            Respuesta<List<Impresion>> lsImpresion = new Respuesta<List<Impresion>>();
            

            string strQuery = "";
            try
            {
                               
                strQuery = " SELECT ID_IMPRESION, CB.ETIQUETA_ZPL, I.CANTIDAD,I.NUM_CODE_BAR FROM TBL_IMPRESION I INNER JOIN TBL_CODIGO_BARRAS CB ON CB.ID_CODIGO_BARRAS = I.ID_CODIGO_BARRAS  " +
                       "  WHERE I.IMPRESO =  0 ";              

                dsResult = utileriasSql.EjecutaTextDataSet(strQuery, cnnstrConexion);

                    lsImpresion.Value = (dsResult.Tables[0].AsEnumerable()
                        .Select(datarow => new Impresion()
                        {
                            idImpresion = datarow.Field<int>("ID_IMPRESION"),
                            etiquetaZpl = datarow.Field<string>("ETIQUETA_ZPL"),
                            cantidad = datarow.Field<int>("CANTIDAD"),
                            numcodeBar = datarow.Field<string>("NUM_CODE_BAR")
                        })).ToList();

                if (lsImpresion.Value.Count > 0)
                {

                    lsImpresion.codigoRespuesta = "0000";
                    lsImpresion.mensaje = "OK";

                }
                else
                {
                    lsImpresion.codigoRespuesta = "0001";
                    lsImpresion.mensaje = "No existen registros para consultar impresiones";

                }
            }
            catch (Exception ex)
            {

                lsImpresion.mensaje = "Mensaje: " + ex.Message
                                    + "Fuente: " + ex.Source
                                    + "Metodo: " + ex.TargetSite
                                    + "Rastreo" + ex.StackTrace;
                lsImpresion.codigoRespuesta = "consultaimpresionId";
                lsImpresion.codigoRespuesta = "DALImpresion";
                lsImpresion.codigoRespuesta = "1000";

            }
            return lsImpresion;
        }

        public static Respuesta<List<Impresion>> consultaimpresion(Impresion objImpresion)
        {
            DataSet dsResult = new DataSet();

            Respuesta<List<Impresion>> objresplistimp = new Respuesta<List<Impresion>>();

            string strQuery = "";
            try
            {

                strQuery = " SELECT ID_IMPRESION, CB.ETIQUETA_ZPL, I.CANTIDAD,I.NUM_CODE_BAR,P.NOMBRE_PRODUCTO FROM TBL_IMPRESION I INNER JOIN TBL_CODIGO_BARRAS CB ON CB.ID_CODIGO_BARRAS = I.ID_CODIGO_BARRAS  " +
                           "  INNER JOIN TBL_PRODUCTO P ON P.ID_PRODUCTO = I.ID_PRODUCTO WHERE I.ID_IMPRESION = " +  objImpresion.idImpresion + " AND  I.ACTIVO =  0 ";

                dsResult = utileriasSql.EjecutaTextDataSet(strQuery, cnnstrConexion);

                objresplistimp.Value = (dsResult.Tables[0].AsEnumerable()
                    .Select(datarow => new Impresion()
                    {
                        idImpresion = datarow.Field<int>("ID_IMPRESION"),
                        etiquetaZpl = datarow.Field<string>("ETIQUETA_ZPL"),
                        numcodeBar = datarow.Field<string>("NUM_CODE_BAR"),
                        nombreProducto = datarow.Field<string>("NOMBRE_PRODUCTO"),
                        cantidad = datarow.Field<int>("CANTIDAD")

                    })).ToList();

                if (objresplistimp.Value.Count > 0)
                {

                    objresplistimp.codigoRespuesta = "0000";
                    objresplistimp.mensaje = "OK";

                }
                else
                {
                    objresplistimp.codigoRespuesta = "0001";
                    objresplistimp.mensaje = "No  existe registro en la consulta de la impresión";

                }
            }
            catch (Exception ex)
            {
                objresplistimp.mensaje = "Mensaje: " +  ex.Message
                                    + "Fuente: " + ex.Source
                                    + "Metodo: " + ex.TargetSite
                                    + "Rastreo" + ex.StackTrace;
                objresplistimp.codigoRespuesta = "consultaimpresion";
                objresplistimp.codigoRespuesta = "DALImpresion";
                objresplistimp.codigoRespuesta = "1000";

            }
            return objresplistimp;
        }

        public static Respuesta<string> InsertaImpresion(Impresion objImpresion)
        {
            Respuesta<string> objrespImp = new Respuesta<string>();
            
            string strcodigo = "";
            string strmensaje = "";
            string strnombremetodo = "";
            string strnombreclase = "";

            string codeBarr = "";

            try
            {

                codeBarr = generanumcodeBar();

                string lsQuery = "INSERT INTO TBL_IMPRESION (ID_CODIGO_BARRAS, ACTIVO, ID_PRODUCTO, FECHA_ALTA, ID_LOGIN, CANTIDAD, IMPRESO, NUM_CODE_BAR) VALUES ("
                                 + objImpresion.idCodigoBarras + "," + 0 + "," + objImpresion.idProducto + ",getdate(),"
                                 + objImpresion.idLogin + "," + objImpresion.cantidad + "," + 0 + ",'" + codeBarr + "')";

                int respGuardado = utileriasSql.ejecutaTextNonQuery(lsQuery, cnnstrConexion);
                if (respGuardado > 0)
                {

                    strcodigo = "0000";
                    strmensaje = "OK";
                    objrespImp.codigoRespuesta = strcodigo;
                    objrespImp.mensaje = strmensaje;

                }

            }
            catch (Exception ex)
            {
                strmensaje = "Mensaje: " + ex.Message
                                        + "Fuente: " + ex.Source
                                        + "Metodo: " + ex.TargetSite
                                        + "Rastreo" + ex.StackTrace;
                strcodigo = "1000";
                strnombremetodo = "InsertaImpresion";
                strnombreclase = "DALImpresion";
                objrespImp.mensaje = strmensaje;
                objrespImp.nombreMetodo = strnombremetodo;
                objrespImp.nombreClase = strnombreclase;
                objrespImp.codigoRespuesta = strcodigo;

            }
            return objrespImp;
        }

        public static Respuesta<string> eliminaImpresion(Impresion objImpresion)
        {
            Respuesta<string> objrespImp = new Respuesta<string>();
            DataSet dsResult = new DataSet();

            string strcodigo = "";
            string strmensaje = "";
            string strnombremetodo = "";
            string strnombreclase = "";
            int contieneDatos = 0;

            try
            {

                string lsQuery = "DELETE FROM TBL_IMPRESION WHERE ID_IMPRESION = " + objImpresion.idImpresion;

                int respeliminado = utileriasSql.ejecutaTextNonQuery(lsQuery, cnnstrConexion);
                if (respeliminado > 0)
                {
                    string lsqueryConsulta = "SELECT ID_IMPRESION  FROM TBL_IMPRESION ";
                    dsResult = utileriasSql.EjecutaTextDataSet(lsqueryConsulta, cnnstrConexion);

                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        contieneDatos = 1;
                    }

                    strcodigo = "0000";
                    strmensaje = "OK";
                    objrespImp.codigoRespuesta = strcodigo;
                    objrespImp.mensaje = strmensaje;
                    objrespImp.Value = contieneDatos.ToString();

                }
                else {

                    strcodigo = "0001";
                    strmensaje = "No se encontro ningún registro para eliminar";
                    objrespImp.codigoRespuesta = strcodigo;
                    objrespImp.mensaje = strmensaje;

                }

            }
            catch (Exception ex)
            {
                strmensaje = "Mensaje: " + ex.Message
                                        + "Fuente: " + ex.Source
                                        + "Metodo: " + ex.TargetSite
                                        + "Rastreo" + ex.StackTrace;
                strcodigo = "1000";
                strnombremetodo = "eliminaImpresion";
                strnombreclase = "DALImpresion";
                objrespImp.mensaje = strmensaje;
                objrespImp.nombreMetodo = strnombremetodo;
                objrespImp.nombreClase = strnombreclase;
                objrespImp.codigoRespuesta = strcodigo;

            }
            return objrespImp;
        }

        public static Respuesta<string> actualizacantidadImpresion(Impresion objImpresion)
        {
            Respuesta<string> objrespImp = new Respuesta<string>();

            string strcodigo = "";
            string strmensaje = "";
            string strnombremetodo = "";
            string strnombreclase = "";

            try
            {

                string lsQuery = "UPDATE TBL_IMPRESION SET  CANTIDAD = " + objImpresion.cantidad + " ,FECHA_ALTA =GETDATE()   WHERE ID_IMPRESION = " + objImpresion.idImpresion;

                int respeliminado = utileriasSql.ejecutaTextNonQuery(lsQuery, cnnstrConexion);
                if (respeliminado > 0)
                {

                    strcodigo = "0000";
                    strmensaje = "OK";
                    objrespImp.codigoRespuesta = strcodigo;
                    objrespImp.mensaje = strmensaje;

                }
                else
                {

                    strcodigo = "0001";
                    strmensaje = "No se fue posible efectúar la actualización";
                    objrespImp.codigoRespuesta = strcodigo;
                    objrespImp.mensaje = strmensaje;

                }

            }
            catch (Exception ex)
            {
                strmensaje = "Mensaje: " + ex.Message
                                        + "Fuente: " + ex.Source
                                        + "Metodo: " + ex.TargetSite
                                        + "Rastreo" + ex.StackTrace;
                strcodigo = "1000";
                strnombremetodo = "actualizacantidadImpresion";
                strnombreclase = "DALImpresion";
                objrespImp.mensaje = strmensaje;
                objrespImp.nombreMetodo = strnombremetodo;
                objrespImp.nombreClase = strnombreclase;
                objrespImp.codigoRespuesta = strcodigo;

            }
            return objrespImp;
        }

        public static Respuesta<string> preparaEtiqueta(Impresion objImpresion)
        {
            Respuesta<string> objrespImp = new Respuesta<string>();
            string strcodigo = "";
            string strmensaje = "";
            string strnombremetodo = "";
            string strnombreclase = "";
            bool impreso = false;
            string lsQuery = "";

            DataSet dsResult = new DataSet();
            try
            {
                string strQuery = "SELECT  IMPRESO FROM TBL_IMPRESION WHERE ID_IMPRESION = " + objImpresion.idImpresion;

                dsResult = utileriasSql.EjecutaTextDataSet(strQuery, cnnstrConexion);

                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    impreso = bool.Parse(dsResult.Tables[0].Rows[0]["IMPRESO"].ToString());

                    if (impreso == true)
                    {
                        lsQuery = "UPDATE TBL_IMPRESION SET IMPRESO = 0 , FECHA_ALTA = GETDATE()  WHERE ID_IMPRESION = " + objImpresion.idImpresion;
                    }

                    if (impreso == false)
                    {
                        lsQuery = "UPDATE TBL_IMPRESION SET IMPRESO = 1 , FECHA_ALTA = GETDATE()  WHERE ID_IMPRESION = " + objImpresion.idImpresion;
                    }

                    int respactualizado = utileriasSql.ejecutaTextNonQuery(lsQuery, cnnstrConexion);

                    string strquery2 = "SELECT ID_IMPRESION FROM TBL_IMPRESION WHERE IMPRESO = 0;";
                    dsResult = utileriasSql.EjecutaTextDataSet(strquery2, cnnstrConexion);
                    if (dsResult.Tables[0].Rows.Count > 0){
                        objrespImp.Value = "1";
                    }
                    else {
                        objrespImp.Value = "0";
                    }

                    if (respactualizado > 0)
                    {

                        strcodigo = "0000";
                        strmensaje = "OK";
                        objrespImp.codigoRespuesta = strcodigo;
                        objrespImp.mensaje = strmensaje;
                    }
                    else
                    {
                        strcodigo = "0001";
                        strmensaje = "No se fue posible efectúar la preparación";
                        objrespImp.codigoRespuesta = strcodigo;
                        objrespImp.mensaje = strmensaje;
                    }

                }
                else
                {
                    strcodigo = "0002";
                    strmensaje = "El registro para preparar no existe";
                    objrespImp.codigoRespuesta = strcodigo;
                    objrespImp.mensaje = strmensaje;

                }

            }
            catch (Exception ex)
            {
                strmensaje = "Mensaje: " + ex.Message
                                        + "Fuente: " + ex.Source
                                        + "Metodo: " + ex.TargetSite
                                        + "Rastreo" + ex.StackTrace;
                strcodigo = "1000";
                strnombremetodo = "actualizactivoImpresion";
                strnombreclase = "DALImpresion";
                objrespImp.mensaje = strmensaje;
                objrespImp.nombreMetodo = strnombremetodo;
                objrespImp.nombreClase = strnombreclase;
                objrespImp.codigoRespuesta = strcodigo;

            }
            return objrespImp;
        }
        public static Respuesta<string> actualizaImpresiones(Impresion objImpresion)
        {
            Respuesta<string> objrespImp = new Respuesta<string>();
            string strcodigo = "";
            string strmensaje = "";
            string strnombremetodo = "";
            string strnombreclase = "";
            string lsQuery = "";

            DataSet dsResult = new DataSet();
            try
            {                                                 
                    lsQuery = "UPDATE TBL_IMPRESION SET IMPRESO = 1 , FECHA_ALTA = GETDATE()  WHERE ID_IMPRESION = " + objImpresion.idImpresion;
              
                    int respactualizado = utileriasSql.ejecutaTextNonQuery(lsQuery, cnnstrConexion);
                    if (respactualizado > 0){

                        strcodigo = "0000";
                        strmensaje = "OK";
                        objrespImp.codigoRespuesta = strcodigo;
                        objrespImp.mensaje = strmensaje;
                    }else{
                        strcodigo = "0001";
                        strmensaje = "No se fue posible efectúar la actualización (campo impreso) de la impresión";
                        objrespImp.codigoRespuesta = strcodigo;
                        objrespImp.mensaje = strmensaje;
                    }

            }
            catch (Exception ex)
            {
                strmensaje = "Mensaje: " + ex.Message
                                        + "Fuente: " + ex.Source
                                        + "Metodo: " + ex.TargetSite
                                        + "Rastreo" + ex.StackTrace;
                strcodigo = "1000";
                strnombremetodo = "actualizactivoImpresion";
                strnombreclase = "DALImpresion";
                objrespImp.mensaje = strmensaje;
                objrespImp.nombreMetodo = strnombremetodo;
                objrespImp.nombreClase = strnombreclase;
                objrespImp.codigoRespuesta = strcodigo;

            }
            return objrespImp;
        }

        public static Respuesta<List<ConsultaCodeBar>> consultacodeBar(string numcodebar)
        {
            DataSet dsResult = new DataSet();
            Respuesta<List<ConsultaCodeBar>> lsconscodeBar = new Respuesta<List<ConsultaCodeBar>>();


            string strQuery = "";
            try
            {

                strQuery = " SELECT I.ID_PRODUCTO ,P.NOMBRE_PRODUCTO ,P.PRECIO ,I.NUM_CODE_BAR , SC.NOM_SUBCATEGORIA,C.NOM_CATEGORIA FROM TBL_IMPRESION I INNER JOIN TBL_PRODUCTO P ON  " +
                    "P.ID_PRODUCTO = I.ID_PRODUCTO INNER JOIN TBL_SUB_CATEGORIA SC ON  SC.ID_SUB_CATEGORIA = P.ID_SUB_CATEGORIA  INNER JOIN TBL_CATEGORIA C ON C.ID_CATEGORIA = SC.ID_CATEGORIA " +
                    "WHERE I.NUM_CODE_BAR = " + "'" + numcodebar  + "'" ;

                dsResult = utileriasSql.EjecutaTextDataSet(strQuery, cnnstrConexion);

                lsconscodeBar.Value = (dsResult.Tables[0].AsEnumerable()
                    .Select(datarow => new ConsultaCodeBar()
                    {
                        idProducto = datarow.Field<int>("ID_PRODUCTO"),
                        nombreProducto = datarow.Field<string>("NOMBRE_PRODUCTO"),
                        Precio = datarow.Field<decimal>("PRECIO"),
                        numcodeBar = datarow.Field<string>("NUM_CODE_BAR"),
                        nomSubcategoria = datarow.Field<string>("NOM_SUBCATEGORIA"),
                        nomCategoria = datarow.Field<string>("NOM_CATEGORIA")
                    })).ToList();

                if (lsconscodeBar.Value.Count > 0)
                {

                    lsconscodeBar.codigoRespuesta = "0000";
                    lsconscodeBar.mensaje = "OK";

                }
                else
                {
                    lsconscodeBar.codigoRespuesta = "0001";
                    lsconscodeBar.mensaje = "No existen el registro para consultar";

                }
            }
            catch (Exception ex)
            {

                lsconscodeBar.mensaje = "Mensaje: " + ex.Message
                                    + "Fuente: " + ex.Source
                                    + "Metodo: " + ex.TargetSite
                                    + "Rastreo" + ex.StackTrace;
                lsconscodeBar.codigoRespuesta = "consultacodeBar";
                lsconscodeBar.codigoRespuesta = "DALImpresion";
                lsconscodeBar.codigoRespuesta = "1000";

            }
            return lsconscodeBar;
        }
        private static string  generanumcodeBar() {

            string cadenaNumerica = "";          

            try
            {
                var guid = Guid.NewGuid();
                var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
                decimal seed = decimal.Parse(justNumbers.Substring(0, 11));
                //throw new Exception("Ha ocurrido un error al generar el numero de codigo de barras");

                cadenaNumerica = "0" + seed.ToString();

                if (cadenaNumerica.Length < 11)
                {
                    cadenaNumerica = cadenaNumerica + "0";
                    if (cadenaNumerica.Length < 11)
                    {
                        cadenaNumerica = cadenaNumerica + "0";
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;

            }
            finally 
            { 
            
            
            }

            return cadenaNumerica;      
        
        }

    }
}
