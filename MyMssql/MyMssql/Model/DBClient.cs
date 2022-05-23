using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MyMssql.Model
{
    public class DBClient
    {


        private SqlConnection pDbConn;

        public DBClient(string sConnName)
        {
            if (sConnName == "BSBAE_HOME")
            {
                pDbConn = new SqlConnection(GetConnString_BSBAE_HOME());
            }
            else if (sConnName=="COMPANY_BBSPC")
            {
                pDbConn = new SqlConnection(GetConnString_COMPANY_BBSPC());
            }
            else if (sConnName == "AZUREDB")
            {
                pDbConn = new SqlConnection(GetConnString_AZUREDB());
            }
            else
                throw new Exception("Unknown Connection name!!");

            // pDbConn.Open();

            try
            {
                pDbConn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet GetDataSet(SqlCommand dbcmd)
        {
            try
            {
                DataSet ds = new DataSet();

                dbcmd.Connection = pDbConn;
                SqlDataAdapter dbAdapter = new SqlDataAdapter(dbcmd);
                dbAdapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int ExecDBCommand(SqlCommand dbcmd)
        {
            try
            {
                dbcmd.Connection = pDbConn;
                return dbcmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        private string GetConnString_BSBAE_HOME()
        {
            string srvrdbname = "TestDB";
            string srvrname = "bsbae.ddns.net,51433";
            string srvrusername = "sa";
            string srvrpassword = "wkehd124!@$";
            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}";

            return sqlconn;
        }
        private string GetConnString_COMPANY_BBSPC()
        {
            string srvrdbname = "TestDB";
            string srvrname = "172.20.105.36,12345";
            string srvrusername = "sa";
            string srvrpassword = "wkehd123!@";
            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}";

            return sqlconn;
        }
        //nakdong.database.windows.net
        private string GetConnString_AZUREDB()
        {
            string srvrdbname = "NakDongDB";
            string srvrname = "nakdong.database.windows.net";
            string srvrusername = "bsbae";
            string srvrpassword = "wkehd124!@$";
            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}";

            //string sqlconn = "Server=tcp:nakdong.database.windows.net,1433;Initial Catalog=NakDongDB;Persist Security Info=False;User ID=bsbae;Password=wkehd124!@$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return sqlconn;
        }
    }
   
}
