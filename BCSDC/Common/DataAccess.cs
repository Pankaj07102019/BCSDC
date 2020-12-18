using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Common
{
    public class DataAccess
    {
        public DataAccess()
        {

        }
        private SqlConnection mycon;
        private SqlCommand mycmd;
        private SqlDataAdapter myda;
        private DataSet myds;
        private int retvalue;
        private SqlDataReader mydr;
        private SqlTransaction tran;

        public void OpenConnection()
        {
            if (this.mycon == null)
                this.mycon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            if (this.mycon.State != ConnectionState.Closed)
                return;
            this.mycon.Open();
        }

        public void CloseConnection()
        {
            if (this.mycon == null || this.mycon.State != ConnectionState.Open)
                return;
            this.mycon.Close();
        }

        public DataSet GetDataSet(string procname, SqlParameter[] sqlparam)
        {
            this.myds = new DataSet();
            this.OpenConnection();
            this.mycmd = new SqlCommand(procname, this.mycon);
            this.mycmd.CommandType = CommandType.StoredProcedure;
            this.mycmd.Connection = this.mycon;
            this.mycmd.CommandText = procname;
            this.mycmd.Parameters.AddRange(sqlparam);
            this.myda = new SqlDataAdapter(this.mycmd);
            this.myda.Fill(this.myds);
            this.CloseConnection();
            return this.myds;
        }

        public DataSet GetDataSetInline(string qstring)
        {
            this.myds = new DataSet();
            this.OpenConnection();
            this.mycmd = new SqlCommand(qstring, this.mycon);
            this.myda = new SqlDataAdapter(this.mycmd);
            this.myda.Fill(this.myds);
            this.CloseConnection();
            return this.myds;
        }

        public DataSet GetDataSet(string procname)
        {
            this.myds = new DataSet();
            this.OpenConnection();
            this.mycmd = new SqlCommand(procname, this.mycon);
            this.mycmd.CommandType = CommandType.StoredProcedure;
            this.mycmd.Connection = this.mycon;
            this.mycmd.CommandText = procname;
            this.myda = new SqlDataAdapter(this.mycmd);
            this.myda.Fill(this.myds);
            this.CloseConnection();
            return this.myds;
        }

        public int executeDMLQuery(string procname, SqlParameter[] sqlparam)
        {
            try
            {
                this.OpenConnection();
                this.tran = mycon.BeginTransaction();
                this.mycmd = new SqlCommand(procname, this.mycon, tran);
                this.mycmd.CommandType = CommandType.StoredProcedure;
                this.mycmd.Connection = this.mycon;
                this.mycmd.CommandText = procname;
                this.mycmd.Parameters.AddRange(sqlparam);
                this.retvalue = this.mycmd.ExecuteNonQuery();
                if (((DbParameterCollection)this.mycmd.Parameters).Contains("ReturnValue"))
                    this.retvalue = Convert.ToInt32(this.mycmd.Parameters["ReturnValue"].Value);
                tran.Commit();
                this.mycmd.Cancel();
                this.CloseConnection();
                return this.retvalue;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void executeDMLQueryinline(string strq)
        {
            try
            {
                this.OpenConnection();
                this.tran = mycon.BeginTransaction();
                this.mycmd = new SqlCommand(strq, this.mycon, tran);
                this.mycmd.ExecuteNonQuery();
                tran.Commit();
                this.CloseConnection();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void createTable(string tableQuery)
        {
            this.OpenConnection();
            this.mycmd = new SqlCommand(tableQuery, this.mycon);
            this.mycmd.ExecuteNonQuery();
            this.CloseConnection();
        }

        public DataTable getDataTable(string procname, SqlParameter[] sqlparam)
        {
            DataTable dataTable = new DataTable();
            this.OpenConnection();
            this.mycmd = new SqlCommand(procname, this.mycon);
            this.mycmd.CommandType = CommandType.StoredProcedure;
            this.mycmd.Connection = this.mycon;
            this.mycmd.CommandText = procname;
            this.mycmd.Parameters.AddRange(sqlparam);
            this.myda = new SqlDataAdapter(this.mycmd);
            this.myda.Fill(dataTable);
            this.CloseConnection();
            return dataTable;
        }

        public SqlDataReader GetDataReader(string procname, SqlParameter[] sqlparam)
        {
            OpenConnection();
            this.mycmd = new SqlCommand(procname, this.mycon);
            this.mycmd.CommandType = CommandType.StoredProcedure;
            this.mycmd.Connection = this.mycon;
            this.mycmd.CommandText = procname;
            this.mycmd.Parameters.AddRange(sqlparam);
            this.mydr = this.mycmd.ExecuteReader(CommandBehavior.CloseConnection);
            return this.mydr;
        }

        public SqlDataReader GetDataReader(string procname)
        {
            OpenConnection();
            this.mycmd = new SqlCommand(procname, this.mycon);
            this.mycmd.CommandType = CommandType.StoredProcedure;
            this.mycmd.Connection = this.mycon;
            this.mycmd.CommandText = procname;
            this.mydr = this.mycmd.ExecuteReader(CommandBehavior.CloseConnection);
            return this.mydr;
        }

        public SqlDataReader getDataReaderInline(string qstr)
        {
            OpenConnection();
            this.mycmd = new SqlCommand(qstr, this.mycon);
            this.mydr = this.mycmd.ExecuteReader(CommandBehavior.CloseConnection);
            return this.mydr;
        }

        public bool isExist(string procname)
        {
            this.OpenConnection();
            this.mycmd = new SqlCommand(procname, this.mycon);
            this.mycmd.CommandType = CommandType.StoredProcedure;
            this.mycmd.Connection = this.mycon;
            this.mycmd.CommandText = procname;
            int num = (int)this.mycmd.ExecuteScalar();
            this.CloseConnection();
            return num > 0;
        }

        public string getcdate()
        {
            this.OpenConnection();
            this.mycmd = new SqlCommand("select  dbo.getcdate()", this.mycon);
            string str = this.mycmd.ExecuteScalar().ToString();
            this.CloseConnection();
            return str.Split(new char[1]
      {
        ' '
      })[0];
        }

        public string getcdatetime()
        {
            this.OpenConnection();
            this.mycmd = new SqlCommand("select  dbo.getcdate()", this.mycon);
            string str = this.mycmd.ExecuteScalar().ToString();
            this.CloseConnection();
            return str;
        }



        //public string getexecutescalar(string qstr)
        //{
        //    this.OpenConnection();
        //    this.mycmd = new SqlCommand(qstr, this.mycon);
        //    string str = this.mycmd.ExecuteScalar().ToString();
        //    this.CloseConnection();
        //    return str;
        //}
        public string getexecutescalar(string procname, SqlParameter[] sqlparam)
        {
            OpenConnection();
            this.mycmd = new SqlCommand(procname, this.mycon);
            this.mycmd.CommandType = CommandType.StoredProcedure;
            this.mycmd.Connection = this.mycon;
            this.mycmd.CommandText = procname;
            this.mycmd.Parameters.AddRange(sqlparam);
            string str = this.mycmd.ExecuteScalar().ToString();
            this.CloseConnection();
            return str;
        }
        public string getexecutescalar(string procname)
        {
            OpenConnection();
            this.mycmd = new SqlCommand(procname, this.mycon);
            this.mycmd.CommandType = CommandType.StoredProcedure;
            this.mycmd.Connection = this.mycon;
            this.mycmd.CommandText = procname;
            string str = this.mycmd.ExecuteScalar().ToString();
            this.CloseConnection();
            return str;
        }
    }
}
