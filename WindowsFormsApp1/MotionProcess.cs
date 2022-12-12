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
using WindowsFormsApp1;
using ClosedXML.Excel;
using BIS.Communication;
using OracleHelper;
using Oracle.ManagedDataAccess;
using System.IO;
using System.Text;
using System.Net;
using RestClientLib;
using Newtonsoft.Json;

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
        private const int m_iPositionNum = 7;
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

        public static double[] motion_SinglePixelAccuracy = new double[NeedLocate];

        private List<string>[] m_listQrCodeInfer = new List<string>[m_iPositionNum * 2];
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
        public static bool m_bWhileIsSendReasoningMachine = false;
        public static bool m_bRunWhileIsSendReasoningMachine = true;
        //开启发送推理机结果到PLC标志
        public static bool m_bWhileIsSendReasoningResultToPlc = true;
        //开启移动图片的标志
        public static bool m_bMoveFile = true;
        //推理机屏蔽按钮判断
        bool m_bConnectionServer = true;

        // 定义一个静态变量来保存类的实例
        private static MotionProcess mpInstance;
        // 定义一个标识确保线程同步
        private static readonly object locker = new object();
        public bool abort = true;//设备运行停止标识
        public static bool Loseimage = false;//设备运行停止标识

        public static bool AccCheck = false;
        public static bool AccCheckA = false;
        public static List<MvClass> m_listMvCameras = new List<MvClass>();
        List<HalconOperator> halconOperatorList = new List<HalconOperator>();
        MotorsClass motorInstance;
        #region 线程部分
        Thread m_thMotion_thread;//设备运行线程
        Thread[] m_thSaveImage_thread = new Thread[m_nCameraTotalNum];//设备保存图像线程

        Thread m_thSaveQrcode_thread;//二维码读取线程
        Thread m_thSaveQrcode_thread1;//二维码读取线程
        Thread m_thSaveQrcode_thread2;//二维码读取线程
        Thread m_thSaveQrcode_thread3;//二维码读取线程
        List<string>[] Qrcode = new List<string>[8];//本体二维码读取
        public static List<string> Qrcode1;//本体二维码读取
        public static List<string> Qrcode2;//本体二维码读取
        public static List<string> Qrcode3;//本体二维码读取
        public static List<string> Qrcode4;//本体二维码读取
        public static List<string> QrcodeTemple;//本体二维码读取
                                                //治具号没序列化
        public static List<string> FixtureNumber;
        public static List<string> FixtureNumberToMes;
        public static List<string> FixtureNumberTempel;

        public static List<string> QrcodeFisrtRead;//上料二维码读取（iPhone）
        public static List<string> QrcodeFisrtReadTemple;
        public static List<string> QrcodeConnectPRead;//连接器2正面二维码读取
        public static List<string> QrcodeConnectPReadTemple;
        public static List<string> ResulutConnect2Id;
        public static List<string> QrcodeConnectNRead;//连接器1反面二维码读取
        public static List<string> QrcodeConnectNReadTemple;
        public static List<string> ResulutConnect1Id;
        //生成表格
        public static CreateTheExl creattheexl = new CreateTheExl();
        XLWorkbook G_w = new XLWorkbook();
        IXLWorksheet iws;

        XLWorkbook G_wConnect = new XLWorkbook();
        IXLWorksheet iwsConnect;

        XLWorkbook G_wConnect1 = new XLWorkbook();
        IXLWorksheet iwsConnect1;

        int ResultFlag = 1;
        int ConnectResultFlag = 1;
        int ConnectResultFlag1 = 1;
        int ConnectTotal = 0;
        int ConnectNg = 0;
        int ConnectOk = 0;
        int ConnectTotal1 = 0;
        int ConnectNg1 = 0;
        int ConnectOk1 = 0;
        int TheLasterCount = 0;
        int TheLasterOkCount = 0;
        int TheLasterNgCount = 0;
        /// <summary>
        /// 心跳线程
        /// </summary>
        Thread m_thHeartBeat_thread;
        bool m_bFlagHeartBeat = true;
        public static bool IsSaveQrcode = true;
        bool ReadFirstCode = true;
        bool IsSaveQrcode1 = true;
        public static bool IsSaveQrcode2 = true;
        bool IsSaveQrcode3 = true;
        bool IsSaveQrcode4 = true;
        Thread m_thSaveInferImage_thread;
        Thread m_threadSaveImage;
        Thread m_threadSaveInferImage;
        Thread m_thdSendReasoningMachine;
        Thread m_thdSendReasoningResultToPlc;
        Thread m_thSeparateToWholeNames_thread;//设备保存图像线程
        Thread m_thSeparateToWholeNames_thread1;//设备保存图像线程
        Thread m_thSeparateToWholeNames_thread2;//设备保存图像线程
        Thread m_MoveFile_thread;
        #endregion
        #region 委托事件部分
        public delegate void ImgDispHandler(HObject hImage, int camNo, int MotorNum);
        public static event ImgDispHandler event_OnImgDisp;

        public delegate void DataDispHandler(int camNum, MyCamera.MV_DISPLAY_FRAME_INFO stDisplayInfo, IntPtr data);
        public static event DataDispHandler event_OnDataDisp;

        public delegate void QrCodeShow(string code);
        public static event QrCodeShow event_QrCodeShow;

        public delegate void PositionDispHandler(int tooNo, PointXYU pointXYU);
        public static event PositionDispHandler event_OnPositionDisp;
        public delegate void NumDispHandler(int number, int flag);
        public static event NumDispHandler event_LabelDisplay;
        public static event NumDispHandler event_LabelConnectDisplay;
        public static event NumDispHandler event_LabelConnectDisplay1;
        public static event NumDispHandler event_LabelTheLastResultDisplay;
        public static event NumDispHandler event_LabelDiskDisplay;

        public delegate void ResultDispHandler(string sampleId, string Resultcode);
        public static event ResultDispHandler event_ResultDisplay;

        public delegate void DelegateSampleResultData_NVT(SampleResultData_NVT data);
        public static event DelegateSampleResultData_NVT event_ProcessSampleResultData_NVT;
        public delegate void DelegateSampleResultData2_NVT(SampleResultData2_NVT data);
        public static event DelegateSampleResultData2_NVT event_ProcessSampleResultData2_NVT;
        public delegate void DelegateSampleConnectResultData(List<string> resultid, string code);
        public static event DelegateSampleConnectResultData ProcessSampleConnectResultData2_NVT_Data;
        public static event DelegateSampleConnectResultData ProcessSampleConnectResultData2_NVT_Data1;
        private object lockfileimagelock = new object();
        private object Directoryimagelock = new object();
        private object CysServerlock = new object();
        public static object HslCommunicationlock = new object();


        #endregion
        #region 相机和通讯变量部分
        public static OmronFinsNet omronInstance1, omronInstance2;
        public static bool Camera_state = true;
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
        public static string plc_HeatBeat_status = "D4000";////读写 0待机 1拍照 2拍照NG 


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
        public static List<string>[] m_arrPositionFilnames = new List<string>[m_iPositionNum];
        public static List<string>[] m_arrPositionFilnames1 = new List<string>[m_iPositionNum];
        public static List<string>[] m_arrPositionFilnames2 = new List<string>[m_iPositionNum];

        public static List<string> m_arrPositionConnectFilnames = new List<string>();
        public static List<string> m_arrPositionConnectFilnames1 = new List<string>();
        //开启保存图像的线程标志
        bool m_bWhileStatus_SaveInferImage = false;
        public static bool m_bWhileStatus_SaveConnectionImage = false;
        bool m_bWhileStatus_SaveFPCImage = false;
        bool m_bRunSaveImage_Infer = true;
        bool m_bRunSaveImage_Connection = true;
        bool m_bRunSaveImage_FPC = true;
        bool m_bRunSaveImage = true;
        bool m_bRunSaveQrcode = true;
        bool m_bRunSaveQrcode1 = true;
        public static bool m_bWhileCsvWrite = true;
        public static bool m_bWhileSeparateToWholeNames = true;
        public static bool m_bWhileSeparateToWholeNames1 = true;
        public static bool m_bWhileSeparateToWholeNames2 = true;
        public bool m_bRunWhileSeparateToWholeNames = true;
        public bool m_bRunWhileSeparateToWholeNames1 = true;
        public bool m_bRunWhileSeparateToWholeNames2 = true;
        public static bool IsCloseInfer = false;
        private Object m_BufForSaveImageLock = new Object();
        private object m_MemoryaveImageLock1 = new object();
        private object m_MemoryaveImageLock2 = new object();
        private object m_SamNumLock = new object();
        private object m_MemoryaveImageCameraIndexLock = new object();

        private object m_MemoryaveImageNumberLock = new object();

        private object m_MemoryaveCamNumberLock1 = new object();
        private object[] Position_NumberLock = new object[4];
        private object visoninputLock = new object();
        List<string> test = new List<string>();

        private Object m_lockBufForSaveConnectionImageLock = new Object();
        private Object m_lockBufForSaveFPCImageLock = new Object();
        private Object m_lockBufForFilenameImageLock = new Object();
        public static MyCamera.MV_SAVE_IMG_TO_FILE_PARAM[] m_stSaveParamInfer = new MyCamera.MV_SAVE_IMG_TO_FILE_PARAM[4];
        //连接器检测钢片和金属面共两个，再加上Fpc
        public static MyCamera.MV_SAVE_IMG_TO_FILE_PARAM[] m_stSaveParamConnection = new MyCamera.MV_SAVE_IMG_TO_FILE_PARAM[m_nCameraSaveConnectionNum];

        name_Image m_nameConnection = new name_Image();

        name_Image m_niInfer = new name_Image();
        //现在的时间路径
        string m_sJob_name_file = null;
        //样本序号
        public static int[] sample_id_num = new int[m_iPositionNum];
        int[] m_iSampleIdConnection_num = new int[2];
        int m_iSampleIdFPC_num = 0;
        public static List<string> m_listSampleIDs;
        //二维码位数
        public static int TheLengthQrcode = 11;
        //复检二维码
        public static List<string> m_ReCheckIdNumber;
        public static List<string> m_ReCheckIdNumberTempA = new List<string>();
        public static List<string> m_ReCheckIdNumberFalgA = new List<string>();
        int ReCheckFalgA = 1;
        public static List<string> m_ReCheckIdNumberTempB = new List<string>();
        List<int>[] m_listTotalSample = new List<int>[m_iPositionNum];

        //识别二维码
        string qrCode = "";
        string[] qrCode_back = new string[m_nCameraSaveNum];
        string m_QrCodeInfer = "";
        List<string> Qcode = new List<string>();
        string m_sQrCodeConnection = "";
        string qrCode_backInfer = "";
        //模板文件
        HTuple hv_DataCodeHandle1 = new HTuple();
        private MyCamera[] m_pMyCamera;
        //List<string>[] Filenames = new List<string>[m_nCameraSaveNum];
        List<string> Filenames = new List<string>();
        //开启保存图像的线程标志
        public static bool m_bWhileEnd = false;
        int m_icamNo;//相机序号
        //推理机使用相机
        bool m_bConnectionServerCamNum = true;
        //CYS服务器是否连接
        bool m_CheckConnectedCys = false;
        public static bool IsOpenConectedServer = true;
        //服务器是否链接
        bool m_bCheckConnected;
        private Object m_SendReasoningMachineLock = new object();
        private Object m_SendReasoningMachineLock1 = new object();
        private Object m_SendReasoningMachineLock2 = new object();
        public static Object JsonConvertLock = new object();
        int m_nSendInference = 0;
        private Object m_WhileSeparateToWholeNames = new Object();
        private Object m_WhileSeparateToWholeNames1 = new Object();
        private Object m_WhileSeparateToWholeNames2 = new Object();
        //推理机第一次返回结果
        List<Data_NVT_Defect_Message>[] Pose_result = new List<Data_NVT_Defect_Message>[m_iPositionNum];
        public static List<Data_NVT_Defect_Message> Pose_result1;
        public static List<Data_NVT_Defect_Message> Pose_result2;
        public static List<Data_NVT_Defect_Message> Pose_result3;
        public static List<Data_NVT_Defect_Message> Pose_result4;
        public static List<Data_NVT_Defect_Message> Pose_result5;
        public static List<Data_NVT_Defect_Message> Pose_result6;
        public static List<Data_NVT_Defect_Message> Pose_result7;
        /// <summary>
        /// 发送服务器容器
        /// </summary>
        AlgoSampleCommitRequest_NVT m_arrictAscr = new AlgoSampleCommitRequest_NVT();
        AlgoSampleCommitRequest_NVT m_arrictAscr1 = new AlgoSampleCommitRequest_NVT();
        AlgoSampleCommitRequest_NVT m_arrictAscr02 = new AlgoSampleCommitRequest_NVT();
        /// <summary>
        /// 接受服务器容器
        /// </summary>
        SampleResultData_NVT m_ayySrdn = new SampleResultData_NVT();
        SampleResultData_NVT m_ayySrdn1 = new SampleResultData_NVT();
        SampleResultData_NVT m_ayySrdn02 = new SampleResultData_NVT();
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

        /// <summary>
        /// mes 上传数据类
        /// </summary>
        TestingBLL MesHelper = new TestingBLL();

        public static List<string> m_ConncetionResultId;
        public static List<string> m_Conncetion1ResultId;
        public static List<string> m_ConncetionResultIdToExl;
        public static List<string> m_ConncetionResultIdToExl1;
        public static List<string> m_ConncetionResultIdTodataGridView;
        public static List<string> m_ConncetionResultIdTodataGridView1;
        public static List<string> m_ConncetionResultcode;
        public static List<string> m_Conncetion1Resultcode;
        public static List<string> m_ProductionResultId;
        public static List<string> m_ProductionResultcode;
        public static List<string> m_ProductionResultIdToMoveFile;
        public static List<string> m_ProductionResultcodeToMoveFile;
        public static List<string> ProductionDataIdToExl;
        public static List<float> ProductionDataResultScoreToExl;
        public static List<string> ProductionDataResultCodeToExl;
        public static List<string> ConnectProductionDataResultIdToMoveFile;
        public static List<string> ConnectProductionDataResultCodeToMoveFile;
        public static List<string> ConnectProductionDataResultIdToMoveFile1;
        public static List<string> ConnectProductionDataResultCodeToMoveFile1;
        public static List<string> ConnectProductionDataResultIdToXmlExl;
        public static List<string> ConnectProductionDataResultCodeToXmlExl;
        public static List<string> ConnectProductionDataResultIdToXmlExl1;
        public static List<string> ConnectProductionDataResultCodeToXmlExl1;
        public static List<string> FixtureNumberToXml;

        public static List<string> DayCountProductionDataIdToExl = new List<string>();
        public static List<float> DayCountProductionDataResultScoreToExl = new List<float>();
        public static List<string> DayCountProductionDataResultCodeToExl = new List<string>();
        public static List<string> DayCountConnectProductionDataResultIdToXmlExl = new List<string>();
        public static List<string> DayCountConnectProductionDataResultCodeToXmlExl = new List<string>();
        public static List<string> DayCountConnectProductionDataResultIdToXmlExl1 = new List<string>();
        public static List<string> DayCountConnectProductionDataResultCodeToXmlExl1 = new List<string>();
        //现在的时间路径
        string m_sTimePath = null;
        string m_sDefect_file1 = null;
        //Cys服务器连接
        public static string ClientCys_IP = "192.168.250.100";
        public static int CilentCys_Port = 0;
        //CYs 接口类及参数
        SourceParamLst Cys_Soure = new SourceParamLst();
        SourceParamLst Cys_Soure1 = new SourceParamLst();
        ResultParam Cys_result = new ResultParam();
        ResultParam Cys_result1 = new ResultParam();
        List<double[]> Listbbox = new List<double[]>();
        List<string> listpath = new List<string>();
        List<List<double[]>> ListMask = new List<List<double[]>>();
        List<string> ConnnectResultCode = new List<string>();

        //服务器连接
        public static string Client_IP = "192.168.250.100";
        public static int Cilent_Port = 0;
        int m_nCanOpenDeviceNum;        // ch:设备使用数量 | en:Used Device SerialNumber
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
        //public static string ServerSaveFilePath = "F:/tc2/data/";
        public static string ServerSaveFilePath = "C:/tc2/data/";
        public static string SaveCheckPath1 = "F:/checkP1/";
        public static string SaveCheckPath2 = "F:/checkP2/";
        public static string SaveCheckPath3 = "F:/checkP3/";
        public static string SaveCheckPath4 = "F:/checkP4/";
        public static string SaveCheckPath5 = "F:/checkP5/";
        public static string SaveCheckPath6 = "F:/checkP6/";

        public static string SaveCheckPath1_1 = "F:/checkP1_A/";
        public static string SaveCheckPath2_1 = "F:/checkP2_A/";
        public static string SaveCheckPath3_1 = "F:/checkP3_A/";
        public static string SaveCheckPath4_1 = "F:/checkP4_A/";
        public static string SaveCheckPath5_1 = "F:/checkP5_A/";
        public static string SaveCheckPath6_1 = "F:/checkP6_A/";
        //public string ConnectionFilePath = "F:/Connection/data/00/";
        //public string ConnectionFilePath1 = "F:/Connection/data/01/";
        public string ConnectionFilePath = "C:/Connection/data/00/";
        public string ConnectionFilePath1 = "C:/Connection/data/01/";
        /// <summary>
        /// 缩略图保存地址
        /// </summary>
        public string m_sThumbnailServerSaveFilePath = "C:/data/SmallPic/";
        public string m_sThumbnailConnectionSaveFilePath = "C:/Connection/SmallPic/";
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


        public static List<IntPtr>[] Linprt = new List<IntPtr>[4];
        public static List<int>[] ListNumflag = new List<int>[4];
        public static int[] SampleIDFlag = new int[7];
        public static List<int> ListNumflag1 = new List<int>();
        public static List<int> ListNumflag2 = new List<int>();
        public static List<int> ListNumflag3 = new List<int>();
        public static List<int> ListNumflag4 = new List<int>();


        public MotionProcess()
        {
            for (int i = 0; i < m_nCameraSaveNum; i++)
            {
                Linprt[i] = new List<IntPtr>();
            }
            for (int i = 0; i < m_nCameraSaveNum; i++)
            {
                ListNumflag[i] = new List<int>();
            }
            for (int i = 0; i < m_nCameraSaveConnectionNum; i++)
            {
                if (!m_dictImageConnectionIndex.ContainsKey(i))
                {
                    m_dictImageConnectionIndex.Add(i, new List<int>());
                }
            }
            for (int i = 0; i < m_iPositionNum; i++)
            {
                m_arrPositionFilnames[i] = new List<string>();
                m_arrPositionFilnames1[i] = new List<string>();
                m_arrPositionFilnames2[i] = new List<string>();
                Pose_result[i] = new List<Data_NVT_Defect_Message>();
            }

            //keyenceInstance = KeyencePlcFactory.GetInstance();
            for (int i = 0; i < m_nCameraTotalNum; i++)
            {
                m_listMvCameras.Add(MvClass.GetInstance(i));
            }
            for (int i = 0; i < m_iPositionNum * 2; i++)
            {
                m_listQrCodeInfer[i] = new List<string>();
            }


            for (int i = 0; i < m_ihalconProcesEndNum; i++)
            {
                if (i != 5)
                {
                    halconOperatorList.Add(HalconOperator.GetInstance(i, 0));
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
            MainForm.Alarmevent += Alarm;
            MainForm.ResetAlarmevent += Reset;

        }
        private void MotionProcess_eventProcessImage6(HObject hImage, int motorNum)
        {
            if (!abort)
                event_OnImgDisp?.Invoke(hImage, 6, 0);
        }
        private void MotionProcess_eventProcessImage8(HObject hImage, int motorNum)
        {
            if (!abort)
                event_OnImgDisp?.Invoke(hImage, 8, 0);
        }
        private void MotionProcess_eventProcessImage4(HObject hImage, int motorNum)
        {
            if (!abort)
                event_OnImgDisp?.Invoke(hImage, 4, 0);
        }
        private void MotionProcess_eventProcessImage5(HObject hImage, int motorNum)
        {
            switch (motorNum)
            {
                case 0:
                    if (!abort)
                        event_OnImgDisp?.Invoke(hImage, 5, 1);
                    break;
                case 1:
                    if (!abort)
                        event_OnImgDisp?.Invoke(hImage, 5, 2);
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
                            Camera_state = false;
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
                    m_bCheckConnected = hq.CheckConnected(Client_IP, Cilent_Port);
                    if (m_bCheckConnected)
                    {
                        LogHelper.LogInfo("已连接服务器");
                    }
                    else
                    {
                        LogHelper.LogError("未连接服务器");
                    }

                }

                //m_CheckConnectedCys = hq.TcpClientCheck(ClientCys_IP,CilentCys_Port);
                //if (m_CheckConnectedCys)
                //{
                //    LogHelper.LogInfo("已连接Cys服务器");

                //}
                //else
                //{
                //    m_CheckConnectedCys = hq.TcpClientCheck(ClientCys_IP, CilentCys_Port);
                //    if (m_CheckConnectedCys)
                //    {
                //        LogHelper.LogInfo("已连接Cys服务器");
                //    }
                //    else
                //    {
                //        LogHelper.LogError("未连接Cys服务器");
                //    }

                //}

                for (int i = 0; i < m_iPositionNum; i++)
                {
                    m_listTotalSample[i] = new List<int>();
                }
                /*   if (mvcamera_connect)
                   {*/
                is_init = true;
                //表格输出
                string str = DateTime.Now.ToString("yyyyMMddHHmmss");
                G_w.AddWorksheet("缺陷");
                G_w.SaveAs("缺陷报表" + str + ".xlsx");
                G_w = new XLWorkbook("缺陷报表" + str + ".xlsx");
                iws = G_w.Worksheet(1);
                iws.Cell(1, 1).Value = "序列号";
                iws.Cell(1, 2).Value = "推理机缺陷结果";
                iws.Cell(1, 3).Value = "Score";
                iws.Cell(1, 4).Value = "治具号";


                string str1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                G_wConnect.AddWorksheet("连接器缺陷");
                G_wConnect.SaveAs("连接器缺陷报表" + str + ".xlsx");
                G_wConnect = new XLWorkbook("连接器缺陷报表" + str + ".xlsx");
                iwsConnect = G_wConnect.Worksheet(1);
                iwsConnect.Cell(1, 1).Value = "连接器序列号";
                iwsConnect.Cell(1, 2).Value = "连接器缺陷结果";

                string str2 = DateTime.Now.ToString("yyyyMMddHHmmss");
                G_wConnect1.AddWorksheet("连接器钢片缺陷");
                G_wConnect1.SaveAs("连接器钢片缺陷报表" + str + ".xlsx");
                G_wConnect1 = new XLWorkbook("连接器钢片缺陷报表" + str + ".xlsx");
                iwsConnect1 = G_wConnect1.Worksheet(1);
                iwsConnect1.Cell(1, 1).Value = "连接器钢片序列号";
                iwsConnect1.Cell(1, 2).Value = "连接器钢片缺陷结果";




                //mes上传字段
                MesHelper.StrModelNo = "";//机种
                MesHelper.StrLineNo = "";//线体
                MesHelper.StrFlowType = "";//工序方案
                MesHelper.StrProcName = "";//工序名
                MesHelper.StrTestUser = "";//操作用户
                MesHelper.StrWo = "";//产品工单
                MesHelper.StrPN = "";//产品料号
                MesHelper.StrjigNo = "";//治具编号
                MesHelper.StrLotNo = "";//工单批次
                MesHelper.StrDeviceNo = "";//设备编号
                MesHelper.StrBatchNo = "";//批次号
                MesHelper.StrChannelNo = "";//通道号
                                            // MesHelper.StrErrorMsg = "";//错误描述



                Start();
                //}
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Init"+ex.Message);
            }
        }
        struct stThreadParam
        {
            public int Index;
            public object LockObject;
        }
        private void Start()
        {
            motorInstance = new MotorsClass(0);

            // ConfigVars.configInfo.sampleId= sample_id_num;
            //保存图像线程
            for (int i = 0; i < m_nCameraTotalNum; i++)
            {
                if (i == 0 || i == 1 || i == 2 || i == 3)
                {
                    m_thSaveImage_thread[i] = new Thread(SaveMemoryToInferImage);
                    m_thSaveImage_thread[i].Name = "保存" + i.ToString() + "图像线程";
                    //m_thSaveImage_thread[i].IsBackground = true;
                    stThreadParam stParam = new stThreadParam()
                    {
                        Index = i,
                        LockObject = MvClass.m_BufFerSaveImageLock[i]
                    };
                    m_thSaveImage_thread[i].Start(stParam);
                    LogHelper.LogInfo("保存" + i.ToString() + "图像线程开启！");
                }
                //else if (i == 9)
                //{
                //    m_thSaveImage_thread[i] = new Thread(SaveFPCImage);
                //    m_thSaveImage_thread[i].Name = "保存" + i.ToString() + "连接器图像线程";
                //    m_thSaveImage_thread[i].Start(i);
                //    LogHelper.LogInfo("保存" + i.ToString() + "图像线程开启！");
                //}
                else if (i == 5 || i == 7)
                {
                    m_thSaveImage_thread[i] = new Thread(SaveConnectionImage);
                    m_thSaveImage_thread[i].Name = "保存" + i.ToString() + "连接器图像线程";
                    //m_thSaveImage_thread[i].IsBackground = true;
                    m_thSaveImage_thread[i].Start(i);
                    LogHelper.LogInfo("保存" + i.ToString() + "图像线程开启！");
                }

            }

            m_thSaveQrcode_thread = new Thread(SaveQrcode);
            m_thSaveQrcode_thread.IsBackground = true;
            m_thSaveQrcode_thread.Start();

            ////上料二维码读取
            //m_thSaveQrcode_thread1 = new Thread(SaveQrcode1);
            //m_thSaveQrcode_thread1.IsBackground = true;
            //m_thSaveQrcode_thread1.Start();
            ////连接器正反二维码读取
            ///
            Task.Run(() => { SaveQrcode2(); });
            //m_thSaveQrcode_thread2 = new Thread(SaveQrcode2);
            //m_thSaveQrcode_thread2.IsBackground = true;
            //m_thSaveQrcode_thread2.Start();

            /*
            //m_thSaveQrcode_thread3 = new Thread(SaveQrcode3);
            //m_thSaveQrcode_thread3.IsBackground = true;
            //m_thSaveQrcode_thread3.Start();
            */


            //for (int i = 0; i < 3; i++)
            //{
            Task.Run(() => { SaveSeparateToWholeNames(); });
            //m_thSeparateToWholeNames_thread = new Thread(SaveSeparateToWholeNames);
            //m_thSeparateToWholeNames_thread.Name = "保存" + 1.ToString() + "图像路径名字线程开启！";
            //m_thSeparateToWholeNames_thread.IsBackground = true;
            //m_thSeparateToWholeNames_thread.Start();
            LogHelper.LogInfo("保存" + 1.ToString() + "图像路径名字线程开启！！");
            //}


            //for (int i = 3; i < 5; i++)
            //{
            Task.Run(() => { SaveSeparateToWholeNames1(); });
            //m_thSeparateToWholeNames_thread1 = new Thread(SaveSeparateToWholeNames1);
            //m_thSeparateToWholeNames_thread1.Name = "保存" + 2.ToString() + "图像路径名字线程开启！";
            //m_thSeparateToWholeNames_thread1.IsBackground = true;
            //m_thSeparateToWholeNames_thread1.Start();
            LogHelper.LogInfo("保存" + 2.ToString() + "图像路径名字线程开启！！");
            //}

            //for (int i = 5; i < 7; i++)
            //{
            Task.Run(() => { SaveSeparateToWholeNames2(); });
            //m_thSeparateToWholeNames_thread2 = new Thread(SaveSeparateToWholeNames2);
            //m_thSeparateToWholeNames_thread2.Name = "保存" + 3.ToString() + "图像路径名字线程开启！";
            //m_thSeparateToWholeNames_thread2.IsBackground = true;
            //m_thSeparateToWholeNames_thread2.Start();
            LogHelper.LogInfo("保存" + 3.ToString() + "图像路径名字线程开启！！");
            //}

            Task.Run(() => { MoveFile(); });
            //m_MoveFile_thread = new Thread(MoveFile);
            //m_MoveFile_thread.Name = "移动图片工作线程";
            //m_MoveFile_thread.IsBackground = true;
            //m_MoveFile_thread.Start();
            LogHelper.LogInfo("移动图片工作线程开启！");

            Task.Run(() => { WriteCVS(); });
            LogHelper.LogInfo("每日设备点检工作线程开启！");

            // 发送推理机信息线程
            Task.Run(() => { SendMessage2ReasoningMachine(); });
            //m_thdSendReasoningMachine = new Thread(SendMessage2ReasoningMachine);
            //    m_thdSendReasoningMachine.Name = "发送推理机工作线程";
            //m_thdSendReasoningMachine.IsBackground = true;
            //    m_thdSendReasoningMachine.Start();
            LogHelper.LogInfo("发送推理机工作线程开启！");


            Task.Run(() => { SenTheResultToPlc(); });
            //m_thdSendReasoningResultToPlc = new Thread(SenTheResultToPlc);
            //m_thdSendReasoningResultToPlc.Name = "发送推理机结果线程";
            //m_thdSendReasoningResultToPlc.IsBackground = true;
            //m_thdSendReasoningResultToPlc.Start();
            LogHelper.LogInfo("发送推理机结果到PLC线程开启！");


            LogHelper.LogInfo("发送心跳与二维码读取线程开启！");

            //生成表格xml
            Task.Run(() => { CreateXMlToExl(); });
            LogHelper.LogInfo("生成表格xml线程开启！");
            //CysAi服务开启
            Task.Run(() => { SendToCysAiServer(); });

            Task.Run(() => { TheConnect1ImageSendToCysAiServer(); });
            LogHelper.LogInfo("发送给CysAi服务线程开启！");

            //Task.Run(()=> { CheckDisk(); });
            LogHelper.LogInfo("硬盘检测线程开启！");
            string MODEL_PATH2D = Application.StartupPath + "/" + "2D_model" + "/" + "data_code_model.dcm";
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



        }

        private void SaveQrcode()
        {
            for (int i = 0; i < 8; i++)
            {
                Qrcode[i] = new List<string>();
            }
            Qrcode[4].Add("123");
            //QrcodeTemple.Add("123");
            string code = "";
            string fixturecode = "";
            string FirstCode = "";
            string ConnnectPCode = "";
            string ConnnectNCode = "";
            // PerformanceCounter diskRt = new PerformanceCounter("PhysicalDisk", "% Disk Time", "2 F:");       
            //int nCamIndex = (int)ob;
            System.Diagnostics.Stopwatch stopwathch = new System.Diagnostics.Stopwatch();
            while (IsSaveQrcode)
            {
                if (m_bRunSaveQrcode)
                {
                    //if (!abort)
                    //{
                    //    if (!Loseimage)
                    //    {
                    //        MotorsClass.omronInstance.Write(plc_HeatBeat_status, (float)1);
                    //        if (!MotorsClass.omronInstance.Write(plc_HeatBeat_status, (float)1).IsSuccess)
                    //        {
                    //            MotorsClass.omronInstance.Write(plc_HeatBeat_status, (float)1);
                    //            if (!MotorsClass.omronInstance.Write(plc_HeatBeat_status, (float)1).IsSuccess)
                    //            {
                    //                LogHelper.LogError("plc连接心跳断开！");
                    //                System.Windows.MessageBox.Show("plc连接心跳断开！");
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MotorsClass.omronInstance.Write(plc_HeatBeat_status, (float)0);
                    //    }


                    //}

                    code = GetQrCode(0);//本体二维码读取
                    if (!AccCheck && !AccCheckA)
                    {
                        if (code.Count() > 5)
                        {
                            //if (!Qrcode[4].Contains(code))
                            //{

                            //    m_listSampleIDs.Add(code);
                            //}
                            if (!QrcodeTemple.Contains(code))
                            {
                                m_listSampleIDs.Add(code);
                                // m_ReCheckIdNumber.Add(code);
                                Qrcode1.Add(code);
                                Qrcode2.Add(code);
                                Qrcode3.Add(code);
                                Qrcode4.Add(code);
                                QrcodeTemple.Add(code);
                                MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ShortSide_Flag, (float)1);
                                if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ShortSide_Flag, (float)1).IsSuccess)
                                {
                                    LogHelper.LogInfo(code + "短边二维码读取并发送PLC成功1");
                                }
                                else
                                {
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ShortSide_Flag, (float)1);
                                    if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ShortSide_Flag, (float)1).IsSuccess)
                                    {
                                        LogHelper.LogInfo(code + "短边二维码读取并发送PLC成功2");
                                    }
                                    else
                                    {
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ShortSide_Flag, (float)1);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ShortSide_Flag, (float)1).IsSuccess)
                                        {
                                            LogHelper.LogInfo(code + "短边二维码读取并发送PLC成功3");
                                        }
                                        else
                                        {
                                            LogHelper.LogInfo(code + "短边二维码读取并发送PLC失败");
                                        }
                                    }

                                }
                            }
                            if (QrcodeTemple.Count > 3)
                            {
                                QrcodeTemple.RemoveAt(0);
                            }
                            //if (QrcodeTemple.Count > 10)
                            //{
                            //    QrcodeTemple.RemoveRange(0, 2);
                            //}
                            //for (int i = 0; i < 5; i++)
                            //{
                            //    if (Qrcode[0].Count == 0 && (!Qrcode[4].Contains(code)) == true)
                            //    {

                            //        Qrcode[i].Add(code);

                            //    }
                            //    else
                            //    {
                            //        if (!Qrcode[4].Contains(code))
                            //        {
                            //            Qrcode[i].Add(code);

                            //        }
                            //    }

                            //}

                            //if (Qrcode[4].Count > 30)
                            //{
                            //    Qrcode[4].RemoveRange(0, 2);
                            //}
                        }
                        else
                        {
                            code = GetQrCode(0);
                            if (code.Count() > 5)
                            {

                                if (!QrcodeTemple.Contains(code))
                                {
                                    m_listSampleIDs.Add(code);
                                    // m_ReCheckIdNumber.Add(code);
                                    Qrcode1.Add(code);
                                    Qrcode2.Add(code);
                                    Qrcode3.Add(code);
                                    Qrcode4.Add(code);
                                    QrcodeTemple.Add(code);
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ShortSide_Flag, (float)1);
                                    if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ShortSide_Flag, (float)1).IsSuccess)
                                    {
                                        LogHelper.LogInfo(code + "短边二维码读取并发送PLC成功1");
                                    }
                                    else
                                    {
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ShortSide_Flag, (float)1);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ShortSide_Flag, (float)1).IsSuccess)
                                        {
                                            LogHelper.LogInfo(code + "短边二维码读取并发送PLC成功2");
                                        }
                                        else
                                        {
                                            MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ShortSide_Flag, (float)1);
                                            if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ShortSide_Flag, (float)1).IsSuccess)
                                            {
                                                LogHelper.LogInfo(code + "短边二维码读取并发送PLC成功3");
                                            }
                                            else
                                            {
                                                LogHelper.LogInfo(code + "短边二维码读取并发送PLC失败");
                                            }
                                        }

                                    }
                                }
                                //if (QrcodeTemple.Count > 10)
                                //{
                                //    QrcodeTemple.RemoveRange(0, 2);
                                //}
                                if (QrcodeTemple.Count > 3)
                                {
                                    QrcodeTemple.RemoveAt(0);
                                }

                            }
                        }

                    }
                    fixturecode = GetQrCode(1);
                    if (!AccCheck && !AccCheckA)
                    {
                        if (fixturecode.Count() > 1)
                        {
                            if (!FixtureNumberTempel.Contains(fixturecode))
                            {
                                FixtureNumber.Add(fixturecode);
                                FixtureNumberToXml.Add(fixturecode);
                                FixtureNumberToMes.Add(fixturecode);
                                MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_Fixture_Flag, (float)1);
                                if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_Fixture_Flag, (float)1).IsSuccess)
                                {
                                    LogHelper.LogInfo(fixturecode + "治具二维码读取并发送PLC成功1");
                                }
                                else
                                {
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_Fixture_Flag, (float)1);
                                    if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_Fixture_Flag, (float)1).IsSuccess)
                                    {
                                        LogHelper.LogInfo(fixturecode + "治具二维码读取并发送PLC成功2");
                                    }
                                    else
                                    {
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_Fixture_Flag, (float)1);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_Fixture_Flag, (float)1).IsSuccess)
                                        {
                                            LogHelper.LogInfo(fixturecode + "治具二维码读取并发送PLC成功3");
                                        }
                                        else
                                        {
                                            LogHelper.LogInfo(fixturecode + "治具二维码读取并发送PLC失败");
                                        }
                                    }

                                }
                            }
                            if (FixtureNumberTempel.Count > 3)
                            {
                                FixtureNumberTempel.RemoveAt(0);
                            }

                        }
                        else
                        {
                            fixturecode = GetQrCode(1);
                            if (fixturecode.Count() > 1)
                            {
                                if (!FixtureNumberTempel.Contains(fixturecode))
                                {
                                    FixtureNumber.Add(fixturecode);
                                    FixtureNumberToXml.Add(fixturecode);
                                    FixtureNumberToMes.Add(fixturecode);
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_Fixture_Flag, (float)1);
                                    if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_Fixture_Flag, (float)1).IsSuccess)
                                    {
                                        LogHelper.LogInfo(fixturecode + "治具二维码读取并发送PLC成功1");
                                    }
                                    else
                                    {
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_Fixture_Flag, (float)1);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_Fixture_Flag, (float)1).IsSuccess)
                                        {
                                            LogHelper.LogInfo(fixturecode + "治具二维码读取并发送PLC成功2");
                                        }
                                        else
                                        {
                                            MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_Fixture_Flag, (float)1);
                                            if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_Fixture_Flag, (float)1).IsSuccess)
                                            {
                                                LogHelper.LogInfo(fixturecode + "治具二维码读取并发送PLC成功3");
                                            }
                                            else
                                            {
                                                LogHelper.LogInfo(fixturecode + "治具二维码读取并发送PLC失败");
                                            }
                                        }

                                    }
                                }
                                if (FixtureNumberTempel.Count > 3)
                                {
                                    FixtureNumberTempel.RemoveAt(0);
                                }

                            }
                        }

                    }
                }

                Thread.Sleep(10);

            }

        }
        //上料二维码读取（iPhone）
        private void SaveQrcode1()
        {
            QrcodeFisrtRead = new List<string>();//上料二维码读取（iPhone）
            QrcodeFisrtReadTemple = new List<string>();
            QrcodeFisrtReadTemple.Add("111");
            //QrcodeConnectPReadTemple.Add("1345");
            //QrcodeConnectNReadTemple.Add("1245");
            string FirstCode = "";
            string ConnnectPCode = "";
            string ConnnectNCode = "";
            //int nCamIndex = (int)ob;
            while (IsSaveQrcode4)
            {
                FirstCode = GetQrCode09();//上料二维码读取（iPhone）               
                if (FirstCode.Count() > 5)
                {

                    if (!QrcodeFisrtReadTemple.Contains(FirstCode) && FirstCode.Substring(0, 5) != "Sweep")
                    {
                        QrcodeFisrtRead.Add(FirstCode);
                        QrcodeFisrtReadTemple.Add(FirstCode);
                    }

                    if (QrcodeFisrtReadTemple.Count > 4)
                    {
                        QrcodeFisrtReadTemple.RemoveRange(0, 2);
                    }

                }


                Thread.Sleep(10);

            }

        }

        object m_lockObj = new object();
        private void SaveQrcode2()
        {

            //QrcodeConnectNRead = new List<string>();//连接器反面二维码读取
            //QrcodeConnectNReadTemple = new List<string>();
            //QrcodeConnectPRead = new List<string>();//连接器正面二维码读取
            //QrcodeConnectPReadTemple = new List<string>();
            //QrcodeConnectNReadTemple.Add("1345");
            //QrcodeConnectPReadTemple.Add("1345");
            string FirstCode = "";
            string ConnnectPCode = "";
            string ConnnectNCode = "";

            while (IsSaveQrcode2)
            {
                if (m_bRunSaveQrcode1)
                {
                    #region iWatch连接器钢片

                    ConnnectNCode = GetQrCode();//连接器反面05

                    if (ConnnectNCode.Count() > 3)
                    {
                        // LogHelper.LogInfo("反面二维码不为空");
                        // System.Diagnostics.Stopwatch stopwathch2 = new System.Diagnostics.Stopwatch();
                        // stopwathch2.Start();
                        if (!QrcodeConnectNReadTemple.Contains(ConnnectNCode))
                        {
                            QrcodeConnectNRead.Add(ConnnectNCode);
                            m_ConncetionResultIdToExl1.Add(ConnnectNCode);
                            m_ConncetionResultIdTodataGridView1.Add(ConnnectNCode);
                            ConnectProductionDataResultIdToXmlExl1.Add(ConnnectNCode);
                            ConnectProductionDataResultIdToMoveFile1.Add(ConnnectNCode);
                            // LogHelper.LogInfo("反面二维码加入列表成功");
                            m_Conncetion1ResultId.Add(ConnnectNCode);
                            QrcodeConnectNReadTemple.Add(ConnnectNCode);
                            // stopwathch2.Stop();
                            // TimeSpan timeSpan = stopwathch2.Elapsed;
                            // double mimilliseconds = timeSpan.TotalMilliseconds;
                            // LogHelper.LogInfo("读码成功时间" + ConnnectNCode + mimilliseconds);
                            Thread.Sleep(10);
                            MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorNegativeGet, (float)3);
                            if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorNegativeGet, (float)3).IsSuccess)
                            {
                                // LogHelper.LogInfo(code2 + "连接器1二维码读取并发送PLC成功1");
                            }
                            else
                            {
                                MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorNegativeGet, (float)3);
                                if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorNegativeGet, (float)3).IsSuccess)
                                {
                                    //  LogHelper.LogInfo(code2 + "连接器1二维码读取并发送PLC成功2");
                                }
                                else
                                {
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorNegativeGet, (float)3);
                                    if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorNegativeGet, (float)3).IsSuccess)
                                    {
                                        // LogHelper.LogInfo(code2 + "连接器1二维码读取并发送PLC成功3");
                                    }
                                }
                            }
                        }
                        else
                        {
                            LogHelper.LogInfo("反面二维码加入列表重复，失败");
                        }
                        if (QrcodeConnectNReadTemple.Count > 1)
                        {
                            QrcodeConnectNReadTemple.RemoveAt(0);
                        }
                        //if (QrcodeConnectNReadTemple.Count > 4)
                        //{
                        //    QrcodeConnectNReadTemple.RemoveRange(0, 2);
                        //}

                    }
                    else
                    {
                        ConnnectNCode = GetQrCode();//连接器反面05

                        if (ConnnectNCode.Count() > 5)
                        {
                            // LogHelper.LogInfo("反面二维码不为空");
                            System.Diagnostics.Stopwatch stopwathch2 = new System.Diagnostics.Stopwatch();
                            stopwathch2.Start();
                            if (!QrcodeConnectNReadTemple.Contains(ConnnectNCode))
                            {
                                QrcodeConnectNRead.Add(ConnnectNCode);
                                // LogHelper.LogInfo("反面二维码加入列表成功");
                                m_ConncetionResultIdToExl1.Add(ConnnectNCode);
                                m_ConncetionResultIdTodataGridView1.Add(ConnnectNCode);
                                QrcodeConnectNReadTemple.Add(ConnnectNCode);
                                ConnectProductionDataResultIdToXmlExl1.Add(ConnnectNCode);
                                ConnectProductionDataResultIdToMoveFile1.Add(ConnnectNCode);
                                m_Conncetion1ResultId.Add(ConnnectNCode);
                                Thread.Sleep(10);
                                MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorNegativeGet, (float)3);
                                if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorNegativeGet, (float)3).IsSuccess)
                                {
                                    // LogHelper.LogInfo(code2 + "连接器1二维码读取并发送PLC成功1");
                                }
                                else
                                {
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorNegativeGet, (float)3);
                                    if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorNegativeGet, (float)3).IsSuccess)
                                    {
                                        //  LogHelper.LogInfo(code2 + "连接器1二维码读取并发送PLC成功2");
                                    }
                                    else
                                    {
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorNegativeGet, (float)3);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorNegativeGet, (float)3).IsSuccess)
                                        {
                                            // LogHelper.LogInfo(code2 + "连接器1二维码读取并发送PLC成功3");
                                        }
                                    }
                                }
                                stopwathch2.Stop();
                                TimeSpan timeSpan = stopwathch2.Elapsed;
                                double mimilliseconds = timeSpan.TotalMilliseconds;
                                // LogHelper.LogInfo("读码成功时间" + ConnnectNCode + mimilliseconds);
                            }
                            //}
                            if (QrcodeConnectNReadTemple.Count > 3)
                            {
                                QrcodeConnectNReadTemple.RemoveAt(0);
                            }
                            //if (QrcodeConnectNReadTemple.Count > 4)
                            //{
                            //    QrcodeConnectNReadTemple.RemoveRange(0, 2);
                            //}

                        }
                    }


                    #endregion

                    ConnnectPCode = GetQrCodetest();//phone连接器钢片，iWatch连接器反面
                    if (ConnnectPCode.Count() > 5)
                    {

                        if (!QrcodeConnectPReadTemple.Contains(ConnnectPCode))
                        {
                            QrcodeConnectPRead.Add(ConnnectPCode);
                            //LogHelper.LogInfo("正面二维码加入列表成功");
                            QrcodeConnectPReadTemple.Add(ConnnectPCode);
                            m_ConncetionResultIdToExl.Add(ConnnectPCode);
                            m_ConncetionResultIdTodataGridView.Add(ConnnectPCode);
                            ConnectProductionDataResultIdToXmlExl.Add(ConnnectPCode);
                            ConnectProductionDataResultIdToMoveFile.Add(ConnnectPCode);
                            m_ConncetionResultId.Add(ConnnectPCode);
                            Thread.Sleep(10);
                            MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet, (float)3);
                            if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet, (float)3).IsSuccess)
                            {
                                LogHelper.LogInfo(ConnnectPCode + "连接器二维码读取并发送PLC成功1");
                            }
                            else
                            {
                                MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet, (float)3);
                                if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet, (float)3).IsSuccess)
                                {
                                    LogHelper.LogInfo(ConnnectPCode + "连接器二维码读取并发送PLC成功2");
                                }
                                else
                                {
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet, (float)3);
                                    if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet, (float)3).IsSuccess)
                                    {
                                        LogHelper.LogInfo(ConnnectPCode + "连接器二维码读取并发送PLC成功3");
                                    }
                                }
                            }
                        }
                        if (QrcodeConnectPReadTemple.Count > 3)
                        {
                            QrcodeConnectPReadTemple.RemoveAt(0);
                        }
                        //if (QrcodeConnectPReadTemple.Count > 4)
                        //{
                        //    QrcodeConnectPReadTemple.RemoveRange(0, 2);
                        //}
                    }
                    else
                    {
                        // ConnnectPCode = GetQrCode07();//连接器正面
                        ConnnectPCode = GetQrCodetest();//连接器正面
                        if (ConnnectPCode.Count() > 5)
                        {

                            if (!QrcodeConnectPReadTemple.Contains(ConnnectPCode))
                            {
                                QrcodeConnectPRead.Add(ConnnectPCode);
                                //  LogHelper.LogInfo("正面二维码加入列表成功");
                                QrcodeConnectPReadTemple.Add(ConnnectPCode);
                                m_ConncetionResultIdToExl.Add(ConnnectPCode);
                                m_ConncetionResultIdTodataGridView.Add(ConnnectPCode);
                                ConnectProductionDataResultIdToXmlExl.Add(ConnnectPCode);
                                ConnectProductionDataResultIdToMoveFile.Add(ConnnectPCode);
                                m_ConncetionResultId.Add(ConnnectPCode);
                                Thread.Sleep(10);
                                MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet, (float)3);
                                if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet, (float)3).IsSuccess)
                                {
                                    LogHelper.LogInfo(ConnnectPCode + "连接器二维码读取并发送PLC成功1");
                                }
                                else
                                {
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet, (float)3);
                                    if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet, (float)3).IsSuccess)
                                    {
                                        LogHelper.LogInfo(ConnnectPCode + "连接器二维码读取并发送PLC成功2");
                                    }
                                    else
                                    {
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet, (float)3);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet, (float)3).IsSuccess)
                                        {
                                            LogHelper.LogInfo(ConnnectPCode + "连接器二维码读取并发送PLC成功3");
                                        }
                                    }
                                }
                            }
                            if (QrcodeConnectPReadTemple.Count > 3)
                            {
                                QrcodeConnectPReadTemple.RemoveAt(0);
                            }
                            //if (QrcodeConnectPReadTemple.Count > 4)
                            //{
                            //    QrcodeConnectPReadTemple.RemoveRange(0, 2);
                            //}
                        }
                    }
                    if (m_ReCheckIdNumber.Count > 50000)
                    {
                        m_ReCheckIdNumber.RemoveRange(0, 10000);
                    }
                    GC.Collect();
                }

                Thread.Sleep(5);

            }
            //}

        }

        private void SaveQrcode3()
        {
            QrcodeConnectPRead = new List<string>();//连接器正面二维码读取
            QrcodeConnectPReadTemple = new List<string>();

            QrcodeConnectPReadTemple.Add("1345");

            string FirstCode = "";
            string ConnnectPCode = "";
            string ConnnectNCode = "";


            while (IsSaveQrcode3)
            {
                ConnnectPCode = GetQrCodetest();//连接器正面
                if (ConnnectPCode.Count() > 5)
                {

                    if (!QrcodeConnectPReadTemple.Contains(ConnnectPCode))
                    {
                        QrcodeConnectPRead.Add(ConnnectPCode);
                        QrcodeConnectPReadTemple.Add(ConnnectPCode);
                    }

                    if (QrcodeConnectPReadTemple.Count > 4)
                    {
                        QrcodeConnectPReadTemple.RemoveRange(0, 2);
                    }
                }
                else
                {
                    // ConnnectPCode = GetQrCode07();//连接器正面
                    ConnnectPCode = GetQrCodetest();//连接器正面
                    if (ConnnectPCode.Count() > 5)
                    {

                        if (!QrcodeConnectPReadTemple.Contains(ConnnectPCode))
                        {
                            QrcodeConnectPRead.Add(ConnnectPCode);
                            QrcodeConnectPReadTemple.Add(ConnnectPCode);
                        }

                        if (QrcodeConnectPReadTemple.Count > 4)
                        {
                            QrcodeConnectPReadTemple.RemoveRange(0, 2);
                        }
                    }
                }


                GC.Collect();

                Thread.Sleep(2);

            }

        }

        public void Exit()
        {
            if (omronInstance1 != null)
            {
                omronInstance1.ConnectClose();
                // omronInstance1.Dispose();

            }
            if (omronInstance2 != null)
            {
                omronInstance2.ConnectClose();
                // omronInstance2.Dispose();
            }
            if (motorInstance != null)
            {
                MotorsClass.omronInstance.ConnectClose();
                if (!MotorsClass.omronInstance.ConnectClose().IsSuccess)
                {
                    MotorsClass.omronInstance.ConnectClose();
                }

            }

            //退线程
            m_bWhileStatus_SaveInferImage = true;
            m_bFlagHeartBeat = false;
            m_bWhileStatus_SaveConnectionImage = true;
            m_bWhileStatus_SaveFPCImage = true;
            m_bWhileEnd = true;
            m_bWhileIsSendReasoningMachine = true;
            m_bMoveFile = false;
            m_bWhileSeparateToWholeNames = false;
            m_bWhileSeparateToWholeNames1 = false;
            m_bWhileSeparateToWholeNames2 = false;
            m_bWhileIsSendReasoningResultToPlc = false;
            m_bWhileCsvWrite = false;
            IsSaveQrcode = false;
            IsSaveQrcode1 = false;
            IsSaveQrcode2 = false;
            IsSaveQrcode3 = false;
            IsSaveQrcode4 = false;
            //MotorsClass.omronInstance.Dispose();
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
        List<string> listtest = new List<string>() { "123" };
        //连接器反面读码  05
        private string GetQrCode()
        {


            string code2 = "";
            string code02 = "";
            //System.Diagnostics.Stopwatch stopwathch = new System.Diagnostics.Stopwatch();
            //stopwathch.Start();
            // OperateResult<string> word2 = MotorsClass.omronInstance.ReadString(MotorsClass.plc_Qrcode_motor_ConnectorNegative, 15);

            //stopwathch.Stop();
            //TimeSpan timeSpan = stopwathch.Elapsed;
            //double mimilliseconds = timeSpan.TotalMilliseconds;
            //LogHelper.LogInfo("PLC读取时间" + mimilliseconds);

            //Stopwatch sth = new Stopwatch();
            //sth.Start();
            //lock (MotionProcess.HslCommunicationlock)
            //{
            OperateResult<float> word4 = MotorsClass.omronInstance.ReadFloat(MotorsClass.plc_Qrcode_motor_ConnectorNegativeGet);
            if (word4.Content == 1)
            {
                //LogHelper.LogInfo("反面二维码收到信号");
                OperateResult<string> word2 = MotorsClass.omronInstance.ReadString(MotorsClass.plc_Qrcode_motor_ConnectorNegative, (ushort)TheLengthQrcode);

                if (word2.IsSuccess)
                {
                    if (word2.Content.First() != '0')
                    {
                        if (word2.Content.First() != '\0')
                        {
                            if (word2.Content[1] != '\0')
                            {
                                // listtest.Add("123");
                                if (!listtest.Contains(word2.Content))
                                {

                                    code02 = word2.Content.Replace("\0", "").Trim();

                                    char[] ccode3 = code02.ToArray();
                                    char middle;
                                    code2 = string.Empty;
                                    for (int i = 0; i < code02.Count() / 2; i++)
                                    {
                                        middle = ccode3[2 * i];
                                        ccode3[2 * i] = ccode3[2 * i + 1];
                                        ccode3[2 * i + 1] = middle;
                                    }
                                    for (int i = 0; i < code02.Count(); i++)
                                    {
                                        code2 += ccode3[i];
                                    }
                                    listtest.Add(word2.Content);
                                    if (listtest.Count > 4)
                                    {
                                        listtest.RemoveRange(0, 2);
                                    }
                                    // LogHelper.LogInfo(listtest.Count.ToString());

                                    //sth.Stop();
                                    //TimeSpan timeSpan1 = stopwathch.Elapsed;
                                    //double mimilliseconds1 = timeSpan.TotalMilliseconds;
                                    //LogHelper.LogInfo("高低位转换时间" + mimilliseconds1);
                                    //MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorNegativeGet, (float)3);
                                    //if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorNegativeGet, (float)3).IsSuccess)
                                    //{
                                    //   // LogHelper.LogInfo(code2 + "连接器1二维码读取并发送PLC成功1");
                                    //}
                                    //else
                                    //{
                                    //    MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorNegativeGet, (float)3);
                                    //    if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorNegativeGet, (float)3).IsSuccess)
                                    //    {
                                    //      //  LogHelper.LogInfo(code2 + "连接器1二维码读取并发送PLC成功2");
                                    //    }
                                    //    else
                                    //    {
                                    //        MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorNegativeGet, (float)3);
                                    //        if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorNegativeGet, (float)3).IsSuccess)
                                    //        {
                                    //           // LogHelper.LogInfo(code2 + "连接器1二维码读取并发送PLC成功3");
                                    //        }
                                    //    }
                                    //}

                                }
                            }


                        }
                        else
                        {
                            LogHelper.LogInfo("反面二维码格式不对" + word2.Content);
                            code02 = string.Empty;

                        }
                    }


                }
                else
                {

                    LogHelper.LogInfo("反面二维码读取失败...");
                    System.Windows.MessageBox.Show("Plc二维码读取失败反面");
                }
            }
            //}
            GC.Collect();
            //  LogHelper.LogInfo("返回反面二维码" + code2);
            return code2;
        }
        //连接器正面读二维码 07
        List<string> listtest1 = new List<string>() { "123" };
        private string GetQrCodetest()
        {


            string code2 = "";
            string code02 = "";
            //System.Diagnostics.Stopwatch stopwathch = new System.Diagnostics.Stopwatch();
            //stopwathch.Start();


            //stopwathch.Stop();
            //TimeSpan timeSpan = stopwathch.Elapsed;
            //double mimilliseconds = timeSpan.TotalMilliseconds;
            //LogHelper.LogInfo("PLC读取时间" + mimilliseconds);

            //Stopwatch sth = new Stopwatch();
            //sth.Start();
            // MotorsClass.omronInstance.ConnectServer();
            //lock (MotionProcess.HslCommunicationlock)
            //{


            OperateResult<float> word4 = MotorsClass.omronInstance.ReadFloat(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet);
            if (word4.Content == 1)
            {
                //  LogHelper.LogInfo("正面二维码收到信号");
                OperateResult<string> word2 = MotorsClass.omronInstance.ReadString(MotorsClass.plc_Qrcode_motor_ConnectorPositive, (ushort)TheLengthQrcode);
                if (word2.IsSuccess)
                {
                    if (word2.Content.First() != '0')
                    {
                        if (word2.Content.First() != '\0')
                        {
                            if (word2.Content[1] != '\0')
                            {
                                // listtest.Add("123");
                                if (!listtest1.Contains(word2.Content))
                                {
                                    code02 = word2.Content.Replace("\0", "").Trim();
                                    char[] ccode3 = code02.ToArray();
                                    char middle;
                                    code2 = string.Empty;
                                    for (int i = 0; i < code02.Count() / 2; i++)
                                    {
                                        middle = ccode3[2 * i];
                                        ccode3[2 * i] = ccode3[2 * i + 1];
                                        ccode3[2 * i + 1] = middle;
                                    }
                                    for (int i = 0; i < code02.Count(); i++)
                                    {
                                        code2 += ccode3[i];
                                    }
                                    listtest1.Add(word2.Content);
                                    if (listtest1.Count > 3)
                                    {
                                        listtest1.RemoveAt(0);
                                    }
                                    //if (listtest1.Count > 4)
                                    //{
                                    //    listtest1.RemoveRange(0, 2);
                                    //}
                                    // LogHelper.LogInfo(listtest.Count.ToString());

                                    //sth.Stop();
                                    //TimeSpan timeSpan1 = stopwathch.Elapsed;
                                    //double mimilliseconds1 = timeSpan.TotalMilliseconds;
                                    //LogHelper.LogInfo("高低位转换时间" + mimilliseconds1);

                                    // LogHelper.LogInfo("正面二维码读取成功" + code2);
                                    // LogHelper.LogInfo("正面二维码读取成功...");
                                    //MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet, (float)3);
                                    //if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet, (float)3).IsSuccess)
                                    //{
                                    //    LogHelper.LogInfo(code2 + "连接器二维码读取并发送PLC成功1");
                                    //}
                                    //else
                                    //{
                                    //    MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet, (float)3);
                                    //    if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet, (float)3).IsSuccess)
                                    //    {
                                    //        LogHelper.LogInfo(code2 + "连接器二维码读取并发送PLC成功2");
                                    //    }
                                    //    else
                                    //    {
                                    //        MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet, (float)3);
                                    //        if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet, (float)3).IsSuccess)
                                    //        {
                                    //            LogHelper.LogInfo(code2 + "连接器二维码读取并发送PLC成功3");
                                    //        }
                                    //    }
                                    //}
                                }
                            }


                        }
                        else
                        {
                            LogHelper.LogInfo("正面二维码格式不对" + word2.Content);
                            code02 = string.Empty;
                        }
                    }


                }
                else
                {

                    LogHelper.LogInfo("正面二维码读取失败...");
                    System.Windows.MessageBox.Show("Plc二维码读取失败正面");
                }
            }
            //}
            GC.Collect();

            return code2;
        }

        //上料读二维码
        List<string> listtest09 = new List<string>() { "123" };
        private string GetQrCode09()
        {
            string code3 = "";
            string code03 = "";

            // code3 = word3.Content.Replace("\0", "").Trim();
            OperateResult<float> word4 = MotorsClass.omronInstance.ReadFloat(MotorsClass.plc_Qrcode_motor_startreadGet);
            if (word4.Content == 1)
            {
                OperateResult<string> word3 = MotorsClass.omronInstance.ReadString(MotorsClass.plc_Qrcode_motor_startread, (ushort)TheLengthQrcode);
                if (word3.IsSuccess)
                {
                    if (!listtest09.Contains(word3.Content))
                    {
                        if (word3.Content.First() != '\0')
                        {
                            code03 = word3.Content.Replace("\0", "").Trim();
                            char[] ccode3 = code03.ToArray();
                            char middle;
                            code3 = string.Empty;
                            for (int i = 0; i < code03.Count() / 2; i++)
                            {
                                middle = ccode3[2 * i];
                                ccode3[2 * i] = ccode3[2 * i + 1];
                                ccode3[2 * i + 1] = middle;
                            }
                            for (int i = 0; i < code03.Count(); i++)
                            {
                                code3 += ccode3[i];
                            }
                            listtest09.Add(word3.Content);
                            if (listtest09.Count > 4)
                            {
                                listtest09.RemoveRange(0, 2);
                            }
                            MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_startreadGet, (float)3);
                        }

                    }
                    else
                    {
                        code3 = string.Empty;
                    }


                }
                else
                {
                    LogHelper.LogInfo("上料二维码读取失败...");
                    System.Windows.MessageBox.Show("Plc上料二维码读取错误");
                }
            }
            return code3;
        }
        public static List<string> listtest00 = new List<string>() { "123" };
        public static List<string> listtest01 = new List<string>() { "123" };
        public static List<string> listFixture = new List<string>() { "123" };
        List<string> listtestA = new List<string>() { "123" };
        List<string> listtestB = new List<string>() { "123" };
        //List<string> TestAcode = new List<string>();
        //List<string> TestBcode = new List<string>();
        private string GetResulteAQrCode()
        {
            string code3 = "";
            string code03 = "";


            // code3 = word3.Content.Replace("\0", "").Trim();
            lock (MotionProcess.HslCommunicationlock)
            {


                OperateResult<string> word3 = MotorsClass.omronInstance.ReadString(MotorsClass.plc_Qrcode_motor_checkA, (ushort)TheLengthQrcode);
                if (word3.IsSuccess)
                {
                    if (word3.Content.First() != '\0')
                    {
                        if (word3.Content[1] != '\0')
                        {
                            //if (!TestAcode.Contains(word3.Content))
                            //{
                            code03 = word3.Content.Replace("\0", "").Trim();
                            char[] ccode3 = code03.ToArray();
                            char middle;
                            code3 = string.Empty;
                            for (int i = 0; i < code03.Count() / 2; i++)
                            {
                                middle = ccode3[2 * i];
                                ccode3[2 * i] = ccode3[2 * i + 1];
                                ccode3[2 * i + 1] = middle;
                            }
                            for (int i = 0; i < code03.Count(); i++)
                            {
                                code3 += ccode3[i];
                            }
                            //TestAcode.Add(word3.Content);
                            //if (TestAcode.Count>4)
                            //{
                            //    TestAcode.RemoveRange(0,2);
                            //}
                            //}
                            //else
                            //{
                            //    code3 = string.Empty;
                            //}

                        }

                    }


                    else
                    {
                        code3 = string.Empty;
                    }


                }
                else
                {
                    LogHelper.LogInfo("下料二维码校验A读取失败...");
                    System.Windows.MessageBox.Show("Plc下料二维码校验A读取错误");
                }
            }

            return code3;
        }

        private string GetResulteBQrCode()
        {
            string code3 = "";
            string code03 = "";

            // code3 = word3.Content.Replace("\0", "").Trim();
            lock (MotionProcess.HslCommunicationlock)
            {

                OperateResult<string> word3 = MotorsClass.omronInstance.ReadString(MotorsClass.plc_Qrcode_motor_checkB, (ushort)TheLengthQrcode);
                if (word3.IsSuccess)
                {
                    if (word3.Content.First() != '\0')
                    {
                        if (word3.Content[1] != '\0')
                        {
                            //if (!TestBcode.Contains(word3.Content))
                            //{
                            code03 = word3.Content.Replace("\0", "").Trim();
                            char[] ccode3 = code03.ToArray();
                            char middle;
                            code3 = string.Empty;
                            for (int i = 0; i < code03.Count() / 2; i++)
                            {
                                middle = ccode3[2 * i];
                                ccode3[2 * i] = ccode3[2 * i + 1];
                                ccode3[2 * i + 1] = middle;
                            }
                            for (int i = 0; i < code03.Count(); i++)
                            {
                                code3 += ccode3[i];
                            }
                            //    TestBcode.Add(word3.Content);
                            //    if (TestBcode.Count > 4)
                            //    {
                            //        TestBcode.RemoveRange(0, 2);
                            //    }
                            //}
                            //else
                            //{
                            //    code3 = string.Empty;
                            //}

                        }


                    }


                    else
                    {
                        code3 = string.Empty;
                    }


                }
                else
                {
                    LogHelper.LogInfo("下料二维码校验B读取失败...");
                    System.Windows.MessageBox.Show("Plc下料二维码校验B读取错误");
                }
            }
            return code3;
        }
        bool FixtureIsRead = false;
        bool ShortNumberIsRead = false;
        private string GetQrCode(int nCamIndex)
        {
            //string Address
            //string QRCode = string.Empty;

            //OperateResult<string> word = MotorsClass.omronInstance.ReadString(MotorsClass.plc_Qrcode_motor_ShortSide, 15);
            // MotorsClass.omronInstance.ConnectServer();
            //return QRCode = word.Content;
            string code00 = "";
            string code01 = "";
            string code02 = "";
            string code03 = "";
            string code05 = "";
            string code = "";
            string code1 = "";
            string code2 = "";
            string code3 = "";
            string code5 = "";
            switch (nCamIndex)
            {

                case 0:
                    //本体二维码读取
                    //lock (MotionProcess.HslCommunicationlock)
                    //{

                    OperateResult<string> word = MotorsClass.omronInstance.ReadString(MotorsClass.plc_Qrcode_motor_ShortSide, (ushort)TheLengthQrcode);

                    if (word.IsSuccess)
                    {
                        if (word.Content.First() != '0')
                        {
                            if (!listtest00.Contains(word.Content))
                            {
                                if (word.Content.First() != '\0')
                                {
                                    if (word.Content[1] != '\0')
                                    {
                                        code00 = word.Content.Replace("\0", "").Trim();
                                        char[] ccode0 = code00.ToArray();
                                        char middle;
                                        code = string.Empty;
                                        for (int i = 0; i < code00.Count() / 2; i++)
                                        {
                                            middle = ccode0[2 * i];
                                            ccode0[2 * i] = ccode0[2 * i + 1];
                                            ccode0[2 * i + 1] = middle;
                                        }
                                        for (int i = 0; i < code00.Count(); i++)
                                        {
                                            code += ccode0[i];
                                        }
                                        listtest00.Add(word.Content);
                                        //if (listtest00.Count > 6)
                                        //{
                                        //    listtest00.RemoveRange(0, 2);
                                        //}
                                        if (listtest00.Count > 3)
                                        {
                                            listtest00.RemoveAt(0);
                                        }




                                    }


                                }
                            }
                        }

                    }
                    else
                    {
                        LogHelper.LogInfo("二维码读取失败本体...");
                        System.Windows.MessageBox.Show("Plc读取失败电池本体");
                    }
                    //}
                    // return code;
                    return code;
                    break;
                case 1:
                    //治具二维码读取
                    //lock (MotionProcess.HslCommunicationlock)
                    //{

                    OperateResult<string> word1 = MotorsClass.omronInstance.ReadString(MotorsClass.plc_motor_FixtureA, 5);

                    if (word1.IsSuccess)
                    {
                        if (word1.Content.First() != '0')
                        {
                            if (!listFixture.Contains(word1.Content))
                            {
                                if (word1.Content.First() != '\0')
                                {

                                    code01 = word1.Content.Replace("\0", "").Trim();
                                    code01 = code01.Substring(8, 2);
                                    listFixture.Add(word1.Content);
                                    //if (listtest00.Count > 6)
                                    //{
                                    //    listtest00.RemoveRange(0, 2);
                                    //}
                                    if (listFixture.Count > 1)
                                    {
                                        listFixture.RemoveAt(0);
                                    }
                                    FixtureIsRead = true;
                                    //if (ShortNumberIsRead)
                                    //{
                                    //    MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ShortSide_Flag, (float)1);
                                    //    if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ShortSide_Flag, (float)1).IsSuccess)
                                    //    {
                                    //        LogHelper.LogInfo(code01 + "治具二维码读取并发送PLC成功1");
                                    //    }
                                    //    else
                                    //    {
                                    //        MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ShortSide_Flag, (float)1);
                                    //        if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ShortSide_Flag, (float)1).IsSuccess)
                                    //        {
                                    //            LogHelper.LogInfo(code01 + "治具二维码读取并发送PLC成功2");
                                    //        }
                                    //        else
                                    //        {
                                    //            MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ShortSide_Flag, (float)1);
                                    //            if (MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ShortSide_Flag, (float)1).IsSuccess)
                                    //            {
                                    //                LogHelper.LogInfo(code01 + "治具二维码读取并发送PLC成功3");
                                    //            }
                                    //        }
                                    //    }
                                    //    ShortNumberIsRead = false;
                                    //}


                                }
                            }
                        }

                    }
                    else
                    {
                        LogHelper.LogInfo("治具二维码读取失败本体...");
                        System.Windows.MessageBox.Show("Plc读取失败电池本体");
                    }
                    //}
                    // return code;
                    return code01;
                    break;
                case 2:
                    //奇数清料
                    OperateResult<float> word2 = MotorsClass.omronInstance.ReadFloat(MotorsClass.plc_motor_ClearConnectInfo);

                    if (word2.IsSuccess)
                    {
                        if (word2.Content == 1)
                        {
                            m_Conncetion1Resultcode.Clear();
                            m_Conncetion1ResultId.Clear();
                            m_ConncetionResultcode.Clear();
                            m_ConncetionResultId.Clear();
                            MotorsClass.omronInstance.Write(MotorsClass.plc_motor_ClearConnectInfo, (float)0);
                            if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_ClearConnectInfo, (float)0).IsSuccess)
                            {
                                LogHelper.LogInfo("奇数清料完成！");
                            }

                        }

                    }
                    else
                    {
                        LogHelper.LogInfo("二维码读取失败...");
                        System.Windows.MessageBox.Show("Plc连接错误");
                    }

                    return code2 = word2.Content.ToString();
                    break;
                case 3:
                    //连接器反面二维码读取
                    OperateResult<string> word3 = MotorsClass.omronInstance.ReadString(MotorsClass.plc_Qrcode_motor_ConnectorNegative, (ushort)TheLengthQrcode);
                    // code3 = word3.Content.Replace("\0", "").Trim();
                    if (word3.IsSuccess)
                    {

                        if (word3.Content.First() != '\0')
                        {
                            code03 = word3.Content.Replace("\0", "").Trim();
                            char[] ccode3 = code03.ToArray();
                            char middle;
                            code3 = string.Empty;
                            for (int i = 0; i < code03.Count() / 2; i++)
                            {
                                middle = ccode3[2 * i];
                                ccode3[2 * i] = ccode3[2 * i + 1];
                                ccode3[2 * i + 1] = middle;
                            }
                            for (int i = 0; i < code03.Count(); i++)
                            {
                                code3 += ccode3[i];
                            }
                        }

                    }
                    else
                    {
                        LogHelper.LogInfo("二维码读取失败...");
                        System.Windows.MessageBox.Show("Plc连接错误");
                    }

                    return code3;
                    break;
                default:
                    OperateResult<float> word4 = MotorsClass.omronInstance.ReadFloat(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet);
                    if (word4.Content == 1)
                    {
                        OperateResult<string> word5 = MotorsClass.omronInstance.ReadString(MotorsClass.plc_Qrcode_motor_ConnectorPositive, (ushort)TheLengthQrcode);

                        if (word5.IsSuccess)
                        {


                            if (word5.Content.First() != '\0')
                            {
                                code05 = word5.Content.Replace("\0", "").Trim();
                                char[] ccode5 = code05.ToArray();
                                char middle;
                                code5 = string.Empty;
                                for (int i = 0; i < code05.Count() / 2; i++)
                                {
                                    middle = ccode5[2 * i];
                                    ccode5[2 * i] = ccode5[2 * i + 1];
                                    ccode5[2 * i + 1] = middle;
                                }
                                for (int i = 0; i < code05.Count(); i++)
                                {
                                    code5 += ccode5[i];
                                }
                            }


                        }

                        MotorsClass.omronInstance.Write(MotorsClass.plc_Qrcode_motor_ConnectorPositiveGet, (float)0);
                    }
                    return code5;
                    break;

            }

        }


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

        int tmp = 0;


        public void Reset()
        {
            m_bRunSaveImage = true;
            m_bRunSaveImage_Connection = true;
            m_bRunWhileIsSendReasoningMachine = true;
            m_bRunWhileSeparateToWholeNames = true;
            m_bRunWhileSeparateToWholeNames1 = true;
            m_bRunWhileSeparateToWholeNames2 = true;
            m_bRunSaveQrcode = true;
            m_bRunSaveQrcode1 = true;

        }
        public void Stop()
        {
            m_bRunSaveImage = false;
            m_bRunSaveImage_Connection = false;
            m_bRunSaveImage_FPC = false;
        }
        private void SaveMemoryToInferImage(object ob)
        {
            stThreadParam stParam = (stThreadParam)ob;

            int nCamIndex = stParam.Index;


            int[] m_nMemoryindex = new int[m_nCameraSaveNum];

            for (int i = 0; i < 4; i++)
            {
                Position_NumberLock[i] = new object();
            }


            int SamNum = 0;
            string Save_file_tmp = "";
            string Save_file_tmp1 = "";
            int QrcodeFlag = 0;

            while (!m_bWhileEnd)
            {
                //if (!abort)
                //{


                if (m_bRunSaveImage)
                {
                    bHasData[nCamIndex] = false;

                    if (Linprt[nCamIndex].Count > 0)
                    {
                        if (Linprt[nCamIndex].Count > 0)
                        {
                            lock (stParam.LockObject)
                            {
                                m_nMemoryindex[nCamIndex] = ListNumflag[nCamIndex].First();
                                ListNumflag[nCamIndex].RemoveAt(0);

                                bHasData[nCamIndex] = true;
                                if (bHasData[nCamIndex])
                                {
                                    switch (nCamIndex)
                                    {

                                        //侧面短边检测相机
                                        case 0:

                                            //偶数
                                            if (!AccCheck && !AccCheckA)
                                            {
                                                lock (Position_NumberLock[0])
                                                {

                                                    //m_nMemoryindex[0] = ListNumflag1.First();
                                                    //ListNumflag1.RemoveAt(0);
                                                    if (!IsOdd(m_nMemoryindex[0] / m_iOnePositionPic))
                                                    {


                                                        SampleIDFlag[0]++;
                                                        lock (m_SamNumLock)
                                                        {
                                                            SamNum = 0;
                                                        }


                                                        if (SampleIDFlag[0] == 6)
                                                        {
                                                            sample_id_num[0]++;
                                                            m_listTotalSample[0].Add(1);

                                                            SampleIDFlag[0] = 1;
                                                        }
                                                        m_niInfer.SampleID = sample_id_num[0].ToString("000000");
                                                    }
                                                    else//奇数
                                                    {
                                                        SampleIDFlag[1]++;

                                                        lock (m_SamNumLock)
                                                        {
                                                            SamNum = 1;
                                                        }

                                                        if (SampleIDFlag[1] == 6)
                                                        {
                                                            sample_id_num[1]++;
                                                            m_listTotalSample[1].Add(1);

                                                            SampleIDFlag[1] = 1;
                                                        }
                                                        m_niInfer.SampleID = sample_id_num[1].ToString("000000");
                                                    }
                                                    //if (Qrcode[0].Count == 0)
                                                    //{
                                                    //    m_QrCodeInfer = "test0000000000000000";
                                                    //}
                                                    if (Qrcode1.Count == 0)
                                                    {
                                                        //int i = 0;
                                                        QrcodeFlag++;
                                                        m_QrCodeInfer = "test0000000000000000";
                                                        if (QrcodeFlag == 1)
                                                        {
                                                            Qrcode2.Add("test0000000000000000");
                                                            Qrcode3.Add("test0000000000000000");
                                                            Qrcode4.Add("test0000000000000000");
                                                        }
                                                        if (QrcodeFlag == 5)
                                                        {
                                                            QrcodeFlag = 0;
                                                        }

                                                    }
                                                    else
                                                    {
                                                        if (m_nMemoryindex[0] < 9)
                                                        {
                                                            // m_QrCodeInfer = Qrcode[0].First();
                                                            m_QrCodeInfer = Qrcode1.First();
                                                        }
                                                        else
                                                        {
                                                            //m_QrCodeInfer = Qrcode[0].First();
                                                            //Qrcode[0].RemoveAt(0);
                                                            m_QrCodeInfer = Qrcode1.First();
                                                            Qrcode1.RemoveAt(0);

                                                        }
                                                    }
                                                    m_niInfer.Sample_Number = m_QrCodeInfer;
                                                    // m_QrCodeInfer = "test0000000000000000";
                                                    m_niInfer.Position_Number = ((m_nMemoryindex[0]) / 5 + 0 + 1).ToString("00");
                                                    m_niInfer.Cam_Num = 0.ToString("00");
                                                    m_niInfer.Light_Num = (m_nMemoryindex[0] % 5).ToString("0");
                                                    m_stSaveParamInfer[0].pData = Linprt[0].First();
                                                    m_niInfer.Time_Now = DateTime.Now.ToString("yyyyMMddHHmmssff");
                                                    Save_file_tmp = ServerSaveFilePath + m_Job_name + "_" + m_QrCodeInfer + "_S" + m_niInfer.SampleID + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_" + m_niInfer.Time_Now;
                                                    // Save_file_tmp = ServerSaveFilePath + m_Job_name + "_" + m_niInfer.Sample_Number + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_S" + m_niInfer.SampleID + "_" + m_niInfer.Time_Now;
                                                    string m_StsServers0 = "";
                                                    // m_StsServers = m_Job_name + "_" + m_niInfer.Sample_Number + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_S" + m_niInfer.SampleID + "_" + m_niInfer.Time_Now;
                                                    m_StsServers0 = m_Job_name + "_" + m_QrCodeInfer + "_S" + m_niInfer.SampleID + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_" + m_niInfer.Time_Now;
                                                    m_stSaveParamInfer[0].enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                                                    m_stSaveParamInfer[0].pImagePath = Save_file_tmp + ".jpg";
                                                    m_stSaveParamInfer[0].nQuality = 98;//存Jpeg时有效
                                                    int nRet1 = m_listMvCameras[0].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer[0]);
                                                    if (MyCamera.MV_OK != nRet1)
                                                    {
                                                        LogHelper.LogInfo("相机00J26664742" + Save_file_tmp + "false");
                                                        int nRet2 = m_listMvCameras[0].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer[0]);
                                                        if (MyCamera.MV_OK == nRet2)
                                                        {
                                                            LogHelper.LogInfo("相机00J26664742二次存图");
                                                        }
                                                    }
                                                    //else
                                                    //{
                                                    //    LogHelper.LogInfo("相机00J26664742" + Save_file_tmp);
                                                    //}
                                                    //LogHelper.LogInfo(string.Join(",", Filenames[nCamIndex]));
                                                    // Filenames.Add(m_StsServers + ".jpg");
                                                    Linprt[0].RemoveAt(0);
                                                    if (m_QrCodeInfer != "test0000000000000000")
                                                    {
                                                        if (!IsOdd(m_nMemoryindex[0] / m_iOnePositionPic))
                                                        {
                                                            m_arrPositionFilnames[0].Add(m_StsServers0 + ".jpg");
                                                        }
                                                        else
                                                        {
                                                            m_arrPositionFilnames[1].Add(m_StsServers0 + ".jpg");
                                                        }
                                                    }

                                                    GC.Collect();
                                                }
                                            }
                                            else
                                            {
                                                if (AccCheck)
                                                {
                                                    m_stSaveParamInfer[0].pData = Linprt[0].First();
                                                    m_niInfer.Time_Now = DateTime.Now.ToString("yyyyMMddHHmmssff");
                                                    Save_file_tmp1 = SaveCheckPath1 + "_c1" + m_niInfer.Time_Now;
                                                    m_stSaveParamInfer[0].enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                                                    m_stSaveParamInfer[0].pImagePath = Save_file_tmp1 + ".jpg";
                                                    m_stSaveParamInfer[0].nQuality = 98;//存Jpeg时有效
                                                    int nRet1 = m_listMvCameras[0].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer[0]);
                                                    Linprt[0].RemoveAt(0);
                                                }
                                                else
                                                {
                                                    m_stSaveParamInfer[0].pData = Linprt[0].First();
                                                    m_niInfer.Time_Now = DateTime.Now.ToString("yyyyMMddHHmmssff");
                                                    Save_file_tmp1 = SaveCheckPath1_1 + "_c1" + m_niInfer.Time_Now;
                                                    m_stSaveParamInfer[0].enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                                                    m_stSaveParamInfer[0].pImagePath = Save_file_tmp1 + ".jpg";
                                                    m_stSaveParamInfer[0].nQuality = 98;//存Jpeg时有效
                                                    int nRet1 = m_listMvCameras[0].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer[0]);
                                                    Linprt[0].RemoveAt(0);
                                                }

                                                // m_nMemoryindex[0] = 0;




                                            }

                                            break;
                                        //反面和mayler检测相机

                                        case 1:
                                            if (!AccCheck && !AccCheckA)
                                            {
                                                lock (Position_NumberLock[0])
                                                {
                                                    //m_nMemoryindex[1] = ListNumflag2.First();
                                                    //ListNumflag2.RemoveAt(0);
                                                    if (!IsOdd(m_nMemoryindex[1] / m_iOnePositionPic))
                                                    {


                                                        SampleIDFlag[2]++;
                                                        lock (m_SamNumLock)
                                                        {
                                                            SamNum = 2;
                                                        }


                                                        if (SampleIDFlag[2] == 6)
                                                        {
                                                            sample_id_num[2]++;
                                                            m_listTotalSample[2].Add(1);

                                                            SampleIDFlag[2] = 1;
                                                        }
                                                        m_niInfer.SampleID = sample_id_num[2].ToString("000000");
                                                    }
                                                    else//奇数
                                                    {
                                                        SampleIDFlag[3]++;

                                                        lock (m_SamNumLock)
                                                        {
                                                            SamNum = 3;
                                                        }

                                                        if (SampleIDFlag[3] == 6)
                                                        {
                                                            sample_id_num[3]++;
                                                            m_listTotalSample[3].Add(1);

                                                            SampleIDFlag[3] = 1;
                                                        }
                                                        m_niInfer.SampleID = sample_id_num[3].ToString("000000");
                                                    }
                                                    //if (Qrcode[1].Count == 0)
                                                    //{
                                                    //    m_QrCodeInfer = "test0000000000000000";
                                                    //}
                                                    if (Qrcode2.Count == 0)
                                                    {
                                                        m_QrCodeInfer = "test0000000000000000";
                                                    }
                                                    else
                                                    {
                                                        if (m_nMemoryindex[1] < 9)
                                                        {
                                                            //m_QrCodeInfer = Qrcode[1].First();
                                                            m_QrCodeInfer = Qrcode2.First();
                                                        }
                                                        else
                                                        {
                                                            //m_QrCodeInfer = Qrcode[1].First();
                                                            //Qrcode[1].RemoveAt(0);
                                                            m_QrCodeInfer = Qrcode2.First();
                                                            Qrcode2.RemoveAt(0);

                                                        }
                                                    }
                                                    m_niInfer.Sample_Number = m_QrCodeInfer;
                                                    // m_QrCodeInfer = "test0000000000000000";
                                                    m_niInfer.Position_Number = ((m_nMemoryindex[1]) / 5 + 1 + 2).ToString("00");
                                                    m_niInfer.Cam_Num = 1.ToString("00");
                                                    m_niInfer.Light_Num = (m_nMemoryindex[1] % 5).ToString("0");
                                                    m_stSaveParamInfer[1].pData = Linprt[1].First();
                                                    m_niInfer.Time_Now = DateTime.Now.ToString("yyyyMMddHHmmssff");
                                                    Save_file_tmp = ServerSaveFilePath + m_Job_name + "_" + m_QrCodeInfer + "_S" + m_niInfer.SampleID + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_" + m_niInfer.Time_Now;
                                                    // Save_file_tmp = ServerSaveFilePath + m_Job_name + "_" + m_niInfer.Sample_Number + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_S" + m_niInfer.SampleID + "_" + m_niInfer.Time_Now;
                                                    string m_StsServers1 = "";
                                                    // m_StsServers = m_Job_name + "_" + m_niInfer.Sample_Number + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_S" + m_niInfer.SampleID + "_" + m_niInfer.Time_Now;
                                                    m_StsServers1 = m_Job_name + "_" + m_QrCodeInfer + "_S" + m_niInfer.SampleID + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_" + m_niInfer.Time_Now;
                                                    m_stSaveParamInfer[1].enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                                                    m_stSaveParamInfer[1].pImagePath = Save_file_tmp + ".jpg";
                                                    //m_stSaveParamInfer.pImagePath = Save_file_tmp + ".jpg";
                                                    m_stSaveParamInfer[1].nQuality = 98;//存Jpeg时有效
                                                    int nRet1 = m_listMvCameras[1].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer[1]);
                                                    if (MyCamera.MV_OK != nRet1)
                                                    {
                                                        LogHelper.LogInfo("相机00J26664742" + Save_file_tmp + "false");
                                                        int nRet2 = m_listMvCameras[1].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer[1]);
                                                        if (MyCamera.MV_OK == nRet2)
                                                        {
                                                            LogHelper.LogInfo("相机00J26664742二次存图");
                                                        }
                                                    }
                                                    //else
                                                    //{
                                                    //    LogHelper.LogInfo("相机00J26664742" + Save_file_tmp);
                                                    //}
                                                    //LogHelper.LogInfo(string.Join(",", Filenames[nCamIndex]));
                                                    // Filenames.Add(m_StsServers + ".jpg");
                                                    Linprt[1].RemoveAt(0);
                                                    if (m_QrCodeInfer != "test0000000000000000")
                                                    {
                                                        if (!IsOdd(m_nMemoryindex[1] / m_iOnePositionPic))
                                                        {
                                                            m_arrPositionFilnames[2].Add(m_StsServers1 + ".jpg");
                                                        }
                                                        else
                                                        {
                                                            m_arrPositionFilnames1[0].Add(m_StsServers1 + ".jpg");
                                                        }
                                                    }

                                                    GC.Collect();
                                                }
                                            }
                                            else
                                            {
                                                if (AccCheck)
                                                {
                                                    m_stSaveParamInfer[1].pData = Linprt[1].First();
                                                    m_niInfer.Time_Now = DateTime.Now.ToString("yyyyMMddHHmmssff");
                                                    Save_file_tmp1 = SaveCheckPath2 + "_C2" + m_niInfer.Time_Now;
                                                    m_stSaveParamInfer[1].enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                                                    m_stSaveParamInfer[1].pImagePath = Save_file_tmp1 + ".jpg";
                                                    m_stSaveParamInfer[1].nQuality = 98;//存Jpeg时有效
                                                    int nRet1 = m_listMvCameras[1].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer[1]);
                                                    Linprt[1].RemoveAt(0);
                                                }
                                                else
                                                {
                                                    m_stSaveParamInfer[1].pData = Linprt[1].First();
                                                    m_niInfer.Time_Now = DateTime.Now.ToString("yyyyMMddHHmmssff");
                                                    Save_file_tmp1 = SaveCheckPath2_1 + "_C2" + m_niInfer.Time_Now;
                                                    m_stSaveParamInfer[1].enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                                                    m_stSaveParamInfer[1].pImagePath = Save_file_tmp1 + ".jpg";
                                                    m_stSaveParamInfer[1].nQuality = 98;//存Jpeg时有效
                                                    int nRet1 = m_listMvCameras[1].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer[1]);
                                                    Linprt[1].RemoveAt(0);
                                                }

                                            }

                                            break;
                                        //侧面长边检测相机
                                        case 2:
                                            //偶数
                                            if (!AccCheck && !AccCheckA)
                                            {
                                                lock (Position_NumberLock[0])
                                                {
                                                    //m_nMemoryindex[2] = ListNumflag3.First();
                                                    //ListNumflag3.RemoveAt(0);
                                                    if (!IsOdd(m_nMemoryindex[2] / m_iOnePositionPic))
                                                    {
                                                        SampleIDFlag[4]++;

                                                        lock (m_SamNumLock)
                                                        {
                                                            SamNum = 4;
                                                        }

                                                        if (SampleIDFlag[4] == 6)
                                                        {


                                                            sample_id_num[4]++;
                                                            m_listTotalSample[4].Add(1);
                                                            SampleIDFlag[4] = 1;
                                                        }
                                                        m_niInfer.SampleID = sample_id_num[4].ToString("000000");
                                                    }
                                                    else//奇数
                                                    {
                                                        SampleIDFlag[5]++;

                                                        lock (m_SamNumLock)
                                                        {
                                                            SamNum = 5;
                                                        }

                                                        if (SampleIDFlag[5] == 6)
                                                        {


                                                            sample_id_num[5]++;
                                                            m_listTotalSample[5].Add(1);
                                                            SampleIDFlag[5] = 1;
                                                        }
                                                        m_niInfer.SampleID = sample_id_num[5].ToString("000000");
                                                    }
                                                    //if (Qrcode[2].Count == 0)
                                                    //{
                                                    //    m_QrCodeInfer = "test0000000000000000";
                                                    //}
                                                    if (Qrcode3.Count == 0)
                                                    {
                                                        m_QrCodeInfer = "test0000000000000000";
                                                    }
                                                    else
                                                    {
                                                        if (m_nMemoryindex[2] < 9)
                                                        {
                                                            //m_QrCodeInfer = Qrcode[2].First();
                                                            m_QrCodeInfer = Qrcode3.First();
                                                        }
                                                        else
                                                        {
                                                            //m_QrCodeInfer = Qrcode[2].First();
                                                            //Qrcode[2].RemoveAt(0);
                                                            m_QrCodeInfer = Qrcode3.First();
                                                            Qrcode3.RemoveAt(0);
                                                        }
                                                    }

                                                    // m_QrCodeInfer = "test0000000000000000";
                                                    m_niInfer.Sample_Number = m_QrCodeInfer;
                                                    m_niInfer.Position_Number = ((m_nMemoryindex[2]) / 5 + 2 + 3).ToString("00");
                                                    m_niInfer.Cam_Num = 2.ToString("00");
                                                    m_niInfer.Light_Num = (m_nMemoryindex[2] % 5).ToString("0");
                                                    m_stSaveParamInfer[2].pData = Linprt[2].First();
                                                    m_niInfer.Time_Now = DateTime.Now.ToString("yyyyMMddHHmmssff");
                                                    Save_file_tmp = ServerSaveFilePath + m_Job_name + "_" + m_QrCodeInfer + "_S" + m_niInfer.SampleID + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_" + m_niInfer.Time_Now;
                                                    // Save_file_tmp = ServerSaveFilePath + m_Job_name + "_" + m_niInfer.Sample_Number + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_S" + m_niInfer.SampleID + "_" + m_niInfer.Time_Now;
                                                    string m_StsServers2 = "";
                                                    m_StsServers2 = m_Job_name + "_" + m_QrCodeInfer + "_S" + m_niInfer.SampleID + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_" + m_niInfer.Time_Now;
                                                    m_stSaveParamInfer[2].enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                                                    m_stSaveParamInfer[2].pImagePath = Save_file_tmp + ".jpg";
                                                    //m_stSaveParamInfer.pImagePath = Save_file_tmp + ".jpg";
                                                    m_stSaveParamInfer[2].nQuality = 98;//存Jpeg时有效
                                                    if (m_stSaveParamInfer[2].pData != null)
                                                    {
                                                        int nRet1 = m_listMvCameras[2].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer[2]);
                                                        if (MyCamera.MV_OK != nRet1)
                                                        {
                                                            LogHelper.LogInfo("相机00J27960926" + Save_file_tmp + "false");
                                                            int nRet2 = m_listMvCameras[2].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer[2]);
                                                            if (MyCamera.MV_OK != nRet1)
                                                            {
                                                                LogHelper.LogInfo("相机00J27960926二次存图");
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Windows.MessageBox.Show("没有图像数据");
                                                    }


                                                    //else
                                                    //{
                                                    //    LogHelper.LogInfo("相机00J27960926" + Save_file_tmp);
                                                    //}
                                                    //LogHelper.LogInfo(string.Join(",", Filenames[nCamIndex]));
                                                    // Filenames.Add(m_StsServers + ".jpg");
                                                    Linprt[2].RemoveAt(0);
                                                    if (m_QrCodeInfer != "test0000000000000000")
                                                    {
                                                        if (!IsOdd(m_nMemoryindex[2] / m_iOnePositionPic))
                                                        {
                                                            m_arrPositionFilnames1[1].Add(m_StsServers2 + ".jpg");
                                                        }
                                                        else
                                                        {
                                                            m_arrPositionFilnames2[0].Add(m_StsServers2 + ".jpg");
                                                        }
                                                    }
                                                    GC.Collect();
                                                }
                                            }
                                            else
                                            {
                                                if (AccCheck)
                                                {
                                                    m_stSaveParamInfer[2].pData = Linprt[2].First();
                                                    m_niInfer.Time_Now = DateTime.Now.ToString("yyyyMMddHHmmssff");
                                                    Save_file_tmp1 = SaveCheckPath3 + "_C3" + m_niInfer.Time_Now;
                                                    m_stSaveParamInfer[2].enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                                                    m_stSaveParamInfer[2].pImagePath = Save_file_tmp1 + ".jpg";
                                                    m_stSaveParamInfer[2].nQuality = 98;//存Jpeg时有效
                                                    int nRet1 = m_listMvCameras[2].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer[2]);
                                                    Linprt[2].RemoveAt(0);
                                                }
                                                else
                                                {
                                                    m_stSaveParamInfer[2].pData = Linprt[2].First();
                                                    m_niInfer.Time_Now = DateTime.Now.ToString("yyyyMMddHHmmssff");
                                                    Save_file_tmp1 = SaveCheckPath3_1 + "_C3" + m_niInfer.Time_Now;
                                                    m_stSaveParamInfer[2].enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                                                    m_stSaveParamInfer[2].pImagePath = Save_file_tmp1 + ".jpg";
                                                    m_stSaveParamInfer[2].nQuality = 98;//存Jpeg时有效
                                                    int nRet1 = m_listMvCameras[2].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer[2]);
                                                    Linprt[2].RemoveAt(0);
                                                }


                                            }

                                            break;
                                        //正面检测相机一个工位
                                        case 3:
                                            if (!AccCheck && !AccCheckA)
                                            {
                                                lock (Position_NumberLock[0])
                                                {
                                                    //m_nMemoryindex[3] = ListNumflag4.First();
                                                    //ListNumflag4.RemoveAt(0);
                                                    lock (m_SamNumLock)
                                                    {
                                                        SamNum = 6;
                                                    }

                                                    SampleIDFlag[6]++;
                                                    if (SampleIDFlag[6] == 6)
                                                    {

                                                        // SamNum = 5;
                                                        sample_id_num[6]++;
                                                        m_listTotalSample[6].Add(1);
                                                        SampleIDFlag[6] = 1;
                                                    }
                                                    //if (Qrcode[3].Count == 0)
                                                    //{
                                                    //    m_QrCodeInfer = "test0000000000000000";
                                                    //}
                                                    if (Qrcode4.Count == 0)
                                                    {
                                                        m_QrCodeInfer = "test0000000000000000";
                                                    }
                                                    else
                                                    {
                                                        if (m_nMemoryindex[3] == 4 || m_nMemoryindex[3] == 9)
                                                        {
                                                            //m_QrCodeInfer = Qrcode[3].First();
                                                            //Qrcode[3].RemoveAt(0);
                                                            m_QrCodeInfer = Qrcode4.First();
                                                            Qrcode4.RemoveAt(0);
                                                        }
                                                        else
                                                        {
                                                            //m_QrCodeInfer = Qrcode[3].First();
                                                            m_QrCodeInfer = Qrcode4.First();
                                                        }
                                                    }

                                                    //  m_QrCodeInfer = "test0000000000000000";
                                                    m_niInfer.Sample_Number = m_QrCodeInfer;
                                                    m_niInfer.Position_Number = ((m_nMemoryindex[3]) / 10 + 3 + 4).ToString("00");
                                                    m_niInfer.Cam_Num = 3.ToString("00");
                                                    m_niInfer.Light_Num = (m_nMemoryindex[3] % 5).ToString("0");
                                                    m_niInfer.SampleID = sample_id_num[6].ToString("000000");
                                                    m_stSaveParamInfer[3].pData = Linprt[3].First();
                                                    m_niInfer.Time_Now = DateTime.Now.ToString("yyyyMMddHHmmssff");
                                                    Save_file_tmp = ServerSaveFilePath + m_Job_name + "_" + m_QrCodeInfer + "_S" + m_niInfer.SampleID + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_" + m_niInfer.Time_Now;
                                                    // Save_file_tmp = ServerSaveFilePath + m_Job_name + "_" + m_niInfer.Sample_Number + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_S" + m_niInfer.SampleID + "_" + m_niInfer.Time_Now;
                                                    string m_StsServers3 = "";
                                                    m_StsServers3 = m_Job_name + "_" + m_QrCodeInfer + "_S" + m_niInfer.SampleID + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_" + m_niInfer.Time_Now;
                                                    m_stSaveParamInfer[3].enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                                                    m_stSaveParamInfer[3].pImagePath = Save_file_tmp + ".jpg";
                                                    //m_stSaveParamInfer.pImagePath = Save_file_tmp + ".jpg";
                                                    m_stSaveParamInfer[3].nQuality = 98;//存Jpeg时有效
                                                    int nRet1 = m_listMvCameras[3].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer[3]);
                                                    if (MyCamera.MV_OK != nRet1)
                                                    {
                                                        LogHelper.LogInfo("相机00J26664733" + Save_file_tmp + "false");
                                                        int nRet2 = m_listMvCameras[3].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer[3]);
                                                        if (MyCamera.MV_OK == nRet2)
                                                        {
                                                            LogHelper.LogInfo("相机00J26664733二次存图");
                                                        }
                                                    }
                                                    //else
                                                    //{
                                                    //    LogHelper.LogInfo("相机00J26664733" + Save_file_tmp);
                                                    //}
                                                    //LogHelper.LogInfo(string.Join(",", Filenames[nCamIndex]));
                                                    //Filenames.Add(m_StsServers + ".jpg");
                                                    Linprt[3].RemoveAt(0);
                                                    if (m_QrCodeInfer != "test0000000000000000")
                                                    {
                                                        m_arrPositionFilnames2[1].Add(m_StsServers3 + ".jpg");
                                                    }
                                                    GC.Collect();
                                                }
                                            }
                                            else
                                            {
                                                if (AccCheck)
                                                {
                                                    m_stSaveParamInfer[3].pData = Linprt[3].First();
                                                    m_niInfer.Time_Now = DateTime.Now.ToString("yyyyMMddHHmmssff");
                                                    Save_file_tmp1 = SaveCheckPath4 + "_C4" + m_niInfer.Time_Now;
                                                    m_stSaveParamInfer[3].enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                                                    m_stSaveParamInfer[3].pImagePath = Save_file_tmp1 + ".jpg";
                                                    m_stSaveParamInfer[3].nQuality = 98;//存Jpeg时有效
                                                    int nRet1 = m_listMvCameras[3].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer[3]);
                                                    Linprt[3].RemoveAt(0);
                                                }
                                                else
                                                {
                                                    m_stSaveParamInfer[3].pData = Linprt[3].First();
                                                    m_niInfer.Time_Now = DateTime.Now.ToString("yyyyMMddHHmmssff");
                                                    Save_file_tmp1 = SaveCheckPath4_1 + "_C4" + m_niInfer.Time_Now;
                                                    m_stSaveParamInfer[3].enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                                                    m_stSaveParamInfer[3].pImagePath = Save_file_tmp1 + ".jpg";
                                                    m_stSaveParamInfer[3].nQuality = 98;//存Jpeg时有效
                                                    int nRet1 = m_listMvCameras[3].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer[3]);
                                                    Linprt[3].RemoveAt(0);
                                                }


                                            }

                                            break;

                                        default:
                                            break;

                                    }
                                    if (!AccCheck && !AccCheckA)
                                    {
                                        bool isNeedAdd = false;

                                        for (int i = 0; i < m_iPositionNum; i++)
                                        {
                                            if (m_listTotalSample[i].Count > 0)
                                            {
                                                tmp++;
                                            }

                                        }
                                        if (tmp == 7)
                                        {
                                            isNeedAdd = true;
                                            tmp = 0;
                                        }
                                        if (isNeedAdd)
                                        {
                                            for (int i = 0; i < m_iPositionNum; i++)
                                            {
                                                if (m_listTotalSample[i].Count > 0)
                                                {
                                                    m_listTotalSample[i].RemoveAt(0);
                                                }

                                            }


                                        }
                                    }



                                    // m_niInfer.Sample_Number = m_QrCodeInfer;




                                    //lock (m_MemoryaveCamNumberLock1)
                                    //{
                                    //    m_niInfer.Cam_Num = nCamIndex.ToString("00");
                                    //}

                                    //保存图像
                                    //Save_file_tmp = ServerSaveFilePath + m_Job_name + "_S" + m_niInfer.SampleID + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_" + m_niInfer.Time_Now;
                                    //// Save_file_tmp = ServerSaveFilePath + m_Job_name + "_" + m_niInfer.Sample_Number + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_S" + m_niInfer.SampleID + "_" + m_niInfer.Time_Now;
                                    //string m_StsServers = "";
                                    //m_StsServers = m_Job_name + "_" + m_niInfer.Sample_Number + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num + "_L" + m_niInfer.Light_Num + "_S" + m_niInfer.SampleID + "_" + m_niInfer.Time_Now;
                                    //m_stSaveParamInfer.enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                                    //m_stSaveParamInfer.pImagePath = Save_file_tmp + ".jpg";
                                    //m_stSaveParamInfer.nQuality = 98;//存Jpeg时有效
                                    //int nRet1 = m_listMvCameras[0].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer);

                                    //if (MyCamera.MV_OK != nRet1)
                                    //{
                                    //    LogHelper.LogInfo(Save_file_tmp + "false");
                                    //}
                                    //else
                                    //{
                                    //    LogHelper.LogInfo(Save_file_tmp);
                                    //}


                                    //给推理机的数据加入列表
                                    //LogHelper.LogInfo(string.Join(",", Filenames[nCamIndex]));
                                    //Filenames.Add(m_StsServers + ".jpg");
                                    /*  //存储缩略图
                                      string saveThumbnailFilePath = m_sThumbnailServerSaveFilePath + m_Job_name + "_" + m_niInfer.Sample_Number + "_P" + m_niInfer.Position_Number + "_C" + m_niInfer.Cam_Num +
                                          "_L" + m_niInfer.Light_Num + "_S" + m_niInfer.SampleID + "_" + m_niInfer.Time_Now;
                                      m_stSaveParamInfer.pImagePath = saveThumbnailFilePath + ".jpg";
                                      m_stSaveParamInfer.nQuality = 80;//存Jpeg时有效
                                      int nRet2 = m_listMvCameras[0].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamInfer);*/
                                }
                            }
                        }

                    }



                }

                //}
                Thread.Sleep(5);
            }
        }

        private void SaveSeparateToWholeNames()
        {
            // int PositionNum = (int)obj;
            int ImageIndex = 0;
            bool bHasData = false;
            string js1 = "";
            string js11 = "";
            m_arrictAscr.file_names = new List<string>();
            while (m_bWhileSeparateToWholeNames)
            {
                while (!IsCloseInfer)
                {
                    if (m_bRunWhileSeparateToWholeNames)
                    {
                        //lock (m_WhileSeparateToWholeNames)//202211221814
                        //{
                        if (m_bConnectionServer == true)
                        {
                            if (m_bCheckConnected)
                            {

                                bHasData = false;
                                lock (m_SendReasoningMachineLock)
                                {

                                    if (m_arrPositionFilnames[ImageIndex].Count >= 5)
                                    {

                                        // m_arrictAscr.file_names = m_arrPositionFilnames[ImageIndex].GetRange(0, 5);

                                        for (int i = 0; i < 5; i++)
                                        {
                                            m_arrictAscr.file_names.Add(m_arrPositionFilnames[ImageIndex][i]);
                                        }
                                        // m_arrPositionFilnames[ImageIndex].RemoveRange(0, 5);
                                        bHasData = true;
                                    }

                                }


                                if (bHasData)
                                {

                                    m_arrictAscr.relative_dir = "data";

                                    Stopwatch st = new Stopwatch();
                                    // LogHelper.LogInfo(string.Join("\n", m_arrictAscr));
                                    st.Start();
                                    m_ayySrdn = hq.CommitSample00(Client_IP, Cilent_Port, m_arrictAscr);
                                    st.Stop();
                                    int k = ImageIndex + 1;
                                    //  LogHelper.LogInfo("推理机第一次返回时间" + "第" + k + "面" + st.ElapsedMilliseconds.ToString());
                                    if (m_ayySrdn == null || m_ayySrdn.data.res == null)
                                    {
                                        LogHelper.LogError(m_arrPositionFilnames[ImageIndex][0]);
                                        LogHelper.LogError(m_arrPositionFilnames[ImageIndex][1]);
                                        LogHelper.LogError(m_arrPositionFilnames[ImageIndex][2]);
                                        LogHelper.LogError(m_arrPositionFilnames[ImageIndex][3]);
                                        LogHelper.LogError(m_arrPositionFilnames[ImageIndex][4]);
                                        LogHelper.LogError(m_arrictAscr.file_names[0]);
                                        LogHelper.LogError(m_arrictAscr.file_names[1]);
                                        LogHelper.LogError(m_arrictAscr.file_names[2]);
                                        LogHelper.LogError(m_arrictAscr.file_names[3]);
                                        LogHelper.LogError(m_arrictAscr.file_names[4]);
                                        lock (MotionProcess.JsonConvertLock)
                                        {
                                             js1 = JsonConvert.SerializeObject(m_arrictAscr);                                         
                                        }
                                        LogHelper.LogError(js1);
                                        m_arrictAscr.file_names.Clear();
                                        for (int i = 0; i < 5; i++)
                                        {
                                            m_arrictAscr.file_names.Add(m_arrPositionFilnames[ImageIndex][i]);
                                        }
                                        Stopwatch st1 = new Stopwatch();
                                        st1.Start();
                                        m_ayySrdn = hq.CommitSample00(Client_IP, Cilent_Port, m_arrictAscr);
                                        st1.Stop();
                                        LogHelper.LogInfo("推理机重新发送一次返回时间" + st1.ElapsedMilliseconds.ToString());
                                    }

                                    try
                                    {
                                        if (m_ayySrdn == null || m_ayySrdn.data.res == null)
                                        {
                                            switch (ImageIndex)
                                            {
                                                case 0:
                                                    LogHelper.LogError("推理服务器P1面返回错误!");
                                                    break;
                                                case 1:
                                                    LogHelper.LogError("推理服务器P2面返回错误!");
                                                    break;
                                                case 2:
                                                    LogHelper.LogError("推理服务器P3面返回错误!");
                                                    break;
                                            }
                                            LogHelper.LogError(m_arrictAscr.file_names[0]);
                                            LogHelper.LogError(m_arrictAscr.file_names[1]);
                                            LogHelper.LogError(m_arrictAscr.file_names[2]);
                                            LogHelper.LogError(m_arrictAscr.file_names[3]);
                                            LogHelper.LogError(m_arrictAscr.file_names[4]);
                                            lock (MotionProcess.JsonConvertLock)
                                            {
                                                 js11 = JsonConvert.SerializeObject(m_arrictAscr);
                                            }
                                            LogHelper.LogError(js11);
                                            LogHelper.LogError("推理服务器返回错误!");
                                            m_arrictAscr.file_names.RemoveRange(0, 5);
                                            Alarm();

                                        }
                                        else
                                        {
                                            switch (ImageIndex)
                                            {
                                                case 0:
                                                    // Pose_result[ImageIndex].Add(m_ayySrdn.data);
                                                    Pose_result1.Add(m_ayySrdn.data);
                                                    break;
                                                case 1:
                                                    // Pose_result[ImageIndex].Add(m_ayySrdn.data);
                                                    Pose_result2.Add(m_ayySrdn.data);
                                                    break;
                                                case 2:
                                                    //  Pose_result[ImageIndex].Add(m_ayySrdn.data);
                                                    Pose_result3.Add(m_ayySrdn.data);
                                                    break;
                                                    //case 3:
                                                    //    Pose_result[PositionNum].Add(m_ayySrdn.data);
                                                    //    break;
                                                    //case 4:
                                                    //    Pose_result[PositionNum].Add(m_ayySrdn.data);
                                                    //    break;
                                                    //case 5:
                                                    //    Pose_result[PositionNum].Add(m_ayySrdn.data);
                                                    //    break;
                                                    //case 6:
                                                    //    Pose_result[PositionNum].Add(m_ayySrdn.data);
                                                    //    break;

                                            }
                                            //  m_arrictAscr2.pose_result.Add(m_ayySrdn.data);
                                            //if (m_nSendInference == 6)
                                            //{
                                            //    m_arrictAscr2.sample_id = m_listSampleIDs[0];
                                            //    m_listSampleIDs.RemoveAt(0);
                                            //}
                                            //else
                                            //{
                                            //    m_arrictAscr2.sample_id = m_listSampleIDs[0];
                                            //}
                                            m_arrPositionFilnames[ImageIndex].RemoveRange(0, 5);
                                            m_arrictAscr.file_names.RemoveRange(0, 5);
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
                                        // event_ProcessSampleResultData_NVT(m_ayySrdn);
                                        // break;

                                    }
                                }
                                ImageIndex++;
                                if (ImageIndex == 3)
                                {
                                    ImageIndex = 0;
                                }


                                //}

                            }


                        }
                        else
                        {
                            LogHelper.LogInfo("推理机通讯断开!");
                        }
                        // }
                    }


                }
                Thread.Sleep(5);

            }
        }

        private void SaveSeparateToWholeNames1()
        {
            // int PositionNum = (int)obj;
            int ImageIndex1 = 0;
            bool bHasData1 = false;
            string js21 = "";
            string js22 = "";
            m_arrictAscr1.file_names = new List<string>();
            while (m_bWhileSeparateToWholeNames1)
            {
                while (!IsCloseInfer)
                {
                    if (m_bRunWhileSeparateToWholeNames1)
                    {
                        //lock (m_WhileSeparateToWholeNames1)
                        //{
                        if (m_bConnectionServer == true)
                        {
                            if (m_bCheckConnected)
                            {

                                bHasData1 = false;
                                lock (m_SendReasoningMachineLock1)
                                {
                                    if (m_arrPositionFilnames1[ImageIndex1].Count >= 5)
                                    {

                                        for (int i = 0; i < 5; i++)
                                        {
                                            m_arrictAscr1.file_names.Add(m_arrPositionFilnames1[ImageIndex1][i]);
                                        }
                                       // m_arrictAscr1.file_names = m_arrPositionFilnames1[ImageIndex1].GetRange(0, 5);
                                        // m_arrPositionFilnames1[ImageIndex1].RemoveRange(0, 5);
                                        bHasData1 = true;
                                    }

                                }


                                if (bHasData1)
                                {

                                    m_arrictAscr1.relative_dir = "data";

                                    Stopwatch st = new Stopwatch();
                                    // LogHelper.LogInfo(string.Join("\n", m_arrictAscr));
                                    st.Start();
                                    m_ayySrdn1 = hq.CommitSample01(Client_IP, Cilent_Port, m_arrictAscr1);
                                    st.Stop();
                                    int K = ImageIndex1 + 4;
                                    // LogHelper.LogInfo("推理机第一次返回时间" + "第" + K + "面" + st.ElapsedMilliseconds.ToString());
                                    if (m_ayySrdn1 == null || m_ayySrdn1.data.res == null)
                                    {
                                        LogHelper.LogError(m_arrPositionFilnames1[ImageIndex1][0]);
                                        LogHelper.LogError(m_arrPositionFilnames1[ImageIndex1][1]);
                                        LogHelper.LogError(m_arrPositionFilnames1[ImageIndex1][2]);
                                        LogHelper.LogError(m_arrPositionFilnames1[ImageIndex1][3]);
                                        LogHelper.LogError(m_arrPositionFilnames1[ImageIndex1][4]);
                                        LogHelper.LogError(m_arrictAscr1.file_names[0]);
                                        LogHelper.LogError(m_arrictAscr1.file_names[1]);
                                        LogHelper.LogError(m_arrictAscr1.file_names[2]);
                                        LogHelper.LogError(m_arrictAscr1.file_names[3]);
                                        LogHelper.LogError(m_arrictAscr1.file_names[4]);
                                        lock (MotionProcess.JsonConvertLock)
                                        {
                                             js21 = JsonConvert.SerializeObject(m_arrictAscr1);
                                          
                                        }
                                        LogHelper.LogError(js21);
                                        //LogHelper.LogError(JsonConvert.SerializeObject(m_arrictAscr1));
                                        m_arrictAscr1.file_names.Clear();
                                        for (int i = 0; i < 5; i++)
                                        {
                                            m_arrictAscr1.file_names.Add(m_arrPositionFilnames1[ImageIndex1][i]);
                                        }
                                        //LogHelper.LogError(JsonConvert.SerializeObject(m_arrictAscr1));
                                        Stopwatch st1 = new Stopwatch();
                                        st1.Start();
                                        m_ayySrdn1 = hq.CommitSample01(Client_IP, Cilent_Port, m_arrictAscr1);
                                        st1.Stop();
                                        LogHelper.LogInfo("推理机重新发送一次返回时间" + st1.ElapsedMilliseconds.ToString());
                                    }

                                    try
                                    {
                                        if (m_ayySrdn1 == null || m_ayySrdn1.data.res == null)
                                        {
                                            switch (ImageIndex1)
                                            {
                                                case 0:
                                                    LogHelper.LogError("推理服务器P4面返回错误!");
                                                    break;
                                                case 1:
                                                    LogHelper.LogError("推理服务器P5面返回错误!");
                                                    break;

                                            }
                                            LogHelper.LogError(m_arrictAscr1.file_names[0]);
                                            LogHelper.LogError(m_arrictAscr1.file_names[1]);
                                            LogHelper.LogError(m_arrictAscr1.file_names[2]);
                                            LogHelper.LogError(m_arrictAscr1.file_names[3]);
                                            LogHelper.LogError(m_arrictAscr1.file_names[4]);
                                            lock (MotionProcess.JsonConvertLock)
                                            {
                                                 js22 = JsonConvert.SerializeObject(m_arrictAscr1);
                                               
                                            }
                                            LogHelper.LogError(js22);
                                            // LogHelper.LogError(JsonConvert.SerializeObject(m_arrictAscr1));
                                            LogHelper.LogError("推理服务器返回错误!");
                                            m_arrictAscr1.file_names.RemoveRange(0, 5);
                                            Alarm();

                                        }
                                        else
                                        {
                                            switch (ImageIndex1)
                                            {

                                                case 0:
                                                    // Pose_result[3].Add(m_ayySrdn1.data);
                                                    Pose_result4.Add(m_ayySrdn1.data);
                                                    break;
                                                case 1:
                                                    // Pose_result[4].Add(m_ayySrdn1.data);
                                                    Pose_result5.Add(m_ayySrdn1.data);
                                                    break;
                                                    //case 6:
                                                    //    Pose_result[PositionNum].Add(m_ayySrdn1.data);
                                                    //    break;

                                            }
                                            //  m_arrictAscr2.pose_result.Add(m_ayySrdn.data);
                                            //if (m_nSendInference == 6)
                                            //{
                                            //    m_arrictAscr2.sample_id = m_listSampleIDs[0];
                                            //    m_listSampleIDs.RemoveAt(0);
                                            //}
                                            //else
                                            //{
                                            //    m_arrictAscr2.sample_id = m_listSampleIDs[0];
                                            //}
                                            m_arrPositionFilnames1[ImageIndex1].RemoveRange(0, 5);
                                            m_arrictAscr1.file_names.RemoveRange(0, 5);
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
                                        // event_ProcessSampleResultData_NVT(m_ayySrdn);
                                        // break;

                                    }
                                }
                                ImageIndex1++;
                                if (ImageIndex1 == 2)
                                {
                                    ImageIndex1 = 0;
                                }


                                // }

                            }


                        }
                        else
                        {
                            LogHelper.LogInfo("推理机通讯断开!");
                        }
                        // }
                    }

                }
                Thread.Sleep(5);

            }
        }

        private void SaveSeparateToWholeNames2()
        {
            //int PositionNum = (int)obj;
            int ImageIndex2 = 0;
            bool bHasData2 = false;
            string js31 = "";
            string js32 = "";
            m_arrictAscr02.file_names = new List<string>();
            while (m_bWhileSeparateToWholeNames2)
            {
                while (!IsCloseInfer)
                {

                    if (m_bRunWhileSeparateToWholeNames2)
                    {
                        //lock (m_WhileSeparateToWholeNames2)
                        //{
                        if (m_bConnectionServer == true)
                        {
                            if (m_bCheckConnected)
                            {

                                bHasData2 = false;
                                lock (m_SendReasoningMachineLock2)
                                {
                                    if (m_arrPositionFilnames2[ImageIndex2].Count >= 5)
                                    {
                                        for (int i = 0; i < 5; i++)
                                        {
                                            m_arrictAscr02.file_names.Add(m_arrPositionFilnames2[ImageIndex2][i]);
                                        }
                                      //  m_arrictAscr02.file_names = m_arrPositionFilnames2[ImageIndex2].GetRange(0, 5);
                                        //  m_arrPositionFilnames2[ImageIndex2].RemoveRange(0, 5);
                                        bHasData2 = true;
                                    }
                                }



                                if (bHasData2)
                                {

                                    m_arrictAscr02.relative_dir = "data";

                                    Stopwatch st = new Stopwatch();
                                    // LogHelper.LogInfo(string.Join("\n", m_arrictAscr));
                                    st.Start();
                                    m_ayySrdn02 = hq.CommitSample02(Client_IP, Cilent_Port, m_arrictAscr02);

                                    st.Stop();
                                    int K = ImageIndex2 + 6;
                                    // LogHelper.LogInfo("推理机第一次返回时间" + "第" + K + "面" + st.ElapsedMilliseconds.ToString());
                                    if (m_ayySrdn02 == null || m_ayySrdn02.data.res == null)
                                    {
                                        LogHelper.LogError(m_arrPositionFilnames2[ImageIndex2][0]);
                                        LogHelper.LogError(m_arrPositionFilnames2[ImageIndex2][1]);
                                        LogHelper.LogError(m_arrPositionFilnames2[ImageIndex2][2]);
                                        LogHelper.LogError(m_arrPositionFilnames2[ImageIndex2][3]);
                                        LogHelper.LogError(m_arrPositionFilnames2[ImageIndex2][4]);
                                        LogHelper.LogError(m_arrictAscr02.file_names[0]);
                                        LogHelper.LogError(m_arrictAscr02.file_names[1]);
                                        LogHelper.LogError(m_arrictAscr02.file_names[2]);
                                        LogHelper.LogError(m_arrictAscr02.file_names[3]);
                                        LogHelper.LogError(m_arrictAscr02.file_names[4]);
                                        lock (MotionProcess.JsonConvertLock)
                                        {
                                             js31 = JsonConvert.SerializeObject(m_arrictAscr02);
                                           
                                        }
                                        LogHelper.LogError(js31);
                                        // LogHelper.LogError(JsonConvert.SerializeObject(m_arrictAscr02));
                                        m_arrictAscr02.file_names.Clear();
                                        for (int i = 0; i < 5; i++)
                                        {
                                            m_arrictAscr02.file_names.Add(m_arrPositionFilnames2[ImageIndex2][i]);
                                        }
                                        //LogHelper.LogError(JsonConvert.SerializeObject(m_arrictAscr02));
                                        Stopwatch st1 = new Stopwatch();
                                        st1.Start();
                                        m_ayySrdn02 = hq.CommitSample02(Client_IP, Cilent_Port, m_arrictAscr02);
                                        st1.Stop();
                                        LogHelper.LogInfo("推理机重新发送一次返回时间" + st1.ElapsedMilliseconds.ToString());
                                    }

                                    try
                                    {
                                        if (m_ayySrdn02 == null || m_ayySrdn02.data.res == null)
                                        {
                                            switch (ImageIndex2)
                                            {
                                                case 0:
                                                    LogHelper.LogError("推理服务器P6面返回错误!");
                                                    break;
                                                case 1:
                                                    LogHelper.LogError("推理服务器P7面返回错误!");
                                                    break;

                                            }
                                            LogHelper.LogError(m_arrictAscr02.file_names[0]);
                                            LogHelper.LogError(m_arrictAscr02.file_names[1]);
                                            LogHelper.LogError(m_arrictAscr02.file_names[2]);
                                            LogHelper.LogError(m_arrictAscr02.file_names[3]);
                                            LogHelper.LogError(m_arrictAscr02.file_names[4]);
                                            lock (MotionProcess.JsonConvertLock)
                                            {
                                                 js32 = JsonConvert.SerializeObject(m_arrictAscr02);
                                               
                                            }
                                            LogHelper.LogError(js32);
                                            // LogHelper.LogError(JsonConvert.SerializeObject(m_arrictAscr02));
                                            LogHelper.LogError("推理服务器返回错误!");
                                            m_arrictAscr02.file_names.RemoveRange(0, 5);
                                            Alarm();

                                        }
                                        else
                                        {
                                            switch (ImageIndex2)
                                            {

                                                case 0:
                                                    // Pose_result[5].Add(m_ayySrdn02.data);
                                                    Pose_result6.Add(m_ayySrdn02.data);
                                                    break;
                                                case 1:
                                                    // Pose_result[6].Add(m_ayySrdn02.data);
                                                    Pose_result7.Add(m_ayySrdn02.data);
                                                    break;
                                                    //case 6:
                                                    //    Pose_result[PositionNum].Add(m_ayySrdn02.data);
                                                    //    break;

                                            }

                                            m_arrPositionFilnames2[ImageIndex2].RemoveRange(0, 5);
                                            m_arrictAscr02.file_names.RemoveRange(0, 5);
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
                                        // event_ProcessSampleResultData_NVT(m_ayySrdn);
                                        // break;

                                    }
                                }
                                ImageIndex2++;
                                if (ImageIndex2 == 2)
                                {
                                    ImageIndex2 = 0;
                                }

                                //}

                            }


                        }
                        else
                        {
                            LogHelper.LogInfo("推理机通讯断开!");
                        }
                        // }
                    }

                    Thread.Sleep(5);
                }

            }
        }
        public void GetModelPoint(HWindowControl hWindowControl, int CamerNum, HObject hImage, int motorNum)
        {

            HalconOperator.ShowImage(hWindowControl, hImage);
            halconOperatorList[CamerNum + 1].OnlyFindShapModel(hWindowControl.HalconWindow, hImage, CamerNum, out HalconOperator.ModelResult PointResult);
            List<PointXYU> pointXYUs = new List<PointXYU>();
            if (PointResult.Row != 0)
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
        public void GetPointList(int camNo, List<PointXYU> points, int motorNum)
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
                        lock (visoninputLock)
                        {
                            motorInstance.SendPoint(pickPoints6, camNo);
                        }

                        MotorsClass.omronInstance.Write(plc_cam_status6, (float)0);
                        // string code = GetQrCode();20220513屏蔽
                        //for (int i = 0; i < m_iPositionNum; i++)
                        //{
                        //    m_listQrCodeInfer[i].Add(code);
                        //}

                        //m_listQrCodeConnectionPositive.Add(code);
                        //m_listQrCodeConnectionNegative.Add(code);
                        //event_QrCodeShow(code);
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
                        lock (visoninputLock)
                        {
                            motorInstance.SendPoint(pickPoints8, camNo);
                        }

                        MotorsClass.omronInstance.Write(plc_cam_status8, (float)0);
                    }
                    if (pickPoints8 != null)
                    {
                        pickPointsShow = pickPoints8[0];
                    }
                    break;
                case 5:
                    if (motorNum == 1)
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
        public void SaveFPCImage(object ob)
        {
            int CamNum = (int)ob;
            //  int nCamIndex = 0;

            bool bHasData = false;
            while (!m_bWhileStatus_SaveFPCImage)
            {

                if (m_bRunSaveImage_FPC)
                {
                    bHasData = false;
                    lock (m_lockBufForSaveFPCImageLock)
                    {
                        if (m_dictImageConnectionIndex[0].Count > 0)
                        {
                            m_nMemoryindex = m_dictImageConnectionIndex[0].First();
                            m_dictImageConnectionIndex[0].RemoveAt(0);
                            bHasData = true;
                        }
                    }

                    if (bHasData)
                    {
                        //Thread.Sleep(150);
                        //保存图像
                        m_stSaveParamConnection[0].pData = m_pSaveImageBuf_Connection[0, m_nMemoryindex];
                        m_nameConnection.Time_Now = DateTime.Now.ToString("yyyyMMddHHmmss");
                        try
                        {


                            m_nameConnection.SampleID = m_iSampleIdFPC_num.ToString("000000");
                            if (QrcodeFisrtRead.Count > 0)
                            {
                                m_sQrCodeConnection = QrcodeFisrtRead.First();
                                QrcodeFisrtRead.RemoveAt(0);
                            }

                            else
                            {
                                m_sQrCodeConnection = "FPC0000000000000000000000";
                            }

                            m_nameConnection.Position_Number = (9).ToString("00");
                            m_nameConnection.Light_Num = "0";
                            m_nameConnection.Cam_Num = (9).ToString("00");

                            m_nameConnection.Sample_Number = m_sQrCodeConnection;
                            string Save_file_tmp = ConnectionFilePath + m_Job_name + "_" + m_nameConnection.Sample_Number + "_P" + m_nameConnection.Position_Number +
                                "_C" + m_nameConnection.Cam_Num + "_L" + m_nameConnection.Light_Num + "_S" + m_nameConnection.SampleID + "_" + m_nameConnection.Time_Now;
                            m_stSaveParamConnection[0].enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                            m_stSaveParamConnection[0].pImagePath = Save_file_tmp + ".jpg";
                            m_stSaveParamConnection[0].nQuality = 98;//存Jpeg时有效
                            int nRet1 = m_listMvCameras[9].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamConnection[0]);
                            m_iSampleIdFPC_num++;
                            //string saveThumbnailFilePath = m_sThumbnailConnectionSaveFilePath + m_Job_name + "_" + m_nameConnection.Sample_Number + "_P" + m_nameConnection.Position_Number + "_C" + m_nameConnection.Cam_Num + "_L" + m_nameConnection.Light_Num + "_S" + m_nameConnection.SampleID + "_" + m_nameConnection.Time_Now;
                            //m_stSaveParamConnection[nCamIndex].pImagePath = saveThumbnailFilePath + ".jpg";
                            //m_stSaveParamConnection[nCamIndex].nQuality = 80;//存Jpeg时有效
                            //int nRet2 = m_listMvCameras[9].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamConnection[nCamIndex]);

                        }
                        catch (Exception ex)
                        {

                            System.Windows.MessageBox.Show(ex.ToString());
                        }

                    }
                }
            }
        }

        public void SaveConnectionImage(object ob)
        {
            int CamNum = (int)ob;
            int nCamIndex;
            if (CamNum == 5)
            {
                nCamIndex = 1;
            }
            else
            {
                nCamIndex = 2;
            }
            //  int nCamIndex = 0;

            bool bHasData = false;
            while (!m_bWhileStatus_SaveConnectionImage)
            {
                //if (!abort)
                //{


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
                        switch (nCamIndex)
                        {
                            //反面 watch钢片
                            case 1:
                                lock (m_lockBufForSaveConnectionImageLock)
                                {

                                    //  Thread.Sleep(180);
                                    m_stSaveParamConnection[nCamIndex].pData = m_pSaveImageBuf_Connection[nCamIndex, m_nMemoryindex];
                                    m_nameConnection.Time_Now = DateTime.Now.ToString("yyyyMMddHHmmss");
                                    try
                                    {


                                        m_nameConnection.SampleID = m_iSampleIdConnection_num[0].ToString("000000");

                                        if (QrcodeConnectNRead.Count > 0)
                                        {
                                            m_sQrCodeConnection = QrcodeConnectNRead.First();
                                            QrcodeConnectNRead.RemoveAt(0);
                                        }

                                        else
                                        {
                                            m_sQrCodeConnection = "Nc0000000000000000000000";
                                        }

                                        m_nameConnection.Position_Number = (5).ToString("00");
                                        m_nameConnection.Light_Num = "0";
                                        m_nameConnection.Cam_Num = (5).ToString("00");

                                        m_nameConnection.Sample_Number = m_sQrCodeConnection;
                                        string Save_file = SaveCheckPath5 + "connection1" + m_nameConnection.Time_Now;
                                        string Save_file1 = SaveCheckPath5_1 + "connection1" + m_nameConnection.Time_Now;
                                        string Save_file_tmp = ConnectionFilePath1 + m_Job_name + "_" + m_nameConnection.Sample_Number + "_P" + m_nameConnection.Position_Number +
                                            "_C" + m_nameConnection.Cam_Num + "_L" + m_nameConnection.Light_Num + "_S" + m_nameConnection.SampleID + "_" + m_nameConnection.Time_Now;
                                        m_stSaveParamConnection[1].enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                                        if (!AccCheck && !AccCheckA)
                                        {
                                            m_stSaveParamConnection[1].nQuality = 85;//存Jpeg时有效
                                            m_stSaveParamConnection[1].pImagePath = Save_file_tmp + ".jpg";
                                            int nRet1 = m_listMvCameras[5].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamConnection[1]);
                                            if (nRet1 != MyCamera.MV_OK)
                                            {
                                                int nRet2 = m_listMvCameras[5].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamConnection[1]);
                                                LogHelper.LogError("钢片存图第一次没存上");
                                            }
                                            if (m_sQrCodeConnection != "Nc0000000000000000000000")
                                            {
                                                m_arrPositionConnectFilnames1.Add(Save_file_tmp + ".jpg");
                                            }
                                        }
                                        else
                                        {
                                            m_stSaveParamConnection[1].nQuality = 98;//存Jpeg时有效
                                            if (AccCheck)
                                            {

                                                m_stSaveParamConnection[1].pImagePath = Save_file + ".jpg";
                                                int nRet1 = m_listMvCameras[5].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamConnection[1]);
                                            }
                                            else
                                            {
                                                m_stSaveParamConnection[1].pImagePath = Save_file1 + ".jpg";
                                                int nRet1 = m_listMvCameras[5].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamConnection[1]);
                                            }


                                        }



                                        m_iSampleIdConnection_num[0]++;

                                    }
                                    catch (Exception ex)
                                    {

                                        System.Windows.MessageBox.Show(ex.ToString());
                                    }
                                }
                                break;
                            //正面  watch连接器
                            case 2:
                                lock (m_lockBufForSaveConnectionImageLock)
                                {

                                    //  Thread.Sleep(150);
                                    m_stSaveParamConnection[nCamIndex].pData = m_pSaveImageBuf_Connection[nCamIndex, m_nMemoryindex];
                                    m_nameConnection.Time_Now = DateTime.Now.ToString("yyyyMMddHHmmss");
                                    try
                                    {


                                        m_nameConnection.SampleID = m_iSampleIdConnection_num[1].ToString("000000");

                                        if (QrcodeConnectPRead.Count > 0)
                                        {
                                            //if (QrcodeConnectPRead.First()=="")
                                            //{

                                            //    QrcodeConnectPRead.RemoveAt(0);
                                            //    m_sQrCodeConnection = QrcodeConnectPRead.First();
                                            //    QrcodeConnectPRead.RemoveAt(0);
                                            //}
                                            //else
                                            //{
                                            m_sQrCodeConnection = QrcodeConnectPRead.First();
                                            QrcodeConnectPRead.RemoveAt(0);
                                            //}

                                        }

                                        else
                                        {
                                            m_sQrCodeConnection = "Nc0000000000000000000001";
                                        }
                                        m_nameConnection.Position_Number = (7).ToString("00");
                                        m_nameConnection.Light_Num = "0";
                                        m_nameConnection.Cam_Num = (7).ToString("00");

                                        m_nameConnection.Sample_Number = m_sQrCodeConnection;
                                        string Save_file = SaveCheckPath6 + "connection2" + m_nameConnection.Time_Now;
                                        string Save_file1 = SaveCheckPath6_1 + "connection2" + m_nameConnection.Time_Now;
                                        string Save_file_tmp = ConnectionFilePath + m_Job_name + "_" + m_nameConnection.Sample_Number + "_P" + m_nameConnection.Position_Number +
                                            "_C" + m_nameConnection.Cam_Num + "_L" + m_nameConnection.Light_Num + "_S" + m_nameConnection.SampleID + "_" + m_nameConnection.Time_Now;
                                        m_stSaveParamConnection[2].enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                                        if (!AccCheck && !AccCheckA)
                                        {
                                            m_stSaveParamConnection[2].pImagePath = Save_file_tmp + ".jpg";

                                        }
                                        else
                                        {
                                            if (AccCheck)
                                            {
                                                m_stSaveParamConnection[2].pImagePath = Save_file + ".jpg";
                                            }
                                            else
                                            {
                                                m_stSaveParamConnection[2].pImagePath = Save_file1 + ".jpg";
                                            }

                                        }
                                        //  m_stSaveParamConnection[2].pImagePath = Save_file_tmp + ".jpg";
                                        m_stSaveParamConnection[2].nQuality = 85;//存Jpeg时有效
                                        int nRet1 = m_listMvCameras[7].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamConnection[2]);
                                        if (nRet1 != MyCamera.MV_OK)
                                        {
                                            int nRet2 = m_listMvCameras[7].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamConnection[2]);
                                            LogHelper.LogError("连接器第一次存图存不上");
                                        }
                                        m_iSampleIdConnection_num[1]++;
                                        if (!AccCheck && !AccCheckA)
                                        {
                                            if (m_sQrCodeConnection != "Nc0000000000000000000001")
                                            {
                                                m_arrPositionConnectFilnames.Add(Save_file_tmp + ".jpg");
                                            }

                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                        System.Windows.MessageBox.Show(ex.ToString());
                                    }
                                }
                                break;


                        }
                        //保存图像


                        //string saveThumbnailFilePath = m_sThumbnailConnectionSaveFilePath + m_Job_name + "_" + m_nameConnection.Sample_Number + "_P" + m_nameConnection.Position_Number + "_C" + m_nameConnection.Cam_Num + "_L" + m_nameConnection.Light_Num + "_S" + m_nameConnection.SampleID + "_" + m_nameConnection.Time_Now;
                        //m_stSaveParamConnection[nCamIndex].pImagePath = saveThumbnailFilePath + ".jpg";
                        //m_stSaveParamConnection[nCamIndex].nQuality = 80;//存Jpeg时有效
                        //int nRet2 = m_listMvCameras[9].m_MyCamera.MV_CC_SaveImageToFile_NET(ref m_stSaveParamConnection[nCamIndex]);
                    }

                }
                //}
                Thread.Sleep(5);
            }
        }

        public void SendHeatBeat()
        {
            OperateResult opr = MotorsClass.omronInstance.ConnectServer();
            while (m_bFlagHeartBeat)
            {
                MotorsClass.omronInstance.Write(plc_HeatBeat_status, (float)1);
                Thread.Sleep(200);
                if (!opr.IsSuccess)
                {
                    LogHelper.LogInfo("连接断开！");
                }

            }

        }
        /// <summary>
        /// 停机警告
        /// </summary>
        public void Alarm()
        {
            LogHelper.LogError("报警停机!");
            m_bRunSaveImage = false;
            m_bRunSaveImage_Connection = false;
            m_bRunWhileIsSendReasoningMachine = false;
            m_bRunWhileSeparateToWholeNames = false;
            m_bRunWhileSeparateToWholeNames1 = false;
            m_bRunWhileSeparateToWholeNames2 = false;
            m_bRunSaveQrcode = false;
            m_bRunSaveQrcode1 = false;
            //for (int i = 0; i < m_nCanOpenDeviceNum; ++i)
            //{
            //    int nRet;
            //    nRet = m_pMyCamera[i].MV_CC_CloseDevice_NET();
            //    if (MyCamera.MV_OK != nRet)
            //    {
            //        return;
            //    }

            //    nRet = m_pMyCamera[i].MV_CC_DestroyDevice_NET();
            //    if (MyCamera.MV_OK != nRet)
            //    {
            //        return;
            //    }
            //}
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
            int PoseCount = 0;
            while (!m_bWhileIsSendReasoningMachine)
            {
                while (!IsCloseInfer)
                {
                    if (m_bRunWhileIsSendReasoningMachine)
                    {
                        try
                        {
                            //和推理机通讯
                            if (m_bConnectionServer == true)
                            {
                                if (m_bCheckConnected)
                                {

                                    bHasData = false;
                                    lock (m_SendReasoningMachineLock)
                                    {

                                        if (Pose_result1.Count > 0 && Pose_result2.Count > 0 && Pose_result3.Count > 0 && Pose_result4.Count > 0 && Pose_result5.Count > 0 && Pose_result6.Count > 0 && Pose_result7.Count > 0)
                                        {
                                            bHasData = true;
                                        }
                                        if (bHasData)
                                        {

                                            m_arrictAscr2.pose_result.Add(Pose_result1.First());
                                            Pose_result1.RemoveAt(0);
                                            m_arrictAscr2.pose_result.Add(Pose_result2.First());
                                            Pose_result2.RemoveAt(0);
                                            m_arrictAscr2.pose_result.Add(Pose_result3.First());
                                            Pose_result3.RemoveAt(0);
                                            m_arrictAscr2.pose_result.Add(Pose_result4.First());
                                            Pose_result4.RemoveAt(0);
                                            m_arrictAscr2.pose_result.Add(Pose_result5.First());
                                            Pose_result5.RemoveAt(0);
                                            m_arrictAscr2.pose_result.Add(Pose_result6.First());
                                            Pose_result6.RemoveAt(0);
                                            m_arrictAscr2.pose_result.Add(Pose_result7.First());
                                            Pose_result7.RemoveAt(0);

                                            m_arrictAscr2.sample_id = m_listSampleIDs[0];
                                            m_listSampleIDs.RemoveAt(0);
                                            Thread.Sleep(15);
                                            ResultFlag++;
                                            Stopwatch st1 = new Stopwatch();
                                            st1.Start();
                                            m_ayySrdn2 = hq.CommitSample2(Client_IP, Cilent_Port, m_arrictAscr2);
                                            st1.Stop();
                                            if (m_ayySrdn2 != null)
                                            {
                                                // LogHelper.LogInfo("推理机第二次返回结果时间" + st1.ElapsedMilliseconds.ToString());
                                                m_ProductionResultId.Add(m_ayySrdn2.data.sample_id);
                                                m_ProductionResultIdToMoveFile.Add(m_ayySrdn2.data.sample_id);
                                                m_ProductionResultcode.Add(m_ayySrdn2.data.code);
                                                m_ProductionResultcodeToMoveFile.Add(m_ayySrdn2.data.code);
                                                ProductionDataResultCodeToExl.Add(m_ayySrdn2.data.code);
                                                ProductionDataIdToExl.Add(m_ayySrdn2.data.sample_id);
                                                ProductionDataResultScoreToExl.Add(m_ayySrdn2.data.score);
                                                if (m_ayySrdn2.data.sample_id!=null)
                                                {
                                                    iws.Cell(ResultFlag, 1).Value = m_ayySrdn2.data.sample_id;
                                                }
                                                
                                                if (m_ayySrdn2.data.code != "OK")
                                                {
                                                    iws.Cell(ResultFlag, 2).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                                                }
                                                if (m_ayySrdn2.data.code!=null)
                                                {
                                                    iws.Cell(ResultFlag, 2).Value = m_ayySrdn2.data.code;
                                                    iws.Cell(ResultFlag, 3).Value = m_ayySrdn2.data.score;
                                                }
                                                if (FixtureNumber.Count>0)
                                                {
                                                    iws.Cell(ResultFlag, 4).Value = FixtureNumber.First();
                                                }                                              
                                                G_w.Save();


                                                event_ResultDisplay(m_ayySrdn2.data.sample_id, m_ayySrdn2.data.code);
                                                // m_listSampleIDs.Clear();
                                                totalNumberSamples++;
                                                //显示总数
                                                event_LabelDisplay(totalNumberSamples, m_ayySrdn2.code);


                                                //显示结果到列表
                                                event_ProcessSampleResultData2_NVT(m_ayySrdn2);
                                                m_arrictAscr2.pose_result.Clear();
                                                FixtureNumber.RemoveAt(0);
                                                // m_arrictAscr2.pose_result.RemoveAt(0);

                                                // }
                                            }

                                        }


                                    }

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
                Thread.Sleep(5);
            }

        }

        public void SenTheResultToPlc()
        {
            while (m_bWhileIsSendReasoningResultToPlc)
            {
                //AI推理全部开启
                if (!IsCloseInfer && IsOpenConectedServer)
                {  //奇数清料
                    GetQrCode(2);
                    if (GetResulteAQrCode().Count() > 5 || GetResulteBQrCode().Count() > 5)
                    {
                        #region 复检
                        // m_ReCheckIdNumberTempA.Add("111");
                        // if (!m_ReCheckIdNumberTempA.Contains(GetResulteAQrCode()))
                        // {
                        //     m_ReCheckIdNumberTempA.Add(GetResulteAQrCode());
                        //     m_ReCheckIdNumberFalgA.Add(GetResulteAQrCode());
                        // }
                        //// 二维码复检交互
                        //  if (m_ReCheckIdNumber.Contains(m_ReCheckIdNumberFalgA.First()) )
                        // {
                        //     if (ReCheckFalgA==1)
                        //     {
                        //         MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)2);
                        //         if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)2).IsSuccess)
                        //         {
                        //             LogHelper.LogInfo("复检二维码写入plc完成！");
                        //         }
                        //         ReCheckFalgA++;
                        //     }

                        // }
                        // else if (m_ReCheckIdNumber.Contains(GetResulteBQrCode()))
                        // {
                        //     MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)2);
                        //     if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)2).IsSuccess)
                        //     {
                        //         LogHelper.LogInfo("复检二维码写入plc完成！");
                        //     }
                        // }
                        // else
                        // {
                        //     MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)1);
                        //     if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)1).IsSuccess)
                        //     {
                        //         LogHelper.LogInfo("正常二维码写入plc完成！");
                        //     }
                        //     MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)1);
                        //     if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)1).IsSuccess)
                        //     {
                        //         LogHelper.LogInfo("正常二维码写入plc完成！");
                        //     }
                        // }
                        #endregion
                        if (m_ProductionResultId.Count > 0 && m_ConncetionResultId.Count > 0 && m_Conncetion1ResultId.Count > 0)
                        {
                            if (m_ProductionResultId.First() == GetResulteBQrCode() && m_ConncetionResultId.First() == GetResulteBQrCode() && m_Conncetion1ResultId.First() == GetResulteBQrCode())
                            {
                                TheLasterCount++;
                                event_LabelTheLastResultDisplay(TheLasterCount, 0);
                                if (m_ConncetionResultcode.First() != "OK")
                                {
                                    TheLasterNgCount++;
                                    if (!m_ReCheckIdNumber.Contains(m_ProductionResultId.First()))
                                    {
                                        m_ReCheckIdNumber.Add(m_ProductionResultId.First());
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)1);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)1).IsSuccess)
                                        {
                                            LogHelper.LogInfo("正常二维码写入plc完成！");
                                        }
                                    }
                                    else
                                    {
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)2);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)2).IsSuccess)
                                        {
                                            LogHelper.LogInfo("复检二维码写入plc完成！");
                                        }
                                    }
                                    event_LabelTheLastResultDisplay(TheLasterNgCount, 1);
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)2);
                                    if (!MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)2).IsSuccess)
                                    {
                                        LogHelper.LogInfo(m_ConncetionResultId.First() + "汇总--连接器结果B写入给PLC失败");
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)2);
                                    }
                                    else
                                    {
                                        LogHelper.LogInfo(m_ConncetionResultId.First() + "汇总--连接器NG结果B写入给PLC成功");
                                    }
                                }
                                else if (m_Conncetion1Resultcode.First() != "OK")
                                {
                                    TheLasterNgCount++;
                                    if (!m_ReCheckIdNumber.Contains(m_ProductionResultId.First()))
                                    {
                                        m_ReCheckIdNumber.Add(m_ProductionResultId.First());
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)1);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)1).IsSuccess)
                                        {
                                            LogHelper.LogInfo("正常二维码写入plc完成！");
                                        }
                                    }
                                    else
                                    {
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)2);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)2).IsSuccess)
                                        {
                                            LogHelper.LogInfo("复检二维码写入plc完成！");
                                        }
                                    }
                                    event_LabelTheLastResultDisplay(TheLasterNgCount, 1);
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)2);
                                    if (!MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)2).IsSuccess)
                                    {
                                        LogHelper.LogInfo(m_Conncetion1ResultId.First() + "汇总--连接器钢片结果B写入给PLC失败");
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)2);
                                    }
                                    else
                                    {
                                        LogHelper.LogInfo(m_Conncetion1ResultId.First() + "汇总--连接器钢片NG结果B写入给PLC成功");
                                    }
                                }
                                else if (m_ProductionResultcode.First() != "OK")
                                {
                                    TheLasterNgCount++;
                                    if (!m_ReCheckIdNumber.Contains(m_ProductionResultId.First()))
                                    {
                                        m_ReCheckIdNumber.Add(m_ProductionResultId.First());
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)1);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)1).IsSuccess)
                                        {
                                            LogHelper.LogInfo("正常二维码写入plc完成！");
                                        }
                                    }
                                    else
                                    {
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)2);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)2).IsSuccess)
                                        {
                                            LogHelper.LogInfo("复检二维码写入plc完成！");
                                        }
                                    }
                                    event_LabelTheLastResultDisplay(TheLasterNgCount, 1);
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)2);
                                    if (!MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)2).IsSuccess)
                                    {
                                        LogHelper.LogInfo(m_ProductionResultId.First() + "汇总--本体结果B写入给PLC失败");
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)2);
                                    }
                                    else
                                    {
                                        LogHelper.LogInfo(m_ProductionResultId.First() + "汇总--本体NG结果B写入给PLC成功");
                                    }
                                    //mes上传
                                    //MesHelper.StrBarcode = m_ProductionResultId.First();
                                    //MesHelper.IntTestResult = 0;
                                    //MesHelper.StrErrCode = m_ProductionResultcode.First();
                                    //bool flag = MesHelper.BlnSaveProcessData(MesHelper);
                                    //if (!flag)
                                    //{
                                    //    LogHelper.LogError(MesHelper.StrErrMsg);
                                    //}
                                }
                                else
                                {
                                    TheLasterOkCount++;
                                    if (!m_ReCheckIdNumber.Contains(m_ProductionResultId.First()))
                                    {
                                        m_ReCheckIdNumber.Add(m_ProductionResultId.First());
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)1);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)1).IsSuccess)
                                        {
                                            LogHelper.LogInfo("正常二维码写入plc完成！");
                                        }
                                    }
                                    else
                                    {
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)2);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDB, (float)2).IsSuccess)
                                        {
                                            LogHelper.LogInfo("复检二维码写入plc完成！");
                                        }
                                    }
                                    event_LabelTheLastResultDisplay(TheLasterOkCount, 2);
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)1);
                                    if (!MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)1).IsSuccess)
                                    {
                                        LogHelper.LogInfo(m_ProductionResultId.First() + "汇总--结果B写入给PLC失败");
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)1);
                                    }
                                    else
                                    {
                                        LogHelper.LogInfo(m_ProductionResultId.First() + "汇总--OK结果B写入给PLC成功");
                                    }
                                    //mes上传
                                    //MesHelper.StrBarcode = m_ProductionResultId.First();
                                    //MesHelper.IntTestResult = 1;
                                    //MesHelper.StrErrCode = "";
                                    //bool flag = MesHelper.BlnSaveProcessData(MesHelper);
                                    //if (!flag)
                                    //{
                                    //    LogHelper.LogError(MesHelper.StrErrMsg);
                                    //}
                                }
                                m_ProductionResultId.RemoveAt(0);
                                m_ProductionResultcode.RemoveAt(0);
                                m_ConncetionResultId.RemoveAt(0);
                                m_ConncetionResultcode.RemoveAt(0);
                                m_Conncetion1ResultId.RemoveAt(0);
                                m_Conncetion1Resultcode.RemoveAt(0);
                            }
                            else if (m_ProductionResultId.First() == GetResulteAQrCode() && m_ConncetionResultId.First() == GetResulteAQrCode() && m_Conncetion1ResultId.First() == GetResulteAQrCode())
                            {
                                TheLasterCount++;
                                event_LabelTheLastResultDisplay(TheLasterCount, 0);
                                if (m_ConncetionResultcode.First() != "OK")
                                {
                                    TheLasterNgCount++;
                                    if (!m_ReCheckIdNumber.Contains(m_ProductionResultId.First()))
                                    {
                                        m_ReCheckIdNumber.Add(m_ProductionResultId.First());
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)1);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)1).IsSuccess)
                                        {
                                            LogHelper.LogInfo("正常二维码写入plc完成！");
                                        }
                                    }
                                    else
                                    {
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)2);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)2).IsSuccess)
                                        {
                                            LogHelper.LogInfo("复检二维码写入plc完成！");
                                        }
                                    }
                                    event_LabelTheLastResultDisplay(TheLasterNgCount, 1);
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)2);
                                    if (!MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)2).IsSuccess)
                                    {
                                        LogHelper.LogInfo(m_ConncetionResultId.First() + "汇总--连接器结果A写入给PLC失败");
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)2);
                                    }
                                    else
                                    {
                                        LogHelper.LogInfo(m_ConncetionResultId.First() + "汇总--连接器NG结果A写入给PLC成功");
                                    }

                                }
                                else if (m_Conncetion1Resultcode.First() != "OK")
                                {
                                    TheLasterNgCount++;
                                    if (!m_ReCheckIdNumber.Contains(m_ProductionResultId.First()))
                                    {
                                        m_ReCheckIdNumber.Add(m_ProductionResultId.First());
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)1);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)1).IsSuccess)
                                        {
                                            LogHelper.LogInfo("正常二维码写入plc完成！");
                                        }
                                    }
                                    else
                                    {
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)2);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)2).IsSuccess)
                                        {
                                            LogHelper.LogInfo("复检二维码写入plc完成！");
                                        }
                                    }
                                    event_LabelTheLastResultDisplay(TheLasterNgCount, 1);
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)2);
                                    if (!MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)2).IsSuccess)
                                    {
                                        LogHelper.LogInfo(m_Conncetion1ResultId.First() + "汇总--连接器钢片结果A写入给PLC失败");
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)2);
                                    }
                                    else
                                    {
                                        LogHelper.LogInfo(m_Conncetion1ResultId.First() + "汇总--连接器钢片NG结果A写入给PLC成功");
                                    }
                                }
                                else if (m_ProductionResultcode.First() != "OK")
                                {
                                    TheLasterNgCount++;
                                    if (!m_ReCheckIdNumber.Contains(m_ProductionResultId.First()))
                                    {
                                        m_ReCheckIdNumber.Add(m_ProductionResultId.First());
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)1);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)1).IsSuccess)
                                        {
                                            LogHelper.LogInfo("正常二维码写入plc完成！");
                                        }
                                    }
                                    else
                                    {
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)2);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)2).IsSuccess)
                                        {
                                            LogHelper.LogInfo("复检二维码写入plc完成！");
                                        }
                                    }
                                    event_LabelTheLastResultDisplay(TheLasterNgCount, 1);
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)2);
                                    if (!MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)2).IsSuccess)
                                    {
                                        LogHelper.LogInfo(m_ProductionResultId.First() + "汇总--本体结果A写入给PLC失败");
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)2);
                                    }
                                    else
                                    {
                                        LogHelper.LogInfo(m_ProductionResultId.First() + "汇总--本体NG结果A写入给PLC成功");
                                    }
                                    //mes上传
                                    //MesHelper.StrBarcode = m_ProductionResultId.First();
                                    //MesHelper.IntTestResult = 0;
                                    //MesHelper.StrErrCode = m_ProductionResultcode.First();
                                    //bool flag = MesHelper.BlnSaveProcessData(MesHelper);
                                    //if (!flag)
                                    //{
                                    //    LogHelper.LogError(MesHelper.StrErrMsg);
                                    //}
                                }
                                else
                                {
                                    TheLasterOkCount++;
                                    if (!m_ReCheckIdNumber.Contains(m_ProductionResultId.First()))
                                    {
                                        m_ReCheckIdNumber.Add(m_ProductionResultId.First());
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)1);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)1).IsSuccess)
                                        {
                                            LogHelper.LogInfo("正常二维码写入plc完成！");
                                        }
                                    }
                                    else
                                    {
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)2);
                                        if (MotorsClass.omronInstance.Write(MotorsClass.plc_motor_RecheckIDA, (float)2).IsSuccess)
                                        {
                                            LogHelper.LogInfo("复检二维码写入plc完成！");
                                        }
                                    }
                                    event_LabelTheLastResultDisplay(TheLasterOkCount, 2);
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)1);
                                    if (!MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)1).IsSuccess)
                                    {
                                        LogHelper.LogInfo(m_ProductionResultId.First() + "汇总--结果A写入给PLC失败");
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)1);
                                    }
                                    else
                                    {
                                        LogHelper.LogInfo(m_ProductionResultId.First() + "汇总--OK结果A写入给PLC成功");
                                    }
                                    //mes上传
                                    //MesHelper.StrBarcode = m_ProductionResultId.First();
                                    //MesHelper.IntTestResult = 1;
                                    //MesHelper.StrErrCode = "";
                                    //bool flag = MesHelper.BlnSaveProcessData(MesHelper);
                                    //if (!flag)
                                    //{
                                    //    LogHelper.LogError(MesHelper.StrErrMsg);
                                    //}
                                }
                                m_ProductionResultId.RemoveAt(0);
                                m_ProductionResultcode.RemoveAt(0);
                                m_ConncetionResultId.RemoveAt(0);
                                m_ConncetionResultcode.RemoveAt(0);
                                m_Conncetion1ResultId.RemoveAt(0);
                                m_Conncetion1Resultcode.RemoveAt(0);
                            }
                            //else if(GetResulteAQrCode().Count() > 5 && GetResulteBQrCode().Count() > 5)
                            //{

                            //    LogHelper.LogError("下料连接器二维码：  " + m_ConncetionResultId.First());
                            //    LogHelper.LogError("下料钢片二维码：  " + m_Conncetion1ResultId.First());
                            //    LogHelper.LogError("下料本体二维码：  " + m_ProductionResultId.First());
                            //    LogHelper.LogError("下料A二维码：  " + GetResulteAQrCode());
                            //    LogHelper.LogError("下料B二维码：  " + GetResulteBQrCode());

                            //    Alarm();
                            //}


                        }
                    }
                }
                //本体AI开启
                else if (!IsCloseInfer && !IsOpenConectedServer)
                {
                    if (GetResulteAQrCode().Count() > 5 || GetResulteBQrCode().Count() > 5)
                    {
                        if (m_ProductionResultId.Count > 0)
                        {
                            if (m_ProductionResultId.First() == GetResulteBQrCode())
                            {

                                if (m_ProductionResultcode.First() != "OK")
                                {
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)2);
                                    if (!MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)2).IsSuccess)
                                    {
                                        LogHelper.LogInfo(m_ProductionResultId.First() + "--本体结果B写入给PLC失败");
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)2);
                                    }
                                    else
                                    {
                                        LogHelper.LogInfo(m_ProductionResultId.First() + "--本体NG结果B写入给PLC成功");
                                    }
                                    //mes上传
                                    //MesHelper.StrBarcode = m_ProductionResultId.First();
                                    //MesHelper.IntTestResult = 0;
                                    //MesHelper.StrErrCode = m_ProductionResultcode.First();
                                    //bool flag = MesHelper.BlnSaveProcessData(MesHelper);
                                    //if (!flag)
                                    //{
                                    //    LogHelper.LogError(MesHelper.StrErrMsg);
                                    //}
                                }
                                else
                                {
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)1);
                                    if (!MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)1).IsSuccess)
                                    {
                                        LogHelper.LogInfo(m_ProductionResultId.First() + "--本体结果B写入给PLC失败");
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)1);
                                    }
                                    else
                                    {
                                        LogHelper.LogInfo(m_ProductionResultId.First() + "--本体OK结果B写入给PLC成功");
                                    }
                                    //mes上传
                                    //MesHelper.StrBarcode = m_ProductionResultId.First();
                                    //MesHelper.IntTestResult = 1;
                                    //MesHelper.StrErrCode = "";
                                    //bool flag = MesHelper.BlnSaveProcessData(MesHelper);
                                    //if (!flag)
                                    //{
                                    //    LogHelper.LogError(MesHelper.StrErrMsg);
                                    //}
                                }
                                m_ProductionResultId.RemoveAt(0);
                                m_ProductionResultcode.RemoveAt(0);
                                m_ConncetionResultId.RemoveAt(0);
                                m_Conncetion1ResultId.RemoveAt(0);
                            }
                            else if (m_ProductionResultId.First() == GetResulteAQrCode())
                            {

                                if (m_ProductionResultcode.First() != "OK")
                                {
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)2);
                                    if (!MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)2).IsSuccess)
                                    {
                                        LogHelper.LogInfo(m_ProductionResultId.First() + "--本体结果A写入给PLC失败");
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)2);
                                    }
                                    else
                                    {
                                        LogHelper.LogInfo(m_ProductionResultId.First() + "--本体NG结果A写入给PLC成功");
                                    }
                                    //mes上传
                                    //MesHelper.StrBarcode = m_ProductionResultId.First();
                                    //MesHelper.IntTestResult = 0;
                                    //MesHelper.StrErrCode = m_ProductionResultcode.First();
                                    //bool flag = MesHelper.BlnSaveProcessData(MesHelper);
                                    //if (!flag)
                                    //{
                                    //    LogHelper.LogError(MesHelper.StrErrMsg);
                                    //}
                                }
                                else
                                {
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)1);
                                    if (!MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)1).IsSuccess)
                                    {
                                        LogHelper.LogInfo(m_ProductionResultId.First() + "--本体结果A写入给PLC失败");
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)1);
                                    }
                                    else
                                    {
                                        LogHelper.LogInfo(m_ProductionResultId.First() + "--本体OK结果A写入给PLC成功");
                                    }
                                    //mes上传
                                    //MesHelper.StrBarcode = m_ProductionResultId.First();
                                    //MesHelper.IntTestResult = 1;
                                    //MesHelper.StrErrCode = "";
                                    //bool flag = MesHelper.BlnSaveProcessData(MesHelper);
                                    //if (!flag)
                                    //{
                                    //    LogHelper.LogError(MesHelper.StrErrMsg);
                                    //}
                                }
                                m_ProductionResultId.RemoveAt(0);
                                m_ProductionResultcode.RemoveAt(0);
                                m_ConncetionResultId.RemoveAt(0);
                                m_Conncetion1ResultId.RemoveAt(0);
                            }


                        }
                    }
                }
                //连接器AI开启
                else if (IsCloseInfer && IsOpenConectedServer)
                {
                    if (GetResulteAQrCode().Count() > 5 || GetResulteBQrCode().Count() > 5)
                    {
                        if (m_ConncetionResultId.Count > 0 && m_Conncetion1ResultId.Count > 0)
                        {
                            if (m_ConncetionResultId.First() == GetResulteBQrCode() && m_Conncetion1ResultId.First() == GetResulteBQrCode())
                            {
                                if (m_ConncetionResultcode.First() != "OK")
                                {
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)2);
                                    if (!MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)2).IsSuccess)
                                    {
                                        LogHelper.LogInfo(m_ConncetionResultId.First() + "--连接器结果B写入给PLC失败");
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)2);
                                    }
                                    else
                                    {
                                        LogHelper.LogInfo(m_ConncetionResultId.First() + "--连接器NG结果B写入给PLC成功");
                                    }
                                }

                                //mes上传
                                //MesHelper.StrBarcode = m_ProductionResultId.First();
                                //MesHelper.IntTestResult = 0;
                                //MesHelper.StrErrCode = m_ProductionResultcode.First();
                                //bool flag = MesHelper.BlnSaveProcessData(MesHelper);
                                //if (!flag)
                                //{
                                //    LogHelper.LogError(MesHelper.StrErrMsg);
                                //}
                                else if (m_Conncetion1Resultcode.First() != "OK")
                                {
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)2);
                                    if (!MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)2).IsSuccess)
                                    {
                                        LogHelper.LogInfo(m_Conncetion1ResultId.First() + "--连接器钢片结果B写入给PLC失败");
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)2);
                                    }
                                    else
                                    {
                                        LogHelper.LogInfo(m_Conncetion1ResultId.First() + "--连接器钢片NG结果B写入给PLC成功");
                                    }
                                }
                                else
                                {
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)1);
                                    if (!MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)1).IsSuccess)
                                    {
                                        LogHelper.LogInfo(m_ConncetionResultId.First() + "--连接器结果B写入给PLC失败");
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultB, (float)1);
                                    }
                                    else
                                    {
                                        LogHelper.LogInfo(m_ConncetionResultId.First() + "--连接器OK结果B写入给PLC成功");
                                    }
                                    //mes上传
                                    //MesHelper.StrBarcode = m_ProductionResultId.First();
                                    //MesHelper.IntTestResult = 1;
                                    //MesHelper.StrErrCode = "";
                                    //bool flag = MesHelper.BlnSaveProcessData(MesHelper);
                                    //if (!flag)
                                    //{
                                    //    LogHelper.LogError(MesHelper.StrErrMsg);
                                    //}
                                }
                                //  m_ProductionResultId.RemoveAt(0);
                                // m_ProductionResultcode.RemoveAt(0);
                                m_ConncetionResultId.RemoveAt(0);
                                m_ConncetionResultcode.RemoveAt(0);
                                m_Conncetion1ResultId.RemoveAt(0);
                                m_Conncetion1Resultcode.RemoveAt(0);
                            }
                            else if (m_ConncetionResultId.First() == GetResulteAQrCode() && m_Conncetion1ResultId.First() == GetResulteAQrCode())
                            {
                                if (m_ConncetionResultcode.First() != "OK")
                                {
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)2);
                                    if (!MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)2).IsSuccess)
                                    {
                                        LogHelper.LogInfo(m_ConncetionResultId.First() + "--连接器结果A写入给PLC失败");
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)2);
                                    }
                                    else
                                    {
                                        LogHelper.LogInfo(m_ConncetionResultId.First() + "--连接器NG结果A写入给PLC成功");
                                    }

                                }

                                //mes上传
                                //MesHelper.StrBarcode = m_ProductionResultId.First();
                                //MesHelper.IntTestResult = 0;
                                //MesHelper.StrErrCode = m_ProductionResultcode.First();
                                //bool flag = MesHelper.BlnSaveProcessData(MesHelper);
                                //if (!flag)
                                //{
                                //    LogHelper.LogError(MesHelper.StrErrMsg);
                                //}
                                else if (m_Conncetion1Resultcode.First() != "OK")
                                {
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)2);
                                    if (!MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)2).IsSuccess)
                                    {
                                        LogHelper.LogInfo(m_Conncetion1ResultId.First() + "--连接器钢片结果A写入给PLC失败");
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)2);
                                    }
                                    else
                                    {
                                        LogHelper.LogInfo(m_Conncetion1ResultId.First() + "--连接器钢片NG结果A写入给PLC成功");
                                    }

                                }
                                else
                                {
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)1);
                                    if (!MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)1).IsSuccess)
                                    {
                                        LogHelper.LogInfo(m_ConncetionResultId.First() + "--连接器结果A写入给PLC失败");
                                        MotorsClass.omronInstance.Write(MotorsClass.plc_motor_resultA, (float)1);
                                    }
                                    else
                                    {
                                        LogHelper.LogInfo(m_ConncetionResultId.First() + "--连接器OK结果A写入给PLC成功");
                                    }
                                    //mes上传
                                    //MesHelper.StrBarcode = m_ProductionResultId.First();
                                    //MesHelper.IntTestResult = 1;
                                    //MesHelper.StrErrCode = "";
                                    //bool flag = MesHelper.BlnSaveProcessData(MesHelper);
                                    //if (!flag)
                                    //{
                                    //    LogHelper.LogError(MesHelper.StrErrMsg);
                                    //}
                                }
                                // m_ProductionResultId.RemoveAt(0);
                                //  m_ProductionResultcode.RemoveAt(0);
                                m_ConncetionResultId.RemoveAt(0);
                                m_ConncetionResultcode.RemoveAt(0);
                                m_Conncetion1ResultId.RemoveAt(0);
                                m_Conncetion1Resultcode.RemoveAt(0);
                            }


                        }
                    }
                }

                Thread.Sleep(50);
            }

        }
        public void Show_image2Picbox(SampleResultData_NVT SRDN, PictureBox pic1, PictureBox pic2, PictureBox pic3, PictureBox pic4, PictureBox pic5, PictureBox pic6, PictureBox pic7, PictureBox pic_Show)
        {

            List<string> tmp = new List<string>();
            if (SRDN.data != null)
            {
                string[] PosFlaggetAry1 = SRDN.data.file_path.Split('_');//单个字符分割
                SRDN.data.result_path = SRDN.data.result_path.Substring(5, SRDN.data.result_path.Length - 5);
                switch (PosFlaggetAry1[3])
                {
                    case "P01":
                        if (pic1.InvokeRequired)
                        {
                            pic1.BeginInvoke(new MethodInvoker(() =>
                            {
                                pic1.Image = Image.FromFile(ServerSaveFilePath + SRDN.data.result_path);
                                Pic_Path1 = ServerSaveFilePath + SRDN.data.result_path;
                                LogHelper.LogInfo(Pic_Path1);
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


        }
        public void Show_Result2Table(SampleResultData2_NVT SRDN, DataGridView dataGridView1)
        {


            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.BeginInvoke(new MethodInvoker(() =>
                {
                    int index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = (index + 1).ToString();
                    dataGridView1.Rows[index].Cells[1].Value = SRDN.data.sample_id;
                    dataGridView1.Rows[index].Cells[3].Value = SRDN.data.code;
                    dataGridView1.Rows[index].Cells[4].Value = SRDN.data.score;                 
                    if (SRDN.data.code != "OK")
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
                    //if (index>2000)
                    //{
                    //    dataGridView1.Rows.Clear();

                    //}
                }));
            }
            else
            {
                dataGridView1.Hide();
            }

        }

        public void Show_ConnectResult2Table(List<string> resultid, string code, DataGridView dataGridView1)
        {


            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.BeginInvoke(new MethodInvoker(() =>
                {
                    int index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = (index + 1).ToString();
                    dataGridView1.Rows[index].Cells[1].Value = resultid.First();
                    dataGridView1.Rows[index].Cells[2].Value = code;


                    resultid.RemoveAt(0);


                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
                    //if (index > 2000)
                    //{
                    //    dataGridView1.Rows.Clear();

                    //}
                }));
               
            }
            else
            {
                dataGridView1.Hide();
            }

        }

        public void Show_Connect1Result2Table(List<string> resultid, string code, DataGridView dataGridView1)
        {


            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.BeginInvoke(new MethodInvoker(() =>
                {
                    int index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = (index + 1).ToString();
                    dataGridView1.Rows[index].Cells[1].Value = resultid.First();
                    dataGridView1.Rows[index].Cells[2].Value = code;


                    resultid.RemoveAt(0);


                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
                    //if (index > 2000)
                    //{
                    //    dataGridView1.Rows.Clear();

                    //}
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
        /*
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
        */

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
            try
            {
                //str_HardDiskName = str_HardDiskName + ":\\";
                System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
                foreach (System.IO.DriveInfo drive in drives)
                {
                    if (drive.Name == str_HardDiskName)
                    {
                        freeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024);
                    }
                }
            }
            catch (Exception)
            {

                return 0;
            }

            return freeSpace;
        }
        string FilePath = "E:\\NVTphoto";
        string FileConncet1Path = "E:\\NVTconnect1";
        string FileConncet2Path = "E:\\NVTconnect2";
        string fileDirect = "C:\\tc2\\data";
        string fileDirectconnect1 = "C:\\Connection\\data\\00";
        string fileDirectconnect2 = "C:\\Connection\\data\\01";
        //移动图片
        public void MoveFile()
        {
            while (m_bMoveFile)
            {
                string current_day = DateTime.Now.ToString("MM-dd");//读取当前时间的月份和日期，如:06-16
                string current_month = DateTime.Now.ToString("MM"); //读取当前时间的月份，如:06
                string current_year = DateTime.Now.Year.ToString(); //读取当前时间的年份，如:2022
                string year_Path = FilePath + "\\" + current_year;
                if (!Directory.Exists(year_Path))   //创建年文件夹
                {
                    Directory.CreateDirectory(year_Path);
                }
                string month_Path = year_Path + "\\" + current_month;
                if (!Directory.Exists(month_Path))//创建月文件夹
                {
                    Directory.CreateDirectory(month_Path);
                }
                string day_Path = month_Path + "\\" + current_day;
                if (!Directory.Exists(day_Path))    //创建日文件夹
                {
                    Directory.CreateDirectory(day_Path);
                }
                string TodayPath = day_Path;

                string NgPath = day_Path + "\\" + "NG";
                if (!Directory.Exists(NgPath))
                {
                    Directory.CreateDirectory(NgPath);
                }
                string OkPath = day_Path + "\\" + "OK";
                if (!Directory.Exists(OkPath))
                {
                    Directory.CreateDirectory(OkPath);
                }
                if (m_ProductionResultcodeToMoveFile.Count > 0)
                {
                    if (m_ProductionResultcodeToMoveFile.First() != "OK")
                    {
                        try
                        {
                            string SNPath = NgPath + "\\" + m_ProductionResultIdToMoveFile.First();
                            if (!Directory.Exists(SNPath))
                            {
                                Directory.CreateDirectory(SNPath);
                            }
                            string[] str;
                            lock (Directoryimagelock)
                            {
                                str = Directory.GetFiles(fileDirect);
                            }

                            foreach (var file in str)
                            {
                                string s = file.Remove(0, 11);
                                if (file.Contains(m_ProductionResultIdToMoveFile.First()))
                                {


                                    File.Move(file, SNPath + s);

                                }
                            }
                            m_ProductionResultcodeToMoveFile.RemoveAt(0);
                            m_ProductionResultIdToMoveFile.RemoveAt(0);
                        }
                        catch (Exception ex)
                        {

                            LogHelper.LogError("MoveFile"+ex.Message);
                        }


                    }
                    else
                    {
                        try
                        {
                            string SNPath1 = OkPath + "\\" + m_ProductionResultIdToMoveFile.First();
                            if (!Directory.Exists(SNPath1))
                            {
                                Directory.CreateDirectory(SNPath1);
                            }
                            string[] str1;
                            lock (Directoryimagelock)
                            {
                                str1 = Directory.GetFiles(fileDirect);
                            }
                            foreach (var file in str1)
                            {
                                string s = file.Remove(0, 11);
                                if (file.Contains(m_ProductionResultIdToMoveFile.First()))
                                {
                                    File.Move(file, SNPath1 + s);
                                }
                            }
                            m_ProductionResultcodeToMoveFile.RemoveAt(0);
                            m_ProductionResultIdToMoveFile.RemoveAt(0);
                        }
                        catch (Exception ex)
                        {

                            LogHelper.LogError("MoveFile"+ex.Message);
                        }

                    }
                    MoveResultFile(FileConncet1Path, fileDirectconnect1, ConnectProductionDataResultCodeToMoveFile, ConnectProductionDataResultIdToMoveFile);
                   MoveResultFile(FileConncet2Path, fileDirectconnect2, ConnectProductionDataResultCodeToMoveFile1, ConnectProductionDataResultIdToMoveFile1);
                }
               
                Thread.Sleep(50);

            }


        }

        public void MoveResultFile(string FilePath, string FileDirect, List<string> ListResultcode, List<string> ListResultId)
        {
            string current_day = DateTime.Now.ToString("MM-dd");//读取当前时间的月份和日期，如:06-16
            string current_month = DateTime.Now.ToString("MM"); //读取当前时间的月份，如:06
            string current_year = DateTime.Now.Year.ToString(); //读取当前时间的年份，如:2022
            string year_Path = FilePath + "\\" + current_year;
            if (!Directory.Exists(year_Path))   //创建年文件夹
            {
                Directory.CreateDirectory(year_Path);
            }
            string month_Path = year_Path + "\\" + current_month;
            if (!Directory.Exists(month_Path))//创建月文件夹
            {
                Directory.CreateDirectory(month_Path);
            }
            string day_Path = month_Path + "\\" + current_day;
            if (!Directory.Exists(day_Path))    //创建日文件夹
            {
                Directory.CreateDirectory(day_Path);
            }
            string TodayPath = day_Path;

            string NgPath = day_Path + "\\" + "NG";
            if (!Directory.Exists(NgPath))
            {
                Directory.CreateDirectory(NgPath);
            }
            string OkPath = day_Path + "\\" + "OK";
            if (!Directory.Exists(OkPath))
            {
                Directory.CreateDirectory(OkPath);
            }
            if (ListResultcode.Count > 0)
            {
                if (ListResultcode.First() != "OK")
                {
                    try
                    {
                        //string SNPath = NgPath + "\\" + ListResultId.First();
                        //if (!Directory.Exists(SNPath))
                        //{
                        //    Directory.CreateDirectory(SNPath);
                        //}
                        string[] str;
                        lock (Directoryimagelock)
                        {
                            str = Directory.GetFiles(FileDirect);
                        }

                        foreach (var file in str)
                        {
                            string s = file.Remove(0, 21);
                            if (file.Contains(ListResultId.First()))
                            {


                                File.Move(file, NgPath + s);

                            }
                        }
                        ListResultcode.RemoveAt(0);
                        ListResultId.RemoveAt(0);
                    }
                    catch (Exception ex)
                    {

                        LogHelper.LogError("MoveResultFile"+ex.Message);
                    }


                }
                else
                {
                    try
                    {
                        //string SNPath1 = OkPath + "\\" + ListResultId.First();
                        //if (!Directory.Exists(SNPath1))
                        //{
                        //    Directory.CreateDirectory(SNPath1);
                        //}
                        string[] str1;
                        lock (Directoryimagelock)
                        {
                            str1 = Directory.GetFiles(FileDirect);
                        }
                        foreach (var file in str1)
                        {
                            string s = file.Remove(0, 21);
                            if (file.Contains(ListResultId.First()))
                            {
                                File.Move(file, OkPath + s);
                            }
                        }
                        ListResultcode.RemoveAt(0);
                        ListResultId.RemoveAt(0);
                    }
                    catch (Exception ex)
                    {

                        LogHelper.LogError("MoveResultFile"+ex.Message);
                    }

                }
            }





        }


        public void WriteCVS()
        {
            while (m_bWhileCsvWrite)
            {
                if (m_bRunSaveQrcode)
                {
                    if (!abort)
                    {
                        if (!Loseimage)
                        {
                            MotorsClass.omronInstance.Write(plc_HeatBeat_status, (float)1);
                            if (!MotorsClass.omronInstance.Write(plc_HeatBeat_status, (float)1).IsSuccess)
                            {
                                MotorsClass.omronInstance.Write(plc_HeatBeat_status, (float)1);
                                if (!MotorsClass.omronInstance.Write(plc_HeatBeat_status, (float)1).IsSuccess)
                                {
                                    LogHelper.LogError("plc连接心跳断开！");
                                    System.Windows.MessageBox.Show("plc连接心跳断开！");
                                }
                            }
                            else
                            {
                               // LogHelper.LogInfo("plc连接心跳正常！");
                            }
                        }
                        else
                        {
                            MotorsClass.omronInstance.Write(plc_HeatBeat_status, (float)0);
                        }


                    }
                }
               
                if (MotorsClass.omronInstance.ReadFloat(MotorsClass.plc_MachineCheckState).Content == 1)
                {
                    string filepath = @"C:\Users\qabis.NVTPOWER\Desktop\设备点检表\";
                    string daytime = DateTime.Now.ToString("yyyyMMdd");
                    string fileName = filepath + daytime + ".csv";
                    if (!File.Exists(fileName)) //当文件不存在时创建文件
                    {
                        //创建文件流(创建文件)
                        FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                        //创建流写入对象，并绑定文件流
                        StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                        //实例化字符串流
                        StringBuilder sb = new StringBuilder();
                        //将数据添加进字符串流中（如果数据标题有变更，修改此处）
                        sb.Append("时间").Append(",").Append("设备外观5S").Append(",").Append("电路").Append(",").
                            Append("线路安全").Append(",").Append("设备气压（Mpa）").Append(",").
                            Append("开关按钮是否正常").Append(",").Append("设备安全").Append(",").
                            Append("机构是否正常").Append(",").Append("吸电池负压(Kpa)").Append(",").Append("总结果").Append(",");
                        //将字符串流数据写入文件
                        sw.WriteLine(sb);
                        //刷新文件流
                        sw.Flush();
                        sw.Close();
                        fs.Close();
                    }
                    //将数据写入文件

                    //实例化文件写入对象
                    StreamWriter swd = new StreamWriter(fileName, true, Encoding.Default);
                    StringBuilder sbd = new StringBuilder();
                    //将需要保存的数据添加到字符串流中
                    sbd.Append(DateTime.Now.ToString("yyyy:MM:dd:HH:mm:ss")).Append(",").Append("OK").Append(",").
                        Append("OK").Append(",").Append("OK").Append(",").Append(MotorsClass.omronInstance.ReadFloat(MotorsClass.plc_MachineCheck).Content.ToString()).Append(",").Append("OK").Append(",").
                        Append("OK").Append(",").Append("OK").Append(",").Append(MotorsClass.omronInstance.ReadFloat(MotorsClass.plc_MachineCheck1).Content.ToString("f2")).Append(",").Append("OK").Append(",");
                    swd.WriteLine(sbd);
                    swd.Flush();
                    swd.Close();
                    MotorsClass.omronInstance.Write(MotorsClass.plc_MachineCheckState, (float)0);
                    LogHelper.LogInfo("设备点检完成！");
                }
                Thread.Sleep(100);
            }



        }


        public void CreateXMlToExl()
        {
            while (m_bMoveFile)
            {
                string current_day = DateTime.Now.ToString("MM-dd");//读取当前时间的月份和日期，如:06-16
                string current_month = DateTime.Now.ToString("MM"); //读取当前时间的月份，如:06
                string current_year = DateTime.Now.Year.ToString(); //读取当前时间的年份，如:2022
                string year_Path = Application.StartupPath + "\\" + current_year;
                if (!Directory.Exists(year_Path))   //创建年文件夹
                {
                    Directory.CreateDirectory(year_Path);
                }
                string month_Path = year_Path + "\\" + current_month;
                if (!Directory.Exists(month_Path))//创建月文件夹
                {
                    Directory.CreateDirectory(month_Path);
                }
                string day_Path = month_Path + "\\" + current_day;
                string TodayPath = day_Path + "/test.txt";
                if (!Directory.Exists(day_Path))    //创建日文件夹
                {
                    Directory.CreateDirectory(day_Path);
                }
                if (DateTime.Now.Hour == 20)
                {
                    if (DateTime.Now.Minute == 0)
                    {
                        if (DateTime.Now.Second == 0)
                        {
                            ConnectTotal = 0;
                            ConnectNg = 0;
                            ConnectOk = 0;
                            ConnectTotal1 = 0;
                            ConnectNg1 = 0;
                            ConnectOk1 = 0;
                            TheLasterCount = 0;
                            TheLasterOkCount = 0;
                            TheLasterNgCount = 0;
                            totalNumberSamples = 0;
                            ngSamples = 0;
                            okSamples = 0;

                            creattheexl.ProductionDataIdToExl = ProductionDataIdToExl;
                            creattheexl.ProductionDataResultScoreToExl = ProductionDataResultScoreToExl;
                            creattheexl.ProductionDataResultCodeToExl = ProductionDataResultCodeToExl;
                            creattheexl.ConnectProductionDataResultIdToXmlExl = ConnectProductionDataResultIdToXmlExl;
                            creattheexl.ConnectProductionDataResultCodeToXmlExl = ConnectProductionDataResultCodeToXmlExl;
                            creattheexl.ConnectProductionDataResultIdToXmlExl1 = ConnectProductionDataResultIdToXmlExl1;
                            creattheexl.ConnectProductionDataResultCodeToXmlExl1 = ConnectProductionDataResultCodeToXmlExl1;
                            creattheexl.FixtureNumberToXml = FixtureNumberToXml;


                            if (ProductionDataResultCodeToExl.Count > 0)
                            {
                                int numbercounts = ProductionDataResultCodeToExl.Count();
                                int numberconnectcounts = ConnectProductionDataResultIdToXmlExl.Count();
                                int numberconnectcouts2 = ConnectProductionDataResultCodeToXmlExl.Count();
                                int numberconnectcounts1 = ConnectProductionDataResultIdToXmlExl1.Count();
                                int numberconnectcouts3 = ConnectProductionDataResultCodeToXmlExl1.Count();
                                int numberfixturecount = FixtureNumberToXml.Count();
                                XmlHelper02.SerializeToXml(creattheexl, "每天数据统计", day_Path);
                                ProductionDataIdToExl.RemoveRange(0, numbercounts);
                                ProductionDataResultScoreToExl.RemoveRange(0, numbercounts);
                                ProductionDataResultCodeToExl.RemoveRange(0, numbercounts);
                                ConnectProductionDataResultIdToXmlExl.RemoveRange(0, numberconnectcounts);
                                ConnectProductionDataResultCodeToXmlExl.RemoveRange(0, numberconnectcouts2);
                                ConnectProductionDataResultIdToXmlExl1.RemoveRange(0, numberconnectcounts1);
                                ConnectProductionDataResultCodeToXmlExl1.RemoveRange(0, numberconnectcouts3);
                                FixtureNumberToXml.RemoveRange(0, numberfixturecount);

                            }
                            else
                            {
                                XmlHelper02.SerializeToXml(creattheexl, "每天数据统计", day_Path);
                            }


                            //}

                        }

                    }

                }



                Thread.Sleep(1000);

            }


        }



        string urlAddress = "http://127.0.0.1:5000/predict";
        public bool ConverTheImageToBase64AndSendServer(List<string> imagePaths, SourceParamLst source, out ResultParam result)
        {
            if (imagePaths.Count > 0)
            {
                var image_path1 = imagePaths.First();
                var image_data = File.ReadAllBytes(image_path1);
                var image_base64 = Convert.ToBase64String(image_data);
                source.image = image_base64;
                lock (CysServerlock)
                {
                    HttpWebRequest request = WebRequest.Create(urlAddress) as HttpWebRequest; //创建请求
                    CookieContainer cookieContainer = new CookieContainer();
                    request.CookieContainer = cookieContainer;
                    request.AllowAutoRedirect = true;
                    request.MaximumResponseHeadersLength = 1024;
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.Timeout = 5000;

                    string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(source);
                    byte[] jsonbyte = Encoding.UTF8.GetBytes(jsonstring);
                    Stream postStream = request.GetRequestStream();
                    postStream.Write(jsonbyte, 0, jsonbyte.Length);
                    postStream.Close();
                    //发送请求并获取相应回应数据
                    HttpWebResponse res;
                    try
                    {
                        res = (HttpWebResponse)request.GetResponse();
                    }
                    catch (WebException ex)
                    {
                        LogHelper.LogError("连接器反面" + image_path1 + "CysAI异常!" + ex.Message);
                        Alarm();
                        res = (HttpWebResponse)ex.Response;
                        result = null;
                        return false;
                    }
                    StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                    string content = sr.ReadToEnd(); //获得响应字符串0
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultParam>(content);
                    imagePaths.RemoveAt(0);
                    return true;
                }

            }
            result = null;
            return false;
        }

        public bool ConverTheConnect1ImageToBase64AndSendServer(List<string> imagePaths, SourceParamLst source, out ResultParam result)
        {
            if (imagePaths.Count > 0)
            {
                var image_path1 = imagePaths.First();
                var image_data = File.ReadAllBytes(image_path1);
                var image_base64 = Convert.ToBase64String(image_data);
                source.image = image_base64;
                lock (CysServerlock)
                {
                    HttpWebRequest request = WebRequest.Create(urlAddress) as HttpWebRequest; //创建请求
                    CookieContainer cookieContainer = new CookieContainer();
                    request.CookieContainer = cookieContainer;
                    request.AllowAutoRedirect = true;
                    request.MaximumResponseHeadersLength = 1024;
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.Timeout = 5000;

                    string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(source);
                    byte[] jsonbyte = Encoding.UTF8.GetBytes(jsonstring);
                    Stream postStream = request.GetRequestStream();
                    postStream.Write(jsonbyte, 0, jsonbyte.Length);
                    postStream.Close();
                    //发送请求并获取相应回应数据
                    HttpWebResponse res;
                    try
                    {
                        res = (HttpWebResponse)request.GetResponse();
                    }
                    catch (WebException ex)
                    {
                        LogHelper.LogError("连接器钢片" + image_path1 + "CysAI异常!" + ex.Message);
                        Alarm();
                        res = (HttpWebResponse)ex.Response;
                        result = null;
                        return false;
                    }
                    StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                    string content = sr.ReadToEnd(); //获得响应字符串0
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultParam>(content);
                    imagePaths.RemoveAt(0);
                    return true;
                }

            }
            result = null;
            return false;
        }
        //发送CYsAi服务
        int Hobject1 = 1;
        public void SendToCysAiServer()
        {
            while (true)
            {
                if (IsOpenConectedServer)
                {
                    if (ConverTheImageToBase64AndSendServer(m_arrPositionConnectFilnames, Cys_Soure, out Cys_result))
                    {
                        if (Cys_result.code == "00000")
                        {
                            ConnectResultFlag++;
                            ConnectTotal++;
                            event_LabelConnectDisplay(ConnectTotal, 0);

                            if (Cys_result.result.Count > 0)
                            {
                                ConnectNg++;
                                double score = 0;
                                string code = "";
                                double[] bbox = new double[4];
                                for (int i = 0; i < Cys_result.result.Count; i++)
                                {

                                    if (Cys_result.result[i].score > score)
                                    {
                                        score = Cys_result.result[i].score;
                                        code = Cys_result.result[i].category_name;
                                        bbox = Cys_result.result[i].bbox;
                                    }

                                }
                                event_LabelConnectDisplay(ConnectNg, 1);
                                ListMask.Add(Cys_result.result[0].mask);
                                Listbbox.Add(Cys_result.result[0].bbox);
                                m_ConncetionResultcode.Add(Cys_result.result[0].category_name);
                                ConnectProductionDataResultCodeToXmlExl.Add(Cys_result.result[0].category_name);
                                ConnectProductionDataResultCodeToMoveFile.Add(Cys_result.result[0].category_name);
                                iwsConnect.Cell(ConnectResultFlag, 1).Value = m_ConncetionResultIdToExl.First();
                                string[] str;
                                lock (Directoryimagelock)
                                {
                                    // str = Directory.GetFiles("F:/Connection/data/00");
                                    str = Directory.GetFiles("C:/Connection/data/00");
                                }
                                foreach (var item in str)
                                {
                                    if (item.Contains(m_ConncetionResultIdToExl.First()))
                                    {
                                        // GetBboxImage(code, score, bbox, item);
                                        if (Hobject1 == 1)
                                        {
                                            GetBboxImageHide(code, score, bbox, item);
                                            GetBboxImageHide(code, score, bbox, item);
                                            Hobject1++;
                                        }
                                        else
                                        {
                                            GetBboxImageHide(code, score, bbox, item);
                                        }

                                    }
                                }
                                m_ConncetionResultIdToExl.RemoveAt(0);
                                iwsConnect.Cell(ConnectResultFlag, 2).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                                iwsConnect.Cell(ConnectResultFlag, 2).Value = code;
                                G_wConnect.Save();
                                ProcessSampleConnectResultData2_NVT_Data(m_ConncetionResultIdTodataGridView, code);
                            }
                            else
                            {
                                ConnectOk++;
                                event_LabelConnectDisplay(ConnectOk, 2);
                                iwsConnect.Cell(ConnectResultFlag, 1).Value = m_ConncetionResultIdToExl.First();
                                m_ConncetionResultIdToExl.RemoveAt(0);
                                iwsConnect.Cell(ConnectResultFlag, 2).Value = "Ok";
                                ProcessSampleConnectResultData2_NVT_Data(m_ConncetionResultIdTodataGridView, "OK");
                                G_wConnect.Save();
                                ConnectProductionDataResultCodeToXmlExl.Add("OK");
                                m_ConncetionResultcode.Add("OK");
                                ConnectProductionDataResultCodeToMoveFile.Add("OK");
                            }
                        }
                    }
                    else
                    {
                        // Alarm();
                    }
                }
                Thread.Sleep(10);
            }

        }
        int Hobject = 1;
        public void TheConnect1ImageSendToCysAiServer()
        {
            while (true)
            {
                if (IsOpenConectedServer)
                {
                    if (ConverTheConnect1ImageToBase64AndSendServer(m_arrPositionConnectFilnames1, Cys_Soure1, out Cys_result1))
                    {
                        if (Cys_result1.code == "00000")
                        {
                            ConnectResultFlag1++;
                            ConnectTotal1++;
                            event_LabelConnectDisplay1(ConnectTotal1, 0);

                            if (Cys_result1.result.Count > 0)
                            {
                                ConnectNg1++;
                                double score = 0;
                                string code = "";
                                double[] bbox = new double[4];
                                for (int i = 0; i < Cys_result1.result.Count; i++)
                                {

                                    if (Cys_result1.result[i].score > score)
                                    {
                                        score = Cys_result1.result[i].score;
                                        code = Cys_result1.result[i].category_name;
                                        bbox = Cys_result1.result[i].bbox;
                                    }

                                }
                                event_LabelConnectDisplay1(ConnectNg1, 1);
                                ListMask.Add(Cys_result1.result[0].mask);
                                Listbbox.Add(Cys_result1.result[0].bbox);
                                m_Conncetion1Resultcode.Add(Cys_result1.result[0].category_name);
                                ConnectProductionDataResultCodeToXmlExl1.Add(Cys_result1.result[0].category_name);
                                ConnectProductionDataResultCodeToMoveFile1.Add(Cys_result1.result[0].category_name);
                                iwsConnect1.Cell(ConnectResultFlag1, 1).Value = m_ConncetionResultIdToExl1.First();
                                string[] str;
                                lock (Directoryimagelock)
                                {
                                    // str = Directory.GetFiles("F:/Connection/data/01");
                                    str = Directory.GetFiles("C:/Connection/data/01");
                                }
                                foreach (var item in str)
                                {
                                    if (item.Contains(m_ConncetionResultIdToExl1.First()))
                                    {
                                        // GetBboxImage(code, score, bbox, item);
                                        if (Hobject == 1)
                                        {
                                            GetBboxImageHide1(code, score, bbox, item);
                                            GetBboxImageHide1(code, score, bbox, item);
                                            Hobject++;
                                        }
                                        else
                                        {
                                            GetBboxImageHide1(code, score, bbox, item);
                                        }

                                    }
                                }
                                m_ConncetionResultIdToExl1.RemoveAt(0);
                                iwsConnect1.Cell(ConnectResultFlag1, 2).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                                iwsConnect1.Cell(ConnectResultFlag1, 2).Value = code;
                                G_wConnect1.Save();
                                ProcessSampleConnectResultData2_NVT_Data1(m_ConncetionResultIdTodataGridView1, code);
                            }
                            else
                            {
                                ConnectOk1++;
                                event_LabelConnectDisplay1(ConnectOk1, 2);
                                iwsConnect1.Cell(ConnectResultFlag1, 1).Value = m_ConncetionResultIdToExl1.First();
                                m_ConncetionResultIdToExl1.RemoveAt(0);
                                iwsConnect1.Cell(ConnectResultFlag1, 2).Value = "Ok";
                                ProcessSampleConnectResultData2_NVT_Data1(m_ConncetionResultIdTodataGridView1, "OK");
                                G_wConnect1.Save();
                                ConnectProductionDataResultCodeToXmlExl1.Add("Ok");
                                m_Conncetion1Resultcode.Add("OK");
                                ConnectProductionDataResultCodeToMoveFile1.Add("OK");
                            }
                        }
                    }
                    else
                    {
                        // Alarm();
                    }
                }
                Thread.Sleep(10);
            }

        }


        private void GetBboxImage(string code, double score, double[] bbox, string imagepath)
        {
            HTuple row = ((int)bbox[1]);
            HTuple column = ((int)bbox[0]);
            HTuple width = ((int)bbox[2]);
            HTuple high = ((int)bbox[3]);
            string Score = score.ToString("0.00");
            string[] imageResult = imagepath.Split('.');
            HObject image;
            HObject ho_Rectangle, ho_Image;
            HTuple hv_WindowHandle = new HTuple();
            HOperatorSet.GenEmptyObj(out image);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Image);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.CloseWindow(HDevWindowStack.Pop());
            }
            HOperatorSet.SetWindowAttr("background_color", "black");
            HOperatorSet.OpenWindow(0, 0, 5120, 2548, 0, "visible", "", out hv_WindowHandle);
            HDevWindowStack.Push(hv_WindowHandle);
            image.Dispose();
            HOperatorSet.ReadImage(out image, imagepath);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(image, HDevWindowStack.GetActive()
                    );
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "margin");
            }
            ho_Rectangle.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle, row, column, 0, width, high);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Rectangle, HDevWindowStack.GetActive());
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispText(HDevWindowStack.GetActive(), code + " " + Score, "image", row,
                    column, "red", new HTuple(), new HTuple());
            }
            ho_Image.Dispose();
            HOperatorSet.DumpWindowImage(out ho_Image, hv_WindowHandle);
            HOperatorSet.WriteImage(ho_Image, "jpeg 60", 255, imageResult[0] + "result.jpeg");
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.CloseWindow(HDevWindowStack.Pop());
            }
            image.Dispose();
            ho_Rectangle.Dispose();
            ho_Image.Dispose();
            hv_WindowHandle.Dispose();

        }

        private void GetBboxImageHide(string code, double score, double[] bbox, string imagepath)
        {
            HTuple row = ((int)bbox[1]);
            HTuple column = ((int)bbox[0]);
            HTuple width = ((int)bbox[2]);
            HTuple high = ((int)bbox[3]);
            string Score = score.ToString("0.00");
            string[] imageResult = imagepath.Split('.');
            HObject image;
            HObject ho_Rectangle, ho_Contours, ho_Image;

            HTuple hv_WindowHandle0 = new HTuple();
            HOperatorSet.GenEmptyObj(out image);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_Image);
            hv_WindowHandle0.Dispose();
            HOperatorSet.OpenWindow(0, 0, 5120, 2548, 0, "buffer", "", out hv_WindowHandle0);
            image.Dispose();
            HOperatorSet.SetColor(hv_WindowHandle0, "red");
            HOperatorSet.ReadImage(out image, imagepath);
            HOperatorSet.DispObj(image, hv_WindowHandle0);
            ho_Rectangle.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle, row, column, 0, width, high);
            ho_Contours.Dispose();
            HOperatorSet.GenContourRegionXld(ho_Rectangle, out ho_Contours, "border");
            HOperatorSet.DispObj(ho_Contours, hv_WindowHandle0);
            HOperatorSet.DispText(hv_WindowHandle0, code + " " + Score, "image", row, column, "red",
                new HTuple(), new HTuple());
            ho_Image.Dispose();
            HOperatorSet.DumpWindowImage(out ho_Image, hv_WindowHandle0);
            HOperatorSet.WriteImage(ho_Image, "jpeg 60", 0, imageResult[0] + "_result.jpeg");
            HOperatorSet.ClearWindow(hv_WindowHandle0);
            HOperatorSet.CloseWindow(hv_WindowHandle0);
            image.Dispose();
            ho_Rectangle.Dispose();
            ho_Contours.Dispose();
            ho_Image.Dispose();
            hv_WindowHandle0.Dispose();

        }

        private void GetBboxImageHide1(string code, double score, double[] bbox, string imagepath)
        {
            HTuple row = ((int)bbox[1]);
            HTuple column = ((int)bbox[0]);
            HTuple width = ((int)bbox[2]);
            HTuple high = ((int)bbox[3]);
            string Score = score.ToString("0.00");
            string[] imageResult = imagepath.Split('.');
            HObject image;
            HObject ho_Rectangle, ho_Contours, ho_Image;

            HTuple hv_WindowHandle1 = new HTuple();
            HOperatorSet.GenEmptyObj(out image);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_Image);
            hv_WindowHandle1.Dispose();
            HOperatorSet.OpenWindow(0, 0, 3080, 5120, 0, "buffer", "", out hv_WindowHandle1);
            image.Dispose();
            HOperatorSet.SetColor(hv_WindowHandle1, "red");
            HOperatorSet.ReadImage(out image, imagepath);
            HOperatorSet.DispObj(image, hv_WindowHandle1);
            ho_Rectangle.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle, row, column, 0, width, high);
            ho_Contours.Dispose();
            HOperatorSet.GenContourRegionXld(ho_Rectangle, out ho_Contours, "border");
            HOperatorSet.DispObj(ho_Contours, hv_WindowHandle1);
            HOperatorSet.DispText(hv_WindowHandle1, code + " " + Score, "image", row, column, "red",
                new HTuple(), new HTuple());
            ho_Image.Dispose();
            HOperatorSet.DumpWindowImage(out ho_Image, hv_WindowHandle1);
            HOperatorSet.WriteImage(ho_Image, "jpeg 60", 0, imageResult[0] + "_result.jpeg");
            HOperatorSet.ClearWindow(hv_WindowHandle1);
            HOperatorSet.CloseWindow(hv_WindowHandle1);

            image.Dispose();
            ho_Rectangle.Dispose();
            ho_Contours.Dispose();
            ho_Image.Dispose();
            hv_WindowHandle1.Dispose();

        }

        private void CheckDisk()
        {
            PerformanceCounter diskRt = new PerformanceCounter("PhysicalDisk", "% Disk Time", "0 C:");
            PerformanceCounter diskRt1 = new PerformanceCounter("PhysicalDisk", "% Disk Write Time", "0 C:");
            PerformanceCounter diskRt2 = new PerformanceCounter("PhysicalDisk", "% Disk Read Time", "0 C:");

            while (true)
            {
                Thread.Sleep(1000);
                float t = diskRt.NextValue();
                float t1 = diskRt1.NextValue();
                float t2 = diskRt2.NextValue();
                event_LabelDiskDisplay((int)t, 0);
                event_LabelDiskDisplay((int)t1, 1);
                event_LabelDiskDisplay((int)t2, 2);
                if ((int)t > 90 || (int)t1 > 90 || (int)t2 > 90)
                {
                    MotorsClass.omronInstance.Write(MotorsClass.plc_DiskCheck, (float)2);
                    if (!MotorsClass.omronInstance.Write(MotorsClass.plc_DiskCheck, (float)2).IsSuccess)
                    {
                        MotorsClass.omronInstance.Write(MotorsClass.plc_DiskCheck, (float)2);
                        LogHelper.LogError("硬盘使用率过高1" + t.ToString());
                    }
                    else
                    {
                        LogHelper.LogError("硬盘使用率过高写入PLC成功" + t.ToString());
                    }
                    LogHelper.LogError("硬盘使用率过高" + t.ToString());
                }
                else
                {
                    MotorsClass.omronInstance.Write(MotorsClass.plc_DiskCheck, (float)0);

                }



            }
            diskRt.Close();
            diskRt.Dispose();
            diskRt1.Close();
            diskRt1.Dispose();
            diskRt2.Close();
            diskRt2.Dispose();

        }

    }
}
