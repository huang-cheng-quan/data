using BaseForm;
using Camera_Capture_demo.GlobalVariable;
using Camera_Capture_demo.HalconVision;
using Camera_Capture_demo.Helpers;
using Camera_Capture_demo.Models;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.HalconTools
{
    public partial class HalconTools_CreateModelForm : ZoomForm
    {
        MvClass mvclass = new MvClass();
        private HWindow hWindow;
        HObject ho_Image, ho_ImageROI;
        HTuple hv_ModelID, hv_ModelParams;
        HTuple modelCenterRow = new HTuple (), modelCenterCol =new HTuple ();//模板中心点坐标
        string modelParams_Path, modelID_Path;
        HalconOperator halconOperator;
        HTuple hv_ModelRegionArea = new HTuple();
        HTuple hv_RefRow = new HTuple(), hv_RefColumn = new HTuple();
        int m_icamNo;
        public HalconTools_CreateModelForm(int camNo)
        {
            InitializeComponent();
            this.m_icamNo = camNo;
            this.Name += camNo;
            this.Text = $"相机{camNo + 1}模板设置";
        }
        private void HalconTools_CreateModelForm_Load(object sender, EventArgs e)
        {
            nudMinScore.DecimalPlaces = 2;
            nudMinScore.Increment = 0.1M;
            trbEraserSize.Minimum = 1;
            trbEraserSize.Maximum = 50;
            rdoRect.Checked = true;

            hWindow = hWindowControl1.HalconWindow;
            hWindow.SetColor("blue");
            hWindow.SetDraw("margin");
            hWindow.SetLineWidth(2);
            HOperatorSet.SetSystem("border_shape_models", "false");

            string dir_path = Application.StartupPath + "\\Project\\" + ConfigVars.configInfo.ProductInfos.SelectProject + $"\\Model{m_icamNo}";
            if (!Directory.Exists(dir_path))
            {
                Directory.CreateDirectory(dir_path);
            }
            modelParams_Path = dir_path + "\\model_cell.tup";
            if (File.Exists(modelParams_Path))
            {
                HOperatorSet.ReadTuple(modelParams_Path, out hv_ModelParams);
                nudMinScore.Value = Convert.ToDecimal(hv_ModelParams[2].F);
                nudThreshold.Value = Convert.ToDecimal(hv_ModelParams[3].I);
                cboPolarity.SelectedIndex = hv_ModelParams[6].I;
            }

            modelID_Path = dir_path + "\\model_cell.shm";
            HOperatorSet.ReadShapeModel(modelID_Path, out hv_ModelID);


            halconOperator = HalconOperator.GetInstance(m_icamNo, 0);
            halconOperator.ReUpdateParams();
            mvclass = MvClass.GetInstance(m_icamNo);
            mvclass.Clear_EventProcessImage_Event();
            mvclass.eventProcessImage += mvclass_eventProcessImage;
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
                    HObject hImage = ho_Image.Clone();
                    HalconOperator.ShowImage(hWindowControl1, hImage);
                    //ho_Image.Dispose();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void btnTakePic_Click(object sender, EventArgs e)
        {
            if (!mvclass.isOpen)
            {
                mvclass.OpenCam(m_icamNo);
            }
        }

        private void btnDrawROI1_Click(object sender, EventArgs e)
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
            hWindow.DrawRectangle1(out double row1, out double column1, out double row2, out double column2);
            HOperatorSet.GenRectangle1(out HObject ho_Rect, row1, column1, row2, column2);
            hWindow.DispObj(ho_Rect);
            this.ControlBox = true;
            try
            {
                HOperatorSet.WriteObject(ho_Rect, Application.StartupPath + "\\Project\\" + ConfigVars.configInfo.ProductInfos.SelectProject + $"\\Model{m_icamNo}\\model_roi{cboRoiNo.SelectedIndex + 1}");
                MessageBox.Show("ROI区域保存成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        private void mvclass_eventProcessImage(HObject hImage, int motorNum)
        {
            BeginInvoke(new MethodInvoker(() => {
                HOperatorSet.CopyImage(hImage, out ho_Image);
                HalconOperator.ShowImage(hWindowControl1, hImage);
            }));
        }

        private void HalconTools_CreateModelForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mvclass != null)
            {
                if (mvclass.isOpen)
                {
                    mvclass.StopGrabbing();
                    //mvclass.CloseCam();
                    mvclass.Clear_EventProcessImage_Event();
                }

            }
            if (hWindow != null)
            {
                hWindow.Dispose();
            }
            if (hv_ModelID != null)
            {
#if NCC
                HOperatorSet.ClearNccModel(hv_ModelID);
#else
                HOperatorSet.ClearShapeModel(hv_ModelID);
#endif
            }
        }

        private void btnDrawRect_Click(object sender, EventArgs e)
        {
            HObject ho_ModelContours, ho_ModelRegion, ho_TransContours;
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            HOperatorSet.GenEmptyObj(out ho_ModelRegion);
            HOperatorSet.GenEmptyObj(out ho_TransContours);
            HTuple hv_HomMat2D = new HTuple();
            hWindow.SetDraw("margin");
            this.ControlBox = false;
            if (ho_Image == null)
            {
                MessageBox.Show("未打开需要处理的原图");
                return;
            }
            try
            {
                hWindow.DispObj(ho_Image);
                HalconOperator.DispMessage(hWindow, "鼠标左键按下开始ROI选取,\n松开鼠标结束,\n鼠标右键退出", "window", 12, 12, "yellow", "false");
                hWindow.SetDraw("margin");
                hWindow.DrawRectangle1(out double row1, out double column1, out double row2, out double column2);
                HOperatorSet.GenRectangle1(out HObject ho_Rect, row1, column1, row2, column2);
                HOperatorSet.ReduceDomain(ho_Image, ho_Rect, out ho_ImageROI);
                int rectCenterRow = Convert.ToInt32((row2 + row1) / 2);
                int rectCenterCol = Convert.ToInt32((column2 + column1) / 2);
                //HOperatorSet.CropRectangle1(ho_Image, out ho_ImageROI, row1, column1, row2, column2);
                hWindow.DispObj(ho_Rect);
                CreateShapeModel(ho_ImageROI);
                HOperatorSet.GenCrossContourXld(out HObject ho_Cross1, rectCenterRow, rectCenterCol, 10, 0.785398);
                hWindow.DispObj(ho_Cross1);
                #region 修改模板中心位置
                DialogResult dr;
                dr = MessageBox.Show("是否需要修改模板中心位置？", "tip", MessageBoxButtons.YesNoCancel,
                         MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    hWindow.ClearWindow();
                    HOperatorSet.GenEmptyObj(out HObject ho_Cross);
                    HOperatorSet.GenEmptyObj(out HObject ho_ContCircle);
                    int hv_Button = 0;
                    while (hv_Button != 4)
                    {
                        HOperatorSet.SetCheck("~give_error");
                        HOperatorSet.SetSystem("flush_graphic", "false");
                        hWindow.GetMbutton(out _, out _, out hv_Button);
                        if (hv_Button == 1)
                        {
                            hWindow.GetMposition(out int hv_Row, out int hv_Column, out hv_Button);
                            HOperatorSet.GenCrossContourXld(out ho_Cross, hv_Row, hv_Column, 10, 0.785398);
                            HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_Row, hv_Column, 10, 0, 6.28318, "positive", 1);
                            modelCenterRow = hv_Row - rectCenterRow;
                            modelCenterCol = hv_Column - rectCenterCol;
                        }
                        hWindow.DispObj(ho_Image);
                        hWindow.DispObj(ho_Rect);
                        HalconOperator.DispMessage(hWindow, "鼠标左键按下更改中心点位置,\n鼠标右键确定", "window", 12, 12, "yellow", "false");
                        hWindow.DispObj(ho_ContCircle);
                        hWindow.DispObj(ho_Cross);
                        HOperatorSet.SetSystem("flush_graphic", "true");
                        Application.DoEvents();
                        Thread.Sleep(10);
                    }
                }
                else
                {
                    modelCenterRow = rectCenterRow;
                    modelCenterCol = rectCenterCol;
                }
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            this.ControlBox = true;
        }

        private void btnClrEraser_Click(object sender, EventArgs e)
        {
            hWindow.ClearWindow();
            hWindow.DispObj(ho_ImageROI);
        }

        private void hWindowControl1_MouseMove(object sender, MouseEventArgs e)
        {
            int leftBorder = hWindowControl1.Location.X;
            int rightBorder = hWindowControl1.Location.X + hWindowControl1.Size.Width;
            int topBorder = hWindowControl1.Location.Y;
            int bottomBorder = hWindowControl1.Location.Y + hWindowControl1.Size.Height;
            if (ho_Image != null)
            {
                try
                {
                    HOperatorSet.GetImageSize(ho_Image, out HTuple width, out HTuple height);
                    if (e.X > leftBorder && e.X < rightBorder && e.Y > topBorder && e.Y < bottomBorder)
                    {
                        hWindow.GetMposition(out int row, out int col, out int button);
                        if (row < height && col < width)
                        {
                            HOperatorSet.GetGrayval(ho_Image, row, col, out HTuple grayval);
                            txtPixelLocation.Text = "当前坐标：  R↓：" + row.ToString() + "  C→: " + col.ToString() + "  灰度值  " + grayval.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void btnSetEraser_Click(object sender, EventArgs e)
        {
            this.ControlBox = false;
            HTuple hv_EraserSize;
            if (ho_ImageROI == null)
            {
                MessageBox.Show("未框选模板ROI");
                return;
            }
            #region 显示ROI区域图像
            hWindow.ClearWindow();
            hWindow.DispObj(ho_ImageROI);
            int hv_Button = 0;
            #endregion
            #region 橡皮擦擦除
            HOperatorSet.GenEmptyObj(out HObject ho_Eraser);
            HOperatorSet.GenEmptyObj(out HObject ho_region_removeds);
            hWindow.SetColor("red");
            hWindow.SetDraw("fill");
            //橡皮擦路过的坐标集
            HalconOperator.DispMessage(hWindow, "鼠标左键按下生成擦除区域,\n中键按下清除擦除区域,\n鼠标右键退出",
     "window", 12, 12, "yellow", "false");
            //擦除工作
            while (hv_Button == 0)
            {
                hWindow.GetMbutton(out _, out _,
                    out hv_Button);
                while (hv_Button != 4)
                {
                    hWindow.SetWindowParam("flush", "false");
                    hv_EraserSize = trbEraserSize.Value;
                    try
                    {
                        hWindow.GetMposition(out int hv_Row, out int hv_Column, out hv_Button);
                        //hv_Rows = hv_Rows.TupleConcat(hv_Row);
                        //hv_Cols = hv_Cols.TupleConcat(hv_Column);
                        //生成橡皮擦擦过的区域
                        if (rdoRect.Checked)
                        {
                            HOperatorSet.GenRectangle2(out ho_Eraser, hv_Row, hv_Column, 0, hv_EraserSize,
                                hv_EraserSize);
                        }
                        else
                        {
                            HOperatorSet.GenCircle(out ho_Eraser, hv_Row, hv_Column, hv_EraserSize);
                        }
                        //橡皮擦区域合并
                        if (hv_Button == 1)
                            HOperatorSet.Union2(ho_region_removeds, ho_Eraser, out ho_region_removeds);
                        if (hv_Button == 2)
                            HOperatorSet.Difference(ho_region_removeds, ho_Eraser, out ho_region_removeds);
                        hWindow.DispObj(ho_ImageROI);
                        hWindow.SetWindowParam("flush", "true");
                        hWindow.DispObj(ho_region_removeds);
                        Application.DoEvents();
                        Thread.Sleep(10);
                    }
                    catch (Exception)
                    {

                    }
                }
                //区域相减
                HOperatorSet.Difference(ho_ImageROI, ho_region_removeds, out HObject ho_RegionDifference
                    );
                HOperatorSet.ReduceDomain(ho_ImageROI, ho_RegionDifference, out HObject ho_ImageReduced
                    );
                #endregion
                #region 创建模板
                CreateShapeModel(ho_ImageReduced);
                #endregion
            }
            this.ControlBox = true;
        }
        HalconOperator.ModelResult modelresult = new HalconOperator.ModelResult();

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (ho_Image != null)
            {
                HObject hImage = ho_Image.Clone();
                HalconOperator.ShowImage(hWindowControl1, hImage);
                halconOperator.OnlyFindShapModel(hWindow, hImage,6, out _);
            }
        }

        private void btnSaveModel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ho_ImageROI == null && hv_ModelParams == null)
                {
                    return;
                }
                if (hv_ModelID == null)
                {
                    return;
                }
                string dirPath = Path.GetDirectoryName(modelID_Path);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                HOperatorSet.WriteShapeModel(hv_ModelID, modelID_Path);
                if (ho_ImageROI != null)
                {
                    HOperatorSet.RegionFeatures(ho_ImageROI, "width", out HTuple width);
                    HOperatorSet.RegionFeatures(ho_ImageROI, "height", out HTuple height);
                    //hv_ModelParams:0.模板框选区域宽度;1.模板框选区域高度;2.最低匹配值;3.模板平均灰度阈值;4.模板中心点R;5.模板中心点C;6.电池极性
                    hv_ModelParams = width.TupleConcat(height).
                        TupleConcat((float)nudMinScore.Value).
                        TupleConcat((int)nudThreshold.Value).
                        TupleConcat(modelCenterRow).
                        TupleConcat(modelCenterCol).
                        TupleConcat(cboPolarity.SelectedIndex);
                }
                else
                {
                    hv_ModelParams[2] = (float)nudMinScore.Value;
                    hv_ModelParams[3] = (int)nudThreshold.Value;
                    hv_ModelParams[6] = cboPolarity.SelectedIndex;
                }
                ConfigVars.configInfo.calibrationData[m_icamNo].FitCenterRow = modelCenterRow.D;
                ConfigVars.configInfo.calibrationData[m_icamNo].FitCenterColumn = modelCenterCol.D;
                HOperatorSet.WriteTuple(hv_ModelParams, modelParams_Path);
                MessageBox.Show("模板保存成功");
                halconOperator.ReUpdateParams();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void CreateShapeModel(HObject hImage)
        {
            if (hv_ModelID != null)
                HOperatorSet.ClearShapeModel(hv_ModelID);
            HOperatorSet.CreateShapeModel(hImage, int.Parse(cmb_NumLevels.Text), HalconOperator.Rad(double.Parse(txt_AngleStart.Text)),
                HalconOperator.Rad(double.Parse(txt_AngleExtent.Text)), HalconOperator.Rad(double.Parse(txt_AngleStep.Text)),
               cmb_Optimization.Text,cmb_Metric.Text, txt_Contrast.Text, txt_MinContrast.Text, out hv_ModelID);


        }
    }
}
