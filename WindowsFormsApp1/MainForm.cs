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

namespace WindowsFormsApp1
{
    public partial class MainForm : ZoomForm
    {
        //载入配置
        List<HalconOperator> halconOperatorList;
        //和PLC的连接    
        MotionProcess motionProcess;
        //整体变量
        int m_CamerNum = 8;
        //TcpSocket类接口
        public delegate void delegateAcceptePLCMessage1(string AccepteMessage);
        public event delegateAcceptePLCMessage1 EventAcceptePLCMessage1;
        public delegate void delegateAcceptePLCMessage2(string AccepteMessage);
        public event delegateAcceptePLCMessage2 EventAcceptePLCMessage2;
        //深度学习变量
        string m_sModel_Path_P1 = "D:/halcon_DeepLearning/Watch_deep_segment_data/model_backupdata/20219271519";
        string m_sModel_Path_P2_P6 = "D:/halcon_DeepLearning/Watch_deep_segment_data/model_backupdata/20219261653";
        HTuple m_hv_DLModelHandle_P01 = new HTuple();
        HTuple m_hv_DLModelHandle_P2_P6 = new HTuple();
        string Model_Path = "";
        HTuple m_hv_DatasetInfo = new HTuple();
        HWindow m_hwWin1 = new HWindow();
        HalonWinow hw = new HalonWinow();
        HalconInfer hi = new HalconInfer();

        
        public MainForm()
        {
            InitializeComponent();
           // prepareData(1);
        }
        private bool Login(int level)
        {
            LoginFrm frm = new LoginFrm(level);
            return (frm.ShowDialog() == DialogResult.OK);
        }
        private bool CheckHImageFrmIsOpen()
        {
            return WindowHelper.CheckFormIsOpen("CalibrationFrm0") || WindowHelper.CheckFormIsOpen("CalibrationFrm1")
                || WindowHelper.CheckFormIsOpen("CreateShapeModelFrm");
        }
        private void MotionProcess_OnImgDisp(HObject hImage, int camNo)
        {
            if (hImage == null)
            {
                return;
            }
            if (halconOperatorList == null)
            {
                halconOperatorList = new List<HalconOperator>();
                for (int i = 0; i < 2; i++)
                {
                    halconOperatorList.Add(HalconOperator.GetInstance(i + 1));
                }
            }
            this.BeginInvoke(new MethodInvoker(() =>
            {
                HObject ho_Image = hImage.Clone();
                switch (camNo)
                {
                    case 1:
                        HalconOperator.ShowImage(hWindowControl1, hImage);
                        halconOperatorList[0].FindShapeModel(hWindowControl1.HalconWindow, ho_Image, out _, out List<PointXYU> pointXYUs1);
                        motionProcess.GetPointList(1, pointXYUs1);
                        break;
                    case 2:
                        HalconOperator.ShowImage(hWindowControl2, hImage);
                        halconOperatorList[1].FindShapeModel(hWindowControl2.HalconWindow, ho_Image, out _, out List<PointXYU> pointXYUs2);
                        motionProcess.GetPointList(2, pointXYUs2);
                        break;
                    default:
                        break;
                }
            }));
        }
        private void MotionProcess_OnPositionDisp(int toolNo, PointXYU pointXYU)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                //lblCurrentNo.Text = current.ToString();
                //lblTotalNum.Text = total.ToString();
                lblToolNo.Text = toolNo.ToString();
                if (pointXYU != null)
                {
                    lblPosition.Text = string.Format("X:{0}\nY:{1}\nU:{2}", pointXYU.X, pointXYU.Y, pointXYU.U);
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
        private void 相机设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Login(0))
            {
                WindowHelper.ShowOrActiveForm<MV_CameraSettingForm>("MV_CameraSettingForm");
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            #region 事件注册
            LogHelper.LogDisp += LogHelper_LogDisp;
            MotionProcess.OnImgDisp += MotionProcess_OnImgDisp;
            MotionProcess.OnPositionDisp += MotionProcess_OnPositionDisp;
            #endregion
            ConfigVars.configInfo = XmlHelper.DeserializeFromXml<ConfigInfo>();
                
            if (ConfigVars.configInfo.UserInfos == null)
            {
                ConfigVars.configInfo.UserInfos = new UserInfos()
                {
                    OperatorPwd = "1",
                    AdministratorPwd = "1",
                    DeveloperPwd = "1"
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
            if (ConfigVars.configInfo.Cameras == null || ConfigVars.configInfo.Cameras.Count == 0)
            {
                List<MvClass> cameras = new List<MvClass>();
                for (int i = 0; i < 8; i++)
                {
                    cameras.Add(new MvClass() { CameraNo = i });
                }
                ConfigVars.configInfo.Cameras = cameras;
            }
            XmlHelper.SerializeToXml(ConfigVars.configInfo);
        }
        private void 标定设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 坐标标定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("CalibrationFrm0"))
                {
                    CalibrationFrm frm = new CalibrationFrm(0);  
                    frm.Show();
                }
            }
        }      
        private void btn_FeedBelt_Click(object sender, EventArgs e)
        {
            if (btn_FeedBelt.Text == "启动")
            {
                if (motionProcess == null || !motionProcess.is_init)
                {
                    /*motionProcess = MotionProcess.GetInstance();*/
                    motionProcess.Init();
                }
                if (CheckHImageFrmIsOpen())
                {
                    MessageBox.Show("请先关闭设置窗口");
                    return;
                }
                if (motionProcess.is_init)
                {
                    motionProcess.abort = false;
                    btn_FeedBelt.Text = "停止";
                }
            }
            else
            {
                motionProcess.abort = true;
                btn_FeedBelt.Text = "启动";
            }
        }
        private void 坐标标定本体定位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("CalibrationFrm0"))
                {
                    CalibrationFrm frm = new CalibrationFrm(1);
                    frm.Show();
                }
            }
        }
        private void 模板建立ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (motionProcess != null && !motionProcess.abort)
            {
                MessageBox.Show("设备运行中,设置界面不可打开");
                return;
            }
            if (Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("CreateShapeModelFrm0"))
                {
                    CreateShapeModelFrm frm = new CreateShapeModelFrm(0);
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
            if (Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("CreateShapeModelFrm1"))
                {
                    CreateShapeModelFrm frm = new CreateShapeModelFrm(1);
                    frm.Show();
                }
            }
        }
        private void plc设置上料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Login(1))
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
            if (Login(1))
            {
                if (!WindowHelper.CheckFormIsOpen("OmronPlcSettingFrm1"))
                {
                    OmronPlcSettingFrm frm = new OmronPlcSettingFrm(1);
                    frm.Show();
                }
            }
        }
        public void  prepareData(int model_flag)

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
        private void btn_ResetFeedBelt_Click(object sender, EventArgs e)
        {
        }
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

   
}
