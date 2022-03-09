using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.InferData
{
    public class Utils
    {

        public static bool CheckUrlConnected(string url)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Timeout = 100;
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }

            }
            catch (Exception)
            {
                return false;

            }
            return false;
        }

        /// <summary>
        /// Http Post 请求
        /// </summary>
        /// <param name="url">服务地址</param>
        /// <param name="content">请求内容</param>
        /// <returns></returns>
        public static string HttpPost(string url, string content, int timeout = 500)
        {
            string response = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            req.Timeout = timeout;
            if (content != null)
            {
                byte[] data = Encoding.UTF8.GetBytes(content);
                req.ContentLength = data.Length;
                try
                {
                    using (Stream reqStream = req.GetRequestStream())
                    {
                        reqStream.Write(data, 0, data.Length);
                        reqStream.Close();
                    }
                }
                catch (Exception ex)
                {

                    //LeeSDK.Log.WriteLog("HttpPost Error " + url + " content: " + content + " Exception:" + ex.ToString());
                    return null;
                }

            }

            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();

                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    response = reader.ReadToEnd();
                }

                return response;
            }
            catch (Exception ex)
            {
                //LeeSDK.Log.WriteLog("HttpWebResponse Error " + url + " content: " + content + " Exception:" + ex.ToString());
                return null;
            }

        }

        /// <summary>
        /// Http Get 请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string HttpGet(string url, int timeout = 500)
        {
            string response = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.ContentType = "application/json";
            req.Timeout = timeout;
            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();

                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    response = reader.ReadToEnd();
                }

                return response;
            }
            catch (Exception ex)
            {
                //LeeSDK.Log.WriteLog("HttpGet Error " + url + " Exception:" + ex.ToString());
                return null;
            }
        }

        // 从url获图片
        public static System.Drawing.Bitmap GetImageUrl(string url, int timeout)
        {
            if (string.IsNullOrEmpty(url))
            {
                //LeeSDK.Log.WriteLog("GetImageUrl Return Null as url empty ");
                return null;
            }
            try
            {
                System.Drawing.Bitmap img = null;

                using (var webClient = new WebClient())
                {
                    var imgdata = webClient.DownloadData(url);
                    using (System.IO.Stream stream = new MemoryStream(imgdata))
                    {
                        img = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromStream(stream);
                        return img;
                    }
                }

            }
            catch (Exception ex)
            {
                // LeeSDK.Log.WriteLog("GetImageUrl Error " + url + " Exception:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取文件夹名字
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>文件夹名字</returns>
        private static string GetFolder(string path)
        {
            string folder = GetDirectory(path);
            folder = folder.Substring(folder.LastIndexOf("/") + 1);
            return folder;
        }

        /// <summary>
        /// 获取目录名字
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>目录名</returns>
        private static string GetDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }

            return Path.GetDirectoryName(path);
        }

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>文件名</returns>
        private static string GetFileName(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }

            return Path.GetFileName(path);
        }

        /// <summary>
        /// 获取XML文件路径
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>XML文件路径</returns>
        private static string GetXmlFileName(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }

            return Path.GetFileNameWithoutExtension(path) + ".xml";
        }


    }
}
