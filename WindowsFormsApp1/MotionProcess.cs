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
using HslCommunication.Profinet.Omron;
using Dyestripping.Models;

namespace BatteryFeederDemo
{
    class MotionProcess
    {
       
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
        List<HalconOperator> halconOperatorList = new List<HalconOperator>();
        MotorsClass motorInstance1, motorInstance2;
        Thread motion_thread;//设备运行线程
        public delegate void ImgDispHandler(HObject hImage, int camNo);
        public static event ImgDispHandler OnImgDisp;
        public delegate void PositionDispHandler(int tooNo, PointXYU pointXYU);
        public static event PositionDispHandler OnPositionDisp;   
        OmronFinsNet omronInstance1, omronInstance2;
        bool plc_connect = false;//PLC是否连接
        bool mvcamera_connect = false;//相机是否连接
        bool plc1_connect = false;//PLC1是否连接
        bool plc2_connect = false;//PLC2是否连接
        bool basler_connect = false;//相机是否连接
        bool light_connect = false;//光源控制器是否连接成功
        public bool is_init;//是否初始化连接完成
        /// <summary>
        /// 0待机 1拍照 2拍照OK 3拍照NG 4拍照异常
        /// </summary>
        string plc_cam_status = "D8020";////读写 0待机 1拍照 2拍照OK 3拍照NG 4拍照异常
        float cam_pos;//拍照时相机位置

        List<PointXYU> photoPoints;//所有相机获取的电池点位集合
        Stopwatch[] photoWatchs = new Stopwatch[] { new Stopwatch(), new Stopwatch() };
        
        int m_icamNo;//相机序号
        public MotionProcess()
        {
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
            for (int i = 0; i < 2; i++)
            {
                halconOperatorList.Add(HalconOperator.GetInstance(i));
            }
            omronInstance1 = OmronPlcFactory.GetInstance(0);
            omronInstance2 = OmronPlcFactory.GetInstance(1);
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


        public static MotionProcess GetInstance()
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
        }

        public void Init()
        {
            if (is_init)
            {
                return;
            }
            try
            {
                if (!plc1_connect)
                {
                    plc1_connect = omronInstance1.ConnectServer().IsSuccess;
                    if (plc1_connect)
                        LogHelper.LogInfo("PLC1连接成功！");
                    else
                        LogHelper.LogError("PLC1连接失败！");
                }
                if (!plc2_connect)
                {
                    plc2_connect = omronInstance2.ConnectServer().IsSuccess;
                    if (plc2_connect)
                        LogHelper.LogInfo("PLC2连接成功！");
                    else
                        LogHelper.LogError("PLC2连接失败！");
                }
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
                if (plc1_connect && plc2_connect && mvcamera_connect)
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
            motorInstance1 = new MotorsClass(0);
            motorInstance2 = new MotorsClass(1);
            motion_thread = new Thread(new ThreadStart(DeviceMotionThread));
            motion_thread.Name = "设备自动工作线程";
            motion_thread.Start();          
        }

        public void Exit()
        {
            if (motion_thread != null)
                motion_thread.Abort();
            if (omronInstance1 != null)
            {
                omronInstance1.ConnectClose();
            }
        }
        /// <summary>
        /// 皮带上料工作线程
        /// </summary>
       
        private void DeviceMotionThread()
        {
            while (true)
            {
                while (!abort)
                {
                    if (omronInstance1.ReadInt16(plc_cam_status).Content == 1)
                    {
                        omronInstance1.Write(plc_cam_status, 0);
                        TakePicTask(0);
                    }
                    if (omronInstance2.ReadInt16(plc_cam_status).Content == 1)
                    {
                        omronInstance2.Write(plc_cam_status, 0);
                        TakePicTask(1);
                    }
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
            photoWatchs[CameraNum].Start();
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
               
            });
        }
      
        
        public void GetPointList(int camNo, List<PointXYU> points)
        {
            photoPoints = points;
            switch (camNo)
            {
                case 1:
                    PointXYU[] pickPoints1 = motorInstance1.CalPickPoint(points, 0, out bool[] isFound1);               
                    motorInstance1.SendPoint(pickPoints1);
                    if (isFound1[0])
                        omronInstance1.Write(plc_cam_status, 2);
                    else { omronInstance1.Write(plc_cam_status, 3); }
                    break;
         
                case 2:
                    PointXYU[] pickPoints2 = motorInstance2.CalPickPoint(points, 0, out bool[] isFound2);
                    motorInstance2.SendPoint(pickPoints2);
                    if (isFound2[0])
                        omronInstance1.Write(plc_cam_status, 2);
                    else { omronInstance1.Write(plc_cam_status, 3); }
                    break;
                default:
                    break;
            }
            photoWatchs[camNo].Stop();
            LogHelper.LogInfo($"定位{ camNo + 1 }拍照完成,耗时{photoWatchs[camNo].ElapsedMilliseconds}ms");
            photoWatchs[camNo].Reset();
        }
       


    }
}
