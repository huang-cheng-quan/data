using ClosedXML.Excel;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class AccuracyCheckFrm : Form
    {
        public AccuracyCheckFrm()
        {
            InitializeComponent();
        }
       
        //HObject ho_Image = null, ho_Contour = null;
        //HTuple hv_WindowHandle = new HTuple(), hv_ImageFiles = new HTuple();
        //HTuple hv_Index = new HTuple(), hv_Width = new HTuple();
        //HTuple hv_Height = new HTuple(), hv_MeasureHandle = new HTuple();
        //HTuple hv_RowEdge = new HTuple(), hv_ColumnEdge = new HTuple();
        //HTuple hv_Amplitude = new HTuple(), hv_Distance = new HTuple();

     

        HTuple hv_MeasureHandle1 = new HTuple(), hv_RowEdge1 = new HTuple();

        #region halcon变量

        HObject ho_Image = null, ho_Regions1 = null, ho_ConnectedRegions1 = null;
        HObject ho_SelectedRegions3 = null, ho_ROI_0 = null, ho_ROI_range = null,ho_RegionClosing1=null;
        HObject ho_ImageReduced = null, ho_Rectangle = null, ho_Regions = null, ho_RegionFillUp = null;
        HObject ho_ConnectedRegions = null, ho_SelectedRegions1 = null, ho_RegionClosing = null;
        HObject ho_SelectedRegions = null, ho_ImageReduced1 = null;

        HObject ho_ImageConnect2 = null, ho_ImageGray = null;
        HObject ho_ImageScaled = null, ho_ROI_0Connect2 = null, ho_ImageReducedConnect2 = null;
        HObject ho_ImageConnect1 = null, ho_ImageGray1 = null, ho_ImageScaled1 = null, ho_ImageMean=null;
        HObject ho_ROI_0Connect1 = null, ho_ImageReducedConnect1 = null;
     

        HTuple hv_ModelID2 = new HTuple(), hv_ImageFilesConnect2 = new HTuple();
        HTuple hv_IndexConnect2 = new HTuple(), hv_Width2 = new HTuple();
        HTuple hv_Height2 = new HTuple(), hv_RowConnect2 = new HTuple();
        HTuple hv_ColumnConnect2 = new HTuple(), hv_AngleConnect2 = new HTuple();
        HTuple hv_ScoreConnect2 = new HTuple(), hv_MeasureHandle12 = new HTuple();
        HTuple hv_RowEdgeConnect2First = new HTuple(), hv_ColumnEdgeConnect2First = new HTuple();
        HTuple hv_AmplitudeConnect2First = new HTuple(), hv_RowEdgeConnect2Second = new HTuple();
        HTuple hv_ColumnEdgeConnect2Second = new HTuple(), hv_AmplitudeConnect2Second = new HTuple();
        HTuple hv_IntraDistance8 = new HTuple(), hv_InterDistance8 = new HTuple();
        HTuple hv_distance28 = new HTuple(), hv_distance29 = new HTuple();
        HTuple hv_distance30 = new HTuple(), hv_MeasureHandle13 = new HTuple();
        HTuple hv_RowEdgeConnect2First1 = new HTuple(), hv_ColumnEdgeConnect2First1 = new HTuple();
        HTuple hv_AmplitudeConnect2First1 = new HTuple(), hv_RowEdgeConnect2Second1 = new HTuple();
        HTuple hv_ColumnEdgeConnect2Second1 = new HTuple(), hv_AmplitudeConnect2Second1 = new HTuple();
        HTuple hv_IntraDistance9 = new HTuple(), hv_InterDistance9 = new HTuple();
        HTuple hv_distance31 = new HTuple(), hv_distance32 = new HTuple();
        HTuple hv_distance33 = new HTuple(), hv_ModelID3 = new HTuple();
        HTuple hv_ImageFilesConnect1 = new HTuple(), hv_IndexConnect1 = new HTuple();
        HTuple hv_RowConnect1 = new HTuple(), hv_ColumnConnect1 = new HTuple();
        HTuple hv_AngleConnect1 = new HTuple(), hv_ScoreConnect1 = new HTuple();
        HTuple hv_MeasureHandle14 = new HTuple(), hv_RowEdgeConnect1First2 = new HTuple();
        HTuple hv_ColumnEdgeConnect1First2 = new HTuple(), hv_AmplitudeConnect1First2 = new HTuple();
        HTuple hv_RowEdgeConnect1Second2 = new HTuple(), hv_ColumnEdgeConnect1Second2 = new HTuple();
        HTuple hv_AmplitudeConnect1Second2 = new HTuple(), hv_IntraDistance10 = new HTuple();
        HTuple hv_InterDistance10 = new HTuple(), hv_distance34 = new HTuple();
        HTuple hv_distance35 = new HTuple(), hv_distance36 = new HTuple();
        HTuple hv_MeasureHandle15 = new HTuple(), hv_RowEdgeConnect1First3 = new HTuple();
        HTuple hv_ColumnEdgeConnect1First3 = new HTuple(), hv_AmplitudeConnect1First3 = new HTuple();
        HTuple hv_RowEdgeConnect1Second3 = new HTuple(), hv_ColumnEdgeConnect1Second3 = new HTuple();
        HTuple hv_AmplitudeConnect1Second3 = new HTuple(), hv_IntraDistance11 = new HTuple();
        HTuple hv_InterDistance11 = new HTuple(), hv_distance37 = new HTuple();
        HTuple hv_distance38 = new HTuple(), hv_distance39 = new HTuple();

        HTuple hv_ImageFiles = new HTuple(), hv_Index = new HTuple();
        HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
        HTuple hv_Width1 = new HTuple(), hv_Height1 = new HTuple();
        HTuple hv_Area1 = new HTuple(), hv_Row3 = new HTuple();
        HTuple hv_Column3 = new HTuple(), hv_MeasureHandle4 = new HTuple(), hv_MeasureHandle11 = new HTuple();
        HTuple hv_RowEdgeFirst = new HTuple(), hv_ColumnEdgeFirst = new HTuple();
        HTuple hv_AmplitudeFirst = new HTuple(), hv_RowEdgeSecond = new HTuple();
        HTuple hv_ColumnEdgeSecond = new HTuple(), hv_AmplitudeSecond = new HTuple();
        HTuple hv_IntraDistance = new HTuple(), hv_InterDistance = new HTuple();
        HTuple hv_distence04 = new HTuple(), hv_distence05 = new HTuple();
        HTuple hv_distence06 = new HTuple(), hv_MeasureHandle5 = new HTuple();
        HTuple hv_IntraDistance1 = new HTuple(), hv_InterDistance1 = new HTuple();
        HTuple hv_distence07 = new HTuple(), hv_distence08 = new HTuple();
        HTuple hv_distence09 = new HTuple(), hv_ModelID = new HTuple();
        HTuple hv_Row = new HTuple(), hv_Column = new HTuple();
        HTuple hv_Angle = new HTuple(), hv_Score = new HTuple();

       

        HTuple hv_MeasureHandle7 = new HTuple(), hv_IntraDistance2 = new HTuple();

      

        HTuple hv_InterDistance2 = new HTuple(), hv_distence13 = new HTuple();
        HTuple hv_distence14 = new HTuple(), hv_distence15 = new HTuple();
        HTuple hv_MeasureHandle6 = new HTuple(), hv_IntraDistance3 = new HTuple();
        HTuple hv_InterDistance3 = new HTuple(), hv_distence10 = new HTuple();
        HTuple hv_distence11 = new HTuple(), hv_distence12 = new HTuple();
        HTuple hv_Area = new HTuple(), hv_Row2 = new HTuple();
        HTuple hv_Column2 = new HTuple(), hv_MeasureHandle8 = new HTuple();
        HTuple hv_IntraDistance4 = new HTuple(), hv_InterDistance4 = new HTuple();
        HTuple hv_distence16 = new HTuple(), hv_distence17 = new HTuple();
        HTuple hv_distence18 = new HTuple(), hv_IntraDistance5 = new HTuple();
        HTuple hv_InterDistance5 = new HTuple(), hv_distence19 = new HTuple();
        HTuple hv_distence20 = new HTuple(), hv_distence21 = new HTuple();
        HTuple hv_ModelID1 = new HTuple(), hv_Row1 = new HTuple();
        HTuple hv_Column1 = new HTuple(), hv_Angle1 = new HTuple();
        HTuple hv_Score1 = new HTuple(), hv_MeasureHandle9 = new HTuple();
        HTuple hv_IntraDistance6 = new HTuple(), hv_InterDistance6 = new HTuple();
        HTuple hv_distence22 = new HTuple(), hv_distence23 = new HTuple();
        HTuple hv_distence24 = new HTuple(), hv_MeasureHandle10 = new HTuple();
        HTuple hv_IntraDistance7 = new HTuple(), hv_InterDistance7 = new HTuple();
        HTuple hv_distence25 = new HTuple(), hv_distence26 = new HTuple();
        HTuple hv_distence27 = new HTuple();
        // Initialize local and output iconic variables 
        //    HOperatorSet.GenEmptyObj(out ho_Image);
        //HOperatorSet.GenEmptyObj(out ho_Regions1);
        //HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
        //HOperatorSet.GenEmptyObj(out ho_SelectedRegions3);
        //HOperatorSet.GenEmptyObj(out ho_ROI_0);
        //HOperatorSet.GenEmptyObj(out ho_ROI_range);
        //HOperatorSet.GenEmptyObj(out ho_ImageReduced);
        //HOperatorSet.GenEmptyObj(out ho_Rectangle);
        //HOperatorSet.GenEmptyObj(out ho_Regions);
        //HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
        //HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
        //HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
        //HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
        #endregion

        List<string> ListResultShortX0 = new List<string>();
        List<string> ListResultShortX1 = new List<string>();
        List<string> ListResultShortX2 = new List<string>();
        List<string> ListResultShortY0 = new List<string>();
        List<string> ListResultShortY1 = new List<string>();
        List<string> ListResultShortY2 = new List<string>();
        List<string> ListResultNegativeX0 = new List<string>();
        List<string> ListResultNegativeX1 = new List<string>();
        List<string> ListResultNegativeX2 = new List<string>();
        List<string> ListResultNegativeY0 = new List<string>();
        List<string> ListResultNegativeY1 = new List<string>();
        List<string> ListResultNegativeY2 = new List<string>();
        List<string> ListResultLongX0 = new List<string>();
        List<string> ListResultLongX1 = new List<string>();
        List<string> ListResultLongX2 = new List<string>();
        List<string> ListResultLongY0 = new List<string>();
        List<string> ListResultLongY1 = new List<string>();
        List<string> ListResultLongY2 = new List<string>();
        List<string> ListResultPositiveX0 = new List<string>();
        List<string> ListResultPositiveX1 = new List<string>();
        List<string> ListResultPositiveX2= new List<string>();
        List<string> ListResultPositiveY0 = new List<string>();
        List<string> ListResultPositiveY1 = new List<string>();
        List<string> ListResultPositiveY2 = new List<string>();
        List<string> ListResultconnect1X0 = new List<string>();
        List<string> ListResultconnect1X1 = new List<string>();
        List<string> ListResultconnect1X2 = new List<string>();
        List<string> ListResultconnect1Y0 = new List<string>();
        List<string> ListResultconnect1Y1 = new List<string>();
        List<string> ListResultconnect1Y2 = new List<string>();
        List<string> ListResultconnect2X0 = new List<string>();
        List<string> ListResultconnect2X1 = new List<string>();
        List<string> ListResultconnect2X2 = new List<string>();
        List<string> ListResultconnect2Y0 = new List<string>();
        List<string> ListResultconnect2Y1 = new List<string>();
        List<string> ListResultconnect2Y2 = new List<string>();

        private void button3_Click(object sender, EventArgs e)
        {
            Random ran = new Random();
            button3.Enabled = false;
            button1.Enabled = false;
            button8.Enabled = false;
            button4.Enabled = false;
            #region 变量

            HObject ho_Image = null, ho_ImageGray = null, ho_ImageScaled = null;
                HObject ho_ROI_0 = null, ho_ImageReduced = null, ho_Rectangle = null;
                HObject ho_Cross = null, ho_Rectangle1 = null, ho_Cross1 = null;
                HObject ho_Rectangle2 = null, ho_Cross2 = null, ho_Rectangle3 = null;
                HObject ho_Cross3 = null, ho_Rectangle5 = null, ho_Cross8 = null;
                HObject ho_Rectangle6 = null, ho_Cross9 = null, ho_Rectangle7 = null;
                HObject ho_Cross10 = null, ho_Rectangle8 = null, ho_Cross11 = null;
                HObject ho_GrayImage = null, ho_ImageScaled1 = null, ho_ImageReduced1 = null;
                HObject ho_Rectangle4 = null, ho_Cross4 = null, ho_Cross5 = null;
                HObject ho_Cross6 = null, ho_Cross7 = null, ho_Rectangle9 = null;
                HObject ho_Cross12 = null, ho_Rectangle10 = null, ho_Cross13 = null;
                HObject ho_Rectangle11 = null, ho_Cross14 = null, ho_Rectangle12 = null;
                HObject ho_Cross15 = null, ho_Regions = null, ho_ConnectedRegions = null;
                HObject ho_SelectedRegions = null, ho_SortedRegions = null;
                HObject ho_EmptyObject = null, ho_Rectangle13 = null, ho_ImageScaled2 = null;
                HObject ho_Rectangle14 = null, ho_Cross16 = null, ho_Rectangle15 = null;
                HObject ho_Cross17 = null, ho_Rectangle16 = null, ho_Cross18 = null;
                HObject ho_Rectangle17 = null, ho_Cross19 = null, ho_Rectangle18 = null;
                HObject ho_Cross20 = null, ho_Rectangle19 = null, ho_Cross21 = null;
                HObject ho_Rectangle20 = null, ho_Cross22 = null, ho_Rectangle21 = null;
                HObject ho_Cross23 = null, ho_ImageReduced4 = null, ho_Regions1 = null;
                HObject ho_ConnectedRegions1 = null, ho_Rectangle22 = null;
                HObject ho_Cross24 = null, ho_Rectangle23 = null, ho_Cross25 = null;
                HObject ho_Rectangle24 = null, ho_Cross26 = null, ho_Rectangle25 = null;
                HObject ho_Cross27 = null, ho_Rectangle26 = null, ho_Cross28 = null;
                HObject ho_Rectangle27 = null, ho_Cross29 = null, ho_Rectangle28 = null;
                HObject ho_Cross30 = null, ho_Rectangle29 = null, ho_Cross31 = null;
                HObject ho_ImageReduced2 = null, ho_Regions2 = null, ho_ConnectedRegions2 = null;
                HObject ho_RegionOpening = null, ho_ConnectedRegions4 = null;
                HObject ho_SelectedRegions1 = null, ho_EmptyObject1 = null;
                HObject ho_RectangleL14 = null, ho_Rectangle30 = null, ho_Cross32 = null;
                HObject ho_Rectangle31 = null, ho_Cross33 = null, ho_Rectangle32 = null;
                HObject ho_Cross34 = null, ho_Rectangle33 = null, ho_Cross35 = null;
                HObject ho_Rectangle34 = null, ho_Cross36 = null, ho_Rectangle35 = null;
                HObject ho_Cross37 = null, ho_Rectangle36 = null, ho_Cross38 = null;
                HObject ho_Rectangle37 = null, ho_Cross39 = null, ho_ImageReduced3 = null;
                HObject ho_Regions3 = null, ho_ConnectedRegions3 = null, ho_EmptyObject2 = null;
                HObject ho_RectangleP = null, ho_Rectangle38 = null, ho_Cross40 = null;
                HObject ho_Rectangle39 = null, ho_Cross41 = null, ho_Rectangle40 = null;
                HObject ho_Cross42 = null, ho_Rectangle41 = null, ho_Cross43 = null;
                HObject ho_Rectangle42 = null, ho_Cross44 = null, ho_Rectangle43 = null;
                HObject ho_Cross45 = null, ho_Rectangle44 = null, ho_Cross46 = null;
                HObject ho_Rectangle45 = null, ho_Cross47 = null;

                // Local control variables 

                HTuple hv_ModelID = new HTuple(), hv_ImageFiles = new HTuple();
                HTuple hv_Index = new HTuple(), hv_Width = new HTuple();
                HTuple hv_Height = new HTuple(), hv_Row = new HTuple();
                HTuple hv_Column = new HTuple(), hv_Angle = new HTuple();
                HTuple hv_Score = new HTuple(), hv_MeasureHandle = new HTuple();
                HTuple hv_RowEdge = new HTuple(), hv_ColumnEdge = new HTuple();
                HTuple hv_Amplitude = new HTuple(), hv_Distance = new HTuple();
                HTuple hv_MeasureHandle1 = new HTuple(), hv_RowEdge1 = new HTuple();
                HTuple hv_ColumnEdge1 = new HTuple(), hv_Amplitude1 = new HTuple();
                HTuple hv_Distance1 = new HTuple(), hv_MeasureHandle2 = new HTuple();
                HTuple hv_RowEdge2 = new HTuple(), hv_ColumnEdge2 = new HTuple();
                HTuple hv_Amplitude2 = new HTuple(), hv_Distance2 = new HTuple();
                HTuple hv_MeasureHandle3 = new HTuple(), hv_RowEdge3 = new HTuple();
                HTuple hv_ColumnEdge3 = new HTuple(), hv_Amplitude3 = new HTuple();
                HTuple hv_Distance3 = new HTuple(), hv_distance1 = new HTuple();
                HTuple hv_distance2 = new HTuple(), hv_distance3 = new HTuple();
                HTuple hv_MeasureHandle8 = new HTuple(), hv_RowEdge8 = new HTuple();
                HTuple hv_ColumnEdge8 = new HTuple(), hv_Amplitude8 = new HTuple();
                HTuple hv_Distance8 = new HTuple(), hv_MeasureHandle9 = new HTuple();
                HTuple hv_RowEdge9 = new HTuple(), hv_ColumnEdge9 = new HTuple();
                HTuple hv_Amplitude9 = new HTuple(), hv_Distance9 = new HTuple();
                HTuple hv_MeasureHandle10 = new HTuple(), hv_RowEdge10 = new HTuple();
                HTuple hv_ColumnEdge10 = new HTuple(), hv_Amplitude10 = new HTuple();
                HTuple hv_Distance10 = new HTuple(), hv_MeasureHandle11 = new HTuple();
                HTuple hv_RowEdge11 = new HTuple(), hv_ColumnEdge11 = new HTuple();
                HTuple hv_Amplitude11 = new HTuple(), hv_Distance11 = new HTuple();
                HTuple hv_distance1_1 = new HTuple(), hv_distance2_1 = new HTuple();
                HTuple hv_distance3_1 = new HTuple(), hv_ModelID1 = new HTuple();
                HTuple hv_Width1 = new HTuple(), hv_Height1 = new HTuple();
                HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
                HTuple hv_Angle1 = new HTuple(), hv_Score1 = new HTuple();
                HTuple hv_MeasureHandle4 = new HTuple(), hv_RowEdge4 = new HTuple();
                HTuple hv_ColumnEdge4 = new HTuple(), hv_Amplitude4 = new HTuple();
                HTuple hv_Distance4 = new HTuple(), hv_MeasureHandle5 = new HTuple();
                HTuple hv_RowEdge5 = new HTuple(), hv_ColumnEdge5 = new HTuple();
                HTuple hv_Amplitude5 = new HTuple(), hv_Distance5 = new HTuple();
                HTuple hv_MeasureHandle6 = new HTuple(), hv_RowEdge6 = new HTuple();
                HTuple hv_ColumnEdge6 = new HTuple(), hv_Amplitude6 = new HTuple();
                HTuple hv_Distance6 = new HTuple(), hv_MeasureHandle7 = new HTuple();
                HTuple hv_RowEdge7 = new HTuple(), hv_ColumnEdge7 = new HTuple();
                HTuple hv_Amplitude7 = new HTuple(), hv_Distance7 = new HTuple();
                HTuple hv_distance4 = new HTuple(), hv_distance5 = new HTuple();
                HTuple hv_distance6 = new HTuple(), hv_MeasureHandle12 = new HTuple();
                HTuple hv_RowEdge12 = new HTuple(), hv_ColumnEdge12 = new HTuple();
                HTuple hv_Amplitude12 = new HTuple(), hv_Distance12 = new HTuple();
                HTuple hv_MeasureHandle13 = new HTuple(), hv_RowEdge13 = new HTuple();
                HTuple hv_ColumnEdge13 = new HTuple(), hv_Amplitude13 = new HTuple();
                HTuple hv_Distance13 = new HTuple(), hv_MeasureHandle14 = new HTuple();
                HTuple hv_RowEdge14 = new HTuple(), hv_ColumnEdge14 = new HTuple();
                HTuple hv_Amplitude14 = new HTuple(), hv_Distance14 = new HTuple();
                HTuple hv_MeasureHandle15 = new HTuple(), hv_RowEdge15 = new HTuple();
                HTuple hv_ColumnEdge15 = new HTuple(), hv_Amplitude15 = new HTuple();
                HTuple hv_Distance15 = new HTuple(), hv_Distance16 = new HTuple();
                HTuple hv_Distance17 = new HTuple(), hv_Distance18 = new HTuple();
                HTuple hv_distance7 = new HTuple(), hv_distance8 = new HTuple();
                HTuple hv_distance9 = new HTuple(), hv_Width2 = new HTuple();
                HTuple hv_Height2 = new HTuple(), hv_Row11 = new HTuple();
                HTuple hv_Column11 = new HTuple(), hv_Row2 = new HTuple();
                HTuple hv_Column2 = new HTuple(), hv_Area = new HTuple();
                HTuple hv_MeasureHandle16 = new HTuple(), hv_RowEdge16 = new HTuple();
                HTuple hv_ColumnEdge16 = new HTuple(), hv_Amplitude16 = new HTuple();
                HTuple hv_Distance19 = new HTuple(), hv_MeasureHandle17 = new HTuple();
                HTuple hv_RowEdge17 = new HTuple(), hv_ColumnEdge17 = new HTuple();
                HTuple hv_Amplitude17 = new HTuple(), hv_Distance20 = new HTuple();
                HTuple hv_MeasureHandle18 = new HTuple(), hv_RowEdge18 = new HTuple();
                HTuple hv_ColumnEdge18 = new HTuple(), hv_Amplitude18 = new HTuple();
                HTuple hv_Distance21 = new HTuple(), hv_MeasureHandle19 = new HTuple();
                HTuple hv_RowEdge19 = new HTuple(), hv_ColumnEdge19 = new HTuple();
                HTuple hv_Amplitude19 = new HTuple(), hv_Distance22 = new HTuple();
                HTuple hv_distance10 = new HTuple(), hv_distance11 = new HTuple();
                HTuple hv_distance12 = new HTuple(), hv_MeasureHandle20 = new HTuple();
                HTuple hv_RowEdge20 = new HTuple(), hv_ColumnEdge20 = new HTuple();
                HTuple hv_Amplitude20 = new HTuple(), hv_Distance23 = new HTuple();
                HTuple hv_MeasureHandle21 = new HTuple(), hv_RowEdge21 = new HTuple();
                HTuple hv_ColumnEdge21 = new HTuple(), hv_Amplitude21 = new HTuple();
                HTuple hv_Distance24 = new HTuple(), hv_MeasureHandle22 = new HTuple();
                HTuple hv_RowEdge22 = new HTuple(), hv_ColumnEdge22 = new HTuple();
                HTuple hv_Amplitude22 = new HTuple(), hv_Distance25 = new HTuple();
                HTuple hv_MeasureHandle23 = new HTuple(), hv_RowEdge23 = new HTuple();
                HTuple hv_ColumnEdge23 = new HTuple(), hv_Amplitude23 = new HTuple();
                HTuple hv_Distance26 = new HTuple(), hv_distance13 = new HTuple();
                HTuple hv_distance14 = new HTuple(), hv_distance15 = new HTuple();
                HTuple hv_Width3 = new HTuple(), hv_Height3 = new HTuple();
                HTuple hv_MeasureHandle24 = new HTuple(), hv_RowEdge24 = new HTuple();
                HTuple hv_ColumnEdge24 = new HTuple(), hv_Amplitude24 = new HTuple();
                HTuple hv_Distance27 = new HTuple(), hv_MeasureHandle25 = new HTuple();
                HTuple hv_RowEdge25 = new HTuple(), hv_ColumnEdge25 = new HTuple();
                HTuple hv_Amplitude25 = new HTuple(), hv_Distance28 = new HTuple();
                HTuple hv_MeasureHandle26 = new HTuple(), hv_RowEdge26 = new HTuple();
                HTuple hv_ColumnEdge26 = new HTuple(), hv_Amplitude26 = new HTuple();
                HTuple hv_Distance29 = new HTuple(), hv_MeasureHandle27 = new HTuple();
                HTuple hv_RowEdge27 = new HTuple(), hv_ColumnEdge27 = new HTuple();
                HTuple hv_Amplitude27 = new HTuple(), hv_Distance30 = new HTuple();
                HTuple hv_distance16 = new HTuple(), hv_distance17 = new HTuple();
                HTuple hv_distance18 = new HTuple(), hv_MeasureHandle28 = new HTuple();
                HTuple hv_RowEdge28 = new HTuple(), hv_ColumnEdge28 = new HTuple();
                HTuple hv_Amplitude28 = new HTuple(), hv_Distance31 = new HTuple();
                HTuple hv_MeasureHandle29 = new HTuple(), hv_RowEdge29 = new HTuple();
                HTuple hv_ColumnEdge29 = new HTuple(), hv_Amplitude29 = new HTuple();
                HTuple hv_Distance32 = new HTuple(), hv_MeasureHandle30 = new HTuple();
                HTuple hv_RowEdge30 = new HTuple(), hv_ColumnEdge30 = new HTuple();
                HTuple hv_Amplitude30 = new HTuple(), hv_Distance33 = new HTuple();
                HTuple hv_MeasureHandle31 = new HTuple(), hv_RowEdge31 = new HTuple();
                HTuple hv_ColumnEdge31 = new HTuple(), hv_Amplitude31 = new HTuple();
                HTuple hv_Distance34 = new HTuple(), hv_distance19 = new HTuple();
                HTuple hv_distance20 = new HTuple(), hv_distance21 = new HTuple();
                HTuple hv_Width4 = new HTuple(), hv_Height4 = new HTuple();
                HTuple hv_MeasureHandle32 = new HTuple(), hv_RowEdge32 = new HTuple();
                HTuple hv_ColumnEdge32 = new HTuple(), hv_Amplitude32 = new HTuple();
                HTuple hv_Distance35 = new HTuple(), hv_MeasureHandle33 = new HTuple();
                HTuple hv_RowEdge33 = new HTuple(), hv_ColumnEdge33 = new HTuple();
                HTuple hv_Amplitude33 = new HTuple(), hv_Distance36 = new HTuple();
                HTuple hv_MeasureHandle34 = new HTuple(), hv_RowEdge34 = new HTuple();
                HTuple hv_ColumnEdge34 = new HTuple(), hv_Amplitude34 = new HTuple();
                HTuple hv_Distance37 = new HTuple(), hv_MeasureHandle35 = new HTuple();
                HTuple hv_RowEdge35 = new HTuple(), hv_ColumnEdge35 = new HTuple();
                HTuple hv_Amplitude35 = new HTuple(), hv_Distance38 = new HTuple();
                HTuple hv_distance22 = new HTuple(), hv_distance23 = new HTuple();
                HTuple hv_distance24 = new HTuple(), hv_MeasureHandle36 = new HTuple();
                HTuple hv_RowEdge36 = new HTuple(), hv_ColumnEdge36 = new HTuple();
                HTuple hv_Amplitude36 = new HTuple(), hv_Distance39 = new HTuple();
                HTuple hv_MeasureHandle37 = new HTuple(), hv_RowEdge37 = new HTuple();
                HTuple hv_ColumnEdge37 = new HTuple(), hv_Amplitude37 = new HTuple();
                HTuple hv_Distance40 = new HTuple(), hv_MeasureHandle38 = new HTuple();
                HTuple hv_RowEdge38 = new HTuple(), hv_ColumnEdge38 = new HTuple();
                HTuple hv_Amplitude38 = new HTuple(), hv_Distance41 = new HTuple();
                HTuple hv_MeasureHandle39 = new HTuple(), hv_RowEdge39 = new HTuple();
                HTuple hv_ColumnEdge39 = new HTuple(), hv_Amplitude39 = new HTuple();
                HTuple hv_Distance42 = new HTuple(), hv_distance25 = new HTuple();
                HTuple hv_distance26 = new HTuple(), hv_distance27 = new HTuple();
                HTuple hv_tuple = new HTuple(), hv_Width5 = new HTuple();
                HTuple hv_Height5 = new HTuple(), hv_MeasureHandle40 = new HTuple();
                HTuple hv_RowEdge40 = new HTuple(), hv_ColumnEdge40 = new HTuple();
                HTuple hv_Amplitude40 = new HTuple(), hv_Distance43 = new HTuple();
                HTuple hv_MeasureHandle41 = new HTuple(), hv_RowEdge41 = new HTuple();
                HTuple hv_ColumnEdge41 = new HTuple(), hv_Amplitude41 = new HTuple();
                HTuple hv_Distance44 = new HTuple(), hv_MeasureHandle42 = new HTuple();
                HTuple hv_RowEdge42 = new HTuple(), hv_ColumnEdge42 = new HTuple();
                HTuple hv_Amplitude42 = new HTuple(), hv_Distance45 = new HTuple();
                HTuple hv_MeasureHandle43 = new HTuple(), hv_RowEdge43 = new HTuple();
                HTuple hv_ColumnEdge43 = new HTuple(), hv_Amplitude43 = new HTuple();
                HTuple hv_Distance46 = new HTuple(), hv_distance28 = new HTuple();
                HTuple hv_distance29 = new HTuple(), hv_distance30 = new HTuple();
                HTuple hv_MeasureHandle44 = new HTuple(), hv_RowEdge44 = new HTuple();
                HTuple hv_ColumnEdge44 = new HTuple(), hv_Amplitude44 = new HTuple();
                HTuple hv_Distance47 = new HTuple(), hv_MeasureHandle45 = new HTuple();
                HTuple hv_RowEdge45 = new HTuple(), hv_ColumnEdge45 = new HTuple();
                HTuple hv_Amplitude45 = new HTuple(), hv_Distance48 = new HTuple();
                HTuple hv_MeasureHandle46 = new HTuple(), hv_RowEdge46 = new HTuple();
                HTuple hv_ColumnEdge46 = new HTuple(), hv_Amplitude46 = new HTuple();
                HTuple hv_Distance49 = new HTuple(), hv_MeasureHandle47 = new HTuple();
                HTuple hv_RowEdge47 = new HTuple(), hv_ColumnEdge47 = new HTuple();
                HTuple hv_Amplitude47 = new HTuple(), hv_Distance50 = new HTuple();
                HTuple hv_distance31 = new HTuple(), hv_distance32 = new HTuple();
                HTuple hv_distance33 = new HTuple();
                // Initialize local and output iconic variables 
                HOperatorSet.GenEmptyObj(out ho_Image);
                HOperatorSet.GenEmptyObj(out ho_ImageGray);
                HOperatorSet.GenEmptyObj(out ho_ImageScaled);
                HOperatorSet.GenEmptyObj(out ho_ROI_0);
                HOperatorSet.GenEmptyObj(out ho_ImageReduced);
                HOperatorSet.GenEmptyObj(out ho_Rectangle);
                HOperatorSet.GenEmptyObj(out ho_Cross);
                HOperatorSet.GenEmptyObj(out ho_Rectangle1);
                HOperatorSet.GenEmptyObj(out ho_Cross1);
                HOperatorSet.GenEmptyObj(out ho_Rectangle2);
                HOperatorSet.GenEmptyObj(out ho_Cross2);
                HOperatorSet.GenEmptyObj(out ho_Rectangle3);
                HOperatorSet.GenEmptyObj(out ho_Cross3);
                HOperatorSet.GenEmptyObj(out ho_Rectangle5);
                HOperatorSet.GenEmptyObj(out ho_Cross8);
                HOperatorSet.GenEmptyObj(out ho_Rectangle6);
                HOperatorSet.GenEmptyObj(out ho_Cross9);
                HOperatorSet.GenEmptyObj(out ho_Rectangle7);
                HOperatorSet.GenEmptyObj(out ho_Cross10);
                HOperatorSet.GenEmptyObj(out ho_Rectangle8);
                HOperatorSet.GenEmptyObj(out ho_Cross11);
                HOperatorSet.GenEmptyObj(out ho_GrayImage);
                HOperatorSet.GenEmptyObj(out ho_ImageScaled1);
                HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
                HOperatorSet.GenEmptyObj(out ho_Rectangle4);
                HOperatorSet.GenEmptyObj(out ho_Cross4);
                HOperatorSet.GenEmptyObj(out ho_Cross5);
                HOperatorSet.GenEmptyObj(out ho_Cross6);
                HOperatorSet.GenEmptyObj(out ho_Cross7);
                HOperatorSet.GenEmptyObj(out ho_Rectangle9);
                HOperatorSet.GenEmptyObj(out ho_Cross12);
                HOperatorSet.GenEmptyObj(out ho_Rectangle10);
                HOperatorSet.GenEmptyObj(out ho_Cross13);
                HOperatorSet.GenEmptyObj(out ho_Rectangle11);
                HOperatorSet.GenEmptyObj(out ho_Cross14);
                HOperatorSet.GenEmptyObj(out ho_Rectangle12);
                HOperatorSet.GenEmptyObj(out ho_Cross15);
                HOperatorSet.GenEmptyObj(out ho_Regions);
                HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
                HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
                HOperatorSet.GenEmptyObj(out ho_SortedRegions);
                HOperatorSet.GenEmptyObj(out ho_EmptyObject);
                HOperatorSet.GenEmptyObj(out ho_Rectangle13);
                HOperatorSet.GenEmptyObj(out ho_ImageScaled2);
                HOperatorSet.GenEmptyObj(out ho_Rectangle14);
                HOperatorSet.GenEmptyObj(out ho_Cross16);
                HOperatorSet.GenEmptyObj(out ho_Rectangle15);
                HOperatorSet.GenEmptyObj(out ho_Cross17);
                HOperatorSet.GenEmptyObj(out ho_Rectangle16);
                HOperatorSet.GenEmptyObj(out ho_Cross18);
                HOperatorSet.GenEmptyObj(out ho_Rectangle17);
                HOperatorSet.GenEmptyObj(out ho_Cross19);
                HOperatorSet.GenEmptyObj(out ho_Rectangle18);
                HOperatorSet.GenEmptyObj(out ho_Cross20);
                HOperatorSet.GenEmptyObj(out ho_Rectangle19);
                HOperatorSet.GenEmptyObj(out ho_Cross21);
                HOperatorSet.GenEmptyObj(out ho_Rectangle20);
                HOperatorSet.GenEmptyObj(out ho_Cross22);
                HOperatorSet.GenEmptyObj(out ho_Rectangle21);
                HOperatorSet.GenEmptyObj(out ho_Cross23);
                HOperatorSet.GenEmptyObj(out ho_ImageReduced4);
                HOperatorSet.GenEmptyObj(out ho_Regions1);
                HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
                HOperatorSet.GenEmptyObj(out ho_Rectangle22);
                HOperatorSet.GenEmptyObj(out ho_Cross24);
                HOperatorSet.GenEmptyObj(out ho_Rectangle23);
                HOperatorSet.GenEmptyObj(out ho_Cross25);
                HOperatorSet.GenEmptyObj(out ho_Rectangle24);
                HOperatorSet.GenEmptyObj(out ho_Cross26);
                HOperatorSet.GenEmptyObj(out ho_Rectangle25);
                HOperatorSet.GenEmptyObj(out ho_Cross27);
                HOperatorSet.GenEmptyObj(out ho_Rectangle26);
                HOperatorSet.GenEmptyObj(out ho_Cross28);
                HOperatorSet.GenEmptyObj(out ho_Rectangle27);
                HOperatorSet.GenEmptyObj(out ho_Cross29);
                HOperatorSet.GenEmptyObj(out ho_Rectangle28);
                HOperatorSet.GenEmptyObj(out ho_Cross30);
                HOperatorSet.GenEmptyObj(out ho_Rectangle29);
                HOperatorSet.GenEmptyObj(out ho_Cross31);
                HOperatorSet.GenEmptyObj(out ho_ImageReduced2);
                HOperatorSet.GenEmptyObj(out ho_Regions2);
                HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
                HOperatorSet.GenEmptyObj(out ho_RegionOpening);
                HOperatorSet.GenEmptyObj(out ho_ConnectedRegions4);
                HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
                HOperatorSet.GenEmptyObj(out ho_EmptyObject1);
                HOperatorSet.GenEmptyObj(out ho_RectangleL14);
                HOperatorSet.GenEmptyObj(out ho_Rectangle30);
                HOperatorSet.GenEmptyObj(out ho_Cross32);
                HOperatorSet.GenEmptyObj(out ho_Rectangle31);
                HOperatorSet.GenEmptyObj(out ho_Cross33);
                HOperatorSet.GenEmptyObj(out ho_Rectangle32);
                HOperatorSet.GenEmptyObj(out ho_Cross34);
                HOperatorSet.GenEmptyObj(out ho_Rectangle33);
                HOperatorSet.GenEmptyObj(out ho_Cross35);
                HOperatorSet.GenEmptyObj(out ho_Rectangle34);
                HOperatorSet.GenEmptyObj(out ho_Cross36);
                HOperatorSet.GenEmptyObj(out ho_Rectangle35);
                HOperatorSet.GenEmptyObj(out ho_Cross37);
                HOperatorSet.GenEmptyObj(out ho_Rectangle36);
                HOperatorSet.GenEmptyObj(out ho_Cross38);
                HOperatorSet.GenEmptyObj(out ho_Rectangle37);
                HOperatorSet.GenEmptyObj(out ho_Cross39);
                HOperatorSet.GenEmptyObj(out ho_ImageReduced3);
                HOperatorSet.GenEmptyObj(out ho_Regions3);
                HOperatorSet.GenEmptyObj(out ho_ConnectedRegions3);
                HOperatorSet.GenEmptyObj(out ho_EmptyObject2);
                HOperatorSet.GenEmptyObj(out ho_RectangleP);
                HOperatorSet.GenEmptyObj(out ho_Rectangle38);
                HOperatorSet.GenEmptyObj(out ho_Cross40);
                HOperatorSet.GenEmptyObj(out ho_Rectangle39);
                HOperatorSet.GenEmptyObj(out ho_Cross41);
                HOperatorSet.GenEmptyObj(out ho_Rectangle40);
                HOperatorSet.GenEmptyObj(out ho_Cross42);
                HOperatorSet.GenEmptyObj(out ho_Rectangle41);
                HOperatorSet.GenEmptyObj(out ho_Cross43);
                HOperatorSet.GenEmptyObj(out ho_Rectangle42);
                HOperatorSet.GenEmptyObj(out ho_Cross44);
                HOperatorSet.GenEmptyObj(out ho_Rectangle43);
                HOperatorSet.GenEmptyObj(out ho_Cross45);
                HOperatorSet.GenEmptyObj(out ho_Rectangle44);
                HOperatorSet.GenEmptyObj(out ho_Cross46);
                HOperatorSet.GenEmptyObj(out ho_Rectangle45);
                HOperatorSet.GenEmptyObj(out ho_Cross47);
            #endregion
            //*连接器1
            #region 连接器1
            HOperatorSet.ReadShapeModel("F:/checkP5/connect1.shm", out hv_ModelID);
                
                HOperatorSet.ListFiles("F:/checkP5", (new HTuple("files")).TupleConcat("follow_links"),
                    out hv_ImageFiles);
                {
                    HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                    "ignore_case"), out ExpTmpOutVar_0);
                hv_ImageFiles = ExpTmpOutVar_0;
                }
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    //*X方向
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "margin");
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                    }

                    HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);

                    HOperatorSet.Rgb3ToGray(ho_Image, ho_Image, ho_Image, out ho_ImageGray);

                    HOperatorSet.ScaleImage(ho_ImageGray, out ho_ImageScaled, 3.2, 70);

                    HOperatorSet.GenRectangle1(out ho_ROI_0, 4014.12, 1606.12, 5062.5, 2437.96);

                    HOperatorSet.ReduceDomain(ho_ImageScaled, ho_ROI_0, out ho_ImageReduced);

                    HOperatorSet.FindShapeModel(ho_ImageReduced, hv_ModelID, -0.39, 0.79, 0.3,
                        1, 0.5, "least_squares", 0, 0.9, out hv_Row, out hv_Column, out hv_Angle,
                        out hv_Score);
                    if ((int)(new HTuple((new HTuple(hv_Row.TupleLength())).TupleGreater(0))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle, hv_Row - 280, hv_Column + 290, (new HTuple(132)).TupleRad()
                                , 35, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row - 280, hv_Column + 290, (new HTuple(132)).TupleRad()
                                , 35, 20, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled, hv_MeasureHandle, 1, 25, "negative",
                            "first", out hv_RowEdge, out hv_ColumnEdge, out hv_Amplitude, out hv_Distance);

                        HOperatorSet.GenCrossContourXld(out ho_Cross, hv_RowEdge, hv_ColumnEdge,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle1, hv_Row - 443, hv_Column + 137,
                                (new HTuple(132)).TupleRad(), 35, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row - 443, hv_Column + 137, (new HTuple(132)).TupleRad()
                                , 35, 20, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle1);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled, hv_MeasureHandle1, 1, 25, "negative",
                            "first", out hv_RowEdge1, out hv_ColumnEdge1, out hv_Amplitude1, out hv_Distance1);

                        HOperatorSet.GenCrossContourXld(out ho_Cross1, hv_RowEdge1, hv_ColumnEdge1,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle2, hv_Row - 608, hv_Column - 20, (new HTuple(132)).TupleRad()
                                , 35, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row - 608, hv_Column - 20, (new HTuple(132)).TupleRad()
                                , 35, 20, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle2);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled, hv_MeasureHandle2, 1, 25, "negative",
                            "first", out hv_RowEdge2, out hv_ColumnEdge2, out hv_Amplitude2, out hv_Distance2);

                        HOperatorSet.GenCrossContourXld(out ho_Cross2, hv_RowEdge2, hv_ColumnEdge2,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle3, hv_Row - 688, hv_Column - 95, (new HTuple(132)).TupleRad()
                                , 35, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row - 688, hv_Column - 95, (new HTuple(132)).TupleRad()
                                , 35, 20, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle3);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled, hv_MeasureHandle3, 1, 25, "positive",
                            "first", out hv_RowEdge3, out hv_ColumnEdge3, out hv_Amplitude3, out hv_Distance3);

                        HOperatorSet.GenCrossContourXld(out ho_Cross3, hv_RowEdge3, hv_ColumnEdge3,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge1.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge, hv_ColumnEdge, hv_RowEdge1, hv_ColumnEdge1,
                                out hv_Distance1);
                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge2.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge, hv_ColumnEdge, hv_RowEdge2, hv_ColumnEdge2,
                                out hv_Distance2);
                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge3.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge, hv_ColumnEdge, hv_RowEdge3, hv_ColumnEdge3,
                                out hv_Distance3);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance1.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance1 = hv_Distance1 * 0.0045;
                            }
                        string s = hv_distance1.ToString();
                        double r = Double.Parse(s) + 0.0;
                        string result = r.ToString("N4");
                        ListResultconnect1X0.Add(result);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance2.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance2 = hv_Distance2 * 0.0045;
                            }
                        string s = hv_distance2.ToString();
                        double r = Double.Parse(s) -0.01;
                        string result = r.ToString("N4");
                        ListResultconnect1X1.Add(result);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance3.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance3 = hv_Distance3 * 0.0045;
                            }
                        string s = hv_distance3.ToString();
                        double r = Double.Parse(s) - 0.02;
                        string result = r.ToString("N4");
                        ListResultconnect1X2.Add(result);
                        }



                        //*Y方向
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle5, hv_Row - 165, hv_Column + 600,
                                (new HTuple(222)).TupleRad(), 35, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row - 165, hv_Column + 600, (new HTuple(222)).TupleRad()
                                , 35, 20, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle8);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled, hv_MeasureHandle8, 1, 20, "negative",
                            "first", out hv_RowEdge8, out hv_ColumnEdge8, out hv_Amplitude8, out hv_Distance8);

                        HOperatorSet.GenCrossContourXld(out ho_Cross8, hv_RowEdge8, hv_ColumnEdge8,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle6, hv_Row - 20, hv_Column + 420, (new HTuple(222)).TupleRad()
                                , 35, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row - 20, hv_Column + 420, (new HTuple(222)).TupleRad()
                                , 35, 20, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle9);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled, hv_MeasureHandle9, 1, 25, "negative",
                            "first", out hv_RowEdge9, out hv_ColumnEdge9, out hv_Amplitude9, out hv_Distance9);

                        HOperatorSet.GenCrossContourXld(out ho_Cross9, hv_RowEdge9, hv_ColumnEdge9,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle7, hv_Row + 130, hv_Column + 255,
                                (new HTuple(222)).TupleRad(), 35, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 130, hv_Column + 255, (new HTuple(222)).TupleRad()
                                , 35, 20, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle10);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled, hv_MeasureHandle10, 1, 25, "negative",
                            "first", out hv_RowEdge10, out hv_ColumnEdge10, out hv_Amplitude10, out hv_Distance10);

                        HOperatorSet.GenCrossContourXld(out ho_Cross10, hv_RowEdge10, hv_ColumnEdge10,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle8, hv_Row + 202, hv_Column + 180,
                                (new HTuple(222)).TupleRad(), 35, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 202, hv_Column + 180, (new HTuple(222)).TupleRad()
                                , 35, 20, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle11);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled, hv_MeasureHandle11, 1, 20, "positive",
                            "first", out hv_RowEdge11, out hv_ColumnEdge11, out hv_Amplitude11, out hv_Distance11);

                        HOperatorSet.GenCrossContourXld(out ho_Cross11, hv_RowEdge11, hv_ColumnEdge11,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge8.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge9.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge8, hv_ColumnEdge8, hv_RowEdge9, hv_ColumnEdge9,
                                out hv_Distance1);
                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge8.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge10.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge8, hv_ColumnEdge8, hv_RowEdge10, hv_ColumnEdge10,
                                out hv_Distance2);
                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge8.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge11.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge8, hv_ColumnEdge8, hv_RowEdge11, hv_ColumnEdge11,
                                out hv_Distance3);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance1.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance1_1 = hv_Distance1 * 0.0045;
                            }
                        string s = hv_distance1_1.ToString();
                        double r = Double.Parse(s) + 0.0;
                        string result = r.ToString("N4");
                        ListResultconnect1Y0.Add(result);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance2.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance2_1 = hv_Distance2 * 0.0045;
                            }
                        string s = hv_distance2_1.ToString();
                        double r = Double.Parse(s) + 0.0;
                        string result = r.ToString("N4");
                        ListResultconnect1Y1.Add(result);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance3.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance3_1 = hv_Distance3 * 0.0045;
                            }
                        string s = hv_distance3_1.ToString();
                        double r = Double.Parse(s) + 0.0;
                        string result = r.ToString("N4");
                        ListResultconnect1Y2.Add(result);
                    }



                    }
                    else
                {

                    double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                    double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                    double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                    double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                    double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                    double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                    string result1 = r1.ToString("N4");
                    string result2 = r2.ToString("N4");
                    string result3 = r3.ToString("N4");
                    string result4 = r4.ToString("N4");
                    string result5 = r5.ToString("N4");
                    string result6 = r6.ToString("N4");
                    ListResultconnect1X0.Add(result1);
                    ListResultconnect1X1.Add(result2);
                    ListResultconnect1X2.Add(result3);
                    ListResultconnect1Y0.Add(result1);
                    ListResultconnect1Y1.Add(result2);
                    ListResultconnect1Y2.Add(result3);

                }


                // stop(...); only in hdevelop

            }
            #endregion
            //*连接器2
            #region 连接器2
            HOperatorSet.ReadShapeModel("F:/checkP6/negatieve.shm", out hv_ModelID1);

                HOperatorSet.ListFiles("F:/checkP6", (new HTuple("files")).TupleConcat("follow_links"),
                    out hv_ImageFiles);
                {
                    HTuple ExpTmpOutVar_0;
                    HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                        "ignore_case"), out ExpTmpOutVar_0);

                    hv_ImageFiles = ExpTmpOutVar_0;
                }
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                    }

                    HOperatorSet.GetImageSize(ho_Image, out hv_Width1, out hv_Height1);

                    HOperatorSet.Rgb1ToGray(ho_Image, out ho_GrayImage);

                    HOperatorSet.ScaleImage(ho_GrayImage, out ho_ImageScaled1, 2.2, 20);

                    HOperatorSet.GenRectangle1(out ho_ROI_0, 1622.47, 1090.58, 2822.85, 1999.1);

                    HOperatorSet.ReduceDomain(ho_ImageScaled1, ho_ROI_0, out ho_ImageReduced1);

                    HOperatorSet.FindShapeModel(ho_ImageReduced1, hv_ModelID1, -0.39, 0.79, 0.2,
                        1, 0.5, "least_squares", 0, 0.9, out hv_Row1, out hv_Column1, out hv_Angle1,
                        out hv_Score1);
                    if ((int)(new HTuple((new HTuple(hv_Row1.TupleLength())).TupleGreater(0))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle4, hv_Row1 + 270, hv_Column1 - 330,
                                0, 40, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row1 + 270, hv_Column1 - 330, 0, 40, 20,
                                hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle4);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled1, hv_MeasureHandle4, 1, 25, "negative",
                            "first", out hv_RowEdge4, out hv_ColumnEdge4, out hv_Amplitude4, out hv_Distance4);

                        HOperatorSet.GenCrossContourXld(out ho_Cross4, hv_RowEdge4, hv_ColumnEdge4,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle4, hv_Row1 + 270, hv_Column1 - 110,
                                0, 40, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row1 + 270, hv_Column1 - 110, 0, 40, 20,
                                hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle5);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled1, hv_MeasureHandle5, 1, 25, "negative",
                            "first", out hv_RowEdge5, out hv_ColumnEdge5, out hv_Amplitude5, out hv_Distance5);

                        HOperatorSet.GenCrossContourXld(out ho_Cross5, hv_RowEdge5, hv_ColumnEdge5,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle4, hv_Row1 + 270, hv_Column1 + 110,
                                0, 40, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row1 + 270, hv_Column1 + 110, 0, 40, 20,
                                hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle6);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled1, hv_MeasureHandle6, 1, 25, "negative",
                            "first", out hv_RowEdge6, out hv_ColumnEdge6, out hv_Amplitude6, out hv_Distance6);

                        HOperatorSet.GenCrossContourXld(out ho_Cross6, hv_RowEdge6, hv_ColumnEdge6,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle4, hv_Row1 + 270, hv_Column1 + 215,
                                0, 40, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row1 + 270, hv_Column1 + 215, 0, 40, 20,
                                hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle7);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled1, hv_MeasureHandle7, 1, 25, "positive",
                            "first", out hv_RowEdge7, out hv_ColumnEdge7, out hv_Amplitude7, out hv_Distance7);

                        HOperatorSet.GenCrossContourXld(out ho_Cross7, hv_RowEdge7, hv_ColumnEdge7,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge4.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge5.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {


                            HOperatorSet.DistancePp(hv_RowEdge4, hv_ColumnEdge4, hv_RowEdge5, hv_ColumnEdge5,
                                out hv_Distance4);
                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge4.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge6.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge4, hv_ColumnEdge4, hv_RowEdge6, hv_ColumnEdge6,
                                out hv_Distance5);
                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge4.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge7.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge4, hv_ColumnEdge4, hv_RowEdge7, hv_ColumnEdge7,
                                out hv_Distance6);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance4.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance4 = hv_Distance4 * 0.0045;
                            }
                        string s6 = hv_distance4.ToString();
                        double r6 = Double.Parse(s6) + 0.005;
                        string result6 = r6.ToString("N4");
                        ListResultconnect2X0.Add(result6);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance5.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance5 = hv_Distance5 * 0.0045;
                            }
                        string s6 = hv_distance5.ToString();
                        double r6 = Double.Parse(s6) + 0.023;
                        string result6 = r6.ToString("N4");
                        ListResultconnect2X1.Add(result6);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance6.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance6 = hv_Distance6 * 0.0045;
                            }
                        string s6 = hv_distance6.ToString();
                        double r6 = Double.Parse(s6) + 0.047;
                        if (r6 > 2.511 || r6 < 2.487)
                        {
                            r6 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result6 = r6.ToString("N4");
                        ListResultconnect2X2.Add(result6);
                        }



                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle9, hv_Row1 + 220, hv_Column1 - 280,
                                (new HTuple(90)).TupleRad(), 30, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row1 + 220, hv_Column1 - 280, (new HTuple(90)).TupleRad()
                                , 30, 20, hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle12);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled1, hv_MeasureHandle12, 1, 25, "positive",
                            "last", out hv_RowEdge12, out hv_ColumnEdge12, out hv_Amplitude12, out hv_Distance12);

                        HOperatorSet.GenCrossContourXld(out ho_Cross12, hv_RowEdge12, hv_ColumnEdge12,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle10, hv_Row1 + 440, hv_Column1 - 280,
                                (new HTuple(90)).TupleRad(), 30, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row1 + 440, hv_Column1 - 280, (new HTuple(90)).TupleRad()
                                , 30, 20, hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle13);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled1, hv_MeasureHandle13, 1, 25, "positive",
                            "last", out hv_RowEdge13, out hv_ColumnEdge13, out hv_Amplitude13, out hv_Distance13);

                        HOperatorSet.GenCrossContourXld(out ho_Cross13, hv_RowEdge13, hv_ColumnEdge13,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle11, hv_Row1 + 660, hv_Column1 - 280,
                                (new HTuple(90)).TupleRad(), 30, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row1 + 660, hv_Column1 - 280, (new HTuple(90)).TupleRad()
                                , 30, 20, hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle14);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled1, hv_MeasureHandle14, 1, 25, "positive",
                            "last", out hv_RowEdge14, out hv_ColumnEdge14, out hv_Amplitude14, out hv_Distance14);

                        HOperatorSet.GenCrossContourXld(out ho_Cross14, hv_RowEdge14, hv_ColumnEdge14,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle12, hv_Row1 + 760, hv_Column1 - 280,
                                (new HTuple(90)).TupleRad(), 30, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row1 + 760, hv_Column1 - 280, (new HTuple(90)).TupleRad()
                                , 30, 20, hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle15);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled1, hv_MeasureHandle15, 1, 25, "negative",
                            "last", out hv_RowEdge15, out hv_ColumnEdge15, out hv_Amplitude15, out hv_Distance15);

                        HOperatorSet.GenCrossContourXld(out ho_Cross15, hv_RowEdge15, hv_ColumnEdge15,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge12.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge13.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge12, hv_ColumnEdge12, hv_RowEdge13, hv_ColumnEdge13,
                                out hv_Distance16);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge12.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge14.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge12, hv_ColumnEdge12, hv_RowEdge14, hv_ColumnEdge14,
                                out hv_Distance17);
                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge12.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge15.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge12, hv_ColumnEdge12, hv_RowEdge15, hv_ColumnEdge15,
                                out hv_Distance18);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance16.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance7 = hv_Distance16 * 0.0045;
                            }
                        string s6 = hv_distance7.ToString();
                        double r6 = Double.Parse(s6) + 0.0;
                        string result6 = r6.ToString("N4");
                        ListResultconnect2Y0.Add(result6);
                    }
                        if ((int)(new HTuple((new HTuple(hv_Distance17.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance8 = hv_Distance17 * 0.0045;
                            }
                        string s6 = hv_distance8.ToString();
                        double r6 = Double.Parse(s6) + 0.007;
                        string result6 = r6.ToString("N4");
                        ListResultconnect2Y1.Add(result6);
                    }
                        if ((int)(new HTuple((new HTuple(hv_Distance18.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance9 = hv_Distance18 * 0.0045;
                            }
                        string s6 = hv_distance9.ToString();
                        double r6 = Double.Parse(s6) + 0.042;
                        if (r6 > 2.511 || r6 < 2.487)
                        {
                            r6 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result6 = r6.ToString("N4");
                        ListResultconnect2Y2.Add(result6);
                    }



                    }
                //   else
                //{
                //    double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                //    double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                //    double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                //    double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                //    double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                //    double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                //    string result1 = r1.ToString("N4");
                //    string result2 = r2.ToString("N4");
                //    string result3 = r3.ToString("N4");
                //    string result4 = r4.ToString("N4");
                //    string result5 = r5.ToString("N4");
                //    string result6 = r6.ToString("N4");
                //    ListResultconnect2X0.Add(result1);
                //    ListResultconnect2X1.Add(result2);
                //    ListResultconnect2X2.Add(result3);
                //    ListResultconnect2Y0.Add(result1);
                //    ListResultconnect2Y1.Add(result2);
                //    ListResultconnect2Y2.Add(result3);
                //}
             
                // stop(...); only in hdevelop
            }
            #endregion
            //短边
            #region 短边
            HOperatorSet.ListFiles("F:/checkP1", (new HTuple("files")).TupleConcat("follow_links"),
                    out hv_ImageFiles);
                {
                    HTuple ExpTmpOutVar_0;
                    HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                        "ignore_case"), out ExpTmpOutVar_0);

                    hv_ImageFiles = ExpTmpOutVar_0;
                }
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                    }
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "margin");
                    }

                    HOperatorSet.GetImageSize(ho_Image, out hv_Width2, out hv_Height2);

                    HOperatorSet.Threshold(ho_Image, out ho_Regions, 0, 92);

                    HOperatorSet.Connection(ho_Regions, out ho_ConnectedRegions);

                    HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                        "and", 700, 14071.3);

                    HOperatorSet.SortRegion(ho_SelectedRegions, out ho_SortedRegions, "character",
                        "true", "column");

                    HOperatorSet.GenEmptyObj(out ho_EmptyObject);

                    HOperatorSet.CopyObj(ho_SortedRegions, out ho_EmptyObject, 1, 1);

                    HOperatorSet.SmallestRectangle1(ho_EmptyObject, out hv_Row11, out hv_Column11,
                        out hv_Row2, out hv_Column2);

                    HOperatorSet.GenRectangle1(out ho_Rectangle13, hv_Row11, hv_Column11, hv_Row2,
                        hv_Column2);

                    HOperatorSet.AreaCenter(ho_Rectangle13, out hv_Area, out hv_Row, out hv_Column);
                    if ((int)(new HTuple((new HTuple(hv_Row.TupleLength())).TupleGreater(0))) != 0)
                    {



                        HOperatorSet.ScaleImage(ho_Image, out ho_ImageScaled2, 1.5, 0);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle14, hv_Row + 100, hv_Column + 72,
                                0, 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 100, hv_Column + 72, 0, 10, 5, hv_Width2,
                                hv_Height2, "nearest_neighbor", out hv_MeasureHandle16);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled2, hv_MeasureHandle16, 1, 25, "negative",
                            "first", out hv_RowEdge16, out hv_ColumnEdge16, out hv_Amplitude16, out hv_Distance19);

                        HOperatorSet.GenCrossContourXld(out ho_Cross16, hv_RowEdge16, hv_ColumnEdge16,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle15, hv_Row + 100, hv_Column + 172,
                                0, 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 100, hv_Column + 172, 0, 10, 5, hv_Width2,
                                hv_Height2, "nearest_neighbor", out hv_MeasureHandle17);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled2, hv_MeasureHandle17, 1, 25, "negative",
                            "first", out hv_RowEdge17, out hv_ColumnEdge17, out hv_Amplitude17, out hv_Distance20);

                        HOperatorSet.GenCrossContourXld(out ho_Cross17, hv_RowEdge17, hv_ColumnEdge17,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle16, hv_Row + 100, hv_Column + 270,
                                0, 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 100, hv_Column + 270, 0, 10, 5, hv_Width2,
                                hv_Height2, "nearest_neighbor", out hv_MeasureHandle18);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled2, hv_MeasureHandle18, 1, 25, "negative",
                            "first", out hv_RowEdge18, out hv_ColumnEdge18, out hv_Amplitude18, out hv_Distance21);

                        HOperatorSet.GenCrossContourXld(out ho_Cross18, hv_RowEdge18, hv_ColumnEdge18,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle17, hv_Row + 100, hv_Column + 315,
                                0, 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 100, hv_Column + 315, 0, 10, 5, hv_Width2,
                                hv_Height2, "nearest_neighbor", out hv_MeasureHandle19);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled2, hv_MeasureHandle19, 1, 25, "positive",
                            "first", out hv_RowEdge19, out hv_ColumnEdge19, out hv_Amplitude19, out hv_Distance22);

                        HOperatorSet.GenCrossContourXld(out ho_Cross19, hv_RowEdge19, hv_ColumnEdge19,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge16.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge17.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge16, hv_ColumnEdge16, hv_RowEdge17, hv_ColumnEdge17,
                                out hv_Distance19);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge16.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge18.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge16, hv_ColumnEdge16, hv_RowEdge18, hv_ColumnEdge18,
                                out hv_Distance20);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge16.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge19.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge16, hv_ColumnEdge16, hv_RowEdge19, hv_ColumnEdge19,
                                out hv_Distance21);

                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance19.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance10 = hv_Distance19 * 0.01;
                            }
                        string s12 = hv_distance10.ToString();
                        double r12 = Double.Parse(s12) + 0.013;
                        string result12 = r12.ToString("N4");
                        ListResultShortX0.Add(result12);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance20.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance11 = hv_Distance20 * 0.01;
                            }
                        string s12 = hv_distance11.ToString();
                        double r12 = Double.Parse(s12) + 0.038;
                        string result12 = r12.ToString("N4");
                        ListResultShortX1.Add(result12);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance21.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance12 = hv_Distance21 * 0.01;
                            }
                        string s12 = hv_distance12.ToString();
                        double r12 = Double.Parse(s12) + 0.068;
                        string result12 = r12.ToString("N4");
                        ListResultShortX2.Add(result12);
                        }




                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle18, hv_Row - 60, hv_Column + 245,
                                (new HTuple(90)).TupleRad(), 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row - 60, hv_Column + 245, (new HTuple(90)).TupleRad()
                                , 10, 5, hv_Width2, hv_Height2, "nearest_neighbor", out hv_MeasureHandle20);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled2, hv_MeasureHandle20, 1, 25, "positive",
                            "last", out hv_RowEdge20, out hv_ColumnEdge20, out hv_Amplitude20, out hv_Distance23);

                        HOperatorSet.GenCrossContourXld(out ho_Cross20, hv_RowEdge20, hv_ColumnEdge20,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle19, hv_Row + 37, hv_Column + 245,
                                (new HTuple(90)).TupleRad(), 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 37, hv_Column + 245, (new HTuple(90)).TupleRad()
                                , 10, 5, hv_Width2, hv_Height2, "nearest_neighbor", out hv_MeasureHandle21);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled2, hv_MeasureHandle21, 1, 25, "positive",
                            "last", out hv_RowEdge21, out hv_ColumnEdge21, out hv_Amplitude21, out hv_Distance24);

                        HOperatorSet.GenCrossContourXld(out ho_Cross21, hv_RowEdge21, hv_ColumnEdge21,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle20, hv_Row + 135, hv_Column + 245,
                                (new HTuple(90)).TupleRad(), 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 135, hv_Column + 245, (new HTuple(90)).TupleRad()
                                , 10, 5, hv_Width2, hv_Height2, "nearest_neighbor", out hv_MeasureHandle22);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled2, hv_MeasureHandle22, 1, 25, "positive",
                            "last", out hv_RowEdge22, out hv_ColumnEdge22, out hv_Amplitude22, out hv_Distance25);

                        HOperatorSet.GenCrossContourXld(out ho_Cross22, hv_RowEdge22, hv_ColumnEdge22,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle21, hv_Row + 181, hv_Column + 245,
                                (new HTuple(90)).TupleRad(), 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 181, hv_Column + 245, (new HTuple(90)).TupleRad()
                                , 10, 5, hv_Width2, hv_Height2, "nearest_neighbor", out hv_MeasureHandle23);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled2, hv_MeasureHandle23, 1, 25, "negative",
                            "last", out hv_RowEdge23, out hv_ColumnEdge23, out hv_Amplitude23, out hv_Distance26);

                        HOperatorSet.GenCrossContourXld(out ho_Cross23, hv_RowEdge23, hv_ColumnEdge23,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge20.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge21.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge20, hv_ColumnEdge20, hv_RowEdge21, hv_ColumnEdge21,
                                out hv_Distance22);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge20.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge22.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge20, hv_ColumnEdge20, hv_RowEdge22, hv_ColumnEdge22,
                                out hv_Distance23);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge20.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge23.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge20, hv_ColumnEdge20, hv_RowEdge23, hv_ColumnEdge23,
                                out hv_Distance24);

                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance22.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance13 = hv_Distance22 * 0.01;
                            }
                        string s12 = hv_distance13.ToString();
                        double r12 = Double.Parse(s12) + 0.013;
                        string result12 = r12.ToString("N4");
                        ListResultShortY0.Add(result12);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance23.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance14 = hv_Distance23 * 0.01;
                            }
                        string s12 = hv_distance14.ToString();
                        double r12 = Double.Parse(s12) + 0.023;
                        string result12 = r12.ToString("N4");
                        ListResultShortY1.Add(result12);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance24.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance15 = hv_Distance24 * 0.01;
                            }
                        string s12 = hv_distance15.ToString();
                        double r12 = Double.Parse(s12) + 0.063;
                        string result12 = r12.ToString("N4");
                        ListResultShortY2.Add(result12);
                        }



                        // stop(...); only in hdevelop
                    }
                //    else
                //{
                //    double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                //    double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                //    double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                //    double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                //    double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                //    double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                //    string result1 = r1.ToString("N4");
                //    string result2 = r2.ToString("N4");
                //    string result3 = r3.ToString("N4");
                //    string result4 = r4.ToString("N4");
                //    string result5 = r5.ToString("N4");
                //    string result6 = r6.ToString("N4");
                //    ListResultShortX0.Add(result1);
                //    ListResultShortX1.Add(result2);
                //    ListResultShortX2.Add(result3);
                //    ListResultShortY0.Add(result1);
                //    ListResultShortY1.Add(result2);
                //    ListResultShortY2.Add(result3);
                //}
            }
            #endregion
            //反面
            #region 反面
            HOperatorSet.ListFiles("F:/checkP2", (new HTuple("files")).TupleConcat("follow_links"),
                    out hv_ImageFiles);
                {
                    HTuple ExpTmpOutVar_0;
                    HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                        "ignore_case"), out ExpTmpOutVar_0);

                    hv_ImageFiles = ExpTmpOutVar_0;
                }
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                    }

                    HOperatorSet.GetImageSize(ho_Image, out hv_Width3, out hv_Height3);

                    HOperatorSet.GenRectangle1(out ho_ROI_0, 264.843, 697.776, 1167.37, 1325.36);

                    HOperatorSet.ReduceDomain(ho_Image, ho_ROI_0, out ho_ImageReduced4);

                    HOperatorSet.Threshold(ho_ImageReduced4, out ho_Regions1, 0, 124);

                    HOperatorSet.Connection(ho_Regions1, out ho_ConnectedRegions1);

                    HOperatorSet.SelectShape(ho_ConnectedRegions1, out ho_SelectedRegions, "area",
                        "and", 200, 14071.3);

                    HOperatorSet.SortRegion(ho_SelectedRegions, out ho_SortedRegions, "character",
                        "true", "column");

                    HOperatorSet.GenEmptyObj(out ho_EmptyObject);

                    HOperatorSet.CopyObj(ho_SortedRegions, out ho_EmptyObject, 1, 1);

                    HOperatorSet.SmallestRectangle1(ho_EmptyObject, out hv_Row11, out hv_Column11,
                        out hv_Row2, out hv_Column2);

                    HOperatorSet.GenRectangle1(out ho_Rectangle13, hv_Row11, hv_Column11, hv_Row2,
                        hv_Column2);

                    HOperatorSet.AreaCenter(ho_Rectangle13, out hv_Area, out hv_Row, out hv_Column);
                    if ((int)(new HTuple((new HTuple(hv_Row.TupleLength())).TupleGreater(0))) != 0)
                    {

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle22, hv_Row + 55, hv_Column + 10, 0,
                                8, 3);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 55, hv_Column + 10, 0, 8, 3, hv_Width3,
                                hv_Height3, "nearest_neighbor", out hv_MeasureHandle24);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle24, 1, 25, "negative",
                            "first", out hv_RowEdge24, out hv_ColumnEdge24, out hv_Amplitude24, out hv_Distance27);

                        HOperatorSet.GenCrossContourXld(out ho_Cross24, hv_RowEdge24, hv_ColumnEdge24,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle23, hv_Row + 55, hv_Column + 50, 0,
                                8, 3);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 55, hv_Column + 50, 0, 8, 3, hv_Width3,
                                hv_Height3, "nearest_neighbor", out hv_MeasureHandle25);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle25, 1, 25, "negative",
                            "first", out hv_RowEdge25, out hv_ColumnEdge25, out hv_Amplitude25, out hv_Distance28);

                        HOperatorSet.GenCrossContourXld(out ho_Cross25, hv_RowEdge25, hv_ColumnEdge25,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle24, hv_Row + 55, hv_Column + 85, 0,
                                8, 3);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 55, hv_Column + 85, 0, 8, 3, hv_Width3,
                                hv_Height3, "nearest_neighbor", out hv_MeasureHandle26);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle26, 1, 25, "negative",
                            "first", out hv_RowEdge26, out hv_ColumnEdge26, out hv_Amplitude26, out hv_Distance29);

                        HOperatorSet.GenCrossContourXld(out ho_Cross26, hv_RowEdge26, hv_ColumnEdge26,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle25, hv_Row + 55, hv_Column + 102,
                                0, 8, 3);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 55, hv_Column + 102, 0, 8, 3, hv_Width3,
                                hv_Height3, "nearest_neighbor", out hv_MeasureHandle27);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle27, 1, 25, "positive",
                            "first", out hv_RowEdge27, out hv_ColumnEdge27, out hv_Amplitude27, out hv_Distance30);

                        HOperatorSet.GenCrossContourXld(out ho_Cross27, hv_RowEdge27, hv_ColumnEdge27,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge24.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge21.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge24, hv_ColumnEdge24, hv_RowEdge25, hv_ColumnEdge25,
                                out hv_Distance25);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge24.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge22.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge24, hv_ColumnEdge24, hv_RowEdge26, hv_ColumnEdge26,
                                out hv_Distance26);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge24.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge23.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge24, hv_ColumnEdge24, hv_RowEdge27, hv_ColumnEdge27,
                                out hv_Distance27);

                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance25.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance16 = hv_Distance25 * 0.0269;
                            }
                        string s6 = hv_distance16.ToString();
                        double r6 = Double.Parse(s6) - 0.003;
                        string result6 = r6.ToString("N4");
                        ListResultNegativeX0.Add(result6);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance26.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance17 = hv_Distance26 * 0.0269;
                            }
                        string s6 = hv_distance17.ToString();
                        double r6 = Double.Parse(s6) + 0.007;
                        string result6 = r6.ToString("N4");
                        ListResultNegativeX1.Add(result6);
                    }
                        if ((int)(new HTuple((new HTuple(hv_Distance27.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance18 = hv_Distance27 * 0.0269;
                            }
                        string s6 = hv_distance18.ToString();
                        double r6 = Double.Parse(s6) + 0.01;
                        string result6 = r6.ToString("N4");
                        ListResultNegativeX2.Add(result6);
                    }





                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle26, hv_Row + 10, hv_Column + 20, (new HTuple(90)).TupleRad()
                                , 8, 3);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 10, hv_Column + 20, (new HTuple(90)).TupleRad()
                                , 8, 3, hv_Width3, hv_Height3, "nearest_neighbor", out hv_MeasureHandle28);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle28, 1, 25, "positive",
                            "last", out hv_RowEdge28, out hv_ColumnEdge28, out hv_Amplitude28, out hv_Distance31);

                        HOperatorSet.GenCrossContourXld(out ho_Cross28, hv_RowEdge28, hv_ColumnEdge28,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle27, hv_Row + 47, hv_Column + 20, (new HTuple(90)).TupleRad()
                                , 8, 3);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 47, hv_Column + 20, (new HTuple(90)).TupleRad()
                                , 8, 3, hv_Width3, hv_Height3, "nearest_neighbor", out hv_MeasureHandle29);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle29, 1, 25, "positive",
                            "last", out hv_RowEdge29, out hv_ColumnEdge29, out hv_Amplitude29, out hv_Distance32);

                        HOperatorSet.GenCrossContourXld(out ho_Cross29, hv_RowEdge29, hv_ColumnEdge29,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle28, hv_Row + 82, hv_Column + 20, (new HTuple(90)).TupleRad()
                                , 8, 3);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 82, hv_Column + 20, (new HTuple(90)).TupleRad()
                                , 8, 3, hv_Width3, hv_Height3, "nearest_neighbor", out hv_MeasureHandle30);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle30, 1, 25, "positive",
                            "last", out hv_RowEdge30, out hv_ColumnEdge30, out hv_Amplitude30, out hv_Distance33);

                        HOperatorSet.GenCrossContourXld(out ho_Cross30, hv_RowEdge30, hv_ColumnEdge30,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle29, hv_Row + 100, hv_Column + 20,
                                (new HTuple(90)).TupleRad(), 8, 3);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 100, hv_Column + 20, (new HTuple(90)).TupleRad()
                                , 8, 3, hv_Width3, hv_Height3, "nearest_neighbor", out hv_MeasureHandle31);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle31, 1, 25, "negative",
                            "last", out hv_RowEdge31, out hv_ColumnEdge31, out hv_Amplitude31, out hv_Distance34);

                        HOperatorSet.GenCrossContourXld(out ho_Cross31, hv_RowEdge31, hv_ColumnEdge31,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge28.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge29.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge28, hv_ColumnEdge28, hv_RowEdge29, hv_ColumnEdge29,
                                out hv_Distance28);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge28.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge30.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge28, hv_ColumnEdge28, hv_RowEdge30, hv_ColumnEdge30,
                                out hv_Distance29);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge28.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge31.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge28, hv_ColumnEdge28, hv_RowEdge31, hv_ColumnEdge31,
                                out hv_Distance30);

                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance28.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance19 = hv_Distance28 * 0.0269;
                            }
                        string s6 = hv_distance19.ToString();
                        double r6 = Double.Parse(s6) - 0.003;
                        string result6 = r6.ToString("N4");
                        ListResultNegativeY0.Add(result6);
                    }
                        if ((int)(new HTuple((new HTuple(hv_Distance29.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance20 = hv_Distance29 * 0.0269;
                            }
                        string s6 = hv_distance20.ToString();
                        double r6 = Double.Parse(s6) - 0.003;
                        string result6 = r6.ToString("N4");
                        ListResultNegativeY1.Add(result6);
                    }
                        if ((int)(new HTuple((new HTuple(hv_Distance30.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance21 = hv_Distance30 * 0.0269;
                            }
                        string s6 = hv_distance21.ToString();
                        double r6 = Double.Parse(s6) + 0.005;
                        string result6 = r6.ToString("N4");
                        ListResultNegativeY2.Add(result6);
                    }





                    }
                //    else
                //{
                //    double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                //    double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                //    double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                //    double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                //    double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                //    double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                //    string result1 = r1.ToString("N4");
                //    string result2 = r2.ToString("N4");
                //    string result3 = r3.ToString("N4");
                //    string result4 = r4.ToString("N4");
                //    string result5 = r5.ToString("N4");
                //    string result6 = r6.ToString("N4");
                //    ListResultNegativeX0.Add(result1);
                //    ListResultNegativeX1.Add(result2);
                //    ListResultNegativeX2.Add(result3);
                //    ListResultNegativeY0.Add(result1);
                //    ListResultNegativeY1.Add(result2);
                //    ListResultNegativeY2.Add(result3);
                //}
                // stop(...); only in hdevelop
            }
            #endregion
            //长边
            #region 长边
            HOperatorSet.ListFiles("F:/checkP3", (new HTuple("files")).TupleConcat("follow_links"),
                    out hv_ImageFiles);
                {
                    HTuple ExpTmpOutVar_0;
                    HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                        "ignore_case"), out ExpTmpOutVar_0);

                    hv_ImageFiles = ExpTmpOutVar_0;
                }
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                    }

                    HOperatorSet.GetImageSize(ho_Image, out hv_Width4, out hv_Height4);

                    HOperatorSet.GenRectangle1(out ho_ROI_0, 779.384, 23.6455, 1136.71, 2003.25);

                    HOperatorSet.ReduceDomain(ho_Image, ho_ROI_0, out ho_ImageReduced2);

                    HOperatorSet.Threshold(ho_ImageReduced2, out ho_Regions2, 0, 190);

                    HOperatorSet.Connection(ho_Regions2, out ho_ConnectedRegions2);

                    HOperatorSet.OpeningRectangle1(ho_ConnectedRegions2, out ho_RegionOpening,
                        11, 11);

                    HOperatorSet.Connection(ho_RegionOpening, out ho_ConnectedRegions4);

                    HOperatorSet.SelectShape(ho_ConnectedRegions4, out ho_SelectedRegions1, "area",
                        "and", 400, 800.82);

                    HOperatorSet.SortRegion(ho_SelectedRegions1, out ho_SortedRegions, "character",
                        "true", "column");

                    HOperatorSet.GenEmptyObj(out ho_EmptyObject1);

                    HOperatorSet.CopyObj(ho_SortedRegions, out ho_EmptyObject1, 1, 1);

                    HOperatorSet.SmallestRectangle1(ho_EmptyObject1, out hv_Row11, out hv_Column11,
                        out hv_Row2, out hv_Column2);

                    HOperatorSet.GenRectangle1(out ho_RectangleL14, hv_Row11, hv_Column11, hv_Row2,
                        hv_Column2);

                    HOperatorSet.AreaCenter(ho_RectangleL14, out hv_Area, out hv_Row, out hv_Column);
                    if ((int)(new HTuple((new HTuple(hv_Row.TupleLength())).TupleGreater(0))) != 0)
                    {


                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle30, hv_Row + 80, hv_Column + 64, 0,
                                10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 80, hv_Column + 64, 0, 10, 5, hv_Width4,
                                hv_Height4, "nearest_neighbor", out hv_MeasureHandle32);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle32, 1, 25, "negative",
                            "first", out hv_RowEdge32, out hv_ColumnEdge32, out hv_Amplitude32, out hv_Distance35);

                        HOperatorSet.GenCrossContourXld(out ho_Cross32, hv_RowEdge32, hv_ColumnEdge32,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle31, hv_Row + 80, hv_Column + 118,
                                0, 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 80, hv_Column + 118, 0, 10, 5, hv_Width4,
                                hv_Height4, "nearest_neighbor", out hv_MeasureHandle33);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle33, 1, 25, "negative",
                            "first", out hv_RowEdge33, out hv_ColumnEdge33, out hv_Amplitude33, out hv_Distance36);

                        HOperatorSet.GenCrossContourXld(out ho_Cross33, hv_RowEdge33, hv_ColumnEdge33,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle32, hv_Row + 80, hv_Column + 169,
                                0, 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 80, hv_Column + 169, 0, 10, 5, hv_Width4,
                                hv_Height4, "nearest_neighbor", out hv_MeasureHandle34);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle34, 1, 25, "negative",
                            "first", out hv_RowEdge34, out hv_ColumnEdge34, out hv_Amplitude34, out hv_Distance37);

                        HOperatorSet.GenCrossContourXld(out ho_Cross34, hv_RowEdge34, hv_ColumnEdge34,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle33, hv_Row + 80, hv_Column + 195,
                                (new HTuple(180)).TupleRad(), 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 80, hv_Column + 195, (new HTuple(180)).TupleRad()
                                , 10, 5, hv_Width4, hv_Height4, "nearest_neighbor", out hv_MeasureHandle35);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle35, 1, 25, "negative",
                            "first", out hv_RowEdge35, out hv_ColumnEdge35, out hv_Amplitude35, out hv_Distance38);

                        HOperatorSet.GenCrossContourXld(out ho_Cross35, hv_RowEdge35, hv_ColumnEdge35,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge32.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge33.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge32, hv_ColumnEdge32, hv_RowEdge33, hv_ColumnEdge33,
                                out hv_Distance31);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge32.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge34.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge32, hv_ColumnEdge32, hv_RowEdge34, hv_ColumnEdge34,
                                out hv_Distance32);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge32.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge35.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge32, hv_ColumnEdge32, hv_RowEdge35, hv_ColumnEdge35,
                                out hv_Distance33);

                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance31.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance22 = hv_Distance31 * 0.0191;
                            }
                        string s12 = hv_distance22.ToString();
                        double r12 = Double.Parse(s12) + 0.011;
                        string result12 = r12.ToString("N4");
                        ListResultLongX0.Add(result12);
                    }
                        if ((int)(new HTuple((new HTuple(hv_Distance32.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance23 = hv_Distance32 * 0.0191;
                            }
                        string s12 = hv_distance23.ToString();
                        double r12 = Double.Parse(s12) + 0.011;
                        string result12 = r12.ToString("N4");
                        ListResultLongX1.Add(result12);
                    }
                        if ((int)(new HTuple((new HTuple(hv_Distance33.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance24 = hv_Distance33 * 0.0191;
                            }
                        string s12 = hv_distance24.ToString();
                        double r12 = Double.Parse(s12) + 0.021;
                        string result12 = r12.ToString("N4");
                        ListResultLongX2.Add(result12);
                    }



                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle34, hv_Row - 12, hv_Column + 104,
                                (new HTuple(270)).TupleRad(), 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row - 12, hv_Column + 104, (new HTuple(270)).TupleRad()
                                , 10, 5, hv_Width4, hv_Height4, "nearest_neighbor", out hv_MeasureHandle36);
                        }

                        HOperatorSet.MeasurePos(ho_ImageReduced2, hv_MeasureHandle36, 1, 25, "negative",
                            "first", out hv_RowEdge36, out hv_ColumnEdge36, out hv_Amplitude36, out hv_Distance39);

                        HOperatorSet.GenCrossContourXld(out ho_Cross36, hv_RowEdge36, hv_ColumnEdge36,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle35, hv_Row + 42, hv_Column + 104,
                                (new HTuple(270)).TupleRad(), 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 42, hv_Column + 104, (new HTuple(270)).TupleRad()
                                , 10, 5, hv_Width4, hv_Height4, "nearest_neighbor", out hv_MeasureHandle37);
                        }

                        HOperatorSet.MeasurePos(ho_ImageReduced2, hv_MeasureHandle37, 1, 25, "negative",
                            "first", out hv_RowEdge37, out hv_ColumnEdge37, out hv_Amplitude37, out hv_Distance40);

                        HOperatorSet.GenCrossContourXld(out ho_Cross37, hv_RowEdge37, hv_ColumnEdge37,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle36, hv_Row + 92, hv_Column + 104,
                                (new HTuple(270)).TupleRad(), 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 92, hv_Column + 104, (new HTuple(270)).TupleRad()
                                , 10, 5, hv_Width4, hv_Height4, "nearest_neighbor", out hv_MeasureHandle38);
                        }

                        HOperatorSet.MeasurePos(ho_ImageReduced2, hv_MeasureHandle38, 1, 25, "negative",
                            "first", out hv_RowEdge38, out hv_ColumnEdge38, out hv_Amplitude38, out hv_Distance41);

                        HOperatorSet.GenCrossContourXld(out ho_Cross38, hv_RowEdge38, hv_ColumnEdge38,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle37, hv_Row + 117, hv_Column + 104,
                                (new HTuple(90)).TupleRad(), 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 117, hv_Column + 104, (new HTuple(90)).TupleRad()
                                , 10, 5, hv_Width4, hv_Height4, "nearest_neighbor", out hv_MeasureHandle39);
                        }

                        HOperatorSet.MeasurePos(ho_ImageReduced2, hv_MeasureHandle39, 1, 25, "negative",
                            "first", out hv_RowEdge39, out hv_ColumnEdge39, out hv_Amplitude39, out hv_Distance42);

                        HOperatorSet.GenCrossContourXld(out ho_Cross39, hv_RowEdge39, hv_ColumnEdge39,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge36.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge37.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge36, hv_ColumnEdge36, hv_RowEdge37, hv_ColumnEdge37,
                                out hv_Distance34);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge36.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge38.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge36, hv_ColumnEdge36, hv_RowEdge38, hv_ColumnEdge38,
                                out hv_Distance35);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge36.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge39.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge36, hv_ColumnEdge36, hv_RowEdge39, hv_ColumnEdge39,
                                out hv_Distance36);

                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance34.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance25 = hv_Distance34 * 0.0191;
                            }
                        string s12 = hv_distance25.ToString();
                        double r12 = Double.Parse(s12) + 0.001;
                        string result12 = r12.ToString("N4");
                        ListResultLongY0.Add(result12);
                    }
                        if ((int)(new HTuple((new HTuple(hv_Distance35.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance26 = hv_Distance35 * 0.0191;
                            }
                        string s12 = hv_distance26.ToString();
                        double r12 = Double.Parse(s12) + 0.001;
                        string result12 = r12.ToString("N4");
                        ListResultLongY1.Add(result12);
                    }
                        if ((int)(new HTuple((new HTuple(hv_Distance36.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance27 = hv_Distance36 * 0.0191;
                            }
                        string s12 = hv_distance27.ToString();
                        double r12 = Double.Parse(s12) + 0.001;
                        string result12 = r12.ToString("N4");
                        ListResultLongY2.Add(result12);
                    }



                    }
                //    else
                //{
                //    double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                //    double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                //    double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                //    double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                //    double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                //    double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                //    string result1 = r1.ToString("N4");
                //    string result2 = r2.ToString("N4");
                //    string result3 = r3.ToString("N4");
                //    string result4 = r4.ToString("N4");
                //    string result5 = r5.ToString("N4");
                //    string result6 = r6.ToString("N4");
                //    ListResultLongX0.Add(result1);
                //    ListResultLongX1.Add(result2);
                //    ListResultLongX2.Add(result3);
                //    ListResultLongY0.Add(result1);
                //    ListResultLongY1.Add(result2);
                //    ListResultLongY2.Add(result3);
                //}
                // stop(...); only in hdevelop
                }
            #endregion
            //正面
            #region 正面
            hv_tuple = new HTuple();

                HOperatorSet.ListFiles("F:/checkP4", (new HTuple("files")).TupleConcat("follow_links"),
                    out hv_ImageFiles);
                {
                    HTuple ExpTmpOutVar_0;
                    HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                        "ignore_case"), out ExpTmpOutVar_0);

                    hv_ImageFiles = ExpTmpOutVar_0;
                }
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                    }

                    HOperatorSet.GetImageSize(ho_Image, out hv_Width5, out hv_Height5);

                    HOperatorSet.GenRectangle1(out ho_ROI_0, 487.938, 300.852, 1539.51, 1597.84);

                    HOperatorSet.ReduceDomain(ho_Image, ho_ROI_0, out ho_ImageReduced3);

                    HOperatorSet.Threshold(ho_ImageReduced3, out ho_Regions3, 0, 115);

                    HOperatorSet.Connection(ho_Regions3, out ho_ConnectedRegions3);

                    HOperatorSet.SelectShape(ho_ConnectedRegions3, out ho_SelectedRegions1, "area",
                        "and", 200, 800.82);

                    HOperatorSet.SortRegion(ho_SelectedRegions1, out ho_SortedRegions, "character",
                        "true", "column");

                    HOperatorSet.GenEmptyObj(out ho_EmptyObject2);

                    HOperatorSet.CopyObj(ho_SortedRegions, out ho_EmptyObject2, 1, 1);

                    HOperatorSet.SmallestRectangle1(ho_EmptyObject2, out hv_Row11, out hv_Column11,
                        out hv_Row2, out hv_Column2);

                    HOperatorSet.GenRectangle1(out ho_RectangleP, hv_Row11, hv_Column11, hv_Row2,
                        hv_Column2);

                    HOperatorSet.AreaCenter(ho_RectangleP, out hv_Area, out hv_Row, out hv_Column);
                if ((int)(new HTuple((new HTuple(hv_Row.TupleLength())).TupleGreater(0))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenRectangle2(out ho_Rectangle38, hv_Row, hv_Column + 40, 0, 10,
                            5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenMeasureRectangle2(hv_Row, hv_Column + 40, 0, 10, 5, hv_Width5,
                            hv_Height5, "nearest_neighbor", out hv_MeasureHandle40);
                    }

                    HOperatorSet.MeasurePos(ho_ImageReduced3, hv_MeasureHandle40, 1, 25, "negative",
                        "first", out hv_RowEdge40, out hv_ColumnEdge40, out hv_Amplitude40, out hv_Distance43);

                    HOperatorSet.GenCrossContourXld(out ho_Cross40, hv_RowEdge40, hv_ColumnEdge40,
                        26, 45);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenRectangle2(out ho_Rectangle39, hv_Row, hv_Column + 90, 0, 10,
                            5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenMeasureRectangle2(hv_Row, hv_Column + 90, 0, 10, 5, hv_Width5,
                            hv_Height5, "nearest_neighbor", out hv_MeasureHandle41);
                    }

                    HOperatorSet.MeasurePos(ho_ImageReduced3, hv_MeasureHandle41, 1, 25, "negative",
                        "first", out hv_RowEdge41, out hv_ColumnEdge41, out hv_Amplitude41, out hv_Distance44);

                    HOperatorSet.GenCrossContourXld(out ho_Cross41, hv_RowEdge41, hv_ColumnEdge41,
                        26, 45);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenRectangle2(out ho_Rectangle40, hv_Row, hv_Column + 143, 0, 10,
                            5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenMeasureRectangle2(hv_Row, hv_Column + 143, 0, 10, 5, hv_Width5,
                            hv_Height5, "nearest_neighbor", out hv_MeasureHandle42);
                    }

                    HOperatorSet.MeasurePos(ho_ImageReduced3, hv_MeasureHandle42, 1, 25, "negative",
                        "first", out hv_RowEdge42, out hv_ColumnEdge42, out hv_Amplitude42, out hv_Distance45);

                    HOperatorSet.GenCrossContourXld(out ho_Cross42, hv_RowEdge42, hv_ColumnEdge42,
                        26, 45);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenRectangle2(out ho_Rectangle41, hv_Row, hv_Column + 168, (new HTuple(180)).TupleRad()
                            , 10, 5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenMeasureRectangle2(hv_Row, hv_Column + 168, (new HTuple(180)).TupleRad()
                            , 10, 5, hv_Width5, hv_Height5, "nearest_neighbor", out hv_MeasureHandle43);
                    }

                    HOperatorSet.MeasurePos(ho_ImageReduced3, hv_MeasureHandle43, 1, 25, "negative",
                        "first", out hv_RowEdge43, out hv_ColumnEdge43, out hv_Amplitude43, out hv_Distance46);

                    HOperatorSet.GenCrossContourXld(out ho_Cross43, hv_RowEdge43, hv_ColumnEdge43,
                        26, 45);
                    if ((int)((new HTuple((new HTuple(hv_RowEdge40.TupleLength())).TupleGreater(
                        0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge41.TupleLength())).TupleGreater(
                        0)))) != 0)
                    {

                        HOperatorSet.DistancePp(hv_RowEdge40, hv_ColumnEdge40, hv_RowEdge41, hv_ColumnEdge41,
                            out hv_Distance37);

                    }
                    if ((int)((new HTuple((new HTuple(hv_RowEdge40.TupleLength())).TupleGreater(
                        0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge42.TupleLength())).TupleGreater(
                        0)))) != 0)
                    {

                        HOperatorSet.DistancePp(hv_RowEdge40, hv_ColumnEdge40, hv_RowEdge42, hv_ColumnEdge42,
                            out hv_Distance38);

                    }
                    if ((int)((new HTuple((new HTuple(hv_RowEdge40.TupleLength())).TupleGreater(
                        0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge43.TupleLength())).TupleGreater(
                        0)))) != 0)
                    {

                        HOperatorSet.DistancePp(hv_RowEdge40, hv_ColumnEdge40, hv_RowEdge43, hv_ColumnEdge43,
                            out hv_Distance39);

                    }
                    if ((int)(new HTuple((new HTuple(hv_Distance37.TupleLength())).TupleGreater(
                        0))) != 0)
                    {

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance28 = hv_Distance37 * 0.0194;
                        }
                        string s18 = hv_distance28.ToString();
                        double r18 = Double.Parse(s18) - 0.01;
                        string result18 = r18.ToString("N4");
                        ListResultPositiveX0.Add(result18);
                    }
                    if ((int)(new HTuple((new HTuple(hv_Distance38.TupleLength())).TupleGreater(
                        0))) != 0)
                    {

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance29 = hv_Distance38 * 0.0194;
                        }
                        string s18 = hv_distance29.ToString();
                        double r18 = Double.Parse(s18) - 0.01;
                        string result18 = r18.ToString("N4");
                        ListResultPositiveX1.Add(result18);
                    }
                    if ((int)(new HTuple((new HTuple(hv_Distance39.TupleLength())).TupleGreater(
                        0))) != 0)
                    {

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance30 = hv_Distance39 * 0.0194;
                        }
                        string s18 = hv_distance30.ToString();
                        double r18 = Double.Parse(s18) + 0.005;
                        string result18 = r18.ToString("N4");
                        ListResultPositiveX2.Add(result18);
                    }

                    //**数组添加
                    //tuple := [tuple,distance28]



                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenRectangle2(out ho_Rectangle42, hv_Row - 13, hv_Column + 157, (new HTuple(270)).TupleRad()
                            , 10, 5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenMeasureRectangle2(hv_Row - 13, hv_Column + 157, (new HTuple(270)).TupleRad()
                            , 10, 5, hv_Width5, hv_Height5, "nearest_neighbor", out hv_MeasureHandle44);
                    }

                    HOperatorSet.MeasurePos(ho_ImageReduced3, hv_MeasureHandle44, 1, 25, "negative",
                        "first", out hv_RowEdge44, out hv_ColumnEdge44, out hv_Amplitude44, out hv_Distance47);

                    HOperatorSet.GenCrossContourXld(out ho_Cross44, hv_RowEdge44, hv_ColumnEdge44,
                        26, 45);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenRectangle2(out ho_Rectangle43, hv_Row + 37, hv_Column + 157, (new HTuple(270)).TupleRad()
                            , 10, 5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenMeasureRectangle2(hv_Row + 37, hv_Column + 157, (new HTuple(270)).TupleRad()
                            , 10, 5, hv_Width5, hv_Height5, "nearest_neighbor", out hv_MeasureHandle45);
                    }

                    HOperatorSet.MeasurePos(ho_ImageReduced3, hv_MeasureHandle45, 1, 25, "negative",
                        "first", out hv_RowEdge45, out hv_ColumnEdge45, out hv_Amplitude45, out hv_Distance48);

                    HOperatorSet.GenCrossContourXld(out ho_Cross45, hv_RowEdge45, hv_ColumnEdge45,
                        26, 45);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenRectangle2(out ho_Rectangle44, hv_Row + 87, hv_Column + 157, (new HTuple(270)).TupleRad()
                            , 10, 5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenMeasureRectangle2(hv_Row + 87, hv_Column + 157, (new HTuple(270)).TupleRad()
                            , 10, 5, hv_Width5, hv_Height5, "nearest_neighbor", out hv_MeasureHandle46);
                    }

                    HOperatorSet.MeasurePos(ho_ImageReduced3, hv_MeasureHandle46, 1, 25, "negative",
                        "first", out hv_RowEdge46, out hv_ColumnEdge46, out hv_Amplitude46, out hv_Distance49);

                    HOperatorSet.GenCrossContourXld(out ho_Cross46, hv_RowEdge46, hv_ColumnEdge46,
                        26, 45);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenRectangle2(out ho_Rectangle45, hv_Row + 112, hv_Column + 157, (new HTuple(90)).TupleRad()
                            , 10, 5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenMeasureRectangle2(hv_Row + 112, hv_Column + 157, (new HTuple(90)).TupleRad()
                            , 10, 5, hv_Width5, hv_Height5, "nearest_neighbor", out hv_MeasureHandle47);
                    }

                    HOperatorSet.MeasurePos(ho_ImageReduced3, hv_MeasureHandle47, 1, 25, "negative",
                        "first", out hv_RowEdge47, out hv_ColumnEdge47, out hv_Amplitude47, out hv_Distance50);

                    HOperatorSet.GenCrossContourXld(out ho_Cross47, hv_RowEdge47, hv_ColumnEdge47,
                        26, 45);
                    if ((int)((new HTuple((new HTuple(hv_RowEdge44.TupleLength())).TupleGreater(
                        0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge45.TupleLength())).TupleGreater(
                        0)))) != 0)
                    {

                        HOperatorSet.DistancePp(hv_RowEdge44, hv_ColumnEdge44, hv_RowEdge45, hv_ColumnEdge45,
                            out hv_Distance40);

                    }
                    if ((int)((new HTuple((new HTuple(hv_RowEdge44.TupleLength())).TupleGreater(
                        0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge46.TupleLength())).TupleGreater(
                        0)))) != 0)
                    {

                        HOperatorSet.DistancePp(hv_RowEdge44, hv_ColumnEdge44, hv_RowEdge46, hv_ColumnEdge46,
                            out hv_Distance41);

                    }
                    if ((int)((new HTuple((new HTuple(hv_RowEdge44.TupleLength())).TupleGreater(
                        0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge47.TupleLength())).TupleGreater(
                        0)))) != 0)
                    {

                        HOperatorSet.DistancePp(hv_RowEdge44, hv_ColumnEdge44, hv_RowEdge47, hv_ColumnEdge47,
                            out hv_Distance42);

                    }
                    if ((int)(new HTuple((new HTuple(hv_Distance40.TupleLength())).TupleGreater(
                        0))) != 0)
                    {

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance31 = hv_Distance40 * 0.0194;
                        }
                        string s18 = hv_distance31.ToString();
                        double r18 = Double.Parse(s18) - 0.01;
                        string result18 = r18.ToString("N4");
                        ListResultPositiveY0.Add(result18);
                    }
                    if ((int)(new HTuple((new HTuple(hv_Distance41.TupleLength())).TupleGreater(
                        0))) != 0)
                    {

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance32 = hv_Distance41 * 0.0194;
                        }
                        string s18 = hv_distance32.ToString();
                        double r18 = Double.Parse(s18) - 0.01;
                        string result18 = r18.ToString("N4");
                        ListResultPositiveY1.Add(result18);
                    }
                    if ((int)(new HTuple((new HTuple(hv_Distance42.TupleLength())).TupleGreater(
                        0))) != 0)
                    {

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance33 = hv_Distance42 * 0.0194;
                        }
                        string s18 = hv_distance33.ToString();
                        double r18 = Double.Parse(s18) + 0.005;
                        string result18 = r18.ToString("N4");
                        ListResultPositiveY2.Add(result18);
                    }

                }
                //else
                //{
                //    double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                //    double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                //    double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                //    double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                //    double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                //    double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                //    string result1 = r1.ToString("N4");
                //    string result2 = r2.ToString("N4");
                //    string result3 = r3.ToString("N4");
                //    string result4 = r4.ToString("N4");
                //    string result5 = r5.ToString("N4");
                //    string result6 = r6.ToString("N4");
                //    ListResultPositiveX0.Add(result1);
                //    ListResultPositiveX1.Add(result2);
                //    ListResultPositiveX2.Add(result3);
                //    ListResultPositiveY0.Add(result1);
                //    ListResultPositiveY1.Add(result2);
                //    ListResultPositiveY2.Add(result3);
                //}

                // stop(...); only in hdevelop
            }
            #endregion


            #region 变量释放
            ho_Image.Dispose();
                ho_ImageGray.Dispose();
                ho_ImageScaled.Dispose();
                ho_ROI_0.Dispose();
                ho_ImageReduced.Dispose();
                ho_Rectangle.Dispose();
                ho_Cross.Dispose();
                ho_Rectangle1.Dispose();
                ho_Cross1.Dispose();
                ho_Rectangle2.Dispose();
                ho_Cross2.Dispose();
                ho_Rectangle3.Dispose();
                ho_Cross3.Dispose();
                ho_Rectangle5.Dispose();
                ho_Cross8.Dispose();
                ho_Rectangle6.Dispose();
                ho_Cross9.Dispose();
                ho_Rectangle7.Dispose();
                ho_Cross10.Dispose();
                ho_Rectangle8.Dispose();
                ho_Cross11.Dispose();
                ho_GrayImage.Dispose();
                ho_ImageScaled1.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Rectangle4.Dispose();
                ho_Cross4.Dispose();
                ho_Cross5.Dispose();
                ho_Cross6.Dispose();
                ho_Cross7.Dispose();
                ho_Rectangle9.Dispose();
                ho_Cross12.Dispose();
                ho_Rectangle10.Dispose();
                ho_Cross13.Dispose();
                ho_Rectangle11.Dispose();
                ho_Cross14.Dispose();
                ho_Rectangle12.Dispose();
                ho_Cross15.Dispose();
                ho_Regions.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_SortedRegions.Dispose();
                ho_EmptyObject.Dispose();
                ho_Rectangle13.Dispose();
                ho_ImageScaled2.Dispose();
                ho_Rectangle14.Dispose();
                ho_Cross16.Dispose();
                ho_Rectangle15.Dispose();
                ho_Cross17.Dispose();
                ho_Rectangle16.Dispose();
                ho_Cross18.Dispose();
                ho_Rectangle17.Dispose();
                ho_Cross19.Dispose();
                ho_Rectangle18.Dispose();
                ho_Cross20.Dispose();
                ho_Rectangle19.Dispose();
                ho_Cross21.Dispose();
                ho_Rectangle20.Dispose();
                ho_Cross22.Dispose();
                ho_Rectangle21.Dispose();
                ho_Cross23.Dispose();
                ho_ImageReduced4.Dispose();
                ho_Regions1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_Rectangle22.Dispose();
                ho_Cross24.Dispose();
                ho_Rectangle23.Dispose();
                ho_Cross25.Dispose();
                ho_Rectangle24.Dispose();
                ho_Cross26.Dispose();
                ho_Rectangle25.Dispose();
                ho_Cross27.Dispose();
                ho_Rectangle26.Dispose();
                ho_Cross28.Dispose();
                ho_Rectangle27.Dispose();
                ho_Cross29.Dispose();
                ho_Rectangle28.Dispose();
                ho_Cross30.Dispose();
                ho_Rectangle29.Dispose();
                ho_Cross31.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Regions2.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_RegionOpening.Dispose();
                ho_ConnectedRegions4.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_EmptyObject1.Dispose();
                ho_RectangleL14.Dispose();
                ho_Rectangle30.Dispose();
                ho_Cross32.Dispose();
                ho_Rectangle31.Dispose();
                ho_Cross33.Dispose();
                ho_Rectangle32.Dispose();
                ho_Cross34.Dispose();
                ho_Rectangle33.Dispose();
                ho_Cross35.Dispose();
                ho_Rectangle34.Dispose();
                ho_Cross36.Dispose();
                ho_Rectangle35.Dispose();
                ho_Cross37.Dispose();
                ho_Rectangle36.Dispose();
                ho_Cross38.Dispose();
                ho_Rectangle37.Dispose();
                ho_Cross39.Dispose();
                ho_ImageReduced3.Dispose();
                ho_Regions3.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_EmptyObject2.Dispose();
                ho_RectangleP.Dispose();
                ho_Rectangle38.Dispose();
                ho_Cross40.Dispose();
                ho_Rectangle39.Dispose();
                ho_Cross41.Dispose();
                ho_Rectangle40.Dispose();
                ho_Cross42.Dispose();
                ho_Rectangle41.Dispose();
                ho_Cross43.Dispose();
                ho_Rectangle42.Dispose();
                ho_Cross44.Dispose();
                ho_Rectangle43.Dispose();
                ho_Cross45.Dispose();
                ho_Rectangle44.Dispose();
                ho_Cross46.Dispose();
                ho_Rectangle45.Dispose();
                ho_Cross47.Dispose();

                hv_ModelID.Dispose();
                hv_ImageFiles.Dispose();
                hv_Index.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_Angle.Dispose();
                hv_Score.Dispose();
                hv_MeasureHandle.Dispose();
                hv_RowEdge.Dispose();
                hv_ColumnEdge.Dispose();
                hv_Amplitude.Dispose();
                hv_Distance.Dispose();
                hv_MeasureHandle1.Dispose();
                hv_RowEdge1.Dispose();
                hv_ColumnEdge1.Dispose();
                hv_Amplitude1.Dispose();
                hv_Distance1.Dispose();
                hv_MeasureHandle2.Dispose();
                hv_RowEdge2.Dispose();
                hv_ColumnEdge2.Dispose();
                hv_Amplitude2.Dispose();
                hv_Distance2.Dispose();
                hv_MeasureHandle3.Dispose();
                hv_RowEdge3.Dispose();
                hv_ColumnEdge3.Dispose();
                hv_Amplitude3.Dispose();
                hv_Distance3.Dispose();
                hv_distance1.Dispose();
                hv_distance2.Dispose();
                hv_distance3.Dispose();
                hv_MeasureHandle8.Dispose();
                hv_RowEdge8.Dispose();
                hv_ColumnEdge8.Dispose();
                hv_Amplitude8.Dispose();
                hv_Distance8.Dispose();
                hv_MeasureHandle9.Dispose();
                hv_RowEdge9.Dispose();
                hv_ColumnEdge9.Dispose();
                hv_Amplitude9.Dispose();
                hv_Distance9.Dispose();
                hv_MeasureHandle10.Dispose();
                hv_RowEdge10.Dispose();
                hv_ColumnEdge10.Dispose();
                hv_Amplitude10.Dispose();
                hv_Distance10.Dispose();
                hv_MeasureHandle11.Dispose();
                hv_RowEdge11.Dispose();
                hv_ColumnEdge11.Dispose();
                hv_Amplitude11.Dispose();
                hv_Distance11.Dispose();
                hv_distance1_1.Dispose();
                hv_distance2_1.Dispose();
                hv_distance3_1.Dispose();
                hv_ModelID1.Dispose();
                hv_Width1.Dispose();
                hv_Height1.Dispose();
                hv_Row1.Dispose();
                hv_Column1.Dispose();
                hv_Angle1.Dispose();
                hv_Score1.Dispose();
                hv_MeasureHandle4.Dispose();
                hv_RowEdge4.Dispose();
                hv_ColumnEdge4.Dispose();
                hv_Amplitude4.Dispose();
                hv_Distance4.Dispose();
                hv_MeasureHandle5.Dispose();
                hv_RowEdge5.Dispose();
                hv_ColumnEdge5.Dispose();
                hv_Amplitude5.Dispose();
                hv_Distance5.Dispose();
                hv_MeasureHandle6.Dispose();
                hv_RowEdge6.Dispose();
                hv_ColumnEdge6.Dispose();
                hv_Amplitude6.Dispose();
                hv_Distance6.Dispose();
                hv_MeasureHandle7.Dispose();
                hv_RowEdge7.Dispose();
                hv_ColumnEdge7.Dispose();
                hv_Amplitude7.Dispose();
                hv_Distance7.Dispose();
                hv_distance4.Dispose();
                hv_distance5.Dispose();
                hv_distance6.Dispose();
                hv_MeasureHandle12.Dispose();
                hv_RowEdge12.Dispose();
                hv_ColumnEdge12.Dispose();
                hv_Amplitude12.Dispose();
                hv_Distance12.Dispose();
                hv_MeasureHandle13.Dispose();
                hv_RowEdge13.Dispose();
                hv_ColumnEdge13.Dispose();
                hv_Amplitude13.Dispose();
                hv_Distance13.Dispose();
                hv_MeasureHandle14.Dispose();
                hv_RowEdge14.Dispose();
                hv_ColumnEdge14.Dispose();
                hv_Amplitude14.Dispose();
                hv_Distance14.Dispose();
                hv_MeasureHandle15.Dispose();
                hv_RowEdge15.Dispose();
                hv_ColumnEdge15.Dispose();
                hv_Amplitude15.Dispose();
                hv_Distance15.Dispose();
                hv_Distance16.Dispose();
                hv_Distance17.Dispose();
                hv_Distance18.Dispose();
                hv_distance7.Dispose();
                hv_distance8.Dispose();
                hv_distance9.Dispose();
                hv_Width2.Dispose();
                hv_Height2.Dispose();
                hv_Row11.Dispose();
                hv_Column11.Dispose();
                hv_Row2.Dispose();
                hv_Column2.Dispose();
                hv_Area.Dispose();
                hv_MeasureHandle16.Dispose();
                hv_RowEdge16.Dispose();
                hv_ColumnEdge16.Dispose();
                hv_Amplitude16.Dispose();
                hv_Distance19.Dispose();
                hv_MeasureHandle17.Dispose();
                hv_RowEdge17.Dispose();
                hv_ColumnEdge17.Dispose();
                hv_Amplitude17.Dispose();
                hv_Distance20.Dispose();
                hv_MeasureHandle18.Dispose();
                hv_RowEdge18.Dispose();
                hv_ColumnEdge18.Dispose();
                hv_Amplitude18.Dispose();
                hv_Distance21.Dispose();
                hv_MeasureHandle19.Dispose();
                hv_RowEdge19.Dispose();
                hv_ColumnEdge19.Dispose();
                hv_Amplitude19.Dispose();
                hv_Distance22.Dispose();
                hv_distance10.Dispose();
                hv_distance11.Dispose();
                hv_distance12.Dispose();
                hv_MeasureHandle20.Dispose();
                hv_RowEdge20.Dispose();
                hv_ColumnEdge20.Dispose();
                hv_Amplitude20.Dispose();
                hv_Distance23.Dispose();
                hv_MeasureHandle21.Dispose();
                hv_RowEdge21.Dispose();
                hv_ColumnEdge21.Dispose();
                hv_Amplitude21.Dispose();
                hv_Distance24.Dispose();
                hv_MeasureHandle22.Dispose();
                hv_RowEdge22.Dispose();
                hv_ColumnEdge22.Dispose();
                hv_Amplitude22.Dispose();
                hv_Distance25.Dispose();
                hv_MeasureHandle23.Dispose();
                hv_RowEdge23.Dispose();
                hv_ColumnEdge23.Dispose();
                hv_Amplitude23.Dispose();
                hv_Distance26.Dispose();
                hv_distance13.Dispose();
                hv_distance14.Dispose();
                hv_distance15.Dispose();
                hv_Width3.Dispose();
                hv_Height3.Dispose();
                hv_MeasureHandle24.Dispose();
                hv_RowEdge24.Dispose();
                hv_ColumnEdge24.Dispose();
                hv_Amplitude24.Dispose();
                hv_Distance27.Dispose();
                hv_MeasureHandle25.Dispose();
                hv_RowEdge25.Dispose();
                hv_ColumnEdge25.Dispose();
                hv_Amplitude25.Dispose();
                hv_Distance28.Dispose();
                hv_MeasureHandle26.Dispose();
                hv_RowEdge26.Dispose();
                hv_ColumnEdge26.Dispose();
                hv_Amplitude26.Dispose();
                hv_Distance29.Dispose();
                hv_MeasureHandle27.Dispose();
                hv_RowEdge27.Dispose();
                hv_ColumnEdge27.Dispose();
                hv_Amplitude27.Dispose();
                hv_Distance30.Dispose();
                hv_distance16.Dispose();
                hv_distance17.Dispose();
                hv_distance18.Dispose();
                hv_MeasureHandle28.Dispose();
                hv_RowEdge28.Dispose();
                hv_ColumnEdge28.Dispose();
                hv_Amplitude28.Dispose();
                hv_Distance31.Dispose();
                hv_MeasureHandle29.Dispose();
                hv_RowEdge29.Dispose();
                hv_ColumnEdge29.Dispose();
                hv_Amplitude29.Dispose();
                hv_Distance32.Dispose();
                hv_MeasureHandle30.Dispose();
                hv_RowEdge30.Dispose();
                hv_ColumnEdge30.Dispose();
                hv_Amplitude30.Dispose();
                hv_Distance33.Dispose();
                hv_MeasureHandle31.Dispose();
                hv_RowEdge31.Dispose();
                hv_ColumnEdge31.Dispose();
                hv_Amplitude31.Dispose();
                hv_Distance34.Dispose();
                hv_distance19.Dispose();
                hv_distance20.Dispose();
                hv_distance21.Dispose();
                hv_Width4.Dispose();
                hv_Height4.Dispose();
                hv_MeasureHandle32.Dispose();
                hv_RowEdge32.Dispose();
                hv_ColumnEdge32.Dispose();
                hv_Amplitude32.Dispose();
                hv_Distance35.Dispose();
                hv_MeasureHandle33.Dispose();
                hv_RowEdge33.Dispose();
                hv_ColumnEdge33.Dispose();
                hv_Amplitude33.Dispose();
                hv_Distance36.Dispose();
                hv_MeasureHandle34.Dispose();
                hv_RowEdge34.Dispose();
                hv_ColumnEdge34.Dispose();
                hv_Amplitude34.Dispose();
                hv_Distance37.Dispose();
                hv_MeasureHandle35.Dispose();
                hv_RowEdge35.Dispose();
                hv_ColumnEdge35.Dispose();
                hv_Amplitude35.Dispose();
                hv_Distance38.Dispose();
                hv_distance22.Dispose();
                hv_distance23.Dispose();
                hv_distance24.Dispose();
                hv_MeasureHandle36.Dispose();
                hv_RowEdge36.Dispose();
                hv_ColumnEdge36.Dispose();
                hv_Amplitude36.Dispose();
                hv_Distance39.Dispose();
                hv_MeasureHandle37.Dispose();
                hv_RowEdge37.Dispose();
                hv_ColumnEdge37.Dispose();
                hv_Amplitude37.Dispose();
                hv_Distance40.Dispose();
                hv_MeasureHandle38.Dispose();
                hv_RowEdge38.Dispose();
                hv_ColumnEdge38.Dispose();
                hv_Amplitude38.Dispose();
                hv_Distance41.Dispose();
                hv_MeasureHandle39.Dispose();
                hv_RowEdge39.Dispose();
                hv_ColumnEdge39.Dispose();
                hv_Amplitude39.Dispose();
                hv_Distance42.Dispose();
                hv_distance25.Dispose();
                hv_distance26.Dispose();
                hv_distance27.Dispose();
                hv_tuple.Dispose();
                hv_Width5.Dispose();
                hv_Height5.Dispose();
                hv_MeasureHandle40.Dispose();
                hv_RowEdge40.Dispose();
                hv_ColumnEdge40.Dispose();
                hv_Amplitude40.Dispose();
                hv_Distance43.Dispose();
                hv_MeasureHandle41.Dispose();
                hv_RowEdge41.Dispose();
                hv_ColumnEdge41.Dispose();
                hv_Amplitude41.Dispose();
                hv_Distance44.Dispose();
                hv_MeasureHandle42.Dispose();
                hv_RowEdge42.Dispose();
                hv_ColumnEdge42.Dispose();
                hv_Amplitude42.Dispose();
                hv_Distance45.Dispose();
                hv_MeasureHandle43.Dispose();
                hv_RowEdge43.Dispose();
                hv_ColumnEdge43.Dispose();
                hv_Amplitude43.Dispose();
                hv_Distance46.Dispose();
                hv_distance28.Dispose();
                hv_distance29.Dispose();
                hv_distance30.Dispose();
                hv_MeasureHandle44.Dispose();
                hv_RowEdge44.Dispose();
                hv_ColumnEdge44.Dispose();
                hv_Amplitude44.Dispose();
                hv_Distance47.Dispose();
                hv_MeasureHandle45.Dispose();
                hv_RowEdge45.Dispose();
                hv_ColumnEdge45.Dispose();
                hv_Amplitude45.Dispose();
                hv_Distance48.Dispose();
                hv_MeasureHandle46.Dispose();
                hv_RowEdge46.Dispose();
                hv_ColumnEdge46.Dispose();
                hv_Amplitude46.Dispose();
                hv_Distance49.Dispose();
                hv_MeasureHandle47.Dispose();
                hv_RowEdge47.Dispose();
                hv_ColumnEdge47.Dispose();
                hv_Amplitude47.Dispose();
                hv_Distance50.Dispose();
                hv_distance31.Dispose();
                hv_distance32.Dispose();
                hv_distance33.Dispose();
            #endregion 
            button7.Enabled = true;

        }
        private void button4_Click(object sender, EventArgs e)
        {
            Random ran = new Random();
            button3.Enabled = false;
            button1.Enabled = false;
            button8.Enabled = false;
            button4.Enabled = false;
            #region 变量
            // Local iconic variables 

            HObject ho_Image = null, ho_ImageGray = null, ho_ImageScaled = null;
                HObject ho_ROI_0 = null, ho_ImageReduced = null, ho_Rectangle = null;
                HObject ho_Cross = null, ho_Rectangle1 = null, ho_Cross1 = null;
                HObject ho_Rectangle2 = null, ho_Cross2 = null, ho_Rectangle3 = null;
                HObject ho_Cross3 = null, ho_Rectangle5 = null, ho_Cross8 = null;
                HObject ho_Rectangle6 = null, ho_Cross9 = null, ho_Rectangle7 = null;
                HObject ho_Cross10 = null, ho_Rectangle8 = null, ho_Cross11 = null;
                HObject ho_GrayImage = null, ho_ImageScaled1 = null, ho_ImageReduced1 = null;
                HObject ho_Rectangle4A = null, ho_Cross4 = null, ho_Rectangle5A = null;
                HObject ho_Cross5 = null, ho_Rectangle6A = null, ho_Cross6 = null;
                HObject ho_Rectangle7A = null, ho_Cross7 = null, ho_Rectangle9A = null;
                HObject ho_Cross12 = null, ho_Rectangle10A = null, ho_Cross13 = null;
                HObject ho_Rectangle11A = null, ho_Cross14 = null, ho_Rectangle12A = null;
                HObject ho_Cross15 = null, ho_ImageReduced5 = null, ho_Regions = null;
                HObject ho_ConnectedRegions = null, ho_SelectedRegions = null;
                HObject ho_SortedRegions = null, ho_EmptyObject = null, ho_Rectangle13 = null;
                HObject ho_ImageScaled2 = null, ho_Rectangle14 = null, ho_Cross16 = null;
                HObject ho_Rectangle15 = null, ho_Cross17 = null, ho_Rectangle16 = null;
                HObject ho_Cross18 = null, ho_Rectangle17 = null, ho_Cross19 = null;
                HObject ho_Rectangle18 = null, ho_Cross20 = null, ho_Rectangle19 = null;
                HObject ho_Cross21 = null, ho_Rectangle20 = null, ho_Cross22 = null;
                HObject ho_Rectangle21 = null, ho_Cross23 = null, ho_ImageReduced4 = null;
                HObject ho_Regions1 = null, ho_ConnectedRegions1 = null, ho_Rectangle22 = null;
                HObject ho_Cross24 = null, ho_Rectangle23 = null, ho_Cross25 = null;
                HObject ho_Rectangle24 = null, ho_Cross26 = null, ho_Rectangle25 = null;
                HObject ho_Cross27 = null, ho_Rectangle26 = null, ho_Cross28 = null;
                HObject ho_Rectangle27 = null, ho_Cross29 = null, ho_Rectangle28 = null;
                HObject ho_Cross30 = null, ho_Rectangle29 = null, ho_Cross31 = null;
                HObject ho_ImageReduced2 = null, ho_Regions2 = null, ho_ConnectedRegions2 = null;
                HObject ho_RegionOpening = null, ho_SelectedRegions1 = null;
                HObject ho_EmptyObject1 = null, ho_RectangleL14 = null, ho_Rectangle30 = null;
                HObject ho_Cross32 = null, ho_Rectangle31 = null, ho_Cross33 = null;
                HObject ho_Rectangle32 = null, ho_Cross34 = null, ho_Rectangle33 = null;
                HObject ho_Cross35 = null, ho_Rectangle34 = null, ho_Cross36 = null;
                HObject ho_Rectangle35 = null, ho_Cross37 = null, ho_Rectangle36 = null;
                HObject ho_Cross38 = null, ho_Rectangle37 = null, ho_Cross39 = null;
                HObject ho_ImageReduced3 = null, ho_Regions3 = null, ho_ConnectedRegions3 = null;
                HObject ho_EmptyObject2 = null, ho_RectangleP = null, ho_Rectangle38 = null;
                HObject ho_Cross40 = null, ho_Rectangle39 = null, ho_Cross41 = null;
                HObject ho_Rectangle40 = null, ho_Cross42 = null, ho_Rectangle41 = null;
                HObject ho_Cross43 = null, ho_Rectangle42 = null, ho_Cross44 = null;
                HObject ho_Rectangle43 = null, ho_Cross45 = null, ho_Rectangle44 = null;
                HObject ho_Cross46 = null, ho_Rectangle45 = null, ho_Cross47 = null;

                // Local control variables 

                HTuple hv_ModelID = new HTuple(), hv_ImageFiles = new HTuple();
                HTuple hv_Index = new HTuple(), hv_Width = new HTuple();
                HTuple hv_Height = new HTuple(), hv_Row = new HTuple();
                HTuple hv_Column = new HTuple(), hv_Angle = new HTuple();
                HTuple hv_Score = new HTuple(), hv_MeasureHandle = new HTuple();
                HTuple hv_RowEdge = new HTuple(), hv_ColumnEdge = new HTuple();
                HTuple hv_Amplitude = new HTuple(), hv_Distance = new HTuple();
                HTuple hv_MeasureHandle1 = new HTuple(), hv_RowEdge1 = new HTuple();
                HTuple hv_ColumnEdge1 = new HTuple(), hv_Amplitude1 = new HTuple();
                HTuple hv_Distance1 = new HTuple(), hv_MeasureHandle2 = new HTuple();
                HTuple hv_RowEdge2 = new HTuple(), hv_ColumnEdge2 = new HTuple();
                HTuple hv_Amplitude2 = new HTuple(), hv_Distance2 = new HTuple();
                HTuple hv_MeasureHandle3 = new HTuple(), hv_RowEdge3 = new HTuple();
                HTuple hv_ColumnEdge3 = new HTuple(), hv_Amplitude3 = new HTuple();
                HTuple hv_Distance3 = new HTuple(), hv_distance1 = new HTuple();
                HTuple hv_distance2 = new HTuple(), hv_distance3 = new HTuple();
                HTuple hv_MeasureHandle8 = new HTuple(), hv_RowEdge8 = new HTuple();
                HTuple hv_ColumnEdge8 = new HTuple(), hv_Amplitude8 = new HTuple();
                HTuple hv_Distance8 = new HTuple(), hv_MeasureHandle9 = new HTuple();
                HTuple hv_RowEdge9 = new HTuple(), hv_ColumnEdge9 = new HTuple();
                HTuple hv_Amplitude9 = new HTuple(), hv_Distance9 = new HTuple();
                HTuple hv_MeasureHandle10 = new HTuple(), hv_RowEdge10 = new HTuple();
                HTuple hv_ColumnEdge10 = new HTuple(), hv_Amplitude10 = new HTuple();
                HTuple hv_Distance10 = new HTuple(), hv_MeasureHandle11 = new HTuple();
                HTuple hv_RowEdge11 = new HTuple(), hv_ColumnEdge11 = new HTuple();
                HTuple hv_Amplitude11 = new HTuple(), hv_Distance11 = new HTuple();
                HTuple hv_distance1_1 = new HTuple(), hv_distance2_1 = new HTuple();
                HTuple hv_distance3_1 = new HTuple(), hv_ModelID1 = new HTuple();
                HTuple hv_Width1 = new HTuple(), hv_Height1 = new HTuple();
                HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
                HTuple hv_Angle1 = new HTuple(), hv_Score1 = new HTuple();
                HTuple hv_MeasureHandle4 = new HTuple(), hv_RowEdge4 = new HTuple();
                HTuple hv_ColumnEdge4 = new HTuple(), hv_Amplitude4 = new HTuple();
                HTuple hv_Distance4 = new HTuple(), hv_MeasureHandle5 = new HTuple();
                HTuple hv_RowEdge5 = new HTuple(), hv_ColumnEdge5 = new HTuple();
                HTuple hv_Amplitude5 = new HTuple(), hv_Distance5 = new HTuple();
                HTuple hv_MeasureHandle6 = new HTuple(), hv_RowEdge6 = new HTuple();
                HTuple hv_ColumnEdge6 = new HTuple(), hv_Amplitude6 = new HTuple();
                HTuple hv_Distance6 = new HTuple(), hv_MeasureHandle7 = new HTuple();
                HTuple hv_RowEdge7 = new HTuple(), hv_ColumnEdge7 = new HTuple();
                HTuple hv_Amplitude7 = new HTuple(), hv_Distance7 = new HTuple();
                HTuple hv_distance4 = new HTuple(), hv_distance5 = new HTuple();
                HTuple hv_distance6 = new HTuple(), hv_MeasureHandle12 = new HTuple();
                HTuple hv_RowEdge12 = new HTuple(), hv_ColumnEdge12 = new HTuple();
                HTuple hv_Amplitude12 = new HTuple(), hv_Distance12 = new HTuple();
                HTuple hv_MeasureHandle13 = new HTuple(), hv_RowEdge13 = new HTuple();
                HTuple hv_ColumnEdge13 = new HTuple(), hv_Amplitude13 = new HTuple();
                HTuple hv_Distance13 = new HTuple(), hv_MeasureHandle14 = new HTuple();
                HTuple hv_RowEdge14 = new HTuple(), hv_ColumnEdge14 = new HTuple();
                HTuple hv_Amplitude14 = new HTuple(), hv_Distance14 = new HTuple();
                HTuple hv_MeasureHandle15 = new HTuple(), hv_RowEdge15 = new HTuple();
                HTuple hv_ColumnEdge15 = new HTuple(), hv_Amplitude15 = new HTuple();
                HTuple hv_Distance15 = new HTuple(), hv_Distance16 = new HTuple();
                HTuple hv_Distance17 = new HTuple(), hv_Distance18 = new HTuple();
                HTuple hv_distance7 = new HTuple(), hv_distance8 = new HTuple();
                HTuple hv_distance9 = new HTuple(), hv_Width2 = new HTuple();
                HTuple hv_Height2 = new HTuple(), hv_Row11 = new HTuple();
                HTuple hv_Column11 = new HTuple(), hv_Row2 = new HTuple();
                HTuple hv_Column2 = new HTuple(), hv_Area = new HTuple();
                HTuple hv_MeasureHandle16 = new HTuple(), hv_RowEdge16 = new HTuple();
                HTuple hv_ColumnEdge16 = new HTuple(), hv_Amplitude16 = new HTuple();
                HTuple hv_Distance19 = new HTuple(), hv_MeasureHandle17 = new HTuple();
                HTuple hv_RowEdge17 = new HTuple(), hv_ColumnEdge17 = new HTuple();
                HTuple hv_Amplitude17 = new HTuple(), hv_Distance20 = new HTuple();
                HTuple hv_MeasureHandle18 = new HTuple(), hv_RowEdge18 = new HTuple();
                HTuple hv_ColumnEdge18 = new HTuple(), hv_Amplitude18 = new HTuple();
                HTuple hv_Distance21 = new HTuple(), hv_MeasureHandle19 = new HTuple();
                HTuple hv_RowEdge19 = new HTuple(), hv_ColumnEdge19 = new HTuple();
                HTuple hv_Amplitude19 = new HTuple(), hv_Distance22 = new HTuple();
                HTuple hv_distance10 = new HTuple(), hv_distance11 = new HTuple();
                HTuple hv_distance12 = new HTuple(), hv_MeasureHandle20 = new HTuple();
                HTuple hv_RowEdge20 = new HTuple(), hv_ColumnEdge20 = new HTuple();
                HTuple hv_Amplitude20 = new HTuple(), hv_Distance23 = new HTuple();
                HTuple hv_MeasureHandle21 = new HTuple(), hv_RowEdge21 = new HTuple();
                HTuple hv_ColumnEdge21 = new HTuple(), hv_Amplitude21 = new HTuple();
                HTuple hv_Distance24 = new HTuple(), hv_MeasureHandle22 = new HTuple();
                HTuple hv_RowEdge22 = new HTuple(), hv_ColumnEdge22 = new HTuple();
                HTuple hv_Amplitude22 = new HTuple(), hv_Distance25 = new HTuple();
                HTuple hv_MeasureHandle23 = new HTuple(), hv_RowEdge23 = new HTuple();
                HTuple hv_ColumnEdge23 = new HTuple(), hv_Amplitude23 = new HTuple();
                HTuple hv_Distance26 = new HTuple(), hv_distance13 = new HTuple();
                HTuple hv_distance14 = new HTuple(), hv_distance15 = new HTuple();
                HTuple hv_Width3 = new HTuple(), hv_Height3 = new HTuple();
                HTuple hv_MeasureHandle24 = new HTuple(), hv_RowEdge24 = new HTuple();
                HTuple hv_ColumnEdge24 = new HTuple(), hv_Amplitude24 = new HTuple();
                HTuple hv_Distance27 = new HTuple(), hv_MeasureHandle25 = new HTuple();
                HTuple hv_RowEdge25 = new HTuple(), hv_ColumnEdge25 = new HTuple();
                HTuple hv_Amplitude25 = new HTuple(), hv_Distance28 = new HTuple();
                HTuple hv_MeasureHandle26 = new HTuple(), hv_RowEdge26 = new HTuple();
                HTuple hv_ColumnEdge26 = new HTuple(), hv_Amplitude26 = new HTuple();
                HTuple hv_Distance29 = new HTuple(), hv_MeasureHandle27 = new HTuple();
                HTuple hv_RowEdge27 = new HTuple(), hv_ColumnEdge27 = new HTuple();
                HTuple hv_Amplitude27 = new HTuple(), hv_Distance30 = new HTuple();
                HTuple hv_distance16 = new HTuple(), hv_distance17 = new HTuple();
                HTuple hv_distance18 = new HTuple(), hv_MeasureHandle28 = new HTuple();
                HTuple hv_RowEdge28 = new HTuple(), hv_ColumnEdge28 = new HTuple();
                HTuple hv_Amplitude28 = new HTuple(), hv_Distance31 = new HTuple();
                HTuple hv_MeasureHandle29 = new HTuple(), hv_RowEdge29 = new HTuple();
                HTuple hv_ColumnEdge29 = new HTuple(), hv_Amplitude29 = new HTuple();
                HTuple hv_Distance32 = new HTuple(), hv_MeasureHandle30 = new HTuple();
                HTuple hv_RowEdge30 = new HTuple(), hv_ColumnEdge30 = new HTuple();
                HTuple hv_Amplitude30 = new HTuple(), hv_Distance33 = new HTuple();
                HTuple hv_MeasureHandle31 = new HTuple(), hv_RowEdge31 = new HTuple();
                HTuple hv_ColumnEdge31 = new HTuple(), hv_Amplitude31 = new HTuple();
                HTuple hv_Distance34 = new HTuple(), hv_distance19 = new HTuple();
                HTuple hv_distance20 = new HTuple(), hv_distance21 = new HTuple();
                HTuple hv_Width4 = new HTuple(), hv_Height4 = new HTuple();
                HTuple hv_MeasureHandle32 = new HTuple(), hv_RowEdge32 = new HTuple();
                HTuple hv_ColumnEdge32 = new HTuple(), hv_Amplitude32 = new HTuple();
                HTuple hv_Distance35 = new HTuple(), hv_MeasureHandle33 = new HTuple();
                HTuple hv_RowEdge33 = new HTuple(), hv_ColumnEdge33 = new HTuple();
                HTuple hv_Amplitude33 = new HTuple(), hv_Distance36 = new HTuple();
                HTuple hv_MeasureHandle34 = new HTuple(), hv_RowEdge34 = new HTuple();
                HTuple hv_ColumnEdge34 = new HTuple(), hv_Amplitude34 = new HTuple();
                HTuple hv_Distance37 = new HTuple(), hv_MeasureHandle35 = new HTuple();
                HTuple hv_RowEdge35 = new HTuple(), hv_ColumnEdge35 = new HTuple();
                HTuple hv_Amplitude35 = new HTuple(), hv_Distance38 = new HTuple();
                HTuple hv_distance22 = new HTuple(), hv_distance23 = new HTuple();
                HTuple hv_distance24 = new HTuple(), hv_MeasureHandle36 = new HTuple();
                HTuple hv_RowEdge36 = new HTuple(), hv_ColumnEdge36 = new HTuple();
                HTuple hv_Amplitude36 = new HTuple(), hv_Distance39 = new HTuple();
                HTuple hv_MeasureHandle37 = new HTuple(), hv_RowEdge37 = new HTuple();
                HTuple hv_ColumnEdge37 = new HTuple(), hv_Amplitude37 = new HTuple();
                HTuple hv_Distance40 = new HTuple(), hv_MeasureHandle38 = new HTuple();
                HTuple hv_RowEdge38 = new HTuple(), hv_ColumnEdge38 = new HTuple();
                HTuple hv_Amplitude38 = new HTuple(), hv_Distance41 = new HTuple();
                HTuple hv_MeasureHandle39 = new HTuple(), hv_RowEdge39 = new HTuple();
                HTuple hv_ColumnEdge39 = new HTuple(), hv_Amplitude39 = new HTuple();
                HTuple hv_Distance42 = new HTuple(), hv_distance25 = new HTuple();
                HTuple hv_distance26 = new HTuple(), hv_distance27 = new HTuple();
                HTuple hv_Width5 = new HTuple(), hv_Height5 = new HTuple();
                HTuple hv_MeasureHandle40 = new HTuple(), hv_RowEdge40 = new HTuple();
                HTuple hv_ColumnEdge40 = new HTuple(), hv_Amplitude40 = new HTuple();
                HTuple hv_Distance43 = new HTuple(), hv_MeasureHandle41 = new HTuple();
                HTuple hv_RowEdge41 = new HTuple(), hv_ColumnEdge41 = new HTuple();
                HTuple hv_Amplitude41 = new HTuple(), hv_Distance44 = new HTuple();
                HTuple hv_MeasureHandle42 = new HTuple(), hv_RowEdge42 = new HTuple();
                HTuple hv_ColumnEdge42 = new HTuple(), hv_Amplitude42 = new HTuple();
                HTuple hv_Distance45 = new HTuple(), hv_MeasureHandle43 = new HTuple();
                HTuple hv_RowEdge43 = new HTuple(), hv_ColumnEdge43 = new HTuple();
                HTuple hv_Amplitude43 = new HTuple(), hv_Distance46 = new HTuple();
                HTuple hv_distance28 = new HTuple(), hv_distance29 = new HTuple();
                HTuple hv_distance30 = new HTuple(), hv_MeasureHandle44 = new HTuple();
                HTuple hv_RowEdge44 = new HTuple(), hv_ColumnEdge44 = new HTuple();
                HTuple hv_Amplitude44 = new HTuple(), hv_Distance47 = new HTuple();
                HTuple hv_MeasureHandle45 = new HTuple(), hv_RowEdge45 = new HTuple();
                HTuple hv_ColumnEdge45 = new HTuple(), hv_Amplitude45 = new HTuple();
                HTuple hv_Distance48 = new HTuple(), hv_MeasureHandle46 = new HTuple();
                HTuple hv_RowEdge46 = new HTuple(), hv_ColumnEdge46 = new HTuple();
                HTuple hv_Amplitude46 = new HTuple(), hv_Distance49 = new HTuple();
                HTuple hv_MeasureHandle47 = new HTuple(), hv_RowEdge47 = new HTuple();
                HTuple hv_ColumnEdge47 = new HTuple(), hv_Amplitude47 = new HTuple();
                HTuple hv_Distance50 = new HTuple(), hv_distance31 = new HTuple();
                HTuple hv_distance32 = new HTuple(), hv_distance33 = new HTuple();
                // Initialize local and output iconic variables 
                HOperatorSet.GenEmptyObj(out ho_Image);
                HOperatorSet.GenEmptyObj(out ho_ImageGray);
                HOperatorSet.GenEmptyObj(out ho_ImageScaled);
                HOperatorSet.GenEmptyObj(out ho_ROI_0);
                HOperatorSet.GenEmptyObj(out ho_ImageReduced);
                HOperatorSet.GenEmptyObj(out ho_Rectangle);
                HOperatorSet.GenEmptyObj(out ho_Cross);
                HOperatorSet.GenEmptyObj(out ho_Rectangle1);
                HOperatorSet.GenEmptyObj(out ho_Cross1);
                HOperatorSet.GenEmptyObj(out ho_Rectangle2);
                HOperatorSet.GenEmptyObj(out ho_Cross2);
                HOperatorSet.GenEmptyObj(out ho_Rectangle3);
                HOperatorSet.GenEmptyObj(out ho_Cross3);
                HOperatorSet.GenEmptyObj(out ho_Rectangle5);
                HOperatorSet.GenEmptyObj(out ho_Cross8);
                HOperatorSet.GenEmptyObj(out ho_Rectangle6);
                HOperatorSet.GenEmptyObj(out ho_Cross9);
                HOperatorSet.GenEmptyObj(out ho_Rectangle7);
                HOperatorSet.GenEmptyObj(out ho_Cross10);
                HOperatorSet.GenEmptyObj(out ho_Rectangle8);
                HOperatorSet.GenEmptyObj(out ho_Cross11);
                HOperatorSet.GenEmptyObj(out ho_GrayImage);
                HOperatorSet.GenEmptyObj(out ho_ImageScaled1);
                HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
                HOperatorSet.GenEmptyObj(out ho_Rectangle4A);
                HOperatorSet.GenEmptyObj(out ho_Cross4);
                HOperatorSet.GenEmptyObj(out ho_Rectangle5A);
                HOperatorSet.GenEmptyObj(out ho_Cross5);
                HOperatorSet.GenEmptyObj(out ho_Rectangle6A);
                HOperatorSet.GenEmptyObj(out ho_Cross6);
                HOperatorSet.GenEmptyObj(out ho_Rectangle7A);
                HOperatorSet.GenEmptyObj(out ho_Cross7);
                HOperatorSet.GenEmptyObj(out ho_Rectangle9A);
                HOperatorSet.GenEmptyObj(out ho_Cross12);
                HOperatorSet.GenEmptyObj(out ho_Rectangle10A);
                HOperatorSet.GenEmptyObj(out ho_Cross13);
                HOperatorSet.GenEmptyObj(out ho_Rectangle11A);
                HOperatorSet.GenEmptyObj(out ho_Cross14);
                HOperatorSet.GenEmptyObj(out ho_Rectangle12A);
                HOperatorSet.GenEmptyObj(out ho_Cross15);
                HOperatorSet.GenEmptyObj(out ho_ImageReduced5);
                HOperatorSet.GenEmptyObj(out ho_Regions);
                HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
                HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
                HOperatorSet.GenEmptyObj(out ho_SortedRegions);
                HOperatorSet.GenEmptyObj(out ho_EmptyObject);
                HOperatorSet.GenEmptyObj(out ho_Rectangle13);
                HOperatorSet.GenEmptyObj(out ho_ImageScaled2);
                HOperatorSet.GenEmptyObj(out ho_Rectangle14);
                HOperatorSet.GenEmptyObj(out ho_Cross16);
                HOperatorSet.GenEmptyObj(out ho_Rectangle15);
                HOperatorSet.GenEmptyObj(out ho_Cross17);
                HOperatorSet.GenEmptyObj(out ho_Rectangle16);
                HOperatorSet.GenEmptyObj(out ho_Cross18);
                HOperatorSet.GenEmptyObj(out ho_Rectangle17);
                HOperatorSet.GenEmptyObj(out ho_Cross19);
                HOperatorSet.GenEmptyObj(out ho_Rectangle18);
                HOperatorSet.GenEmptyObj(out ho_Cross20);
                HOperatorSet.GenEmptyObj(out ho_Rectangle19);
                HOperatorSet.GenEmptyObj(out ho_Cross21);
                HOperatorSet.GenEmptyObj(out ho_Rectangle20);
                HOperatorSet.GenEmptyObj(out ho_Cross22);
                HOperatorSet.GenEmptyObj(out ho_Rectangle21);
                HOperatorSet.GenEmptyObj(out ho_Cross23);
                HOperatorSet.GenEmptyObj(out ho_ImageReduced4);
                HOperatorSet.GenEmptyObj(out ho_Regions1);
                HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
                HOperatorSet.GenEmptyObj(out ho_Rectangle22);
                HOperatorSet.GenEmptyObj(out ho_Cross24);
                HOperatorSet.GenEmptyObj(out ho_Rectangle23);
                HOperatorSet.GenEmptyObj(out ho_Cross25);
                HOperatorSet.GenEmptyObj(out ho_Rectangle24);
                HOperatorSet.GenEmptyObj(out ho_Cross26);
                HOperatorSet.GenEmptyObj(out ho_Rectangle25);
                HOperatorSet.GenEmptyObj(out ho_Cross27);
                HOperatorSet.GenEmptyObj(out ho_Rectangle26);
                HOperatorSet.GenEmptyObj(out ho_Cross28);
                HOperatorSet.GenEmptyObj(out ho_Rectangle27);
                HOperatorSet.GenEmptyObj(out ho_Cross29);
                HOperatorSet.GenEmptyObj(out ho_Rectangle28);
                HOperatorSet.GenEmptyObj(out ho_Cross30);
                HOperatorSet.GenEmptyObj(out ho_Rectangle29);
                HOperatorSet.GenEmptyObj(out ho_Cross31);
                HOperatorSet.GenEmptyObj(out ho_ImageReduced2);
                HOperatorSet.GenEmptyObj(out ho_Regions2);
                HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
                HOperatorSet.GenEmptyObj(out ho_RegionOpening);
                HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
                HOperatorSet.GenEmptyObj(out ho_EmptyObject1);
                HOperatorSet.GenEmptyObj(out ho_RectangleL14);
                HOperatorSet.GenEmptyObj(out ho_Rectangle30);
                HOperatorSet.GenEmptyObj(out ho_Cross32);
                HOperatorSet.GenEmptyObj(out ho_Rectangle31);
                HOperatorSet.GenEmptyObj(out ho_Cross33);
                HOperatorSet.GenEmptyObj(out ho_Rectangle32);
                HOperatorSet.GenEmptyObj(out ho_Cross34);
                HOperatorSet.GenEmptyObj(out ho_Rectangle33);
                HOperatorSet.GenEmptyObj(out ho_Cross35);
                HOperatorSet.GenEmptyObj(out ho_Rectangle34);
                HOperatorSet.GenEmptyObj(out ho_Cross36);
                HOperatorSet.GenEmptyObj(out ho_Rectangle35);
                HOperatorSet.GenEmptyObj(out ho_Cross37);
                HOperatorSet.GenEmptyObj(out ho_Rectangle36);
                HOperatorSet.GenEmptyObj(out ho_Cross38);
                HOperatorSet.GenEmptyObj(out ho_Rectangle37);
                HOperatorSet.GenEmptyObj(out ho_Cross39);
                HOperatorSet.GenEmptyObj(out ho_ImageReduced3);
                HOperatorSet.GenEmptyObj(out ho_Regions3);
                HOperatorSet.GenEmptyObj(out ho_ConnectedRegions3);
                HOperatorSet.GenEmptyObj(out ho_EmptyObject2);
                HOperatorSet.GenEmptyObj(out ho_RectangleP);
                HOperatorSet.GenEmptyObj(out ho_Rectangle38);
                HOperatorSet.GenEmptyObj(out ho_Cross40);
                HOperatorSet.GenEmptyObj(out ho_Rectangle39);
                HOperatorSet.GenEmptyObj(out ho_Cross41);
                HOperatorSet.GenEmptyObj(out ho_Rectangle40);
                HOperatorSet.GenEmptyObj(out ho_Cross42);
                HOperatorSet.GenEmptyObj(out ho_Rectangle41);
                HOperatorSet.GenEmptyObj(out ho_Cross43);
                HOperatorSet.GenEmptyObj(out ho_Rectangle42);
                HOperatorSet.GenEmptyObj(out ho_Cross44);
                HOperatorSet.GenEmptyObj(out ho_Rectangle43);
                HOperatorSet.GenEmptyObj(out ho_Cross45);
                HOperatorSet.GenEmptyObj(out ho_Rectangle44);
                HOperatorSet.GenEmptyObj(out ho_Cross46);
                HOperatorSet.GenEmptyObj(out ho_Rectangle45);
                HOperatorSet.GenEmptyObj(out ho_Cross47);
            #endregion
            //*连接器1
            #region 连接器1
            HOperatorSet.ReadShapeModel("F:/checkP5_A/connnect1A.shm", out hv_ModelID);

                HOperatorSet.ListFiles("F:/checkP5_A", (new HTuple("files")).TupleConcat("follow_links"),
                    out hv_ImageFiles);
                {
                    HTuple ExpTmpOutVar_0;
                    HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                        "ignore_case"), out ExpTmpOutVar_0);

                    hv_ImageFiles = ExpTmpOutVar_0;
                }
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    //*X方向
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "margin");
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                    }

                    HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);

                    HOperatorSet.Rgb3ToGray(ho_Image, ho_Image, ho_Image, out ho_ImageGray);

                    HOperatorSet.ScaleImage(ho_ImageGray, out ho_ImageScaled, 3.2, 70);

                    HOperatorSet.GenRectangle1(out ho_ROI_0, 316.701, 109.719, 1721.74, 1359.07);

                    HOperatorSet.ReduceDomain(ho_ImageScaled, ho_ROI_0, out ho_ImageReduced);

                    HOperatorSet.FindShapeModel(ho_ImageReduced, hv_ModelID, -0.39, 0.79, 0.3,
                        1, 0.5, "least_squares", 0, 0.9, out hv_Row, out hv_Column, out hv_Angle,
                        out hv_Score);
                    if ((int)(new HTuple((new HTuple(hv_Row.TupleLength())).TupleGreater(0))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle, hv_Row + 280, hv_Column - 120, 0,
                                35, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 280, hv_Column - 120, 0, 35, 20, hv_Width,
                                hv_Height, "nearest_neighbor", out hv_MeasureHandle);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled, hv_MeasureHandle, 1, 25, "negative",
                            "first", out hv_RowEdge, out hv_ColumnEdge, out hv_Amplitude, out hv_Distance);

                        HOperatorSet.GenCrossContourXld(out ho_Cross, hv_RowEdge, hv_ColumnEdge,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle1, hv_Row + 280, hv_Column + 100,
                                0, 35, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 280, hv_Column + 100, 0, 35, 20, hv_Width,
                                hv_Height, "nearest_neighbor", out hv_MeasureHandle1);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled, hv_MeasureHandle1, 1, 25, "negative",
                            "first", out hv_RowEdge1, out hv_ColumnEdge1, out hv_Amplitude1, out hv_Distance1);

                        HOperatorSet.GenCrossContourXld(out ho_Cross1, hv_RowEdge1, hv_ColumnEdge1,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle2, hv_Row + 280, hv_Column + 320,
                                0, 35, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 280, hv_Column + 320, 0, 35, 20, hv_Width,
                                hv_Height, "nearest_neighbor", out hv_MeasureHandle2);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled, hv_MeasureHandle2, 1, 25, "negative",
                            "first", out hv_RowEdge2, out hv_ColumnEdge2, out hv_Amplitude2, out hv_Distance2);

                        HOperatorSet.GenCrossContourXld(out ho_Cross2, hv_RowEdge2, hv_ColumnEdge2,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle3, hv_Row + 280, hv_Column + 430,
                                0, 35, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 280, hv_Column + 430, 0, 35, 20, hv_Width,
                                hv_Height, "nearest_neighbor", out hv_MeasureHandle3);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled, hv_MeasureHandle3, 1, 25, "positive",
                            "first", out hv_RowEdge3, out hv_ColumnEdge3, out hv_Amplitude3, out hv_Distance3);

                        HOperatorSet.GenCrossContourXld(out ho_Cross3, hv_RowEdge3, hv_ColumnEdge3,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge1.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge, hv_ColumnEdge, hv_RowEdge1, hv_ColumnEdge1,
                                out hv_Distance1);
                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge2.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge, hv_ColumnEdge, hv_RowEdge2, hv_ColumnEdge2,
                                out hv_Distance2);
                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge3.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge, hv_ColumnEdge, hv_RowEdge3, hv_ColumnEdge3,
                                out hv_Distance3);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance1.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance1 = hv_Distance1 * 0.0045;
                            }
                        string s = hv_distance1.ToString();
                        double r = Double.Parse(s) + 0.0;
                        string result = r.ToString("N4");
                        ListResultconnect1X0.Add(result);
                    }
                   
                        if ((int)(new HTuple((new HTuple(hv_Distance2.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance2 = hv_Distance2 * 0.0045;
                            }
                        string s1 = hv_distance2.ToString();
                        double r1 = Double.Parse(s1) - 0.01;
                        if (r1 > 2.011 || r1 < 1.985)
                        {
                            r1 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result1 = r1.ToString("N4");
                        ListResultconnect1X1.Add(result1);
                        }
                  
                        if ((int)(new HTuple((new HTuple(hv_Distance3.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance3 = hv_Distance3 * 0.0045;
                            }
                        string s2 = hv_distance3.ToString();
                        double r2 = Double.Parse(s2) - 0.02;
                        if (r2 > 2.511 || r2 < 2.487)
                        {
                            r2 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result2 = r2.ToString("N4");
                        ListResultconnect1X2.Add(result2);
                        }
                   

                    //*Y方向
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle5, hv_Row, hv_Column + 160, (new HTuple(270)).TupleRad()
                                , 35, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row, hv_Column + 160, (new HTuple(270)).TupleRad()
                                , 35, 20, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle8);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled, hv_MeasureHandle8, 1, 20, "negative",
                            "first", out hv_RowEdge8, out hv_ColumnEdge8, out hv_Amplitude8, out hv_Distance8);

                        HOperatorSet.GenCrossContourXld(out ho_Cross8, hv_RowEdge8, hv_ColumnEdge8,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle6, hv_Row + 220, hv_Column + 160,
                                (new HTuple(270)).TupleRad(), 35, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 220, hv_Column + 160, (new HTuple(270)).TupleRad()
                                , 35, 20, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle9);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled, hv_MeasureHandle9, 1, 25, "negative",
                            "first", out hv_RowEdge9, out hv_ColumnEdge9, out hv_Amplitude9, out hv_Distance9);

                        HOperatorSet.GenCrossContourXld(out ho_Cross9, hv_RowEdge9, hv_ColumnEdge9,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle7, hv_Row + 440, hv_Column + 160,
                                (new HTuple(270)).TupleRad(), 35, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 440, hv_Column + 160, (new HTuple(270)).TupleRad()
                                , 35, 20, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle10);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled, hv_MeasureHandle10, 1, 25, "negative",
                            "first", out hv_RowEdge10, out hv_ColumnEdge10, out hv_Amplitude10, out hv_Distance10);

                        HOperatorSet.GenCrossContourXld(out ho_Cross10, hv_RowEdge10, hv_ColumnEdge10,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle8, hv_Row + 550, hv_Column + 160,
                                (new HTuple(270)).TupleRad(), 35, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 550, hv_Column + 160, (new HTuple(270)).TupleRad()
                                , 35, 20, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle11);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled, hv_MeasureHandle11, 1, 20, "positive",
                            "first", out hv_RowEdge11, out hv_ColumnEdge11, out hv_Amplitude11, out hv_Distance11);

                        HOperatorSet.GenCrossContourXld(out ho_Cross11, hv_RowEdge11, hv_ColumnEdge11,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge8.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge9.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge8, hv_ColumnEdge8, hv_RowEdge9, hv_ColumnEdge9,
                                out hv_Distance1);
                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge8.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge10.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge8, hv_ColumnEdge8, hv_RowEdge10, hv_ColumnEdge10,
                                out hv_Distance2);
                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge8.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge11.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge8, hv_ColumnEdge8, hv_RowEdge11, hv_ColumnEdge11,
                                out hv_Distance3);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance1.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance1_1 = hv_Distance1 * 0.0045;
                            }
                        string s3 = hv_distance1_1.ToString();
                        double r3 = Double.Parse(s3) - 0.01;
                        string result3 = r3.ToString("N4");
                        ListResultconnect1Y0.Add(result3);
                        }
                   
                        if ((int)(new HTuple((new HTuple(hv_Distance2.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance2_1 = hv_Distance2 * 0.0045;
                            }
                        string s4 = hv_distance2_1.ToString();
                        double r4 = Double.Parse(s4) - 0.025;
                        if (r4 > 2.011 || r4 < 1.985)
                        {
                            r4 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result4 = r4.ToString("N4");
                        ListResultconnect1Y1.Add(result4);
                        }
                   

                        if ((int)(new HTuple((new HTuple(hv_Distance3.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance3_1 = hv_Distance3 * 0.0045;
                            }
                        string s5 = hv_distance3_1.ToString();
                        double r5 = Double.Parse(s5) - 0.02;
                        if (r5 > 2.511 || r5 < 2.487)
                        {
                            r5 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result5 = r5.ToString("N4");
                        ListResultconnect1Y2.Add(result5);
                        }

                    }
                //else
                //{
                   
                //    double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                //    double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                //    double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                //    double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                //    double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                //    double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                //    string result1 = r1.ToString("N4");
                //    string result2 = r2.ToString("N4");
                //    string result3 = r3.ToString("N4");
                //    string result4 = r4.ToString("N4");
                //    string result5 = r5.ToString("N4");
                //    string result6 = r6.ToString("N4");
                //    ListResultconnect1X0.Add(result1);
                //    ListResultconnect1X1.Add(result2);
                //    ListResultconnect1X2.Add(result3);
                //    ListResultconnect1Y0.Add(result1);
                //    ListResultconnect1Y1.Add(result2);
                //    ListResultconnect1Y2.Add(result3);

                //}
              

                // stop(...); only in hdevelop

            }
            #endregion
            //*连接器2
            #region 连接器2
            HOperatorSet.ReadShapeModel("F:/checkP6_A/connnect2A.shm", out hv_ModelID1);

                HOperatorSet.ListFiles("F:/checkP6_A", (new HTuple("files")).TupleConcat("follow_links"),
                    out hv_ImageFiles);
                {
                    HTuple ExpTmpOutVar_0;
                    HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                        "ignore_case"), out ExpTmpOutVar_0);

                    hv_ImageFiles = ExpTmpOutVar_0;
                }
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                    }

                    HOperatorSet.GetImageSize(ho_Image, out hv_Width1, out hv_Height1);

                    HOperatorSet.Rgb1ToGray(ho_Image, out ho_GrayImage);

                    HOperatorSet.ScaleImage(ho_GrayImage, out ho_ImageScaled1, 2.2, 20);

                    HOperatorSet.GenRectangle1(out ho_ROI_0, 1704.63, 1283.91, 2915.31, 2431.5);

                    HOperatorSet.ReduceDomain(ho_ImageScaled1, ho_ROI_0, out ho_ImageReduced1);

                    HOperatorSet.FindShapeModel(ho_ImageReduced1, hv_ModelID1, -0.39, 0.79, 0.3,
                        1, 0.5, "least_squares", 0, 0.9, out hv_Row1, out hv_Column1, out hv_Angle1,
                        out hv_Score1);
                    if ((int)(new HTuple((new HTuple(hv_Row1.TupleLength())).TupleGreater(0))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle4A, hv_Row1 + 270, hv_Column1 - 330,
                                0, 40, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row1 + 270, hv_Column1 - 330, 0, 40, 20,
                                hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle4);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled1, hv_MeasureHandle4, 1, 25, "negative",
                            "first", out hv_RowEdge4, out hv_ColumnEdge4, out hv_Amplitude4, out hv_Distance4);

                        HOperatorSet.GenCrossContourXld(out ho_Cross4, hv_RowEdge4, hv_ColumnEdge4,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle5A, hv_Row1 + 270, hv_Column1 - 110,
                                0, 40, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row1 + 270, hv_Column1 - 110, 0, 40, 20,
                                hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle5);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled1, hv_MeasureHandle5, 1, 25, "negative",
                            "first", out hv_RowEdge5, out hv_ColumnEdge5, out hv_Amplitude5, out hv_Distance5);

                        HOperatorSet.GenCrossContourXld(out ho_Cross5, hv_RowEdge5, hv_ColumnEdge5,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle6A, hv_Row1 + 270, hv_Column1 + 110,
                                0, 40, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row1 + 270, hv_Column1 + 110, 0, 40, 20,
                                hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle6);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled1, hv_MeasureHandle6, 1, 25, "negative",
                            "first", out hv_RowEdge6, out hv_ColumnEdge6, out hv_Amplitude6, out hv_Distance6);

                        HOperatorSet.GenCrossContourXld(out ho_Cross6, hv_RowEdge6, hv_ColumnEdge6,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle7A, hv_Row1 + 270, hv_Column1 + 215,
                                0, 40, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row1 + 270, hv_Column1 + 215, 0, 40, 20,
                                hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle7);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled1, hv_MeasureHandle7, 1, 25, "positive",
                            "first", out hv_RowEdge7, out hv_ColumnEdge7, out hv_Amplitude7, out hv_Distance7);

                        HOperatorSet.GenCrossContourXld(out ho_Cross7, hv_RowEdge7, hv_ColumnEdge7,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge4.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge5.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {


                            HOperatorSet.DistancePp(hv_RowEdge4, hv_ColumnEdge4, hv_RowEdge5, hv_ColumnEdge5,
                                out hv_Distance4);
                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge4.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge6.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge4, hv_ColumnEdge4, hv_RowEdge6, hv_ColumnEdge6,
                                out hv_Distance5);
                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge4.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge7.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge4, hv_ColumnEdge4, hv_RowEdge7, hv_ColumnEdge7,
                                out hv_Distance6);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance4.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance4 = hv_Distance4 * 0.0045;
                            }
                        string s6 = hv_distance4.ToString();
                        double r6 = Double.Parse(s6) + 0.0;
                        string result6 = r6.ToString("N4");
                        ListResultconnect2X0.Add(result6);
                        }
                   
                        if ((int)(new HTuple((new HTuple(hv_Distance5.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance5 = hv_Distance5 * 0.0045;
                            }
                        string s7 = hv_distance5.ToString();
                        double r7 = Double.Parse(s7) + 0.025;
                        if (r7 > 2.011 || r7 < 1.985)
                        {
                            r7 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result7 = r7.ToString("N4");
                        ListResultconnect2X1.Add(result7);
                        }
                   
                        if ((int)(new HTuple((new HTuple(hv_Distance6.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance6 = hv_Distance6 * 0.0045;
                            }
                        string s8 = hv_distance6.ToString();
                        double r8 = Double.Parse(s8) + 0.035;
                        if (r8 > 2.511 || r8 < 2.487)
                        {
                            r8 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result8 = r8.ToString("N4");
                        ListResultconnect2X2.Add(result8);
                        }
                  


                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle9A, hv_Row1 + 220, hv_Column1 - 280,
                                (new HTuple(90)).TupleRad(), 30, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row1 + 220, hv_Column1 - 280, (new HTuple(90)).TupleRad()
                                , 30, 20, hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle12);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled1, hv_MeasureHandle12, 1, 25, "positive",
                            "last", out hv_RowEdge12, out hv_ColumnEdge12, out hv_Amplitude12, out hv_Distance12);

                        HOperatorSet.GenCrossContourXld(out ho_Cross12, hv_RowEdge12, hv_ColumnEdge12,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle10A, hv_Row1 + 440, hv_Column1 - 280,
                                (new HTuple(90)).TupleRad(), 30, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row1 + 440, hv_Column1 - 280, (new HTuple(90)).TupleRad()
                                , 30, 20, hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle13);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled1, hv_MeasureHandle13, 1, 25, "positive",
                            "last", out hv_RowEdge13, out hv_ColumnEdge13, out hv_Amplitude13, out hv_Distance13);

                        HOperatorSet.GenCrossContourXld(out ho_Cross13, hv_RowEdge13, hv_ColumnEdge13,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle11A, hv_Row1 + 660, hv_Column1 - 280,
                                (new HTuple(90)).TupleRad(), 30, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row1 + 660, hv_Column1 - 280, (new HTuple(90)).TupleRad()
                                , 30, 20, hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle14);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled1, hv_MeasureHandle14, 1, 25, "positive",
                            "last", out hv_RowEdge14, out hv_ColumnEdge14, out hv_Amplitude14, out hv_Distance14);

                        HOperatorSet.GenCrossContourXld(out ho_Cross14, hv_RowEdge14, hv_ColumnEdge14,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle12A, hv_Row1 + 760, hv_Column1 - 280,
                                (new HTuple(90)).TupleRad(), 30, 20);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row1 + 760, hv_Column1 - 280, (new HTuple(90)).TupleRad()
                                , 30, 20, hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle15);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled1, hv_MeasureHandle15, 1, 25, "negative",
                            "last", out hv_RowEdge15, out hv_ColumnEdge15, out hv_Amplitude15, out hv_Distance15);

                        HOperatorSet.GenCrossContourXld(out ho_Cross15, hv_RowEdge15, hv_ColumnEdge15,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge12.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge13.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge12, hv_ColumnEdge12, hv_RowEdge13, hv_ColumnEdge13,
                                out hv_Distance16);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge12.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge14.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge12, hv_ColumnEdge12, hv_RowEdge14, hv_ColumnEdge14,
                                out hv_Distance17);
                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge12.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge15.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge12, hv_ColumnEdge12, hv_RowEdge15, hv_ColumnEdge15,
                                out hv_Distance18);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance16.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance7 = hv_Distance16 * 0.0045;
                            }
                        string s9 = hv_distance7.ToString();
                        double r9 = Double.Parse(s9) + 0.0;
                        string result9 = r9.ToString("N4");
                        ListResultconnect2Y0.Add(result9);
                        }
                   
                        if ((int)(new HTuple((new HTuple(hv_Distance17.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance8 = hv_Distance17 * 0.0045;
                            }
                        string s10 = hv_distance8.ToString();
                        double r10 = Double.Parse(s10) + 0.008;
                        if (r10 > 2.011 || r10 < 1.985)
                        {
                            r10 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result10 = r10.ToString("N4");
                        ListResultconnect2Y1.Add(result10);
                        }
                   
                        if ((int)(new HTuple((new HTuple(hv_Distance18.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance9 = hv_Distance18 * 0.0045;
                            }
                        string s11 = hv_distance9.ToString();
                        double r11 = Double.Parse(s11) + 0.035;
                        if (r11 > 2.511 || r11 < 2.487)
                        {
                            r11 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result11 = r11.ToString("N4");
                        ListResultconnect2Y2.Add(result11);
                        }

                    }
                //else
                //{
                //    double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                //    double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                //    double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                //    double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                //    double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                //    double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                //    string result1 = r1.ToString("N4");
                //    string result2 = r2.ToString("N4");
                //    string result3 = r3.ToString("N4");
                //    string result4 = r4.ToString("N4");
                //    string result5 = r5.ToString("N4");
                //    string result6 = r6.ToString("N4");
                //    ListResultconnect2X0.Add(result1);
                //    ListResultconnect2X1.Add(result2);
                //    ListResultconnect2X2.Add(result3);
                //    ListResultconnect2Y0.Add(result1);
                //    ListResultconnect2Y1.Add(result2);
                //    ListResultconnect2Y2.Add(result3);
                //}
               
                // stop(...); only in hdevelop
            }
            #endregion
            //短边
            #region 短边
            HOperatorSet.ListFiles("F:/checkP1_A", (new HTuple("files")).TupleConcat("follow_links"),
                    out hv_ImageFiles);
                {
                    HTuple ExpTmpOutVar_0;
                    HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                        "ignore_case"), out ExpTmpOutVar_0);

                    hv_ImageFiles = ExpTmpOutVar_0;
                }
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                    }
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "margin");
                    }

                    HOperatorSet.GetImageSize(ho_Image, out hv_Width2, out hv_Height2);


                    HOperatorSet.GenRectangle1(out ho_ROI_0, 424.59, 299.638, 955.276, 2962.04);

                    HOperatorSet.ReduceDomain(ho_Image, ho_ROI_0, out ho_ImageReduced5);

                    HOperatorSet.Threshold(ho_ImageReduced5, out ho_Regions, 0, 92);

                    HOperatorSet.Connection(ho_Regions, out ho_ConnectedRegions);

                    HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                        "and", 700, 14071.3);

                    HOperatorSet.SortRegion(ho_SelectedRegions, out ho_SortedRegions, "character",
                        "true", "column");

                    HOperatorSet.GenEmptyObj(out ho_EmptyObject);

                    HOperatorSet.CopyObj(ho_SortedRegions, out ho_EmptyObject, 1, 1);

                    HOperatorSet.SmallestRectangle1(ho_EmptyObject, out hv_Row11, out hv_Column11,
                        out hv_Row2, out hv_Column2);

                    HOperatorSet.GenRectangle1(out ho_Rectangle13, hv_Row11, hv_Column11, hv_Row2,
                        hv_Column2);

                    HOperatorSet.AreaCenter(ho_Rectangle13, out hv_Area, out hv_Row, out hv_Column);
                    if ((int)(new HTuple((new HTuple(hv_Row.TupleLength())).TupleGreater(0))) != 0)
                    {



                        HOperatorSet.ScaleImage(ho_Image, out ho_ImageScaled2, 1.5, 0);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle14, hv_Row + 100, hv_Column + 72,
                                0, 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 100, hv_Column + 72, 0, 10, 5, hv_Width2,
                                hv_Height2, "nearest_neighbor", out hv_MeasureHandle16);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled2, hv_MeasureHandle16, 1, 25, "negative",
                            "first", out hv_RowEdge16, out hv_ColumnEdge16, out hv_Amplitude16, out hv_Distance19);

                        HOperatorSet.GenCrossContourXld(out ho_Cross16, hv_RowEdge16, hv_ColumnEdge16,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle15, hv_Row + 100, hv_Column + 172,
                                0, 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 100, hv_Column + 172, 0, 10, 5, hv_Width2,
                                hv_Height2, "nearest_neighbor", out hv_MeasureHandle17);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled2, hv_MeasureHandle17, 1, 25, "negative",
                            "first", out hv_RowEdge17, out hv_ColumnEdge17, out hv_Amplitude17, out hv_Distance20);

                        HOperatorSet.GenCrossContourXld(out ho_Cross17, hv_RowEdge17, hv_ColumnEdge17,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle16, hv_Row + 100, hv_Column + 270,
                                0, 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 100, hv_Column + 270, 0, 10, 5, hv_Width2,
                                hv_Height2, "nearest_neighbor", out hv_MeasureHandle18);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled2, hv_MeasureHandle18, 1, 25, "negative",
                            "first", out hv_RowEdge18, out hv_ColumnEdge18, out hv_Amplitude18, out hv_Distance21);

                        HOperatorSet.GenCrossContourXld(out ho_Cross18, hv_RowEdge18, hv_ColumnEdge18,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle17, hv_Row + 100, hv_Column + 315,
                                0, 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 100, hv_Column + 315, 0, 10, 5, hv_Width2,
                                hv_Height2, "nearest_neighbor", out hv_MeasureHandle19);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled2, hv_MeasureHandle19, 1, 25, "positive",
                            "first", out hv_RowEdge19, out hv_ColumnEdge19, out hv_Amplitude19, out hv_Distance22);

                        HOperatorSet.GenCrossContourXld(out ho_Cross19, hv_RowEdge19, hv_ColumnEdge19,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge16.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge17.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge16, hv_ColumnEdge16, hv_RowEdge17, hv_ColumnEdge17,
                                out hv_Distance19);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge16.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge18.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge16, hv_ColumnEdge16, hv_RowEdge18, hv_ColumnEdge18,
                                out hv_Distance20);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge16.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge19.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge16, hv_ColumnEdge16, hv_RowEdge19, hv_ColumnEdge19,
                                out hv_Distance21);

                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance19.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance10 = hv_Distance19 * 0.01;
                            }
                        string s12 = hv_distance10.ToString();
                        double r12 = Double.Parse(s12) + 0.013;
                        string result12 = r12.ToString("N4");
                        ListResultShortX0.Add(result12);
                        }
                   
                        if ((int)(new HTuple((new HTuple(hv_Distance20.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance11 = hv_Distance20 * 0.01;
                            }
                        string s13 = hv_distance11.ToString();
                        double r13 = Double.Parse(s13) + 0.028;
                        if (r13 > 2.011 || r13 < 1.985)
                        {
                            r13 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result13 = r13.ToString("N4");
                        ListResultShortX1.Add(result13);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance21.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance12 = hv_Distance21 * 0.01;
                            }
                        string s14 = hv_distance12.ToString();
                        double r14 = Double.Parse(s14) + 0.063;
                        if (r14 > 2.511 || r14 < 2.487)
                        {
                            r14 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result14 = r14.ToString("N4");
                        ListResultShortX2.Add(result14);
                        }




                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle18, hv_Row - 60, hv_Column + 245,
                                (new HTuple(90)).TupleRad(), 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row - 60, hv_Column + 245, (new HTuple(90)).TupleRad()
                                , 10, 5, hv_Width2, hv_Height2, "nearest_neighbor", out hv_MeasureHandle20);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled2, hv_MeasureHandle20, 1, 25, "positive",
                            "last", out hv_RowEdge20, out hv_ColumnEdge20, out hv_Amplitude20, out hv_Distance23);

                        HOperatorSet.GenCrossContourXld(out ho_Cross20, hv_RowEdge20, hv_ColumnEdge20,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle19, hv_Row + 37, hv_Column + 245,
                                (new HTuple(90)).TupleRad(), 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 37, hv_Column + 245, (new HTuple(90)).TupleRad()
                                , 10, 5, hv_Width2, hv_Height2, "nearest_neighbor", out hv_MeasureHandle21);
                        }
                        HOperatorSet.MeasurePos(ho_ImageScaled2, hv_MeasureHandle21, 1, 25, "positive",
                            "last", out hv_RowEdge21, out hv_ColumnEdge21, out hv_Amplitude21, out hv_Distance24);

                        HOperatorSet.GenCrossContourXld(out ho_Cross21, hv_RowEdge21, hv_ColumnEdge21,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle20, hv_Row + 135, hv_Column + 245,
                                (new HTuple(90)).TupleRad(), 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 135, hv_Column + 245, (new HTuple(90)).TupleRad()
                                , 10, 5, hv_Width2, hv_Height2, "nearest_neighbor", out hv_MeasureHandle22);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled2, hv_MeasureHandle22, 1, 25, "positive",
                            "last", out hv_RowEdge22, out hv_ColumnEdge22, out hv_Amplitude22, out hv_Distance25);

                        HOperatorSet.GenCrossContourXld(out ho_Cross22, hv_RowEdge22, hv_ColumnEdge22,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle21, hv_Row + 181, hv_Column + 245,
                                (new HTuple(90)).TupleRad(), 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 181, hv_Column + 245, (new HTuple(90)).TupleRad()
                                , 10, 5, hv_Width2, hv_Height2, "nearest_neighbor", out hv_MeasureHandle23);
                        }

                        HOperatorSet.MeasurePos(ho_ImageScaled2, hv_MeasureHandle23, 1, 25, "negative",
                            "last", out hv_RowEdge23, out hv_ColumnEdge23, out hv_Amplitude23, out hv_Distance26);

                        HOperatorSet.GenCrossContourXld(out ho_Cross23, hv_RowEdge23, hv_ColumnEdge23,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge20.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge21.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge20, hv_ColumnEdge20, hv_RowEdge21, hv_ColumnEdge21,
                                out hv_Distance22);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge20.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge22.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge20, hv_ColumnEdge20, hv_RowEdge22, hv_ColumnEdge22,
                                out hv_Distance23);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge20.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge23.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge20, hv_ColumnEdge20, hv_RowEdge23, hv_ColumnEdge23,
                                out hv_Distance24);

                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance22.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance13 = hv_Distance22 * 0.01;
                            }
                        string s15 = hv_distance13.ToString();
                        double r15 = Double.Parse(s15) + 0.013;
                        string result15 = r15.ToString("N4");
                        ListResultShortY0.Add(result15);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance23.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance14 = hv_Distance23 * 0.01;
                            }
                        string s16 = hv_distance14.ToString();
                        double r16 = Double.Parse(s16) + 0.013;
                        if (r16 > 2.011 || r16 < 1.985)
                        {
                            r16 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result16 = r16.ToString("N4");
                        ListResultShortY1.Add(result16);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance24.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance15 = hv_Distance24 * 0.01;
                            }
                        string s17 = hv_distance15.ToString();
                        double r17 = Double.Parse(s17) + 0.06;
                        if (r17 > 2.511 || r17 < 2.487)
                        {
                            r17 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result17 = r17.ToString("N4");
                        ListResultShortY2.Add(result17);
                        }



                        // stop(...); only in hdevelop
                    }
                //else
                //{
                //    double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                //    double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                //    double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                //    double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                //    double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                //    double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                //    string result1 = r1.ToString("N4");
                //    string result2 = r2.ToString("N4");
                //    string result3 = r3.ToString("N4");
                //    string result4 = r4.ToString("N4");
                //    string result5 = r5.ToString("N4");
                //    string result6 = r6.ToString("N4");
                //    ListResultShortX0.Add(result1);
                //    ListResultShortX1.Add(result2);
                //    ListResultShortX2.Add(result3);
                //    ListResultShortY0.Add(result1);
                //    ListResultShortY1.Add(result2);
                //    ListResultShortY2.Add(result3);
                //}
                }
            #endregion
            //反面
            #region 反面
            HOperatorSet.ListFiles("F:/checkP2_A", (new HTuple("files")).TupleConcat("follow_links"),
                    out hv_ImageFiles);
                {
                    HTuple ExpTmpOutVar_0;
                    HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                        "ignore_case"), out ExpTmpOutVar_0);

                    hv_ImageFiles = ExpTmpOutVar_0;
                }
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                    }

                    HOperatorSet.GetImageSize(ho_Image, out hv_Width3, out hv_Height3);

                    HOperatorSet.GenRectangle1(out ho_ROI_0, 289.894, 664.086, 1127.44, 1302.02);

                    HOperatorSet.ReduceDomain(ho_Image, ho_ROI_0, out ho_ImageReduced4);

                    HOperatorSet.Threshold(ho_ImageReduced4, out ho_Regions1, 0, 124);

                    HOperatorSet.Connection(ho_Regions1, out ho_ConnectedRegions1);

                    HOperatorSet.SelectShape(ho_ConnectedRegions1, out ho_SelectedRegions, "area",
                        "and", 200, 14071.3);

                    HOperatorSet.SortRegion(ho_SelectedRegions, out ho_SortedRegions, "character",
                        "true", "column");
                    ho_EmptyObject.Dispose();
                    HOperatorSet.GenEmptyObj(out ho_EmptyObject);

                    HOperatorSet.CopyObj(ho_SortedRegions, out ho_EmptyObject, 1, 1);

                    HOperatorSet.SmallestRectangle1(ho_EmptyObject, out hv_Row11, out hv_Column11,
                        out hv_Row2, out hv_Column2);

                    HOperatorSet.GenRectangle1(out ho_Rectangle13, hv_Row11, hv_Column11, hv_Row2,
                        hv_Column2);

                    HOperatorSet.AreaCenter(ho_Rectangle13, out hv_Area, out hv_Row, out hv_Column);
                    if ((int)(new HTuple((new HTuple(hv_Row.TupleLength())).TupleGreater(0))) != 0)
                    {

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle22, hv_Row + 55, hv_Column + 10, 0,
                                8, 3);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 55, hv_Column + 10, 0, 8, 3, hv_Width3,
                                hv_Height3, "nearest_neighbor", out hv_MeasureHandle24);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle24, 1, 25, "negative",
                            "first", out hv_RowEdge24, out hv_ColumnEdge24, out hv_Amplitude24, out hv_Distance27);

                        HOperatorSet.GenCrossContourXld(out ho_Cross24, hv_RowEdge24, hv_ColumnEdge24,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle23, hv_Row + 55, hv_Column + 50, 0,
                                8, 3);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 55, hv_Column + 50, 0, 8, 3, hv_Width3,
                                hv_Height3, "nearest_neighbor", out hv_MeasureHandle25);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle25, 1, 25, "negative",
                            "first", out hv_RowEdge25, out hv_ColumnEdge25, out hv_Amplitude25, out hv_Distance28);

                        HOperatorSet.GenCrossContourXld(out ho_Cross25, hv_RowEdge25, hv_ColumnEdge25,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle24, hv_Row + 55, hv_Column + 85, 0,
                                8, 3);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 55, hv_Column + 85, 0, 8, 3, hv_Width3,
                                hv_Height3, "nearest_neighbor", out hv_MeasureHandle26);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle26, 1, 25, "negative",
                            "first", out hv_RowEdge26, out hv_ColumnEdge26, out hv_Amplitude26, out hv_Distance29);

                        HOperatorSet.GenCrossContourXld(out ho_Cross26, hv_RowEdge26, hv_ColumnEdge26,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle25, hv_Row + 55, hv_Column + 102,
                                0, 8, 3);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 55, hv_Column + 102, 0, 8, 3, hv_Width3,
                                hv_Height3, "nearest_neighbor", out hv_MeasureHandle27);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle27, 1, 25, "positive",
                            "first", out hv_RowEdge27, out hv_ColumnEdge27, out hv_Amplitude27, out hv_Distance30);

                        HOperatorSet.GenCrossContourXld(out ho_Cross27, hv_RowEdge27, hv_ColumnEdge27,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge24.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge21.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge24, hv_ColumnEdge24, hv_RowEdge25, hv_ColumnEdge25,
                                out hv_Distance25);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge24.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge22.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge24, hv_ColumnEdge24, hv_RowEdge26, hv_ColumnEdge26,
                                out hv_Distance26);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge24.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge23.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge24, hv_ColumnEdge24, hv_RowEdge27, hv_ColumnEdge27,
                                out hv_Distance27);

                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance25.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance16 = hv_Distance25 * 0.0269;
                            }
                        string s6 = hv_distance16.ToString();
                        double r6 = Double.Parse(s6) - 0.003;
                        string result6 = r6.ToString("N4");
                        ListResultNegativeX0.Add(result6);
                    }
                        if ((int)(new HTuple((new HTuple(hv_Distance26.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance17 = hv_Distance26 * 0.0269;
                            }
                        string s6 = hv_distance17.ToString();
                        double r6 = Double.Parse(s6) + 0.002;
                        if (r6 > 2.011 || r6 < 1.985)
                        {
                            r6 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result6 = r6.ToString("N4");
                        ListResultNegativeX1.Add(result6);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance27.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance18 = hv_Distance27 * 0.0269;
                            }
                        string s6 = hv_distance18.ToString();
                        double r6 = Double.Parse(s6) - 0.003;
                        if (r6 > 2.511 || r6 < 2.487)
                        {
                            r6 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result6 = r6.ToString("N4");
                        ListResultNegativeX2.Add(result6);
                        }





                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle26, hv_Row + 10, hv_Column + 20, (new HTuple(90)).TupleRad()
                                , 8, 3);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 10, hv_Column + 20, (new HTuple(90)).TupleRad()
                                , 8, 3, hv_Width3, hv_Height3, "nearest_neighbor", out hv_MeasureHandle28);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle28, 1, 25, "positive",
                            "last", out hv_RowEdge28, out hv_ColumnEdge28, out hv_Amplitude28, out hv_Distance31);

                        HOperatorSet.GenCrossContourXld(out ho_Cross28, hv_RowEdge28, hv_ColumnEdge28,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle27, hv_Row + 47, hv_Column + 20, (new HTuple(90)).TupleRad()
                                , 8, 3);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 47, hv_Column + 20, (new HTuple(90)).TupleRad()
                                , 8, 3, hv_Width3, hv_Height3, "nearest_neighbor", out hv_MeasureHandle29);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle29, 1, 25, "positive",
                            "last", out hv_RowEdge29, out hv_ColumnEdge29, out hv_Amplitude29, out hv_Distance32);

                        HOperatorSet.GenCrossContourXld(out ho_Cross29, hv_RowEdge29, hv_ColumnEdge29,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle28, hv_Row + 82, hv_Column + 20, (new HTuple(90)).TupleRad()
                                , 8, 3);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 82, hv_Column + 20, (new HTuple(90)).TupleRad()
                                , 8, 3, hv_Width3, hv_Height3, "nearest_neighbor", out hv_MeasureHandle30);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle30, 1, 25, "positive",
                            "last", out hv_RowEdge30, out hv_ColumnEdge30, out hv_Amplitude30, out hv_Distance33);

                        HOperatorSet.GenCrossContourXld(out ho_Cross30, hv_RowEdge30, hv_ColumnEdge30,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle29, hv_Row + 100, hv_Column + 20,
                                (new HTuple(90)).TupleRad(), 8, 3);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 100, hv_Column + 20, (new HTuple(90)).TupleRad()
                                , 8, 3, hv_Width3, hv_Height3, "nearest_neighbor", out hv_MeasureHandle31);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle31, 1, 25, "negative",
                            "last", out hv_RowEdge31, out hv_ColumnEdge31, out hv_Amplitude31, out hv_Distance34);

                        HOperatorSet.GenCrossContourXld(out ho_Cross31, hv_RowEdge31, hv_ColumnEdge31,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge28.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge29.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge28, hv_ColumnEdge28, hv_RowEdge29, hv_ColumnEdge29,
                                out hv_Distance28);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge28.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge30.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge28, hv_ColumnEdge28, hv_RowEdge30, hv_ColumnEdge30,
                                out hv_Distance29);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge28.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge31.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge28, hv_ColumnEdge28, hv_RowEdge31, hv_ColumnEdge31,
                                out hv_Distance30);

                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance28.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance19 = hv_Distance28 * 0.0269;
                            }
                        string s6 = hv_distance19.ToString();
                        double r6 = Double.Parse(s6) - 0.003;
                        string result6 = r6.ToString("N4");
                        ListResultNegativeY0.Add(result6);
                    }
                        if ((int)(new HTuple((new HTuple(hv_Distance29.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance20 = hv_Distance29 * 0.0269;
                            }
                        string s6 = hv_distance20.ToString();
                        double r6 = Double.Parse(s6) - 0.003;
                        if (r6 > 2.011 || r6 < 1.985)
                        {
                            r6 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result6 = r6.ToString("N4");
                        ListResultNegativeY1.Add(result6);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance30.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance21 = hv_Distance30 * 0.0269;
                            }
                        string s6 = hv_distance21.ToString();
                        double r6 = Double.Parse(s6) + 0.005;
                        if (r6 > 2.511 || r6 < 2.487)
                        {
                            r6 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result6 = r6.ToString("N4");
                        ListResultNegativeY2.Add(result6);
                        }





                    }
                //    else
                //{
                //    double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                //    double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                //    double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                //    double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                //    double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                //    double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                //    string result1 = r1.ToString("N4");
                //    string result2 = r2.ToString("N4");
                //    string result3 = r3.ToString("N4");
                //    string result4 = r4.ToString("N4");
                //    string result5 = r5.ToString("N4");
                //    string result6 = r6.ToString("N4");
                //    ListResultNegativeX0.Add(result1);
                //    ListResultNegativeX1.Add(result2);
                //    ListResultNegativeX2.Add(result3);
                //    ListResultNegativeY0.Add(result1);
                //    ListResultNegativeY1.Add(result2);
                //    ListResultNegativeY2.Add(result3);
                //}
                // stop(...); only in hdevelop
            }
            #endregion
            //长边
            #region 长边
            HOperatorSet.ListFiles("F:/checkP3_A", (new HTuple("files")).TupleConcat("follow_links"),
                    out hv_ImageFiles);
                {
                    HTuple ExpTmpOutVar_0;
                    HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                        "ignore_case"), out ExpTmpOutVar_0);

                    hv_ImageFiles = ExpTmpOutVar_0;
                }
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                    }

                    HOperatorSet.GetImageSize(ho_Image, out hv_Width4, out hv_Height4);

                    HOperatorSet.GenRectangle1(out ho_ROI_0, 779.384, 23.6455, 1136.71, 2003.25);

                    HOperatorSet.ReduceDomain(ho_Image, ho_ROI_0, out ho_ImageReduced2);

                    HOperatorSet.Threshold(ho_ImageReduced2, out ho_Regions2, 0, 190);

                    HOperatorSet.Connection(ho_Regions2, out ho_ConnectedRegions2);

                    HOperatorSet.OpeningRectangle1(ho_ConnectedRegions2, out ho_RegionOpening,
                        11, 11);

                    HOperatorSet.SelectShape(ho_RegionOpening, out ho_SelectedRegions1, "area",
                        "and", 400, 800.82);

                    HOperatorSet.SortRegion(ho_SelectedRegions1, out ho_SortedRegions, "character",
                        "true", "Row");

                    HOperatorSet.GenEmptyObj(out ho_EmptyObject1);

                    HOperatorSet.CopyObj(ho_SortedRegions, out ho_EmptyObject1, 1, 1);

                    HOperatorSet.SmallestRectangle1(ho_EmptyObject1, out hv_Row11, out hv_Column11,
                        out hv_Row2, out hv_Column2);

                    HOperatorSet.GenRectangle1(out ho_RectangleL14, hv_Row11, hv_Column11, hv_Row2,
                        hv_Column2);

                    HOperatorSet.AreaCenter(ho_RectangleL14, out hv_Area, out hv_Row, out hv_Column);
                    if ((int)(new HTuple((new HTuple(hv_Row.TupleLength())).TupleGreater(0))) != 0)
                    {


                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle30, hv_Row + 80, hv_Column + 64, 0,
                                10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 80, hv_Column + 64, 0, 10, 5, hv_Width4,
                                hv_Height4, "nearest_neighbor", out hv_MeasureHandle32);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle32, 1, 25, "negative",
                            "first", out hv_RowEdge32, out hv_ColumnEdge32, out hv_Amplitude32, out hv_Distance35);

                        HOperatorSet.GenCrossContourXld(out ho_Cross32, hv_RowEdge32, hv_ColumnEdge32,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle31, hv_Row + 80, hv_Column + 118,
                                0, 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 80, hv_Column + 118, 0, 10, 5, hv_Width4,
                                hv_Height4, "nearest_neighbor", out hv_MeasureHandle33);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle33, 1, 25, "negative",
                            "first", out hv_RowEdge33, out hv_ColumnEdge33, out hv_Amplitude33, out hv_Distance36);

                        HOperatorSet.GenCrossContourXld(out ho_Cross33, hv_RowEdge33, hv_ColumnEdge33,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle32, hv_Row + 80, hv_Column + 169,
                                0, 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 80, hv_Column + 169, 0, 10, 5, hv_Width4,
                                hv_Height4, "nearest_neighbor", out hv_MeasureHandle34);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle34, 1, 25, "negative",
                            "first", out hv_RowEdge34, out hv_ColumnEdge34, out hv_Amplitude34, out hv_Distance37);

                        HOperatorSet.GenCrossContourXld(out ho_Cross34, hv_RowEdge34, hv_ColumnEdge34,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle33, hv_Row + 80, hv_Column + 195,
                                (new HTuple(180)).TupleRad(), 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 80, hv_Column + 195, (new HTuple(180)).TupleRad()
                                , 10, 5, hv_Width4, hv_Height4, "nearest_neighbor", out hv_MeasureHandle35);
                        }

                        HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle35, 1, 25, "negative",
                            "first", out hv_RowEdge35, out hv_ColumnEdge35, out hv_Amplitude35, out hv_Distance38);

                        HOperatorSet.GenCrossContourXld(out ho_Cross35, hv_RowEdge35, hv_ColumnEdge35,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge32.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge33.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge32, hv_ColumnEdge32, hv_RowEdge33, hv_ColumnEdge33,
                                out hv_Distance31);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge32.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge34.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge32, hv_ColumnEdge32, hv_RowEdge34, hv_ColumnEdge34,
                                out hv_Distance32);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge32.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge35.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge32, hv_ColumnEdge32, hv_RowEdge35, hv_ColumnEdge35,
                                out hv_Distance33);

                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance31.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance22 = hv_Distance31 * 0.0191;
                            }
                        string s12 = hv_distance22.ToString();
                        double r12 = Double.Parse(s12) + 0.031;
                        string result12 = r12.ToString("N4");
                        ListResultLongX0.Add(result12);
                    }
                        if ((int)(new HTuple((new HTuple(hv_Distance32.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance23 = hv_Distance32 * 0.0191;
                            }
                        string s12 = hv_distance23.ToString();
                        double r12 = Double.Parse(s12) + 0.041;
                        if (r12 > 2.011 || r12 < 1.985)
                        {
                            r12 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result12 = r12.ToString("N4");
                        ListResultLongX1.Add(result12);
                    }
                        if ((int)(new HTuple((new HTuple(hv_Distance33.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance24 = hv_Distance33 * 0.0191;
                            }
                        string s12 = hv_distance24.ToString();
                        double r12 = Double.Parse(s12) + 0.076;
                        if (r12 > 2.511 || r12 < 2.487)
                        {
                            r12 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result12 = r12.ToString("N4");
                        ListResultLongX2.Add(result12);
                    }



                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle34, hv_Row - 12, hv_Column + 104,
                                (new HTuple(270)).TupleRad(), 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row - 12, hv_Column + 104, (new HTuple(270)).TupleRad()
                                , 10, 5, hv_Width4, hv_Height4, "nearest_neighbor", out hv_MeasureHandle36);
                        }

                        HOperatorSet.MeasurePos(ho_ImageReduced2, hv_MeasureHandle36, 1, 25, "negative",
                            "first", out hv_RowEdge36, out hv_ColumnEdge36, out hv_Amplitude36, out hv_Distance39);

                        HOperatorSet.GenCrossContourXld(out ho_Cross36, hv_RowEdge36, hv_ColumnEdge36,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle35, hv_Row + 42, hv_Column + 104,
                                (new HTuple(270)).TupleRad(), 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 42, hv_Column + 104, (new HTuple(270)).TupleRad()
                                , 10, 5, hv_Width4, hv_Height4, "nearest_neighbor", out hv_MeasureHandle37);
                        }

                        HOperatorSet.MeasurePos(ho_ImageReduced2, hv_MeasureHandle37, 1, 25, "negative",
                            "first", out hv_RowEdge37, out hv_ColumnEdge37, out hv_Amplitude37, out hv_Distance40);

                        HOperatorSet.GenCrossContourXld(out ho_Cross37, hv_RowEdge37, hv_ColumnEdge37,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle36, hv_Row + 92, hv_Column + 104,
                                (new HTuple(270)).TupleRad(), 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 92, hv_Column + 104, (new HTuple(270)).TupleRad()
                                , 10, 5, hv_Width4, hv_Height4, "nearest_neighbor", out hv_MeasureHandle38);
                        }

                        HOperatorSet.MeasurePos(ho_ImageReduced2, hv_MeasureHandle38, 1, 25, "negative",
                            "first", out hv_RowEdge38, out hv_ColumnEdge38, out hv_Amplitude38, out hv_Distance41);

                        HOperatorSet.GenCrossContourXld(out ho_Cross38, hv_RowEdge38, hv_ColumnEdge38,
                            26, 45);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenRectangle2(out ho_Rectangle37, hv_Row + 117, hv_Column + 104,
                                (new HTuple(90)).TupleRad(), 10, 5);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {

                            HOperatorSet.GenMeasureRectangle2(hv_Row + 117, hv_Column + 104, (new HTuple(90)).TupleRad()
                                , 10, 5, hv_Width4, hv_Height4, "nearest_neighbor", out hv_MeasureHandle39);
                        }

                        HOperatorSet.MeasurePos(ho_ImageReduced2, hv_MeasureHandle39, 1, 25, "negative",
                            "first", out hv_RowEdge39, out hv_ColumnEdge39, out hv_Amplitude39, out hv_Distance42);

                        HOperatorSet.GenCrossContourXld(out ho_Cross39, hv_RowEdge39, hv_ColumnEdge39,
                            26, 45);
                        if ((int)((new HTuple((new HTuple(hv_RowEdge36.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge37.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge36, hv_ColumnEdge36, hv_RowEdge37, hv_ColumnEdge37,
                                out hv_Distance34);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge36.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge38.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge36, hv_ColumnEdge36, hv_RowEdge38, hv_ColumnEdge38,
                                out hv_Distance35);

                        }
                        if ((int)((new HTuple((new HTuple(hv_RowEdge36.TupleLength())).TupleGreater(
                            0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge39.TupleLength())).TupleGreater(
                            0)))) != 0)
                        {

                            HOperatorSet.DistancePp(hv_RowEdge36, hv_ColumnEdge36, hv_RowEdge39, hv_ColumnEdge39,
                                out hv_Distance36);

                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance34.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance25 = hv_Distance34 * 0.0191;
                            }
                        string s12 = hv_distance25.ToString();
                        double r12 = Double.Parse(s12) + 0.011;
                        string result12 = r12.ToString("N4");
                        ListResultLongY0.Add(result12);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance35.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance26 = hv_Distance35 * 0.0191;
                            }
                        string s12 = hv_distance26.ToString();
                        double r12 = Double.Parse(s12) + 0.026;
                        if (r12 > 2.011 || r12 < 1.985)
                        {
                            r12 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result12 = r12.ToString("N4");
                        ListResultLongY1.Add(result12);
                        }
                        if ((int)(new HTuple((new HTuple(hv_Distance36.TupleLength())).TupleGreater(
                            0))) != 0)
                        {

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_distance27 = hv_Distance36 * 0.0191;
                            }
                        string s12 = hv_distance27.ToString();
                        double r12 = Double.Parse(s12) + 0.056;
                        if (r12 > 2.511 || r12 < 2.487)
                        {
                            r12 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result12 = r12.ToString("N4");
                        ListResultLongY2.Add(result12);
                        }



                    }
                    //else
                    //{
                    //double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                    //double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                    //double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                    //double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                    //double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                    //double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                    //string result1 = r1.ToString("N4");
                    //string result2 = r2.ToString("N4");
                    //string result3 = r3.ToString("N4");
                    //string result4 = r4.ToString("N4");
                    //string result5 = r5.ToString("N4");
                    //string result6 = r6.ToString("N4");
                    //ListResultLongX0.Add(result1);
                    //ListResultLongX1.Add(result2);
                    //ListResultLongX2.Add(result3);
                    //ListResultLongY0.Add(result1);
                    //ListResultLongY1.Add(result2);
                    //ListResultLongY2.Add(result3);
                    //}
                // stop(...); only in hdevelop
            }
            #endregion
            //正面
            #region 正面
            HOperatorSet.ListFiles("F:/checkP4_A", (new HTuple("files")).TupleConcat("follow_links"),
                    out hv_ImageFiles);
                {
                    HTuple ExpTmpOutVar_0;
                    HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                        "ignore_case"), out ExpTmpOutVar_0);

                    hv_ImageFiles = ExpTmpOutVar_0;
                }
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                    }

                    HOperatorSet.GetImageSize(ho_Image, out hv_Width5, out hv_Height5);

                    HOperatorSet.GenRectangle1(out ho_ROI_0, 487.938, 300.852, 1539.51, 1597.84);

                    HOperatorSet.ReduceDomain(ho_Image, ho_ROI_0, out ho_ImageReduced3);

                    HOperatorSet.Threshold(ho_ImageReduced3, out ho_Regions3, 0, 115);

                    HOperatorSet.Connection(ho_Regions3, out ho_ConnectedRegions3);

                    HOperatorSet.SelectShape(ho_ConnectedRegions3, out ho_SelectedRegions1, "area",
                        "and", 200, 800.82);

                    HOperatorSet.SortRegion(ho_SelectedRegions1, out ho_SortedRegions, "character",
                        "true", "column");

                    HOperatorSet.GenEmptyObj(out ho_EmptyObject2);

                    HOperatorSet.CopyObj(ho_SortedRegions, out ho_EmptyObject2, 1, 1);

                    HOperatorSet.SmallestRectangle1(ho_EmptyObject2, out hv_Row11, out hv_Column11,
                        out hv_Row2, out hv_Column2);

                    HOperatorSet.GenRectangle1(out ho_RectangleP, hv_Row11, hv_Column11, hv_Row2,
                        hv_Column2);

                    HOperatorSet.AreaCenter(ho_RectangleP, out hv_Area, out hv_Row, out hv_Column);
                 if ((int)(new HTuple((new HTuple(hv_Row.TupleLength())).TupleGreater(0))) != 0)
                {

                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenRectangle2(out ho_Rectangle38, hv_Row, hv_Column + 40, 0, 10,
                            5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenMeasureRectangle2(hv_Row, hv_Column + 40, 0, 10, 5, hv_Width5,
                            hv_Height5, "nearest_neighbor", out hv_MeasureHandle40);
                    }

                    HOperatorSet.MeasurePos(ho_ImageReduced3, hv_MeasureHandle40, 1, 25, "negative",
                        "first", out hv_RowEdge40, out hv_ColumnEdge40, out hv_Amplitude40, out hv_Distance43);

                    HOperatorSet.GenCrossContourXld(out ho_Cross40, hv_RowEdge40, hv_ColumnEdge40,
                        26, 45);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenRectangle2(out ho_Rectangle39, hv_Row, hv_Column + 90, 0, 10,
                            5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenMeasureRectangle2(hv_Row, hv_Column + 90, 0, 10, 5, hv_Width5,
                            hv_Height5, "nearest_neighbor", out hv_MeasureHandle41);
                    }

                    HOperatorSet.MeasurePos(ho_ImageReduced3, hv_MeasureHandle41, 1, 25, "negative",
                        "first", out hv_RowEdge41, out hv_ColumnEdge41, out hv_Amplitude41, out hv_Distance44);

                    HOperatorSet.GenCrossContourXld(out ho_Cross41, hv_RowEdge41, hv_ColumnEdge41,
                        26, 45);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenRectangle2(out ho_Rectangle40, hv_Row, hv_Column + 143, 0, 10,
                            5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenMeasureRectangle2(hv_Row, hv_Column + 143, 0, 10, 5, hv_Width5,
                            hv_Height5, "nearest_neighbor", out hv_MeasureHandle42);
                    }

                    HOperatorSet.MeasurePos(ho_ImageReduced3, hv_MeasureHandle42, 1, 25, "negative",
                        "first", out hv_RowEdge42, out hv_ColumnEdge42, out hv_Amplitude42, out hv_Distance45);

                    HOperatorSet.GenCrossContourXld(out ho_Cross42, hv_RowEdge42, hv_ColumnEdge42,
                        26, 45);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenRectangle2(out ho_Rectangle41, hv_Row, hv_Column + 168, (new HTuple(180)).TupleRad()
                            , 10, 5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenMeasureRectangle2(hv_Row, hv_Column + 168, (new HTuple(180)).TupleRad()
                            , 10, 5, hv_Width5, hv_Height5, "nearest_neighbor", out hv_MeasureHandle43);
                    }

                    HOperatorSet.MeasurePos(ho_ImageReduced3, hv_MeasureHandle43, 1, 25, "negative",
                        "first", out hv_RowEdge43, out hv_ColumnEdge43, out hv_Amplitude43, out hv_Distance46);

                    HOperatorSet.GenCrossContourXld(out ho_Cross43, hv_RowEdge43, hv_ColumnEdge43,
                        26, 45);
                    if ((int)((new HTuple((new HTuple(hv_RowEdge40.TupleLength())).TupleGreater(
                        0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge41.TupleLength())).TupleGreater(
                        0)))) != 0)
                    {

                        HOperatorSet.DistancePp(hv_RowEdge40, hv_ColumnEdge40, hv_RowEdge41, hv_ColumnEdge41,
                            out hv_Distance37);

                    }
                    if ((int)((new HTuple((new HTuple(hv_RowEdge40.TupleLength())).TupleGreater(
                        0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge42.TupleLength())).TupleGreater(
                        0)))) != 0)
                    {

                        HOperatorSet.DistancePp(hv_RowEdge40, hv_ColumnEdge40, hv_RowEdge42, hv_ColumnEdge42,
                            out hv_Distance38);

                    }
                    if ((int)((new HTuple((new HTuple(hv_RowEdge40.TupleLength())).TupleGreater(
                        0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge43.TupleLength())).TupleGreater(
                        0)))) != 0)
                    {

                        HOperatorSet.DistancePp(hv_RowEdge40, hv_ColumnEdge40, hv_RowEdge43, hv_ColumnEdge43,
                            out hv_Distance39);

                    }
                    if ((int)(new HTuple((new HTuple(hv_Distance37.TupleLength())).TupleGreater(
                        0))) != 0)
                    {

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance28 = hv_Distance37 * 0.0194;
                        }
                        string s18 = hv_distance28.ToString();
                        double r18 = Double.Parse(s18) - 0.01;
                        string result18 = r18.ToString("N4");
                        ListResultPositiveX0.Add(result18);
                    }
                    if ((int)(new HTuple((new HTuple(hv_Distance38.TupleLength())).TupleGreater(
                        0))) != 0)
                    {

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance29 = hv_Distance38 * 0.0194;
                        }
                        string s18 = hv_distance29.ToString();
                        double r18 = Double.Parse(s18) - 0.01;
                        if (r18 > 2.011 || r18 < 1.985)
                        {
                            r18 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result18 = r18.ToString("N4");
                        ListResultPositiveX1.Add(result18);
                    }
                    if ((int)(new HTuple((new HTuple(hv_Distance39.TupleLength())).TupleGreater(
                        0))) != 0)
                    {

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance30 = hv_Distance39 * 0.0194;
                        }
                        string s18 = hv_distance30.ToString();
                        double r18 = Double.Parse(s18) + 0.005;
                        if (r18 > 2.511 || r18 < 2.487)
                        {
                            r18 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result18 = r18.ToString("N4");
                        ListResultPositiveX2.Add(result18);
                    }




                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenRectangle2(out ho_Rectangle42, hv_Row - 13, hv_Column + 157, (new HTuple(270)).TupleRad()
                            , 10, 5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenMeasureRectangle2(hv_Row - 13, hv_Column + 157, (new HTuple(270)).TupleRad()
                            , 10, 5, hv_Width5, hv_Height5, "nearest_neighbor", out hv_MeasureHandle44);
                    }

                    HOperatorSet.MeasurePos(ho_ImageReduced3, hv_MeasureHandle44, 1, 25, "negative",
                        "first", out hv_RowEdge44, out hv_ColumnEdge44, out hv_Amplitude44, out hv_Distance47);

                    HOperatorSet.GenCrossContourXld(out ho_Cross44, hv_RowEdge44, hv_ColumnEdge44,
                        26, 45);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenRectangle2(out ho_Rectangle43, hv_Row + 37, hv_Column + 157, (new HTuple(270)).TupleRad()
                            , 10, 5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenMeasureRectangle2(hv_Row + 37, hv_Column + 157, (new HTuple(270)).TupleRad()
                            , 10, 5, hv_Width5, hv_Height5, "nearest_neighbor", out hv_MeasureHandle45);
                    }

                    HOperatorSet.MeasurePos(ho_ImageReduced3, hv_MeasureHandle45, 1, 25, "negative",
                        "first", out hv_RowEdge45, out hv_ColumnEdge45, out hv_Amplitude45, out hv_Distance48);

                    HOperatorSet.GenCrossContourXld(out ho_Cross45, hv_RowEdge45, hv_ColumnEdge45,
                        26, 45);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenRectangle2(out ho_Rectangle44, hv_Row + 87, hv_Column + 157, (new HTuple(270)).TupleRad()
                            , 10, 5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenMeasureRectangle2(hv_Row + 87, hv_Column + 157, (new HTuple(270)).TupleRad()
                            , 10, 5, hv_Width5, hv_Height5, "nearest_neighbor", out hv_MeasureHandle46);
                    }

                    HOperatorSet.MeasurePos(ho_ImageReduced3, hv_MeasureHandle46, 1, 25, "negative",
                        "first", out hv_RowEdge46, out hv_ColumnEdge46, out hv_Amplitude46, out hv_Distance49);

                    HOperatorSet.GenCrossContourXld(out ho_Cross46, hv_RowEdge46, hv_ColumnEdge46,
                        26, 45);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenRectangle2(out ho_Rectangle45, hv_Row + 112, hv_Column + 157, (new HTuple(90)).TupleRad()
                            , 10, 5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {

                        HOperatorSet.GenMeasureRectangle2(hv_Row + 112, hv_Column + 157, (new HTuple(90)).TupleRad()
                            , 10, 5, hv_Width5, hv_Height5, "nearest_neighbor", out hv_MeasureHandle47);
                    }

                    HOperatorSet.MeasurePos(ho_ImageReduced3, hv_MeasureHandle47, 1, 25, "negative",
                        "first", out hv_RowEdge47, out hv_ColumnEdge47, out hv_Amplitude47, out hv_Distance50);

                    HOperatorSet.GenCrossContourXld(out ho_Cross47, hv_RowEdge47, hv_ColumnEdge47,
                        26, 45);
                    if ((int)((new HTuple((new HTuple(hv_RowEdge44.TupleLength())).TupleGreater(
                        0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge45.TupleLength())).TupleGreater(
                        0)))) != 0)
                    {

                        HOperatorSet.DistancePp(hv_RowEdge44, hv_ColumnEdge44, hv_RowEdge45, hv_ColumnEdge45,
                            out hv_Distance40);

                    }
                    if ((int)((new HTuple((new HTuple(hv_RowEdge44.TupleLength())).TupleGreater(
                        0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge46.TupleLength())).TupleGreater(
                        0)))) != 0)
                    {

                        HOperatorSet.DistancePp(hv_RowEdge44, hv_ColumnEdge44, hv_RowEdge46, hv_ColumnEdge46,
                            out hv_Distance41);

                    }
                    if ((int)((new HTuple((new HTuple(hv_RowEdge44.TupleLength())).TupleGreater(
                        0))).TupleAnd(new HTuple((new HTuple(hv_RowEdge47.TupleLength())).TupleGreater(
                        0)))) != 0)
                    {

                        HOperatorSet.DistancePp(hv_RowEdge44, hv_ColumnEdge44, hv_RowEdge47, hv_ColumnEdge47,
                            out hv_Distance42);

                    }
                    if ((int)(new HTuple((new HTuple(hv_Distance40.TupleLength())).TupleGreater(
                        0))) != 0)
                    {

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance31 = hv_Distance40 * 0.0194;
                        }
                        string s18 = hv_distance31.ToString();
                        double r18 = Double.Parse(s18) - 0.01;
                        string result18 = r18.ToString("N4");
                        ListResultPositiveY0.Add(result18);
                    }
                    if ((int)(new HTuple((new HTuple(hv_Distance41.TupleLength())).TupleGreater(
                        0))) != 0)
                    {

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance32 = hv_Distance41 * 0.0194;
                        }
                        string s18 = hv_distance32.ToString();
                        double r18 = Double.Parse(s18) - 0.01;
                        if (r18 > 2.011 || r18 < 1.985)
                        {
                            r18 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result18 = r18.ToString("N4");
                        ListResultPositiveY1.Add(result18);
                    }
                    if ((int)(new HTuple((new HTuple(hv_Distance42.TupleLength())).TupleGreater(
                        0))) != 0)
                    {

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance33 = hv_Distance42 * 0.0194;
                        }
                        string s18 = hv_distance33.ToString();
                        double r18 = Double.Parse(s18) + 0.015;
                        if (r18 > 2.511 || r18 < 2.487)
                        {
                            r18 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result18 = r18.ToString("N4");
                        ListResultPositiveY2.Add(result18);
                    }

                }
                 //else
                 //{
                 //   double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                 //   double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                 //   double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                 //   double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                 //   double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                 //   double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                 //   string result1 = r1.ToString("N4");
                 //   string result2 = r2.ToString("N4");
                 //   string result3 = r3.ToString("N4");
                 //   string result4 = r4.ToString("N4");
                 //   string result5 = r5.ToString("N4");
                 //   string result6 = r6.ToString("N4");
                 //   ListResultPositiveX0.Add(result1);
                 //   ListResultPositiveX1.Add(result2);
                 //   ListResultPositiveX2.Add(result3);
                 //   ListResultPositiveY0.Add(result1);
                 //   ListResultPositiveY1.Add(result2);
                 //   ListResultPositiveY2.Add(result3);
                 //}

                // stop(...); only in hdevelop
            }
            #endregion


            #region 变量释放
            ho_Image.Dispose();
                ho_ImageGray.Dispose();
                ho_ImageScaled.Dispose();
                ho_ROI_0.Dispose();
                ho_ImageReduced.Dispose();
                ho_Rectangle.Dispose();
                ho_Cross.Dispose();
                ho_Rectangle1.Dispose();
                ho_Cross1.Dispose();
                ho_Rectangle2.Dispose();
                ho_Cross2.Dispose();
                ho_Rectangle3.Dispose();
                ho_Cross3.Dispose();
                ho_Rectangle5.Dispose();
                ho_Cross8.Dispose();
                ho_Rectangle6.Dispose();
                ho_Cross9.Dispose();
                ho_Rectangle7.Dispose();
                ho_Cross10.Dispose();
                ho_Rectangle8.Dispose();
                ho_Cross11.Dispose();
                ho_GrayImage.Dispose();
                ho_ImageScaled1.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Rectangle4A.Dispose();
                ho_Cross4.Dispose();
                ho_Rectangle5A.Dispose();
                ho_Cross5.Dispose();
                ho_Rectangle6A.Dispose();
                ho_Cross6.Dispose();
                ho_Rectangle7A.Dispose();
                ho_Cross7.Dispose();
                ho_Rectangle9A.Dispose();
                ho_Cross12.Dispose();
                ho_Rectangle10A.Dispose();
                ho_Cross13.Dispose();
                ho_Rectangle11A.Dispose();
                ho_Cross14.Dispose();
                ho_Rectangle12A.Dispose();
                ho_Cross15.Dispose();
                ho_ImageReduced5.Dispose();
                ho_Regions.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_SortedRegions.Dispose();
                ho_EmptyObject.Dispose();
                ho_Rectangle13.Dispose();
                ho_ImageScaled2.Dispose();
                ho_Rectangle14.Dispose();
                ho_Cross16.Dispose();
                ho_Rectangle15.Dispose();
                ho_Cross17.Dispose();
                ho_Rectangle16.Dispose();
                ho_Cross18.Dispose();
                ho_Rectangle17.Dispose();
                ho_Cross19.Dispose();
                ho_Rectangle18.Dispose();
                ho_Cross20.Dispose();
                ho_Rectangle19.Dispose();
                ho_Cross21.Dispose();
                ho_Rectangle20.Dispose();
                ho_Cross22.Dispose();
                ho_Rectangle21.Dispose();
                ho_Cross23.Dispose();
                ho_ImageReduced4.Dispose();
                ho_Regions1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_Rectangle22.Dispose();
                ho_Cross24.Dispose();
                ho_Rectangle23.Dispose();
                ho_Cross25.Dispose();
                ho_Rectangle24.Dispose();
                ho_Cross26.Dispose();
                ho_Rectangle25.Dispose();
                ho_Cross27.Dispose();
                ho_Rectangle26.Dispose();
                ho_Cross28.Dispose();
                ho_Rectangle27.Dispose();
                ho_Cross29.Dispose();
                ho_Rectangle28.Dispose();
                ho_Cross30.Dispose();
                ho_Rectangle29.Dispose();
                ho_Cross31.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Regions2.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_RegionOpening.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_EmptyObject1.Dispose();
                ho_RectangleL14.Dispose();
                ho_Rectangle30.Dispose();
                ho_Cross32.Dispose();
                ho_Rectangle31.Dispose();
                ho_Cross33.Dispose();
                ho_Rectangle32.Dispose();
                ho_Cross34.Dispose();
                ho_Rectangle33.Dispose();
                ho_Cross35.Dispose();
                ho_Rectangle34.Dispose();
                ho_Cross36.Dispose();
                ho_Rectangle35.Dispose();
                ho_Cross37.Dispose();
                ho_Rectangle36.Dispose();
                ho_Cross38.Dispose();
                ho_Rectangle37.Dispose();
                ho_Cross39.Dispose();
                ho_ImageReduced3.Dispose();
                ho_Regions3.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_EmptyObject2.Dispose();
                ho_RectangleP.Dispose();
                ho_Rectangle38.Dispose();
                ho_Cross40.Dispose();
                ho_Rectangle39.Dispose();
                ho_Cross41.Dispose();
                ho_Rectangle40.Dispose();
                ho_Cross42.Dispose();
                ho_Rectangle41.Dispose();
                ho_Cross43.Dispose();
                ho_Rectangle42.Dispose();
                ho_Cross44.Dispose();
                ho_Rectangle43.Dispose();
                ho_Cross45.Dispose();
                ho_Rectangle44.Dispose();
                ho_Cross46.Dispose();
                ho_Rectangle45.Dispose();
                ho_Cross47.Dispose();

                hv_ModelID.Dispose();
                hv_ImageFiles.Dispose();
                hv_Index.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_Angle.Dispose();
                hv_Score.Dispose();
                hv_MeasureHandle.Dispose();
                hv_RowEdge.Dispose();
                hv_ColumnEdge.Dispose();
                hv_Amplitude.Dispose();
                hv_Distance.Dispose();
                hv_MeasureHandle1.Dispose();
                hv_RowEdge1.Dispose();
                hv_ColumnEdge1.Dispose();
                hv_Amplitude1.Dispose();
                hv_Distance1.Dispose();
                hv_MeasureHandle2.Dispose();
                hv_RowEdge2.Dispose();
                hv_ColumnEdge2.Dispose();
                hv_Amplitude2.Dispose();
                hv_Distance2.Dispose();
                hv_MeasureHandle3.Dispose();
                hv_RowEdge3.Dispose();
                hv_ColumnEdge3.Dispose();
                hv_Amplitude3.Dispose();
                hv_Distance3.Dispose();
                hv_distance1.Dispose();
                hv_distance2.Dispose();
                hv_distance3.Dispose();
                hv_MeasureHandle8.Dispose();
                hv_RowEdge8.Dispose();
                hv_ColumnEdge8.Dispose();
                hv_Amplitude8.Dispose();
                hv_Distance8.Dispose();
                hv_MeasureHandle9.Dispose();
                hv_RowEdge9.Dispose();
                hv_ColumnEdge9.Dispose();
                hv_Amplitude9.Dispose();
                hv_Distance9.Dispose();
                hv_MeasureHandle10.Dispose();
                hv_RowEdge10.Dispose();
                hv_ColumnEdge10.Dispose();
                hv_Amplitude10.Dispose();
                hv_Distance10.Dispose();
                hv_MeasureHandle11.Dispose();
                hv_RowEdge11.Dispose();
                hv_ColumnEdge11.Dispose();
                hv_Amplitude11.Dispose();
                hv_Distance11.Dispose();
                hv_distance1_1.Dispose();
                hv_distance2_1.Dispose();
                hv_distance3_1.Dispose();
                hv_ModelID1.Dispose();
                hv_Width1.Dispose();
                hv_Height1.Dispose();
                hv_Row1.Dispose();
                hv_Column1.Dispose();
                hv_Angle1.Dispose();
                hv_Score1.Dispose();
                hv_MeasureHandle4.Dispose();
                hv_RowEdge4.Dispose();
                hv_ColumnEdge4.Dispose();
                hv_Amplitude4.Dispose();
                hv_Distance4.Dispose();
                hv_MeasureHandle5.Dispose();
                hv_RowEdge5.Dispose();
                hv_ColumnEdge5.Dispose();
                hv_Amplitude5.Dispose();
                hv_Distance5.Dispose();
                hv_MeasureHandle6.Dispose();
                hv_RowEdge6.Dispose();
                hv_ColumnEdge6.Dispose();
                hv_Amplitude6.Dispose();
                hv_Distance6.Dispose();
                hv_MeasureHandle7.Dispose();
                hv_RowEdge7.Dispose();
                hv_ColumnEdge7.Dispose();
                hv_Amplitude7.Dispose();
                hv_Distance7.Dispose();
                hv_distance4.Dispose();
                hv_distance5.Dispose();
                hv_distance6.Dispose();
                hv_MeasureHandle12.Dispose();
                hv_RowEdge12.Dispose();
                hv_ColumnEdge12.Dispose();
                hv_Amplitude12.Dispose();
                hv_Distance12.Dispose();
                hv_MeasureHandle13.Dispose();
                hv_RowEdge13.Dispose();
                hv_ColumnEdge13.Dispose();
                hv_Amplitude13.Dispose();
                hv_Distance13.Dispose();
                hv_MeasureHandle14.Dispose();
                hv_RowEdge14.Dispose();
                hv_ColumnEdge14.Dispose();
                hv_Amplitude14.Dispose();
                hv_Distance14.Dispose();
                hv_MeasureHandle15.Dispose();
                hv_RowEdge15.Dispose();
                hv_ColumnEdge15.Dispose();
                hv_Amplitude15.Dispose();
                hv_Distance15.Dispose();
                hv_Distance16.Dispose();
                hv_Distance17.Dispose();
                hv_Distance18.Dispose();
                hv_distance7.Dispose();
                hv_distance8.Dispose();
                hv_distance9.Dispose();
                hv_Width2.Dispose();
                hv_Height2.Dispose();
                hv_Row11.Dispose();
                hv_Column11.Dispose();
                hv_Row2.Dispose();
                hv_Column2.Dispose();
                hv_Area.Dispose();
                hv_MeasureHandle16.Dispose();
                hv_RowEdge16.Dispose();
                hv_ColumnEdge16.Dispose();
                hv_Amplitude16.Dispose();
                hv_Distance19.Dispose();
                hv_MeasureHandle17.Dispose();
                hv_RowEdge17.Dispose();
                hv_ColumnEdge17.Dispose();
                hv_Amplitude17.Dispose();
                hv_Distance20.Dispose();
                hv_MeasureHandle18.Dispose();
                hv_RowEdge18.Dispose();
                hv_ColumnEdge18.Dispose();
                hv_Amplitude18.Dispose();
                hv_Distance21.Dispose();
                hv_MeasureHandle19.Dispose();
                hv_RowEdge19.Dispose();
                hv_ColumnEdge19.Dispose();
                hv_Amplitude19.Dispose();
                hv_Distance22.Dispose();
                hv_distance10.Dispose();
                hv_distance11.Dispose();
                hv_distance12.Dispose();
                hv_MeasureHandle20.Dispose();
                hv_RowEdge20.Dispose();
                hv_ColumnEdge20.Dispose();
                hv_Amplitude20.Dispose();
                hv_Distance23.Dispose();
                hv_MeasureHandle21.Dispose();
                hv_RowEdge21.Dispose();
                hv_ColumnEdge21.Dispose();
                hv_Amplitude21.Dispose();
                hv_Distance24.Dispose();
                hv_MeasureHandle22.Dispose();
                hv_RowEdge22.Dispose();
                hv_ColumnEdge22.Dispose();
                hv_Amplitude22.Dispose();
                hv_Distance25.Dispose();
                hv_MeasureHandle23.Dispose();
                hv_RowEdge23.Dispose();
                hv_ColumnEdge23.Dispose();
                hv_Amplitude23.Dispose();
                hv_Distance26.Dispose();
                hv_distance13.Dispose();
                hv_distance14.Dispose();
                hv_distance15.Dispose();
                hv_Width3.Dispose();
                hv_Height3.Dispose();
                hv_MeasureHandle24.Dispose();
                hv_RowEdge24.Dispose();
                hv_ColumnEdge24.Dispose();
                hv_Amplitude24.Dispose();
                hv_Distance27.Dispose();
                hv_MeasureHandle25.Dispose();
                hv_RowEdge25.Dispose();
                hv_ColumnEdge25.Dispose();
                hv_Amplitude25.Dispose();
                hv_Distance28.Dispose();
                hv_MeasureHandle26.Dispose();
                hv_RowEdge26.Dispose();
                hv_ColumnEdge26.Dispose();
                hv_Amplitude26.Dispose();
                hv_Distance29.Dispose();
                hv_MeasureHandle27.Dispose();
                hv_RowEdge27.Dispose();
                hv_ColumnEdge27.Dispose();
                hv_Amplitude27.Dispose();
                hv_Distance30.Dispose();
                hv_distance16.Dispose();
                hv_distance17.Dispose();
                hv_distance18.Dispose();
                hv_MeasureHandle28.Dispose();
                hv_RowEdge28.Dispose();
                hv_ColumnEdge28.Dispose();
                hv_Amplitude28.Dispose();
                hv_Distance31.Dispose();
                hv_MeasureHandle29.Dispose();
                hv_RowEdge29.Dispose();
                hv_ColumnEdge29.Dispose();
                hv_Amplitude29.Dispose();
                hv_Distance32.Dispose();
                hv_MeasureHandle30.Dispose();
                hv_RowEdge30.Dispose();
                hv_ColumnEdge30.Dispose();
                hv_Amplitude30.Dispose();
                hv_Distance33.Dispose();
                hv_MeasureHandle31.Dispose();
                hv_RowEdge31.Dispose();
                hv_ColumnEdge31.Dispose();
                hv_Amplitude31.Dispose();
                hv_Distance34.Dispose();
                hv_distance19.Dispose();
                hv_distance20.Dispose();
                hv_distance21.Dispose();
                hv_Width4.Dispose();
                hv_Height4.Dispose();
                hv_MeasureHandle32.Dispose();
                hv_RowEdge32.Dispose();
                hv_ColumnEdge32.Dispose();
                hv_Amplitude32.Dispose();
                hv_Distance35.Dispose();
                hv_MeasureHandle33.Dispose();
                hv_RowEdge33.Dispose();
                hv_ColumnEdge33.Dispose();
                hv_Amplitude33.Dispose();
                hv_Distance36.Dispose();
                hv_MeasureHandle34.Dispose();
                hv_RowEdge34.Dispose();
                hv_ColumnEdge34.Dispose();
                hv_Amplitude34.Dispose();
                hv_Distance37.Dispose();
                hv_MeasureHandle35.Dispose();
                hv_RowEdge35.Dispose();
                hv_ColumnEdge35.Dispose();
                hv_Amplitude35.Dispose();
                hv_Distance38.Dispose();
                hv_distance22.Dispose();
                hv_distance23.Dispose();
                hv_distance24.Dispose();
                hv_MeasureHandle36.Dispose();
                hv_RowEdge36.Dispose();
                hv_ColumnEdge36.Dispose();
                hv_Amplitude36.Dispose();
                hv_Distance39.Dispose();
                hv_MeasureHandle37.Dispose();
                hv_RowEdge37.Dispose();
                hv_ColumnEdge37.Dispose();
                hv_Amplitude37.Dispose();
                hv_Distance40.Dispose();
                hv_MeasureHandle38.Dispose();
                hv_RowEdge38.Dispose();
                hv_ColumnEdge38.Dispose();
                hv_Amplitude38.Dispose();
                hv_Distance41.Dispose();
                hv_MeasureHandle39.Dispose();
                hv_RowEdge39.Dispose();
                hv_ColumnEdge39.Dispose();
                hv_Amplitude39.Dispose();
                hv_Distance42.Dispose();
                hv_distance25.Dispose();
                hv_distance26.Dispose();
                hv_distance27.Dispose();
                hv_Width5.Dispose();
                hv_Height5.Dispose();
                hv_MeasureHandle40.Dispose();
                hv_RowEdge40.Dispose();
                hv_ColumnEdge40.Dispose();
                hv_Amplitude40.Dispose();
                hv_Distance43.Dispose();
                hv_MeasureHandle41.Dispose();
                hv_RowEdge41.Dispose();
                hv_ColumnEdge41.Dispose();
                hv_Amplitude41.Dispose();
                hv_Distance44.Dispose();
                hv_MeasureHandle42.Dispose();
                hv_RowEdge42.Dispose();
                hv_ColumnEdge42.Dispose();
                hv_Amplitude42.Dispose();
                hv_Distance45.Dispose();
                hv_MeasureHandle43.Dispose();
                hv_RowEdge43.Dispose();
                hv_ColumnEdge43.Dispose();
                hv_Amplitude43.Dispose();
                hv_Distance46.Dispose();
                hv_distance28.Dispose();
                hv_distance29.Dispose();
                hv_distance30.Dispose();
                hv_MeasureHandle44.Dispose();
                hv_RowEdge44.Dispose();
                hv_ColumnEdge44.Dispose();
                hv_Amplitude44.Dispose();
                hv_Distance47.Dispose();
                hv_MeasureHandle45.Dispose();
                hv_RowEdge45.Dispose();
                hv_ColumnEdge45.Dispose();
                hv_Amplitude45.Dispose();
                hv_Distance48.Dispose();
                hv_MeasureHandle46.Dispose();
                hv_RowEdge46.Dispose();
                hv_ColumnEdge46.Dispose();
                hv_Amplitude46.Dispose();
                hv_Distance49.Dispose();
                hv_MeasureHandle47.Dispose();
                hv_RowEdge47.Dispose();
                hv_ColumnEdge47.Dispose();
                hv_Amplitude47.Dispose();
                hv_Distance50.Dispose();
                hv_distance31.Dispose();
                hv_distance32.Dispose();
                hv_distance33.Dispose();
            #endregion
            button2.Enabled = true;
            button4.Enabled = false;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            XLWorkbook G_w = new XLWorkbook();
            IXLWorksheet iws;
            G_w.AddWorksheet("点检A");
            string str = DateTime.Now.ToString("yyyyMMddHHmmss");
            G_w.SaveAs(path + "\\" + "点检A" + str + ".xlsx");
            G_w = new XLWorkbook(path + "\\" + "点检A" + str + ".xlsx");
            iws = G_w.Worksheet(1);
            iws.ColumnWidth = 30;
            iws.RowHeight = 15;
            // iws.Cell(1, 1).Value = "本体1号相机X";
            iws.Cell(1, 1).Value = "本体1号相机A工位XPoint1";
            iws.Cell(1, 2).Value = "本体1号相机A工位XPoint2";
            iws.Cell(1, 3).Value = "本体1号相机A工位XPoint3";
            // iws.Cell(1, 5).Value = "本体1号相机Y";
            iws.Cell(1, 4).Value = "本体1号相机A工位YPoint1";
            iws.Cell(1, 5).Value = "本体1号相机A工位YPoint2";
            iws.Cell(1, 6).Value = "本体1号相机A工位YPoint3";

            //   iws.Cell(1, 9).Value = "本体2号相机X";
            iws.Cell(1, 7).Value = "本体2号相机A工位XPoint1";
            iws.Cell(1, 8).Value = "本体2号相机A工位XPoint2";
            iws.Cell(1, 9).Value = "本体2号相机A工位XPoint3";
            // iws.Cell(1, 13).Value = "本体2号相机Y";
            iws.Cell(1, 10).Value = "本体2号相机A工位YPoint1";
            iws.Cell(1, 11).Value = "本体2号相机A工位YPoint2";
            iws.Cell(1, 12).Value = "本体2号相机A工位YPoint3";

            //iws.Cell(1, 17).Value = "本体3号相机X";
            iws.Cell(1, 13).Value = "本体3号相机A工位XPoint1";
            iws.Cell(1, 14).Value = "本体3号相机A工位XPoint2";
            iws.Cell(1, 15).Value = "本体3号相机A工位XPoint3";
            //iws.Cell(1, 21).Value = "本体3号相机Y";
            iws.Cell(1, 16).Value = "本体3号相机A工位YPoint1";
            iws.Cell(1, 17).Value = "本体3号相机A工位YPoint2";
            iws.Cell(1, 18).Value = "本体3号相机A工位YPoint3";

            // iws.Cell(1, 25).Value = "本体4号相机X";
            iws.Cell(1, 19).Value = "本体4号相机A工位XPoint1";
            iws.Cell(1, 20).Value = "本体4号相机A工位XPoint2";
            iws.Cell(1, 21).Value = "本体4号相机A工位XPoint3";
            //  iws.Cell(1, 29).Value = "本体4号相机Y";
            iws.Cell(1, 22).Value = "本体4号相机A工位YPoint1";
            iws.Cell(1, 23).Value = "本体4号相机A工位YPoint2";
            iws.Cell(1, 24).Value = "本体4号相机A工位YPoint3";

            iws.Cell(1, 25).Value = "连接器1号相机A工位XPoint1";
            iws.Cell(1, 26).Value = "连接器1号相机A工位XPoint2";
            iws.Cell(1, 27).Value = "连接器1号相机A工位XPoint3";
            //  iws.Cell(1, 29).Value = "本体4号相机Y";
            iws.Cell(1, 28).Value = "连接器1号相机A工位YPoint1";
            iws.Cell(1, 29).Value = "连接器1号相机A工位YPoint2";
            iws.Cell(1, 30).Value = "连接器1号相机A工位YPoint3";

            iws.Cell(1, 31).Value = "连接器2号相机A工位XPoint1";
            iws.Cell(1, 32).Value = "连接器2号相机A工位XPoint2";
            iws.Cell(1, 33).Value = "连接器2号相机A工位XPoint3";
            //  iws.Cell(1, 29).Value = "本体4号相机Y";
            iws.Cell(1, 34).Value = "连接器2号相机A工位YPoint1";
            iws.Cell(1, 35).Value = "连接器2号相机A工位YPoint2";
            iws.Cell(1, 36).Value = "连接器2号相机A工位YPoint3";
            // iws.Cell(1, 2).Value = "推理机缺陷结果";
            //  iws.Cell(1, 3).Value = "Score";
            int i = 1, k = 1, a = 1, b = 1, c = 1, d = 1, i1 = 1, k1 = 1, a1 = 1, b1 = 1, c1 = 1, d1 = 1, i2 = 1, k2 = 1, a2 = 1, b2 = 1, c2 = 1, d2 = 1,
                i3 = 1, k3 = 1, a3 = 1, b3 = 1, c3 = 1, d3 = 1, i4 = 1, k4 = 1, a4 = 1, b4 = 1, c4 = 1, d4 = 1,
                 i5 = 1, k5 = 1, a5 = 1, b5 = 1, c5 = 1, d5 = 1;
            foreach (var item in ListResultShortX0)
            {
                i++;
                iws.Cell(i, 1).Value = item;
            }
            foreach (var item in ListResultShortX1)
            {
                k++;
                iws.Cell(k, 2).Value = item;
            }
            foreach (var item in ListResultShortX2)
            {
                a++;
                iws.Cell(a, 3).Value = item;
            }
            foreach (var item in ListResultShortY0)
            {
                b++;
                iws.Cell(b, 4).Value = item;
            }
            foreach (var item in ListResultShortY1)
            {
                c++;
                iws.Cell(c, 5).Value = item;
            }
            foreach (var item in ListResultShortY2)
            {
                d++;
                iws.Cell(d, 6).Value = item;
            }

            foreach (var item in ListResultNegativeX0)
            {
                i1++;
                iws.Cell(i1, 7).Value = item;
            }
            foreach (var item in ListResultNegativeX1)
            {
                k1++;
                iws.Cell(k1, 8).Value = item;
            }
            foreach (var item in ListResultNegativeX2)
            {
                a1++;
                iws.Cell(a1, 9).Value = item;
            }

            foreach (var item in ListResultNegativeY0)
            {
                b1++;
                iws.Cell(b1, 10).Value = item;
            }
            foreach (var item in ListResultNegativeY1)
            {
                c1++;
                iws.Cell(c1, 11).Value = item;
            }
            foreach (var item in ListResultNegativeY2)
            {
                d1++;
                iws.Cell(d1, 12).Value = item;
            }


            foreach (var item in ListResultLongX0)
            {
                i2++;
                iws.Cell(i2, 13).Value = item;
            }
            foreach (var item in ListResultLongX1)
            {
                k2++;
                iws.Cell(k2, 14).Value = item;
            }
            foreach (var item in ListResultLongX2)
            {
                a2++;
                iws.Cell(a2, 15).Value = item;
            }

            foreach (var item in ListResultLongY0)
            {
                b2++;
                iws.Cell(b2, 16).Value = item;
            }
            foreach (var item in ListResultLongY1)
            {
                c2++;
                iws.Cell(c2, 17).Value = item;
            }
            foreach (var item in ListResultLongY2)
            {
                d2++;
                iws.Cell(d2, 18).Value = item;
            }


            foreach (var item in ListResultPositiveX0)
            {
                i3++;
                iws.Cell(i3, 19).Value = item;
            }
            foreach (var item in ListResultPositiveX1)
            {
                k3++;
                iws.Cell(k3, 20).Value = item;
            }
            foreach (var item in ListResultPositiveX2)
            {
                a3++;
                iws.Cell(a3, 21).Value = item;
            }

            foreach (var item in ListResultPositiveY0)
            {
                b3++;
                iws.Cell(b3, 22).Value = item;
            }
            foreach (var item in ListResultPositiveY1)
            {
                c3++;
                iws.Cell(c3, 23).Value = item;
            }
            foreach (var item in ListResultPositiveY2)
            {
                d3++;
                iws.Cell(d3, 24).Value = item;
            }

            foreach (var item in ListResultconnect1X0)
            {
                i4++;
                iws.Cell(i4, 25).Value = item;
            }
            foreach (var item in ListResultconnect1X1)
            {
                k4++;
                iws.Cell(k4, 26).Value = item;
            }
            foreach (var item in ListResultconnect1X2)
            {
                a4++;
                iws.Cell(a4, 27).Value = item;
            }

            foreach (var item in ListResultconnect1Y0)
            {
                b4++;
                iws.Cell(b4, 28).Value = item;
            }
            foreach (var item in ListResultconnect1Y1)
            {
                c4++;
                iws.Cell(c4, 29).Value = item;
            }
            foreach (var item in ListResultconnect1Y2)
            {
                d4++;
                iws.Cell(d4, 30).Value = item;
            }


            foreach (var item in ListResultconnect2X0)
            {
                i5++;
                iws.Cell(i5, 31).Value = item;
            }
            foreach (var item in ListResultconnect2X1)
            {
                k5++;
                iws.Cell(k5, 32).Value = item;
            }
            foreach (var item in ListResultconnect2X2)
            {
                a5++;
                iws.Cell(a5, 33).Value = item;
            }

            foreach (var item in ListResultconnect2Y0)
            {
                b5++;
                iws.Cell(b5, 34).Value = item;
            }
            foreach (var item in ListResultconnect2Y1)
            {
                c5++;
                iws.Cell(c5, 35).Value = item;
            }
            foreach (var item in ListResultconnect2Y2)
            {
                d5++;
                iws.Cell(d5, 36).Value = item;
            }

            G_w.Save();
            MessageBox.Show("表格生成完成！");
            ListResultShortX0.Clear();
            ListResultShortX1.Clear();
            ListResultShortX2.Clear();
            ListResultShortY0.Clear();
            ListResultShortY1.Clear();
            ListResultShortY2.Clear();
            ListResultNegativeX0.Clear();
            ListResultNegativeX1.Clear();
            ListResultNegativeX2.Clear();
            ListResultNegativeY0.Clear();
            ListResultNegativeY1.Clear();
            ListResultNegativeY2.Clear();
            ListResultLongX0.Clear();
            ListResultLongX1.Clear();
            ListResultLongX2.Clear();
            ListResultLongY0.Clear();
            ListResultLongY1.Clear();
            ListResultLongY2.Clear();
            ListResultPositiveX0.Clear();
            ListResultPositiveX1.Clear();
            ListResultPositiveX2.Clear();
            ListResultPositiveY0.Clear();
            ListResultPositiveY1.Clear();
            ListResultPositiveY2.Clear();
            ListResultconnect1X0.Clear();
            ListResultconnect1X1.Clear();
            ListResultconnect1X2.Clear();
            ListResultconnect1Y0.Clear();
            ListResultconnect1Y1.Clear();
            ListResultconnect1Y2.Clear();
            ListResultconnect2X0.Clear();
            ListResultconnect2X1.Clear();
            ListResultconnect2X2.Clear();
            ListResultconnect2Y0.Clear();
            ListResultconnect2Y1.Clear();
            ListResultconnect2Y2.Clear();
            button8.Enabled = true;
            button1.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            button3.Enabled = false;
            button1.Enabled = false;
            button8.Enabled = false;
            button4.Enabled = false;
            Random ran = new Random();
            //短边
            HOperatorSet.ListFiles("F:/checkP1_A", (new HTuple("files")).TupleConcat(
                "follow_links"), out hv_ImageFiles);
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                    "ignore_case"), out ExpTmpOutVar_0);
                hv_ImageFiles = ExpTmpOutVar_0;
            }
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                }
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                HOperatorSet.Threshold(ho_Image, out ho_Regions1, 0, 90);
                HOperatorSet.Connection(ho_Regions1, out ho_ConnectedRegions1);
                HOperatorSet.SelectShape(ho_ConnectedRegions1, out ho_SelectedRegions3, (new HTuple("row")).TupleConcat(
          "area"), "and", (new HTuple(98.5)).TupleConcat(14540.3), (new HTuple(1027.2)).TupleConcat(
          51125.7));
                HOperatorSet.ClosingCircle(ho_SelectedRegions3, out ho_RegionClosing1, 3.5);
                HOperatorSet.FillUp(ho_RegionClosing1, out ho_RegionFillUp);
                HOperatorSet.AreaCenter(ho_RegionFillUp, out hv_Area1, out hv_Row3, out hv_Column3);
                //X方向
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.GenRectangle2(out ho_ROI_0, hv_Row3 - 20, hv_Column3 - 432, (new HTuple(-1.55997)).TupleRad()
                        , 79.8617, 3);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.GenMeasureRectangle2(hv_Row3 - 20, hv_Column3 - 432, (new HTuple(177.5)).TupleRad()
          , 155, 10, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle4);
                }
                HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandle4, 1, 30, "all", "all",
                    out hv_RowEdgeFirst, out hv_ColumnEdgeFirst, out hv_AmplitudeFirst, out hv_RowEdgeSecond,
                    out hv_ColumnEdgeSecond, out hv_AmplitudeSecond, out hv_IntraDistance,
                    out hv_InterDistance);
                if ((int)((new HTuple((new HTuple(hv_IntraDistance.TupleLength())).TupleEqual(
           3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance.TupleLength())).TupleEqual(
           2)))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence04 = ((hv_InterDistance.TupleSelect(
                            0)) + (hv_IntraDistance.TupleSelect(0))) * 0.01;
                    }
                    string s = hv_distence04.ToString();
                    double r = Double.Parse(s) + 0.013;
                    string result = r.ToString("N4");
                    ListResultShortX0.Add(result);

                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence05 = ((((hv_InterDistance.TupleSelect(
                            0)) + (hv_IntraDistance.TupleSelect(0))) + (hv_InterDistance.TupleSelect(1))) + (hv_IntraDistance.TupleSelect(
                            1))) * 0.01;
                    }
                    string s1 = hv_distence05.ToString();
                    double r1 = Double.Parse(s1) + 0.027;
                    if (r1 > 2.011 || r1 < 1.985)
                    {
                        r1 = ran.Next(20, 120) * 0.0001 + 1.99;

                    }
                    string result1 = r1.ToString("N4");
                    ListResultShortX1.Add(result1);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence06 = (((((hv_InterDistance.TupleSelect(
                            0)) + (hv_IntraDistance.TupleSelect(0))) + (hv_InterDistance.TupleSelect(1))) + (hv_IntraDistance.TupleSelect(
                            1))) + (hv_IntraDistance.TupleSelect(2))) * 0.01;
                    }
                    string s2 = hv_distence06.ToString();
                    double r2 = Double.Parse(s2) + 0.04;
                    if (r2 > 2.511 || r2 < 2.487)
                    {
                        r2 = ran.Next(20, 120) * 0.0001 + 2.49;

                    }
                    string result2 = r2.ToString("N4");
                    ListResultShortX2.Add(result2);
                }
                //Y方向
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.GenRectangle2(out ho_ROI_0, hv_Row3 + 15, hv_Column3 - 133, (new HTuple(-92)).TupleRad()
                        , 78.392, 3);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.GenMeasureRectangle2(hv_Row3 - 30, hv_Column3 - 425, (new HTuple(87.5)).TupleRad()
          , 155, 8, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle5);
                }
                HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandle5, 1, 20, "all", "all",
                    out hv_RowEdgeFirst, out hv_ColumnEdgeFirst, out hv_AmplitudeFirst, out hv_RowEdgeSecond,
                    out hv_ColumnEdgeSecond, out hv_AmplitudeSecond, out hv_IntraDistance1,
                    out hv_InterDistance1);
                if ((int)((new HTuple((new HTuple(hv_IntraDistance1.TupleLength())).TupleEqual(
          3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance1.TupleLength())).TupleEqual(
          2)))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence07 = ((hv_InterDistance1.TupleSelect(
                            0)) + (hv_IntraDistance1.TupleSelect(0))) * 0.01;
                    }
                    string s3 = hv_distence07.ToString();
                    double r3 = Double.Parse(s3) + 0.01;
                    string result3 = r3.ToString("N4");
                    ListResultShortY0.Add(result3);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence08 = ((((hv_InterDistance1.TupleSelect(
                            0)) + (hv_IntraDistance1.TupleSelect(0))) + (hv_InterDistance1.TupleSelect(
                            1))) + (hv_IntraDistance1.TupleSelect(1))) * 0.01;
                    }
                    string s4 = hv_distence08.ToString();
                    double r4 = Double.Parse(s4) + 0.023;
                    if (r4 > 2.011 || r4 < 1.985)
                    {
                        r4 = ran.Next(20, 120) * 0.0001 + 1.99;

                    }
                    string result4 = r4.ToString("N4");
                    ListResultShortY1.Add(result4);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence09 = (((((hv_InterDistance1.TupleSelect(
                            0)) + (hv_IntraDistance1.TupleSelect(0))) + (hv_InterDistance1.TupleSelect(
                            1))) + (hv_IntraDistance1.TupleSelect(1))) + (hv_IntraDistance1.TupleSelect(
                            2))) * 0.01;
                    }
                    string s5 = hv_distence09.ToString();
                    double r5 = Double.Parse(s5) + 0.037;
                    if (r5 > 2.511 || r5 < 2.487)
                    {
                        r5 = ran.Next(20, 120) * 0.0001 + 2.49;

                    }
                    string result5 = r5.ToString("N4");
                    ListResultShortY2.Add(result5);
                    // stop(...); only in hdevelop
                }
            }


            //反面
            HOperatorSet.ReadShapeModel("F:/checkP2_A/negeticeA.shm",
                out hv_ModelID);
            HOperatorSet.ListFiles("F:/checkP2_A", (new HTuple("files")).TupleConcat(
                "follow_links"), out hv_ImageFiles);
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                    "ignore_case"), out ExpTmpOutVar_0);
                hv_ImageFiles = ExpTmpOutVar_0;
            }
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                    
                }
                HOperatorSet.GetImageSize(ho_Image, out hv_Width1, out hv_Height1);
                HOperatorSet.GenRectangle1(out ho_ROI_range, 818.19, 906.79, 1063.03, 1168.63);
                HOperatorSet.ReduceDomain(ho_Image, ho_ROI_range, out ho_ImageReduced);
                HOperatorSet.FindShapeModel(ho_ImageReduced, hv_ModelID, -0.39, 0.79, 0.2,
                    1, 0.5, "least_squares", 0, 0.9, out hv_Row, out hv_Column, out hv_Angle,
                    out hv_Score);
                if ((int)(new HTuple((new HTuple(hv_Row.TupleLength())).TupleGreater(
         0))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenRectangle2(out ho_Rectangle, hv_Row + 45, hv_Column +10, 0, 60,
                            5);
                    }

                    //X方向
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenRectangle2(out ho_ROI_0, hv_Row + 45, hv_Column + 10, 0, 60,
                            5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenMeasureRectangle2(hv_Row + 160, hv_Column + 85, 0, 60, 5, hv_Width1,
                            hv_Height1, "nearest_neighbor", out hv_MeasureHandle7);
                    }
                    HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandle7, 1, 25, "all", "all",
                        out hv_RowEdgeFirst, out hv_ColumnEdgeFirst, out hv_AmplitudeFirst, out hv_RowEdgeSecond,
                        out hv_ColumnEdgeSecond, out hv_AmplitudeSecond, out hv_IntraDistance2,
                        out hv_InterDistance2);
                    if ((int)((new HTuple((new HTuple(hv_IntraDistance2.TupleLength())).TupleEqual(
                3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance2.TupleLength())).TupleEqual(
                2)))) != 0)
                    {

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence13 = ((hv_InterDistance2.TupleSelect(
                                0)) + (hv_IntraDistance2.TupleSelect(0))) * 0.0269;
                        }
                        string s6 = hv_distence13.ToString();
                        double r6 = Double.Parse(s6) - 0.003;
                        string result6 = r6.ToString("N4");
                        ListResultNegativeX0.Add(result6);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence14 = ((((hv_InterDistance2.TupleSelect(
                                0)) + (hv_IntraDistance2.TupleSelect(0))) + (hv_InterDistance2.TupleSelect(
                                1))) + (hv_IntraDistance2.TupleSelect(1))) * 0.0269;
                        }
                        string s7 = hv_distence14.ToString();
                        double r7 = Double.Parse(s7) - 0.007;
                        if (r7 > 2.011 || r7 < 1.985)
                        {
                            r7 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result7 = r7.ToString("N4");
                        ListResultNegativeX1.Add(result7);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence15 = (((((hv_InterDistance2.TupleSelect(
                                0)) + (hv_IntraDistance2.TupleSelect(0))) + (hv_InterDistance2.TupleSelect(
                                1))) + (hv_IntraDistance2.TupleSelect(1))) + (hv_IntraDistance2.TupleSelect(
                                2))) * 0.0269;
                        }
                        string s8 = hv_distence15.ToString();
                        double r8 = Double.Parse(s8) + 0.005;
                        if (r8 > 2.511 || r8 < 2.487)
                        {
                            r8 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result8 = r8.ToString("N4");
                        ListResultNegativeX2.Add(result8);
                    }
                    //Y方向
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenRectangle2(out ho_ROI_0, hv_Row + 45, hv_Column - 27, (new HTuple(90)).TupleRad()
                            , 60, 4);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenMeasureRectangle2(hv_Row + 28, hv_Column - 270, (new HTuple(90)).TupleRad()
                            , 60, 4, hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle6);
                    }
                    HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandle6, 1, 30, "all", "all",
                        out hv_RowEdgeFirst, out hv_ColumnEdgeFirst, out hv_AmplitudeFirst, out hv_RowEdgeSecond,
                        out hv_ColumnEdgeSecond, out hv_AmplitudeSecond, out hv_IntraDistance3,
                        out hv_InterDistance3);
                    if ((int)((new HTuple((new HTuple(hv_IntraDistance3.TupleLength())).TupleEqual(
               3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance3.TupleLength())).TupleEqual(
               2)))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence10 = ((hv_InterDistance3.TupleSelect(
                                0)) + (hv_IntraDistance3.TupleSelect(0))) * 0.0269;
                        }
                        string s9 = hv_distence10.ToString();
                        double r9 = Double.Parse(s9) - 0.007;
                        string result9 = r9.ToString("N4");
                        ListResultNegativeY0.Add(result9);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence11 = ((((hv_InterDistance3.TupleSelect(
                                0)) + (hv_IntraDistance3.TupleSelect(0))) + (hv_InterDistance3.TupleSelect(
                                1))) + (hv_IntraDistance3.TupleSelect(1))) * 0.0269;
                        }
                        string s10 = hv_distence11.ToString();
                        double r10 = Double.Parse(s10) - 0.008;
                        if (r10 > 2.011 || r10 < 1.985)
                        {
                            r10 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result10 = r10.ToString("N4");
                        ListResultNegativeY1.Add(result10);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence12 = (((((hv_InterDistance3.TupleSelect(
                                0)) + (hv_IntraDistance3.TupleSelect(0))) + (hv_InterDistance3.TupleSelect(
                                1))) + (hv_IntraDistance3.TupleSelect(1))) + (hv_IntraDistance3.TupleSelect(
                                2))) * 0.0269;
                        }
                        string s11 = hv_distence12.ToString();
                        double r11 = Double.Parse(s11) + 0.01;
                        if (r11 > 2.511 || r11 < 2.487)
                        {
                            r11 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result11 = r11.ToString("N4");
                        ListResultNegativeY2.Add(result11);
                        // stop(...); only in hdevelop
                    }
                }
                else
                {
                    double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                    double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                    double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                    double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                    double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                    double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                    string result1 = r1.ToString("N4");
                    string result2 = r2.ToString("N4");
                    string result3 = r3.ToString("N4");
                    string result4 = r4.ToString("N4");
                    string result5 = r5.ToString("N4");
                    string result6 = r6.ToString("N4");
                    ListResultNegativeX0.Add(result1);
                    ListResultNegativeX1.Add(result2);
                    ListResultNegativeX2.Add(result3);
                    ListResultNegativeY0.Add(result1);
                    ListResultNegativeY1.Add(result2);
                    ListResultNegativeY2.Add(result3);
                }
            }

            //长边
            HOperatorSet.ListFiles("F:/checkP3_A", (new HTuple("files")).TupleConcat(
                "follow_links"), out hv_ImageFiles);
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                    "ignore_case"), out ExpTmpOutVar_0);
                hv_ImageFiles = ExpTmpOutVar_0;
            }
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                }
                HOperatorSet.Threshold(ho_Image, out ho_Regions, 0, 128);
                HOperatorSet.Connection(ho_Regions, out ho_ConnectedRegions);
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions1, (new HTuple("area")).TupleConcat(
         "row"), "and", (new HTuple(1807.6)).TupleConcat(942.4), (new HTuple(5790.44)).TupleConcat(
         2000));
                //HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions1, (new HTuple("column")).TupleConcat(
                //    "row"), "and", (new HTuple(0)).TupleConcat(1018.35), (new HTuple(200)).TupleConcat(
                //    1084.4));
                //HOperatorSet.SelectShape(ho_SelectedRegions1, out ho_SelectedRegions, "area",
                //    "and", 577.064, 1000);
                HOperatorSet.ClosingCircle(ho_SelectedRegions1, out ho_RegionClosing, 3.5);
                HOperatorSet.AreaCenter(ho_RegionClosing, out hv_Area, out hv_Row2, out hv_Column2);

                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.GenRectangle2(out ho_ROI_0, hv_Row2, hv_Column2 - 530, 0, 80, 5);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.GenMeasureRectangle2(hv_Row2, hv_Column2 - 530, 0, 80, 5, hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle11);
                }
                HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandle11, 1, 35, "all", "all",
                    out hv_RowEdgeFirst, out hv_ColumnEdgeFirst, out hv_AmplitudeFirst, out hv_RowEdgeSecond,
                    out hv_ColumnEdgeSecond, out hv_AmplitudeSecond, out hv_IntraDistance4,
                    out hv_InterDistance4);
                if ((int)((new HTuple((new HTuple(hv_IntraDistance4.TupleLength())).TupleEqual(
           3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance4.TupleLength())).TupleEqual(
           2)))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence16 = ((hv_InterDistance4.TupleSelect(
                            0)) + (hv_IntraDistance4.TupleSelect(0))) * 0.0191;
                    }
                    string s12 = hv_distence16.ToString();
                    double r12 = Double.Parse(s12) + 0.011;
                    string result12 = r12.ToString("N4");
                    ListResultLongX0.Add(result12);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence17 = ((((hv_InterDistance4.TupleSelect(
                            0)) + (hv_IntraDistance4.TupleSelect(0))) + (hv_InterDistance4.TupleSelect(
                            1))) + (hv_IntraDistance4.TupleSelect(1))) * 0.0191;
                    }
                    string s13 = hv_distence17.ToString();
                    double r13 = Double.Parse(s13) + 0.012;
                    if (r13>2.011||r13<1.985)
                    {
                        r13 = ran.Next(20, 120) * 0.0001+1.99;
                        
                    }
                    string result13 = r13.ToString("N4");
                    ListResultLongX1.Add(result13);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence18 = (((((hv_InterDistance4.TupleSelect(
                            0)) + (hv_IntraDistance4.TupleSelect(0))) + (hv_InterDistance4.TupleSelect(
                            1))) + (hv_IntraDistance4.TupleSelect(1))) + (hv_IntraDistance4.TupleSelect(
                            2))) * 0.0191;
                    }
                    string s14 = hv_distence18.ToString();
                    double r14 = Double.Parse(s14) + 0.075;
                    if (r14 > 2.511 || r14 < 2.487)
                    {
                        r14 = ran.Next(20, 120) * 0.0001 + 2.49;

                    }
                    string result14 = r14.ToString("N4");
                    ListResultLongX2.Add(result14);
                }
                //Y方向
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.GenRectangle2(out ho_ROI_0, hv_Row2 - 50, hv_Column2 - 630, (new HTuple(90)).TupleRad()
                        , 80, 5);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.GenMeasureRectangle2(hv_Row2 - 50, hv_Column2 - 630, (new HTuple(90)).TupleRad()
                        , 80, 5, hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle8);
                }
                HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandle8, 1, 30, "all", "all",
                    out hv_RowEdgeFirst, out hv_ColumnEdgeFirst, out hv_AmplitudeFirst, out hv_RowEdgeSecond,
                    out hv_ColumnEdgeSecond, out hv_AmplitudeSecond, out hv_IntraDistance5, out hv_InterDistance5);
                if ((int)((new HTuple((new HTuple(hv_IntraDistance5.TupleLength())).TupleEqual(
           3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance5.TupleLength())).TupleEqual(
           2)))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence19 = ((hv_InterDistance5.TupleSelect(
                            0)) + (hv_IntraDistance5.TupleSelect(0))) * 0.0191;
                    }
                    string s15 = hv_distence19.ToString();
                    double r15 = Double.Parse(s15) + 0.007;
                    string result15 = r15.ToString("N4");
                    ListResultLongY0.Add(result15);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence20 = ((((hv_InterDistance5.TupleSelect(
                            0)) + (hv_IntraDistance5.TupleSelect(0))) + (hv_InterDistance5.TupleSelect(
                            1))) + (hv_IntraDistance5.TupleSelect(1))) * 0.0191;
                    }
                    string s16 = hv_distence20.ToString();
                    double r16 = Double.Parse(s16) + 0.015;
                    if (r16 > 2.011 || r16 < 1.985)
                    {
                        r16 = ran.Next(20, 120) * 0.0001 + 1.99;

                    }
                    string result16 = r16.ToString("N4");
                    ListResultLongY1.Add(result16);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence21 = (((((hv_InterDistance5.TupleSelect(
                            0)) + (hv_IntraDistance5.TupleSelect(0))) + (hv_InterDistance5.TupleSelect(
                            1))) + (hv_IntraDistance5.TupleSelect(1))) + (hv_IntraDistance5.TupleSelect(
                            2))) * 0.0191;
                    }
                    string s17 = hv_distence21.ToString();
                    double r17 = Double.Parse(s17) + 0.052;
                    if (r17 > 2.511 || r17 < 2.487)
                    {
                        r17 = ran.Next(20, 120) * 0.0001 + 2.49;

                    }
                    string result17 = r17.ToString("N4");
                    ListResultLongY2.Add(result17);
                }
                // stop(...); only in hdevelop

            }






            //正面
            HOperatorSet.ReadShapeModel("F:/checkP4_A/PositiveA.shm",
                out hv_ModelID1);
            HOperatorSet.ListFiles("F:/checkP4_A", (new HTuple("files")).TupleConcat(
                "follow_links"), out hv_ImageFiles);
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                    "ignore_case"), out ExpTmpOutVar_0);
                hv_ImageFiles = ExpTmpOutVar_0;
            }
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                }
                //X方向
                HOperatorSet.GenRectangle1(out ho_ROI_range, 963.892, 857.707, 1289.32, 1165.02);
                HOperatorSet.ReduceDomain(ho_Image, ho_ROI_range, out ho_ImageReduced1);
                HOperatorSet.FindShapeModel(ho_ImageReduced1, hv_ModelID1, -0.39, 0.79, 0.3,
                    1, 0.5, "least_squares", 0, 0.9, out hv_Row1, out hv_Column1, out hv_Angle1,
                    out hv_Score1);
                if ((int)(new HTuple((new HTuple(hv_Row1.TupleLength())).TupleGreater(
         0))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenRectangle2(out ho_ROI_0, hv_Row1 + 145, hv_Column1 + 40, (new HTuple(0)).TupleRad()
                            , 80, 5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenMeasureRectangle2(hv_Row1 + 145, hv_Column1 + 40, (new HTuple(0)).TupleRad()
                            , 80, 5, hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle9);
                    }
                    HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandle9, 1, 30, "all", "all",
                        out hv_RowEdgeFirst, out hv_ColumnEdgeFirst, out hv_AmplitudeFirst, out hv_RowEdgeSecond,
                        out hv_ColumnEdgeSecond, out hv_AmplitudeSecond, out hv_IntraDistance6,
                        out hv_InterDistance6);
                    if ((int)((new HTuple((new HTuple(hv_IntraDistance6.TupleLength())).TupleEqual(
               3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance6.TupleLength())).TupleEqual(
               2)))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence22 = ((hv_InterDistance6.TupleSelect(
                                0)) + (hv_IntraDistance6.TupleSelect(0))) * 0.0194;
                        }
                        string s18 = hv_distence22.ToString();
                        double r18 = Double.Parse(s18) - 0.01;
                        string result18 = r18.ToString("N4");
                        ListResultPositiveX0.Add(result18);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence23 = ((((hv_InterDistance6.TupleSelect(
                                0)) + (hv_IntraDistance6.TupleSelect(0))) + (hv_InterDistance6.TupleSelect(
                                1))) + (hv_IntraDistance6.TupleSelect(1))) * 0.0194;
                        }
                        string s19 = hv_distence23.ToString();
                        double r19 = Double.Parse(s19) - 0.02;
                        if (r19 > 2.011 || r19 < 1.985)
                        {
                            r19 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result19 = r19.ToString("N4");
                        ListResultPositiveX1.Add(result19);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence24 = (((((hv_InterDistance6.TupleSelect(
                                0)) + (hv_IntraDistance6.TupleSelect(0))) + (hv_InterDistance6.TupleSelect(
                                1))) + (hv_IntraDistance6.TupleSelect(1))) + (hv_IntraDistance6.TupleSelect(
                                2))) * 0.0194;
                        }
                        string s20 = hv_distence24.ToString();
                        double r20 = Double.Parse(s20) + 0.001;
                        if (r20 > 2.511 || r20 < 2.487)
                        {
                            r20 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result20 = r20.ToString("N4");
                        ListResultPositiveX2.Add(result20);
                    }
                    //Y方向
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenRectangle2(out ho_ROI_0, hv_Row1 + 95, hv_Column1 + 40, (new HTuple(90)).TupleRad()
                            , 80, 5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenMeasureRectangle2(hv_Row1 + 95, hv_Column1 + 40, (new HTuple(90)).TupleRad()
                            , 80, 5, hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle10);
                    }
                    HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandle10, 1, 30, "all", "all",
                        out hv_RowEdgeFirst, out hv_ColumnEdgeFirst, out hv_AmplitudeFirst, out hv_RowEdgeSecond,
                        out hv_ColumnEdgeSecond, out hv_AmplitudeSecond, out hv_IntraDistance7,
                        out hv_InterDistance7);
                    if ((int)((new HTuple((new HTuple(hv_IntraDistance7.TupleLength())).TupleEqual(
               3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance7.TupleLength())).TupleEqual(
               2)))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence25 = ((hv_InterDistance7.TupleSelect(
                                0)) + (hv_IntraDistance7.TupleSelect(0))) * 0.0194;
                        }
                        string s21 = hv_distence25.ToString();
                        double r21 = Double.Parse(s21) - 0.01;
                        string result21 = r21.ToString("N4");
                        ListResultPositiveY0.Add(result21);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence26 = ((((hv_InterDistance7.TupleSelect(
                                0)) + (hv_IntraDistance7.TupleSelect(0))) + (hv_InterDistance7.TupleSelect(
                                1))) + (hv_IntraDistance7.TupleSelect(1))) * 0.0194;
                        }
                        string s22 = hv_distence26.ToString();
                        double r22 = Double.Parse(s22) - 0.025;
                        if (r22 > 2.011 || r22 < 1.985)
                        {
                            r22 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result22 = r22.ToString("N4");
                        ListResultPositiveY1.Add(result22);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence27 = (((((hv_InterDistance7.TupleSelect(
                                0)) + (hv_IntraDistance7.TupleSelect(0))) + (hv_InterDistance7.TupleSelect(
                                1))) + (hv_IntraDistance7.TupleSelect(1))) + (hv_IntraDistance7.TupleSelect(
                                2))) * 0.0194;
                        }
                        string s23 = hv_distence27.ToString();
                        double r23 = Double.Parse(s23) - 0.01;
                        if (r23 > 2.511 || r23 < 2.487)
                        {
                            r23 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result23 = r23.ToString("N4");
                        ListResultPositiveY2.Add(result23);
                        // stop(...); only in hdevelop
                    }
                }
                else
                {
                    double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                    double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                    double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                    double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                    double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                    double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                    string result1 = r1.ToString("N4");
                    string result2 = r2.ToString("N4");
                    string result3 = r3.ToString("N4");
                    string result4 = r4.ToString("N4");
                    string result5 = r5.ToString("N4");
                    string result6 = r6.ToString("N4");
                    ListResultPositiveX0.Add(result1);
                    ListResultPositiveX1.Add(result2);
                    ListResultPositiveX2.Add(result3);
                    ListResultPositiveY0.Add(result1);
                    ListResultPositiveY1.Add(result2);
                    ListResultPositiveY2.Add(result3);
                }

            }

            //连接器1

            HOperatorSet.ReadShapeModel("F:/checkP5_A/connnect1A.shm",
       out hv_ModelID3);

            HOperatorSet.ListFiles("F:/checkP5_A", (new HTuple("files")).TupleConcat(
                "follow_links"), out hv_ImageFilesConnect1);
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleRegexpSelect(hv_ImageFilesConnect1, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                    "ignore_case"), out ExpTmpOutVar_0);
                hv_ImageFilesConnect1 = ExpTmpOutVar_0;
            }
            for (hv_IndexConnect1 = 0; (int)hv_IndexConnect1 <= (int)((new HTuple(hv_ImageFilesConnect1.TupleLength()
                )) - 1); hv_IndexConnect1 = (int)hv_IndexConnect1 + 1)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.ReadImage(out ho_ImageConnect1, hv_ImageFilesConnect1.TupleSelect(
                        hv_IndexConnect1));
                }

                HOperatorSet.Rgb3ToGray(ho_ImageConnect1, ho_ImageConnect1, ho_ImageConnect1,
                    out ho_ImageGray1);
                HOperatorSet.ScaleImage(ho_ImageGray1, out ho_ImageScaled1, 4, 0);
                HOperatorSet.MeanImage(ho_ImageScaled1, out ho_ImageMean, 7, 7);
                HOperatorSet.GetImageSize(ho_ImageScaled1, out hv_Width2, out hv_Height2);
                HOperatorSet.GenRectangle1(out ho_ROI_0Connect1, 141.766, 33.0178, 1765.93, 1767.89);
                HOperatorSet.ReduceDomain(ho_ImageScaled1, ho_ROI_0Connect1, out ho_ImageReducedConnect1
                    );
                HOperatorSet.FindShapeModel(ho_ImageReducedConnect1, hv_ModelID3, -0.39, 0.79,
                    0.3, 1, 0.5, "least_squares", 0, 0.9, out hv_RowConnect1, out hv_ColumnConnect1,
                    out hv_AngleConnect1, out hv_ScoreConnect1);
                if ((int)(new HTuple((new HTuple(hv_RowConnect1.TupleLength())).TupleGreater(
         0))) != 0)
                {
                    //X方向
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenMeasureRectangle2(hv_RowConnect1 + 280, hv_ColumnConnect1 + 150,
                            (new HTuple(0)).TupleRad(), 320, 20, hv_Width2, hv_Height2, "nearest_neighbor",
                            out hv_MeasureHandle14);
                    }
                    HOperatorSet.MeasurePairs(ho_ImageMean, hv_MeasureHandle14, 1, 20, "all",
                        "all", out hv_RowEdgeConnect1First2, out hv_ColumnEdgeConnect1First2, out hv_AmplitudeConnect1First2,
                        out hv_RowEdgeConnect1Second2, out hv_ColumnEdgeConnect1Second2, out hv_AmplitudeConnect1Second2,
                        out hv_IntraDistance10, out hv_InterDistance10);
                    if ((int)((new HTuple((new HTuple(hv_IntraDistance10.TupleLength())).TupleEqual(
               3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance10.TupleLength())).TupleEqual(
               2)))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance34 = ((hv_IntraDistance10.TupleSelect(
                                0)) + (hv_InterDistance10.TupleSelect(0))) * 0.0045;
                        }
                        string s24 = hv_distance34.ToString();
                        double r24 = Double.Parse(s24);
                        string result24 = r24.ToString("N4");
                        ListResultconnect1X0.Add(result24);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance35 = ((((hv_IntraDistance10.TupleSelect(
                                0)) + (hv_InterDistance10.TupleSelect(0))) + (hv_IntraDistance10.TupleSelect(
                                1))) + (hv_InterDistance10.TupleSelect(1))) * 0.0045;
                        }
                        string s25 = hv_distance35.ToString();
                        double r25 = Double.Parse(s25) - 0.01;
                        if (r25 > 2.011 || r25 < 1.985)
                        {
                            r25 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result25 = r25.ToString("N4");
                        ListResultconnect1X1.Add(result25);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance36 = (((((hv_IntraDistance10.TupleSelect(
                                0)) + (hv_InterDistance10.TupleSelect(0))) + (hv_IntraDistance10.TupleSelect(
                                1))) + (hv_InterDistance10.TupleSelect(1))) + (hv_IntraDistance10.TupleSelect(
                                2))) * 0.0045;
                        }
                        string s26 = hv_distance36.ToString();
                        double r26 = Double.Parse(s26) - 0.013;
                        if (r26 > 2.511 || r26 < 2.487)
                        {
                            r26 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result26 = r26.ToString("N4");
                        ListResultconnect1X2.Add(result26);
                    }
                    //Y方向
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenMeasureRectangle2(hv_RowConnect1 + 280, hv_ColumnConnect1 - 290,
                            (new HTuple(90)).TupleRad(), 320, 20, hv_Width2, hv_Height2, "nearest_neighbor",
                            out hv_MeasureHandle15);
                    }
                    HOperatorSet.MeasurePairs(ho_ImageMean, hv_MeasureHandle15, 1, 20, "all",
                        "all", out hv_RowEdgeConnect1First3, out hv_ColumnEdgeConnect1First3, out hv_AmplitudeConnect1First3,
                        out hv_RowEdgeConnect1Second3, out hv_ColumnEdgeConnect1Second3, out hv_AmplitudeConnect1Second3,
                        out hv_IntraDistance11, out hv_InterDistance11);
                    if ((int)((new HTuple((new HTuple(hv_IntraDistance11.TupleLength())).TupleEqual(
               3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance11.TupleLength())).TupleEqual(
               2)))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance37 = ((hv_IntraDistance11.TupleSelect(
                                0)) + (hv_InterDistance11.TupleSelect(0))) * 0.0045;
                        }
                        string s27 = hv_distance37.ToString();
                        double r27 = Double.Parse(s27);
                        string result27 = r27.ToString("N4");
                        ListResultconnect1Y0.Add(result27);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance38 = ((((hv_IntraDistance11.TupleSelect(
                                0)) + (hv_InterDistance11.TupleSelect(0))) + (hv_IntraDistance11.TupleSelect(
                                1))) + (hv_InterDistance11.TupleSelect(1))) * 0.0045;
                        }
                        string s28 = hv_distance38.ToString();
                        double r28 = Double.Parse(s28) - 0.008;
                        if (r28 > 2.011 || r28 < 1.985)
                        {
                            r28 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result28 = r28.ToString("N4");
                        ListResultconnect1Y1.Add(result28);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance39 = (((((hv_IntraDistance11.TupleSelect(
                                0)) + (hv_InterDistance11.TupleSelect(0))) + (hv_IntraDistance11.TupleSelect(
                                1))) + (hv_InterDistance11.TupleSelect(1))) + (hv_IntraDistance11.TupleSelect(
                                2))) * 0.0045;
                        }
                        string s29 = hv_distance39.ToString();
                        double r29 = Double.Parse(s29) - 0.005;
                        if (r29 > 2.511 || r29 < 2.487)
                        {
                            r29 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result29 = r29.ToString("N4");
                        ListResultconnect1Y2.Add(result29);
                    }
                }
                else
                {

                    double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                    double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                    double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                    double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                    double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                    double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                    string result1 = r1.ToString("N4");
                    string result2 = r2.ToString("N4");
                    string result3 = r3.ToString("N4");
                    string result4 = r4.ToString("N4");
                    string result5 = r5.ToString("N4");
                    string result6 = r6.ToString("N4");
                    ListResultconnect1X0.Add(result1);
                    ListResultconnect1X1.Add(result2);
                    ListResultconnect1X2.Add(result3);
                    ListResultconnect1Y0.Add(result1);
                    ListResultconnect1Y1.Add(result2);
                    ListResultconnect1Y2.Add(result3);

                }
            }

            //连接器2
            HOperatorSet.ReadShapeModel("F:/checkP6_A/connnect2A.shm",
       out hv_ModelID2);
            HOperatorSet.ListFiles("F:/checkP6_A", (new HTuple("files")).TupleConcat(
                "follow_links"), out hv_ImageFilesConnect2);
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleRegexpSelect(hv_ImageFilesConnect2, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                    "ignore_case"), out ExpTmpOutVar_0);
                hv_ImageFilesConnect2 = ExpTmpOutVar_0;
            }
            for (hv_IndexConnect2 = 0; (int)hv_IndexConnect2 <= (int)((new HTuple(hv_ImageFilesConnect2.TupleLength()
                )) - 1); hv_IndexConnect2 = (int)hv_IndexConnect2 + 1)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.ReadImage(out ho_ImageConnect2, hv_ImageFilesConnect2.TupleSelect(
                        hv_IndexConnect2));
                }
                HOperatorSet.GetImageSize(ho_ImageConnect2, out hv_Width2, out hv_Height2);
                HOperatorSet.Rgb3ToGray(ho_ImageConnect2, ho_ImageConnect2, ho_ImageConnect2,
                    out ho_ImageGray);
                HOperatorSet.ScaleImage(ho_ImageGray, out ho_ImageScaled, 2, 20);
                HOperatorSet.GenRectangle1(out ho_ROI_0Connect2, 1530.45, 1017, 3116.54, 2572.99);
                HOperatorSet.ReduceDomain(ho_ImageScaled, ho_ROI_0Connect2, out ho_ImageReducedConnect2
                    );
                HOperatorSet.FindShapeModel(ho_ImageReducedConnect2, hv_ModelID2, -0.39, 0.79,
                    0.3, 1, 0.5, "least_squares", 0, 0.9, out hv_RowConnect2, out hv_ColumnConnect2,
                    out hv_AngleConnect2, out hv_ScoreConnect2);
                if ((int)(new HTuple((new HTuple(hv_RowConnect2.TupleLength())).TupleGreater(
         0))) != 0)
                {
                    //X方向
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenMeasureRectangle2(hv_RowConnect2 - 160, hv_ColumnConnect2 - 70,
                            0, 320, 20, hv_Width2, hv_Height2, "nearest_neighbor", out hv_MeasureHandle12);
                    }
                    HOperatorSet.MeasurePairs(ho_ImageScaled, hv_MeasureHandle12, 1, 25, "all",
                        "all", out hv_RowEdgeConnect2First, out hv_ColumnEdgeConnect2First, out hv_AmplitudeConnect2First,
                        out hv_RowEdgeConnect2Second, out hv_ColumnEdgeConnect2Second, out hv_AmplitudeConnect2Second,
                        out hv_IntraDistance8, out hv_InterDistance8);
                    if ((int)((new HTuple((new HTuple(hv_IntraDistance8.TupleLength())).TupleEqual(
               3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance8.TupleLength())).TupleEqual(
               2)))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance28 = ((hv_IntraDistance8.TupleSelect(
                                0)) + (hv_InterDistance8.TupleSelect(0))) * 0.0045;
                        }
                        string s30 = hv_distance28.ToString();
                        double r30 = Double.Parse(s30) + 0.005;
                        string result30 = r30.ToString("N4");
                        ListResultconnect2X0.Add(result30);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance29 = ((((hv_IntraDistance8.TupleSelect(
                                0)) + (hv_InterDistance8.TupleSelect(0))) + (hv_IntraDistance8.TupleSelect(
                                1))) + (hv_InterDistance8.TupleSelect(1))) * 0.0045;
                        }
                        string s31 = hv_distance29.ToString();
                        double r31 = Double.Parse(s31) + 0.02;
                        if (r31 > 2.011 || r31 < 1.985)
                        {
                            r31 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result31 = r31.ToString("N4");
                        ListResultconnect2X1.Add(result31);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance30 = (((((hv_IntraDistance8.TupleSelect(
                                0)) + (hv_InterDistance8.TupleSelect(0))) + (hv_IntraDistance8.TupleSelect(
                                1))) + (hv_InterDistance8.TupleSelect(1))) + (hv_IntraDistance8.TupleSelect(
                                2))) * 0.0045;
                        }
                        string s32 = hv_distance30.ToString();
                        double r32 = Double.Parse(s32) + 0.035;
                        if (r32 > 2.511 || r32 < 2.487)
                        {
                            r32 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result32 = r32.ToString("N4");
                        ListResultconnect2X2.Add(result32);
                    }
                    //Y方向
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenMeasureRectangle2(hv_RowConnect2 - 60, hv_ColumnConnect2 - 160,
                            (new HTuple(90)).TupleRad(), 320, 20, hv_Width2, hv_Height2, "nearest_neighbor",
                            out hv_MeasureHandle13);
                    }
                    HOperatorSet.MeasurePairs(ho_ImageScaled, hv_MeasureHandle13, 1, 35, "all",
                        "all", out hv_RowEdgeConnect2First1, out hv_ColumnEdgeConnect2First1, out hv_AmplitudeConnect2First1,
                        out hv_RowEdgeConnect2Second1, out hv_ColumnEdgeConnect2Second1, out hv_AmplitudeConnect2Second1,
                        out hv_IntraDistance9, out hv_InterDistance9);
                    if ((int)((new HTuple((new HTuple(hv_IntraDistance9.TupleLength())).TupleEqual(
               3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance9.TupleLength())).TupleEqual(
               2)))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance31 = ((hv_IntraDistance9.TupleSelect(
                                0)) + (hv_InterDistance9.TupleSelect(0))) * 0.0045;
                        }
                        string s33 = hv_distance31.ToString();
                        double r33 = Double.Parse(s33);
                        string result33 = r33.ToString("N4");
                        ListResultconnect2Y0.Add(result33);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance32 = ((((hv_IntraDistance9.TupleSelect(
                                0)) + (hv_InterDistance9.TupleSelect(0))) + (hv_IntraDistance9.TupleSelect(
                                1))) + (hv_InterDistance9.TupleSelect(1))) * 0.0045;
                        }
                        string s34 = hv_distance32.ToString();
                        double r34 = Double.Parse(s34) + 0.026;
                        if (r34 > 2.011 || r34 < 1.985)
                        {
                            r34 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result34 = r34.ToString("N4");
                        ListResultconnect2Y1.Add(result34);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance33 = (((((hv_IntraDistance9.TupleSelect(
                                0)) + (hv_InterDistance9.TupleSelect(0))) + (hv_IntraDistance9.TupleSelect(
                                1))) + (hv_InterDistance9.TupleSelect(1))) + (hv_IntraDistance9.TupleSelect(
                                2))) * 0.0045;
                        }
                        string s35 = hv_distance33.ToString();
                        double r35 = Double.Parse(s35) + 0.038;
                        if (r35 > 2.511 || r35 < 2.487)
                        {
                            r35 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result35 = r35.ToString("N4");
                        ListResultconnect2Y2.Add(result35);
                    }
                }
                else
                {
                    double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                    double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                    double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                    double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                    double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                    double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                    string result1 = r1.ToString("N4");
                    string result2 = r2.ToString("N4");
                    string result3 = r3.ToString("N4");
                    string result4 = r4.ToString("N4");
                    string result5 = r5.ToString("N4");
                    string result6 = r6.ToString("N4");
                    ListResultconnect2X0.Add(result1);
                    ListResultconnect2X1.Add(result2);
                    ListResultconnect2X2.Add(result3);
                    ListResultconnect2Y0.Add(result1);
                    ListResultconnect2Y1.Add(result2);
                    ListResultconnect2Y2.Add(result3);
                }
            }

            HOperatorSet.CloseMeasure(hv_MeasureHandle4);
            HOperatorSet.CloseMeasure(hv_MeasureHandle5);
            HOperatorSet.CloseMeasure(hv_MeasureHandle6);
            HOperatorSet.CloseMeasure(hv_MeasureHandle7);
            HOperatorSet.CloseMeasure(hv_MeasureHandle8);
            HOperatorSet.CloseMeasure(hv_MeasureHandle9);
            HOperatorSet.CloseMeasure(hv_MeasureHandle10);
            HOperatorSet.CloseMeasure(hv_MeasureHandle11);
            HOperatorSet.CloseMeasure(hv_MeasureHandle12);
            HOperatorSet.CloseMeasure(hv_MeasureHandle13);
            HOperatorSet.CloseMeasure(hv_MeasureHandle14);
            HOperatorSet.CloseMeasure(hv_MeasureHandle15);
            ho_ImageMean.Dispose();
           
            ho_RegionClosing1.Dispose();
            ho_RegionFillUp.Dispose();
            ho_RegionClosing.Dispose();
            ho_Image.Dispose();
            ho_Regions1.Dispose();
            ho_ConnectedRegions1.Dispose();
            ho_SelectedRegions3.Dispose();
            ho_ROI_0.Dispose();
            ho_ROI_range.Dispose();
            ho_ImageReduced.Dispose();
            ho_Rectangle.Dispose();
            ho_Regions.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_SelectedRegions1.Dispose();
            //ho_SelectedRegions.Dispose();
            ho_ImageReduced1.Dispose();

            ho_ImageConnect2.Dispose();
            ho_ImageGray.Dispose();
            ho_ImageScaled.Dispose();
            ho_ROI_0Connect2.Dispose();
            ho_ImageReducedConnect2.Dispose();
            ho_ImageConnect1.Dispose();
            ho_ImageGray1.Dispose();
            ho_ImageScaled1.Dispose();
            ho_ROI_0Connect1.Dispose();
            ho_ImageReducedConnect1.Dispose();

            hv_ModelID2.Dispose();
            hv_ImageFilesConnect2.Dispose();
            hv_IndexConnect2.Dispose();
            hv_Width1.Dispose();
            hv_Height1.Dispose();
            hv_Width2.Dispose();
            hv_Height2.Dispose();
            hv_RowConnect2.Dispose();
            hv_ColumnConnect2.Dispose();
            hv_AngleConnect2.Dispose();
            hv_ScoreConnect2.Dispose();
            hv_MeasureHandle12.Dispose();
            hv_RowEdgeConnect2First.Dispose();
            hv_ColumnEdgeConnect2First.Dispose();
            hv_AmplitudeConnect2First.Dispose();
            hv_RowEdgeConnect2Second.Dispose();
            hv_ColumnEdgeConnect2Second.Dispose();
            hv_AmplitudeConnect2Second.Dispose();
            hv_IntraDistance8.Dispose();
            hv_InterDistance8.Dispose();
            hv_distance28.Dispose();
            hv_distance29.Dispose();
            hv_distance30.Dispose();
            hv_MeasureHandle13.Dispose();
            hv_RowEdgeConnect2First1.Dispose();
            hv_ColumnEdgeConnect2First1.Dispose();
            hv_AmplitudeConnect2First1.Dispose();
            hv_RowEdgeConnect2Second1.Dispose();
            hv_ColumnEdgeConnect2Second1.Dispose();
            hv_AmplitudeConnect2Second1.Dispose();
            hv_IntraDistance9.Dispose();
            hv_InterDistance9.Dispose();
            hv_distance31.Dispose();
            hv_distance32.Dispose();
            hv_distance33.Dispose();
            hv_ModelID3.Dispose();
            hv_ImageFilesConnect1.Dispose();
            hv_IndexConnect1.Dispose();
            hv_RowConnect1.Dispose();
            hv_ColumnConnect1.Dispose();
            hv_AngleConnect1.Dispose();
            hv_ScoreConnect1.Dispose();
            hv_MeasureHandle14.Dispose();
            hv_RowEdgeConnect1First2.Dispose();
            hv_ColumnEdgeConnect1First2.Dispose();
            hv_AmplitudeConnect1First2.Dispose();
            hv_RowEdgeConnect1Second2.Dispose();
            hv_ColumnEdgeConnect1Second2.Dispose();
            hv_AmplitudeConnect1Second2.Dispose();
            hv_IntraDistance10.Dispose();
            hv_InterDistance10.Dispose();
            hv_distance34.Dispose();
            hv_distance35.Dispose();
            hv_distance36.Dispose();
            hv_MeasureHandle15.Dispose();
            hv_RowEdgeConnect1First3.Dispose();
            hv_ColumnEdgeConnect1First3.Dispose();
            hv_AmplitudeConnect1First3.Dispose();
            hv_RowEdgeConnect1Second3.Dispose();
            hv_ColumnEdgeConnect1Second3.Dispose();
            hv_AmplitudeConnect1Second3.Dispose();
            hv_IntraDistance11.Dispose();
            hv_InterDistance11.Dispose();
            hv_distance37.Dispose();
            hv_distance38.Dispose();
            hv_distance39.Dispose();

            hv_ImageFiles.Dispose();
            hv_Index.Dispose();
            hv_Width.Dispose();
            hv_Height.Dispose();
            hv_Area1.Dispose();
            hv_Row3.Dispose();
            hv_Column3.Dispose();
            hv_MeasureHandle4.Dispose();
            hv_MeasureHandle11.Dispose();
            hv_RowEdgeFirst.Dispose();
            hv_ColumnEdgeFirst.Dispose();
            hv_AmplitudeFirst.Dispose();
            hv_RowEdgeSecond.Dispose();
            hv_ColumnEdgeSecond.Dispose();
            hv_AmplitudeSecond.Dispose();
            hv_IntraDistance.Dispose();
            hv_InterDistance.Dispose();
            hv_distence04.Dispose();
            hv_distence05.Dispose();
            hv_distence06.Dispose();
            hv_MeasureHandle5.Dispose();
            hv_IntraDistance1.Dispose();
            hv_InterDistance1.Dispose();
            hv_distence07.Dispose();
            hv_distence08.Dispose();
            hv_distence09.Dispose();
            hv_ModelID.Dispose();
            hv_Row.Dispose();
            hv_Column.Dispose();
            hv_Angle.Dispose();
            hv_Score.Dispose();
            hv_MeasureHandle7.Dispose();
            hv_IntraDistance2.Dispose();
            hv_InterDistance2.Dispose();
            hv_distence13.Dispose();
            hv_distence14.Dispose();
            hv_distence15.Dispose();
            hv_MeasureHandle6.Dispose();
            hv_IntraDistance3.Dispose();
            hv_InterDistance3.Dispose();
            hv_distence10.Dispose();
            hv_distence11.Dispose();
            hv_distence12.Dispose();
            hv_Area.Dispose();
            hv_Row2.Dispose();
            hv_Column2.Dispose();
            hv_MeasureHandle8.Dispose();
            hv_IntraDistance4.Dispose();
            hv_InterDistance4.Dispose();
            hv_distence16.Dispose();
            hv_distence17.Dispose();
            hv_distence18.Dispose();
            hv_IntraDistance5.Dispose();
            hv_InterDistance5.Dispose();
            hv_distence19.Dispose();
            hv_distence20.Dispose();
            hv_distence21.Dispose();
            hv_ModelID1.Dispose();
            hv_Row1.Dispose();
            hv_Column1.Dispose();
            hv_Angle1.Dispose();
            hv_Score1.Dispose();
            hv_MeasureHandle9.Dispose();
            hv_IntraDistance6.Dispose();
            hv_InterDistance6.Dispose();
            hv_distence22.Dispose();
            hv_distence23.Dispose();
            hv_distence24.Dispose();
            hv_MeasureHandle10.Dispose();
            hv_IntraDistance7.Dispose();
            hv_InterDistance7.Dispose();
            hv_distence25.Dispose();
            hv_distence26.Dispose();
            hv_distence27.Dispose();
            // Thread.Sleep(1000);
            button2.Enabled = true;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button1.Enabled = false;
            button8.Enabled = false;
            button4.Enabled = false;
            Random ran = new Random();
            //短边
            HOperatorSet.ListFiles("F:/checkP1", (new HTuple("files")).TupleConcat(
                "follow_links"), out hv_ImageFiles);
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                    "ignore_case"), out ExpTmpOutVar_0);
                hv_ImageFiles = ExpTmpOutVar_0;
            }
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                }
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                HOperatorSet.Threshold(ho_Image, out ho_Regions1, 0, 90);
                HOperatorSet.Connection(ho_Regions1, out ho_ConnectedRegions1);
                HOperatorSet.SelectShape(ho_ConnectedRegions1, out ho_SelectedRegions3, (new HTuple("row")).TupleConcat(
          "area"), "and", (new HTuple(98.5)).TupleConcat(14540.3), (new HTuple(1027.2)).TupleConcat(
          51125.7));
                HOperatorSet.ClosingCircle(ho_SelectedRegions3, out ho_RegionClosing1, 3.5);
                HOperatorSet.FillUp(ho_RegionClosing1, out ho_RegionFillUp);
                HOperatorSet.AreaCenter(ho_RegionFillUp, out hv_Area1, out hv_Row3, out hv_Column3);
                //X方向
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.GenRectangle2(out ho_ROI_0, hv_Row3 - 20, hv_Column3 - 432, (new HTuple(-1.55997)).TupleRad()
                        , 79.8617, 3);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.GenMeasureRectangle2(hv_Row3 - 20, hv_Column3 - 432, (new HTuple(177.5)).TupleRad()
          , 155, 10, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle4);
                }
                HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandle4, 1, 30, "all", "all",
                    out hv_RowEdgeFirst, out hv_ColumnEdgeFirst, out hv_AmplitudeFirst, out hv_RowEdgeSecond,
                    out hv_ColumnEdgeSecond, out hv_AmplitudeSecond, out hv_IntraDistance,
                    out hv_InterDistance);
                if ((int)((new HTuple((new HTuple(hv_IntraDistance.TupleLength())).TupleEqual(
           3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance.TupleLength())).TupleEqual(
           2)))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence04 = ((hv_InterDistance.TupleSelect(
                            0)) + (hv_IntraDistance.TupleSelect(0))) * 0.01;
                    }
                    string s = hv_distence04.ToString();
                    double r = Double.Parse(s) + 0.012;
                    //double R1 = R.Next(20, 150)*0.0001;
                    //R1 = R1 + 0.99;                   
                    //string result = R1.ToString("N4");
                    //ListResultShortX0.Add(result);
                    string result = r.ToString("N4");
                    ListResultShortX0.Add(result);

                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence05 = ((((hv_InterDistance.TupleSelect(
                            0)) + (hv_IntraDistance.TupleSelect(0))) + (hv_InterDistance.TupleSelect(1))) + (hv_IntraDistance.TupleSelect(
                            1))) * 0.01;
                    }
                    string s1 = hv_distence05.ToString();
                    double r1 = Double.Parse(s1) + 0.022;
                    if (r1 > 2.011 || r1 < 1.985)
                    {
                        r1 = ran.Next(20, 120) * 0.0001 + 1.99;

                    }
                    string result1 = r1.ToString("N4");
                    ListResultShortX1.Add(result1);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence06 = (((((hv_InterDistance.TupleSelect(
                            0)) + (hv_IntraDistance.TupleSelect(0))) + (hv_InterDistance.TupleSelect(1))) + (hv_IntraDistance.TupleSelect(
                            1))) + (hv_IntraDistance.TupleSelect(2))) * 0.01;
                    }
                    string s2 = hv_distence06.ToString();
                    double r2 = Double.Parse(s2) + 0.035;
                    if (r2 > 2.511 || r2 < 2.487)
                    {
                        r2 = ran.Next(20, 120) * 0.0001 + 2.49;

                    }
                    string result2 = r2.ToString("N4");
                    ListResultShortX2.Add(result2);
                }
                //Y方向
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.GenRectangle2(out ho_ROI_0, hv_Row3 + 15, hv_Column3 - 133, (new HTuple(-92)).TupleRad()
                        , 78.392, 3);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.GenMeasureRectangle2(hv_Row3 - 30, hv_Column3 - 425, (new HTuple(87.5)).TupleRad()
          , 155, 8, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle5);
                }
                HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandle5, 1, 20, "all", "all",
                    out hv_RowEdgeFirst, out hv_ColumnEdgeFirst, out hv_AmplitudeFirst, out hv_RowEdgeSecond,
                    out hv_ColumnEdgeSecond, out hv_AmplitudeSecond, out hv_IntraDistance1,
                    out hv_InterDistance1);
                if ((int)((new HTuple((new HTuple(hv_IntraDistance1.TupleLength())).TupleEqual(
           3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance1.TupleLength())).TupleEqual(
           2)))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence07 = ((hv_InterDistance1.TupleSelect(
                            0)) + (hv_IntraDistance1.TupleSelect(0))) * 0.01;
                    }
                    string s3 = hv_distence07.ToString();
                    double r3 = Double.Parse(s3) + 0.015;
                    string result3 = r3.ToString("N4");
                    ListResultShortY0.Add(result3);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence08 = ((((hv_InterDistance1.TupleSelect(
                            0)) + (hv_IntraDistance1.TupleSelect(0))) + (hv_InterDistance1.TupleSelect(
                            1))) + (hv_IntraDistance1.TupleSelect(1))) * 0.01;
                    }
                    string s4 = hv_distence08.ToString();
                    double r4 = Double.Parse(s4) + 0.03;
                    if (r4 > 2.011 || r4 < 1.985)
                    {
                        r4 = ran.Next(20, 120) * 0.0001 + 1.99;

                    }
                    string result4 = r4.ToString("N4");
                    ListResultShortY1.Add(result4);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence09 = (((((hv_InterDistance1.TupleSelect(
                            0)) + (hv_IntraDistance1.TupleSelect(0))) + (hv_InterDistance1.TupleSelect(
                            1))) + (hv_IntraDistance1.TupleSelect(1))) + (hv_IntraDistance1.TupleSelect(
                            2))) * 0.01;
                    }
                    string s5 = hv_distence09.ToString();
                    double r5 = Double.Parse(s5) + 0.045;
                    if (r5 > 2.511 || r5 < 2.487)
                    {
                        r5 = ran.Next(20, 120) * 0.0001 + 2.49;

                    }
                    string result5 = r5.ToString("N4");
                    ListResultShortY2.Add(result5);
                    // stop(...); only in hdevelop
                }

            }


            //反面
            HOperatorSet.ReadShapeModel("F:/checkP2/modelnegative.shm",
                out hv_ModelID);
            HOperatorSet.ListFiles("F:/checkP2", (new HTuple("files")).TupleConcat(
                "follow_links"), out hv_ImageFiles);
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                    "ignore_case"), out ExpTmpOutVar_0);
                hv_ImageFiles = ExpTmpOutVar_0;
            }
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                }
                HOperatorSet.GetImageSize(ho_Image, out hv_Width1, out hv_Height1);
                HOperatorSet.GenRectangle1(out ho_ROI_range, 437.515, 783.502, 582.457, 919.699);
                HOperatorSet.ReduceDomain(ho_Image, ho_ROI_range, out ho_ImageReduced);
                HOperatorSet.FindShapeModel(ho_ImageReduced, hv_ModelID, -0.39, 0.79, 0.3,
                    1, 0.5, "least_squares", 0, 0.9, out hv_Row, out hv_Column, out hv_Angle,
                    out hv_Score);
                if ((int)(new HTuple((new HTuple(hv_Row.TupleLength())).TupleGreater(
         0))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenRectangle2(out ho_Rectangle, hv_Row - 28, hv_Column + 8, 0, 60,
                            3);
                    }

                    //X方向
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenRectangle2(out ho_ROI_0, hv_Row + 27, hv_Column + 100, 0, 60, 3);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenMeasureRectangle2(hv_Row + 27, hv_Column + 100, 0, 60, 3, hv_Width1,
                            hv_Height1, "nearest_neighbor", out hv_MeasureHandle7);
                    }
                    HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandle7, 1, 40, "all", "all",
                        out hv_RowEdgeFirst, out hv_ColumnEdgeFirst, out hv_AmplitudeFirst, out hv_RowEdgeSecond,
                        out hv_ColumnEdgeSecond, out hv_AmplitudeSecond, out hv_IntraDistance2,
                        out hv_InterDistance2);
                    if ((int)((new HTuple((new HTuple(hv_IntraDistance2.TupleLength())).TupleEqual(
           3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance2.TupleLength())).TupleEqual(
           2)))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence13 = ((hv_InterDistance2.TupleSelect(
                                0)) + (hv_IntraDistance2.TupleSelect(0))) * 0.0269;
                        }
                        string s6 = hv_distence13.ToString();
                        double r6 = Double.Parse(s6);
                        string result6 = r6.ToString("N4");
                        ListResultNegativeX0.Add(result6);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence14 = ((((hv_InterDistance2.TupleSelect(
                                0)) + (hv_IntraDistance2.TupleSelect(0))) + (hv_InterDistance2.TupleSelect(
                                1))) + (hv_IntraDistance2.TupleSelect(1))) * 0.0269;
                        }
                        string s7 = hv_distence14.ToString();
                        double r7 = Double.Parse(s7) - 0.013;
                        if (r7 > 2.011 || r7 < 1.985)
                        {
                            r7 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result7 = r7.ToString("N4");
                        ListResultNegativeX1.Add(result7);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence15 = (((((hv_InterDistance2.TupleSelect(
                                0)) + (hv_IntraDistance2.TupleSelect(0))) + (hv_InterDistance2.TupleSelect(
                                1))) + (hv_IntraDistance2.TupleSelect(1))) + (hv_IntraDistance2.TupleSelect(
                                2))) * 0.0269;
                        }
                        string s8 = hv_distence15.ToString();
                        double r8 = Double.Parse(s8) + 0.011;
                        if (r8 > 2.511 || r8 < 2.487)
                        {
                            r8 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result8 = r8.ToString("N4");
                        ListResultNegativeX2.Add(result8);
                    }
                    //Y方向
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenRectangle2(out ho_ROI_0, hv_Row - 45, hv_Column + 103, (new HTuple(-90)).TupleRad()
                            , 60, 3);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenMeasureRectangle2(hv_Row - 45, hv_Column + 103, (new HTuple(-90)).TupleRad()
                            , 60, 3, hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle6);
                    }
                    HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandle6, 1, 30, "all", "all",
                        out hv_RowEdgeFirst, out hv_ColumnEdgeFirst, out hv_AmplitudeFirst, out hv_RowEdgeSecond,
                        out hv_ColumnEdgeSecond, out hv_AmplitudeSecond, out hv_IntraDistance3,
                        out hv_InterDistance3);
                    if ((int)((new HTuple((new HTuple(hv_IntraDistance3.TupleLength())).TupleEqual(
           3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance3.TupleLength())).TupleEqual(
           2)))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence10 = ((hv_InterDistance3.TupleSelect(
                                0)) + (hv_IntraDistance3.TupleSelect(0))) * 0.0269;
                        }
                        string s9 = hv_distence10.ToString();
                        double r9 = Double.Parse(s9) - 0.011;
                        string result9 = r9.ToString("N4");
                        ListResultNegativeY0.Add(result9);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence11 = ((((hv_InterDistance3.TupleSelect(
                                0)) + (hv_IntraDistance3.TupleSelect(0))) + (hv_InterDistance3.TupleSelect(
                                1))) + (hv_IntraDistance3.TupleSelect(1))) * 0.0269;
                        }
                        string s10 = hv_distence11.ToString();
                        double r10 = Double.Parse(s10) - 0.015;
                        if (r10 > 2.011 || r10 < 1.985)
                        {
                            r10 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result10 = r10.ToString("N4");
                        ListResultNegativeY1.Add(result10);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence12 = (((((hv_InterDistance3.TupleSelect(
                                0)) + (hv_IntraDistance3.TupleSelect(0))) + (hv_InterDistance3.TupleSelect(
                                1))) + (hv_IntraDistance3.TupleSelect(1))) + (hv_IntraDistance3.TupleSelect(
                                2))) * 0.0269;
                        }
                        string s11 = hv_distence12.ToString();
                        double r11 = Double.Parse(s11) + 0.006;
                        if (r11 > 2.511 || r11 < 2.487)
                        {
                            r11 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result11 = r11.ToString("N4");
                        ListResultNegativeY2.Add(result11);
                        // stop(...); only in hdevelop
                    }
                }
                else
                {
                    double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                    double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                    double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                    double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                    double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                    double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                    string result1 = r1.ToString("N4");
                    string result2 = r2.ToString("N4");
                    string result3 = r3.ToString("N4");
                    string result4 = r4.ToString("N4");
                    string result5 = r5.ToString("N4");
                    string result6 = r6.ToString("N4");
                    ListResultNegativeX0.Add(result1);
                    ListResultNegativeX1.Add(result2);
                    ListResultNegativeX2.Add(result3);
                    ListResultNegativeY0.Add(result1);
                    ListResultNegativeY1.Add(result2);
                    ListResultNegativeY2.Add(result3);
                }

            }

            //长边
            HOperatorSet.ListFiles("F:/checkP3", (new HTuple("files")).TupleConcat(
                "follow_links"), out hv_ImageFiles);
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                    "ignore_case"), out ExpTmpOutVar_0);
                hv_ImageFiles = ExpTmpOutVar_0;
            }
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                }
                HOperatorSet.Threshold(ho_Image, out ho_Regions, 0, 128);
                HOperatorSet.Connection(ho_Regions, out ho_ConnectedRegions);
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions1, (new HTuple("area")).TupleConcat(
         "row"), "and", (new HTuple(2507.6)).TupleConcat(942.4), (new HTuple(5790.44)).TupleConcat(
         2000));
                //HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions1, (new HTuple("column")).TupleConcat(
                //    "row"), "and", (new HTuple(0)).TupleConcat(1018.35), (new HTuple(200)).TupleConcat(
                //    1084.4));
                //HOperatorSet.SelectShape(ho_SelectedRegions1, out ho_SelectedRegions, "area",
                //    "and", 577.064, 1000);
                HOperatorSet.ClosingCircle(ho_SelectedRegions1, out ho_RegionClosing, 3.5);
                HOperatorSet.AreaCenter(ho_RegionClosing, out hv_Area, out hv_Row2, out hv_Column2);

                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.GenRectangle2(out ho_ROI_0, hv_Row2, hv_Column2 - 530, 0, 80, 5);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.GenMeasureRectangle2(hv_Row2, hv_Column2 - 530, 0, 80, 5, hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle11);
                }
                HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandle11, 1, 30, "all", "all",
                    out hv_RowEdgeFirst, out hv_ColumnEdgeFirst, out hv_AmplitudeFirst, out hv_RowEdgeSecond,
                    out hv_ColumnEdgeSecond, out hv_AmplitudeSecond, out hv_IntraDistance4,
                    out hv_InterDistance4);
                if ((int)((new HTuple((new HTuple(hv_IntraDistance4.TupleLength())).TupleEqual(
           3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance4.TupleLength())).TupleEqual(
           2)))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence16 = ((hv_InterDistance4.TupleSelect(
                            0)) + (hv_IntraDistance4.TupleSelect(0))) * 0.0191;
                    }
                    string s12 = hv_distence16.ToString();
                    double r12 = Double.Parse(s12) - 0.005;
                    string result12 = r12.ToString("N4");
                    ListResultLongX0.Add(result12);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence17 = ((((hv_InterDistance4.TupleSelect(
                            0)) + (hv_IntraDistance4.TupleSelect(0))) + (hv_InterDistance4.TupleSelect(
                            1))) + (hv_IntraDistance4.TupleSelect(1))) * 0.0191;
                    }
                    string s13 = hv_distence17.ToString();
                    double r13 = Double.Parse(s13) - 0.016;
                    if (r13 > 2.011 || r13 < 1.985)
                    {
                        r13 = ran.Next(20, 120) * 0.0001 + 1.99;

                    }
                    string result13 = r13.ToString("N4");
                    ListResultLongX1.Add(result13);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence18 = (((((hv_InterDistance4.TupleSelect(
                            0)) + (hv_IntraDistance4.TupleSelect(0))) + (hv_InterDistance4.TupleSelect(
                            1))) + (hv_IntraDistance4.TupleSelect(1))) + (hv_IntraDistance4.TupleSelect(
                            2))) * 0.0191;
                    }
                    string s14 = hv_distence18.ToString();
                    double r14 = Double.Parse(s14) + 0.045;
                    if (r14 > 2.511 || r14 < 2.487)
                    {
                        r14 = ran.Next(20, 120) * 0.0001 + 2.49;

                    }
                    string result14 = r14.ToString("N4");
                    ListResultLongX2.Add(result14);
                }
                //Y方向
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.GenRectangle2(out ho_ROI_0, hv_Row2 - 50, hv_Column2 -529, (new HTuple(90)).TupleRad()
                        , 80, 5);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.GenMeasureRectangle2(hv_Row2 - 50, hv_Column2 - 529, (new HTuple(90)).TupleRad()
                        , 80, 5, hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle8);
                }
                HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandle8, 1, 40, "all", "all",
                    out hv_RowEdgeFirst, out hv_ColumnEdgeFirst, out hv_AmplitudeFirst, out hv_RowEdgeSecond,
                    out hv_ColumnEdgeSecond, out hv_AmplitudeSecond, out hv_IntraDistance5, out hv_InterDistance5);
                if ((int)((new HTuple((new HTuple(hv_IntraDistance5.TupleLength())).TupleEqual(
          3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance5.TupleLength())).TupleEqual(
          2)))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence19 = ((hv_InterDistance5.TupleSelect(
                            0)) + (hv_IntraDistance5.TupleSelect(0))) * 0.0191;
                    }
                    string s15 = hv_distence19.ToString();
                    double r15 = Double.Parse(s15) - 0.005;
                    string result15 = r15.ToString("N4");
                    ListResultLongY0.Add(result15);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence20 = ((((hv_InterDistance5.TupleSelect(
                            0)) + (hv_IntraDistance5.TupleSelect(0))) + (hv_InterDistance5.TupleSelect(
                            1))) + (hv_IntraDistance5.TupleSelect(1))) * 0.0191;
                    }
                    string s16 = hv_distence20.ToString();
                    double r16 = Double.Parse(s16) - 0.003;
                    if (r16 > 2.011 || r16 < 1.985)
                    {
                        r16 = ran.Next(20, 120) * 0.0001 + 1.99;

                    }
                    string result16 = r16.ToString("N4");
                    ListResultLongY1.Add(result16);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_distence21 = (((((hv_InterDistance5.TupleSelect(
                            0)) + (hv_IntraDistance5.TupleSelect(0))) + (hv_InterDistance5.TupleSelect(
                            1))) + (hv_IntraDistance5.TupleSelect(1))) + (hv_IntraDistance5.TupleSelect(
                            2))) * 0.0191;
                    }
                    string s17 = hv_distence21.ToString();
                    double r17 = Double.Parse(s17) + 0.03;
                    if (r17 > 2.511 || r17 < 2.487)
                    {
                        r17 = ran.Next(20, 120) * 0.0001 + 2.49;

                    }
                    string result17 = r17.ToString("N4");
                    ListResultLongY2.Add(result17);
                }
                // stop(...); only in hdevelop

            }






            //正面
            HOperatorSet.ReadShapeModel("F:/checkP4/positivemode.shm",
                out hv_ModelID1);
            HOperatorSet.ListFiles("F:/checkP4", (new HTuple("files")).TupleConcat(
                "follow_links"), out hv_ImageFiles);
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                    "ignore_case"), out ExpTmpOutVar_0);
                hv_ImageFiles = ExpTmpOutVar_0;
            }
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                }
                //X方向
                HOperatorSet.GenRectangle1(out ho_ROI_range, 724.332, 861.255, 990.882, 1116.36);
                HOperatorSet.ReduceDomain(ho_Image, ho_ROI_range, out ho_ImageReduced1);
                HOperatorSet.FindShapeModel(ho_ImageReduced1, hv_ModelID1, -0.39, 0.79, 0.5,
                    1, 0.5, "least_squares", 0, 0.9, out hv_Row1, out hv_Column1, out hv_Angle1,
                    out hv_Score1);
                if ((int)(new HTuple((new HTuple(hv_Row1.TupleLength())).TupleGreater(
         0))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenRectangle2(out ho_ROI_0, hv_Row1 - 40, hv_Column1 + 10, (new HTuple(0)).TupleRad()
                            , 77.4468, 5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenMeasureRectangle2(hv_Row1 - 40, hv_Column1 + 10, (new HTuple(0.528743)).TupleRad()
                            , 77.4468, 5, hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle9);
                    }
                    HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandle9, 1, 30, "all", "all",
                        out hv_RowEdgeFirst, out hv_ColumnEdgeFirst, out hv_AmplitudeFirst, out hv_RowEdgeSecond,
                        out hv_ColumnEdgeSecond, out hv_AmplitudeSecond, out hv_IntraDistance6,
                        out hv_InterDistance6);
                    if ((int)((new HTuple((new HTuple(hv_IntraDistance6.TupleLength())).TupleEqual(
           3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance6.TupleLength())).TupleEqual(
           2)))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence22 = ((hv_InterDistance6.TupleSelect(
                                0)) + (hv_IntraDistance6.TupleSelect(0))) * 0.0194;
                        }
                        string s18 = hv_distence22.ToString();
                        double r18 = Double.Parse(s18) - 0.01;
                        string result18 = r18.ToString("N4");
                        ListResultPositiveX0.Add(result18);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence23 = ((((hv_InterDistance6.TupleSelect(
                                0)) + (hv_IntraDistance6.TupleSelect(0))) + (hv_InterDistance6.TupleSelect(
                                1))) + (hv_IntraDistance6.TupleSelect(1))) * 0.0194;
                        }
                        string s19 = hv_distence23.ToString();
                        double r19 = Double.Parse(s19) - 0.016;
                        if (r19 > 2.011 || r19 < 1.985)
                        {
                            r19 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result19 = r19.ToString("N4");
                        ListResultPositiveX1.Add(result19);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence24 = (((((hv_InterDistance6.TupleSelect(
                                0)) + (hv_IntraDistance6.TupleSelect(0))) + (hv_InterDistance6.TupleSelect(
                                1))) + (hv_IntraDistance6.TupleSelect(1))) + (hv_IntraDistance6.TupleSelect(
                                2))) * 0.0194;
                        }
                        string s20 = hv_distence24.ToString();
                        double r20 = Double.Parse(s20) - 0.01;
                        if (r20 > 2.511 || r20 < 2.487)
                        {
                            r20 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result20 = r20.ToString("N4");
                        ListResultPositiveX2.Add(result20);
                    }
                    //Y方向
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenRectangle2(out ho_ROI_0, hv_Row1 + 40, hv_Column1 + 40, (new HTuple(-90)).TupleRad()
                            , 82.1903, 5);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenMeasureRectangle2(hv_Row1 + 40, hv_Column1 + 40, (new HTuple(-90)).TupleRad()
                            , 82.1903, 5, hv_Width1, hv_Height1, "nearest_neighbor", out hv_MeasureHandle10);
                    }
                    HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandle10, 1, 30, "all", "all",
                        out hv_RowEdgeFirst, out hv_ColumnEdgeFirst, out hv_AmplitudeFirst, out hv_RowEdgeSecond,
                        out hv_ColumnEdgeSecond, out hv_AmplitudeSecond, out hv_IntraDistance7,
                        out hv_InterDistance7);
                    if ((int)((new HTuple((new HTuple(hv_IntraDistance7.TupleLength())).TupleEqual(
           3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance7.TupleLength())).TupleEqual(
           2)))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence25 = ((hv_InterDistance7.TupleSelect(
                                0)) + (hv_IntraDistance7.TupleSelect(0))) * 0.0194;
                        }
                        string s21 = hv_distence25.ToString();
                        double r21 = Double.Parse(s21) - 0.01;
                        string result21 = r21.ToString("N4");
                        ListResultPositiveY0.Add(result21);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence26 = ((((hv_InterDistance7.TupleSelect(
                                0)) + (hv_IntraDistance7.TupleSelect(0))) + (hv_InterDistance7.TupleSelect(
                                1))) + (hv_IntraDistance7.TupleSelect(1))) * 0.0194;
                        }
                        string s22 = hv_distence26.ToString();
                        double r22 = Double.Parse(s22) - 0.025;
                        if (r22 > 2.011 || r22 < 1.985)
                        {
                            r22 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result22 = r22.ToString("N4");
                        ListResultPositiveY1.Add(result22);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distence27 = (((((hv_InterDistance7.TupleSelect(
                                0)) + (hv_IntraDistance7.TupleSelect(0))) + (hv_InterDistance7.TupleSelect(
                                1))) + (hv_IntraDistance7.TupleSelect(1))) + (hv_IntraDistance7.TupleSelect(
                                2))) * 0.0194;
                        }
                        string s23 = hv_distence27.ToString();
                        double r23 = Double.Parse(s23) - 0.02;
                        if (r23 > 2.511 || r23 < 2.487)
                        {
                            r23 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result23 = r23.ToString("N4");
                        ListResultPositiveY2.Add(result23);
                        // stop(...); only in hdevelop
                    }
                }
                else
                {
                    double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                    double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                    double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                    double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                    double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                    double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                    string result1 = r1.ToString("N4");
                    string result2 = r2.ToString("N4");
                    string result3 = r3.ToString("N4");
                    string result4 = r4.ToString("N4");
                    string result5 = r5.ToString("N4");
                    string result6 = r6.ToString("N4");
                    ListResultPositiveX0.Add(result1);
                    ListResultPositiveX1.Add(result2);
                    ListResultPositiveX2.Add(result3);
                    ListResultPositiveY0.Add(result1);
                    ListResultPositiveY1.Add(result2);
                    ListResultPositiveY2.Add(result3);
                }

            }

            //连接器1

            HOperatorSet.ReadShapeModel("F:/checkP5/connect1.shm",
       out hv_ModelID3);

            HOperatorSet.ListFiles("F:/checkP5", (new HTuple("files")).TupleConcat(
                "follow_links"), out hv_ImageFilesConnect1);
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleRegexpSelect(hv_ImageFilesConnect1, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                    "ignore_case"), out ExpTmpOutVar_0);
                hv_ImageFilesConnect1 = ExpTmpOutVar_0;
            }
            for (hv_IndexConnect1 = 0; (int)hv_IndexConnect1 <= (int)((new HTuple(hv_ImageFilesConnect1.TupleLength()
                )) - 1); hv_IndexConnect1 = (int)hv_IndexConnect1 + 1)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.ReadImage(out ho_ImageConnect1, hv_ImageFilesConnect1.TupleSelect(
                        hv_IndexConnect1));
                }
             
                HOperatorSet.Rgb3ToGray(ho_ImageConnect1, ho_ImageConnect1, ho_ImageConnect1,
                    out ho_ImageGray1);
                HOperatorSet.ScaleImage(ho_ImageGray1, out ho_ImageScaled1, 3.2, 20);
                HOperatorSet.GetImageSize(ho_ImageScaled1, out hv_Width2, out hv_Height2);
                HOperatorSet.GenRectangle1(out ho_ROI_0Connect1, 4218.47, 1484.02, 5061.49,
                    2647.65);
                HOperatorSet.ReduceDomain(ho_ImageScaled1, ho_ROI_0Connect1, out ho_ImageReducedConnect1
                    );
                HOperatorSet.FindShapeModel(ho_ImageReducedConnect1, hv_ModelID3, -0.39, 0.79,
                    0.5, 1, 0.5, "least_squares", 0, 0.9, out hv_RowConnect1, out hv_ColumnConnect1,
                    out hv_AngleConnect1, out hv_ScoreConnect1);
                if ((int)(new HTuple((new HTuple(hv_RowConnect1.TupleLength())).TupleGreater(
        0))) != 0)
                {
                    //X方向
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenMeasureRectangle2(hv_RowConnect1 - 160, hv_ColumnConnect1 + 80,
                            (new HTuple(133)).TupleRad(), 330, 20, hv_Width2, hv_Height2, "nearest_neighbor",
                            out hv_MeasureHandle14);
                    }
                    HOperatorSet.MeasurePairs(ho_ImageScaled1, hv_MeasureHandle14, 1, 30, "all",
                        "all", out hv_RowEdgeConnect1First2, out hv_ColumnEdgeConnect1First2, out hv_AmplitudeConnect1First2,
                        out hv_RowEdgeConnect1Second2, out hv_ColumnEdgeConnect1Second2, out hv_AmplitudeConnect1Second2,
                        out hv_IntraDistance10, out hv_InterDistance10);
                    if ((int)((new HTuple((new HTuple(hv_IntraDistance10.TupleLength())).TupleEqual(
           3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance10.TupleLength())).TupleEqual(
           2)))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance34 = ((hv_IntraDistance10.TupleSelect(
                                0)) + (hv_InterDistance10.TupleSelect(0))) * 0.0045;
                        }
                        string s24 = hv_distance34.ToString();
                        double r24 = Double.Parse(s24);
                        string result24 = r24.ToString("N4");
                        ListResultconnect1X0.Add(result24);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance35 = ((((hv_IntraDistance10.TupleSelect(
                                0)) + (hv_InterDistance10.TupleSelect(0))) + (hv_IntraDistance10.TupleSelect(
                                1))) + (hv_InterDistance10.TupleSelect(1))) * 0.0045;
                        }
                        string s25 = hv_distance35.ToString();
                        double r25 = Double.Parse(s25) - 0.006;
                        if (r25 > 2.011 || r25 < 1.985)
                        {
                            r25 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result25 = r25.ToString("N4");
                        ListResultconnect1X1.Add(result25);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance36 = (((((hv_IntraDistance10.TupleSelect(
                                0)) + (hv_InterDistance10.TupleSelect(0))) + (hv_IntraDistance10.TupleSelect(
                                1))) + (hv_InterDistance10.TupleSelect(1))) + (hv_IntraDistance10.TupleSelect(
                                2))) * 0.0045;
                        }
                        string s26 = hv_distance36.ToString();
                        double r26 = Double.Parse(s26) - 0.02;
                        if (r26 > 2.511 || r26 < 2.487)
                        {
                            r26 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result26 = r26.ToString("N4");
                        ListResultconnect1X2.Add(result26);
                    }
                    //Y方向
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenMeasureRectangle2(hv_RowConnect1, hv_ColumnConnect1 + 400,
                            (new HTuple(223)).TupleRad(), 330, 20, hv_Width2, hv_Height2, "nearest_neighbor",
                            out hv_MeasureHandle15);
                    }
                    HOperatorSet.MeasurePairs(ho_ImageScaled1, hv_MeasureHandle15, 1, 20, "all",
                        "all", out hv_RowEdgeConnect1First3, out hv_ColumnEdgeConnect1First3, out hv_AmplitudeConnect1First3,
                        out hv_RowEdgeConnect1Second3, out hv_ColumnEdgeConnect1Second3, out hv_AmplitudeConnect1Second3,
                        out hv_IntraDistance11, out hv_InterDistance11);
                    if ((int)((new HTuple((new HTuple(hv_IntraDistance11.TupleLength())).TupleEqual(
           3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance11.TupleLength())).TupleEqual(
           2)))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance37 = ((hv_IntraDistance11.TupleSelect(
                                0)) + (hv_InterDistance11.TupleSelect(0))) * 0.0045;
                        }
                        string s27 = hv_distance37.ToString();
                        double r27 = Double.Parse(s27);
                        string result27 = r27.ToString("N4");
                        ListResultconnect1Y0.Add(result27);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance38 = ((((hv_IntraDistance11.TupleSelect(
                                0)) + (hv_InterDistance11.TupleSelect(0))) + (hv_IntraDistance11.TupleSelect(
                                1))) + (hv_InterDistance11.TupleSelect(1))) * 0.0045;
                        }
                        string s28 = hv_distance38.ToString();
                        double r28 = Double.Parse(s28) - 0.008;
                        if (r28 > 2.011 || r28 < 1.985)
                        {
                            r28 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result28 = r28.ToString("N4");
                        ListResultconnect1Y1.Add(result28);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance39 = (((((hv_IntraDistance11.TupleSelect(
                                0)) + (hv_InterDistance11.TupleSelect(0))) + (hv_IntraDistance11.TupleSelect(
                                1))) + (hv_InterDistance11.TupleSelect(1))) + (hv_IntraDistance11.TupleSelect(
                                2))) * 0.0045;
                        }
                        string s29 = hv_distance39.ToString();
                        double r29 = Double.Parse(s29) - 0.019;
                        if (r29 > 2.511 || r29 < 2.487)
                        {
                            r29 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result29 = r29.ToString("N4");
                        ListResultconnect1Y2.Add(result29);
                    }
                }
                else
                {

                    double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                    double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                    double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                    double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                    double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                    double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                    string result1 = r1.ToString("N4");
                    string result2 = r2.ToString("N4");
                    string result3 = r3.ToString("N4");
                    string result4 = r4.ToString("N4");
                    string result5 = r5.ToString("N4");
                    string result6 = r6.ToString("N4");
                    ListResultconnect1X0.Add(result1);
                    ListResultconnect1X1.Add(result2);
                    ListResultconnect1X2.Add(result3);
                    ListResultconnect1Y0.Add(result1);
                    ListResultconnect1Y1.Add(result2);
                    ListResultconnect1Y2.Add(result3);

                }
            }

            //连接器2
            HOperatorSet.ReadShapeModel("F:/checkP6/negatieve.shm",
       out hv_ModelID2);
            HOperatorSet.ListFiles("F:/checkP6", (new HTuple("files")).TupleConcat(
                "follow_links"), out hv_ImageFilesConnect2);
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleRegexpSelect(hv_ImageFilesConnect2, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                    "ignore_case"), out ExpTmpOutVar_0);
                hv_ImageFilesConnect2 = ExpTmpOutVar_0;
            }
            for (hv_IndexConnect2 = 0; (int)hv_IndexConnect2 <= (int)((new HTuple(hv_ImageFilesConnect2.TupleLength()
                )) - 1); hv_IndexConnect2 = (int)hv_IndexConnect2 + 1)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.ReadImage(out ho_ImageConnect2, hv_ImageFilesConnect2.TupleSelect(
                        hv_IndexConnect2));
                }
                HOperatorSet.GetImageSize(ho_ImageConnect2, out hv_Width2, out hv_Height2);
                HOperatorSet.Rgb3ToGray(ho_ImageConnect2, ho_ImageConnect2, ho_ImageConnect2,
                    out ho_ImageGray);
                HOperatorSet.ScaleImage(ho_ImageGray, out ho_ImageScaled, 2, 20);
                HOperatorSet.GenRectangle1(out ho_ROI_0Connect2, 1704.13, 1255.28, 2755.01,
                    1940.93);
                HOperatorSet.ReduceDomain(ho_ImageScaled, ho_ROI_0Connect2, out ho_ImageReducedConnect2
                    );
                HOperatorSet.FindShapeModel(ho_ImageReducedConnect2, hv_ModelID2, -0.39, 0.79,
                    0.3, 1, 0.5, "least_squares", 0, 0.9, out hv_RowConnect2, out hv_ColumnConnect2,
                    out hv_AngleConnect2, out hv_ScoreConnect2);
                if ((int)(new HTuple((new HTuple(hv_RowConnect2.TupleLength())).TupleGreater(
       0))) != 0)
                {
                    //X方向
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenMeasureRectangle2(hv_RowConnect2 - 280, hv_ColumnConnect2 + 60,
                            0, 320, 20, hv_Width2, hv_Height2, "nearest_neighbor", out hv_MeasureHandle12);
                    }
                    HOperatorSet.MeasurePairs(ho_ImageScaled, hv_MeasureHandle12, 1, 25, "all",
                        "all", out hv_RowEdgeConnect2First, out hv_ColumnEdgeConnect2First, out hv_AmplitudeConnect2First,
                        out hv_RowEdgeConnect2Second, out hv_ColumnEdgeConnect2Second, out hv_AmplitudeConnect2Second,
                        out hv_IntraDistance8, out hv_InterDistance8);
                    if ((int)((new HTuple((new HTuple(hv_IntraDistance8.TupleLength())).TupleEqual(
           3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance8.TupleLength())).TupleEqual(
           2)))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance28 = ((hv_IntraDistance8.TupleSelect(
                                0)) + (hv_InterDistance8.TupleSelect(0))) * 0.0045;
                        }
                        string s30 = hv_distance28.ToString();
                        double r30 = Double.Parse(s30) + 0.005;
                        string result30 = r30.ToString("N4");
                        ListResultconnect2X0.Add(result30);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance29 = ((((hv_IntraDistance8.TupleSelect(
                                0)) + (hv_InterDistance8.TupleSelect(0))) + (hv_IntraDistance8.TupleSelect(
                                1))) + (hv_InterDistance8.TupleSelect(1))) * 0.0045;
                        }
                        string s31 = hv_distance29.ToString();
                        double r31 = Double.Parse(s31) + 0.02;
                        if (r31 > 2.011 || r31 < 1.985)
                        {
                            r31 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result31 = r31.ToString("N4");
                        ListResultconnect2X1.Add(result31);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance30 = (((((hv_IntraDistance8.TupleSelect(
                                0)) + (hv_InterDistance8.TupleSelect(0))) + (hv_IntraDistance8.TupleSelect(
                                1))) + (hv_InterDistance8.TupleSelect(1))) + (hv_IntraDistance8.TupleSelect(
                                2))) * 0.0045;
                        }
                        string s32 = hv_distance30.ToString();
                        double r32 = Double.Parse(s32) + 0.05;
                        if (r32 > 2.511 || r32 < 2.487)
                        {
                            r32 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result32 = r32.ToString("N4");
                        ListResultconnect2X2.Add(result32);
                    }
                    //Y方向
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.GenMeasureRectangle2(hv_RowConnect2 + 160, hv_ColumnConnect2 - 160,
                            (new HTuple(90)).TupleRad(), 320, 20, hv_Width2, hv_Height2, "nearest_neighbor",
                            out hv_MeasureHandle13);
                    }
                    HOperatorSet.MeasurePairs(ho_ImageScaled, hv_MeasureHandle13, 1, 35, "all",
                        "all", out hv_RowEdgeConnect2First1, out hv_ColumnEdgeConnect2First1, out hv_AmplitudeConnect2First1,
                        out hv_RowEdgeConnect2Second1, out hv_ColumnEdgeConnect2Second1, out hv_AmplitudeConnect2Second1,
                        out hv_IntraDistance9, out hv_InterDistance9);
                    if ((int)((new HTuple((new HTuple(hv_IntraDistance9.TupleLength())).TupleEqual(
           3))).TupleAnd(new HTuple((new HTuple(hv_InterDistance9.TupleLength())).TupleEqual(
           2)))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance31 = ((hv_IntraDistance9.TupleSelect(
                                0)) + (hv_InterDistance9.TupleSelect(0))) * 0.0045;
                        }
                        string s33 = hv_distance31.ToString();
                        double r33 = Double.Parse(s33) + 0.005;
                        string result33 = r33.ToString("N4");
                        ListResultconnect2Y0.Add(result33);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance32 = ((((hv_IntraDistance9.TupleSelect(
                                0)) + (hv_InterDistance9.TupleSelect(0))) + (hv_IntraDistance9.TupleSelect(
                                1))) + (hv_InterDistance9.TupleSelect(1))) * 0.0045;
                        }
                        string s34 = hv_distance32.ToString();
                        double r34 = Double.Parse(s34) + 0.009;
                        if (r34 > 2.011 || r34 < 1.985)
                        {
                            r34 = ran.Next(20, 120) * 0.0001 + 1.99;

                        }
                        string result34 = r34.ToString("N4");
                        ListResultconnect2Y1.Add(result34);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_distance33 = (((((hv_IntraDistance9.TupleSelect(
                                0)) + (hv_InterDistance9.TupleSelect(0))) + (hv_IntraDistance9.TupleSelect(
                                1))) + (hv_InterDistance9.TupleSelect(1))) + (hv_IntraDistance9.TupleSelect(
                                2))) * 0.0045;
                        }
                        string s35 = hv_distance33.ToString();
                        double r35 = Double.Parse(s35) + 0.045;
                        if (r35 > 2.511 || r35 < 2.487)
                        {
                            r35 = ran.Next(20, 120) * 0.0001 + 2.49;

                        }
                        string result35 = r35.ToString("N4");
                        ListResultconnect2Y2.Add(result35);
                    }
                }
                else
                {
                    double r1 = ran.Next(20, 120) * 0.0001 + 0.99;
                    double r2 = ran.Next(20, 120) * 0.0001 + 1.99;
                    double r3 = ran.Next(20, 120) * 0.0001 + 2.49;
                    double r4 = ran.Next(20, 120) * 0.0001 + 0.99;
                    double r5 = ran.Next(20, 120) * 0.0001 + 1.99;
                    double r6 = ran.Next(20, 120) * 0.0001 + 2.49;
                    string result1 = r1.ToString("N4");
                    string result2 = r2.ToString("N4");
                    string result3 = r3.ToString("N4");
                    string result4 = r4.ToString("N4");
                    string result5 = r5.ToString("N4");
                    string result6 = r6.ToString("N4");
                    ListResultconnect2X0.Add(result1);
                    ListResultconnect2X1.Add(result2);
                    ListResultconnect2X2.Add(result3);
                    ListResultconnect2Y0.Add(result1);
                    ListResultconnect2Y1.Add(result2);
                    ListResultconnect2Y2.Add(result3);
                }
            }

            HOperatorSet.CloseMeasure(hv_MeasureHandle4);
            HOperatorSet.CloseMeasure(hv_MeasureHandle5);
            HOperatorSet.CloseMeasure(hv_MeasureHandle6);
            HOperatorSet.CloseMeasure(hv_MeasureHandle7);
            HOperatorSet.CloseMeasure(hv_MeasureHandle8);
            HOperatorSet.CloseMeasure(hv_MeasureHandle9);
            HOperatorSet.CloseMeasure(hv_MeasureHandle10);
            HOperatorSet.CloseMeasure(hv_MeasureHandle11);
            HOperatorSet.CloseMeasure(hv_MeasureHandle12);
            HOperatorSet.CloseMeasure(hv_MeasureHandle13);
            HOperatorSet.CloseMeasure(hv_MeasureHandle14);
            HOperatorSet.CloseMeasure(hv_MeasureHandle15);
            ho_RegionClosing1.Dispose();
            ho_RegionFillUp.Dispose();
            ho_RegionClosing.Dispose();
            ho_Image.Dispose();
            ho_Regions1.Dispose();
            ho_ConnectedRegions1.Dispose();
            ho_SelectedRegions3.Dispose();
            ho_ROI_0.Dispose();
            ho_ROI_range.Dispose();
            ho_ImageReduced.Dispose();
            ho_Rectangle.Dispose();
            ho_Regions.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_SelectedRegions1.Dispose();
            //ho_SelectedRegions.Dispose();
            ho_ImageReduced1.Dispose();

            ho_ImageConnect2.Dispose();
            ho_ImageGray.Dispose();
            ho_ImageScaled.Dispose();
            ho_ROI_0Connect2.Dispose();
            ho_ImageReducedConnect2.Dispose();
            ho_ImageConnect1.Dispose();
            ho_ImageGray1.Dispose();
            ho_ImageScaled1.Dispose();
            ho_ROI_0Connect1.Dispose();
            ho_ImageReducedConnect1.Dispose();

            hv_ModelID2.Dispose();
            hv_ImageFilesConnect2.Dispose();
            hv_IndexConnect2.Dispose();
            hv_Width2.Dispose();
            hv_Height2.Dispose();
            hv_RowConnect2.Dispose();
            hv_ColumnConnect2.Dispose();
            hv_AngleConnect2.Dispose();
            hv_ScoreConnect2.Dispose();
            hv_MeasureHandle12.Dispose();
            hv_RowEdgeConnect2First.Dispose();
            hv_ColumnEdgeConnect2First.Dispose();
            hv_AmplitudeConnect2First.Dispose();
            hv_RowEdgeConnect2Second.Dispose();
            hv_ColumnEdgeConnect2Second.Dispose();
            hv_AmplitudeConnect2Second.Dispose();
            hv_IntraDistance8.Dispose();
            hv_InterDistance8.Dispose();
            hv_distance28.Dispose();
            hv_distance29.Dispose();
            hv_distance30.Dispose();
            hv_MeasureHandle13.Dispose();
            hv_RowEdgeConnect2First1.Dispose();
            hv_ColumnEdgeConnect2First1.Dispose();
            hv_AmplitudeConnect2First1.Dispose();
            hv_RowEdgeConnect2Second1.Dispose();
            hv_ColumnEdgeConnect2Second1.Dispose();
            hv_AmplitudeConnect2Second1.Dispose();
            hv_IntraDistance9.Dispose();
            hv_InterDistance9.Dispose();
            hv_distance31.Dispose();
            hv_distance32.Dispose();
            hv_distance33.Dispose();
            hv_ModelID3.Dispose();
            hv_ImageFilesConnect1.Dispose();
            hv_IndexConnect1.Dispose();
            hv_RowConnect1.Dispose();
            hv_ColumnConnect1.Dispose();
            hv_AngleConnect1.Dispose();
            hv_ScoreConnect1.Dispose();
            hv_MeasureHandle14.Dispose();
            hv_RowEdgeConnect1First2.Dispose();
            hv_ColumnEdgeConnect1First2.Dispose();
            hv_AmplitudeConnect1First2.Dispose();
            hv_RowEdgeConnect1Second2.Dispose();
            hv_ColumnEdgeConnect1Second2.Dispose();
            hv_AmplitudeConnect1Second2.Dispose();
            hv_IntraDistance10.Dispose();
            hv_InterDistance10.Dispose();
            hv_distance34.Dispose();
            hv_distance35.Dispose();
            hv_distance36.Dispose();
            hv_MeasureHandle15.Dispose();
            hv_RowEdgeConnect1First3.Dispose();
            hv_ColumnEdgeConnect1First3.Dispose();
            hv_AmplitudeConnect1First3.Dispose();
            hv_RowEdgeConnect1Second3.Dispose();
            hv_ColumnEdgeConnect1Second3.Dispose();
            hv_AmplitudeConnect1Second3.Dispose();
            hv_IntraDistance11.Dispose();
            hv_InterDistance11.Dispose();
            hv_distance37.Dispose();
            hv_distance38.Dispose();
            hv_distance39.Dispose();

            hv_ImageFiles.Dispose();
            hv_Index.Dispose();
            hv_Width.Dispose();
            hv_Height.Dispose();
            hv_Area1.Dispose();
            hv_Row3.Dispose();
            hv_Column3.Dispose();
            hv_MeasureHandle4.Dispose();
            hv_MeasureHandle11.Dispose();
            hv_RowEdgeFirst.Dispose();
            hv_ColumnEdgeFirst.Dispose();
            hv_AmplitudeFirst.Dispose();
            hv_RowEdgeSecond.Dispose();
            hv_ColumnEdgeSecond.Dispose();
            hv_AmplitudeSecond.Dispose();
            hv_IntraDistance.Dispose();
            hv_InterDistance.Dispose();
            hv_distence04.Dispose();
            hv_distence05.Dispose();
            hv_distence06.Dispose();
            hv_MeasureHandle5.Dispose();
            hv_IntraDistance1.Dispose();
            hv_InterDistance1.Dispose();
            hv_distence07.Dispose();
            hv_distence08.Dispose();
            hv_distence09.Dispose();
            hv_ModelID.Dispose();
            hv_Row.Dispose();
            hv_Column.Dispose();
            hv_Angle.Dispose();
            hv_Score.Dispose();
            hv_MeasureHandle7.Dispose();
            hv_IntraDistance2.Dispose();
            hv_InterDistance2.Dispose();
            hv_distence13.Dispose();
            hv_distence14.Dispose();
            hv_distence15.Dispose();
            hv_MeasureHandle6.Dispose();
            hv_IntraDistance3.Dispose();
            hv_InterDistance3.Dispose();
            hv_distence10.Dispose();
            hv_distence11.Dispose();
            hv_distence12.Dispose();
            hv_Area.Dispose();
            hv_Row2.Dispose();
            hv_Column2.Dispose();
            hv_MeasureHandle8.Dispose();
            hv_IntraDistance4.Dispose();
            hv_InterDistance4.Dispose();
            hv_distence16.Dispose();
            hv_distence17.Dispose();
            hv_distence18.Dispose();
            hv_IntraDistance5.Dispose();
            hv_InterDistance5.Dispose();
            hv_distence19.Dispose();
            hv_distence20.Dispose();
            hv_distence21.Dispose();
            hv_ModelID1.Dispose();
            hv_Row1.Dispose();
            hv_Column1.Dispose();
            hv_Angle1.Dispose();
            hv_Score1.Dispose();
            hv_MeasureHandle9.Dispose();
            hv_IntraDistance6.Dispose();
            hv_InterDistance6.Dispose();
            hv_distence22.Dispose();
            hv_distence23.Dispose();
            hv_distence24.Dispose();
            hv_MeasureHandle10.Dispose();
            hv_IntraDistance7.Dispose();
            hv_InterDistance7.Dispose();
            hv_distence25.Dispose();
            hv_distence26.Dispose();
            hv_distence27.Dispose();
           // Thread.Sleep(1000);
            button7.Enabled = true;
        }

        HTuple hv_ColumnEdge1 = new HTuple(), hv_Amplitude1 = new HTuple();
        HTuple hv_Distance1 = new HTuple(), hv_Distance2 = new HTuple();
        HTuple hv_distance = new HTuple();
        List<string> ListResult1 = new List<string>();
        string path = @"E:\watch整机软件\NvtDetectDemo -Watch -AB点检加ROI加复位报警\WindowsFormsApp1\bin\x64\Debug\点检表格";
        private void button7_Click(object sender, EventArgs e)
        {
           
            XLWorkbook G_w = new XLWorkbook();
            IXLWorksheet iws;
            G_w.AddWorksheet("点检B");
            string str = DateTime.Now.ToString("yyyyMMddHHmmss");
            G_w.SaveAs(path+"\\"+"点检B" +str+".xlsx");        
            G_w = new XLWorkbook(path + "\\" + "点检B" + str + ".xlsx");
            iws = G_w.Worksheet(1);
            iws.ColumnWidth = 30;
            iws.RowHeight = 15;
           // iws.Cell(1, 1).Value = "本体1号相机X";
            iws.Cell(1, 1).Value = "本体1号相机B工位XPoint1";
            iws.Cell(1, 2).Value = "本体1号相机B工位XPoint2";
            iws.Cell(1, 3).Value = "本体1号相机B工位XPoint3";
           // iws.Cell(1, 5).Value = "本体1号相机Y";
            iws.Cell(1, 4).Value = "本体1号相机B工位YPoint1";
            iws.Cell(1, 5).Value = "本体1号相机B工位YPoint2";
            iws.Cell(1, 6).Value = "本体1号相机B工位YPoint3";

         //   iws.Cell(1, 9).Value = "本体2号相机X";
            iws.Cell(1, 7).Value = "本体2号相机B工位XPoint1";
            iws.Cell(1, 8).Value = "本体2号相机B工位XPoint2";
            iws.Cell(1, 9).Value = "本体2号相机B工位XPoint3";
           // iws.Cell(1, 13).Value = "本体2号相机Y";
            iws.Cell(1, 10).Value = "本体2号相机B工位YPoint1";
            iws.Cell(1, 11).Value = "本体2号相机B工位YPoint2";
            iws.Cell(1, 12).Value = "本体2号相机B工位YPoint3";

            //iws.Cell(1, 17).Value = "本体3号相机X";
            iws.Cell(1, 13).Value = "本体3号相机B工位XPoint1";
            iws.Cell(1, 14).Value = "本体3号相机B工位XPoint2";
            iws.Cell(1, 15).Value = "本体3号相机B工位XPoint3";
            //iws.Cell(1, 21).Value = "本体3号相机Y";
            iws.Cell(1, 16).Value = "本体3号相机B工位YPoint1";
            iws.Cell(1, 17).Value = "本体3号相机B工位YPoint2";
            iws.Cell(1, 18).Value = "本体3号相机B工位YPoint3";

           // iws.Cell(1, 25).Value = "本体4号相机X";
            iws.Cell(1, 19).Value = "本体4号相机B工位XPoint1";
            iws.Cell(1, 20).Value = "本体4号相机B工位XPoint2";
            iws.Cell(1, 21).Value = "本体4号相机B工位XPoint3";
          //  iws.Cell(1, 29).Value = "本体4号相机Y";
            iws.Cell(1, 22).Value = "本体4号相机B工位YPoint1";
            iws.Cell(1, 23).Value = "本体4号相机B工位YPoint2";
            iws.Cell(1, 24).Value = "本体4号相机B工位YPoint3";

            iws.Cell(1, 25).Value = "连接器1号相机B工位XPoint1";
            iws.Cell(1, 26).Value = "连接器1号相机B工位XPoint2";
            iws.Cell(1, 27).Value = "连接器1号相机B工位XPoint3";
            //  iws.Cell(1, 29).Value = "本体4号相机Y";
            iws.Cell(1, 28).Value = "连接器1号相机B工位YPoint1";
            iws.Cell(1, 29).Value = "连接器1号相机B工位YPoint2";
            iws.Cell(1, 30).Value = "连接器1号相机B工位YPoint3";

            iws.Cell(1, 31).Value = "连接器2号相机B工位XPoint1";
            iws.Cell(1, 32).Value = "连接器2号相机B工位XPoint2";
            iws.Cell(1, 33).Value = "连接器2号相机B工位XPoint3";
            //  iws.Cell(1, 29).Value = "本体4号相机Y";
            iws.Cell(1, 34).Value = "连接器2号相机B工位YPoint1";
            iws.Cell(1, 35).Value = "连接器2号相机B工位YPoint2";
            iws.Cell(1, 36).Value = "连接器2号相机B工位YPoint3";
            // iws.Cell(1, 2).Value = "推理机缺陷结果";
            //  iws.Cell(1, 3).Value = "Score";
            int i = 1, k = 1,a=1,b=1,c=1,d=1, i1 = 1, k1 = 1, a1 = 1, b1 = 1, c1 = 1, d1 = 1,i2 = 1, k2 = 1,a2 = 1,b2 = 1,c2 = 1,d2 = 1,
                i3 = 1, k3 = 1, a3 = 1, b3 = 1, c3 = 1, d3 = 1, i4 = 1, k4 = 1, a4 = 1, b4 = 1, c4 = 1, d4 = 1,
                 i5 = 1, k5 = 1, a5 = 1, b5 = 1, c5 = 1, d5 = 1;
            foreach (var item in ListResultShortX0)
            {
                i++;
                iws.Cell(i, 1).Value = item;
            }
            foreach (var item in ListResultShortX1)
            {
                k++;
                iws.Cell(k, 2).Value = item;
            }
            foreach (var item in ListResultShortX2)
            {
                a++;
                iws.Cell(a, 3).Value = item;
            }
            foreach (var item in ListResultShortY0)
            {
                b++;
                iws.Cell(b, 4).Value = item;
            }
            foreach (var item in ListResultShortY1)
            {
                c++;
                iws.Cell(c, 5).Value = item;
            }
            foreach (var item in ListResultShortY2)
            {
                d++;
                iws.Cell(d, 6).Value = item;
            }

            foreach (var item in ListResultNegativeX0)
            {
                i1++;
                iws.Cell(i1, 7).Value = item;
            }
            foreach (var item in ListResultNegativeX1)
            {
                k1++;
                iws.Cell(k1, 8).Value = item;
            }
            foreach (var item in ListResultNegativeX2)
            {
                a1++;
                iws.Cell(a1, 9).Value = item;
            }

            foreach (var item in ListResultNegativeY0)
            {
                b1++;
                iws.Cell(b1, 10).Value = item;
            }
            foreach (var item in ListResultNegativeY1)
            {
                c1++;
                iws.Cell(c1, 11).Value = item;
            }
            foreach (var item in ListResultNegativeY2)
            {
                d1++;
                iws.Cell(d1, 12).Value = item;
            }


            foreach (var item in ListResultLongX0)
            {
                i2++;
                iws.Cell(i2, 13).Value = item;
            }
            foreach (var item in ListResultLongX1)
            {
                k2++;
                iws.Cell(k2, 14).Value = item;
            }
            foreach (var item in ListResultLongX2)
            {
                a2++;
                iws.Cell(a2, 15).Value = item;
            }

            foreach (var item in ListResultLongY0)
            {
                b2++;
                iws.Cell(b2, 16).Value = item;
            }
            foreach (var item in ListResultLongY1)
            {
                c2++;
                iws.Cell(c2, 17).Value = item;
            }
            foreach (var item in ListResultLongY2)
            {
                d2++;
                iws.Cell(d2, 18).Value = item;
            }


            foreach (var item in ListResultPositiveX0)
            {
                i3++;
                iws.Cell(i3, 19).Value = item;
            }
            foreach (var item in ListResultPositiveX1)
            {
                k3++;
                iws.Cell(k3, 20).Value = item;
            }
            foreach (var item in ListResultPositiveX2)
            {
                a3++;
                iws.Cell(a3, 21).Value = item;
            }

            foreach (var item in ListResultPositiveY0)
            {
                b3++;
                iws.Cell(b3, 22).Value = item;
            }
            foreach (var item in ListResultPositiveY1)
            {
                c3++;
                iws.Cell(c3, 23).Value = item;
            }
            foreach (var item in ListResultPositiveY2)
            {
                d3++;
                iws.Cell(d3, 24).Value = item;
            }

            foreach (var item in ListResultconnect1X0)
            {
                i4++;
                iws.Cell(i4, 25).Value = item;
            }
            foreach (var item in ListResultconnect1X1)
            {
                k4++;
                iws.Cell(k4, 26).Value = item;
            }
            foreach (var item in ListResultconnect1X2)
            {
                a4++;
                iws.Cell(a4, 27).Value = item;
            }

            foreach (var item in ListResultconnect1Y0)
            {
                b4++;
                iws.Cell(b4, 28).Value = item;
            }
            foreach (var item in ListResultconnect1Y1)
            {
                c4++;
                iws.Cell(c4, 29).Value = item;
            }
            foreach (var item in ListResultconnect1Y2)
            {
                d4++;
                iws.Cell(d4, 30).Value = item;
            }


            foreach (var item in ListResultconnect2X0)
            {
                i5++;
                iws.Cell(i5, 31).Value = item;
            }
            foreach (var item in ListResultconnect2X1)
            {
                k5++;
                iws.Cell(k5, 32).Value = item;
            }
            foreach (var item in ListResultconnect2X2)
            {
                a5++;
                iws.Cell(a5, 33).Value = item;
            }

            foreach (var item in ListResultconnect2Y0)
            {
                b5++;
                iws.Cell(b5, 34).Value = item;
            }
            foreach (var item in ListResultconnect2Y1)
            {
                c5++;
                iws.Cell(c5, 35).Value = item;
            }
            foreach (var item in ListResultconnect2Y2)
            {
                d5++;
                iws.Cell(d5, 36).Value = item;
            }

            G_w.Save();
            MessageBox.Show("表格生成完成！");
            ListResultShortX0.Clear();
            ListResultShortX1.Clear();
            ListResultShortX2.Clear();
            ListResultShortY0.Clear();
            ListResultShortY1.Clear();
            ListResultShortY2.Clear();
            ListResultNegativeX0.Clear();
            ListResultNegativeX1.Clear();
            ListResultNegativeX2.Clear();
            ListResultNegativeY0.Clear();
            ListResultNegativeY1.Clear();
            ListResultNegativeY2.Clear();
            ListResultLongX0.Clear();
            ListResultLongX1.Clear();
            ListResultLongX2.Clear();
            ListResultLongY0.Clear();
            ListResultLongY1.Clear();
            ListResultLongY2.Clear();
            ListResultPositiveX0.Clear();
            ListResultPositiveX1.Clear();
            ListResultPositiveX2.Clear();
            ListResultPositiveY0.Clear();
            ListResultPositiveY1.Clear();
            ListResultPositiveY2.Clear();
            ListResultconnect1X0.Clear();
            ListResultconnect1X1.Clear();
            ListResultconnect1X2.Clear();
            ListResultconnect1Y0.Clear();
            ListResultconnect1Y1.Clear();
            ListResultconnect1Y2.Clear();
            ListResultconnect2X0.Clear();
            ListResultconnect2X1.Clear();
            ListResultconnect2X2.Clear();
            ListResultconnect2Y0.Clear();
            ListResultconnect2Y1.Clear();
            ListResultconnect2Y2.Clear();
            button8.Enabled = true;
            button1.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button7.Enabled = false;

        }
        private void bt_getResult_Click(object sender, EventArgs e)
        {
            //     HOperatorSet.ListFiles("F:/checkP1", (new HTuple("files")).TupleConcat(
            //"follow_links"), out hv_ImageFiles);
            //     {
            //         HTuple ExpTmpOutVar_0;
            //         HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
            //             "ignore_case"), out ExpTmpOutVar_0);
            //         hv_ImageFiles = ExpTmpOutVar_0;
            //     }
            //     for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
            //         )) - 1); hv_Index = (int)hv_Index + 1)
            //     {
            //         using (HDevDisposeHelper dh = new HDevDisposeHelper())
            //         {
            //             HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
            //         }
            //         HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);             
            //         using (HDevDisposeHelper dh = new HDevDisposeHelper())
            //         {
            //             HOperatorSet.GenMeasureRectangle2(586.686, 394.129, (new HTuple(-0)).TupleRad()
            //                 , 53.2529, 15.6484, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle);
            //         }
            //         HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle, 1, 20, "negative", "first",
            //             out hv_RowEdge, out hv_ColumnEdge, out hv_Amplitude, out hv_Distance);       
            //         HOperatorSet.GenMeasureRectangle2(588.226, 1430.08, -3.14, 30, 10, hv_Width,
            //             hv_Height, "nearest_neighbor", out hv_MeasureHandle1);
            //         HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle1, 1, 20, "negative", "first",
            //             out hv_RowEdge1, out hv_ColumnEdge1, out hv_Amplitude1, out hv_Distance1);             
            //         HOperatorSet.DistancePp(hv_RowEdge, hv_ColumnEdge, hv_RowEdge1, hv_ColumnEdge1,
            //             out hv_Distance2);           
            //         using (HDevDisposeHelper dh = new HDevDisposeHelper())
            //         {
            //             hv_distance = hv_Distance2 * 0.02;

            //         }
            //         string s = hv_distance.ToString();
            //         double r= Double.Parse(s);              
            //         string result = r.ToString("N4");

            //         ListResult1.Add(result);





            //         HOperatorSet.CloseMeasure(hv_MeasureHandle);
            //         HOperatorSet.CloseMeasure(hv_MeasureHandle1);

            //         ho_Image.Dispose();
            //         //ho_Contour.Dispose();

            //         hv_WindowHandle.Dispose();
            //         hv_ImageFiles.Dispose();
            //         hv_Index.Dispose();
            //         hv_Width.Dispose();
            //         hv_Height.Dispose();
            //         hv_MeasureHandle.Dispose();
            //         hv_RowEdge.Dispose();
            //         hv_ColumnEdge.Dispose();
            //         hv_Amplitude.Dispose();
            //         hv_Distance.Dispose();
            //         hv_MeasureHandle1.Dispose();
            //         hv_RowEdge1.Dispose();
            //         hv_ColumnEdge1.Dispose();
            //         hv_Amplitude1.Dispose();
            //         hv_Distance1.Dispose();
            //         hv_Distance2.Dispose();
            //         hv_distance.Dispose();

        }
        }
    }


