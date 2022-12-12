using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1.SqlServer
{
    public  static class SqlHelper
    {
        /// <summary>
        /// 使用锁防止多线程同时操作数据库表
        /// </summary>
        private static readonly object sqlLock = new object();

        /// <summary>
        /// SQL连接
        /// </summary>
        private static SqlConnection connection;

        public static string connStr = "server=127.0.0.1; database=; user id=sa;password=Root123";
        /// <summary>
        /// 创建SQL连接属性
        /// </summary>
        public static SqlConnection Connection
        {
            get
            {
                try
                {
                    if (connection == null)//如果没有创建连接，则先创建
                    {
                        //从配置文件中获取SQL连接字段
                        //string connStr = ConfigurationManager.ConnectionStrings["ConnetcionNmae"].ToString();

                        connection = new SqlConnection(connStr);//创建连接
                        connection.Open();//打开连接
                    }
                    else if (connection.State == ConnectionState.Broken)//如果连接中断，则重现打开
                    {
                        connection.Close();
                        connection.Open();
                    }
                    else if (connection.State == ConnectionState.Closed)//如果关闭，则打开
                    {
                        connection.Open();
                    }
                    return connection;
                }
                catch (Exception ex)
                {
                    if (connection != null)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                    //Log4netHelper.WriteErrorLog("Connection" + ex.Message, ex);
                    return null;
                }
            }
        }

        /// <summary>
        /// 重置连接
        /// </summary>
        public static void ResetConnection()
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="str">执行字符串</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string str)
        {
            lock (sqlLock)
            {
                try
                {
                    SqlDataAdapter sda = new SqlDataAdapter(str, Connection);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    return ds;
                }
                catch (Exception ex)
                {
                    ResetConnection();
                    //Log4netHelper.WriteErrorLog(str, ex);
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="str"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string str, params SqlParameter[] values)
        {
            lock (sqlLock)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = Connection;
                    cmd.CommandText = str;
                    cmd.Parameters.AddRange(values);
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    DataSet dt = new DataSet();
                    sda.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    ResetConnection();
                    //Log4netHelper.WriteErrorLog(str, ex);
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取表格
        /// </summary>
        /// <param name="str">执行字符串</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string str)
        {
            return GetDataSet(str).Tables[0];
        }

        /// <summary>
        /// 获取表格
        /// </summary>
        /// <param name="str">执行字符串</param>
        /// <param name="values">参数值数组</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string str, params SqlParameter[] values)
        {
            return GetDataSet(str, values).Tables[0];
        }


        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="str"></param>
        public static void ExecuteNonQuery(string str)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = str;
                cmd.ExecuteNonQuery();
            }
            catch
            {

            }

        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ExecuteSql(string str)
        {
            lock (sqlLock)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                SqlTransaction trans = Connection.BeginTransaction();
                try
                {
                    if (cmd.Connection != null && cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Transaction = trans;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = str;
                        cmd.ExecuteNonQuery();
                        trans.Commit();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (SqlException ex)
                {
                    trans.Rollback();//事物回滚
                    //ResetConnection();
                    //Log4netHelper.WriteErrorLog(str, ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="str"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool ExecuteSql(string str, params SqlParameter[] values)
        {
            lock (sqlLock)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                SqlTransaction trans = Connection.BeginTransaction();
                try
                {
                    if (cmd.Connection != null && cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Transaction = trans;
                        cmd.CommandText = str;
                        cmd.Parameters.AddRange(values);
                        cmd.ExecuteNonQuery();
                        trans.Commit();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (SqlException ex)
                {
                    trans.Rollback();//事物回滚
                    ResetConnection();
                    //Log4netHelper.WriteErrorLog(str, ex);
                    return false;
                }
            }
        }

       




    }
    public class operateSql
    {
        /// <summary>
        /// 判断数据库是否存在
        /// </summary>
        /// <param name="db">数据库名称</param>
        /// <returns></returns>
        public Boolean IsDBExist(string db)
        {
            string createDbStr = " select * from master.dbo.sysdatabases where name " + "= '" + db + "'";
            DataTable dt = SqlHelper.GetDataTable(createDbStr);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断数据库中指定表格是否存在
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tb"></param>
        /// <returns></returns>
        public Boolean IsTableExist(string db, string tb)
        {
            string createTbStr = "USE " + db + " select 1 from sysobjects where id =object_id('" + tb + "') and type = 'U'";
            DataTable dt = SqlHelper.GetDataTable(createTbStr);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="db"></param>
        public void CreateDataBase(string db)
        {
            if (IsDBExist(db))
            {
                throw new Exception("数据库已经存在！");
            }
            else//不存在则创建
            {
                string createDbStr = "Create DATABASE " + db;
                SqlHelper.ExecuteNonQuery(createDbStr);

            }
        }


        /// <summary>
        /// 创建数据库表
        /// </summary>
        /// <param name="db">数据库名</param>
        /// <param name="tb">表名</param>
        public void CreateDataTable(string db, string tb)
        {
            if (IsDBExist(db) == false)
            {
                throw new Exception("数据库不存在！");
            }
            if (IsTableExist(db, tb))
            {
                throw new Exception("数据库表已经存在！");
            }
            else
            {
                string content = "usseId int IDENTITY(1,1) PRIMARY KEY ,userName nvarchar(50)";
                string createTableStr = "USE " + db + " Create table " + tb + "(" + content + ")";

                SqlHelper.ExecuteNonQuery(createTableStr);
            }
        }
        /// <summary>
        /// 增加数据库内容
        /// </summary>
        /// <param name="sheetName">表格名字</param>
        /// <param name="str1">本数据库中表示DeviceID</param>
        /// <param name="str2">本数据库中表示DeviceErrorCode</param>
        /// <param name="str3">本数据库中表示DeviceErrorCode</param>
        public int addData(SqlConnection conn, string sheetName, string str1, string str2, string str3,string str4)
        {
            string strSQL = "INSERT INTO " + sheetName + " (样本名称,缺陷种类,Code,Score) " + "VALUES ('" + str1 + "', '" + str2 + "', '" + str3 + "', '" + str4  + "')";
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            try
            {
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
                conn.Close();
                return e.Number;
            }
            return 1;

        }

        /// <summary>
        /// 查找表中所有数据
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="sheetName">表名称</param>
        /// <returns></returns>
        public DataSet select(SqlConnection conn,string sheetName)
        {
            //查找所有设备状态信息
            SqlCommand cmd = new SqlCommand("select * from " + sheetName, conn);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataSet dataSet = new DataSet();
            sda.Fill(dataSet, "样本名称");
            return dataSet;
        }

        /// <summary>
        /// 查找某一数据相关
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="sheetName">表名称</param>
        /// <param name="columnName">查找列</param>
        /// <param name="data">查找列中某一数据</param>
        /// <returns></returns>
        public DataSet select(SqlConnection conn,string sheetName, string columnName, string data)
        {
            SqlCommand cmd = new SqlCommand("select * from " + sheetName + " where " + columnName + " in ('" + data + "')", conn);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataSet dataSet = new DataSet();
            sda.Fill(dataSet, "cs");
            return dataSet;
        }

       
    }

}
