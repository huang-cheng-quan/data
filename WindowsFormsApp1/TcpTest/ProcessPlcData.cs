using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.TcpTest
{
    public class ProcessPlcData
    {
        /// <summary>
        /// 皮带上料相机拍摄
        /// </summary>
        public string TakePic1 = "Tapc1#";
        /// <summary>
        /// 本体定位相机拍摄
        /// </summary>
        public string TakePic2 = "Tapc2#";
        /// <summary>
        /// 获得上料电机点击坐标
        /// </summary>
        public string GetCoordinate1 = "Gtcd1#";
        /// <summary>
        /// 获得定位电机电机坐标
        /// </summary>
        public string GetCoordinate2 = "Gtcd2#";
        /// <summary>
        /// 给上料电机发送机械坐标
        /// </summary>
        public string SendCoordinate1 = "Sdcd1";
        /// <summary>
        /// 给定位电机发送机械坐标
        /// </summary>
        public string SendCoordinate2 = "Sdcd2";
        /// <summary>
        /// PLC接受到的标志
        /// </summary>
        public string PlcReceiveCoordinateFlag = "PLRC";



        public string ProcessSendPlcData(string data) 
        {
            string str = null;
            str =  data+ "#";
            return str;
        }
        public PointF ProcessReceivePlcData(string data)
        {
            PointF point = new PointF();
            string str = "";
            str = data.Replace("#", "");
            string[] resAry = str.Split(new string[] { "," }, StringSplitOptions.None);
            
            point.X = float.Parse(resAry[1]);
            point.Y = float.Parse(resAry[2]);

            return point;
        }
    }
}
