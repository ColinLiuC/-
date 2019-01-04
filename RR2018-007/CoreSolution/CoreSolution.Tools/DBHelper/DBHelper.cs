using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CoreSolution.Tools
{
    public static class DBHelper
    {
        public static String connectionstring = "Data Source=192.168.0.33;Initial Catalog=PostStationDB2;User ID=sa;Password=reload;";

        public static int InsertData(String sql, String con)
        {
            return ExecSQL(sql, con);
        }

        public static int ExecSQL(String sql, String con)
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlTransaction transcation = connection.BeginTransaction())
                {
                    try
                    {
                        command.Transaction = transcation;
                        command.ExecuteNonQuery();
                        transcation.Commit();
                        result= 1;
                    }
                    catch
                    {
                        transcation.Rollback();
                        throw;
                    }
                }

            }

            return result;
        }


        public static int ExecSQLNOTrans(String sql, String con)
        {
            try
            {
                SqlConnection connection = new SqlConnection(con);
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                connection.Open();

                command.CommandText = sql;
                int result = command.ExecuteNonQuery();

                command.Connection.Close();

                return result;
            }
            catch
            {
                throw;
            }
        }


        public static int GetSQLResultData(String sql, String con)
        {
            try
            {
                SqlConnection connection = new SqlConnection(con);
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                connection.Open();

                SqlDataAdapter da = new SqlDataAdapter(sql, connection); //创建DataAdapter数据适配器实例
                DataSet ds = new DataSet();
                da.Fill(ds);
                command.Connection.Close();

                if (ds.Tables.Count > 0)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                }
                else
                {
                    return 0;
                }


            }
            catch
            {
                return 0;
            }
        }

        //15-1-22 kz
        public static DataSet ExecuteQuery(string sqlString, string con)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlString, connection);
                    sqlDataAdapter.Fill(ds, "ds");
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }

                return ds;
            }
        }


        public static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataAdapter val = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                val.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
    }
}
