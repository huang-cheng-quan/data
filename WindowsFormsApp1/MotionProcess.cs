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
using HslCommunication.Profinet.Omron;
using Dyestripping.Models;
using System.Runtime.InteropServices;
using MvCamCtrl.NET;
using static WindowsFormsApp1.InferData.InferServerData;
using WindowsFormsApp1.InferData;
using System.Drawing;
using Camera_Capture_demo.LoginFrms;
/*using Ookii.Dialogs.WinForms;*/
using Application = System.Windows.Forms.Application;
using Ookii.Dialogs.WinForms;
using HslCommunication;
using Camera_Capture_demo.GlobalVariable;

namespace BatteryFeederDemo
{
    class MotionProcess
    {
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        private const int m_nBuffSize = 10;
        private const int m_nBuffSizeINfer = 35;
        public static int m_nCameraSaveNum = 4;
        public static int m_nCameraTotalNum = 10;
        public static int m_nCameraSaveConnectionNum = 3;

        public static int NeedLocate = 8;
        //每个工位的拍照数量
        private const int m_iOnePositionPic = 5;
        //检测面数
        private const int m_iPositionNum = 6;
        //保存图像的内存块
        public static IntPtr[,] m_pSaveImageBuf_N = new IntPtr[m_nCameraSaveNum, m_nBuffSize];
        //保存连接器图像的内存块
        public static IntPtr[,] m_pSaveImageBuf_Connection = new IntPtr[m_nCameraSaveConnectionNum, m_nBuffSize];
        /// <summary>
        /// 用于保存各个相机的内存编号
        /// </summary>      
        public static Dictionary<int, List<int>> m_dictImageIndex = new Dictionary<int, List<int>>();
        /// <summary>
        /// 用于保存连接器相机的内存编号
        /// </summary>      
        public static Dictionary<int, List<int>> m_dictImageConnectionIndex = new Dictionary<int, List<int>>();
        /// <summary>
        /// 用于保存发送给推理机的内存块
        /// </summary>
        private IntPtr[] m_pSaveImageBuf_Infer = new IntPtr[m_nBuffSizeINfer];

        public static double[] motion_SinglePixelAccuracy =new double[NeedLocate] ;

        private List<string> m_listQrCodeInfer = new List<string>();
        private List<string> m_listQrCodeConnectionPositive = new List<string>();
        private List<string> m_listQrCodeConnectionNegative = new List<string>();

        private List<int> m_listImage_In_Infer = new List<int>();
        public Dictionary<int, List<IntPtr>> m_dictDtotalSaveBufMomory = new Dictionary<int, List<IntPtr>>();
        public static MyCamera.MV_SAVE_IMG_TO_FILE_PARAM[] m_stSaveParam_N = new MyCamera.MV_SAVE_IMG_TO_FILE_PARAM[4];
        private const int m_nImageWidth = 5120;
        private const int m_nImageHeight = 5120;
        private const int m_nImageWidthInfer = 1500;
        private const int m_nImageHeightInfer = 1000;
        private Dictionary<int, int> m_dictTotalImageIndex = new Dictionary<int, int>();
        //开启发送图像的标志
        bool m_bWhileIsSendReasoningMachine = false;
        //推理机屏蔽按钮判断
        bool m_bConnectionServer = true;
        
        // 定义一个静态变量来保存类的实例
        private static MotionProcess mpInstance;
        // 定义一个标识确保线程同步
        private static readonly object locker = new object();
        public bool abort = true;//设备运行停止标识
        public static List<MvClass> m_listMvCameras = new List<MvClass>();
        List<HalconOperator> halconOperatorList = new List<HalconOperator>();
        MotorsClass motorInstance;
        #region 线程部分
        Thread m_thMotion_thread;//设备运行线程
        Thread[] m_thSaveImage_thread = new Thread[m_nCameraTotalNum];//设备保存图像线程
        Thread m_thSaveInferImage_thread;
        Thread m_threadSaveImage;
        Thread m_threadSaveInferImage;
        Thread m_thdSendReasoningMachine;
        #endregion
        #region 委托事件部分
        public delegate void ImgDispHandler(HObject hImage, int camNo,int MotorNum);
        public static event ImgDispHandler event_OnImgDisp;

        public delegate void DataDispHandler(int camNum, MyCamera.MV_DISPLAY_FRAME_INFO stDisplayInfo, IntPtr data);
        public static event DataDispHandler event_OnDataDisp;

        public delegate void QrCodeShow(string code);
        public static event QrCodeShow event_QrCodeShow;

        public delegate void PositionDispHandler(int tooNo, PointXYU pointXYU);
        public static event PositionDispHandler event_OnPositionDisp;
        public delegate void NumDispHandler(int number, int flag);
        public static event NumDispHandler event_LabelDisplay;
        public delegate void DelegateSampleResultData_NVT(SampleResultData_NVT data);
        public static event DelegateSampleResultData_NVT event_ProcessSampleResultData_NVT;
        public delegate void DelegateSampleResultData2_NVT(SampleResultData2_NVT data);
        public static event DelegateSampleResultData2_NVT event_ProcessSampleResultData2_NVT;



        #endregion
        #region 相机和通讯变量部分
        public static OmronFinsNet omronInstance1, omronInstance2;
        bool mvcamera_connect = false;//相机是否连接
        bool plc1_connect = false;//PLC1是否连接
        bool plc2_connect = false;//PLC2是否连接
        public bool is_init;//是否初始化连接完成
        bool m_bReadModelSucess = false;
        /// <summary>
        /// 0待机 1拍照 2拍照OK 3拍照NG 4拍照异常
        /// </summary>
        public static string plc_cam_status4 = "D1012";////读写 0待机 1拍照 2拍照NG
        public static string plc_cam_status6 = "D1312";////读写 0待机 1拍照 2拍照NG 
        public static string plc_cam_status8 = "D1212";////读写 0待机 1拍照 2拍照NG 


        public static int multitudeMotorToOneCameraFlag = 3;
        public static int multitudeMotorNum = 2;

        public static string m_Job_name = null;
        #endregion
        public class name_Image
        {
            /// <summary>
            /// 工件型号
            /// </summary>

            /// <summary>
            /// 位姿编号
            /// </summary>
            public string Position_Number;
            /// <summary>
            /// 工件编号
            /// </summary>
            public string Sample_Number;
            /// <summary>
            /// 相机编号
            /// </summary>
            public string Cam_Num;
            /// <summary>
            /// 光源编号
            /// </summary>
            public string Light_Num;
            /// <summary>
            /// 拍摄时间
            /// </summary>
            public string Time_Now;
            /// <summary>
            /// 样本序号
            /// </summary>
            public string SampleID;
        }
        //开启保存图像的线程标志
        bool m_bWhileStatus_SaveInferImage = false;
        bool m_bWhileStatus_SaveConnectionImage = false;
        bool m_bRunSaveImage_Infer = true;
        bool m_bRunSaveImage_Connection = true;
        bool m_bRunSaveImage = true;
        private Object m_BufForSaveImageLock = new Object();
        private object m_MemoryaveImageLock1 = new object();
        private object m_MemoryaveImageLock2 = new object();

     
        private Object m_lockBufForSaveConnectionImageLock = new Object();
        public static MyCamera.MV_SAVE_IMG_TO_FILE_PARAM m_stSaveParamInfer = new MyCamera.MV_SAVE_IMG_TO_FILE_PARAM();
        //连接器检测钢片和金属面共两个，再加上Fpc
        public static MyCamera.MV_SAVE_IMG_TO_FILE_PARAM[] m_stSaveParamConnection = new MyCamera.MV_SAVE_IMG_TO_FILE_PARAM[m_nCameraSaveConnectionNum];

        name_Image m_nameConnection = new name_Image();

        name_Image m_niInfer = new name_Image();
        //现在的时间路径
        string m_sJob_name_file = null;
        //样本序号
        int sample_id_num = 0;
        int m_iSampleIdConnection_num = 0;
        List<string> m_listSampleIDs = new List<string>();

       
        //识别二维码
        string qrCode = "";
        string[] qrCode_back = new string[m_nCameraSaveNum];
        string m_QrCodeInfer = "";
        string m_sQrCodeConnection = "";
        string qrCode_backInfer = "";
        //模板文件
        HTuple hv_DataCodeHandle1 = new HTuple();
        private MyCamera[] m_pMyCamera;
        //List<string>[] Filenames = new List<string>[m_nCameraSaveNum];
        List<string> Filenames = new List<string>();
        //开启保存图像的线程标志
        bool m_bWhileEnd = false;
        int m_icamNo;//相机序号
        //推理机使用相机
        bool m_bConnectionServerCamNum = true;
        //服务器是否链接
        bool m_bCheckConnected;
        private Object m_SendReasoningMachineLock = new object();
        int m_nSendInference = 0;
        /// <summary>
        /// 发送服务器容器
        /// </summary>
        AlgoSampleCommitRequest_NVT m_arrictAscr = new AlgoSampleCommitRequest_NVT();
        /// <summary>
        /// 接受服务器容器
        /// </summary>
        SampleResultData_NVT m_ayySrdn = new SampleResultData_NVT();
        /// <summary>
        /// 连接推理机类
        /// </summary>
        HttpQuery hq = new HttpQuery();
        /// <summary>
        /// 第二次发送服务器容器
        /// </summary>
        AlgoSampleCommitRequest2_NVT m_arrictAscr2 = new AlgoSampleCommitRequest2_NVT();
        /// <summary>
        /// 第二次接受服务器容器
        /// </summary>
        SampleResultData2_NVT m_ayySrdn2 = new SampleResultData2_NVT();
        //现在的时间路径
        string m_sTimePath = null;
        string m_sDefect_file1 = null;
        //服务器连接
        public static string Client_IP = null;
        public static int Cilent_Port = 0;
        int m_nCanOpenDeviceNum;        // ch:设备使用数量 | en:Used Device Number
        int totalNumberSamples = 0;
        /// <summary>
        /// 光度立体路径
        /// </summary>
        string PicShow_Path = null;
        private Object m_NgorOKLock = new object();
        //NG样本
        int ngSamples = 0;
        //OK样本
        int okSamples = 0;
        public string ServerSaveFilePath = "C:/data/";
        /// <summary>
        /// 缩略图保存地址
        /// </summary>
        public string m_sThumbnailServerSaveFilePath = "C:/data/SmallPic/";
        string Pic_Path1 = "";
        string Pic_Path2 = "";
        string Pic_Path3 = "";
        string Pic_Path4 = "";
        string Pic_Path5 = "";
        string Pic_Path6 = "";
        string Pic_Path7 = "";
        //深度学习变量
        string m_sModel_Path_P1 = "D:/halcon_DeepLearning/Watch_deep_segment_data/model_backupdata/20219271519";
        string m_sModel_Path_P2_P6 = "D:/halcon_DeepLearning/Watch_deep_segment_data/model_backupdata/20219261653";
        HTuple m_hv_DLModelHandle_P01 = new HTuple();
        HTuple m_hv_DLModelHandle_P2_P6 = new HTuple();
        string Model_Path = "";
        HTuple m_hv_DatasetInfo = new HTuple();
        public static int m_ihalconProcesStartNum = 4;
        public static int m_ihalconProcesEndNum = 9;
        public MotionProcess()
        {
            for (int i = 0; i < m_nCameraSaveNum; i++)
            {
                if (!m_dictImageIndex.ContainsKey(i))
                {
                    m_dictImageIndex.Add(i, new List<int>());
                }
            }
            for (int i = 0; i < m_nCameraSaveConnectionNum; i++)
            {
                if (!m_dictImageConnectionIndex.ContainsKey(i))
                {
                    m_dictImageConnectionIndex.Add(i, new List<int>());
                }
            }

            //keyenceInstance = KeyencePlcFactory.GetInstance();
            for (int i = 0; i < m_nCameraTotalNum; i++)
            {
                m_listMvCameras.Add(MvClass.GetInstance(i));
            }

            for (int i = 0; i < m_ihalconProcesEndNum; i++)
            {
                if (i!=5)
                {
                    halconOperatorList.Add(HalconOperator.GetInstance(i,0));
                }
                else
                {
                    halconOperatorList.Add(HalconOperator.GetInstance(i, 1));
                    halconOperatorList.Add(HalconOperator.GetInstance(i, 2));
                }
                
            }
            /* omronInstance1 = OmronPlcFactory.GetInstance(0);
             omronInstance2 = OmronPlcFactory.GetInstance(1);*/
            //开辟内存
            int Area_Camera = Convert.ToInt32(m_nImageWidth) * Convert.ToInt32(m_nImageHeight);
            for (int i = 0; i < m_nCameraSaveNum; i++)
            {
                for (int k = 0; k < m_nBuffSize; ++k)
                {
                    m_pSaveImageBuf_N[i, k] = Marshal.AllocHGlobal(Area_Camera);
                }
            }
            //开辟保存连接器的内存图像
            int Area_CameraConnection = Convert.ToInt32(m_nImageWidth) * Convert.ToInt32(m_nImageHeight);
            for (int i = 0; i < m_nCameraSaveConnectionNum; i++)
            {
                for (int k = 0; k < m_nBuffSize; ++k)
                {
                    m_pSaveImageBuf_Connection[i, k] = Marshal.AllocHGlobal(Area_CameraConnection);
                }
            }
            //开辟发送给推理机的内存
            int MainArea_Camera = Convert.ToInt32(m_nImageWidthInfer) * Convert.ToInt32(m_nImageHeightInfer);
            for (int i = 0; i < m_nBuffSizeINfer; i++)
            {
                m_pSaveImageBuf_Infer[i] = Marshal.AllocHGlobal(MainArea_Camera);
            }


        }
        private void MotionProcess_eventProcessImage6(HObject hImage, int motorNum)
        {
            if (!abort)
                event_OnImgDisp?.Invoke(hImage, 6,0);
        }
        private void MotionProcess_eventProcessImage8(HObject hImage, int motorNum)
        {
            if (!abort)
                event_OnImgDisp?.Invoke(hImage, 8,0);
        }
        private void MotionProcess_eventProcessImage4(HObject hImage,int motorNum)
        {
            if (!abort)
                event_OnImgDisp?.Invoke(hImage, 4,0);
        }
        private void MotionProcess_eventProcessImage5(HObject hImage, int motorNum)
        {
            switch (motorNum)
            {
                case 0:
                    if (!abort)
                        event_OnImgDisp?.Invoke(hImage, 5,1);
                    break;
                case 1:
                    if (!abort)
                        event_OnImgDisp?.Invoke(hImage, 5,2);
                    break;
                default:
                    break;
            }
           
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
                if (!mvcamera_connect)
                {
                    mvcamera_connect = true;
                    for (int i = 0; i < m_nCameraTotalNum; i++)
                    {
                        if (!m_listMvCameras[i].isOpen)
                        {
                            m_listMvCameras[i].OpenCam(i);
                        }
                        mvcamera_connect = m_listMvCameras[i].isOpen && mvcamera_connect;
                        if (m_listMvCameras[i].isOpen)
                        {
                            LogHelper.LogInfo("相机" + i.ToString() + "连接成功！");
                        }
                        else
                        {
                            LogHelper.LogError("相机" + i.ToString() + "连接失败！");
                        }
                    }


                }
                if (m_listMvCameras[6] != null /*&& m_listMvCameras[1] != null*/)
                {
                    m_listMvCameras[4].eventProcessImage += MotionProcess_eventProcessImage4;
                    m_listMvCameras[6].eventProcessImage += MotionProcess_eventProcessImage6;
                    m_listMvCameras[8].eventProcessImage += MotionProcess_eventProcessImage8;
                    m_listMvCameras[5].eventProcessImage += MotionProcess_eventProcessImage5;
                }
                m_bCheckConnected = hq.CheckConnected(Client_IP, Cilent_Port);
                if (m_bCheckConnected)
                {
                    LogHelper.LogInfo("已连接服务器");

                }
                else
                {
                    LogHelper.LogError("未连接服务器");
                }
              
                if (mvcamera_connect)
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
            motorInstance = new MotorsClass(0);
           
            //保存图像线程
            for (int i = 0; i < m_nCameraTotalNum; i++)
            {
                if (i == 0||i==1||i==2||i==3)
                {
                    /*m_thSaveImage_thread[i] = new Thread(SaveMemoryToInferImage);
                    m_thSaveImage_thread[i].Name = "保存" + i.ToString() + "图像线程";
                    m_thSaveImage_thread[i].Start(i);
                    LogHelper.LogInfo("保存" + i.ToString() + "图像线程开启！");*/
                }
                else if (i == 5)
                {
                    m_thSaveImage_thread[i] = new Thread(SaveConnectionImage);
                    m_thSaveImage_thread[i].Name = "保存" + i.ToString() + "图像线程";
                    m_thSaveImage_thread[i].Start(i);
                    LogHelper.LogInfo("保存" + i.ToString() + "图像线程开启！");
                }

            }
           /* //保存推理图像线程
            m_thSaveInferImage_thread = new Thread(SaveInferImage);
            m_thSaveInferImage_thread.Name = "设备保存推理图像工作线程";
            m_thSaveInferImage_thread.Start();
            LogHelper.LogInfo("设备保存推理图像工作线程开启！");*/

            string MODEL_PATH2D = System.Windows.Forms.Application.StartupPath + "/" + "2D_model" + "/" + "data_code_model.dcm";
            hv_DataCodeHandle1.Dispose();
            HOperatorSet.ReadDataCode2dModel(MODEL_PATH2D, out hv_DataCodeHandle1);
            if (hv_DataCodeHandle1 != null)
            {
                LogHelper.LogInfo("模板载入成功！");
            }
            else
            {
                LogHelper.LogError("模板载入失败！");
            }

            //发送推理机信息线程
           /* m_thdSendReasoningMachine = new Thread(SendMessage2ReasoningMachine);
            m_thdSendReasoningMachine.Name = "发送推理机工作线程";
            m_thdSendReasoningMachine.Start();
            LogHelper.LogInfo("发送推理机工作线程开启！");*/

        }
        public void Exit()
        {
            if (omronInstance1 != null)
            {
                omronInstance1.ConnectClose();
            }
            if (omronInstance2 != null)
            {
                omronInstance2.ConnectClose();
            }
            if (motorInstance != null)
            {
                MotorsClass.omronInstance.ConnectClose();
            }
            
            //退线程
            m_bWhileStatus_SaveInferImage = true;

            m_bWhileStatus_SaveConnectionImage = true;
            m_bWhileEnd = true;
            m_bWhileIsSendReasoningMachine = true;
            /*LogHelper.LogInfo("正在关闭相机...");
            for (int i = 0; i < m_nCameraTotalNum; i++)
            {
                if ((m_listMvCameras[i] != null))
                {
                    if (m_listMvCameras[i].isOpen)
                    {
                        m_listMvCameras[i].StopGrabbing();
                        m_listMvCameras[i].CloseCam();
                    }
                }

            }
            LogHelper.LogInfo("关闭相机完毕");*/
        }
        /// <summary>
        /// 皮带上料工作线程
        /// </summary>       

        /// <summary>
        /// 相机拍照任务
        /// </summary>

        private string Matrix_code_identify(HObject ho_Image)
        {
            String Code2d = null;
            HObject ho_GrayImage, ho_ImageReduced;
            HObject ho_SymbolXLDs;
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            HObject ho_Rectangle = new HObject();
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            // Local control variables 

            HTuple hv_ResultHandles = new HTuple();
            HTuple hv_DecodedDataStrings = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_GrayImage);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_SymbolXLDs);
            hv_Width.Dispose(); hv_Height.Dispose();
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, hv_Width - ((hv_Width / 4) * 3), hv_Height - ((hv_Width / 4) * 3),
                    hv_Width / 2, hv_Height / 2);
            }
            ho_GrayImage.Dispose();
            HOperatorSet.Rgb1ToGray(ho_Image, out ho_GrayImage);

            ho_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(ho_GrayImage, ho_Rectangle, out ho_ImageReduced);
            ho_SymbolXLDs.Dispose(); hv_ResultHandles.Dispose(); hv_DecodedDataStrings.Dispose();
            HOperatorSet.FindDataCode2d(ho_ImageReduced, out ho_SymbolXLDs, hv_DataCodeHandle1,
                "train", "all", out hv_ResultHandles, out hv_DecodedDataStrings);
            if (hv_DecodedDataStrings.Length == 0)
            {
                Code2d = "NC".PadRight(7, '0') + DateTime.Now.ToString("yyyyMMddHHmmss");
            }
            else
            {
                Code2d = hv_DecodedDataStrings;
            }

            return Code2d;


            ho_ImageReduced.Dispose();
            ho_SymbolXLDs.Dispose();
            hv_ResultHandles.Dispose();
            hv_DecodedDataStrings.Dispose();
        }

        private  string GetQrCode()
        {
            string code = "";
            OperateResult<string> word = MotorsClass.omronInstance.ReadString(MotorsClass.plc_Qrcode_motor_FeedPositive2, 10);
            code = word.Content;
            return code;
        }
        #region UnUseCode
        /*public void SaveInferImage()
        {
            int m_nMemoryindex = 0;
            bool bHasData = false;
            while (!m_bWhileStatus_SaveInferImage)
            {
                
                if (m_bRunSaveImage_Infer)
                {
                    
                    bHasData = false;
                    lock (m_BufForSaveImageLock)
                    {
                        
                        if (m_listImage_In_Infer.Count > 0)
                        {
                            m_nMemoryindex = m_listImage_In_Infer.First();
                            m_listImage_In_Infer.RemoveAt(0);
                            bHasData = true;
                        }
                    }

                    if (bHasData)
                    {

                        //保存图像                     
                        m_stSaveParamInfer.pData = m_pSaveImageBuf_Infer[m_nMemoryindex];
                        
                        m_niInfer.Time_Now = DateTime.Now.ToString("yyyyMMddHHmmss");
                       
                        string Save_file_tmp = "";
                        string[] Sample_Number = new string[4];
                        try
                        {
                           
                            if (m_nMemoryindex == 0)
                            {
                                lock (this)
                                {
                                    sample_id_num++;
                                    m_niInfer.SampleID = sample_id_num.ToString("000000");
                                }

                               *//* HObject mp = Intptr2Hobject(m_pSaveImageBuf_Infer[m_nMemoryindex], m_stSaveParamInfer);
                                qrCode = Matrix_code_identify(mp);

                                if (qrCode.Substring(0, 2) == "NC")
                                {
                                    LogHelper.LogError("未识别到二维码！");
                                    Alarm();

                                }*//*
                            }
                            if (m_listQrCodeInfer.Count > 0)
                            {
                                m_QrCodeInfer = m_listQrCodeInfer.First();
                                m_listQrCodeInfer.RemoveAt(0);

                            }
                            else
                            {
                                m_QrCodeInfer = "test0000000000000000";
                            }
                            m_niInfer.Sample_Number = m_QrCodeInfer;
                            m_niInfer.Position_Number = ((m_nMemoryindex) / 5 + 1).ToString("00");
                            m_niInfer.Light_Num = (((((m_nMemoryindex + 1) - 1) % 5))).ToString("0");
                            m_listSampleIDsInfer.Add(m_niInfer.Sample_Number);
                            m_niInfer.Cam_Num = ((m_nMemoryindex) / 10 + 1).ToString("00");
                            //ShowPosition(int.Parse(NI[nCamIndex].Position_Number));
                            Save_file_tmp = ServerSaveFilePath + m_Job_name + "_" + m_niInfer.Sample_Number + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_S" + m_niInfer.SampleID + "_" + m_niInfer.Time_Now;
                            string m_StsServers = "";
                            m_StsServers = m_Job_name + "_" + m_niInfer.Sample_Number + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_S" + m_niInfer.SampleID + "_" + m_niInfer.Time_Now;
                            m_stSaveParamInfer.enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                            m_stSaveParamInfer.pImagePath = Save_file_tmp + ".jpg";
                            m_stSaveParamInfer.nQuality = 98;//存Jpeg时有效
                            int nRet1 = m_listMvCameras[0].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer);

                            //给推理机的数据加入列表
                            //LogHelper.LogInfo(string.Join(",", Filenames[nCamIndex]));
                            Filenames.Add(m_StsServers + ".jpg");
                            string saveThumbnailFilePath = m_sThumbnailServerSaveFilePath + m_Job_name + "_" + m_niInfer.Sample_Number + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num +
                                "_L" + m_niInfer.Light_Num + "_S" + m_niInfer.SampleID + "_" + m_niInfer.Time_Now;
                            m_stSaveParamInfer.pImagePath = saveThumbnailFilePath + ".jpg";
                            m_stSaveParamInfer.nQuality = 80;//存Jpeg时有效
                            int nRet2 = m_listMvCameras[0].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer);


                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show(ex.ToString());
                        }
                        
                    }
                    if (!bHasData)
                    {
                        Thread.Sleep(10);
                    }
                }
            }
        }*/
        /* private void SaveMemory(object ob)
       {
           int nCamIndex = (int)ob;
           int[] m_nMemoryindex = new int[m_nCameraSaveNum];
           while (!m_bWhileEnd)
           {
               if (m_bRunSaveImage)
               {
                   bHasData[nCamIndex] = false;
                   if (m_dictImageIndex[nCamIndex].Count > 0)
                   {
                       lock (m_MemoryaveImageLock1)
                       {
                           if (m_dictImageIndex[nCamIndex].Count > 0)
                           {


                               lock (m_MemoryaveImageLock2)
                               {
                                   m_nMemoryindex[nCamIndex] = m_dictImageIndex[nCamIndex].First();
                                   m_dictImageIndex[nCamIndex].RemoveAt(0);
                                   bHasData[nCamIndex] = true;
                               }
                               if (bHasData[nCamIndex])
                               {
                                   switch (nCamIndex)
                                   {

                                       //侧面短边检测相机
                                       case 0:
                                           //偶数
                                           if (!IsOdd(m_nMemoryindex[nCamIndex] / m_iOnePositionPic))
                                           {
                                               CopyMemory(m_pSaveImageBuf_Infer[(m_nMemoryindex[nCamIndex] % m_iOnePositionPic) + m_iOnePositionPic * 0],
                                                   m_pSaveImageBuf_N[nCamIndex, m_nMemoryindex[nCamIndex]], m_stSaveParam_N[nCamIndex].nDataLen);
                                               int t1 = (m_nMemoryindex[nCamIndex] % m_iOnePositionPic) + m_iOnePositionPic * 0;
                                               m_listImage_In_Infer.Add((m_nMemoryindex[nCamIndex] % m_iOnePositionPic) + m_iOnePositionPic * 0);

                                           }
                                           else//奇数
                                           {
                                               CopyMemory(m_pSaveImageBuf_Infer[(m_nMemoryindex[nCamIndex] % m_iOnePositionPic) + m_iOnePositionPic * 1],
                                                   m_pSaveImageBuf_N[nCamIndex, m_nMemoryindex[nCamIndex]], m_stSaveParam_N[nCamIndex].nDataLen);
                                               int t1 = (m_nMemoryindex[nCamIndex] % m_iOnePositionPic) + m_iOnePositionPic * 1;
                                               m_listImage_In_Infer.Add((m_nMemoryindex[nCamIndex] % m_iOnePositionPic) + m_iOnePositionPic * 1);


                                           }

                                           break;


                                       //反面和mayler检测相机
                                       case 1:
                                           //偶数
                                           if (!IsOdd(m_nMemoryindex[nCamIndex] / m_iOnePositionPic))
                                           {
                                               CopyMemory(m_pSaveImageBuf_Infer[(m_nMemoryindex[nCamIndex] % m_iOnePositionPic) + m_iOnePositionPic * 2],
                                                   m_pSaveImageBuf_N[nCamIndex, m_nMemoryindex[nCamIndex]], m_stSaveParam_N[nCamIndex].nDataLen);
                                               m_listImage_In_Infer.Add((m_nMemoryindex[nCamIndex] % m_iOnePositionPic) + m_iOnePositionPic * 2);
                                           }
                                           else//奇数
                                           {
                                               CopyMemory(m_pSaveImageBuf_Infer[(m_nMemoryindex[nCamIndex] % m_iOnePositionPic) + m_iOnePositionPic * 3],
                                                   m_pSaveImageBuf_N[nCamIndex, m_nMemoryindex[nCamIndex]], m_stSaveParam_N[nCamIndex].nDataLen);
                                               m_listImage_In_Infer.Add((m_nMemoryindex[nCamIndex] % m_iOnePositionPic) + m_iOnePositionPic * 3);

                                           }
                                           break;


                                       //侧面长边检测相机
                                       case 2:
                                           //偶数
                                           if (!IsOdd(m_nMemoryindex[nCamIndex] / m_iOnePositionPic))
                                           {
                                               CopyMemory(m_pSaveImageBuf_Infer[(m_nMemoryindex[nCamIndex] % m_iOnePositionPic) + m_iOnePositionPic * 4],
                                                   m_pSaveImageBuf_N[nCamIndex, m_nMemoryindex[nCamIndex]], m_stSaveParam_N[nCamIndex].nDataLen);
                                               m_listImage_In_Infer.Add((m_nMemoryindex[nCamIndex] % m_iOnePositionPic) + m_iOnePositionPic * 4);
                                           }
                                           else//奇数
                                           {
                                               CopyMemory(m_pSaveImageBuf_Infer[(m_nMemoryindex[nCamIndex] % m_iOnePositionPic) + m_iOnePositionPic * 5],
                                                   m_pSaveImageBuf_N[nCamIndex, m_nMemoryindex[nCamIndex]], m_stSaveParam_N[nCamIndex].nDataLen);
                                               m_listImage_In_Infer.Add((m_nMemoryindex[nCamIndex] % m_iOnePositionPic) + m_iOnePositionPic * 5);
                                           }
                                           break;

                                       //正面检测相机一个工位
                                       case 3:
                                           CopyMemory(m_pSaveImageBuf_Infer[(m_nMemoryindex[nCamIndex] % m_iOnePositionPic) + m_iOnePositionPic * 6],
                                               m_pSaveImageBuf_N[nCamIndex, m_nMemoryindex[nCamIndex]], m_stSaveParam_N[nCamIndex].nDataLen);
                                           m_listImage_In_Infer.Add((m_nMemoryindex[nCamIndex] % m_iOnePositionPic) + m_iOnePositionPic * 6);

                                           break;

                                       default:
                                           break;

                                   }
                               }
                              *//* if (m_listImage_In_Infer.Count >= 35)
                               {
                                   m_listImage_In_Infer.Clear();
                               }*//*

                           }
                       }
                   }


               }
           }
       }*/
        #endregion

        /// <summary>
        /// 判断是否是偶数
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsOdd(int n)
        {
            return Convert.ToBoolean(n % 2);
        }
        bool[] bHasData = new bool[m_nCameraSaveNum];
      

        private void SaveMemoryToInferImage(object ob)
        {
            int nCamIndex = (int)ob;
            int[] m_nMemoryindex = new int[m_nCameraSaveNum];
            int[] SampleIDFlag = new int[7];
            int TotalNum = 0;
            string Save_file_tmp = "";
            while (!m_bWhileEnd)
            {
                if (m_bRunSaveImage)
                {
                    bHasData[nCamIndex] = false;
                    if (m_dictImageIndex[nCamIndex].Count > 0)
                    {
                        lock (m_MemoryaveImageLock1)
                        {
                            if (m_dictImageIndex[nCamIndex].Count > 0)
                            {


                                lock (m_MemoryaveImageLock2)
                                {
                                    m_nMemoryindex[nCamIndex] = m_dictImageIndex[nCamIndex].First();
                                    m_dictImageIndex[nCamIndex].RemoveAt(0);
                                    bHasData[nCamIndex] = true;
                                }
                                if (bHasData[nCamIndex])
                                {
                                    switch (nCamIndex)
                                    {

                                        //侧面短边检测相机
                                        case 0:
                                            //偶数
                                            if (!IsOdd(m_nMemoryindex[nCamIndex] / m_iOnePositionPic))
                                            {

                                                SampleIDFlag[0]++;
                                                if (SampleIDFlag[0] == 5)
                                                {
                                                    TotalNum++;
                                                    SampleIDFlag[0] = 0;
                                                }


                                            }
                                            else//奇数
                                            {
                                                SampleIDFlag[1]++;
                                                if (SampleIDFlag[1] == 5)
                                                {
                                                    TotalNum++;
                                                    SampleIDFlag[1] = 0;
                                                }
                                            }

                                            break;


                                        //反面和mayler检测相机
                                        case 1:
                                            //偶数
                                            if (!IsOdd(m_nMemoryindex[nCamIndex] / m_iOnePositionPic))
                                            {
                                                SampleIDFlag[2]++;
                                                if (SampleIDFlag[2] == 5)
                                                {
                                                    TotalNum++;
                                                    SampleIDFlag[2] = 0;
                                                }
                                            }
                                            else//奇数
                                            {
                                                SampleIDFlag[3]++;
                                                if (SampleIDFlag[3] == 5)
                                                {
                                                    TotalNum++;
                                                    SampleIDFlag[3] = 0;
                                                }
                                            }
                                            break;


                                        //侧面长边检测相机
                                        case 2:
                                           /* //偶数
                                            if (!IsOdd(m_nMemoryindex[nCamIndex] / m_iOnePositionPic))
                                            {
                                                SampleIDFlag[4]++;
                                                if (SampleIDFlag[4] == 5)
                                                {
                                                    TotalNum++;
                                                    SampleIDFlag[4] = 0;
                                                }
                                            }
                                            else//奇数
                                            {
                                                SampleIDFlag[5]++;
                                                if (SampleIDFlag[5] == 5)
                                                {
                                                    TotalNum++;
                                                    SampleIDFlag[5] = 0;
                                                }
                                            }*/
                                            SampleIDFlag[4]++;
                                            if (SampleIDFlag[4] == 5)
                                            {
                                                TotalNum++;
                                                SampleIDFlag[4] = 0;
                                            }
                                            break;

                                        //正面检测相机一个工位
                                        case 3:
                                            SampleIDFlag[5]++;
                                            if (SampleIDFlag[5] == 5)
                                            {
                                                TotalNum++;
                                                SampleIDFlag[5] = 0;
                                            }

                                            break;

                                        default:
                                            break;

                                    }


                                    SampleIDFlag[(m_nMemoryindex[nCamIndex] / m_iOnePositionPic) + 2 * nCamIndex]++;
                                    if (SampleIDFlag[(m_nMemoryindex[nCamIndex] / m_iOnePositionPic) + 2 * nCamIndex]==5)
                                    {
                                        TotalNum++;
                                        SampleIDFlag[(m_nMemoryindex[nCamIndex] / m_iOnePositionPic) + 2 * nCamIndex] = 0;
                                    }


                                    if (m_listQrCodeInfer.Count > 0)
                                    {
                                        m_QrCodeInfer = m_listQrCodeInfer.First();
                                    }
                                    else
                                    {
                                        m_QrCodeInfer = "test0000000000000000";
                                    }
                                    if (TotalNum== m_iPositionNum)
                                    {
                                        TotalNum = 0;
                                        sample_id_num++;
                                        if (m_listQrCodeInfer.Count>0)
                                        {
                                            m_listQrCodeInfer.RemoveAt(0);
                                        }
                                        
                                    }
                                    m_niInfer.SampleID = sample_id_num.ToString("000000");
                                    m_niInfer.Sample_Number = m_QrCodeInfer;
                                    m_stSaveParamInfer.pData = m_pSaveImageBuf_N[nCamIndex, m_nMemoryindex[nCamIndex]];
                                    m_niInfer.Time_Now = DateTime.Now.ToString("yyyyMMddHHmmss");
                                    m_niInfer.Position_Number = ((m_nMemoryindex[nCamIndex]) / 5 + nCamIndex + 1).ToString("00");
                                   
                                    m_niInfer.Light_Num = (m_nMemoryindex[nCamIndex]% 5).ToString("0");
                                    m_niInfer.Cam_Num = nCamIndex.ToString("00");
                                    //保存图像
                                    Save_file_tmp = ServerSaveFilePath + m_Job_name + "_" + m_niInfer.Sample_Number + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_S" + m_niInfer.SampleID + "_" + m_niInfer.Time_Now;
                                    string m_StsServers = "";
                                    m_StsServers = m_Job_name + "_" + m_niInfer.Sample_Number + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_S" + m_niInfer.SampleID + "_" + m_niInfer.Time_Now;
                                    m_stSaveParamInfer.enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                                    m_stSaveParamInfer.pImagePath = Save_file_tmp + ".jpg";
                                    m_stSaveParamInfer.nQuality = 98;//存Jpeg时有效
                                    int nRet1 = m_listMvCameras[0].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer);

                                    //给推理机的数据加入列表
                                    //LogHelper.LogInfo(string.Join(",", Filenames[nCamIndex]));
                                    Filenames.Add(m_StsServers + ".jpg");
                                    //存储缩略图
                                    string saveThumbnailFilePath = m_sThumbnailServerSaveFilePath + m_Job_name + "_" + m_niInfer.Sample_Number + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num +
                                        "_L" + m_niInfer.Light_Num + "_S" + m_niInfer.SampleID + "_" + m_niInfer.Time_Now;
                                    m_stSaveParamInfer.pImagePath = saveThumbnailFilePath + ".jpg";
                                    m_stSaveParamInfer.nQuality = 80;//存Jpeg时有效
                                    int nRet2 = m_listMvCameras[0].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer);
                                }
                            }
                        }
                    }


                }
            }
        }
        public void GetModelPoint(HWindowControl hWindowControl, int CamerNum,HObject hImage,int motorNum)
        {
            HalconOperator.ShowImage(hWindowControl, hImage);
            halconOperatorList[CamerNum+1].OnlyFindShapModel(hWindowControl.HalconWindow, hImage, out HalconOperator.ModelResult PointResult);
            List<PointXYU> pointXYUs = new List<PointXYU>();
            if (PointResult.Row!=0)
            {
                pointXYUs.Add(new PointXYU()
                {
                    X = (float)(ConfigVars.configInfo.calibrationData[CamerNum].ModelWordX -
               (((PointResult.Row - ConfigVars.configInfo.calibrationData[CamerNum].FitCenterRow)) * ConfigVars.configInfo.calibrationData[CamerNum].SinglePixelAccuracy)),
                    Y = (float)((ConfigVars.configInfo.calibrationData[CamerNum].ModelWordY +
               ((PointResult.Column - ConfigVars.configInfo.calibrationData[CamerNum].FitCenterColumn)
               * ConfigVars.configInfo.calibrationData[CamerNum].SinglePixelAccuracy))),
                    U = (float)(ConfigVars.configInfo.calibrationData[CamerNum].ModelWordU - PointResult.Angle)
                });
                GetPointList(CamerNum, pointXYUs, motorNum);
                hImage.Dispose();
            }
            else
            {
                LogHelper.LogError("未发现模板！");
            }
            
            
            
        }
        public void GetPointList(int camNo, List<PointXYU> points,int motorNum)
        {
            PointXYU pickPointsShow = new PointXYU();
            
            switch (camNo)
            {
                case 4:
                    PointXYU[] pickPoints1 = motorInstance.CalPickPoint(points, -1, out bool[] isFound1);
                    if (!isFound1[0])
                        MotorsClass.omronInstance.Write(plc_cam_status4, (float)2);
                    else
                    {
                        motorInstance.SendPoint(pickPoints1, camNo);
                        var reuslt = MotorsClass.omronInstance.Write(plc_cam_status4, (float)0);
                        if (reuslt == null)
                        {
                            Thread.Sleep(10);
                            var rasulta = MotorsClass.omronInstance.Write(plc_cam_status4, (float)0);
                            if (rasulta != null)
                            {
                                break;
                            }

                        }
                       
                    }
                    if (pickPoints1 != null)
                    {
                        pickPointsShow = pickPoints1[0];
                    }
                   

                    break;


                case 6:
                    PointXYU[] pickPoints6 = motorInstance.CalPickPoint(points, 0, out bool[] isFound6);

                    if (!isFound6[0])
                        MotorsClass.omronInstance.Write(plc_cam_status6, (float)2);
                    else
                    {
                        motorInstance.SendPoint(pickPoints6, camNo);
                        MotorsClass.omronInstance.Write(plc_cam_status6, (float)0);
                        string code = GetQrCode();

                        m_listQrCodeInfer.Add(code);
                        m_listQrCodeConnectionPositive.Add(code);
                        m_listQrCodeConnectionNegative.Add(code);
                        event_QrCodeShow(code);
                    }
                    if (pickPoints6 != null)
                    {
                        pickPointsShow = pickPoints6[0];
                    }
                    break;
                case 8:
                    PointXYU[] pickPoints8 = motorInstance.CalPickPoint(points, 1, out bool[] isFound8);
                    if (!isFound8[0])
                        MotorsClass.omronInstance.Write(plc_cam_status8, (float)2);
                    else
                    {
                        motorInstance.SendPoint(pickPoints8, camNo);
                        MotorsClass.omronInstance.Write(plc_cam_status8, (float)0);
                    }
                    if (pickPoints8 != null)
                    {
                        pickPointsShow = pickPoints8[0];
                    }
                    break;
                case 5:
                    if (motorNum==1)
                    {
                        PointXYU[] pickPoints5a = motorInstance.CalPickPoint(points, 3, out bool[] isFound51);
                        if (!isFound51[0])
                            MotorsClass.omronInstance.Write(plc_cam_status8, (float)2);
                        else
                        {
                            motorInstance.SendPoint(pickPoints5a, camNo);
                            MotorsClass.omronInstance.Write(plc_cam_status8, (float)0);
                        }
                        if (pickPoints5a != null)
                        {
                            pickPointsShow = pickPoints5a[0];
                        }
                    }
                    else
                    {
                        PointXYU[] pickPoints5b = motorInstance.CalPickPoint(points, 4, out bool[] isFound52);
                        if (!isFound52[0])
                            MotorsClass.omronInstance.Write(plc_cam_status6, (float)2);
                        else
                        {
                            motorInstance.SendPoint(pickPoints5b, camNo);
                            MotorsClass.omronInstance.Write(plc_cam_status6, (float)0);
                        }
                        if (pickPoints5b != null)
                        {
                            pickPointsShow = pickPoints5b[0];
                        }
                    }
                    
                    break;
               
                default:
                    break;
            }
            event_OnPositionDisp(camNo, pickPointsShow);
          
        }
        int m_nMemoryindex = 0;
        public void SaveConnectionImage(object ob)
        {
            int CamNum = (int)ob;
            int nCamIndex = 0;

            bool bHasData = false;
            while (!m_bWhileStatus_SaveConnectionImage)
            {

                if (m_bRunSaveImage_Connection)
                {
                    bHasData = false;
                    lock (m_lockBufForSaveConnectionImageLock)
                    {
                        if (m_dictImageConnectionIndex[nCamIndex].Count > 0)
                        {
                            m_nMemoryindex = m_dictImageConnectionIndex[nCamIndex].First();
                            m_dictImageConnectionIndex[nCamIndex].RemoveAt(0);
                            bHasData = true;
                        }
                    }

                    if (bHasData)
                    {
                        //保存图像
                        m_stSaveParamConnection[nCamIndex].pData = m_pSaveImageBuf_Connection[nCamIndex, m_nMemoryindex];
                        m_nameConnection.Time_Now = DateTime.Now.ToString("yyyyMMddHHmmss");
                        try
                        {
                            lock (this)
                            {
                                m_iSampleIdConnection_num++;
                                m_nameConnection.SampleID = m_iSampleIdConnection_num.ToString("000000");
                            }
                            if (m_listQrCodeConnectionPositive.Count>0|| m_listQrCodeConnectionNegative.Count>0)
                            {
                                switch (nCamIndex)
                                {
                                    case 0:
                                        if (m_listQrCodeConnectionPositive.Count > 0)
                                        {
                                            m_sQrCodeConnection = m_listQrCodeConnectionPositive.First();
                                            m_listQrCodeConnectionPositive.RemoveAt(0);
                                        }
                                        else
                                        {
                                            m_sQrCodeConnection = "test000000000000";
                                        }
                                       
                                        break;
                                    case 1:
                                        if (m_listQrCodeConnectionNegative.Count > 0)
                                        {
                                            m_sQrCodeConnection = m_listQrCodeConnectionNegative.First();
                                            m_listQrCodeConnectionNegative.RemoveAt(0);
                                        }
                                        else
                                        {
                                            m_sQrCodeConnection = "test000000000000";
                                        }
                                        break;
                                    default:
                                        break;
                                }


                                if (m_sQrCodeConnection == null)
                                {
                                    m_sQrCodeConnection = "Nc0000000000000000000000";
                                }

                                m_nameConnection.Position_Number = (nCamIndex + 8).ToString("00");
                                m_nameConnection.Light_Num = "0";
                                m_nameConnection.Cam_Num = (nCamIndex + 8).ToString("00");

                                m_nameConnection.Sample_Number = m_sQrCodeConnection;
                                string Save_file_tmp = ServerSaveFilePath + m_Job_name + "_" + m_nameConnection.Sample_Number + "_P" + m_nameConnection.Position_Number +
                                    "_C" + m_nameConnection.Cam_Num + "_L" + m_nameConnection.Light_Num + "_S" + m_nameConnection.SampleID + "_" + m_nameConnection.Time_Now;
                                m_stSaveParamConnection[nCamIndex].enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                                m_stSaveParamConnection[nCamIndex].pImagePath = Save_file_tmp + ".jpg";
                                m_stSaveParamConnection[nCamIndex].nQuality = 98;//存Jpeg时有效
                                int nRet1 = m_listMvCameras[5].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamConnection[nCamIndex]);
                                /*if (nRet1 == 0)
                                {
                                    LogHelper.LogInfo("连接器保存图像成功！");
                                }
                                else
                                {
                                    LogHelper.LogError("连接器保存图像失败！");
                                }*/
                                string saveThumbnailFilePath = m_sThumbnailServerSaveFilePath + m_Job_name + "_" + m_nameConnection.Sample_Number + "_P" + m_nameConnection.Position_Number + "_C" + m_nameConnection.Cam_Num + "_L" + m_nameConnection.Light_Num + "_S" + m_nameConnection.SampleID + "_" + m_nameConnection.Time_Now;
                                m_stSaveParamConnection[nCamIndex].pImagePath = saveThumbnailFilePath + ".jpg";
                                m_stSaveParamConnection[nCamIndex].nQuality = 80;//存Jpeg时有效
                                int nRet2 = m_listMvCameras[5].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamConnection[nCamIndex]);
                               /* if (nRet2 == 0)
                                {
                                    LogHelper.LogInfo("连接器保存缩略图像成功！");
                                }
                                else
                                {
                                    LogHelper.LogError("连接器保存缩略图像失败！");
                                }*/
                            }
                            

                        }
                        catch (Exception ex)
                        {

                            System.Windows.MessageBox.Show(ex.ToString());
                        }

                    }

                    nCamIndex++;
                    if (nCamIndex == 2)
                    {
                        nCamIndex = 0;
                    }

                    if (!bHasData)
                    {
                        Thread.Sleep(10);
                    }
                }
            }
        }
        /// <summary>
        /// 停机警告
        /// </summary>
        public void Alarm()
        {
            LogHelper.LogError("报警停机!");
            m_bWhileEnd = true;
            m_bWhileIsSendReasoningMachine = true;
            for (int i = 0; i < m_nCanOpenDeviceNum; ++i)
            {
                int nRet;
                nRet = m_pMyCamera[i].MV_CC_CloseDevice_NET();
                if (MyCamera.MV_OK != nRet)
                {
                    return;
                }

                nRet = m_pMyCamera[i].MV_CC_DestroyDevice_NET();
                if (MyCamera.MV_OK != nRet)
                {
                    return;
                }
            }
            /*  //初始化帧数
              InitializeFrames();
              //控件操作 ch: | en:Control Operation
              SetCtrlWhenClose();
              // ch:取流标志位清零 | en:Zero setting grabbing flag bit
              m_bGrabbing = false;
              // ch:重置成员变量 | en:Reset member variable
              ResetMember();
              if (m_bIsConnectionPLC)
              {
                  TcpSendMessage("stop#");
              }
              else
              {
                  LogHelper.LogError("已经屏蔽PLC");
              }*/

        }
        public void SendMessage2ReasoningMachine()
        {

            bool bHasData = false;

            while (!m_bWhileIsSendReasoningMachine)
            {
                try
                {
                    //和推理机通讯
                    if (m_bConnectionServer == true)
                    {
                        if (m_bCheckConnected)
                        {
                            if (Filenames.Count >= 5)
                            {
                                bHasData = false;
                                lock (m_SendReasoningMachineLock)
                                {
                                    m_nSendInference++;
                                    m_arrictAscr.file_names = Filenames.GetRange(0, 5);
                                    Filenames.RemoveRange(0, 5);
                                    bHasData = true;
                                }
                                if (bHasData)
                                {
                                    
                                    m_arrictAscr.relative_dir = ServerSaveFilePath;

                                    LogHelper.LogInfo(string.Join("\n", m_arrictAscr));
                                    m_ayySrdn = hq.CommitSample(Client_IP, Cilent_Port, m_arrictAscr);

                                    m_arrictAscr2.pose_result.Add(m_ayySrdn.data);
                                    m_arrictAscr2.sample_id = m_listSampleIDs[0];
                                    try
                                    {
                                        if (m_ayySrdn == null || m_ayySrdn.data.res == null)
                                        {
                                            LogHelper.LogError("推理服务器返回错误!");
                                            Alarm();

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LogHelper.LogError("推理服务器返回错误!");
                                        System.Windows.MessageBox.Show(ex.ToString());

                                    }

                                    if (m_ayySrdn != null)
                                    {
                                        //显示结果
                                        event_ProcessSampleResultData_NVT(m_ayySrdn);
                                        break;

                                    }
                                    if (m_nSendInference == 7)
                                    {
                                        m_nSendInference = 0;
                                        m_ayySrdn2 = hq.CommitSample2(Client_IP, Cilent_Port, m_arrictAscr2);
                                       
                                        
                                        m_listSampleIDs.Clear();
                                        totalNumberSamples++;
                                        //显示总数
                                        event_LabelDisplay(totalNumberSamples, 0);
                                     
                                        OperateResult<string> word = MotorsClass.omronInstance.ReadString(MotorsClass.plc_Qrcode_motor_ConnectorPositive, 10);
                                        string code = word.Content;
                                        m_arrictAscr2.pose_result[0].file_path.Substring(0, 12);
                                        //临时演示用
                                        m_ayySrdn2.data.sample_id = code;

                                        if (code.Contains(m_arrictAscr2.pose_result[0].file_path.Substring(0, 12)))
                                        {
                                            if (m_ayySrdn2.data.code != "OK")
                                            {
                                                MotorsClass.omronInstance.Write(motorInstance.plc_Result_motor, (float)1);
                                            }
                                            else
                                            {
                                                MotorsClass.omronInstance.Write(motorInstance.plc_Result_motor, (float)2);
                                            }
                                        }
                                        else
                                        {
                                            LogHelper.LogError("二维码不匹配！");
                                        }
                                        MotorsClass.omronInstance.Write(motorInstance.plc_Result_motor, (float)2);
                                       //显示结果到列表
                                        event_ProcessSampleResultData2_NVT(m_ayySrdn2);
                                        m_arrictAscr2.pose_result.Clear();
                                    }
                                }

                            }
                            /*  else
                              {
                                  LogHelper.LogInfo("图像数量不够5个");

                              }*/

                        }
                        else
                        {
                            LogHelper.LogInfo("推理机通讯断开!");
                        }

                    }
                }
                catch (Exception ex)
                {

                    System.Windows.MessageBox.Show(ex.ToString());
                }
            }
        }
        public void Show_image2Picbox(SampleResultData_NVT SRDN, PictureBox pic1, PictureBox pic2, PictureBox pic3, PictureBox pic4, PictureBox pic5, PictureBox pic6, PictureBox pic7, PictureBox pic_Show)
        {

            List<string> tmp = new List<string>();
            string[] PosFlaggetAry1 = SRDN.data.file_path.Split('_');//单个字符分割

            switch (PosFlaggetAry1[2])
            {
                case "P01":
                    if (pic1.InvokeRequired)
                    {
                        pic1.BeginInvoke(new MethodInvoker(() =>
                        {
                            pic1.Image = Image.FromFile(ServerSaveFilePath + SRDN.data.result_path);
                            Pic_Path1 = ServerSaveFilePath + SRDN.data.result_path;
                            pic_Show.Image = pic1.Image;
                            PicShow_Path = Pic_Path1;
                        }));
                    }
                    else
                    {
                        pic1.Hide();
                    }

                    break;
                case "P02":
                    if (pic2.InvokeRequired)
                    {
                        pic1.BeginInvoke(new MethodInvoker(() =>
                        {
                            pic2.Image = Image.FromFile(ServerSaveFilePath + SRDN.data.result_path);
                            Pic_Path2 = ServerSaveFilePath + SRDN.data.result_path;
                            pic_Show.Image = pic2.Image;
                            PicShow_Path = Pic_Path2;
                        }));
                    }
                    else
                    {
                        pic2.Hide();
                    }
                    break;
                case "P03":
                    if (pic3.InvokeRequired)
                    {
                        pic1.BeginInvoke(new MethodInvoker(() =>
                        {
                            pic3.Image = Image.FromFile(ServerSaveFilePath + SRDN.data.result_path);
                            Pic_Path3 = ServerSaveFilePath + SRDN.data.result_path;
                            pic_Show.Image = pic3.Image;
                            PicShow_Path = Pic_Path3;
                        }));
                    }
                    else
                    {
                        pic3.Hide();
                    }
                    break;
                case "P04":
                    if (pic4.InvokeRequired)
                    {
                        pic1.BeginInvoke(new MethodInvoker(() =>
                        {
                            pic4.Image = Image.FromFile(ServerSaveFilePath + SRDN.data.result_path);
                            Pic_Path4 = ServerSaveFilePath + SRDN.data.result_path;
                            pic_Show.Image = pic4.Image;
                            PicShow_Path = Pic_Path4;
                        }));
                    }
                    else
                    {
                        pic4.Hide();
                    }
                    break;
                case "P05":
                    if (pic5.InvokeRequired)
                    {
                        pic1.BeginInvoke(new MethodInvoker(() =>
                        {
                            pic5.Image = Image.FromFile(ServerSaveFilePath + SRDN.data.result_path);
                            Pic_Path5 = ServerSaveFilePath + SRDN.data.result_path;
                            pic_Show.Image = pic5.Image;
                            PicShow_Path = Pic_Path5;
                        }));
                    }
                    else
                    {
                        pic5.Hide();
                    }
                    break;
                case "P06":
                    if (pic6.InvokeRequired)
                    {
                        pic1.BeginInvoke(new MethodInvoker(() =>
                        {
                            pic6.Image = Image.FromFile(ServerSaveFilePath + SRDN.data.result_path);
                            Pic_Path6 = ServerSaveFilePath + SRDN.data.result_path;
                            pic_Show.Image = pic6.Image;
                            PicShow_Path = Pic_Path6;
                        }));
                    }
                    else
                    {
                        pic6.Hide();
                    }
                    break;
                case "P07":
                    if (pic7.InvokeRequired)
                    {
                        pic1.BeginInvoke(new MethodInvoker(() =>
                        {
                            pic7.Image = Image.FromFile(ServerSaveFilePath + SRDN.data.result_path);
                            Pic_Path7 = ServerSaveFilePath + SRDN.data.result_path;
                            pic_Show.Image = pic7.Image;
                            PicShow_Path = Pic_Path7;
                        }));
                    }
                    else
                    {
                        pic7.Hide();
                    }
                    break;
                default:
                    break;
            }
        }
        public void Show_Result2Table(SampleResultData2_NVT SRDN, DataGridView dataGridView1, int tmp_camIsErrorSum)
        {


            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.BeginInvoke(new MethodInvoker(() =>
                {
                    int index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = (index + 1).ToString();
                    dataGridView1.Rows[index].Cells[1].Value = SRDN.data.sample_id;
                    if (tmp_camIsErrorSum > 0)
                    {
                        dataGridView1.Rows[index].Cells[2].Value = "Yes";
                        lock (m_NgorOKLock)
                        {
                            ngSamples++;
                        }

                        //lbl_NGSampleNums.Text = ngSamples.ToString();
                        event_LabelDisplay(ngSamples, 1);
                        
                    }
                    else
                    {
                        dataGridView1.Rows[index].Cells[2].Value = "No";
                        lock (m_NgorOKLock)
                        {
                            okSamples++;
                        }

                        //lbl_OKSampleNums.Text = okSamples.ToString();
                        event_LabelDisplay(okSamples, 2);
                        MotorsClass.omronInstance.Write(plc_cam_status4, (float)1);
                    }
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
                }));
            }
            else
            {
                dataGridView1.Hide();
            }

        }
        public bool Login(int level)
        {
            LoginFrm frm = new LoginFrm(level);
            return (frm.ShowDialog() == DialogResult.OK);
        }
        public bool CheckHImageFrmIsOpen()
        {
            return WindowHelper.CheckFormIsOpen("CalibrationFrm0") || WindowHelper.CheckFormIsOpen("CalibrationFrm1")
                || WindowHelper.CheckFormIsOpen("CreateShapeModelFrm");
        }
        public string SelectFolder(string Describe, bool ShowNewFolder = true)
        {
            using (VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog())
            {
                fbd.SelectedPath = Application.StartupPath + "\\Project\\";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    if (fbd.SelectedPath.Contains(Application.StartupPath + "\\Project\\"))
                    {
                        return fbd.SelectedPath;
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("请选择" + Application.StartupPath + "\\Project\\下的项目！");
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }

        }
        public void prepareData(int model_flag)

        {
            // Local iconic variables 

            HObject ho_ImageBatch = null, ho_Image = null;
            HObject ho_SegmentationImage = null, ho_ClassRegions = null;
            HObject ho_ClassRegion = null, ho_ConnectedRegions = null, ho_CurrentRegion = null;

            // Local control variables 

            HTuple hv_OKorNGflag = new HTuple(), hv_ShowExampleScreens = new HTuple();
            HTuple hv_UsePretrainedModel = new HTuple(), hv_DLDeviceHandles = new HTuple();
            HTuple hv_DLDevice = new HTuple(), hv_ImageDir = new HTuple();
            HTuple hv_ExampleDataDir = new HTuple(), hv_PreprocessParamFileName = new HTuple();
            HTuple hv_RetrainedModelFileName = new HTuple(), hv_ClassNames = new HTuple();
            HTuple hv_ClassIDs = new HTuple(), hv_BatchSizeInference = new HTuple();
            HTuple hv_DLModelHandle = new HTuple(), hv_DLPreprocessParam = new HTuple();
            HTuple hv_WindowHandleDict = new HTuple();
            HTuple hv_GenParamDisplay = new HTuple(), hv_ImageFiles = new HTuple();
            HTuple hv_BatchIndex = new HTuple(), hv_Batch = new HTuple();
            HTuple hv_BaseName = new HTuple(), hv_Extension = new HTuple();
            HTuple hv_Directory = new HTuple(), hv_DLSampleBatch = new HTuple();
            HTuple hv_DLResultBatch = new HTuple(), hv_SampleIndex = new HTuple();
            HTuple hv_WindowHandles = new HTuple(), hv_Areas = new HTuple();
            HTuple hv_ClassIndex = new HTuple(), hv_Area = new HTuple();
            HTuple hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_ConnectIndex = new HTuple(), hv_Text = new HTuple();
            HTuple hv_BoxColor = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImageBatch);
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_SegmentationImage);
            HOperatorSet.GenEmptyObj(out ho_ClassRegions);
            HOperatorSet.GenEmptyObj(out ho_ClassRegion);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_CurrentRegion);
            try
            {
                hv_OKorNGflag.Dispose();
                hv_OKorNGflag = 0;

                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "margin");
                }
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetLineWidth(HDevWindowStack.GetActive(), 3);
                }
                hv_ShowExampleScreens.Dispose();
                hv_ShowExampleScreens = 1;

                hv_UsePretrainedModel.Dispose();
                hv_UsePretrainedModel = 0;

                hv_DLDeviceHandles.Dispose();
                HOperatorSet.QueryAvailableDlDevices((new HTuple("runtime")), (new HTuple("gpu")), out hv_DLDeviceHandles);
                if ((int)(new HTuple((new HTuple(hv_DLDeviceHandles.TupleLength())).TupleEqual(
                    0))) != 0)
                {
                    throw new HalconException("No supported device found to continue this example.");
                }
                //Due to the filter used in query_available_dl_devices, the first device is a GPU, if available.
                hv_DLDevice.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_DLDevice = hv_DLDeviceHandles.TupleSelect(
                        0);
                }


                hv_ExampleDataDir.Dispose();

                hv_PreprocessParamFileName.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    if (model_flag == 1)
                    {
                        Model_Path = m_sModel_Path_P1;
                    }
                    else if (model_flag == 6)
                    {
                        Model_Path = m_sModel_Path_P2_P6;
                    }
                    hv_PreprocessParamFileName = Model_Path + "/PreprocessParamFileName.hdict";
                }
                //Path of the retrained segmentation model.

                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {

                    hv_RetrainedModelFileName = m_sModel_Path_P1 + "/model_best.hdl";

                }

                //
                //Provide the class names and IDs.
                //标签类
                hv_ClassNames.Dispose();
                hv_ClassNames = new HTuple();
                hv_ClassNames[0] = "background";
                hv_ClassNames[1] = "AK";
                hv_ClassNames[2] = "FL";
                hv_ClassNames[3] = "GS";
                hv_ClassNames[4] = "HH";
                hv_ClassNames[5] = "GB";
                hv_ClassNames[6] = "LL";
                hv_ClassNames[7] = "ZY";
                hv_ClassNames[8] = "JQ";
                hv_ClassNames[9] = "JY";
                hv_ClassNames[10] = "JC";
                hv_ClassNames[11] = "JZZ";
                hv_ClassNames[12] = "MLP";
                hv_ClassNames[13] = "ZW";
                //标签id
                hv_ClassIDs.Dispose();
                hv_ClassIDs = new HTuple();
                hv_ClassIDs[0] = 0;
                hv_ClassIDs[1] = 1;
                hv_ClassIDs[2] = 2;
                hv_ClassIDs[3] = 3;
                hv_ClassIDs[4] = 4;
                hv_ClassIDs[5] = 5;
                hv_ClassIDs[6] = 6;
                hv_ClassIDs[7] = 7;
                hv_ClassIDs[8] = 8;
                hv_ClassIDs[9] = 9;
                hv_ClassIDs[10] = 10;
                hv_ClassIDs[11] = 11;
                hv_ClassIDs[12] = 12;
                hv_ClassIDs[13] = 13;
                //
                //Batch Size used during inference.
                hv_BatchSizeInference.Dispose();
                hv_BatchSizeInference = 1;

                hv_DLModelHandle.Dispose();

                HOperatorSet.ReadDlModel(hv_RetrainedModelFileName, out hv_DLModelHandle);
                switch (model_flag)
                {
                    case 1:
                        m_hv_DLModelHandle_P01 = hv_DLModelHandle;
                        break;
                    case 6:
                        m_hv_DLModelHandle_P2_P6 = hv_DLModelHandle;
                        break;
                    default:
                        break;
                }

                HOperatorSet.SetDlModelParam(hv_DLModelHandle, "batch_size", hv_BatchSizeInference);

                HOperatorSet.SetDlModelParam(hv_DLModelHandle, "device", hv_DLDevice);

                hv_DLPreprocessParam.Dispose();
                HOperatorSet.ReadDict(hv_PreprocessParamFileName, new HTuple(), new HTuple(),
                    out hv_DLPreprocessParam);
                //
                //Set parameters for visualization of results.
                hv_WindowHandleDict.Dispose();
                HOperatorSet.CreateDict(out hv_WindowHandleDict);
                m_hv_DatasetInfo.Dispose();
                HOperatorSet.CreateDict(out m_hv_DatasetInfo);
                HOperatorSet.SetDictTuple(m_hv_DatasetInfo, "class_ids", hv_ClassIDs);
                HOperatorSet.SetDictTuple(m_hv_DatasetInfo, "class_names", hv_ClassNames);
                hv_GenParamDisplay.Dispose();
                HOperatorSet.CreateDict(out hv_GenParamDisplay);
                HOperatorSet.SetDictTuple(hv_GenParamDisplay, "segmentation_exclude_class_ids",
                    0);
                HOperatorSet.SetDictTuple(hv_GenParamDisplay, "segmentation_transparency",
                    "80");
                HOperatorSet.SetDictTuple(hv_GenParamDisplay, "font_size", 16);


            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_ImageBatch.Dispose();
                ho_Image.Dispose();
                ho_SegmentationImage.Dispose();
                ho_ClassRegions.Dispose();
                ho_ClassRegion.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_CurrentRegion.Dispose();

                hv_OKorNGflag.Dispose();
                hv_ShowExampleScreens.Dispose();
                hv_UsePretrainedModel.Dispose();
                hv_DLDevice.Dispose();
                hv_ImageDir.Dispose();
                hv_ExampleDataDir.Dispose();
                hv_PreprocessParamFileName.Dispose();
                hv_RetrainedModelFileName.Dispose();
                hv_ClassNames.Dispose();
                hv_ClassIDs.Dispose();
                hv_BatchSizeInference.Dispose();
                hv_DLModelHandle.Dispose();
                hv_DLPreprocessParam.Dispose();
                hv_WindowHandleDict.Dispose();
                m_hv_DatasetInfo.Dispose();
                hv_GenParamDisplay.Dispose();
                hv_ImageFiles.Dispose();
                hv_BatchIndex.Dispose();
                hv_Batch.Dispose();
                hv_BaseName.Dispose();
                hv_Extension.Dispose();
                hv_Directory.Dispose();
                hv_DLSampleBatch.Dispose();
                hv_DLResultBatch.Dispose();
                hv_SampleIndex.Dispose();
                hv_WindowHandles.Dispose();
                hv_Areas.Dispose();
                hv_ClassIndex.Dispose();
                hv_Area.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_ConnectIndex.Dispose();
                hv_Text.Dispose();
                hv_BoxColor.Dispose();

                throw HDevExpDefaultException;
            }
            ho_ImageBatch.Dispose();
            ho_Image.Dispose();
            ho_SegmentationImage.Dispose();
            ho_ClassRegions.Dispose();
            ho_ClassRegion.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_CurrentRegion.Dispose();

            hv_OKorNGflag.Dispose();
            hv_ShowExampleScreens.Dispose();
            hv_UsePretrainedModel.Dispose();
            hv_DLDeviceHandles.Dispose();
            hv_ImageDir.Dispose();
            hv_ExampleDataDir.Dispose();
            hv_PreprocessParamFileName.Dispose();
            hv_RetrainedModelFileName.Dispose();
            hv_ClassNames.Dispose();
            hv_ClassIDs.Dispose();
            hv_BatchSizeInference.Dispose();
            hv_DLModelHandle.Dispose();
            hv_DLPreprocessParam.Dispose();
            hv_WindowHandleDict.Dispose();
            m_hv_DatasetInfo.Dispose();
            hv_GenParamDisplay.Dispose();
            hv_ImageFiles.Dispose();
            hv_BatchIndex.Dispose();
            hv_Batch.Dispose();
            hv_BaseName.Dispose();
            hv_Extension.Dispose();
            hv_Directory.Dispose();
            hv_DLSampleBatch.Dispose();
            hv_DLResultBatch.Dispose();
            hv_SampleIndex.Dispose();
            hv_WindowHandles.Dispose();
            hv_Areas.Dispose();
            hv_ClassIndex.Dispose();
            hv_Area.Dispose();
            hv_Row.Dispose();
            hv_Column.Dispose();
            hv_ConnectIndex.Dispose();
            hv_Text.Dispose();
            hv_BoxColor.Dispose();

        }

        private HObject Intptr2Hobject(IntPtr pData, MyCamera.MV_SAVE_IMG_TO_FILE_PARAM pFrameInfo)
        {
            HObject Hobj = new HObject();
            try
            {
                HOperatorSet.GenImage1Extern(out Hobj, "byte", (HTuple)pFrameInfo.nWidth, (HTuple)pFrameInfo.nHeight, pData, IntPtr.Zero);

            }
            catch (System.Exception ex)
            {
                LogHelper.LogError(ex.ToString());

            }
            return Hobj;
        }

        /// 获取指定驱动器的空间总大小(单位为B)
        /// </summary>
        /// <param name="str_HardDiskName">只需输入代表驱动器的字母即可 </param>
        /// <returns> </returns>
        public static long GetHardDiskSpace(string str_HardDiskName)
        {
            long totalSize = new long();
            str_HardDiskName = str_HardDiskName + ":\\";
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    totalSize = drive.TotalSize;
                }
            }
            return totalSize;
        }

        /// <summary>
        /// 获取指定驱动器的剩余空间总大小(单位为B)
        /// </summary>
        /// <param name="str_HardDiskName">只需输入代表驱动器的字母即可 </param>
        /// <returns> </returns>
        public static long GetHardDiskFreeSpace(string str_HardDiskName)
        {
            long freeSpace = new long();
            str_HardDiskName = str_HardDiskName + ":\\";
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    freeSpace = drive.TotalFreeSpace;
                }
            }
            return freeSpace;
        }

    }
}
