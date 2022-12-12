using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Camera_Capture_demo.GlobalVariable;
using Camera_Capture_demo.HalconVision;
using Camera_Capture_demo.Helpers;
using Camera_Capture_demo.Models;
using BatteryFeederDemo;
using HslCommunication.Profinet.Omron;

namespace Dyestripping.Models
{
    /// <summary>
    /// 电机控制类
    /// </summary>
    class MotorsClass
    {
        public static  OmronFinsNet omronInstance;
        public static string plc_cam_status4 = "D1012";////读写 0待机 1拍照 2拍照OK 3拍照NG 4拍照异常
        public static string plc_cam_status8 = "D1212";////读写 0待机 1拍照 2拍照OK 3拍照NG 4拍照异常
        public static string plc_cam_status6 = "D1312";////读写 0待机 1拍照 2拍照OK 3拍照NG 4拍照异常

       /* public static string plc_cam_status5_A = plc_cam_status8;////读写 0待机 1拍照 2拍照OK 3拍照NG 4拍照异常
        public static string plc_cam_status5_B = plc_cam_status6;////读写 0待机 1拍照 2拍照OK 3拍照NG 4拍照异常*/
        public static string plc_cam_MotorFlag5 = "D1312";////钢片检测电机标志位


        string plc_r_motor_x = "D3014";//电机X位置
        string plc_r_motor_y = "D3056";//电机Y位置
        string plc_r_motor_u = "D3074";//电机U位置
        //上料定位B
        public static string plc_Upload_motor_xb = "D3032";//电机X位置
        public static string plc_Upload_motor_yb = "D3034";//电机Y位置
        public static string plc_Upload_motor_ub = "D3064";//电机U位置
        //上料定位A
        public static string plc_Upload_motor_xa = "D3032";//电机X位置
        public static string plc_Upload_motor_ya = "D3034";//电机Y位置
        public static string plc_Upload_motor_ua = "D3062";//电机U位置

        //连接器定位B
        public static string plc_Connection_motor_xb = "D3032";//电机X位置
        public static string plc_Connection_motor_yb = "D3034";//电机Y位置
        public static string plc_Connection_motor_ub = "D3064";//电机U位置
        //连接器定位A
        public static string plc_Connection_motor_xa = "D3032";//电机X位置
        public static string plc_Connection_motor_ya = "D3034";//电机Y位置
        public static string plc_Connection_motor_ua = "D3062";//电机U位置

        //上料定位B
        public static string plc_Upload_Write_motor_xb = "D1300";//电机X位置
        public static string plc_Upload_Write_motor_yb = "D1302";//电机Y位置
        public static string plc_Upload_Write_motor_ub = "D1304";//电机U位置
        //上料定位A
        public static string plc_Upload_Write_motor_xa = "D1200";//电机X位置
        public static string plc_Upload_Write_motor_ya = "D1202";//电机Y位置
        public static string plc_Upload_Write_motor_ua = "D1204";//电机U位置
       
        //二维码读取地址
        //上料（watch）
        public static string plc_Qrcode_motor_FeldBelt = "D1020";
        //连接器反面
        public static string plc_Qrcode_motor_ConnectorNegative = "D1120";
        //连接器反面二维码到位信号
        public static string plc_Qrcode_motor_ConnectorNegativeGet = "D1112";
        //本体上料B
        public static string plc_Qrcode_motor_FeedPositive1 = "D1320";
        //连接器正面检测
        public static string plc_Qrcode_motor_ConnectorPositive = "D1420";
        //连接器正面二维码到位信号
        public static string plc_Qrcode_motor_ConnectorPositiveGet = "D1412";
        //本体上料A
        public static string plc_Qrcode_motor_FeedPositive2 = "D1220";
        //侧面短边
        public static string plc_Qrcode_motor_ShortSide = "D1520";
        //反面
        public static string plc_Qrcode_motor_Negative = "D1620";
        //侧面长边
        public static string plc_Qrcode_motor_LongSide = "D1720";
        //正面检测
        public static string plc_Qrcode_motor_Positive = "D1820";
        //上料中转扫码地址（iPhone）
        public static string plc_Qrcode_motor_startread = "D1920";
        //上料中转扫码地址（iPhone)到位信号
        public static string plc_Qrcode_motor_startreadGet = "D1912";
        //下料二维码校验A
        public static string plc_Qrcode_motor_checkA = "D2220";
        //下料二维码校验B
        public static string plc_Qrcode_motor_checkB = "D2320";
        //下料结果返回A
        public static string plc_motor_resultA = "D2206";
        //下料结果返回B
        public static string plc_motor_resultB = "D2306";
        //侧面短边收到信号标志
        public static string plc_Qrcode_motor_ShortSide_Flag = "D1590";
        //public  string plc_Result_motor = "D2006";
        //治具二维码
        public static string plc_motor_FixtureA = "D4520";
        //治具二维码收到信号标志
        public static string plc_Qrcode_motor_Fixture_Flag = "D1592";
        //奇数清料，清除连接器结果
        public static string plc_motor_ClearConnectInfo = "D4002";
        //复检产品判断
        public static string plc_motor_RecheckIDA = "D4004";
        public static string plc_motor_RecheckIDB = "D4006";
        //正面检测
        public static string plc_DiskCheck = "D1810";

        //设备点检
        public static string plc_MachineCheckState = "D4014";
        public static string plc_MachineCheck = "D4010";
        public static string plc_MachineCheck1 = "D4012";

        string plc_w_pick_x = "D1000";//电机X位置
        string plc_w_pick_y = "D1002";//电机Y位置
        string plc_w_pick_u = "D1004";//电机U位置(补偿值)

       
        //临时
        string plc_r_motor_x_Tmp = "";//电机X位置
        string plc_r_motor_y_Tmp = "";//电机Y位置
        string plc_r_motor_u_Tmp = "";//电机U位置

        public MotorsClass(int plcNo)
        {
            omronInstance = OmronPlcFactory.GetInstance(plcNo);
            omronInstance.ConnectServer();
            if (!omronInstance.ConnectServer().IsSuccess)
            {
                //omronInstance.ConnectClose();
                //omronInstance.Dispose();
                //omronInstance = OmronPlcFactory.GetInstance(plcNo);
                omronInstance.ConnectServer();
            }
        }
        /// <summary>
        /// 获取模组当前的点位
        /// </summary>
        /// <returns></returns>
        public PointXYU GetCurrentPos(int Num)
        {
            switch (Num)
            {
                case 4:
                    plc_r_motor_x_Tmp = plc_r_motor_x;
                    plc_r_motor_y_Tmp = plc_r_motor_y;
                    plc_r_motor_u_Tmp = plc_r_motor_u;
                    break;
                case 6:
                    plc_r_motor_x_Tmp = plc_Upload_motor_xb;
                    plc_r_motor_y_Tmp = plc_Upload_motor_yb;
                    plc_r_motor_u_Tmp = plc_Upload_motor_ub;
                    break;
                case 8:
                    plc_r_motor_x_Tmp = plc_Upload_motor_xa;
                    plc_r_motor_y_Tmp = plc_Upload_motor_ya;
                    plc_r_motor_u_Tmp = plc_Upload_motor_ua;
                    break;
                case 51:
                    plc_r_motor_x_Tmp = plc_Connection_motor_xa;
                    plc_r_motor_y_Tmp = plc_Connection_motor_ya;
                    plc_r_motor_u_Tmp = plc_Connection_motor_ua;
                    break;
                case 52:
                    plc_r_motor_x_Tmp = plc_Connection_motor_xa;
                    plc_r_motor_y_Tmp = plc_Connection_motor_ya;
                    plc_r_motor_u_Tmp = plc_Connection_motor_ua;
                    break;

                default:
                    break;
            }
            PointXYU point = new PointXYU();
            //lock (MotionProcess.HslCommunicationlock)
            //{
                HslCommunication.OperateResult<float[]> resultx = omronInstance.ReadFloat(plc_r_motor_x_Tmp, 1);
                HslCommunication.OperateResult<float[]> resulty = omronInstance.ReadFloat(plc_r_motor_y_Tmp, 1);

                if (!resultx.IsSuccess)
                {
                    MessageBox.Show("PLC通讯失败:" + resultx.Message);
                    return null;
                }
                point.X = resultx.Content[0];
                point.Y = resulty.Content[0];
                point.U = 0;
            //}
           
            return point;
        }
        /// <summary>
        /// 电机点动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="direction"></param>
        public void JogMove(string axis, string direction)
        {
            int channel, bit;
            switch (direction)
            {
                case "P":
                    bit = 8;
                    break;
                case "N":
                    bit = 9;
                    break;
                default:
                    bit = 0;
                    break;
            }
            switch (axis)
            {
                case "X":
                    channel = 310;
                    break;
                case "Y":
                    channel = 320;
                    break;
                case "Z":
                    channel = 330;
                    break;
                case "R":
                    channel = 340;
                    break;
                default:
                    channel = 0;
                    break;
            }
            if (channel != 0)
            {
                int address = channel * 16 + bit;
                //lock (MotionProcess.HslCommunicationlock)
                //{
                    omronInstance.Write($"M{address}", true);
                    Thread.Sleep(50);
                    omronInstance.Write($"M{address}", false);
                //}
               
            }
        } 
        /// <summary>
        /// 发送放料点
        /// </summary>
        /// <param name="points"></param>
        public void SendPoint(PointXYU[] points, int Num)
        {
            if (points == null)
                return;

            switch ( Num)
            {
                case 4:
                    plc_r_motor_x_Tmp = plc_w_pick_x;
                    plc_r_motor_y_Tmp = plc_w_pick_y;
                    plc_r_motor_u_Tmp = plc_w_pick_u;
                    break;
                    //B
                case 6:
                    plc_r_motor_x_Tmp = plc_Upload_Write_motor_xb;
                    plc_r_motor_y_Tmp = plc_Upload_Write_motor_yb;
                    plc_r_motor_u_Tmp = plc_Upload_Write_motor_ub;
                    break;
                    //A
                case 8:
                    plc_r_motor_x_Tmp = plc_Upload_Write_motor_xa;
                    plc_r_motor_y_Tmp = plc_Upload_Write_motor_ya;
                    plc_r_motor_u_Tmp = plc_Upload_Write_motor_ua;
                    break;
                case 51:
                    plc_r_motor_x_Tmp = plc_Upload_Write_motor_xa;
                    plc_r_motor_y_Tmp = plc_Upload_Write_motor_ya;
                    plc_r_motor_u_Tmp = plc_Upload_Write_motor_ua;
                    break;
                case 52:
                    plc_r_motor_x_Tmp = plc_Upload_Write_motor_xb;
                    plc_r_motor_y_Tmp = plc_Upload_Write_motor_yb;
                    plc_r_motor_u_Tmp = plc_Upload_Write_motor_ub;
                    break;
                default:
                    break;
            }
           
                if (!omronInstance.Write(plc_r_motor_x_Tmp, points[0].X).IsSuccess)
                {
                    MessageBox.Show("PLC通讯失败");

                }

                omronInstance.Write(plc_r_motor_x_Tmp, points[0].X);
                omronInstance.Write(plc_r_motor_y_Tmp, points[0].Y);
                omronInstance.Write(plc_r_motor_u_Tmp, points[0].U);
            if (!omronInstance.Write(plc_r_motor_x_Tmp, points[0].X).IsSuccess)
            {
                omronInstance.Write(plc_r_motor_x_Tmp, points[0].X);
                LogHelper.LogError("视觉定位二次写入");
            }
            //else
            //{
            //    LogHelper.LogError(Num+"视觉定位写入X" + points[0].X.ToString());
            //}
            if (!omronInstance.Write(plc_r_motor_y_Tmp, points[0].Y).IsSuccess)
            {
                omronInstance.Write(plc_r_motor_y_Tmp, points[0].Y);
            }
            //else
            //{
            //    LogHelper.LogError(Num+"视觉定位写入Y" + points[0].Y.ToString());
            //}
            if (!omronInstance.Write(plc_r_motor_u_Tmp, points[0].U).IsSuccess)
            {
                omronInstance.Write(plc_r_motor_u_Tmp, points[0].U);
            }
            //else
            //{
            //    LogHelper.LogError(Num+"视觉定位写入U" + points[0].U.ToString());
            //}
           
        }
       
        /// <summary>
        /// 添加补偿值
        /// </summary>
        /// <param name="points">点位坐标</param>
        /// <param name="toolNo">补偿的标志</param>
        /// <param name="isFound">是否发现</param>
        /// <returns></returns>
        public PointXYU[] CalPickPoint(List<PointXYU> points, int toolNo , out bool[] isFound)
        {
            isFound = new bool[1];
            if (points[0] == null || points.Count == 0)
            {
                LogHelper.LogError("视觉系统未定位到电池,请尝试重新拍照获取");
                return null;
            }
            ToolInfos toolInfo = ConfigVars.configInfo.ToolInfos;
            PointXYU tcpTran = new PointXYU();
            if (toolInfo != null)
            {
                switch (toolNo)
                {
                    case -1:
                        tcpTran.X = toolInfo.Xoffset3;
                        tcpTran.Y = toolInfo.Yoffset3;
                        break;
                    case 0:
                        tcpTran.X = toolInfo.Xoffset1;
                        tcpTran.Y = toolInfo.Yoffset1;
                        break;
                    case 1:
                        tcpTran.X = toolInfo.Xoffset2;
                        tcpTran.Y = toolInfo.Yoffset2;
                        break;
                    case 2:
                        tcpTran.X = toolInfo.Xoffset4;
                        tcpTran.Y = toolInfo.Yoffset4;
                        break;
                    case 3:
                        tcpTran.X = toolInfo.Xoffset5;
                        tcpTran.Y = toolInfo.Yoffset5;
                        break;
                    default:
                        break;
                }
              
            }
            PointXYU[] pickPoints = new PointXYU[2] {new PointXYU(),new PointXYU()};
            if (points[0]!=null)
            {
                pickPoints[0].X = points[0].X + tcpTran.X;
                pickPoints[0].Y = points[0].Y + tcpTran.Y;
                pickPoints[0].U = points[0].U;

                //pickPoints[0] = HalconOperator.ConvertTcpToTool0(points[0], tcpTran);
            }
            
            for (int i = 0; i < 1; i++)
            {
                isFound[i] = (points[i] != null);
            }
            return pickPoints;
        }

        private int ConvertBoolArrayToInt(bool[] boolArray)
        {
            int boolInt = 0;
            for (int i = 0; i < boolArray.Length; i++)
                if (boolArray[i])
                    boolInt |= 1 << (i);
            return boolInt;
        }
    }
}
