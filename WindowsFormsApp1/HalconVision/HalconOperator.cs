#define NCC

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
        static HTuple hv_ModelID,hv_homMat2D;
        static List<HTuple> hv_ModelParams_List;
        static List<int> modelNoList;
        public static void ReUpdateParams()
        {
            hv_ModelID = new HTuple();
            hv_ModelParams_List = new List<HTuple>();
            modelNoList = new List<int>();
            for (int i = 1; i <= 10; i++)
            {
#if NCC
                string modelID_Path = Application.StartupPath + $"\\Model\\model_cell{i}.ncm";//shapmodel:sbm,nccmodel:ncm
                if (File.Exists(modelID_Path))
                {
                    modelNoList.Add(i);
                    HOperatorSet.ReadNccModel(modelID_Path, out HTuple hv_SubModelID);
                    hv_ModelID = hv_ModelID.TupleConcat(hv_SubModelID);
                }
#else
                string modelID_Path = Application.StartupPath + $"\\Model\\model_cell{i}.sbm";
                if (File.Exists(modelID_Path))
                {
                    HOperatorSet.ReadShapeModel(modelID_Path, out HTuple hv_SubModelID);
                    hv_ModelID = hv_ModelID.TupleConcat(hv_SubModelID);
                }
#endif
                string modelParams_Path = Application.StartupPath + $"\\Model\\model_cell{i}.tup";
                if (File.Exists(modelParams_Path))
                {
                    HOperatorSet.ReadTuple(modelParams_Path, out HTuple hv_ModelParams);
                    hv_ModelParams_List.Add(hv_ModelParams);
                }
            }

        }

        /// <summary>
        /// 将多个图像拼接成一个
        /// </summary>
        /// <param name="images">图像元组</param>
        /// <param name="rows">拼接子图列数</param>
        /// <param name="columns">拼接子图行数</param>
        /// <param name="imageWidth">图像宽度</param>
        /// <param name="imageHeight">图像高度</param>
        public HObject MosaicImages2One(HObject images, int rows, int columns)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            HTuple ProjMatrices = new HTuple();//射影变换矩阵的数组
            HTuple Rows1 = new HTuple();//源图像中对应点的行坐标
            HTuple Cols1 = new HTuple();//源图像中对应点的列坐标
            HTuple Rows2 = new HTuple();//目标图像中对应点的行坐标
            HTuple Cols2 = new HTuple();//目标图像中对应点的列坐标
            HTuple NumMatches = new HTuple();//图像对中对应点的数量
            GetMosaicImgPairs(rows, columns, out int[] from, out int[] to);
            HOperatorSet.GetImageSize(images.SelectObj(0),out HTuple imageWidth,out HTuple imageHeight);
            for (int i = 0; i < from.Length; i++)
            {
                HOperatorSet.SelectObj(images, out HObject ImageF, from[i]);
                HOperatorSet.SelectObj(images, out HObject ImageT, to[i]);
                //提取两个图像中的点
                HOperatorSet.PointsFoerstner(ImageF,
                                             1,
                                             2,
                                             3,
                                             40,
                                             0.2,
                                             "gauss",
                                             "true",
                                             out HTuple RowJunctionsF,
                                             out HTuple ColJunctionsF,
                                             out _,
                                             out _,
                                             out _,
                                             out _,
                                             out _,
                                             out _,
                                             out _,
                                             out _);
                HOperatorSet.PointsFoerstner(ImageT,
                                             1,
                                             2,
                                             3,
                                             40,
                                             0.2,
                                             "gauss",
                                             "true",
                                             out HTuple RowJunctionsT,
                                             out HTuple ColJunctionsT,
                                             out _,
                                             out _,
                                             out _,
                                             out _,
                                             out _,
                                             out _,
                                             out _,
                                             out _);
                Debug.WriteLine(watch.ElapsedMilliseconds.ToString());
                //通过查找点之间的对应关系，计算两个图像之间的投影变换矩阵
                HOperatorSet.ProjMatchPointsRansac(ImageF,
                                                   ImageT,
                                                   RowJunctionsF,
                                                   ColJunctionsF,
                                                   RowJunctionsT,
                                                   ColJunctionsT,
                                                   "ncc",
                                                   10,
                                                   0,
                                                   0,
                                                   imageHeight,
                                                   imageWidth,
                                                   0,
                                                   0.5,
                                                   "gold_standard",
                                                   2,
                                                   42,
                                                   out HTuple ProjMatrix,
                                                   out HTuple Points1,
                                                   out HTuple Points2);
                Debug.WriteLine(watch.ElapsedMilliseconds.ToString());
                ProjMatrices = ProjMatrices.TupleConcat(ProjMatrix);
                Rows1 = Rows1.TupleConcat(RowJunctionsF.TupleSelect(Points1));
                Cols1 = Cols1.TupleConcat(ColJunctionsF.TupleSelect(Points1));
                Rows2 = Rows2.TupleConcat(RowJunctionsT.TupleSelect(Points2));
                Cols2 = Cols2.TupleConcat(ColJunctionsT.TupleSelect(Points2));
                NumMatches = NumMatches.TupleConcat(Points1.Length);
            }

            //将多个图像合并成一个镶嵌图像
            //HObject MosaicImage;
            //HOperatorSet.GenProjectiveMosaic(images, out MosaicImage, 2, from, to, ProjMatrices, "default", "false", out MosaicMatrices2D);
            //对图像马赛克进行捆绑调整
            HOperatorSet.BundleAdjustMosaic(rows * columns, 1, from, to, ProjMatrices, Rows1, Cols1, Rows2, Cols2, NumMatches, "rigid", out HTuple MosaicMatrices2D, out HTuple Rows, out HTuple Cols, out HTuple Error);
            //输出修正后的图像
            HOperatorSet.GenBundleAdjustedMosaic(images, out HObject MosaicImageRigid, MosaicMatrices2D, "default", "false", out HTuple TransMatrix2D);
            //HOperatorSet.WriteImage(MosaicImageRigid, "jpg", 0, ".\\拼接图像");
            watch.Stop();
            Debug.WriteLine(watch.ElapsedMilliseconds.ToString());
            return MosaicImageRigid;
        }

        /// <summary>
        /// 获取两两拼接的图像对数组
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        private void GetMosaicImgPairs(int rows, int columns, out int[] from, out int[] to)
        {
            if (rows <= 0 || columns <= 0 || rows + columns < 3)
            {
                from = to = null;
                return;
            }
            List<int> fromList = new List<int>();
            List<int> toList = new List<int>();
            //int combinationNum = (rows + columns) * 3 - 8;

            for (int i = 1; i <= rows * columns; i++)
            {
                int i_row = (int)Math.Ceiling(i / (float)columns);
                int i_column = i - (i_row - 1) * columns;
                if (i_column < columns)
                {
                    fromList.Add(i);
                    toList.Add(i + 1);
                }
                if (i_row < rows)
                {
                    fromList.Add(i);
                    toList.Add(i + columns);
                }
            }
            from = fromList.ToArray();
            to = toList.ToArray();
        }

        /// <summary>
        /// 寻找模板
        /// </summary>
        /// <param name="hWindow"></param>
        /// <param name="ho_Image"></param>
        /// <param name="pointXYU"></param>
        public void FindShapeModel(HWindow hWindow, HObject ho_Image, int camNo, out List<PointXYU> pixelPointXYUs, out List<PointXYU> worldPointXYUs)
        {
            HTuple angleStart = new HTuple(), angleExtent = new HTuple(), scaleMin = new HTuple(), scaleMax = new HTuple(),minScore = new HTuple();
            List<HObject> ho_Rect_List = new List<HObject>();
            pixelPointXYUs = new List<PointXYU>();
            worldPointXYUs = new List<PointXYU>();
            //HOperatorSet.GenEmptyObj(out HObject ho_SelectedContours);
            
            string roi_path = Application.StartupPath + $"\\Model\\ROI{camNo}.hobj";
            if (File.Exists(roi_path))
            {
                HOperatorSet.ReadObject(out HObject ho_region, Application.StartupPath + $"\\Model\\ROI{camNo}");
                hWindow.SetDraw("margin");
                hWindow.DispObj(ho_region);
                HOperatorSet.ReduceDomain(ho_Image, ho_region, out ho_Image);
            }
            for (int i = 0; i < hv_ModelID.Length; i++)
            {
                HTuple hv_ModelParams = hv_ModelParams_List[i];
                angleStart = angleStart.TupleConcat(-0.39);
                angleExtent = angleExtent.TupleConcat(0.79);
                scaleMin = scaleMin.TupleConcat(0.6);
                scaleMax = scaleMax.TupleConcat(1.4);
                minScore = minScore.TupleConcat(hv_ModelParams[2].F);
                HOperatorSet.GenRectangle2ContourXld(out HObject ho_SubRect, 0, 0, 0, hv_ModelParams[0].F / 2, hv_ModelParams[1].F / 2);
                ho_Rect_List.Add(ho_SubRect);
            }
#if NCC
            HOperatorSet.FindNccModels(ho_Image, hv_ModelID, angleStart, angleExtent, minScore, 0, 0.2, "true", 0, out HTuple hv_RowCheck, out HTuple hv_ColumnCheck, out HTuple hv_AngleCheck, out HTuple hv_Score,out HTuple modelIndex);
#else
            HOperatorSet.FindScaledShapeModels(ho_Image, hv_ModelID, angleStart, angleExtent, scaleMin, scaleMax, minScore, 0, 0.5, "interpolation",
            6, 0.8, out HTuple hv_RowCheck, out HTuple hv_ColumnCheck, out HTuple hv_AngleCheck, out HTuple hv_Scale,
            out HTuple hv_Score,out HTuple modelIndex);
#endif
            hWindow.SetColor("blue");
            for (int i = 0; i < hv_Score.Length; i++)
            {
                int model = modelIndex[i];
                HTuple hv_ModelParams = hv_ModelParams_List[model];
                HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_RowCheck[i], hv_ColumnCheck[i], hv_AngleCheck[i], out HTuple hv_MovementOfObject);
                HOperatorSet.AffineTransContourXld(ho_Rect_List[model], out HObject ho_ModelAtNewPosition,
                    hv_MovementOfObject);
                HOperatorSet.GenRegionContourXld(ho_ModelAtNewPosition, out HObject ho_Region, "filled");
                HOperatorSet.AffineTransPixel(hv_MovementOfObject, hv_ModelParams[4], hv_ModelParams[5], out HTuple hv_RowTrans, out HTuple hv_ColTrans);
                HOperatorSet.GenCrossContourXld(out HObject ho_Cross, hv_RowTrans, hv_ColTrans, 10, 0.785398);
                HOperatorSet.GenCircleContourXld(out HObject ho_ContCircle, hv_RowTrans, hv_ColTrans, 10, 0, 6.28318, "positive", 1);
                HOperatorSet.Intensity(ho_Region, ho_Image, out HTuple hv_Mean, out _);
                #region 寻找区域内频率最高的灰度值
                //HOperatorSet.GrayHisto(ho_Region, ho_Image, out HTuple absoluteHisto,out _);
                //HOperatorSet.TupleSortIndex(absoluteHisto, out HTuple indices);
                //int maxFreqGray = indices[255];
                #endregion
                bool flag;
                if (hv_ModelParams[6] == 0)
                {
                    flag = hv_Mean >= hv_ModelParams[3];
                }
                else
                {
                    flag = hv_Mean <= hv_ModelParams[3];
                }
                if (flag)
                {
                    hWindow.DispObj(ho_ModelAtNewPosition);
                    hWindow.DispObj(ho_Cross);
                    hWindow.DispObj(ho_ContCircle);
                    HTuple hv_text;
                    pixelPointXYUs.Add(new PointXYU() { X = hv_RowTrans[0].F, Y = hv_ColTrans[0].F, U = hv_AngleCheck[i].F });

                    string calibration_Path = Application.StartupPath + $"\\Calibration\\Cam{camNo}_homMat2D";
                    if (File.Exists(calibration_Path))
                    {
                        HOperatorSet.ReadTuple(calibration_Path, out hv_homMat2D);
                    }
                    if (hv_homMat2D != null)
                    {
                        HOperatorSet.AffineTransPoint2d(hv_homMat2D, hv_RowTrans, hv_ColTrans, out HTuple hv_WorldRow, out HTuple hv_WorldCol);
                        worldPointXYUs.Add(new PointXYU() { X = hv_WorldRow[0].F, Y = hv_WorldCol[0].F, U = hv_AngleCheck[i].F });
                        hv_text = $"(M:{modelNoList[model]},\n,{hv_WorldRow.TupleString(".2f")},\n{hv_WorldCol.TupleString(".2f")},\n{hv_AngleCheck.TupleSelect(i).TupleString(".2f")})";
                    }
                    else
                    {
                        worldPointXYUs.Add(new PointXYU() { X = hv_RowTrans[0].F, Y = hv_ColTrans[0].F, U = hv_AngleCheck[i].F });
                        hv_text = $"(M:{modelNoList[model]},\n,{hv_RowTrans.TupleString(".2f")},\n{hv_ColTrans.TupleString(".2f")},\n{hv_AngleCheck.TupleSelect(i).TupleString(".2f")})";
                    }
                    hWindow.DispText(hv_text, "image", hv_RowTrans+80, hv_ColTrans-80, new HTuple("black"), "box", "true");
                }
            }
            #region 去除中心点小于电池距离的点位
            //List<PointXYU> inspectPoints = pixelPointXYUs;
            //List<int> removeIndex = new List<int>();
            //for (int i = 0; i < inspectPoints.Count-1; i++)
            //{
            //    for (int j = i+1; j < inspectPoints.Count; j++)
            //    {
            //        HOperatorSet.DistancePp(inspectPoints[i].X, inspectPoints[i].Y, inspectPoints[j].X, inspectPoints[j].Y, out HTuple distance);
            //        if(distance < 50)
            //        {
            //            removeIndex.Add(i-removeIndex.Count);
            //            break;
            //        }
            //    }
            //}
            
            //foreach (int i in removeIndex)
            //{
            //    pixelPointXYUs.RemoveAt(i);
            //    worldPointXYUs.RemoveAt(i);
            //}
            #endregion
            worldPointXYUs.Sort();
            pixelPointXYUs.Sort();
        }

        // Chapter: Graphics / Text
        // Short Description: This procedure writes a text message. 
        public void DispMessage(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
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

        private void ProjMatchPointsRansacPyramid(HObject ho_ImageF, HObject ho_ImageT,
      HTuple hv_NumLevels, out HTuple hv_RowsF, out HTuple hv_ColsF, out HTuple hv_RowsT,
      out HTuple hv_ColsT, out HTuple hv_ProjMatrix, out HTuple hv_Points1, out HTuple hv_Points2)
        {
            hv_ColsF = new HTuple();
            hv_ColsT = new HTuple();
            hv_Points1 = new HTuple();
            hv_Points2 = new HTuple();
            hv_ProjMatrix = new HTuple();
            hv_RowsF = new HTuple();
            hv_RowsT = new HTuple();
            try
            {
                //This procedure uses an image pyramid to calculate
                //the projective transformation between two images.
                //
                //If UseRigidTransformation is set to true,
                //the results are restricted to rigid transformations
                //(instead of projective transformations)
                HTuple hv_UseRigidTransformation = 1;
                // Local control variables 
                //Parameters for the Harris point detector
                HTuple hv_SigmaGrad = 0.7;
                HTuple hv_SigmaSmooth = 2;
                HTuple hv_Alpha = 0.04;
                HTuple hv_Threshold = 50;
                //
                //Generate image pyramids for both input images
                HOperatorSet.GenGaussPyramid(ho_ImageF, out HObject ho_ImageFPyramid, "constant", 0.5);
                HOperatorSet.GenGaussPyramid(ho_ImageT, out HObject ho_ImageTPyramid, "constant", 0.5);
                //At the beginning, no approximated projection is known
                HTuple hv_HomMat2DGuide = new HTuple();
                //
                //Calculate projective transformation on each pyramid level
                for (int level= hv_NumLevels;level>0;level--)
                {
                    //Select images from image pyramid
                    HOperatorSet.SelectObj(ho_ImageFPyramid, out HObject ho_ImageFLevel, level);
                    HOperatorSet.SelectObj(ho_ImageTPyramid, out HObject ho_ImageTLevel, level);
                    //Extract interest points in both images
                    HOperatorSet.PointsHarris(ho_ImageFLevel, hv_SigmaGrad, hv_SigmaSmooth, hv_Alpha,
                        hv_Threshold, out hv_RowsF, out hv_ColsF);
                    HOperatorSet.PointsHarris(ho_ImageTLevel, hv_SigmaGrad, hv_SigmaSmooth, hv_Alpha,
                        hv_Threshold, out hv_RowsT, out hv_ColsT);
                    //Calculate projection from point correspondences
                    if (hv_HomMat2DGuide.Length == 0)
                    {
                        //On the highest pyramid level, use proj_mathc_points_ransac
                        HOperatorSet.GetImageSize(ho_ImageFLevel, out HTuple hv_Width, out HTuple hv_Height);
                        HOperatorSet.ProjMatchPointsRansac(ho_ImageFLevel, ho_ImageTLevel, hv_RowsF,
                            hv_ColsF, hv_RowsT, hv_ColsT, "ncc", 10, 0, 0, hv_Height, new HTuple(),
                            new HTuple(-40).TupleRad().TupleConcat(new HTuple(40).TupleRad()
                            ), 0.5, "gold_standard", 2.5 * new HTuple(2).TuplePow(4 - level),
                            42, out hv_ProjMatrix, out hv_Points1, out hv_Points2);
                    }
                    else
                    {
                        //On lower levels, use approximation from upper level as
                        //input for proj_match_points_ransac_guided
                        HOperatorSet.ProjMatchPointsRansacGuided(ho_ImageFLevel, ho_ImageTLevel,
                            hv_RowsF, hv_ColsF, hv_RowsT, hv_ColsT, "ncc", 10, hv_HomMat2DGuide,
                            10 * new HTuple(2.0).TuplePow(4.0 - level), 0.5, "gold_standard",
                            2.5 * new HTuple(2.0).TuplePow(4.0 - level), 42, out hv_ProjMatrix,
                            out hv_Points1, out hv_Points2);
                    }
                    if (hv_UseRigidTransformation != 0)
                    {
                        //Use found point correspondences to calculate rigid transformation
                        //with vector_to_rigid
                        //Note, that the resulting transformation of proj_match_points_ransac_guided
                        //is ignored in this case.
                        HTuple hv_RowF = hv_RowsF.TupleSelect(hv_Points1);
                        HTuple hv_ColF = hv_ColsF.TupleSelect(hv_Points1);
                        HTuple hv_RowT = hv_RowsT.TupleSelect(hv_Points2);
                        HTuple hv_ColT = hv_ColsT.TupleSelect(hv_Points2);
                        HOperatorSet.VectorToRigid(hv_RowF + 0.5, hv_ColF + 0.5, hv_RowT + 0.5, hv_ColT + 0.5,
                            out hv_ProjMatrix);
                        hv_ProjMatrix = hv_ProjMatrix.TupleConcat(new HTuple(0).TupleConcat(
                            0)).TupleConcat(1);
                    }
                    //To be used on the next lower pyramid level, the projection has
                    //to be adjusted to the new scale.
                    HOperatorSet.HomMat2dScaleLocal(hv_ProjMatrix, 0.5, 0.5, out hv_HomMat2DGuide);
                    HOperatorSet.HomMat2dScale(hv_HomMat2DGuide, 2, 2, 0, 0, out hv_HomMat2DGuide);
                }
            }
            catch (HalconException HDevExpDefaultException)
            {
                throw HDevExpDefaultException;
            }
        }
    }
}
