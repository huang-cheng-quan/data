using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HalconDotNet;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using Camera_Capture_demo.HalconVision;
using Camera_Capture_demo.Models;
using Camera_Capture_demo.Helpers;
using Camera_Capture_demo.GlobalVariable;
using WindowsFormsApp1;
using System.Net.Sockets;
using WindowsFormsApp1.TcpTest;

namespace BatteryFeederDemo
{
    class MotionProcess
    {
        Socket m_socketPlcFeedBelt;
        Socket m_socketPlcFeedPositive;
        int CamNumber = 8;
        ProcessPlcData process_plc_data = new ProcessPlcData();
        // 定义一个静态变量来保存类的实例
        private static MotionProcess mpInstance;
        // 定义一个标识确保线程同步
        private static readonly object locker = new object();

        public bool abort = true;//设备运行停止标识
        //YamahaClass yamahaObj;
        HalconOperator halconOperator;
        //KeyenceMcNet keyenceInstance;
        List<MvClass> m_listMvCameras = new List<MvClass>();

        Thread motion_thread;//设备运行线程

        public delegate void ImgDispHandler(HObject hImage, int camNo);
        public static event ImgDispHandler OnImgDisp;

        public delegate void PositionDispHandler(int current, int total, int tooNo, PointXYU pointXYU);
        public static event PositionDispHandler OnPositionDisp;

        private delegate void SendPositionDelegate(PointXYU pointXYUs, int battery_index, int tool_index);
        private event SendPositionDelegate SendPositionEvent;

        bool plc_connect = false;//PLC是否连接
        bool mvcamera_connect = false;//相机是否连接
        public bool is_init;//是否初始化连接完成
        /// <summary>
        /// 设备自动 MR12000
        /// </summary>
        string plc_r_run = "M1920";//设备自动 MR12000
        /// <summary>
        /// 触发拍照
        /// </summary>
        string plc_r_cam = "M1936";//触发拍照 MR12100
        /// <summary>
        /// 获取位置
        /// </summary>
        string plc_r_position = "M1939";//获取位置 MR12103
        /// <summary>
        /// 取电池序号
        /// </summary>
        string plc_r_batteryNo = "D1000";//取电池序号
        /// <summary>
        /// 取电池机械手序号
        /// </summary>
        string plc_r_toolNo = "D1002";//取电池机械手序号
        /// <summary>
        /// 相机拍照点坐标
        /// </summary>
        string plc_r_cam_x = "D8116";//相机拍照点坐标
        /// <summary>
        /// 拍照完成
        /// </summary>
        string plc_w_cam_completed = "M1937";//拍照完成 MR12101
        /// <summary>
        /// 电池总数
        /// </summary>
        string plc_w_battery_n = "D1010";//电池总数
        /// <summary>
        /// 取料X轴位置
        /// </summary>
        string plc_w_battery_x = "D1012";//取料X轴位置
        /// <summary>
        /// 取料Y轴位置
        /// </summary>
        string plc_w_battery_y = "D1014";//取料Y轴位置
        /// <summary>
        /// 取料U轴位置
        /// </summary>
        string plc_w_battery_u = "D1016";//取料U轴位置

        float cam_pos;//拍照时相机位置
        bool isCamCompleted1, isCamCompleted2;//相机1、2图像处理完成标识
        PointXYU pointXYUs1;//相机1获取的电池点位集合
        PointXYU pointXYUs2;//相机2获取的电池点位集合
        PointXYU allPointXYUs;//所有相机获取的电池点位集合
        Stopwatch photoWatch = new Stopwatch();
        MainForm mf = new MainForm();
        string m_sReceiveMessage1 = "";
        string m_sReceiveMessage2 = "";
        int m_icamNo;//相机序号
        public MotionProcess(Socket socket1,Socket socket2)
        {
            m_socketPlcFeedBelt = socket1;
            m_socketPlcFeedPositive = socket2;
            SendPositionEvent += SendPosition2Motor;

            //keyenceInstance = KeyencePlcFactory.GetInstance();
            for (int i = 0; i < CamNumber; i++)
            {
                m_listMvCameras.Add(MvClass.GetInstance(i));
            }
            if (m_listMvCameras[0]!=null&& m_listMvCameras[1] != null)
            {
                m_listMvCameras[0].eventProcessImage += MotionProcess_eventProcessImage1;
                m_listMvCameras[1].eventProcessImage += MotionProcess_eventProcessImage2;
            }
           
            halconOperator = new HalconOperator();
        }

        private void MotionProcess_eventProcessImage2(HObject hImage)
        {
            if (!abort)
                OnImgDisp?.Invoke(hImage, 2);
        }

        private void MotionProcess_eventProcessImage1(HObject hImage)
        {
            if (!abort)
                OnImgDisp?.Invoke(hImage, 1);
        }


        /*public static MotionProcess GetInstance()
        {
            if (mpInstance == null)
            {
                lock (locker)
                {
                    if (mpInstance == null)
                    {
                        mpInstance = new MotionProcess();
                    }
                }
            }
            return mpInstance;
        }*/

        public void Init()
        {
            if (is_init)
            {
                return;
            }
            try
            {
                if (!mvcamera_connect)
                {
                    mvcamera_connect = true;
                    for (int i = 0; i < CamNumber; i++)
                    {
                        if (!m_listMvCameras[i].isOpen)
                        {
                            m_listMvCameras[i].OpenCam();
                        }
                        mvcamera_connect = m_listMvCameras[i].isOpen && mvcamera_connect;
                    }

                    if (mvcamera_connect)
                    {
                        LogHelper.LogInfo("相机连接成功！");
                    }
                    else
                    {
                        LogHelper.LogError("相机连接失败！");
                    }
                }
                if (plc_connect && mvcamera_connect)
                {
                    is_init = true;
                    Start();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message);
            }
        }

        private void Start()
        {
            motion_thread = new Thread(new ThreadStart(DeviceMotionThreadFeedBelt));
            motion_thread.Name = "设备自动工作线程";
            motion_thread.Start();
            motion_thread = new Thread(new ThreadStart(DeviceMotionThreadFeedPositive));
            motion_thread.Name = "设备自动工作线程";
            motion_thread.Start();
        }

        public void Exit()
        {
            if (motion_thread != null)
                motion_thread.Abort();          
        }
        /// <summary>
        /// 皮带上料工作线程
        /// </summary>
        private void DeviceMotionThreadFeedBelt()
        {
            while (true)
            {
                while (!abort)
                {

                    if (m_sReceiveMessage1 == process_plc_data.TakePic1)
                    {

                        TakePicTask(1);
                    }
                    
                    BatteryFixture();

                    Thread.Sleep(50);
                }
                Thread.Sleep(500);
            }
        }
        /// <summary>
        /// 主要工作线程
        /// </summary>
        private void DeviceMotionThreadFeedPositive()
        {
            while (true)
            {
                while (!abort)
                {
                   /* if (keyenceInstance.ReadBool(plc_r_cam).Content)
                    {
                        keyenceInstance.Write(plc_r_cam, false);
                        TakePicTask(2);
                    }
                    if (keyenceInstance.ReadBool(plc_r_position).Content)
                    {
                        keyenceInstance.Write(plc_r_position, false);
                        BatteryFixture();
                    }*/
                    Thread.Sleep(50);
                }
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// 相机拍照任务
        /// </summary>
        private async void TakePicTask(int CameraNum)
        {
            photoWatch.Start();
            LogHelper.LogInfo("拍照开始");
            await Task.Run(() =>
            {
                if (CameraNum == 1)
                {
                    if (!m_listMvCameras[0].isOpen)
                        m_listMvCameras[0].OpenCam();
                    m_listMvCameras[0].OneShot();
                }
                else if (CameraNum == 2)
                {
                    if (!m_listMvCameras[1].isOpen)
                        m_listMvCameras[1].OpenCam();
                    m_listMvCameras[1].OneShot();
                }
                //cam_pos = Convert.ToSingle(keyenceInstance.ReadInt32(plc_r_cam_x).Content) / 100.0f;   
            });
        }
        public void ReceivePlcFeedBeltData(string receiveData)
        {
            lock (this)
            {
                m_sReceiveMessage1 = receiveData;
            }
            
        }
        public void ReceivePlcFeedPositive(string receiveData)
        {
            lock (this)
            {
                m_sReceiveMessage2 = receiveData;
            }
            
        }
        public void GetPointList(int camNo, PointXYU pointXYUs)
        {
            switch (camNo)
            {
                case 1:
                    pointXYUs1 = pointXYUs;
                    isCamCompleted1 = true;
                    break;
                case 2:
                    pointXYUs2 = pointXYUs;
                    isCamCompleted2 = true;
                    break;
                default:
                    break;
            }
            if (isCamCompleted1 && isCamCompleted2)
            {
                allPointXYUs = new PointXYU();
                //pointXYUs.AddRange(pointXYUs1);
                //pointXYUs.AddRange(pointXYUs2);
                int cnt1 = 1;
                int cnt2 = 1;
                int cnt = Math.Max(cnt1, cnt2);
               /* for (int i = 0; i < Math.Ceiling(cnt / 3.0); i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int index = i * 3 + j;
                        if (cnt1 > index)
                        {
                            allPointXYUs.Add(pointXYUs1[index]);
                        }
                    }
                    for (int j = 0; j < 3; j++)
                    {
                        int index = i * 3 + j;
                        if (cnt2 > index)
                        {
                            allPointXYUs.Add(pointXYUs2[index]);
                        }
                    }
                }
                int total = allPointXYUs.Count;*/

                /*keyenceInstance.Write(plc_w_cam_completed, true);
                keyenceInstance.Write(plc_w_battery_n, total);*/
                isCamCompleted1 = isCamCompleted2 = false;
                photoWatch.Stop();
                LogHelper.LogInfo("拍照耗时" + photoWatch.ElapsedMilliseconds.ToString() + "ms");
                photoWatch.Reset();
                /*OnPositionDisp?.Invoke(1, total, 1, null);*/
            }
        }
        /// <summary>
        /// 发送取料点位
        /// </summary>
        public async void BatteryFixture()
        {
            await Task.Run(() =>
            {
                try
                {
                    if (allPointXYUs == null)
                        return;
                    int battery_index = 0;
                    int tool_index = 0;
                    if (battery_index > 0)
                    {
                        SendPositionEvent(allPointXYUs, battery_index, tool_index);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            });
        }
        /// <summary>
        /// 给机械手发送坐标
        /// </summary>
        /// <param name="pointXYUs"></param>
        /// <param name="battery_index"></param>
        /// <param name="tool_index"></param>
        private void SendPosition2Motor(PointXYU pointXYUs, int battery_index, int tool_index)
        {
           
            float offsetX = 0.0f;
            float offsetY = 0.0f;
            ToolInfos toolInfos = ConfigVars.configInfo.ToolInfos;
            PointXYU point = new PointXYU();
            switch (tool_index)
            {
                case 1:
                    offsetX = toolInfos.Xoffset1;
                    offsetY = toolInfos.Yoffset1;
                    break;
                case 2:
                    offsetX = toolInfos.Xoffset2;
                    offsetY = toolInfos.Yoffset2;
                    break;
                case 3:
                    offsetX = toolInfos.Xoffset3;
                    offsetY = toolInfos.Yoffset3;
                    break;
                default:
                    break;
            }
            if (plc_connect)
            {
                float offsetCam = cam_pos - toolInfos.CamPositionOnCalib;
                HOperatorSet.VectorAngleToRigid(0, 0, 0, pointXYUs.X, pointXYUs.Y, pointXYUs.U, out HTuple homMat2D);
                HOperatorSet.AffineTransPoint2d(homMat2D, -offsetX, -offsetY, out HTuple qx, out HTuple qy);
                point.X = Convert.ToSingle(qx.D) + offsetCam;
                point.Y = Convert.ToSingle(qy.D);
                point.U = pointXYUs.U;
                //MessageBox.Show(string.Format("{0},{1},{2},{3},{4},{5}", point.X, point.Y,point.U,offsetX,offsetY,offsetCam));
                HOperatorSet.TupleDeg(point.U, out HTuple deg);
                point.U = -(float)deg.D;
               /* keyenceInstance.Write(plc_w_battery_x, (int)(point.X * 100));
                keyenceInstance.Write(plc_w_battery_y, (int)(point.Y * 100));
                keyenceInstance.Write(plc_w_battery_u, (int)(-deg.D * 100));*/
                /*OnPositionDisp(battery_index, pointXYUs.Count, tool_index, point);*/
            }
        }

    }
}
