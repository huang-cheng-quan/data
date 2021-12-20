#define NCC
using Camera_Capture_demo.GlobalVariable;
using Camera_Capture_demo.Helpers;
using Camera_Capture_demo.Models;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Camera_Capture_demo.HalconVision
{
    class HalconOperator
    {
        HObject hImage;
        HTuple hv_ModelID, hv_ModelParams, hv_homMat2D;
        string modelID_Path, modelParams_Path, calibration_Path;
        string[] modelROI_Path = new string[4];

        int camNo;
        private static List<HalconOperator> halconOperatorList;
        private static readonly object locker = new object();

        private HalconOperator(int camNo)
        {
            this.camNo = camNo;
            calibration_Path = Application.StartupPath + $"\\Calibration\\Cam_homMat2D{camNo + 1}";
            ReUpdateParams();
        }

        public static HalconOperator GetInstance(int camNo)
        {
            if (halconOperatorList == null)
            {
                halconOperatorList = new List<HalconOperator>();
                for (int i = 0; i < 2; i++)
                {
                    halconOperatorList.Add(null);
                }
            }
            if (halconOperatorList[camNo] == null)
            {
                lock (locker)
                {
                    if (halconOperatorList[camNo] == null)
                    {
                        halconOperatorList[camNo] = new HalconOperator(camNo);
                    }
                }
            }
            return halconOperatorList[camNo];
        }

        public void ReUpdateParams()
        {
            #region 项目路径
            string parent = Application.StartupPath + "\\Project\\" + ConfigVars.configInfo.ProductInfos.SelectProject;
#if NCC
            modelID_Path = parent + $"\\Model{camNo + 1}\\model_cell.ncm";
#else
            modelID_Path = parent + $"\\Model{camNo+1}\\model_cell.sbm";
#endif
            if (File.Exists(modelID_Path))
            {
#if NCC
                if (hv_ModelID != null)
                    HOperatorSet.ClearNccModel(hv_ModelID);
                HOperatorSet.ReadNccModel(modelID_Path, out hv_ModelID);
#else
                if (hv_ModelID != null)
                    HOperatorSet.ClearShapeModel(hv_ModelID);
                HOperatorSet.ReadShapeModel(modelID_Path, out hv_ModelID);
#endif
            }
            modelParams_Path = parent + $"\\Model{camNo + 1}\\model_cell.tup";
            for (int i = 0; i < 4; i++)
            {
                modelROI_Path[i] = parent + $"\\Model{camNo + 1}\\model_roi{i + 1}.hobj";
            }
            #endregion
            if (File.Exists(modelParams_Path))
            {
                HOperatorSet.ReadTuple(modelParams_Path, out hv_ModelParams);
            }
            if (File.Exists(calibration_Path))
            {
                HOperatorSet.ReadTuple(calibration_Path, out hv_homMat2D);
            }
        }

        /// <summary>
        /// 寻找模板
        /// </summary>
        /// <param name="hWindow"></param>
        /// <param name="ho_Image"></param>
        /// <param name="pointXYU"></param>
        public void FindShapeModel(HWindow hWindow, HObject ho_Image, out List<PointXYU> pixelPointXYUs, out List<PointXYU> worldPointXYUs)
        {
            pixelPointXYUs = new List<PointXYU>();
            worldPointXYUs = new List<PointXYU>();
            for (int index = 0; index < modelROI_Path.Length; index++)
            {
                if (File.Exists(modelROI_Path[index]))
                {
                    HOperatorSet.ReadObject(out HObject ho_region, modelROI_Path[index]);
                    hWindow.SetDraw("margin");
                    hWindow.SetLineWidth(1);
                    hWindow.SetColor("yellow");
                    hWindow.DispObj(ho_region);
                    HOperatorSet.ReduceDomain(ho_Image, ho_region, out HObject ho_reduced);
                    //HOperatorSet.GenEmptyObj(out HObject ho_SelectedContours);
                    HOperatorSet.GenRectangle2ContourXld(out HObject ho_Rect, 0, 0, 0, hv_ModelParams[0].F / 2, hv_ModelParams[1].F / 2);
#if NCC
                    HOperatorSet.FindNccModel(ho_reduced, hv_ModelID, -0.8, 1.6, hv_ModelParams[2].F, 1, 0, "true", 0, out HTuple hv_RowCheck, out HTuple hv_ColumnCheck, out HTuple hv_AngleCheck, out HTuple hv_Score);
#else
            HOperatorSet.FindScaledShapeModel(ho_Image, hv_ModelID, -0.8, 1.6, 0.6, 1.4, hv_ModelParams[2].F, 1, 0.5, "interpolation",
    5, 0.8, out HTuple hv_RowCheck, out HTuple hv_ColumnCheck, out HTuple hv_AngleCheck, out HTuple hv_Scale,
    out HTuple hv_Score);
#endif
                    hWindow.SetColor("blue");
                    if (hv_Score.Length == 0)
                    {
                        pixelPointXYUs.Add(null);
                        worldPointXYUs.Add(null);
                    }
                    for (int i = 0; i < hv_Score.Length; i++)
                    {
                        HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_RowCheck[i], hv_ColumnCheck[i], hv_AngleCheck[i], out HTuple hv_MovementOfObject);
                        HOperatorSet.AffineTransContourXld(ho_Rect, out HObject ho_ModelAtNewPosition,
                            hv_MovementOfObject);
                        HOperatorSet.GenRegionContourXld(ho_ModelAtNewPosition, out HObject ho_Region, "filled");
                        HOperatorSet.AffineTransPixel(hv_MovementOfObject, hv_ModelParams[4], hv_ModelParams[5], out HTuple hv_RowTrans, out HTuple hv_ColTrans);
                        HOperatorSet.GenCrossContourXld(out HObject ho_Cross, hv_RowTrans, hv_ColTrans, 20, 0.785398);
                        HOperatorSet.GenCircleContourXld(out HObject ho_ContCircle, hv_RowTrans, hv_ColTrans, 20, 0, 6.28318, "positive", 1);
                        HOperatorSet.Intensity(ho_Region, ho_reduced, out HTuple hv_Mean, out _);

                        bool flag;
                        if (hv_ModelParams[6] == 0)
                            flag = hv_Mean >= hv_ModelParams[3];
                        else
                            flag = hv_Mean <= hv_ModelParams[3];
                        if (flag)
                        {
                            hWindow.DispObj(ho_ModelAtNewPosition);
                            hWindow.DispObj(ho_Cross);
                            hWindow.DispObj(ho_ContCircle);
                            HTuple hv_text;
                            pixelPointXYUs.Add(new PointXYU() { X = hv_RowTrans[0].F, Y = hv_ColTrans[0].F, U = hv_AngleCheck[i].F });
                            if (hv_homMat2D != null)
                            {
                                HOperatorSet.AffineTransPoint2d(hv_homMat2D, hv_RowTrans, hv_ColTrans, out HTuple hv_WorldRow, out HTuple hv_WorldCol);
                                worldPointXYUs.Add(new PointXYU() { X = hv_WorldRow[0].F, Y = hv_WorldCol[0].F, U = hv_AngleCheck[i].F });
                                hv_text = $"({hv_WorldRow.TupleString(".2f")},\n{hv_WorldCol.TupleString(".2f")},\n{hv_AngleCheck.TupleSelect(i).TupleString(".2f")})";
                            }
                            else
                            {
                                worldPointXYUs.Add(new PointXYU() { X = hv_RowTrans[0].F, Y = hv_ColTrans[0].F, U = hv_AngleCheck[i].F });
                                hv_text = $"({hv_RowTrans.TupleString(".2f")},\n{hv_ColTrans.TupleString(".2f")},\n{hv_AngleCheck.TupleSelect(i).TupleString(".2f")})";
                            }
                            hWindow.DispText(hv_text, "image", hv_RowTrans, hv_ColTrans, new HTuple("black"), "box", "true");
                        }
                        else
                        {
                            pixelPointXYUs.Add(null);
                            worldPointXYUs.Add(null);
                        }
                    }
                    ho_reduced.Dispose();
                }
                else
                {
                    pixelPointXYUs.Add(null);
                    worldPointXYUs.Add(null);
                }
            }
        }

        /// <summary>
        /// 转换工具中心点坐标为Tool0坐标
        /// </summary>
        /// <param name="posTool1">当前Tcp坐标</param>
        /// <param name="tcpTrans">工具坐标系相对于法兰盘中心的偏移和旋转</param>
        /// <returns></returns>
        public static PointXYU ConvertTcpToTool0(PointXYU posTool1, PointXYU tcpTrans)
        {
            HOperatorSet.VectorAngleToRigid(tcpTrans.X, tcpTrans.Y, tcpTrans.U, posTool1.X, posTool1.Y, posTool1.U, out HTuple homMat2D);
            HOperatorSet.AffineTransPoint2d(homMat2D, 0, 0, out HTuple qx, out HTuple qy);
            PointXYU posTool0 = new PointXYU() { X = qx[0].F, Y = qy[0].F, U = posTool1.U - tcpTrans.U };
            return posTool0;
        }

        /// <summary>
        /// 转化Tool0坐标为工具中心点坐标
        /// </summary>
        /// <param name="posTool0"></param>
        /// <param name="tcpTrans"></param>
        /// <returns></returns>
        public static PointXYU ConvertTool0ToTcp(PointXYU posTool0, PointXYU tcpTrans)
        {
            HOperatorSet.VectorAngleToRigid(0, 0, 0, posTool0.X, posTool0.Y, posTool0.U, out HTuple homMat2D);
            HOperatorSet.AffineTransPoint2d(homMat2D, tcpTrans.X, tcpTrans.Y, out HTuple qx, out HTuple qy);
            PointXYU posTool1 = new PointXYU() { X = qx[0].F, Y = qy[0].F, U = posTool0.U + tcpTrans.U };
            return posTool1;
        }

        /// <summary>
        /// 根据下相机拍照结果动态Mask工具点
        /// </summary>
        /// <param name="photoPos">拍照点的机器人坐标</param>
        /// <param name="featurePoint">相机捕捉的Tcp特征点坐标</param>
        /// <returns></returns>
        public static PointXYU CalTcpTrans(PointXYU photoPos, PointXYU featurePoint)
        {
            HOperatorSet.VectorAngleToRigid(0, 0, 0, photoPos.X, photoPos.Y, photoPos.U, out HTuple homMat2D);
            HOperatorSet.HomMat2dInvert(homMat2D, out HTuple homMat2DInvert);
            HOperatorSet.AffineTransPoint2d(homMat2DInvert, featurePoint.X, featurePoint.Y, out HTuple qx, out HTuple qy);
            PointXYU tcpTrans = new PointXYU() { X = qx[0].F, Y = qy[0].F, U = -featurePoint.U };
            return tcpTrans;
        }

        public static PointXYU CalActualFeaturePoint(PointXYU calibPhotoPoint, PointXYU currentPhotoPoint, PointXYU featurePoint)
        {
            HOperatorSet.VectorAngleToRigid(calibPhotoPoint.X, calibPhotoPoint.Y, calibPhotoPoint.U, currentPhotoPoint.X, currentPhotoPoint.Y, currentPhotoPoint.U, out HTuple homMat2D);
            HOperatorSet.AffineTransPoint2d(homMat2D, featurePoint.X, featurePoint.Y, out HTuple qx, out HTuple qy);
            return new PointXYU() { X = qx, Y = qy, U = featurePoint.U };
        }

        public static float Rad(double deg)
        {
            HOperatorSet.TupleRad(deg, out HTuple rad);
            return (float)rad.D;
        }

        public static float Deg(double rad)
        {
            HOperatorSet.TupleDeg(rad, out HTuple reg);
            return (float)reg.D;
        }

        /// <summary>
        /// 多点拟合求圆心
        /// </summary>
        /// <param name="X">点的X坐标元组</param>
        /// <param name="Y">点的Y坐标元组</param>
        /// <param name="RcX">圆心X</param>
        /// <param name="RcY">圆心Y</param>
        /// <param name="R">半径</param>
        /// <returns></returns>
        public static bool FitCircle(List<PointF> points, out double RcX, out double RcY, out double R)
        {
            try
            {
                HTuple hTupleR = new HTuple();
                HTuple hTupleC = new HTuple();
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].X > 0.0 & points[i].Y > 0.0)
                    {
                        hTupleR = hTupleR.TupleConcat(points[i].X);
                        hTupleC = hTupleC.TupleConcat(points[i].Y);
                    }
                }
                HObject contour;
                HOperatorSet.GenContourPolygonXld(out contour, hTupleR, hTupleC);//使用模板中心生成多边形XLD轮廓
                HOperatorSet.FitCircleContourXld(contour, "geotukey", -1, 0, 0, 3, 2, out HTuple row, out HTuple column, out HTuple radius, out HTuple StartPhi, out HTuple EndPhi, out HTuple pointOrder);//拟合圆形                                                                                                                                               //得出结果
                RcX = row;
                RcY = column;
                R = radius;
                contour.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                RcX = -1.0;
                RcY = -1.0;
                R = -1.0;
                return false;
            }
        }
        // Chapter: Graphics / Text
        // Short Description: This procedure writes a text message. 
        public static void DispMessage(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
            HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {
            // Local iconic variables 
            // Local control variables 
            HTuple hv_GenParamName, hv_GenParamValue;
            HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
            HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
            HTuple hv_CoordSystem_COPY_INP_TMP = hv_CoordSystem.Clone();
            HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();

           
            if (hv_Row_COPY_INP_TMP == new HTuple() || hv_Column_COPY_INP_TMP == new HTuple())
            {

                return;
            }
            if (hv_Row_COPY_INP_TMP == -1)
            {
                hv_Row_COPY_INP_TMP = 12;
            }
            if (hv_Column_COPY_INP_TMP == -1)
            {
                hv_Column_COPY_INP_TMP = 12;
            }
            //
            //Convert the parameter Box to generic parameters.
            hv_GenParamName = new HTuple();
            hv_GenParamValue = new HTuple();
            if (hv_Box.TupleLength() > 0)
            {
                if (hv_Box.TupleSelect(0) == "false")
                {
                    //Display no box
                    hv_GenParamName = hv_GenParamName.TupleConcat("box");
                    hv_GenParamValue = hv_GenParamValue.TupleConcat("false");
                }
                else if (hv_Box.TupleSelect(0) == "true")
                {
                    //Set a color other than the default.
                    hv_GenParamName = hv_GenParamName.TupleConcat("box_color");
                    hv_GenParamValue = hv_GenParamValue.TupleConcat(hv_Box.TupleSelect(0));
                }
            }
            if (hv_Box.TupleLength() > 1)
            {
                if (hv_Box.TupleSelect(1) == "false")
                {
                    //Display no shadow.
                    hv_GenParamName = hv_GenParamName.TupleConcat("shadow");
                    hv_GenParamValue = hv_GenParamValue.TupleConcat("false");
                }
                else if (hv_Box.TupleSelect(1) == "true")
                {
                    //Set a shadow color other than the default.
                    hv_GenParamName = hv_GenParamName.TupleConcat("shadow_color");
                    hv_GenParamValue = hv_GenParamValue.TupleConcat(hv_Box.TupleSelect(1));
                }
            }
            //Restore default CoordSystem behavior.
            if (hv_CoordSystem_COPY_INP_TMP != "window")
            {
                hv_CoordSystem_COPY_INP_TMP = "image";
            }
            //
            if (hv_Color_COPY_INP_TMP == "")
            {
                //disp_text does not accept an empty string for Color.
                hv_Color_COPY_INP_TMP = new HTuple();
            }
            //
            HOperatorSet.DispText(hv_WindowHandle, hv_String, hv_CoordSystem_COPY_INP_TMP,
                hv_Row_COPY_INP_TMP, hv_Column_COPY_INP_TMP, hv_Color_COPY_INP_TMP, hv_GenParamName,
                hv_GenParamValue);

            return;
        }

        private byte[] ImageToBytes(Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 图像输出显示委托
        /// </summary>
        /// <param name="hWindowControl"></param>
        /// <param name="hImage"></param>
        public delegate void ShowImageDelegate(UserControl hWindowControl, HObject hImage);

        public static void ShowImage(UserControl hWindowControl, HObject hImage)
        {
            try
            {
                if (hImage != null)
                {
                    if (hWindowControl.InvokeRequired)
                    {
                        ShowImageDelegate d = new ShowImageDelegate(ShowImage);
                        hWindowControl.BeginInvoke(d, hWindowControl, hImage);
                    }
                    else
                    {
                        if (hWindowControl.GetType().Name == "HWindowControl")
                        {
                            HWindow hWindow = (hWindowControl as HWindowControl).HalconWindow;
                            HOperatorSet.GetImageSize(hImage, out HTuple hv_Width, out HTuple hv_Height);
                            hWindow.SetPart(0, 0, (int)hv_Height, (int)hv_Width);
                            hWindow.ClearWindow();
                            hWindow.DispObj(hImage);
                        }
                        if (hWindowControl.GetType().Name == "HSmartWindowControl")
                        {
                            HWindow hWindow = (hWindowControl as HSmartWindowControl).HalconWindow;
                            hWindow.DispObj(hImage);
                            (hWindowControl as HSmartWindowControl).HKeepAspectRatio = true;
                            (hWindowControl as HSmartWindowControl).SetFullImagePart(null);
                        }
                        hImage.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
