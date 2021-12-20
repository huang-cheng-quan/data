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

using HslCommunication.Profinet.Omron;

namespace Dyestripping.Models
{
    /// <summary>
    /// 电机控制类
    /// </summary>
    class MotorsClass
    {
        private OmronFinsNet omronInstance;

        string plc_r_motor_x = "D8000";//电机X位置
        string plc_r_motor_y = "D8002";//电机Y位置
        string plc_r_motor_u = "D8004";//电机U位置

        string plc_w_pick_x = "D10010";//电机X位置
        string plc_w_pick_y = "D10012";//电机Y位置
        string plc_w_pick_u = "D10014";//电机U位置(补偿值)

        public MotorsClass(int plcNo)
        {
            omronInstance = OmronPlcFactory.GetInstance(plcNo);
        }
        /// <summary>
        /// 获取模组当前的点位
        /// </summary>
        /// <returns></returns>
        public PointXYU GetCurrentPos()
        {
            PointXYU point = new PointXYU();
            HslCommunication.OperateResult<int[]> result = omronInstance.ReadInt32(plc_r_motor_x, 2);
            if (!result.IsSuccess)
            {
                MessageBox.Show("PLC通讯失败:" + result.Message);
                return null;
            }
            point.X = result.Content[0] / 100.0f;
            point.Y = result.Content[1] / 100.0f;
            point.U = 0;
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
                omronInstance.Write($"M{address}", true);
                Thread.Sleep(50);
                omronInstance.Write($"M{address}", false);
            }
        } 
        /// <summary>
        /// 发送放料点
        /// </summary>
        /// <param name="points"></param>
        public void SendPoint(PointXYU[] points)
        {
            if (points == null)
                return;
            int[] values = new int[4];
            values[0] = (int)(points[0].X * 100);
            values[1] = (int)(points[0].Y * 100);
            values[2] = (int)(points[1].X * 100);
            values[3] = (int)(points[1].Y * 100);
            //values[2] = (int)(point.U * 100);
            omronInstance.Write(plc_w_pick_x, values);
        }

        public PointXYU[] CalPickPoint(List<PointXYU> points, int toolNo , out bool[] isFound)
        {
            isFound = new bool[1];
            if (points == null || points.Count == 0)
            {
                LogHelper.LogError("视觉系统未定位到电池,请尝试重新拍照获取");
                return null;
            }
            RobotInfo robotInfo = ConfigVars.robotInfo;
            PointXYU tcpTran = new PointXYU();
            if (robotInfo != null && robotInfo.Offsets.Count > 0)
            {
                PickOffset pickOffset = robotInfo.Offsets.Find(c => c.PickStation == (toolNo));

                tcpTran.X = pickOffset.OffsetX;
                tcpTran.Y = pickOffset.OffsetY;
            }
            PointXYU[] pickPoints = new PointXYU[2] {new PointXYU(),new PointXYU()};
            if (points[0]!=null)
            {     
                pickPoints[0] = HalconOperator.ConvertTcpToTool0(points[0], tcpTran);
            }
            
            for (int i = 0; i < points.Count; i++)
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
