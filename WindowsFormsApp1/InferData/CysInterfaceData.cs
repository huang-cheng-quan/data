using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestClientLib;

namespace WindowsFormsApp1.InferData
{
    public  class SourceParamLst 
    {
        public string model_name { get; set; } = "xnd_0-iwatch";
        public double threshold { get; set; } = 0.5;

        public bool with_mask = true;
        public string image{ get; set; }
    }

    public class MaskResultParam
    {
        public int category_id { get; set; }
        public string category_name { get; set; }
        public double [] bbox { get; set; }
        public List<double[]> mask;
        public double area { get; set; }
        public double score { get; set; }
        public bool raw_mask { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public double max_area { get; set; }
        public double min_width { get; set; }
        public double min_height { get; set; }
        public double mean_background { get; set; }
        public double mean_foreground { get; set; }
        public string id { get; set; }
    }

    public class ResultParam
    {
        public string code { get; set; }
        public string message { get; set; }

        //public List<PointF> shape_list { get; set; }

        public SourceParamLst input_config { get; set; }

        public List<MaskResultParam> result { get; set; }
    }


    public abstract class AIWeldInspectionBase
    {
        protected string _uriAddress = "http://127.0.0.1:5000/predict_list";

        protected string _modelName;
        protected bool _withMask = false;
        protected double _threshold = 0.5;


       /* public virtual bool Start(ImageInfo info, HImage hImageG, HImage hImageH, int imageHeight)
        {
            int cameraNumber = info.CameraNumber;
            int sideNumber = info.SideNumber;

            int index = 0;
            if (cameraNumber == 0 && sideNumber == 0)
            {
                index = 0;
            }
            else if (cameraNumber == 0 && sideNumber == 1)
            {
                index = 1;
            }
            else if (cameraNumber == 0 && sideNumber == 2)
            {
                index = 2;
            }
            else if (cameraNumber == 0 && sideNumber == 3)
            {
                index = 3;
            }
            else if (cameraNumber == 1 && sideNumber == 0)
            {
                index = 4;
            }
            else if (cameraNumber == 1 && sideNumber == 1)
            {
                index = 5;
            }
            else if (cameraNumber == 1 && sideNumber == 2)
            {
                index = 6;
            }
            else if (cameraNumber == 1 && sideNumber == 3)
            {
                index = 7;
            }

            List<ImageCutParam> paramList = ParamOper.GetParam().InsParam.ImageCutParamList;
            if (index >= paramList.Count)
            {
                LogHelper.Debug("ImageCutParamList超出索引：Sum = " + paramList.Count + ",Index=" + index);
                return false;
            }

            int height = imageHeight;

            HOperatorSet.GetImageSize(hImageH, out HTuple htW, out HTuple htH);

            int x = paramList[index].X;
            int y = 0;
            int w = paramList[index].Width;
            int h = height;


            HImage hImageCutH = hImageH.CropPart(y, x, w, h);
            HImage hImageCutG = hImageG.CropPart(y, x, w, h);

            if (!System.IO.Directory.Exists("Image//"))
            {
                System.IO.Directory.CreateDirectory("Image//");
            }

            List<HImage> sourceImages = new List<HImage>();
            sourceImages.Add(hImageCutG);
            sourceImages.Add(hImageCutH);
            sourceImages.Add(hImageCutG);

            RestClientLib.RestClientOperater oper = new RestClientLib.RestClientOperater(_uriAddress);
            oper.LogEvent += delegate (string msg)
            {
                LogHelper.Debug(msg);
            };

            var dispWindow = ParamOper.GetParam().SysParam.CameraWindowList[info.CameraNumber].DispWindowHandle;

            SourceParamLst sourceParam = new SourceParamLst();
            sourceParam.model_name = this._modelName;
            sourceParam.with_mask = this._withMask;
            sourceParam.threshold = this._threshold;

            bool res = oper.Excute<SourceParamLst, ResultParam>(sourceParam, sourceImages, out ResultParam resultParam);
            Postprocessing(info, resultParam, x, y, w, h, ref res);

            return true;
        }*/

       // protected abstract bool Postprocessing(ImageInfo info, ResultParam resultParam, double x, double y, double w, double h, ref bool res);
    }
}
