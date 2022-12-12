using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using Camera_Capture_demo.GlobalVariable;

namespace WindowsFormsApp1.SqlServer
{
    public partial class SqlConnectionForm : Form
    {
		operateSql operatesal = new operateSql();
		
		
		public SqlConnectionForm()
        {
            InitializeComponent();
        }
		
		private void btnSQLOK_Click(object sender, EventArgs e)
        {
			string connectionString = $"server={this.txtIP.Text.Trim()};" +
				$"database={this.txtDataBase.Text.Trim()};" +
				$"uid={this.txtUser.Text.Trim()};" +
				$"pwd={this.txtPwd.Text.Trim()};";
			SqlHelper.connStr = connectionString;
			try
			{
				/*using (SqlConnection sqlConnection = new SqlConnection(connectionString))
				{
					sqlConnection.Open();
					
					sqlConnection.Close();
				}*/
				SqlConnection sqlConnection = SqlHelper.Connection;
                if (sqlConnection!=null)
                {
					MessageBox.Show("测试成功！");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        private void btn_CreateTable_Click(object sender, EventArgs e)
        {
            try
            {
				string strText = string.Empty;
				InputDialog.Show(out strText);
				operatesal.CreateDataTable(this.txtDataBase.Text.Trim(), strText);
			}
            catch (Exception ex)
            {
				MessageBox.Show(ex.ToString());
                throw;
            }
			
		}

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (1== operatesal.addData(SqlHelper.Connection, "nvtTable", "1", "2", "3", "4"))

            {
				MessageBox.Show("添加成功！");
            }
            else
            {
				MessageBox.Show("添加失败！");

			}
			

		}
        List<nvtTable> nvtd = new List<nvtTable>();

        //数据库查询语句，也叫数据库命令
        string sqlStrCmd = "select * from nvtTable;";
        SqlCommand sqlCmd;
        SqlDataReader reader;
        //创建cmd对象
        
        private void btnGetData_Click(object sender, EventArgs e)
        {
            /*DataSet trds = operatesal.select(SqlHelper.Connection,"nvtTable");
			this.GridView1.AutoGenerateColumns = false;

			this.GridView1.DataSource = trds.Tables[0];*/
           
           
            sqlCmd = new SqlCommand(sqlStrCmd, SqlHelper.Connection);
            //将执行数据库语句命令结果返回文本传给reader，只能一行一行读取
            reader = sqlCmd.ExecuteReader();

            //判断有没有读取到数据，实际是判断有没有读取到行数据，可以不写
            if (reader.HasRows)
            {
               
                //读取数据
                //如果读取到数据返回true，否则false
                while (reader.Read())
                {
                    
                    //在数据集合加入数据，
                    nvtd.Add(
                    //添加数据库数据到list
                    new nvtTable()
                    {
                        SerialNumber = reader["序号"].ToString(),
                        Sample = reader["样本名称"].ToString(),
                        OKorNG = reader["缺陷种类"].ToString(),
                        Code = reader["Code"].ToString(),
                        Score = reader["Score"].ToString()
                    });
                }
            }
            reader.Close();

          
            //将数据添加到dataGridView中显示
            this.GridView1.DataSource = nvtd;


        }
		public class nvtTable
		{
			//样本名称,缺陷种类,Code,Score
			public string SerialNumber { get; set; }
            public string Sample { get; set; }
            public string OKorNG { get; set; }
            public string Code { get; set; }
            public string Score { get; set; }

        }

        private void SqlConnectionForm_Load(object sender, EventArgs e)
        {
            this.GridView1.AutoGenerateColumns = false;//禁止自动生成
        }

        private void btn_SaveSqlParam_Click(object sender, EventArgs e)
        {
            ConfigVars.configInfo.sqldata.server = this.txtIP.Text.Trim();
            ConfigVars.configInfo.sqldata.database = this.txtDataBase.Text.Trim();
            ConfigVars.configInfo.sqldata.UserId = this.txtUser.Text.Trim();
            ConfigVars.configInfo.sqldata.password = this.txtPwd.Text.Trim();
        }
    }
}
