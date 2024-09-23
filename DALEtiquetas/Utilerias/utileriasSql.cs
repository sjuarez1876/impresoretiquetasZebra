using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DALEtiquetas.Utilerias
{
    public class utileriasSql
    {
        ////public static string strSqlCon = ConfigurationManager.ConnectionStrings["ConnDBEtiquetas"].ToString();
        public static string strSqlCon = "Example";
        public utileriasSql()
        {

        }


        public static string StoredProcedureEscalar(string unSPName, ref SqlParameter[] Params)
        {
            SqlConnection SqlCon = new SqlConnection(strSqlCon);
            string campo = "";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = unSPName;
                cmd.Connection = SqlCon;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in Params)
                {
                    cmd.Parameters.Add(param);
                }
                cmd.CommandTimeout = 0;
                SqlCon.Open();
                campo = (string)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                if (SqlCon.State != ConnectionState.Closed)
                    SqlCon.Close();
                throw ex;
            }
            finally
            {
                SqlCon.Close();
            }
            return campo;
        }

        public static string StoredProcedureEscalar(string unSPName, CommandType Tipo, ref SqlParameter[] Params)
        {
            SqlConnection SqlCon = new SqlConnection(strSqlCon);
            string campo = "";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = unSPName;
                cmd.Connection = SqlCon;
                cmd.CommandType = Tipo;
                foreach (SqlParameter param in Params)
                {
                    cmd.Parameters.Add(param);
                }
                cmd.CommandTimeout = 0;
                SqlCon.Open();
                campo = (string)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                if (SqlCon.State != ConnectionState.Closed)
                    SqlCon.Close();
                throw ex;
            }
            finally
            {
                SqlCon.Close();
            }
            return campo;
        }

        public static int StoredProcedureNonQuery(string unSPName, ref SqlParameter[] Params)
        {
            SqlConnection SqlCon = new SqlConnection(strSqlCon);
            int cuantos = 0;
            try
            {
                SqlCon.Open();
                SqlCommand cmd = new SqlCommand(unSPName, SqlCon);
                //cmd.CommandText = unSPName;
                //cmd.Connection = SqlCon;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in Params)
                {
                    cmd.Parameters.Add(param);
                }
                cmd.CommandTimeout = 0;
                cuantos = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (SqlCon.State != ConnectionState.Closed)
                    SqlCon.Close();

                throw ex;
            }
            finally
            {
                SqlCon.Close();
            }
            return cuantos;
        }

        public static int StoredProcedureNonQuery(string unSPName, ref SqlParameter[] Params, ref int Retorno)
        {
            SqlConnection SqlCon = new SqlConnection(strSqlCon);
            int cuantos = 0;
            try
            {
                SqlCon.Open();
                SqlCommand cmd = new SqlCommand(unSPName, SqlCon);
                //cmd.CommandText = unSPName;
                //cmd.Connection = SqlCon;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in Params)
                {
                    cmd.Parameters.Add(param);
                }
                cmd.CommandTimeout = 0;
                cuantos = cmd.ExecuteNonQuery();

                Retorno = (int)cmd.Parameters["@ReturnValue"].Value;


            }
            catch (Exception ex)
            {
                if (SqlCon.State != ConnectionState.Closed)
                    SqlCon.Close();

                throw ex;
            }
            finally
            {
                SqlCon.Close();
            }
            return cuantos;
        }

        public static int ejecutaTextNonQuery(string SQL, string cnnConexion)
        {
            SqlConnection SqlCon = new SqlConnection(cnnConexion);
            int cuantos = 0;
            try
            {
                SqlCon.Open();
                SqlCommand cmd = new SqlCommand(SQL, SqlCon);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                cuantos = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (SqlCon.State != ConnectionState.Closed)
                    SqlCon.Close();
               
                throw ex;
            }
            finally
            {
                SqlCon.Close();
            }
            return cuantos;
        }
       
        public static SqlDataReader StoredProcedureReader(string unSPName, CommandType Tipo, ref SqlParameter[] Params)
        {
            SqlConnection SqlCon = new SqlConnection(strSqlCon);
            SqlDataReader reader;
            try
            {
                SqlCommand cmd = new SqlCommand(unSPName, SqlCon);
                cmd.CommandType = Tipo;
                //if (Tipo == CommandType.StoredProcedure)
                //{
                foreach (SqlParameter param in Params)
                {
                    cmd.Parameters.Add(param);
                }
                //}
                cmd.CommandTimeout = 0;
                SqlCon.Open();
                reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                if (SqlCon.State != ConnectionState.Closed)
                    SqlCon.Close();
               
                throw ex;
            }
            return reader;
        }
      
        public static SqlDataReader StoredProcedureReader(string unSPName, CommandType Tipo, ref SqlParameter[] Params, out SqlCommand cmd)
        {
            SqlConnection SqlCon = new SqlConnection(strSqlCon);
            SqlDataReader reader;
            try
            {
                cmd = new SqlCommand(unSPName, SqlCon);
                cmd.CommandType = Tipo;
                //if (Tipo == CommandType.StoredProcedure)
                //{
                foreach (SqlParameter param in Params)
                {
                    cmd.Parameters.Add(param);
                }
                //}
                cmd.CommandTimeout = 0;
                SqlCon.Open();
                reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                if (SqlCon.State != ConnectionState.Closed)
                    SqlCon.Close();
               
                throw ex;
            }
            return reader;
        }
      
        public static DataTable StoredProcedureDataTable(string unSPName, CommandType Tipo, ref SqlParameter[] Params)
        {
            SqlConnection SqlCon = new SqlConnection(strSqlCon);
            SqlDataReader reader;
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(unSPName, SqlCon);
                cmd.CommandType = Tipo;
                //if (Tipo == CommandType.StoredProcedure)
                //{
                foreach (SqlParameter param in Params)
                {
                    cmd.Parameters.Add(param);
                }
                //}
                cmd.CommandTimeout = 0;
                SqlCon.Open();
                reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                dt.Load(reader);
            }
            catch (Exception ex)
            {
                if (SqlCon.State != ConnectionState.Closed)
                    SqlCon.Close();
               
                throw ex;
            }
            return dt;
        }

        public static DataSet StoredProcedureDataSet(string unSPName, ref SqlParameter[] Params,  string cnnConexion)
        {
            SqlConnection SqlCon = new SqlConnection(cnnConexion);
            DataSet ds = null;
            try
            {
                SqlCon.Open();
                SqlCommand objCmd = new SqlCommand(unSPName, SqlCon);
                objCmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in Params)
                {
                    objCmd.Parameters.Add(param);
                }
                SqlDataAdapter da = new SqlDataAdapter(objCmd);
                ds = new DataSet("dsResult");
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                if (SqlCon.State != ConnectionState.Closed)
                    SqlCon.Close();
               
                throw ex;
            }
            finally
            {
                SqlCon.Close();
            }
            return ds;
        }
       
        public static DataSet EjecutaTextDataSet(string SQL, string cnnConexion)
        {
            SqlConnection SqlCon = new SqlConnection(cnnConexion);
            DataSet ds = null;
            SqlDataAdapter objData;
            SqlCommand objCmd;
            try
            {
                SqlCon.Open();
                objCmd = new SqlCommand();
                objCmd.CommandText = SQL;
                objCmd.Connection = SqlCon;
                objCmd.CommandType = CommandType.Text;
                objData = new SqlDataAdapter(objCmd);
                ds = new DataSet("dsResult");
                objData.Fill(ds);
            }
            catch (Exception ex)
            {
                if (SqlCon.State != ConnectionState.Closed)
                    SqlCon.Close();
                throw ex;
            }
            finally
            {
                objCmd = null;
                SqlCon.Close();
            }
            return ds;
        }
      
        public DataSet ObtieneHoraFechaServidor(string SQL)
        {
            SqlConnection SqlCon = new SqlConnection(strSqlCon);

            string FechaTiempo = "";

            DataSet ds = null;
            SqlDataAdapter objData;
            SqlCommand objCmd;
            DateTime Tiempo = Convert.ToDateTime(FechaTiempo.ToString());
            string entrada = SQL + "[**]" + SqlCon.ConnectionString;
            try
            {
                SqlCon.Open();
                objCmd = new SqlCommand();
                objCmd.CommandText = SQL;
                objCmd.Connection = SqlCon;
                objCmd.CommandType = CommandType.Text;
                objData = new SqlDataAdapter(objCmd);
                ds = new DataSet("dsResult");
                objData.Fill(ds);
            }
            catch (Exception ex)
            {
                if (SqlCon.State != ConnectionState.Closed)
                    SqlCon.Close();
                throw ex;
            }
            finally
            {
                objCmd = null;
                SqlCon.Close();
            }

            // OWUtilerias.Log("Usuario", "Ultimus0", "", entrada, Mensaje + " " + ds.Tables[0].Rows.Count.ToString(), Tiempo);
            return ds;
        }

        public SqlDataReader StoredProcedureReaderOBD(string unSPName, CommandType Tipo, ref SqlParameter[] Params)
        {
            SqlConnection SqlCon = new SqlConnection(strSqlCon);

            SqlDataReader reader;
            try
            {
                SqlCommand cmd = new SqlCommand(unSPName, SqlCon);
                cmd.CommandType = Tipo;
                //if (Tipo == CommandType.StoredProcedure)
                //{
                foreach (SqlParameter param in Params)
                {
                    cmd.Parameters.Add(param);
                }
                //}
                cmd.CommandTimeout = 0;
                SqlCon.Open();
                reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                if (SqlCon.State != ConnectionState.Closed)
                    SqlCon.Close();
               
                throw ex;
            }
            return reader;
        }
     
        public DataSet StoredProcedureDataSetOBD(string SQL)
        {
            SqlConnection SqlCon = new SqlConnection(strSqlCon);

            DataSet ds = null;
            SqlDataAdapter objData;
            SqlCommand objCmd;
            try
            {
                SqlCon.Open();
                objCmd = new SqlCommand();
                objCmd.CommandText = SQL;
                objCmd.Connection = SqlCon;
                objCmd.CommandType = CommandType.Text;
                objData = new SqlDataAdapter(objCmd);
                ds = new DataSet("dsResult");
                objData.Fill(ds);
            }
            catch (Exception ex)
            {
                if (SqlCon.State != ConnectionState.Closed)
                    SqlCon.Close();
                throw ex;
            }
            finally
            {
                objCmd = null;
                SqlCon.Close();
            }
            return ds;
        }
        
        public string StoredProcedureEscalarOBD(string unSPName, CommandType Tipo, ref SqlParameter[] Params)
        {
            SqlConnection SqlCon = new SqlConnection(strSqlCon);

            string campo = "";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = unSPName;
                cmd.Connection = SqlCon;
                cmd.CommandType = Tipo;
                foreach (SqlParameter param in Params)
                {
                    cmd.Parameters.Add(param);
                }
                cmd.CommandTimeout = 0;
                SqlCon.Open();
                campo = (string)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                if (SqlCon.State != ConnectionState.Closed)
                    SqlCon.Close();
               
                throw ex;
            }
            finally
            {
                SqlCon.Close();
            }
            return campo;
        }


    }
}