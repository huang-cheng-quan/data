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
       
        

        SetTime setTime = new SetTime();
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

        
        public MainForm()
        {
            InitializeComponent();
            // prepareData(1);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

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
            XmlHelper.SerializeToXml(ConfigVars.configInfo);
            //motionProcess = new MotionProcess();

            #region 事件注册
            LogHelper.LogDisp += LogHelper_LogDisp;
            MotionProcess.event_OnImgDisp += MotionProcess_OnImgDisp;
            MotionProcess.event_OnPositionDisp += MotionProcess_OnPositionDisp;
            MotionProcess.event_LabelDisplay += MotionProcess_OnLabelDisp;
            MotionProcess.event_ProcessSampleResultData_NVT += ProcessSampleResultData_NVT_Data;
            MotionProcess.event_ProcessSampleResultData2_NVT += ProcessSampleResultData2_NVT_Data;
            MotionProcess.event_QrCodeShow += MotionProcess_eventQrcodeShow;
            MvClass.eventProcessData += MotionProcess_eventProcessImageShow;
            
            #endregion
            m_hDisplayHandle = new IntPtr[MotionProcess.m_nCameraTotalNum];

            motionProcess = MotionProcess.GetInstance();
            motionProcess.Init();
        }

        #region 显示委托
        private void ProcessSampleResultData_NVT_Data(SampleResultData_NVT Srdn)
        {
            motionProcess.Show_image2Picbox(Srdn, pic_1, pic_2, pic_3, pic_4, pic_5, pic_6, pic_7, picShow);
        }
        private void ProcessSampleResultData2_NVT_Data(SampleResultData2_NVT srdn2)
        {
            motionProcess.Show_Result2Table(srdn2, dataGridView2, 1);
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
            this.BeginInvoke(new MethodInvoker(() =>
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
            }));
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
                label38.Text = code;
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
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            setTime.DeleteFile(OriginalImagesSaveFilePath, m_iOriginalImagesSaveTime);  //删除原始照片超过天数的文件
            setTime.DeleteFile(CompressionImagesSaveFilePath, m_iCompressionImagesSaveTime);  //删除压缩照片超过天数的文件
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
            m_hDisplayHandle[9] = pictureBox9.Handle;
            //motionProcess = new MotionProcess();
            
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
                    btn_Start.Text = "停止";
                }
            }
            else
            {
                motionProcess.abort = true;
                btn_Start.Text = "启动";
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

        }
        private void 坐标标定钢片检测AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            motionProcess = new MotionProcess();

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
            motionProcess = new MotionProcess();

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
            motionProcess = new MotionProcess();
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
    }
}



