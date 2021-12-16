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
using Camera_Capture_demo.VisionFrms;
using Camera_Capture_demo.GlobalVariable;
using Camera_Capture_demo.Helpers;
using Camera_Capture_demo.LoginFrms;
using Camera_Capture_demo.Models;
using TCP_助手;
using WindowsFormsApp1.Models;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using BatteryFeederDemo;
using WindowsFormsApp1.TcpTest;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        //载入配置
        ConfigInfo configInfo_All;
        //和PLC的连接
        string m_sTcpIP1 = null;
        int m_iTcpPort1 = 0;
        string m_sTcpIP2 = null;
        int m_iTcpPort2 = 0;
        MotionProcess motionProcess;

        //整体变量
        int m_CamerNum = 8;
        //TcpSocket类接口
        Socket SocketClient;
        Socket m_SocketClient1;
        Socket m_SocketClient2;
        public delegate void delegateAcceptePLCMessage1(string AccepteMessage);
        public event delegateAcceptePLCMessage1 EventAcceptePLCMessage1;
        public delegate void delegateAcceptePLCMessage2(string AccepteMessage);
        public event delegateAcceptePLCMessage2 EventAcceptePLCMessage2;
        bool m_bWhileReceivedataFlag1 = true;
        bool m_bWhileReceivedataFlag2 = true;
        //上料类文件

        PlcConnection plcconnection = new PlcConnection();
        public MainForm()
        {
            InitializeComponent();
        }
        private bool Login(int level)
        {
            LoginFrm frm = new LoginFrm(level);
            return (frm.ShowDialog() == DialogResult.OK);
        }
        #region TcpIp通讯部分和PLc的通讯
        /// <summary>
        /// 给Tcp服务器发送数据
        /// </summary>
        /// <param name="str"></param>
        public void TcpSendMessage(string str)
        {
            try
            {
                LogHelper.LogInfo("Tcp Send message:" + str);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
                SocketClient.Send(buffer);
                LogHelper.LogInfo(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public bool Tcp_connection(int CamerNum, string IP, int PORT)
        {
            bool isConnectionTcp = false;
            try
            {
                //创建一个通信的socket
                SocketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //textbox5写入的值即为连接的服务器IP地址
                IPAddress ip = IPAddress.Parse(IP);
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(PORT));
                if (CamerNum == 1)
                {
                    m_SocketClient1 = SocketClient;
                    //连接服务器
                    SocketClient.Connect(point);
                    isConnectionTcp = true;
                    //创建一个新的线程用来接收服务器发送过来的数据
                    Thread th1 = new Thread(Receicedata1);
                    th1.IsBackground = true;
                    th1.Start(SocketClient);
                }
                else
                {
                    m_SocketClient2 = SocketClient;
                    //连接服务器
                    SocketClient.Connect(point);
                    isConnectionTcp = true;
                    //创建一个新的线程用来接收服务器发送过来的数据
                    Thread th2 = new Thread(Receicedata2);
                    th2.IsBackground = true;
                    th2.Start(SocketClient);
                }



            }
            catch (Exception ex)
            {
                isConnectionTcp = false;
                MessageBox.Show(ex.ToString());
            }
            return isConnectionTcp;
        }
        private void ConnectPlcClose()
        {
            m_bWhileReceivedataFlag1 = false;
            m_bWhileReceivedataFlag2 = false;

            if (m_SocketClient1 != null)
            {
                m_SocketClient1.Close();
            }
            if (m_SocketClient2 != null)
            {
                m_SocketClient2.Close();
            }

        }
        public void Receicedata1(object o)
        {
            m_SocketClient1 = o as Socket;
            try
            {
                while (m_bWhileReceivedataFlag1)
                {
                    byte[] buffer = new byte[1024 * 1024 * 2];
                    int r = m_SocketClient1.Receive(buffer);
                    if (r == 0)
                    {
                        break;
                    }
                    string str = Encoding.UTF8.GetString(buffer, 0, r);
                    EventAcceptePLCMessage1(str);
                    LogHelper.LogInfo("Tcp Receice message:" + str);
                    string[] tmp = str.Split('#');//单个字符分割
                    str = tmp[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Receicedata2(object o)
        {
            m_SocketClient2 = o as Socket;
            try
            {
                while (m_bWhileReceivedataFlag2)
                {
                    byte[] buffer = new byte[1024 * 1024 * 2];
                    int r = m_SocketClient2.Receive(buffer);
                    if (r == 0)
                    {
                        break;
                    }
                    string str = Encoding.UTF8.GetString(buffer, 0, r);
                    EventAcceptePLCMessage2(str);
                    LogHelper.LogInfo("Tcp Receice message:" + str);
                    string[] tmp = str.Split('#');//单个字符分割
                    str = tmp[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion
        private bool CheckHImageFrmIsOpen()
        {
            return WindowHelper.CheckFormIsOpen("CalibrationFrm0") || WindowHelper.CheckFormIsOpen("CalibrationFrm1")
                || WindowHelper.CheckFormIsOpen("CreateShapeModelFrm");
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
            MotionProcess motionProcess = new MotionProcess(m_SocketClient1, m_SocketClient2);
            EventAcceptePLCMessage1 += motionProcess.ReceivePlcFeedBeltData;
            EventAcceptePLCMessage2 += motionProcess.ReceivePlcFeedPositive;
            if (ConfigVars.configInfo.UserInfos == null)
            {
                ConfigVars.configInfo.UserInfos = new UserInfos()
                {
                    OperatorPwd = "1",
                    AdministratorPwd = "1",
                    DeveloperPwd = "1"
                };
            }
            if (ConfigVars.configInfo.tcp_Data.Count == 2)
            {
                m_sTcpIP1 = ConfigVars.configInfo.tcp_Data[0].IP_Socket;
                m_iTcpPort1 = ConfigVars.configInfo.tcp_Data[0].Port_Socket;
                m_sTcpIP2 = ConfigVars.configInfo.tcp_Data[1].IP_Socket;
                m_iTcpPort2 = ConfigVars.configInfo.tcp_Data[1].Port_Socket;
            }
            else
            {
                List<TCP_data> tcpdata = new List<TCP_data>();
                for (int i = 0; i < 2; i++)
                {
                    tcpdata.Add(new TCP_data() { Cam_Number = i });
                }
                ConfigVars.configInfo.tcp_Data = tcpdata;
            }
            if (ConfigVars.configInfo.Cameras == null || ConfigVars.configInfo.Cameras.Count == 0)
            {
                List<MvClass> cameras = new List<MvClass>();
                for (int i = 0; i < 8; i++)
                {
                    cameras.Add(new MvClass() { CameraNo = i });
                }
                ConfigVars.configInfo.Cameras = cameras;
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

        private void 标定设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 坐标标定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("CalibrationFrm0"))
                {
                    CalibrationFrm frm = new CalibrationFrm(0, SocketClient);
                    EventAcceptePLCMessage1 = new delegateAcceptePLCMessage1(frm.ReceivePlcMessage);
                    frm.Show();
                }
            }
        }

        private void btn_OpenPlc_Click(object sender, EventArgs e)
        {
            if (Tcp_connection(1, m_sTcpIP1, m_iTcpPort1))
            {
                MessageBox.Show("TCP1连接成功");
            }
            else
            {
                MessageBox.Show("TCP1连接失败");
            }
            if (Tcp_connection(2, m_sTcpIP2, m_iTcpPort2))
            {
                MessageBox.Show("TCP2连接成功");
            }
            else
            {
                MessageBox.Show("TCP1连接失败");
            }
        }

        private void btn_FeedBelt_Click(object sender, EventArgs e)
        {
            if (btn_FeedBelt.Text == "启动")
            {
                if (motionProcess == null || !motionProcess.is_init)
                {
                    /*motionProcess = MotionProcess.GetInstance();*/
                    motionProcess.Init();
                }
                if (CheckHImageFrmIsOpen())
                {
                    MessageBox.Show("请先关闭设置窗口");
                    return;
                }
                if (motionProcess.is_init)
                {
                    motionProcess.abort = false;
                    btn_FeedBelt.Text = "停止";
                }
            }
            else
            {
                motionProcess.abort = true;
                btn_FeedBelt.Text = "启动";
            }
        }

        private void 坐标标定本体定位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("CalibrationFrm0"))
                {
                    CalibrationFrm frm = new CalibrationFrm(1, SocketClient);
                    EventAcceptePLCMessage2 = new delegateAcceptePLCMessage2(frm.ReceivePlcMessage);
                    frm.Show();
                }
            }
        }

        private void 模板建立ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (motionProcess != null && !motionProcess.abort)
            {
                MessageBox.Show("设备运行中,设置界面不可打开");
                return;
            }
            if (Login(1))
            {
                WindowHelper.ShowOrActiveForm<CreateShapeModelFrm>("CreateShapeModelFrm");
            }
        }

    }
}
