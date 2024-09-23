using DALEtiquetas.Models;
using DALEtiquetas.Utilerias;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALEtiquetas
{
    public class DALSeguridad
    {
        public static string cnnstrConexion = cnnConexionDB.ConnectionString;
        public static Models.respLogin  loginAutenticacion(Models.Login objLogin) 
        {            
            Models.respLogin respSeguridad = new Models.respLogin();
            DataSet dsResult = new DataSet();
            List<Login> lsLogin = new List<Login>();
            try 
            {             
                    string strQuery = "SELECT (NOMBRE + ' ' +  APELLIDOS) AS NOMUSUARIO,USUARIO, ACTIVO,ID_ROL  " +
                                        "FROM TBL_LOGIN " +
                                        "WHERE USUARIO = '" + objLogin.Usuario + "' AND PASSWORD = '" + objLogin.Password + "'";

                    dsResult = utileriasSql.EjecutaTextDataSet(strQuery, cnnstrConexion);

                    if (dsResult.Tables[0].Rows.Count > 0){

                    lsLogin = (dsResult.Tables[0].AsEnumerable()
                        .Select(datarow => new Login()
                        {
                            nomusuario = datarow.Field<string>("NOMUSUARIO")                                
                        })).ToList();

                        respSeguridad.codigoRespuesta = "0000";
                        respSeguridad.mensaje = "OK , Transacción exitosa";
                        respSeguridad.lslogin = lsLogin.ToList();
                    }
                    else {
                        respSeguridad.codigoRespuesta = "0001";
                        respSeguridad.mensaje = "La consulta es inexistente para este usuario";
                    }
               
            }catch(Exception ex) 
            {

                respSeguridad.mensaje = "Mensaje: " + ex.Message
                                      + "Fuente: " + ex.Source
                                      + "Metodo: " + ex.TargetSite
                                      + "Rastreo" + ex.StackTrace;
                respSeguridad.nombreMetodo = "loginAutenticacion";
                respSeguridad.nombreClase = "DALSeguridad";
                respSeguridad.codigoRespuesta = "1000";

            }
            return respSeguridad;
        }
    }
}
