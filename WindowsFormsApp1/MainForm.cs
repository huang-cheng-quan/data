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
using HalconDotNet;
using BaseForm;
using Camera_Capture_demo.HalconVision;
using Dyestripping.DeviceSettingFrms;
using WindowsFormsApp1.ConnectorDetection;
using WindowsFormsApp1.HalconVision;
using static WindowsFormsApp1.InferData.InferServerData;
using WindowsFormsApp1.TimeDelete;
using System.Globalization;
using Microsoft.VisualBasic;
using System.IO;
using MvCamCtrl.NET;
using WindowsFormsApp1.VisionFrms;
using WindowsFormsApp1.HalconTools;
using WindowsFormsApp1.SqlServer;
using WindowsFormsApp1.InferData;
using Dyestripping.Models;

namespace WindowsFormsApp1
{
    public partial class MainForm : ZoomForm
    {
        //载入配置
        List<HalconOperator> halconOperatorList;
        //和PLC的连接    
        MotionProcess motionProcess;
        //整体变量
        int m_CamerNum = 10;

        public static bool IsdisableInfer;
        SnNumberInfo snnumber = new SnNumberInfo();
        InferResultData Inferesultdata = new InferResultData();
        ReCheckId recheckId = new ReCheckId();
        SetTime setTime = new SetTime();
        CreateTheExl CreatTheExl = new CreateTheExl();
        /// <summary>
        /// 光度立体路径
        /// </summary>
        string PicShow_Path = null;
        //原始图片保存天数
        int m_iOriginalImagesSaveTime = 7;
        //缩略图保存天数
        int m_iCompressionImagesSaveTime = 7;
        string OriginalImagesSaveFilePath = "";
        string CompressionImagesSaveFilePath = "";
        //显示图像窗口
        IntPtr[] m_hDisplayHandle;
        public static string SaveFilePath = "";
        public static bool IsinitOK = false;
        public delegate void Alarm();
        public static event Alarm Alarmevent;
        public static event Alarm ResetAlarmevent;
        public MainForm()
        {
            InitializeComponent();
            // prepareData(1);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            //DelelPhoto();
            CreatTheEmptyInfoXml();
            string str = Application.StartupPath+"\\"+ DateTime.Now.Year.ToString() + "\\" + DateTime.Now.ToString("MM") + "\\" + DateTime.Now.ToString("MM-dd");
            ConfigVars.configInfo = XmlHelper.DeserializeFromXml<ConfigInfo>();
            snnumber = XmlHelper01.DeserializeFromXml<SnNumberInfo>("SnNumberInfo");
            recheckId = XmlHelper01.DeserializeFromXml<ReCheckId>("ReCheckId");
            Inferesultdata = XmlHelper01.DeserializeFromXml<InferResultData>("InferResultData");
            CreatTheExl = XmlHelper02.DeserializeFromXml<CreateTheExl>("CreateTheExl", str);
            #region 推理机结果反序列化 InferResultData
            if (Inferesultdata.Pose_result1 == null|| (Inferesultdata.Pose_result1.Count==0))
            {
                MotionProcess.Pose_result1 = new List<Data_NVT_Defect_Message>();
               
            }
            else
            {
                MotionProcess.Pose_result1 = Inferesultdata.Pose_result1;
            }
            if (Inferesultdata.Pose_result2 == null || (Inferesultdata.Pose_result2.Count == 0))
            {
                MotionProcess.Pose_result2 = new List<Data_NVT_Defect_Message>();

            }
            else
            {
                MotionProcess.Pose_result2 = Inferesultdata.Pose_result2;
            }
            if (Inferesultdata.Pose_result3 == null || (Inferesultdata.Pose_result3.Count == 0))
            {
                MotionProcess.Pose_result3 = new List<Data_NVT_Defect_Message>();

            }
            else
            {
                MotionProcess.Pose_result3 = Inferesultdata.Pose_result3;
            }
            if (Inferesultdata.Pose_result4 == null || (Inferesultdata.Pose_result4.Count == 0))
            {
                MotionProcess.Pose_result4 = new List<Data_NVT_Defect_Message>();

            }
            else
            {
                MotionProcess.Pose_result4 = Inferesultdata.Pose_result4;
            }
            if (Inferesultdata.Pose_result5 == null || (Inferesultdata.Pose_result5.Count == 0))
            {
                MotionProcess.Pose_result5 = new List<Data_NVT_Defect_Message>();

            }
            else
            {
                MotionProcess.Pose_result5 = Inferesultdata.Pose_result5;
            }
            if (Inferesultdata.Pose_result6 == null || (Inferesultdata.Pose_result6.Count == 0))
            {
                MotionProcess.Pose_result6 = new List<Data_NVT_Defect_Message>();

            }
            else
            {
                MotionProcess.Pose_result6 = Inferesultdata.Pose_result6;
            }
            if (Inferesultdata.Pose_result7 == null || (Inferesultdata.Pose_result7.Count == 0))
            {
                MotionProcess.Pose_result7 = new List<Data_NVT_Defect_Message>();

            }
            else
            {
                MotionProcess.Pose_result7 = Inferesultdata.Pose_result7;
            }
            if (Inferesultdata.m_ConnectionResultId == null || Inferesultdata.m_ConnectionResultId.Count == 0)
            {
                MotionProcess.m_ConncetionResultId = new List<string>();
            }
            else
            {
                MotionProcess.m_ConncetionResultId = Inferesultdata.m_ConnectionResultId;
            }
            if (Inferesultdata.m_Connection1ResultId == null || Inferesultdata.m_Connection1ResultId.Count == 0)
            {
                MotionProcess.m_Conncetion1ResultId = new List<string>();
            }
            else
            {
                MotionProcess.m_Conncetion1ResultId = Inferesultdata.m_Connection1ResultId;
            }
            if (Inferesultdata.m_ConncetionResultIdToExl == null || Inferesultdata.m_ConncetionResultIdToExl.Count == 0)
            {
                MotionProcess.m_ConncetionResultIdToExl = new List<string>();
            }
            else
            {
                MotionProcess.m_ConncetionResultIdToExl = Inferesultdata.m_ConncetionResultIdToExl;
            }
            if (Inferesultdata.m_ConncetionResultIdToExl1 == null || Inferesultdata.m_ConncetionResultIdToExl1.Count == 0)
            {
                MotionProcess.m_ConncetionResultIdToExl1 = new List<string>();
            }
            else
            {
                MotionProcess.m_ConncetionResultIdToExl1 = Inferesultdata.m_ConncetionResultIdToExl1;
            }
            if (Inferesultdata.m_ConncetionResultIdTodataGridView == null || Inferesultdata.m_ConncetionResultIdTodataGridView.Count == 0)
            {
                MotionProcess.m_ConncetionResultIdTodataGridView = new List<string>();
            }
            else
            {
                MotionProcess.m_ConncetionResultIdTodataGridView = Inferesultdata.m_ConncetionResultIdTodataGridView;
            }
            if (Inferesultdata.m_ConncetionResultIdTodataGridView1 == null || Inferesultdata.m_ConncetionResultIdTodataGridView1.Count == 0)
            {
                MotionProcess.m_ConncetionResultIdTodataGridView1 = new List<string>();
            }
            else
            {
                MotionProcess.m_ConncetionResultIdTodataGridView1 = Inferesultdata.m_ConncetionResultIdTodataGridView1;
            }

            if (Inferesultdata.m_ConnectionResultcode == null || Inferesultdata.m_ConnectionResultcode.Count == 0)
            {
                MotionProcess.m_ConncetionResultcode = new List<string>();
            }
            else
            {
                MotionProcess.m_ConncetionResultcode = Inferesultdata.m_ConnectionResultcode;
            }
            if (Inferesultdata.m_Connection1Resultcode == null || Inferesultdata.m_Connection1Resultcode.Count == 0)
            {
                MotionProcess.m_Conncetion1Resultcode = new List<string>();
            }
            else
            {
                MotionProcess.m_Conncetion1Resultcode = Inferesultdata.m_Connection1Resultcode;
            }

            if (Inferesultdata.m_ProductionResultId==null||Inferesultdata.m_ProductionResultId.Count==0)
            {
                MotionProcess.m_ProductionResultId = new List<string>();
            }
            else
            {
                MotionProcess.m_ProductionResultId = Inferesultdata.m_ProductionResultId;
            }
            if (Inferesultdata.m_ProductionResultcode==null||Inferesultdata.m_ProductionResultcode.Count==0)
            {
                MotionProcess.m_ProductionResultcode = new List<string>();
            }
            else
            {
                MotionProcess.m_ProductionResultcode = Inferesultdata.m_ProductionResultcode;
            }
            if (Inferesultdata.m_ProductionResultIdToMoveFile==null||Inferesultdata.m_ProductionResultIdToMoveFile.Count==0)
            {
                MotionProcess.m_ProductionResultIdToMoveFile = new List<string>();
            }
            else
            {
                MotionProcess.m_ProductionResultIdToMoveFile = Inferesultdata.m_ProductionResultIdToMoveFile;
            }
            if (Inferesultdata.m_ProductionResultcodeToMoveFile==null||Inferesultdata.m_ProductionResultcodeToMoveFile.Count==0)
            {
                MotionProcess.m_ProductionResultcodeToMoveFile = new List<string>();
            }
            else
            {
                MotionProcess.m_ProductionResultcodeToMoveFile = Inferesultdata.m_ProductionResultcodeToMoveFile;
            }
            if (Inferesultdata.ConnectProductionDataResultIdToMoveFile == null || Inferesultdata.ConnectProductionDataResultIdToMoveFile.Count == 0)
            {
                MotionProcess.ConnectProductionDataResultIdToMoveFile = new List<string>();
            }
            else
            {
                MotionProcess.ConnectProductionDataResultIdToMoveFile = Inferesultdata.ConnectProductionDataResultIdToMoveFile;
            }
            if (Inferesultdata.ConnectProductionDataResultCodeToMoveFile == null || Inferesultdata.ConnectProductionDataResultCodeToMoveFile.Count == 0)
            {
                MotionProcess.ConnectProductionDataResultCodeToMoveFile = new List<string>();
            }
            else
            {
                MotionProcess.ConnectProductionDataResultCodeToMoveFile = Inferesultdata.ConnectProductionDataResultCodeToMoveFile;
            }
            if (Inferesultdata.ConnectProductionDataResultIdToMoveFile1 == null || Inferesultdata.ConnectProductionDataResultIdToMoveFile1.Count == 0)
            {
                MotionProcess.ConnectProductionDataResultIdToMoveFile1 = new List<string>();
            }
            else
            {
                MotionProcess.ConnectProductionDataResultIdToMoveFile1 = Inferesultdata.ConnectProductionDataResultIdToMoveFile1;
            }
            if (Inferesultdata.ConnectProductionDataResultCodeToMoveFile1 == null || Inferesultdata.ConnectProductionDataResultCodeToMoveFile1.Count == 0)
            {
                MotionProcess.ConnectProductionDataResultCodeToMoveFile1 = new List<string>();
            }
            else
            {
                MotionProcess.ConnectProductionDataResultCodeToMoveFile1 = Inferesultdata.ConnectProductionDataResultCodeToMoveFile1;
            }
            #endregion
            #region 二维码反序列化 SnNumberInfo
            if (snnumber.QrcodeBoby1 == null)
            {
                MotionProcess.Qrcode1 = new List<string>();
            }
            else
            {
                MotionProcess.Qrcode1 = snnumber.QrcodeBoby1;
            }
            if (snnumber.QrcodeBoby2 == null)
            {
                MotionProcess.Qrcode2 = new List<string>();
            }
            else
            {
                MotionProcess.Qrcode2 = snnumber.QrcodeBoby2;
            }
            if (snnumber.QrcodeBoby3 == null)
            {
                MotionProcess.Qrcode3 = new List<string>();
            }
            else
            {
                MotionProcess.Qrcode3 = snnumber.QrcodeBoby3;
            }
            if (snnumber.QrcodeBoby4 == null)
            {
                MotionProcess.Qrcode4 = new List<string>();
            }
            else
            {
                MotionProcess.Qrcode4 = snnumber.QrcodeBoby4;
            }
            if (snnumber.QrcodeBobyTemple == null)
            {
                MotionProcess.QrcodeTemple = new List<string>();
                MotionProcess.QrcodeTemple.Add("123");
            }
            else
            {
                MotionProcess.QrcodeTemple = snnumber.QrcodeBobyTemple;
            }
            if (snnumber.FixtureNumber == null)
            {
                MotionProcess.FixtureNumber = new List<string>();
               
            }
            else
            {
                MotionProcess.FixtureNumber = snnumber.FixtureNumber;
            }
            if (snnumber.FixtureNumberToMes == null)
            {
                MotionProcess.FixtureNumberToMes = new List<string>();

            }
            else
            {
                MotionProcess.FixtureNumberToMes = snnumber.FixtureNumberToMes;
            }
           
            if (snnumber.FixtureNumberTempel == null)
            {
                MotionProcess.FixtureNumberTempel = new List<string>();
                MotionProcess.FixtureNumberTempel.Add("123");
            }
            else
            {
                MotionProcess.FixtureNumberTempel = snnumber.FixtureNumberTempel;
            }

            if (snnumber.QrcodeConnectNRead == null)
            {
                MotionProcess.QrcodeConnectNRead = new List<string>();
            }
            else
            {
                MotionProcess.QrcodeConnectNRead = snnumber.QrcodeConnectNRead;
            }
            if (snnumber.QrcodeConnectNRead == null)
            {
                MotionProcess.QrcodeConnectNReadTemple = new List<string>();
                MotionProcess.QrcodeConnectNReadTemple.Add("1345");
            }
            else
            {
                MotionProcess.QrcodeConnectNReadTemple = snnumber.QrcodeConnectNReadTemple;
            }
            if (snnumber.QrcodeConnectPRead == null)
            {
                MotionProcess.QrcodeConnectPRead = new List<string>();
            }
            else
            {
                MotionProcess.QrcodeConnectPRead = snnumber.QrcodeConnectPRead;
            }
            if (snnumber.QrcodeConnectPReadTemple == null)
            {
                MotionProcess.QrcodeConnectPReadTemple = new List<string>();
                MotionProcess.QrcodeConnectPReadTemple.Add("1345");
            }
            else
            {
                MotionProcess.QrcodeConnectPReadTemple = snnumber.QrcodeConnectPReadTemple;
            }
            #endregion
            #region configInfo
            if (ConfigVars.configInfo.UserInfos == null)
            {
                ConfigVars.configInfo.UserInfos = new UserInfos()
                {
                    OperatorPwd = "1",
                    AdministratorPwd = "1",
                    DeveloperPwd = "1"
                };
            }
            if (ConfigVars.configInfo.sqldata==null)
            {
                ConfigVars.configInfo.sqldata = new SqlServerData()
                {
                    server = "DESKTOP-90KHT63", 
                    database = "nvtData",
                    UserId = "sa",
                    password = "123456"

                };
            }
            if (ConfigVars.configInfo.OmronPlcs == null || ConfigVars.configInfo.OmronPlcs.Count == 0)
            {
                List<OmronPlcFactory> omronPlcs = new List<OmronPlcFactory>();
                for (int i = 0; i < 2; i++)
                {
                    omronPlcs.Add(new OmronPlcFactory() { PlcNo = i });
                }
                ConfigVars.configInfo.OmronPlcs = omronPlcs;
            }
            if (ConfigVars.configInfo.calibrationData==null|| ConfigVars.configInfo.calibrationData.Count==0)
            {
                List<CalibrationData> calbraitiondata = new List<CalibrationData>();
               
                for (int i = 0; i < MotionProcess.NeedLocate; i++)
                {
                    if (i != MotionProcess.multitudeMotorToOneCameraFlag)
                    {
                        calbraitiondata.Add(new CalibrationData() { CameraNum = i, MotorNum = 0 });
                    }
                    else
                    {
                        for (int j = 0; j < MotionProcess.multitudeMotorNum; j++)
                        {
                            calbraitiondata.Add(new CalibrationData() { CameraNum = i, MotorNum = j });
                        }
                    }
                    
                }
                ConfigVars.configInfo.calibrationData = calbraitiondata;
            }

            if (ConfigVars.configInfo.Cameras == null || ConfigVars.configInfo.Cameras.Count == 0)
            {
                List<MvClass> cameras = new List<MvClass>();
                for (int i = 0; i < m_CamerNum; i++)
                {

                    cameras.Add(new MvClass() { CameraNo = i });

                }
                ConfigVars.configInfo.Cameras = cameras;
            }
            if (ConfigVars.configInfo.client_Data != null)
            {
                MotionProcess.Client_IP = ConfigVars.configInfo.client_Data.IP_Client;
                MotionProcess.Cilent_Port = ConfigVars.configInfo.client_Data.Port_Client;
            }
            if (ConfigVars.configInfo.sqldata==null)
            {
                ConfigVars.configInfo.sqldata = new SqlServerData();
            }
            if (ConfigVars.configInfo.Save_Time_Period == null)
            {
                ConfigVars.configInfo.Save_Time_Period = new SaveTimePeriod();
            }
            else
            {
                m_iOriginalImagesSaveTime = ConfigVars.configInfo.Save_Time_Period.OriginalImagesSaveTime;
                m_iCompressionImagesSaveTime = ConfigVars.configInfo.Save_Time_Period.CompressionImagesSaveTime;
            }
            if (ConfigVars.configInfo.ProductInfos.SelectProject != null)
            {
                txt_ProjectName.Text = ConfigVars.configInfo.ProductInfos.SelectProject;
                MotionProcess.m_Job_name = ConfigVars.configInfo.ProductInfos.SelectProject;
            }
            if (ConfigVars.configInfo.ToolInfos == null)
            {
                ConfigVars.configInfo.ToolInfos = new ToolInfos();
            }
            if (ConfigVars.configInfo.calibrationData==null)
            {
                for (int i = 0; i < MotionProcess.NeedLocate; i++)
                {
                    ConfigVars.configInfo.calibrationData[i] = new CalibrationData();
                }
            }
            else
            {
                for (int i = 0; i < MotionProcess.NeedLocate; i++)
                {
                    MotionProcess.motion_SinglePixelAccuracy[i] = ConfigVars.configInfo.calibrationData[i].SinglePixelAccuracy;
                }
                
            }
            #endregion
            if (snnumber.m_listSampleIDs==null||snnumber.m_listSampleIDs.Count==0)
            {
                MotionProcess.m_listSampleIDs = new List<string>();
            }
            else
            {
                MotionProcess.m_listSampleIDs = snnumber.m_listSampleIDs;
            }
           // ConfigVars.configInfo.sampleId = MotionProcess.sample_id_num;
           //二维码反序列化参数
            MotionProcess.sample_id_num = ConfigVars.configInfo.sampleId;
            MvClass.numFlag = snnumber.numFlag;
            MotionProcess.SampleIDFlag = snnumber.SampleIDFlag;
            MotionProcess.m_ReCheckIdNumber = recheckId.ReCheckIdNumber;
            MotionProcess.ProductionDataIdToExl = CreatTheExl.ProductionDataIdToExl;
            MotionProcess.ProductionDataResultScoreToExl = CreatTheExl.ProductionDataResultScoreToExl;
            MotionProcess.ProductionDataResultCodeToExl = CreatTheExl.ProductionDataResultCodeToExl;
            MotionProcess.ConnectProductionDataResultCodeToXmlExl = CreatTheExl.ConnectProductionDataResultCodeToXmlExl;
            MotionProcess.ConnectProductionDataResultIdToXmlExl = CreatTheExl.ConnectProductionDataResultIdToXmlExl;
            MotionProcess.ConnectProductionDataResultCodeToXmlExl1 = CreatTheExl.ConnectProductionDataResultCodeToXmlExl1;
            MotionProcess.ConnectProductionDataResultIdToXmlExl1 = CreatTheExl.ConnectProductionDataResultIdToXmlExl1;
            MotionProcess.FixtureNumberToXml = CreatTheExl.FixtureNumberToXml;
            // XmlHelper.SerializeToXml(ConfigVars.configInfo);
            //motionProcess = new MotionProcess();

            #region 事件注册
            LogHelper.LogDisp += LogHelper_LogDisp;
            MotionProcess.event_OnImgDisp += MotionProcess_OnImgDisp;
            MotionProcess.event_OnPositionDisp += MotionProcess_OnPositionDisp;
            MotionProcess.event_LabelDisplay += MotionProcess_OnLabelDisp;
            MotionProcess.event_LabelConnectDisplay += MotionProcess_ConnectOnLabelDisp;
            MotionProcess.event_LabelConnectDisplay1 += MotionProcess_ConnectOnLabelDisp1;
            MotionProcess.event_LabelDiskDisplay += MotionProcess_DiskOnLabelDisp1;
            MotionProcess.event_LabelTheLastResultDisplay += MotionProcess_TheLasterResultOnLabelDisp;
            MotionProcess.event_ResultDisplay += MotionProcess_OnLabelDispResult;
            MotionProcess.event_ProcessSampleResultData_NVT += ProcessSampleResultData_NVT_Data;
            MotionProcess.event_ProcessSampleResultData2_NVT += ProcessSampleResultData2_NVT_Data;
            MotionProcess.ProcessSampleConnectResultData2_NVT_Data += ProcessSampleConnectResultData2_NVT_Data;
            MotionProcess.ProcessSampleConnectResultData2_NVT_Data1 += ProcessSampleConnect1ResultData2_NVT_Data;
            MotionProcess.event_QrCodeShow += MotionProcess_eventQrcodeShow;
            MvClass.eventProcessData += MotionProcess_eventProcessImageShow;
            
            #endregion
            m_hDisplayHandle = new IntPtr[MotionProcess.m_nCameraTotalNum];

            motionProcess = MotionProcess.GetInstance();
            motionProcess.Init();
            timer1.Enabled = true;
            IsinitOK = true;
            //MotionProcess.CreateXMlToExl();

            //本体相机增益初始化
            MvClass.numFlag[0] = 10;
            MvClass.numFlag[1] = 10;
            MvClass.numFlag[2] = 10;
            MvClass.numFlag[3] = 10;
            MotionProcess.m_listMvCameras[0].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 15);
            MotionProcess.m_listMvCameras[1].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 0);
            MotionProcess.m_listMvCameras[2].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 0);
            MotionProcess.m_listMvCameras[3].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 12);
            MotionProcess.m_listMvCameras[2].m_MyCamera.MV_CC_SetFloatValue_NET("ExposureTime", 5000);
           
                Test_TC_Server_State();
           
           
        }

        #region 显示委托
        private void ProcessSampleResultData_NVT_Data(SampleResultData_NVT Srdn)
        {
       //     motionProcess.Show_image2Picbox(Srdn, pic_1, pic_2, pic_3, pic_4, pic_5, pic_6, pic_7, picShow);
        }
        private void ProcessSampleResultData2_NVT_Data(SampleResultData2_NVT srdn2)
        {
            motionProcess.Show_Result2Table(srdn2, dataGridView2);
        }

        private void ProcessSampleConnectResultData2_NVT_Data(List<string> resultid,string  code)
        {
            motionProcess.Show_ConnectResult2Table(resultid, code, dgv_connect);
        }

        private void ProcessSampleConnect1ResultData2_NVT_Data(List<string> resultid, string code)
        {
            motionProcess.Show_Connect1Result2Table(resultid, code, dgv_connect1);
        }
        private void MotionProcess_OnLabelDisp(int Number, int flag)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                switch (flag)
                {
                    case 0:
                        lbl_TotalSampleNums2.Text = Number.ToString();
                        break;
                    case 1:
                        lbl_NGSampleNums2.Text = Number.ToString();
                        break;
                    case 2:
                        lbl_OKSampleNums2.Text = Number.ToString();
                        break;
                    default:
                        break;
                }


            }));
        }

        private void MotionProcess_ConnectOnLabelDisp(int Number, int flag)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                switch (flag)
                {
                    case 0:
                        lb_totalConnect.Text = Number.ToString();
                        break;
                    case 1:
                        lb_ConnectNg.Text = Number.ToString();
                        break;
                    case 2:
                        lb_ConnectOk.Text = Number.ToString();
                        break;
                    default:
                        break;
                }


            }));
        }

        private void MotionProcess_ConnectOnLabelDisp1(int Number, int flag)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                switch (flag)
                {
                    case 0:
                        lb_totalConnect1.Text = Number.ToString();
                        break;
                    case 1:
                        lb_ConnectNg1.Text = Number.ToString();
                        break;
                    case 2:
                        lb_ConnectOk1.Text = Number.ToString();
                        break;
                    default:
                        break;
                }


            }));
        }

        private void MotionProcess_DiskOnLabelDisp1(int Number, int flag)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                switch (flag)
                {
                    case 0:
                        lb_disktotal.Text = Number.ToString()+"%";
                        break;
                    case 1:
                        lb_read.Text = Number.ToString() + "%";
                        break;
                    case 2:
                        lb_write.Text = Number.ToString() + "%";
                        break;
                    default:
                        break;
                }


            }));
        }

        private void MotionProcess_TheLasterResultOnLabelDisp(int Number, int flag)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                switch (flag)
                {
                    case 0:
                        lb_LastTotal.Text = Number.ToString();
                        break;
                    case 1:
                        lb_LastNg.Text = Number.ToString();
                        break;
                    case 2:
                        lb_LastOk.Text = Number.ToString();
                        break;
                    default:
                        break;
                }


            }));
        }

        private void MotionProcess_OnLabelDispResult(string SamlpeId, string Resultcode)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                if (Resultcode == "OK")
                {
                    SampleId_lab.Text = SamlpeId;
                    Result_lab.ForeColor = Color.Green;
                    Result_lab.Text = "OK";
                }
                else
                {
                    SampleId_lab.Text = SamlpeId;
                    Result_lab.ForeColor = Color.Red;
                    Result_lab.Text = "NG";
                }
              


            }));
        }
        private void MotionProcess_OnImgDisp(HObject hImage, int camNo,int motorNum)
        {
            if (hImage == null)
            {
                return;
            }
            if (halconOperatorList == null)
            {
                halconOperatorList = new List<HalconOperator>();
                for (int i = 0; i < MotionProcess.m_ihalconProcesEndNum; i++)
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

            }
            Task.Run(() => {

                switch (camNo)
                {
                    case 4:
                        motionProcess.GetModelPoint(hWindowControl1, camNo, hImage, motorNum);
                        break;
                    case 6:
                        motionProcess.GetModelPoint(hWindowControl2, camNo, hImage, motorNum);
                        break;
                    case 8:
                        motionProcess.GetModelPoint(hWindowControl3, camNo, hImage, motorNum);
                        break;
                    case 5:
                        if (motorNum == 1)
                        {
                            motionProcess.GetModelPoint(hWindowControl4, camNo, hImage, motorNum);
                        }
                        else
                        {
                            motionProcess.GetModelPoint(hWindowControl5, camNo, hImage, motorNum);
                        }

                        break;

                    default:
                        break;
                }






            });

            /*this.BeginInvoke(new MethodInvoker(() =>
            {
                
                switch (camNo)
                {
                    case 4:
                        motionProcess.GetModelPoint(hWindowControl1, camNo, hImage, motorNum);
                        break;
                    case 6:
                        motionProcess.GetModelPoint(hWindowControl2, camNo, hImage, motorNum);
                        break;
                    case 8:
                        motionProcess.GetModelPoint(hWindowControl3, camNo, hImage, motorNum);
                        break;
                    case 5:
                        if (motorNum==1)
                        {
                            motionProcess.GetModelPoint(hWindowControl4, camNo, hImage, motorNum);
                        }
                        else
                        {
                            motionProcess.GetModelPoint(hWindowControl5, camNo, hImage, motorNum);
                        }
                       
                        break;
                   
                    default:
                        break;
                }
            }));*/
        }
        private void MotionProcess_eventProcessImageShow(int camNum, MyCamera.MV_DISPLAY_FRAME_INFO stDisplayInfo, IntPtr data)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                
                stDisplayInfo.hWnd = m_hDisplayHandle[camNum];
                MotionProcess.m_listMvCameras[camNum].m_MyCamera.MV_CC_DisplayOneFrame_NET(ref stDisplayInfo);
                
            }));

        }
        private void MotionProcess_eventQrcodeShow(string code) 
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
               // label38.Text = code;
            }));
        }
        private void MotionProcess_OnPositionDisp(int toolNo, PointXYU pointXYU)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                if (pointXYU != null)
                {
                    switch (toolNo)
                    {

                        case 4:
                            label41.Text = pointXYU.X.ToString("00.00");
                            label42.Text = pointXYU.Y.ToString("00.00");
                            label43.Text = pointXYU.U.ToString("00.00");
                            break;
                        case 6:
                            label44.Text = pointXYU.X.ToString("00.00");
                            label45.Text = pointXYU.Y.ToString("00.00");
                            label46.Text = pointXYU.U.ToString("00.00");
                            break;
                        case 8:
                            label47.Text = pointXYU.X.ToString("00.00");
                            label48.Text = pointXYU.Y.ToString("00.00");
                            label49.Text = pointXYU.U.ToString("00.00");
                            break;
                        default:
                            break;
                    }
                }
               

             
            }));
        }
        private void LogHelper_LogDisp(string msg, string level)
        {
            BeginInvoke(new MethodInvoker(() =>
            {
                Color color = Color.Black;
                switch (level)
                {
                    case "Info":
                        color = Color.Black;
                        break;
                    case "Warn":
                        color = Color.Blue;
                        break;
                    case "Error":
                        color = Color.Red;
                        break;
                    default:
                        break;
                }
                if (this.rtxLog.TextLength >= 5000)
                {
                    this.rtxLog.Clear();
                }
                this.rtxLog.Select(this.rtxLog.Text.Length, 0);
                this.rtxLog.Focus();
                this.rtxLog.SelectionColor = color;
                this.rtxLog.AppendText(msg + "\n");
            }
));
        }
        #endregion

        private void 相机设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (motionProcess.Login(0))
            {
                WindowHelper.ShowOrActiveForm<MV_CameraSettingForm>("MV_CameraSettingForm");
            }
        }
        private void 标定设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 坐标标定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (motionProcess.Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("CalibrationFrm0"))
                {
                    CalibrationFrm frm = new CalibrationFrm(4,0);
                    frm.Show();
                }
            }
        }
        private void 坐标标定本体定位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (motionProcess.Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("OffsetCalibrationFrom"))
                {
                    OffsetCalibrationFrom frm = new OffsetCalibrationFrom(6,0);
                    frm.Show();
                }
            }
        }
        private void 坐标标定本体定位2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (motionProcess.Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("OffsetCalibrationFrom"))
                {
                    OffsetCalibrationFrom frm = new OffsetCalibrationFrom(8,0);
                    frm.Show();
                }
            }
        }
        private void 模板建立ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            motionProcess = new MotionProcess();
            if (motionProcess != null && !motionProcess.abort)
            {
                MessageBox.Show("设备运行中,设置界面不可打开");
                return;
            }
            if (motionProcess.Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("CreateShapeModelFrm0"))
                {
                    CreateShapeModelFrm frm = new CreateShapeModelFrm(4);
                    frm.Show();
                }
            }
        }
        private void 模板建立本体定位ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (motionProcess != null && !motionProcess.abort)
            {
                MessageBox.Show("设备运行中,设置界面不可打开");
                return;
            }

            if (motionProcess.Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("HalconTools_CreateModelForm"))
                {
                    HalconTools_CreateModelForm frm = new HalconTools_CreateModelForm(6);
                    frm.Show();
                }
            }
        }
        private void 模板建立本体定位2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (motionProcess != null && !motionProcess.abort)
            {
                MessageBox.Show("设备运行中,设置界面不可打开");
                return;
            }
            if (motionProcess.Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("HalconTools_CreateModelForm"))
                {
                    HalconTools_CreateModelForm frm = new HalconTools_CreateModelForm(8);
                    frm.Show();
                }
            }
        }
        private void plc设置上料ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (motionProcess.Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("OmronPlcSettingFrm0"))
                {
                    OmronPlcSettingFrm frm = new OmronPlcSettingFrm(0);
                    frm.Show();
                }
            }
        }
        private void pLC设置本体定位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (motionProcess.Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("OmronPlcSettingFrm1"))
                {
                    OmronPlcSettingFrm frm = new OmronPlcSettingFrm(1);
                    frm.Show();
                }
            }
        }
        private void btn_ResetFeedBelt_Click(object sender, EventArgs e)
        {
        }
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否退出程序", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                LogHelper.LogInfo("软件退出");
                if (motionProcess != null)
                   motionProcess.Exit();
               
                LogHelper.ExitThread();
            }
            else
            {
                e.Cancel = true;
            }
            /* for (int i = 0; i < 7; i++)
             {
                 MotionProcess.sample_id_num[i]++;
             }*/
            #region 二维码序列化参数
            //MotionProcess.QrcodeTemple.Clear();
            //MotionProcess.QrcodeConnectNReadTemple.Clear();
            //MotionProcess.QrcodeConnectPReadTemple.Clear();
            //MotionProcess.QrcodeTemple.Add("123");
            //MotionProcess.QrcodeConnectNReadTemple.Add("1345");
            //MotionProcess.QrcodeConnectPReadTemple.Add("1345");
            snnumber.QrcodeBoby1 = MotionProcess.Qrcode1;
            snnumber.QrcodeBoby2 = MotionProcess.Qrcode2;
            snnumber.QrcodeBoby3 = MotionProcess.Qrcode3;
            snnumber.QrcodeBoby4 = MotionProcess.Qrcode4;
            snnumber.QrcodeBobyTemple = MotionProcess.QrcodeTemple;
            snnumber.QrcodeConnectNRead = MotionProcess.QrcodeConnectNRead;
            snnumber.QrcodeConnectNReadTemple = MotionProcess.QrcodeConnectNReadTemple;
            snnumber.QrcodeConnectPRead = MotionProcess.QrcodeConnectPRead;
            snnumber.QrcodeConnectPReadTemple = MotionProcess.QrcodeConnectPReadTemple;
            snnumber.numFlag = MvClass.numFlag;
            snnumber.SampleIDFlag = MotionProcess.SampleIDFlag;
            snnumber.m_listSampleIDs = MotionProcess.m_listSampleIDs;
            snnumber.FixtureNumber = MotionProcess.FixtureNumber;          
            snnumber.FixtureNumberToMes = MotionProcess.FixtureNumberToMes;
            snnumber.FixtureNumberTempel = MotionProcess.FixtureNumberTempel;
            recheckId.ReCheckIdNumber = MotionProcess.m_ReCheckIdNumber;
            #endregion
            #region 推理机结果序列化参数
            Inferesultdata.Pose_result1 = MotionProcess.Pose_result1;
            Inferesultdata.Pose_result2 = MotionProcess.Pose_result2;
            Inferesultdata.Pose_result3 = MotionProcess.Pose_result3;
            Inferesultdata.Pose_result4 = MotionProcess.Pose_result4;
            Inferesultdata.Pose_result5 = MotionProcess.Pose_result5;
            Inferesultdata.Pose_result6 = MotionProcess.Pose_result6;
            Inferesultdata.Pose_result7 = MotionProcess.Pose_result7;
            Inferesultdata.m_ProductionResultId = MotionProcess.m_ProductionResultId;
            Inferesultdata.m_ProductionResultcode = MotionProcess.m_ProductionResultcode;
            Inferesultdata.m_ProductionResultIdToMoveFile = MotionProcess.m_ProductionResultIdToMoveFile;
            Inferesultdata.m_ProductionResultcodeToMoveFile = MotionProcess.m_ProductionResultcodeToMoveFile;
            Inferesultdata.ConnectProductionDataResultCodeToMoveFile = MotionProcess.ConnectProductionDataResultCodeToMoveFile;
            Inferesultdata.ConnectProductionDataResultCodeToMoveFile1 = MotionProcess.ConnectProductionDataResultCodeToMoveFile1;
            Inferesultdata.ConnectProductionDataResultIdToMoveFile = MotionProcess.ConnectProductionDataResultIdToMoveFile;
            Inferesultdata.ConnectProductionDataResultIdToMoveFile1 = MotionProcess.ConnectProductionDataResultIdToMoveFile1;
            Inferesultdata.m_ConnectionResultId = MotionProcess.m_ConncetionResultId;
            Inferesultdata.m_ProductionResultcode = MotionProcess.m_ProductionResultcode;
            Inferesultdata.m_ConncetionResultIdToExl = MotionProcess.m_ConncetionResultIdToExl;
            Inferesultdata.m_ConncetionResultIdTodataGridView = MotionProcess.m_ConncetionResultIdTodataGridView;
            Inferesultdata.m_Connection1ResultId = MotionProcess.m_Conncetion1ResultId;
            Inferesultdata.m_Connection1Resultcode = MotionProcess.m_Conncetion1Resultcode;
            Inferesultdata.m_ConncetionResultIdToExl1 = MotionProcess.m_ConncetionResultIdToExl1;
            Inferesultdata.m_ConncetionResultIdTodataGridView1 = MotionProcess.m_ConncetionResultIdTodataGridView1;
            Inferesultdata.m_ConnectionResultcode= MotionProcess.m_ConncetionResultcode;
         
            #endregion
            XmlHelper.SerializeToXml(ConfigVars.configInfo);
            //HalconOperator.halconOperatorList.Clear();//0707
            string SnNumberIn = "SnNumberInfo";
            XmlHelper01.SerializeToXml(snnumber, SnNumberIn);
            XmlHelper01.SerializeToXml(Inferesultdata, "InferResultData");
            XmlHelper01.SerializeToXml(recheckId, "ReCheckId");
            SaveTheResultInfoToXml();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            /*setTime.DeleteFile(OriginalImagesSaveFilePath, m_iOriginalImagesSaveTime);  //删除原始照片超过天数的文件
            setTime.DeleteFile(CompressionImagesSaveFilePath, m_iCompressionImagesSaveTime);  //删除压缩照片超过天数的文件*/
            long FreeMomory = MotionProcess.GetHardDiskFreeSpace("F:\\");
            lbl_FreeMeomory.Text = FreeMomory.ToString();

        }
        private void 切换项目ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (motionProcess.Login(1))
            {
                try
                {
                    string desc = null;
                    if (CultureInfo.InstalledUICulture.Name == "zh-CN")
                    {
                        desc = "请选择Project目录下的项目";
                    }
                    else
                    {
                        desc = "Select the project in the Project directory";
                    }
                    string _selectedFolder = motionProcess.SelectFolder(desc, false);
                    //显示选择安装目录界面，同时允许创建新文件夹
                    if (_selectedFolder.Length > 0)
                    {
                        ConfigVars.configInfo.ProductInfos.SelectProject = _selectedFolder.Replace(Application.StartupPath + "\\Project\\", string.Empty);
                        XmlHelper.SerializeToXml(ConfigVars.configInfo);
                        txt_ProjectName.Text = ConfigVars.configInfo.ProductInfos.SelectProject;
                        MotionProcess.m_Job_name = txt_ProjectName.Text;
                       
                        if (halconOperatorList != null)
                        {
                            halconOperatorList[0].ReUpdateParams();
                            halconOperatorList[1].ReUpdateParams();
                        }
                        if (CultureInfo.InstalledUICulture.Name == "zh-CN")
                        {
                            MessageBox.Show(ConfigVars.configInfo.ProductInfos.SelectProject + "项目加载完成");
                        }
                        else
                        {
                            MessageBox.Show(ConfigVars.configInfo.ProductInfos.SelectProject + "The project is loaded");
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (CultureInfo.InstalledUICulture.Name == "zh-CN")
                    {
                        MessageBox.Show("项目加载失败！" + ex.Message);
                    }
                    else
                    {
                        MessageBox.Show("Project Load Failure！" + ex.Message);
                    }
                }
            }
        }
        private void 新建项目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (motionProcess.Login(1))
            {
                string proj_name = Interaction.InputBox("请输入项目名称", "新建项目");
                if (!string.IsNullOrEmpty(proj_name))
                {
                    string dest_proj_Path = Application.StartupPath + "\\Project\\" + proj_name;
                    if (!Directory.Exists(dest_proj_Path))
                    {
                        Directory.CreateDirectory(dest_proj_Path);
                    }
                    MotionProcess.m_Job_name = proj_name;
                }
            }
        }
        private void btn_Start_Click(object sender, EventArgs e)
        {
            m_hDisplayHandle[0] = pictureBox1.Handle;
            m_hDisplayHandle[1] = pictureBox2.Handle;
            m_hDisplayHandle[2] = pictureBox3.Handle;
            m_hDisplayHandle[3] = pictureBox4.Handle;
            m_hDisplayHandle[5] = pictureBox5.Handle;
            m_hDisplayHandle[7] = pictureBox8.Handle;
           // m_hDisplayHandle[9] = pictureBox9.Handle;
            //motionProcess = new MotionProcess();
            if (!MotionProcess.Camera_state)
            {
                MessageBox.Show("相机连接错误请检查");
            }
            else
            {
                if (btn_Start.Text == "启动")
                {
                    if (motionProcess == null || !motionProcess.is_init)
                    {
                        motionProcess = MotionProcess.GetInstance();
                        motionProcess.Init();
                    }

                    if (motionProcess.CheckHImageFrmIsOpen())
                    {
                        MessageBox.Show("请先关闭设置窗口");
                        return;
                    }
                    if (motionProcess.is_init)
                    {
                        motionProcess.abort = false;
                        motionProcess.Reset();
                        btn_Start.Text = "停止";
                    }
                }
                else
                {
                    motionProcess.abort = true;
                    motionProcess.Stop();
                    btn_Start.Text = "启动";
                }
            }
           
        }
        private void btn_Stop_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void 设置补偿值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (motionProcess.Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("ToolOffsetSettingFrm"))
                {
                    ToolOffsetSettingFrm frm = new ToolOffsetSettingFrm();
                    frm.Show();
                }
            }
        }
        private void btn_Reset_Click(object sender, EventArgs e)
        {
            motionProcess.Reset();
            btn_Start.Text = "停止";
            LogHelper.LogInfo("复位成功！");
        }
        private void 坐标标定钢片检测AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (motionProcess.Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("OffsetCalibrationFrom"))
                {
                    OffsetCalibrationFrom frm = new OffsetCalibrationFrom(5,1);
                    frm.Show();
                }
            }
        }
        private void 坐标标定钢片检测BToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (motionProcess.Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("OffsetCalibrationFrom"))
                {
                    OffsetCalibrationFrom frm = new OffsetCalibrationFrom(5,2);
                    frm.Show();
                }
            }
        }
        private void 模板建立钢片检测AToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (motionProcess != null && !motionProcess.abort)
            {
                MessageBox.Show("设备运行中,设置界面不可打开");
                return;
            }
            if (motionProcess.Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("HalconTools_CreateModelForm"))
                {
                    HalconTools_CreateModelForm frm = new HalconTools_CreateModelForm(5);
                    frm.Show();
                }
            }
        }
        private void 数据库设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (motionProcess.Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("TestForm"))
                {
                    SqlConnectionForm frm = new SqlConnectionForm();
                    frm.Show();
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //textBox1.Text = MvClass.m_nFrames[0].ToString();
            //textBox2.Text = MvClass.m_nFrames[1].ToString();
            //textBox3.Text = MvClass.m_nFrames[2].ToString();
            //textBox4.Text = MvClass.m_nFrames[3].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog P_File_Folder = new FolderBrowserDialog();
            if (P_File_Folder.ShowDialog() == DialogResult.OK)
            {
                MotionProcess.ServerSaveFilePath = P_File_Folder.SelectedPath + "/";
                LogHelper.LogInfo("保存文件夹路径：" + P_File_Folder.SelectedPath);

            }
        }

        private void inferDisable_btn_Click(object sender, EventArgs e)
        {
            if (motionProcess.Login(1))
            {
                if (inferDisable_btn.Text == "屏蔽推理机")
                {
                    inferDisable_btn.Text = "打开屏蔽";
                    MotionProcess.IsCloseInfer = true;
                    //MotionProcess.m_bWhileSeparateToWholeNames = false;
                    //  MotionProcess.m_bWhileSeparateToWholeNames1 = false;
                    //  MotionProcess.m_bWhileSeparateToWholeNames2 = false;
                    //  MotionProcess.m_bWhileIsSendReasoningMachine = true;
                }
                else
                {
                    MotionProcess.Pose_result1.Clear();
                    MotionProcess.Pose_result2.Clear();
                    MotionProcess.Pose_result3.Clear();
                    MotionProcess.Pose_result4.Clear();
                    MotionProcess.Pose_result5.Clear();
                    MotionProcess.Pose_result6.Clear();
                    MotionProcess.Pose_result7.Clear();
                    MotionProcess.m_listSampleIDs.Clear();
                    MotionProcess.m_arrPositionFilnames[0].Clear();
                    MotionProcess.m_arrPositionFilnames[1].Clear();
                    MotionProcess.m_arrPositionFilnames[2].Clear();
                    MotionProcess.m_arrPositionFilnames1[0].Clear();
                    MotionProcess.m_arrPositionFilnames1[1].Clear();
                    MotionProcess.m_arrPositionFilnames2[0].Clear();
                    MotionProcess.m_arrPositionFilnames2[1].Clear();
                    MotionProcess.IsCloseInfer = false;
                    inferDisable_btn.Text = "屏蔽推理机";
                    //MotionProcess.m_bWhileSeparateToWholeNames = true;
                    //MotionProcess.m_bWhileSeparateToWholeNames1 = true;
                    //MotionProcess.m_bWhileSeparateToWholeNames2 = true;
                    //MotionProcess.m_bWhileIsSendReasoningMachine = false;
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //    #region 二维码序列化参数
            //    snnumber.QrcodeBoby1 = MotionProcess.Qrcode1;
            //    snnumber.QrcodeBoby2 = MotionProcess.Qrcode2;
            //    snnumber.QrcodeBoby3 = MotionProcess.Qrcode3;
            //    snnumber.QrcodeBoby4 = MotionProcess.Qrcode4;
            //    snnumber.QrcodeBobyTemple = MotionProcess.QrcodeTemple;
            //    snnumber.QrcodeConnectNRead = MotionProcess.QrcodeConnectNRead;
            //    snnumber.QrcodeConnectNReadTemple = MotionProcess.QrcodeConnectNReadTemple;
            //    snnumber.QrcodeConnectPRead = MotionProcess.QrcodeConnectPRead;
            //    snnumber.QrcodeConnectPReadTemple = MotionProcess.QrcodeConnectPReadTemple;
            //    snnumber.numFlag = MvClass.numFlag;
            //    snnumber.SampleIDFlag = MotionProcess.SampleIDFlag;
            //    snnumber.m_listSampleIDs = MotionProcess.m_listSampleIDs;
            //    #endregion
            //    #region 推理机结果序列化参数
            //    Inferesultdata.Pose_result1 = MotionProcess.Pose_result1;
            //    Inferesultdata.Pose_result2 = MotionProcess.Pose_result2;
            //    Inferesultdata.Pose_result3 = MotionProcess.Pose_result3;
            //    Inferesultdata.Pose_result4 = MotionProcess.Pose_result4;
            //    Inferesultdata.Pose_result5 = MotionProcess.Pose_result5;
            //    Inferesultdata.Pose_result6 = MotionProcess.Pose_result6;
            //    Inferesultdata.Pose_result7 = MotionProcess.Pose_result7;
            //    Inferesultdata.m_ProductionResultId = MotionProcess.m_ProductionResultId;
            //    Inferesultdata.m_ProductionResultcode = MotionProcess.m_ProductionResultcode;
            //    Inferesultdata.m_ProductionResultIdToMoveFile = MotionProcess.m_ProductionResultIdToMoveFile;
            //    Inferesultdata.m_ProductionResultcodeToMoveFile = MotionProcess.m_ProductionResultcodeToMoveFile;
            //    #endregion
            //    XmlHelper.SerializeToXml(ConfigVars.configInfo);
            //    //HalconOperator.halconOperatorList.Clear();//0707
            //    string SnNumberIn = "SnNumberInfo";
            //    XmlHelper01.SerializeToXml(snnumber, SnNumberIn);
            //    XmlHelper01.SerializeToXml(Inferesultdata, "InferResultData");
            MotorsClass.omronInstance.Dispose();
        }

        private void 表格生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateEXl createEXl = new CreateEXl();
            createEXl.ShowDialog();
        }

        private void 设备精度点检ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccuracyCheckFrm acf = new AccuracyCheckFrm();
            acf.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //if (button1.Text=="点检中。。")
            //{
            //   MotionProcess.AccCheck = false ;
            //    MvClass.numFlag[0] = 10;
            //    MvClass.numFlag[1] = 10;
            //    MvClass.numFlag[2] = 10;
            //    MvClass.numFlag[3] = 10;
            //    MotionProcess.m_listMvCameras[0].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 5);
            //    MotionProcess.m_listMvCameras[1].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 0);
            //    MotionProcess.m_listMvCameras[2].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 0);
            //    MotionProcess.m_listMvCameras[3].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 12);
            //    button1.Text = "B点检";
            //}
            //else
            //{
            if (motionProcess.Login(1))
            {
                button1.Text = "B点检中。。";
                MotionProcess.m_listMvCameras[0].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 10);
                MotionProcess.m_listMvCameras[1].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 10);
                MotionProcess.m_listMvCameras[2].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 0);//  2
                MotionProcess.m_listMvCameras[2].m_MyCamera.MV_CC_SetFloatValue_NET("ExposureTime", 5000);
                MotionProcess.m_listMvCameras[3].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 16);
                MotionProcess.AccCheck = true;
                button1.Enabled = false;
            }
            //}
          


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (motionProcess.Login(1))
            {
                button2.Text = "A点检中。。";
                MotionProcess.m_listMvCameras[0].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 10);
                MotionProcess.m_listMvCameras[1].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 10);
                MotionProcess.m_listMvCameras[2].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 0);//  2
                MotionProcess.m_listMvCameras[2].m_MyCamera.MV_CC_SetFloatValue_NET("ExposureTime", 2000);
                MotionProcess.m_listMvCameras[3].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 16);
                MotionProcess.AccCheckA = true;
                button2.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MotionProcess.AccCheck = false;
            MotionProcess.AccCheckA = false;
            button1.Enabled = true ;
            button2.Enabled = true ;
            button1.Text = "B点检";
            button2.Text = "A点检";
            MvClass.numFlag[0] = 10;
            MvClass.numFlag[1] = 10;
            MvClass.numFlag[2] = 10;
            MvClass.numFlag[3] = 10;
            MotionProcess.m_listMvCameras[0].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 15);
            MotionProcess.m_listMvCameras[1].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 0);
            MotionProcess.m_listMvCameras[2].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 0);
            MotionProcess.m_listMvCameras[3].m_MyCamera.MV_CC_SetFloatValue_NET("Gain", 12);
            MotionProcess.m_listMvCameras[2].m_MyCamera.MV_CC_SetFloatValue_NET("ExposureTime", 5000);
        }

        private void bt_alarmReset_Click(object sender, EventArgs e)
        {
           // MotionProcess.Loseimage = true;
            MvClass.numFlag[0] = 10;
            MvClass.numFlag[1] = 10;
            MvClass.numFlag[2] = 10;
            MvClass.numFlag[3] = 10;
            MotionProcess.Pose_result1.Clear();
            MotionProcess.Pose_result2.Clear();
            MotionProcess.Pose_result3.Clear();
            MotionProcess.Pose_result4.Clear();
            MotionProcess.Pose_result5.Clear();
            MotionProcess.Pose_result6.Clear();
            MotionProcess.Pose_result7.Clear();
            MotionProcess.m_listSampleIDs.Clear();
            MotionProcess.m_arrPositionFilnames[0].Clear();
            MotionProcess.m_arrPositionFilnames[1].Clear();
            MotionProcess.m_arrPositionFilnames[2].Clear();
            MotionProcess.m_arrPositionFilnames1[0].Clear();
            MotionProcess.m_arrPositionFilnames1[1].Clear();
            MotionProcess.m_arrPositionFilnames2[0].Clear();
            MotionProcess.m_arrPositionFilnames2[1].Clear();
            MotionProcess.Qrcode1.Clear();
            MotionProcess.Qrcode2.Clear();
            MotionProcess.Qrcode3.Clear();
            MotionProcess.Qrcode4.Clear();
            MotionProcess.QrcodeTemple.Clear();
            MotionProcess.QrcodeTemple.Add("1234");
            MotionProcess.QrcodeConnectNRead.Clear();
            MotionProcess.QrcodeConnectNReadTemple.Clear();
            MotionProcess.QrcodeConnectNReadTemple.Add("134");
            MotionProcess.QrcodeConnectPRead.Clear();
            MotionProcess.QrcodeConnectPReadTemple.Clear();
            MotionProcess.QrcodeConnectPReadTemple.Add("134");
            MotionProcess.m_ProductionResultId.Clear();
            MotionProcess.m_ProductionResultcode.Clear();
            MotionProcess.m_Conncetion1Resultcode.Clear();
            MotionProcess.m_Conncetion1ResultId.Clear();
            MotionProcess.m_ConncetionResultcode.Clear();
            MotionProcess.m_ConncetionResultId.Clear();
            MotionProcess.m_ProductionResultIdToMoveFile.Clear();
            MotionProcess.m_ProductionResultcodeToMoveFile.Clear();
            MotionProcess.ConnectProductionDataResultIdToMoveFile.Clear();
            MotionProcess.ConnectProductionDataResultIdToMoveFile1.Clear();
            MotionProcess.ConnectProductionDataResultCodeToMoveFile.Clear();
            MotionProcess.ConnectProductionDataResultCodeToMoveFile1.Clear();
            MotionProcess.FixtureNumber.Clear();
            MotionProcess.FixtureNumberToMes.Clear();
            // MotionProcess.FixtureNumberToXml.Clear();
            MotionProcess.m_ConncetionResultIdToExl1.Clear();
            MotionProcess.m_ConncetionResultIdToExl.Clear();
            MotionProcess.m_ConncetionResultIdTodataGridView.Clear();
            MotionProcess.m_ConncetionResultIdTodataGridView1.Clear();
            MotionProcess.m_arrPositionFilnames[0].Clear();
            MotionProcess.m_arrPositionFilnames[1].Clear();
            MotionProcess.m_arrPositionFilnames[2].Clear();
            MotionProcess.m_arrPositionFilnames1[0].Clear();
            MotionProcess.m_arrPositionFilnames1[1].Clear();
            MotionProcess.m_arrPositionFilnames2[0].Clear();
            MotionProcess.m_arrPositionFilnames2[1].Clear();
            for (int i = 0; i < 7; i++)
            {
                MotionProcess.sample_id_num[i] = 0;
            }
            for (int j = 0; j < 7; j++)
            {
                MotionProcess.SampleIDFlag[j] = 5;
            }
            ResetAlarmevent();
            LogHelper.LogInfo("复位完成");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (motionProcess.Login(1))
            {
                if (button4.Text == "屏蔽连接器")
                {
                    MotionProcess.IsOpenConectedServer = false;
                    button4.Text = "打开屏蔽连接器";
                }
                else
                {
                    button4.Text = "屏蔽连接器";
                    MotionProcess.IsOpenConectedServer = true;
                }
            }
           
        }

        private void btn_restartProduction_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否重复跑料", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                MotionProcess.QrcodeTemple.Clear();
                MotionProcess.listtest00.Clear();
                MotionProcess.QrcodeTemple.Add("123");
                MotionProcess.listtest00.Add("123");

            }
        }
        public void SaveTheResultInfoToXml()
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

            CreatTheExl.ProductionDataIdToExl = MotionProcess.ProductionDataIdToExl;
            CreatTheExl.ProductionDataResultScoreToExl = MotionProcess.ProductionDataResultScoreToExl;
            CreatTheExl.ProductionDataResultCodeToExl = MotionProcess.ProductionDataResultCodeToExl;
            CreatTheExl.ConnectProductionDataResultIdToXmlExl = MotionProcess.ConnectProductionDataResultIdToXmlExl;
            CreatTheExl.ConnectProductionDataResultIdToXmlExl1 = MotionProcess.ConnectProductionDataResultIdToXmlExl1;
            CreatTheExl.ConnectProductionDataResultCodeToXmlExl = MotionProcess.ConnectProductionDataResultCodeToXmlExl;
            CreatTheExl.ConnectProductionDataResultCodeToXmlExl1 = MotionProcess.ConnectProductionDataResultCodeToXmlExl1;
            CreatTheExl.FixtureNumberToXml = MotionProcess.FixtureNumberToXml;
            XmlHelper02.SerializeToXml(CreatTheExl, "CreateTheExl" , day_Path);
            XmlHelper02.SerializeToXml(CreatTheExl, "CreateTheExl"+ DateTime.Now.ToString("yyyyMMddHHmmss"), day_Path);
  
        }

        public void CreatTheEmptyInfoXml()
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
                if (!File.Exists(day_Path+ "/CreateTheExl.xml"))
                {
                    List<string> ProductionDataIdToExl = new List<string>();
                    List<float> ProductionDataResultScoreToExl = new List<float>();
                    List<string> ProductionDataResultCodeToExl = new List<string>();
                    List<string> ConnectProductionDataResultIdToXmlExl = new List<string>();
                    List<string> ConnectProductionDataResultIdToXmlExl1 = new List<string>();
                    List<string> ConnectProductionDataResultCodeToXmlExl = new List<string>();
                    List<string> ConnectProductionDataResultCodeToXmlExl1 = new List<string>();
                    List<string> FixtureNumberToXml = new List<string>();
                    CreateTheExl creattheexl = new CreateTheExl();
                    creattheexl.ProductionDataIdToExl = ProductionDataIdToExl;
                    creattheexl.ProductionDataResultScoreToExl = ProductionDataResultScoreToExl;
                    creattheexl.ProductionDataResultCodeToExl = ProductionDataResultCodeToExl;
                    creattheexl.ConnectProductionDataResultIdToXmlExl = ConnectProductionDataResultIdToXmlExl;
                    creattheexl.ConnectProductionDataResultIdToXmlExl1 = ConnectProductionDataResultIdToXmlExl1;
                    creattheexl.ConnectProductionDataResultCodeToXmlExl = ConnectProductionDataResultCodeToXmlExl;
                    creattheexl.ConnectProductionDataResultCodeToXmlExl1 = ConnectProductionDataResultCodeToXmlExl1;
                    creattheexl.FixtureNumberToXml = FixtureNumberToXml;
                    XmlHelper02.SerializeToXml(creattheexl, "CreateTheExl", day_Path);
                }
               
            }
            string[] str1 = Directory.GetFiles(day_Path);
            if (str1.Count()==0)
            {
                List<string> ProductionDataIdToExl = new List<string>();
                List<float> ProductionDataResultScoreToExl = new List<float>();
                List<string> ProductionDataResultCodeToExl = new List<string>();
                List<string> ConnectProductionDataResultIdToXmlExl = new List<string>();
                List<string> ConnectProductionDataResultIdToXmlExl1 = new List<string>();
                List<string> ConnectProductionDataResultCodeToXmlExl = new List<string>();
                List<string> ConnectProductionDataResultCodeToXmlExl1 = new List<string>();
                List<string> FixtureNumberToXml = new List<string>();
                CreateTheExl creattheexl = new CreateTheExl();
                creattheexl.ProductionDataIdToExl = ProductionDataIdToExl;
                creattheexl.ProductionDataResultScoreToExl = ProductionDataResultScoreToExl;
                creattheexl.ProductionDataResultCodeToExl = ProductionDataResultCodeToExl;
                creattheexl.ConnectProductionDataResultIdToXmlExl = ConnectProductionDataResultIdToXmlExl;
                creattheexl.ConnectProductionDataResultIdToXmlExl1 = ConnectProductionDataResultIdToXmlExl1;
                creattheexl.ConnectProductionDataResultCodeToXmlExl = ConnectProductionDataResultCodeToXmlExl;
                creattheexl.ConnectProductionDataResultCodeToXmlExl1 = ConnectProductionDataResultCodeToXmlExl1;
                creattheexl.FixtureNumberToXml = FixtureNumberToXml;
                XmlHelper02.SerializeToXml(creattheexl, "CreateTheExl", day_Path);
            }
            else
            {
                foreach (var file in str1)
                {
                    if (!file.Contains("CreateTheExl"))
                    {
                        List<string> ProductionDataIdToExl = new List<string>();
                        List<float> ProductionDataResultScoreToExl = new List<float>();
                        List<string> ProductionDataResultCodeToExl = new List<string>();
                        CreateTheExl creattheexl = new CreateTheExl();
                        creattheexl.ProductionDataIdToExl = ProductionDataIdToExl;
                        creattheexl.ProductionDataResultScoreToExl = ProductionDataResultScoreToExl;
                        creattheexl.ProductionDataResultCodeToExl = ProductionDataResultCodeToExl;
                        XmlHelper02.SerializeToXml(creattheexl, "CreateTheExl", day_Path);
                    }
                }
            }
            


        }


        private void Test_TC_Server_State()
        {
            AlgoSampleCommitRequest_NVT TestServer = new AlgoSampleCommitRequest_NVT();
            SampleResultData_NVT Test_result = new SampleResultData_NVT();
            HttpQuery hq = new HttpQuery();
            string Client_IP = "192.168.250.100";
            int Cilent_Port = 80;
            //TestServer.relative_dir = "test/watch";
            TestServer.relative_dir = "testwatch";
            List<string> lis = new List<string>();
            lis.Add("WatchDetect_FQ1126401GV14M92U1122_S000001_P01_C00_L0_2022110521563553.jpg");
            lis.Add("WatchDetect_FQ1126401GV14M92U1122_S000001_P01_C00_L1_2022110521563565.jpg");
            lis.Add("WatchDetect_FQ1126401GV14M92U1122_S000001_P01_C00_L2_2022110521563576.jpg");
            lis.Add("WatchDetect_FQ1126401GV14M92U1122_S000001_P01_C00_L3_2022110521563586.jpg");
            lis.Add("WatchDetect_FQ1126401GV14M92U1122_S000001_P01_C00_L4_2022110521563596.jpg");
            //lis.Add("WatchDetect_FQ12343HUNQ1MDK4Z1227_S000170_P01_C00_L0_2022081717000766.jpg");
            //lis.Add("WatchDetect_FQ12343HUNQ1MDK4Z1227_S000170_P01_C00_L1_2022081717000779.jpg");
            //lis.Add("WatchDetect_FQ12343HUNQ1MDK4Z1227_S000170_P01_C00_L2_2022081717000787.jpg");
            //lis.Add("WatchDetect_FQ12343HUNQ1MDK4Z1227_S000170_P01_C00_L3_2022081717000795.jpg");
            //lis.Add("WatchDetect_FQ12343HUNQ1MDK4Z1227_S000170_P01_C00_L4_2022081717000803.jpg");
            TestServer.file_names = lis;
            Test_result = hq.CommitSample00(Client_IP, Cilent_Port, TestServer);
            if (Test_result == null || Test_result.data.res == null)
            {
                System.Windows.MessageBox.Show("服务器服务没打开请重启服务器");
            }
            //else
            //{
            //    MessageBox.Show("服务器服务已开启");
            //}



        }

        private void button5_Click(object sender, EventArgs e)
        {
            Test_TC_Server_State();

        }

        private void dgv_connect1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Text== "二维码位数：21")
            {
                MotionProcess.TheLengthQrcode = 13;
               checkBox2.Text = "二维码位数：26";
            }
            else
            {
                MotionProcess.TheLengthQrcode = 11;
                checkBox2.Text = "二维码位数：21";
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Alarmevent();
        }

        string FilePath = "C:\\tc2\\data";
        string FilePath1 = "C:\\Connection\\data\\00";
        string FilePath2 = "C:\\Connection\\data\\01";
        private void DelelPhoto()
        {
            string [] str = Directory.GetFiles(FilePath);
            if (str.Count()>0)
            {
                foreach (var item in str)
                {
                    File.Delete(item);
                }
               
            }
            string[] str1 = Directory.GetFiles(FilePath1);
            if (str1.Count() > 0)
            {
                foreach (var item in str1)
                {
                    File.Delete(item);
                }

            }
            string[] str2 = Directory.GetFiles(FilePath2);
            if (str2.Count() > 0)
            {
                foreach (var item in str2)
                {
                    File.Delete(item);
                }

            }

        }
    }
}



