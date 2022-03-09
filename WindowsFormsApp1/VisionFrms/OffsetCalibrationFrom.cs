using BaseForm;
using Camera_Capture_demo.GlobalVariable;
using Camera_Capture_demo.HalconVision;
using Camera_Capture_demo.Helpers;
using Dyestripping.Models;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.VisionFrms
{
    public partial class OffsetCalibrationFrom : ZoomForm
    {
        MvClass mvclass = new MvClass();
        HObject ho_Image;
        private HWindow hWindow;
        int m_icamNo;
        HTuple hv_ModelID = new HTuple();
        HTuple ImageWidth = new HTuple(), ImageHeight = new HTuple();
        double ho_Rect, row1, column1, Phi, Len1, Len2;
        
        HTuple hv_distance = new HTuple();
        HTuple measureHandle = new HTuple();
        double OnePixelScale = 0;
        HWindow m_hWindow;
        MotorsClass motorsInstance;
        
        int m_imotorNumber;
        HalconOperator.ModelResult PixelPointResult = new HalconOperator.ModelResult();
        HalconOperator halconOperator ;
        public static Point CircleCenter;
        HTuple FitCircleCenterRow = new HTuple();
        HTuple FitCircleCenterCol = new HTuple();
        string CirclelParams_Path = "";
        bool m_bIsUseCircleCenter = false;
        HTuple hv_TmpCtrl_Row = new HTuple(), hv_TmpCtrl_Column = new HTuple(), hv_TmpCtrl_Dr = new HTuple(), hv_TmpCtrl_Dc = new HTuple(),
              hv_TmpCtrl_Phi = new HTuple(), hv_TmpCtrl_Len1 = new HTuple(), hv_TmpCtrl_Len2 = new HTuple();

        private void btn_SetPixelCoordinates_Click(object sender, EventArgs e)
        {
            halconOperator.OnlyFindShapModel(hWindowControl1.HalconWindow, ho_Image, out PixelPointResult);
            UpdateDataGridViewData(dataGridView1, PixelPointResult);


        }
        public OffsetCalibrationFrom(int camNo, int motorNum)
        {
            this.m_icamNo = camNo;
            this.m_imotorNumber = motorNum;
            halconOperator = new HalconOperator (camNo, motorNum);
            InitializeComponent();
            
        }

        private void btn_StartCalibration_Click(object sender, EventArgs e)
        {
            HObject ho_Image, ho_GrayImage, ho_Region;
            HObject ho_RegionOpening, ho_ConnectedRegions, ho_Contour;
            HObject ho_ContCircle;

            // Local control variables 

            HTuple hv_Area = new HTuple(), hv_Row = new HTuple();
            HTuple hv_Column = new HTuple(), hv_Row1 = new HTuple();
            HTuple hv_Column1 = new HTuple(), hv_Radius = new HTuple();
            HTuple hv_StartPhi = new HTuple(), hv_EndPhi = new HTuple();
            HTuple hv_PointOrder = new HTuple();
            HTuple ModelRows = new HTuple(), ModelCols = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_GrayImage);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Contour);
            HOperatorSet.GenEmptyObj(out ho_ContCircle);
           
            int step = 0;
            bool b_whStatus = true; 
            while (b_whStatus)
            {
                switch (step)
                {
                    case 0:
                        //让机械手回到模板点位
                        MotorsClass.omronInstance.Write(MotorsClass.plc_cam_status4, (float)0);
                        step = 1;
                        break;
                    case 1:
                        //确认拍照位置OK
                        for (int i = 0; i < int.Parse(txt_CalNum.Text); i++)
                        {
                            try
                            {
                                if (MotorsClass.omronInstance.ReadFloat(MotorsClass.plc_cam_status4).Content == 1)
                                {
                                    halconOperator.OnlyFindShapModel(hWindowControl1.HalconWindow, ho_Image, out PixelPointResult);
                                    HOperatorSet.GenCrossContourXld(out HObject cross, PixelPointResult.Row, PixelPointResult.Column, 36, 0);
                                    ModelRows.TupleConcat(PixelPointResult.Row);
                                    ModelCols.TupleConcat(PixelPointResult.Column);
                                    hWindow.DispObj(cross);
                                    UpdateDataGridViewData(dataGridView1, PixelPointResult);
                                    //让机械手转动指定度数
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_cam_status4, float.Parse(txt_Angle.Text));
                                    //发送拍照命令
                                    MotorsClass.omronInstance.Write(MotorsClass.plc_cam_status4, float.Parse(txt_Angle.Text));
                                }

                            }
                            catch (Exception)
                            {
                                step = 999;
                                
                            }
  
                        }
                   
                        break;
                    case 3:
                        //拟合圆

                        halconOperator.GenCircle(ModelRows, ModelCols, out ho_ContCircle, out hv_Row1, out hv_Column1);
                        FitCircleCenterRow = hv_Row1;
                        FitCircleCenterCol = hv_Column1;
                        HOperatorSet.GenCrossContourXld(out HObject cross1, hv_Row1, hv_Column1, 36, 0);
                        hWindow.SetColor("blue");
                        hWindow.DispObj(ho_ContCircle);
                        hWindow.SetColor("red");
                        hWindow.DispObj(cross1);
                        break;
                    case 999:
                        MessageBox.Show("标定错误");
                        break;
                    default:
                        break;
                }
            }
            
           
        }
        private void  UpdateDataGridViewData(DataGridView dataGridView, HalconOperator.ModelResult PixelPointResult1) 
        {
            int index = dataGridView.Rows.Add();
            dataGridView.Rows[index].Cells[0].Value = (index + 1).ToString();
            dataGridView.Rows[index].Cells[1].Value = PixelPointResult1.Row;
            dataGridView.Rows[index].Cells[2].Value = PixelPointResult1.Column;
            dataGridView.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
        }

        private void OffsetCalibrationFrom_Load(object sender, EventArgs e)
        {
            groupBox2.Enabled = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            hWindow = hWindowControl1.HalconWindow;
            trb_SetThreshold.Value = int.Parse(txt_ThresholdValue.Text);
            trb_setSigma.Value = int.Parse(txt_sigmalValue.Text);
            trb_setLineWidth.Value = int.Parse(txt_MeasureWidthValue.Text);

            mvclass = MvClass.GetInstance(m_icamNo);
            motorsInstance = new MotorsClass(m_imotorNumber);
            mvclass.eventProcessImage += Mv_eventProcessImage;
            string dir_path = Application.StartupPath + "\\Project\\" + ConfigVars.configInfo.ProductInfos.SelectProject + $"\\Circle{m_icamNo}";
            if (!Directory.Exists(dir_path))
            {
                Directory.CreateDirectory(dir_path);
            }
            CirclelParams_Path = dir_path + "\\CircleCenter.tup";
            
        }

        private void trb_SetThreshold_Scroll(object sender, EventArgs e)
        {
            Measure();
        }



        private void btnOpenImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            HOperatorSet.SetSystem("int_zooming", "false");
            if (result == DialogResult.OK)
            {
                try
                {
                    HOperatorSet.ReadImage(out ho_Image, fileDialog.FileName);
                    HOperatorSet.Rgb1ToGray(ho_Image, out ho_Image);
                    HObject hImage = ho_Image.Clone();
                    HalconOperator.ShowImage(hWindowControl1, ho_Image);
                    HOperatorSet.GetImageSize(ho_Image, out ImageWidth, out ImageHeight);
                    //ho_Image.Dispose();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void trb_setSigma_Scroll(object sender, EventArgs e)
        {
            Measure();
        }

        private void trb_setLineWidth_Scroll(object sender, EventArgs e)
        {
            Measure();
        }


        private void btn_GenCircleCenter_Click(object sender, EventArgs e)
        {
            HTuple Rows = GetDataGridViewData("Column2");
            HTuple Clos = GetDataGridViewData("Column3");
            HObject Cross;
            HOperatorSet.GenEmptyRegion(out Cross);

            for (int i = 0; i < Rows.Length; i++)
            {
                HOperatorSet.GenCrossContourXld(out HObject crossTmp, Rows[i], Clos[i], 12, 0);
                Cross = Cross.ConcatObj(crossTmp);
            }
            
            halconOperator.GenCircle(Rows, Clos, out HObject ho_ContCircle, out HTuple hv_Row1, out HTuple hv_Column1);
            FitCircleCenterRow = hv_Row1;
            FitCircleCenterCol = hv_Column1;
            HOperatorSet.GenCrossContourXld(out HObject cross1, hv_Row1, hv_Column1, 36, 0);
            hWindow.ClearWindow();
            hWindow.SetColor("green");
            hWindow.DispObj(Cross);
            hWindow.SetColor("blue");
            hWindow.DispObj(ho_ContCircle);
            hWindow.SetColor("red");
            hWindow.DispObj(cross1);
        }

        private HTuple GetDataGridViewData(string ColNum) 
        {
            HTuple listdata = new HTuple();
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                double data = double.Parse(dataGridView1.Rows[i].Cells[ColNum].Value.ToString());
                listdata = listdata.TupleConcat((HTuple)data);
            }
            return listdata;
        }

        private void btn_SaveCalibrationResult_Click(object sender, EventArgs e)
        {
            if (m_bIsUseCircleCenter)
            {
                if (tabControl1.SelectedIndex != 2)
                {
                    HTuple FitCircleParams = new HTuple();
                    FitCircleParams = FitCircleCenterRow.TupleConcat(FitCircleCenterCol);
                    HOperatorSet.WriteTuple(FitCircleParams, CirclelParams_Path);
                    MessageBox.Show("模板保存成功");
                }
                else
                {
                    HTuple FitCircleParams = new HTuple();
                    FitCircleCenterRow = double.Parse(txt_CircleCenterRow.Text);
                    FitCircleCenterCol = double.Parse(txt_CircleCenterColumn.Text);
                    HOperatorSet.WriteTuple(FitCircleParams, CirclelParams_Path);
                    MessageBox.Show("模板保存成功");
                }
            }
            else
            {
                
                FitCircleCenterRow = 0;
                FitCircleCenterCol = 0;
            }
            ConfigVars.configInfo.calibrationData[m_icamNo].MotorNum = m_imotorNumber;
            ConfigVars.configInfo.calibrationData[m_icamNo].SinglePixelAccuracy = double.Parse(lbl_OnePixelAccuracy.Text);
            ConfigVars.configInfo.calibrationData[m_icamNo].FitCenterRow = FitCircleCenterRow;
            ConfigVars.configInfo.calibrationData[m_icamNo].FitCenterColumn = FitCircleCenterCol;
            ConfigVars.configInfo.calibrationData[m_icamNo].ModelWordX = double.Parse(txt_modelWord_X.Text);
            ConfigVars.configInfo.calibrationData[m_icamNo].ModelWordY = double.Parse(txt_modelWord_Y.Text);
            ConfigVars.configInfo.calibrationData[m_icamNo].ModelWordU = double.Parse(txt_modelWord_U.Text);
            XmlHelper.SerializeToXml(ConfigVars.configInfo);
            MessageBox.Show("保存成功！");

        }

        private void btn_SetOnePixelAccuracy_Click(object sender, EventArgs e)
        {
            double OnePixelAccuracy = double.Parse(txt_WordDistance.Text)/ hv_distance.D;
            lbl_OnePixelAccuracy.Text = OnePixelAccuracy.ToString();
           


        }

        private void btn_IsFitCircleCenter_Click(object sender, EventArgs e)
        {
            {
               
                DialogResult MsgBoxResult;//设置对话框的返回值
                MsgBoxResult = MessageBox.Show("是否拟合圆心",//对话框的显示内容 
                "提示",//对话框的标题 
                MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                MessageBoxIcon.Exclamation,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                if (MsgBoxResult == DialogResult.Yes)
                {
                    groupBox2.Enabled = true;
                    m_bIsUseCircleCenter = true;
                }
                if (MsgBoxResult == DialogResult.No)
                {
                    groupBox2.Enabled = false;
                    m_bIsUseCircleCenter = false;
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewRow CurrentRow = dataGridView1.Rows[e.RowIndex];
            CurrentRow.HeaderCell.Value = Convert.ToString(e.RowIndex + 1);
            CurrentRow.HeaderCell.ToolTipText = "示教点" + Convert.ToString(e.RowIndex + 1);
        }
        private void btnTakePic_Click(object sender, EventArgs e)
        {
            if (!mvclass.isOpen)
            {
                mvclass.OpenCam(m_icamNo);
            }
        }
        private void Mv_eventProcessImage(HObject hImage, int motorNum)
        {
            this.ho_Image = hImage.Clone();
            HalconOperator.ShowImage(hWindowControl1, hImage);
        }
        private void btn_SelectModel_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            HOperatorSet.SetSystem("int_zooming", "false");
            if (result == DialogResult.OK)
            {
                try
                {
                    HOperatorSet.ReadShapeModel(fileDialog.FileName, out hv_ModelID);
                    
                    MessageBox.Show("模板加载成功!");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void btn_DrwaRect_Click(object sender, EventArgs e)
        {
           
            this.ControlBox = false;
            if (ho_Image == null)
            {
                MessageBox.Show("未打开需要处理的原图");
                return;
            }
            hWindow.DispObj(ho_Image);
            HalconOperator.DispMessage(hWindow, "鼠标左键按下开始ROI选取,\n松开鼠标结束,\n鼠标右键退出", "window", 12, 12, "yellow", "false");
            hWindow.SetDraw("margin");
            hWindow.SetColor("blue");
            hWindow.DrawLine(out double hv_LineRowStart_Measure_02_0, out double hv_LineColumnStart_Measure_02_0, out double hv_LineRowEnd_Measure_02_0, out double hv_LineColumnEnd_Measure_02_0);

            hv_TmpCtrl_Row = 0.5 * (hv_LineRowStart_Measure_02_0 + hv_LineRowEnd_Measure_02_0);
            hv_TmpCtrl_Column.Dispose();
            hv_TmpCtrl_Column = 0.5 * (hv_LineColumnStart_Measure_02_0 + hv_LineColumnEnd_Measure_02_0);
            hv_TmpCtrl_Dr.Dispose();
            hv_TmpCtrl_Dr = hv_LineRowStart_Measure_02_0 - hv_LineRowEnd_Measure_02_0;
            hv_TmpCtrl_Dc.Dispose();
            hv_TmpCtrl_Dc = hv_LineColumnEnd_Measure_02_0 - hv_LineColumnStart_Measure_02_0;
            hv_TmpCtrl_Phi.Dispose();
            hv_TmpCtrl_Phi = hv_TmpCtrl_Dr.TupleAtan2(
                hv_TmpCtrl_Dc);
            hv_TmpCtrl_Len1.Dispose();
            hv_TmpCtrl_Len1 = 0.5 * ((((hv_TmpCtrl_Dr * hv_TmpCtrl_Dr) + (hv_TmpCtrl_Dc * hv_TmpCtrl_Dc))).TupleSqrt()
                );
            hv_TmpCtrl_Len2.Dispose();
            hv_TmpCtrl_Len2 = new HTuple((HTuple)double.Parse(txt_ThresholdValue.Text));
            
            HOperatorSet.GenRegionLine(out HObject ho_DrawLine, (HTuple)hv_LineRowStart_Measure_02_0, (HTuple)hv_LineColumnStart_Measure_02_0, (HTuple)hv_LineRowEnd_Measure_02_0, (HTuple)hv_LineColumnEnd_Measure_02_0);
            HOperatorSet.GenRectangle2(out HObject rect2, hv_TmpCtrl_Row, hv_TmpCtrl_Column, hv_TmpCtrl_Phi,
      hv_TmpCtrl_Len1, (HTuple)double.Parse(txt_MeasureWidthValue.Text) / 2);
            hWindow.DispObj(ho_DrawLine);
            hWindow.DispObj(rect2);
            this.ControlBox = true;
            
        }

        private void btn_Measurement_Click(object sender, EventArgs e)
        {
            Measure();
        }
        private void Measure() 
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                hWindow.ClearWindow();
                txt_MeasureWidthValue.Text = trb_setLineWidth.Value.ToString();
               
                txt_sigmalValue.Text = (((double)(trb_setSigma.Value))/10).ToString();
                txt_ThresholdValue.Text = trb_SetThreshold.Value.ToString();
                richTextBox1.Clear();
                HOperatorSet.GenMeasureRectangle2(hv_TmpCtrl_Row, hv_TmpCtrl_Column, hv_TmpCtrl_Phi,
      hv_TmpCtrl_Len1, (HTuple)double.Parse(txt_MeasureWidthValue.Text)/2, ImageWidth, ImageHeight, "nearest_neighbor", out measureHandle);
                HTuple m12 = (HTuple)double.Parse(txt_sigmalValue.Text);
                HOperatorSet.MeasurePos(ho_Image, measureHandle, (HTuple)double.Parse(txt_sigmalValue.Text), (HTuple)double.Parse(txt_ThresholdValue.Text), "all", "all",
                    out HTuple rowEdge, out HTuple columnEdge, out HTuple amplitude, out hv_distance);
                HOperatorSet.GenEmptyRegion(out HObject RegionShow);
                for (int i = 0; i < rowEdge.Length; i++)
                {
                    HOperatorSet.GenRectangle2(out HObject Rect2, rowEdge[i], columnEdge[i], HalconOperator.Rad(90)+ hv_TmpCtrl_Phi, (HTuple)double.Parse(txt_MeasureWidthValue.Text) / 2, 1);
                    HOperatorSet.ConcatObj(RegionShow, Rect2, out RegionShow);   
                }
                for (int i = 0; i < rowEdge.Length-1; i++)
                {
                    double tmp = hv_distance[i].D;
                    richTextBox1.Text += tmp.ToString() + "\r\n";
                }
                 
                
                hWindow.SetColor("green");
                hWindow.DispObj(ho_Image);
                hWindow.DispObj(RegionShow);
                
            }));
           
        }
    }
}
