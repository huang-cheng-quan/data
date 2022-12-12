using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApp1.SqlServer
{
    class Test
    {
        private void ConnectionSql() 
        {
            string ConnString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\BackupData\\watch\\watch整机软件\\WatchDetectDemo\\WindowsFormsApp1\\SqlServer\nvtDataBase.mdf;Integrated Security = True"; //这里使用连接字符串来连接
            SqlConnection conn = new SqlConnection(); //实例化连接对象 也可以写成SqlConnection conn = new           	SqlConnection(ConnString); 这样就不用下一句了
            conn.ConnectionString = ConnString;//设置连接字符串
            conn.Open();//连接数据库   
            string sqly = @"insert into [User] (Id,name,age,number) values (7,N'王明阳7',18,15656)";
            //string sqly = @"select * from [User]";
            SqlCommand sqlCommand = new SqlCommand(sqly, conn);
            //sqlCommand.ExecuteNonQuery();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())//判断是否有数据--//读取一行记录
            {
                string username = reader.GetString(1);
                
            }
            
            reader.Close();
            conn.Close();
        }


    }
}
