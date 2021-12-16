using Camera_Capture_demo.GlobalVariable;
using Camera_Capture_demo.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TCP_助手
{
    public partial class TcpSocketForm : Form
    {
        public TcpSocketForm()
        {
            InitializeComponent();
        }
        

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        Socket SocketWitch;
        /// <summary>
        /// 创建一个Socket用于监听客户端连接，
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bnListen_Click(object sender, EventArgs e)
        {
            try
            {
                //创建一个socket用于监听
                SocketWitch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //提供一个IP地址，用于监控网络接口上的所有活动
                IPAddress ip = IPAddress.Any;
                IPEndPoint piont = new IPEndPoint(ip, Convert.ToInt32(txtPoint.Text));
                //绑定监听端口
                SocketWitch.Bind(piont);
                ShowMsg("监听成功");
                SocketWitch.Listen(10);
                //创建一个新线程
                Thread th = new Thread(Listen);
                th.IsBackground = true;
                th.Start(SocketWitch);
            }
            catch
            { }
        }

     
        /// <summary>
        /// 创建一个用于通讯的socket
        /// </summary>
        /// <param name="o"></param>
        void Listen(object o)
        {
            Socket SocketWitch = o as Socket;
            //创建一个用于通讯的socket,等待客服端连接
            try
            {
                while (true)
                {
                    //Socket SocketSend = SocketWitch.Accept();
                   Socket SocketSend = SocketWitch.Accept();
                    //获取远程主机的IP地址和端口号
                    ShowMsg(SocketSend.RemoteEndPoint.ToString() + "连接成功");

                    //创建一个新线程，用于接收客户端发送过来的消息
                    Thread th = new Thread(Reveice);
                    th.IsBackground = true;
                    th.Start(SocketSend);

                }
            }
            catch
            { }
        }

        Socket SocketSend;
        /// <summary>
        /// 不停的接收客服端发送过来的信息
        /// </summary>
        /// <param name="o"></param>
        void Reveice(object o)
        {
            SocketSend = o as Socket;
            //不停的接收客户端发送过来的信息
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024 * 1024 * 2];
                    int r = SocketSend.Receive(buffer);
                    if (r == 0)
                    {
                        break;
                    }
                    string str = Encoding.UTF8.GetString(buffer, 0, r);
                    ShowMsg(SocketSend.RemoteEndPoint.ToString() + ":" + str);
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 服务器发送消息到客服端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string str = Convert.ToString(txtSend.Text);
                byte[] buffer = Encoding.UTF8.GetBytes(str);
                //byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
                SocketSend.Send(buffer);
                txtSend2.AppendText(str + "\r\n");
            }
            catch
            { }
        }

        /// <summary>
        /// server显示输出
        /// </summary>
        /// <param name="str"></param>
        void ShowMsg(string str)
        {
            txtReceive.AppendText(str + "\r\n");
        }
        /// <summary>
        /// 窗体加载的时候取消对错误线程的监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TcpSocketForm_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            tabControl1.Enabled = false;
            for (int i = 0; i < 2; i++)
            {
                ConfigVars.configInfo.tcp_Data[i] = new Camera_Capture_demo.Models.TCP_data();
            }
           
        }




        /// <summary>
        /// 客服端创建一个通信的socket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        Socket SocketClient;
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //创建一个通信的socket
                SocketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //textbox5写入的值即为连接的服务器IP地址
                IPAddress ip = IPAddress.Parse(txt_Ip.Text);
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txt_Port.Text));
                //连接服务器
                SocketClient.Connect(point);
                ShowMsg1("连接成功");
                //创建一个新的线程用来接收服务器发送过来的数据
                Thread th = new Thread(Receice1);
                th.IsBackground = true;
                th.Start(SocketClient);
            }
            catch
            { }

        }


        /// <summary>
        /// 接收服务器发送过来的数据
        /// </summary>
        /// <param name="o"></param>
        void Receice1(object o)
        {
            SocketClient = o as Socket;
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024 * 1024 * 2];
                    int r = SocketClient.Receive(buffer);
                    if (r == 0)
                    {
                        break;
                    }
                    string str = Encoding.UTF8.GetString(buffer, 0, r);
                    ShowMsg1(SocketClient.RemoteEndPoint.ToString() + ":" + str);
                }
            }
            catch
            { }
        }


        /// <summary>
        /// 给服务器发送数据
        /// </summary>
        /// <param name="o"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string str = textBox1.Text.Trim();
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
                SocketClient.Send(buffer);
                textBox2.AppendText(str);
            }
            catch
            { }
        }


        /// <summary>
        /// 客服端显示接收过来的数据
        /// </summary>
        /// <param name="str"></param>
        void ShowMsg1(string str)
        {
            textBox3.AppendText(str + "\r\n");
        }

        /// <summary>
        /// 情况客服端接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
        }

        /// <summary>
        /// 清空客服端发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        /// <summary>
        /// 清空服务器接收的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            txtReceive.Clear();
        }


        /// <summary>
        /// 清空服务器发送的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            txtSend2.Clear();
        }

        private void btn_SaveTcpParameter_Click(object sender, EventArgs e)
        {
            try
            {
               
                ConfigVars.configInfo.tcp_Data[comboBox1.SelectedIndex].IP_Socket = txt_Ip.Text;
                ConfigVars.configInfo.tcp_Data[comboBox1.SelectedIndex].Port_Socket = int.Parse(txt_Port.Text);
                XmlHelper.SerializeToXml(ConfigVars.configInfo);
                MessageBox.Show("保存成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_SetTcpTarget_Click(object sender, EventArgs e)
        {
            /*ConfigVars.configInfo.tcp_Data[comboBox1.SelectedIndex] = new Camera_Capture_demo.Models.TCP_data();*/
            ConfigVars.configInfo.tcp_Data[comboBox1.SelectedIndex].Cam_Number = comboBox1.SelectedIndex;
            tabControl1.Enabled = true;
            txt_CamNum.Text = comboBox1.SelectedIndex.ToString();
            label9.Text = comboBox1.SelectedItem.ToString();

        }
    }
}
