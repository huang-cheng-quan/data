using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsFormsApp1.InferData.InferServerData;

namespace WindowsFormsApp1.InferData
{
    public class HttpQuery
    {
        // 自定义协议检测是否连接服务器
        public bool CheckConnected(string IP, int Port, int timeout = 1000)
        {
            try
            {
                var ping = new System.Net.NetworkInformation.Ping();
                var result = ping.Send(IP, timeout);
                if (result.Status != System.Net.NetworkInformation.IPStatus.Success)
                    return false;
            }
            catch (Exception ex)
            {

                return false;
            }

            return true;
        }
        // 上传样本
        public SampleResultData_NVT CommitSample(string IP, int Port, AlgoSampleCommitRequest_NVT sampleInfo, int timeout = 5000)
        {
            string rep = "";
            string prefix = "http://" + IP + ":" + Port.ToString();
            string url = prefix + "/DefectClassification";
            try
            {
                rep = Utils.HttpPost(url, JsonConvert.SerializeObject(sampleInfo), timeout);
                if (string.IsNullOrEmpty(rep))
                {
                    return null;
                }
                SampleResultData_NVT algorep = JsonConvert.DeserializeObject<SampleResultData_NVT>(rep);
                return algorep;
            }
            catch (Exception ex)
            {
                //LeeSDK.Log.WriteLog("CommitSample " + url +" "+ ex.ToString());
                return null;
            }

        }

        // 上传样本
        public SampleResultData2_NVT CommitSample2(string IP, int Port, AlgoSampleCommitRequest2_NVT sampleInfo, int timeout = 5000)
        {
            string rep = "";
            string prefix = "http://" + IP + ":" + Port.ToString();
            string url = prefix + "/sample_query";
            try
            {
                rep = Utils.HttpPost(url, JsonConvert.SerializeObject(sampleInfo), timeout);
                if (string.IsNullOrEmpty(rep))
                {
                    return null;
                }
                SampleResultData2_NVT algorep = JsonConvert.DeserializeObject<SampleResultData2_NVT>(rep);
                return algorep;
            }
            catch (Exception ex)
            {
                //LeeSDK.Log.WriteLog("CommitSample " + url +" "+ ex.ToString());
                return null;
            }

        }


        // 查询结果
        public SampleResultData_NVT QueryResult(string IP, int Port, Data_NVT_Defect_Message SampleReq, int timeout = 1000)
        {
            string rep = "";
            string prefix = "http://" + IP + ":" + Port.ToString();
            string url = prefix + "/DefectClassification";
            rep = Utils.HttpPost(url, JsonConvert.SerializeObject(SampleReq), timeout);
            if (string.IsNullOrEmpty(rep))
                return null;
            SampleResultData_NVT algorep = JsonConvert.DeserializeObject<SampleResultData_NVT>(rep);
            return algorep;
        }


    }
}

