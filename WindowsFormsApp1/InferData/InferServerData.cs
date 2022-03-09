using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.InferData
{
   public  class InferServerData
    {
        public class AlgoSampleCommitRequest_NVT
        {
            /// <summary>
            /// 文件名列表
            /// </summary>
            public List<string> file_names;

            /// <summary>
            /// 相对路径
            /// </summary>
            public string relative_dir; // 相对路径

        }
        /// <summary>
        /// 接受服务器信息
        /// </summary>
        public class SampleResultData_NVT
        {
            /// <summary>
            /// 生成的唯一ID
            /// </summary>
            public string request_id;
            /// <summary>
            /// 返回的响应码
            /// </summary>
            public int code;
            /// <summary>
            /// 返回的响应码说明
            /// </summary>
            //public Dictionary<string, string> msg; //images_result.txt "A/PC3XXX/10/S00001_XXX.jpg\tLW"
            public string msg; //images_result.txt "A/PC3XXX/10/S00001_XXX.jpg\tLW"
            /// <summary>
            /// data信息
            /// </summary>
            public Data_NVT_Defect_Message data;
        }
        public class Data_NVT_Defect_Message
        {
            /// <summary>
            /// 发送的图片路径
            /// </summary>
            public string file_path;
            /// <summary>
            /// 结果图像路径
            /// </summary>
            public string result_path;
            /// <summary>
            /// 图片的宽度
            /// </summary>
            public int image_width;
            /// <summary>
            /// 图片的高度
            /// </summary>
            public int image_height;
            /// <summary>
            /// 图片中是否含有产品
            /// </summary>
            public string is_empty;
            /// <summary>
            /// 缺陷的位置信息和得分信息
            /// </summary>
            public List<Defect_Mask_message> res;
            public List<float> ak_lens;

        }
        public class Defect_Mask_message
        {
            public int[] bbox;
            public int[] polygon;
            public string code;
            public double score;
            public double length;
            public long area;

        }
        public class AlgoSampleCommitRequest2_NVT
        {
            public string sample_id;

            public List<Data_NVT_Defect_Message> pose_result;
        }
        public class SampleResultData2_NVT
        {
            /// <summary>
            /// 生成的唯一ID
            /// </summary>
            public string request_id;
            /// <summary>
            /// 返回的响应码
            /// </summary>
            public int code;
            /// <summary>
            /// 返回的响应码说明
            /// </summary>
            //public Dictionary<string, string> msg; //images_result.txt "A/PC3XXX/10/S00001_XXX.jpg\tLW"
            public string msg; //images_result.txt "A/PC3XXX/10/S00001_XXX.jpg\tLW"
            /// <summary>
            /// data信息
            /// </summary>
            public Sample_NVT_data2_Message data;
        }
        public class Sample_NVT_data2_Message
        {
            public string sample_id;
            public string code;
            public float score;
        }
    }
}
