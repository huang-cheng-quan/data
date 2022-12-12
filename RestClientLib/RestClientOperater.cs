using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    public class stBBox : List<double>
    {

    }

    public interface ISourceParam
    {
        string image { get; set; }
    }

    public class RestClientOperater
    {
        private string urlAddress;

        public RestClientOperater(string urlAddress)
        {
            this.urlAddress = urlAddress;
        }

        public bool Excute<TSource, TResult>(TSource source, out TResult result) where TSource : ISourceParam
                                                                                   where TResult : class, new()
        {
            result = null;

            HttpWebRequest request = WebRequest.Create(urlAddress) as HttpWebRequest; //创建请求
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.MaximumResponseHeadersLength = 1024;
            request.Method = "POST";
            request.ContentType = "application/json";


            string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(source);

            byte[] jsonbyte = Encoding.UTF8.GetBytes(jsonstring);
            Stream postStream = request.GetRequestStream();
            postStream.Write(jsonbyte, 0, jsonbyte.Length);
            postStream.Close();
            //发送请求并获取相应回应数据
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;
                return false;
            }
            StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            string content = sr.ReadToEnd(); //获得响应字符串0

            result = Newtonsoft.Json.JsonConvert.DeserializeObject<TResult>(content);

            return true;
        }

        public bool Excute<TSource, TResult>(TSource source,List<string> imagePaths, out TResult result) where TSource : ISourceParam
                                                                                   where TResult : class, new()
        {
            if (imagePaths.Count>0)
            {
                result = null;
                var image_path1 = imagePaths.First();
                var image_data = File.ReadAllBytes(image_path1);
                var image_base64 = Convert.ToBase64String(image_data);
                source.image = image_base64;
                HttpWebRequest request = WebRequest.Create(urlAddress) as HttpWebRequest; //创建请求
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.MaximumResponseHeadersLength = 1024;
                request.Method = "POST";
                request.ContentType = "application/json";


                string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(source);

                byte[] jsonbyte = Encoding.UTF8.GetBytes(jsonstring);
                Stream postStream = request.GetRequestStream();
                postStream.Write(jsonbyte, 0, jsonbyte.Length);
                postStream.Close();
                //发送请求并获取相应回应数据
                HttpWebResponse res;
                try
                {
                    res = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    res = (HttpWebResponse)ex.Response;
                    return false;
                }
                StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                string content = sr.ReadToEnd(); //获得响应字符串0

                result = Newtonsoft.Json.JsonConvert.DeserializeObject<TResult>(content);
                imagePaths.RemoveAt(0);
                return true;
            }
            else
            {
                result = null;
                return false;
            }
         
        }

        public bool Excute<TSource, TResult>(TSource source, List<HalconDotNet.HImage> hoImages, out TResult result, string strID = "") where TSource : ISourceParam
                                                                                                                                        where TResult : class, new()
        {
            result = null;

            string[] strImageData = new string[hoImages.Count];
            HalconDotNet.HImage imageToSave = null;
            foreach (var item in hoImages)
            {
                int index = hoImages.IndexOf(item);
                if (index == 0)
                {
                    imageToSave = item.Clone();
                }
                else
                {
                    imageToSave = imageToSave.AppendChannel(item);
                }
            }

            if (imageToSave == null)
            {
                return false;
            }

            string file = strID + ".tiff";
            imageToSave.WriteImage("tiff", 0, file);
            OpenCvSharp.Mat mat = OpenCvSharp.Cv2.ImRead(file);
            var datasL = mat.ToBytes();
            string imageData = Convert.ToBase64String(datasL);

            source.image = imageData;

            HttpWebRequest request = WebRequest.Create(urlAddress) as HttpWebRequest; //创建请求
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.MaximumResponseHeadersLength = 1024;
            request.Method = "POST";
            request.ContentType = "application/json";

            string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(source);

            byte[] jsonbyte = Encoding.UTF8.GetBytes(jsonstring);
            Stream postStream = request.GetRequestStream();
            postStream.Write(jsonbyte, 0, jsonbyte.Length);
            postStream.Close();
            //发送请求并获取相应回应数据
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;
                return false;
            }
            StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            string content = sr.ReadToEnd(); //获得响应字符串0

            result = Newtonsoft.Json.JsonConvert.DeserializeObject<TResult>(content);

            return true;
        }

        public bool Excute<TSource, TResult>(TSource source, HalconDotNet.HImage hoImage, out TResult result, string strID = "") where TSource : ISourceParam
                                                                                                                                where TResult : class, new()
        {
            result = null;

            HalconDotNet.HImage imageToSave = hoImage.Clone();

            string file = strID + ".tiff";
            imageToSave.WriteImage("tiff", 0, file);
            OpenCvSharp.Mat mat = OpenCvSharp.Cv2.ImRead(file, OpenCvSharp.ImreadModes.AnyDepth);
            var datasL = mat.ToBytes();
            string imageData = Convert.ToBase64String(datasL);

            source.image = imageData;

            HttpWebRequest request = WebRequest.Create(urlAddress) as HttpWebRequest; //创建请求
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.MaximumResponseHeadersLength = 1024;
            request.Method = "POST";
            request.ContentType = "application/json";

            string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(source);

            byte[] jsonbyte = Encoding.UTF8.GetBytes(jsonstring);
            Stream postStream = request.GetRequestStream();
            postStream.Write(jsonbyte, 0, jsonbyte.Length);
            postStream.Close();
            //发送请求并获取相应回应数据
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;
                return false;
            }
            StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            string content = sr.ReadToEnd(); //获得响应字符串0

            result = Newtonsoft.Json.JsonConvert.DeserializeObject<TResult>(content);

            return true;
        }

        public bool Excute<TSource, TResult>(TSource source, OpenCvSharp.Mat mat, out TResult result, string strID = "") where TSource : ISourceParam
                                                                                                                        where TResult : class, new()
        {
            result = null;

            var datasL = mat.ToBytes();
            string imageData = Convert.ToBase64String(datasL);

            source.image = imageData;

            HttpWebRequest request = WebRequest.Create(urlAddress) as HttpWebRequest; //创建请求
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.MaximumResponseHeadersLength = 1024;
            request.Method = "POST";
            request.ContentType = "application/json";

            string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(source);

            byte[] jsonbyte = Encoding.UTF8.GetBytes(jsonstring);
            Stream postStream = request.GetRequestStream();
            postStream.Write(jsonbyte, 0, jsonbyte.Length);
            postStream.Close();
            //发送请求并获取相应回应数据
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;
                return false;
            }
            StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            string content = sr.ReadToEnd(); //获得响应字符串0

            result = Newtonsoft.Json.JsonConvert.DeserializeObject<TResult>(content);

            return true;
        }


        public bool ConvertMaskToHObject(List<stBBox> mask, stBBox bbox, out HalconDotNet.HRegion hoRegion, double threshold = 0.5)
        {
            hoRegion = null;

            int maskWidth = mask.Count;
            if (maskWidth == 0)
            {
                return false;
            }
            int maskHeight = mask[0].Count;

            if (bbox.Count != 4)
            {
                return false;
            }

            double boxWidth = bbox[2];
            double boxHeight = bbox[3];

            hoRegion = new HalconDotNet.HRegion();

            HalconDotNet.HTuple htRows = new HalconDotNet.HTuple();
            HalconDotNet.HTuple htCols = new HalconDotNet.HTuple();

            for (int indexX = 0; indexX < maskWidth; indexX++)
            {
                for (int indexY = 0; indexY < maskHeight; indexY++)
                {
                    double val = mask[indexY][indexX];
                    if (val > threshold)
                    {
                        htCols.Append(indexX);
                        htRows.Append(indexY);
                    }
                }
            }

            hoRegion.GenRegionPoints(htRows, htCols);
            hoRegion = hoRegion.ZoomRegion(boxWidth / maskWidth, boxHeight / maskHeight).MoveRegion((int)bbox[1], (int)bbox[0]);

            return true;
        }
    }
}
