using Camera_Capture_demo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WindowsFormsApp1.TcpTest
{
    public class PlcConnection
    {
        public delegate void delegateAcceptePLCMessage1(string AccepteMessage);
        public event delegateAcceptePLCMessage1 EventAcceptePLCMessage1;
        public delegate void delegateAcceptePLCMessage2(string AccepteMessage);
        public event delegateAcceptePLCMessage2 EventAcceptePLCMessage2;

        Socket SocketClient;
        Socket m_SocketClient1;
        Socket m_SocketClient2;
        //和PLC的连接
        string m_sTcpIP1 = null;
        int m_iTcpPort1 = 0;
        string m_sTcpIP2 = null;
        int m_iTcpPort2 = 0;
        bool m_bWhileReceivedataFlag1 = true;
        bool m_bWhileReceivedataFlag2 = true;

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

    }
}
