using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Web;

namespace WebEtiquetas.Helpers
{
    public class EstatusPrinter
    {
        //https://allmastersolutions.com/verificar-disponibilidad-de-impresora-c/
        public bool EstaEnLineaLaImpresora(string printerName)
        {
            string str = "";
            bool online = false;

            ManagementScope scope = new ManagementScope(ManagementPath.DefaultPath);

            scope.Connect();

            //Consulta para obtener las impresoras, en la API Win32
            SelectQuery query = new SelectQuery("select * from Win32_Printer");

            ManagementClass m = new ManagementClass("Win32_Printer");

            ManagementObjectSearcher obj = new ManagementObjectSearcher(scope, query);

            //Obtenemos cada instancia del objeto ManagementObjectSearcher
            using (ManagementObjectCollection printers = m.GetInstances())
                foreach (ManagementObject printer in printers)
                {
                    if (printer != null)
                    {
                        //Obtenemos la primera impresora en el bucle
                        str = printer["Name"].ToString().ToLower();

                        if (str.Equals(printerName.ToLower()))
                        {
                            //Una vez encontrada verificamos el estado de ésta
                            if (printer["WorkOffline"].ToString().ToLower().Equals("true") || printer["PrinterStatus"].Equals(7))
                                //Fuera de línea
                                online = false;
                            else
                                //En línea
                                online = true;
                        }
                    }
                    else
                        throw new Exception("No fueron encontradas impresoras instaladas en el equipo");
                }
            return online;
        }
    }
}