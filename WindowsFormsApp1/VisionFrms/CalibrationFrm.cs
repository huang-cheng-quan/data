
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Threading;
using System.IO;
using BaseForm;
using Camera_Capture_demo.HalconVision;
using WindowsFormsApp1.Models;
using Camera_Capture_demo.GlobalVariable;
using Camera_Capture_demo.Helpers;
using System.Net.Sockets;
using BatteryFeederDemo.VisionFrms;
using WindowsFormsApp1;
using WindowsFormsApp1.TcpTest;
using Dyestripping.Models;
using Camera_Capture_demo.Models;
using BatteryFeederDemo;

namespace Camera_Capture_demo.VisionFrms
{
    public partial class CalibrationFrm : ZoomForm
    {
        HObject m_hImage;
        HWindow m_hWindow;       
        int m_icamNo;//相机序号
        int MotorNo;
        int m_imotorNumber;
        MvClass mvclass;
        BindingList<CoordinatePair> m_datalist = new BindingList<CoordinatePair>();
        MotionProcess motionprocess;
        MotorsClass motorsInstance;
        ProcessPlcData processplcdata = new ProcessPlcData();

        string plc_cam_status = "D1000";////读写 0待机 1拍照 2拍照OK 3拍照NG 4拍照异常
        float? cam_pos;
        public CalibrationFrm(int camNo,int motorNum)
        {
            InitializeComponent();
            this.m_icamNo = camNo;
            this.Name += camNo;
            this.m_imotorNumber = motorNum;
            this.MotorNo = 0;
        }
        private void CalibrationFrm_Load(object sender, EventArgs e)
        {                        
            dataGridView1.DataSource = m_datalist;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            m_hWindow = hWindowControl1.HalconWindow;  
            mvclass = MvClass.GetInstance(m_icamNo);
            motorsInstance = new MotorsClass(MotorNo);
            mvclass.eventProcessImage += Mv_eventProcessImage;

        }    
        private void Mv_eventProcessImage(HObject hImage, int motorNum)
        {
            this.m_hImage = hImage.Clone();
            HalconOperator.ShowImage(hWindowControl1, hImage);
        }
        private void CalibrationFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mvclass != null)
            {
                mvclass.StopGrabbing();
                //basler.CloseCam();
                mvclass.Clear_EventProcessImage_Event();
            }
            if (m_hImage != null)
            {
                m_hImage.Dispose();
            }
            if (m_hWindow != null)
            {
                m_hWindow.Dispose();
            }
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewRow CurrentRow = dataGridView1.Rows[e.RowIndex];
            CurrentRow.HeaderCell.Value = Convert.ToString(e.RowIndex + 1);
            CurrentRow.HeaderCell.ToolTipText = "示教点" + Convert.ToString(e.RowIndex + 1);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 3)
            {
                return;
            }
            PointXYU p = new PointXYU();
            if (m_imotorNumber != 0)
            {
                p = motorsInstance.GetCurrentPos(m_imotorNumber);
            }
            else
            {
                p = motorsInstance.GetCurrentPos(m_icamNo);
            }
            
            if (p != null)
            {
                m_datalist[e.RowIndex].WorldPoint = new PointF(p.X, p.Y);
                dataGridView1.InvalidateRow(e.RowIndex);
            }
        }
        private void btnTakePic_Click(object sender, EventArgs e)
        {
            if (!mvclass.isOpen)
            {
                mvclass.OpenCam(m_icamNo);
            }
            //MotionProcess.omronInstance1.Write(plc_cam_status4, 0);
            //mvclass.OneShot();


        }
        private void btnOpenImg_Click(object sender, EventArgs e)
        {
            HOperatorSet.GenEmptyObj(out m_hImage);
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    HOperatorSet.ReadImage(out m_hImage, fileDialog.FileName);
                    HObject hImageClone = m_hImage.Clone();
                    HalconOperator.ShowImage(hWindowControl1, hImageClone);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void btnCreateCalibModel_Click(object sender, EventArgs e)
        {
            CalibDataSettingFrm frm = new CalibDataSettingFrm();
            frm.Show();
        }
        private void hWindowControl1_MouseMove(object sender, MouseEventArgs e)
        {
            int leftBorder = hWindowControl1.Location.X;
            int rightBorder = hWindowControl1.Location.X + hWindowControl1.Size.Width;
            int topBorder = hWindowControl1.Location.Y;
            int bottomBorder = hWindowControl1.Location.Y + hWindowControl1.Size.Height;
            if (m_hImage != null)
            {
                try
                {
                    HOperatorSet.GetImageSize(m_hImage, out HTuple width, out HTuple height);
                    if (e.X > leftBorder && e.X < rightBorder && e.Y > topBorder && e.Y < bottomBorder)
                    {
                        m_hWindow.GetMposition(out int row, out int col, out int button);
                        if(row < height && col < width)
                        {
                            HOperatorSet.GetGrayval(m_hImage, row, col, out HTuple grayval);
                            txtPixelLocation.Text = "当前坐标：  R↓：" + row.ToString() + "  C→: " + col.ToString() + "  灰度值  " + grayval.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// 寻找标定点按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFindCalibP_Click(object sender, EventArgs e)
        {
            if (m_hImage == null)
                return;
            try
            {
                HOperatorSet.Threshold(m_hImage, out HObject hRegion, 0, 100);
                HOperatorSet.Connection(hRegion, out HObject connectedRegions);
                HOperatorSet.SelectShape(connectedRegions, out HObject selectedRegions, new string[] { "area", "circularity" }, "and", new double[] { 1000, 0.6 }, new double[] { 100000, 1 });
                HOperatorSet.SortRegion(selectedRegions, out HObject sortedRegions, "character", "true", "row");
                m_hWindow.SetColor("red");
                m_hWindow.DispObj(sortedRegions);
                HOperatorSet.AreaCenter(sortedRegions, out HTuple areas, out HTuple rows, out HTuple cols);
                m_hWindow.DispCross(rows, cols, 6, 0);
                m_datalist.Clear();
                for (int i = 0; i < areas.Length; i++)
                {
                    m_hWindow.DispText(i + 1, "image", rows[i], cols[i], "black", new HTuple(), new HTuple());
                    //将找到的标志中心点像素坐标写入表格
                    m_datalist.Add(new CoordinatePair()
                    {
                        PairNo = i + 1,
                        PixelPoint = new PointF((float)Math.Round(rows[i].F, 2), (float)Math.Round(cols[i].F, 2)),
                        WorldPoint = new PointF(),
                        GetRobotPositionFlag = WindowsFormsApp1.Properties.Resources.robot_24px_1172732_easyicon_net
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// 保存标定结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveMat2D_Click(object sender, EventArgs e)
        {

            cam_pos = 0;
            if (MessageBox.Show("是否保存标定结果?", "", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            List<double> px = new List<double>();
            List<double> py = new List<double>();
            List<double> qx = new List<double>();
            List<double> qy = new List<double>();

            try
            {
                foreach (CoordinatePair pair in m_datalist)
                {
                    px.Add(pair.PixelPoint.X);
                    py.Add(pair.PixelPoint.Y);
                    qx.Add(pair.WorldPoint.X);
                    qy.Add(pair.WorldPoint.Y);
                }
                HOperatorSet.VectorToHomMat2d(px.ToArray(), py.ToArray(), qx.ToArray(), qy.ToArray(), out HTuple homMat2D);

                string dir = Application.StartupPath + "\\Calibration";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                if (m_imotorNumber != 0)
                {
                    HOperatorSet.WriteTuple(homMat2D, dir + $"\\Cam{m_icamNo}_homMat2D");
                }
                else
                {
                    HOperatorSet.WriteTuple(homMat2D, dir + $"\\Cam{m_imotorNumber}_homMat2D");
                }
               
                
                if (homMat2D != null)
                {
                   /* ConfigVars.configInfo.ToolInfos.CamPositionOnCalib = (float)cam_pos;
                    XmlHelper.SerializeToXml(ConfigVars.configInfo);*/
                    MessageBox.Show("标定信息保存成功");
                }
                else
                {
                    MessageBox.Show("没有有效的拍照点坐标");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void btnJog_Click(object sender, EventArgs e)
        {
           /* if (keyenceInstance == null)
            {
                keyenceInstance = KeyencePlcFactory.GetInstance();
            }*/
            string name = (sender as Button).Name;
            int channel, bit;
            switch (name.Substring(6, 1))
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
            switch (name.Substring(7, 1))
            {
                case "X":
                    channel = 340;
                    break;
                case "Y":
                    channel = 350;
                    break;
                case "Z":
                    channel = 360;
                    break;
                case "R":
                    channel = 370;
                    break;
                default:
                    channel = 0;
                    break;
            }
           /* if (channel != 0)
            {
                int address = channel * 16 + bit;
                keyenceInstance.Write($"M{address}", true);
                Thread.Sleep(50);
                keyenceInstance.Write($"M{address}", false);
            }*/
        }
        /// <summary>
        /// 获取电机组坐标填入对应的坐标对行，并存储当前的相机位置
        /// </summary>
        /// <returns></returns>     
        class CoordinatePair
            {
                [DisplayName("点对序号")]
                public int PairNo { get; set; }
                [DisplayName("像素坐标")]
                public PointF PixelPoint { get; set; }
                [DisplayName("机械手坐标")]
                public PointF WorldPoint { get; set; }
                [DisplayName(" ")]
                public Bitmap GetRobotPositionFlag { get; set; }
            }

    }
    
}
