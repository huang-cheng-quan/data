using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasicDemo;
using Camera_Capture_demo.GlobalVariable;
using Camera_Capture_demo.Helpers;
using Camera_Capture_demo.LoginFrms;
using Camera_Capture_demo.Models;
using TCP_助手;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        //载入配置
        ConfigInfo configInfo_All;
        //和PLC的连接
        string Tcp_IP = null;
        int Tcp_Port = 0;
        //整体变量
        int m_CamerNum = 8;
        public MainForm()
        {
            InitializeComponent();
        }
        private bool Login(int level)
        {
            LoginFrm frm = new LoginFrm(level);
            return (frm.ShowDialog() == DialogResult.OK);
        }

        private void 相机设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Login(0))
            {
                WindowHelper.ShowOrActiveForm<MV_CameraSettingForm>("MV_CameraSettingForm");
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ConfigVars.configInfo = XmlHelper.DeserializeFromXml<ConfigInfo>();
            configInfo_All = ConfigVars.configInfo;
            if (ConfigVars.configInfo.UserInfos == null)
            {
                ConfigVars.configInfo.UserInfos = new UserInfos()
                {
                    OperatorPwd = "1",
                    AdministratorPwd = "1",
                    DeveloperPwd = "1"
                };
            }
            if (ConfigVars.configInfo.tcp_Data != null)
            {
                Tcp_IP = ConfigVars.configInfo.tcp_Data.IP_Socket;
                Tcp_Port = ConfigVars.configInfo.tcp_Data.Port_Socket;
            }
            if (ConfigVars.configInfo.Camera_information.CamPara == null|| ConfigVars.configInfo.Camera_information.CamPara.Count==0)
            {
                List<CamPara> lCamPara = new List<CamPara>();
                for (int i = 0; i < m_CamerNum; i++)
                {
                    lCamPara.Add(new CamPara() { i });
                }
                Tcp_IP = ConfigVars.configInfo.tcp_Data.IP_Socket;
                Tcp_Port = ConfigVars.configInfo.tcp_Data.Port_Socket;
            }
            XmlHelper.SerializeToXml(ConfigVars.configInfo);
        }

        private void pLC设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Login(0))
            {
                WindowHelper.ShowOrActiveForm<TcpSocketForm>("TcpSocketForm");
            }
        }
    }
}
