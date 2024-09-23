using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALEtiquetas
{
    public class cnnConexionDB
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["ConnDB"].ToString();

        public static string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }
    }
}
