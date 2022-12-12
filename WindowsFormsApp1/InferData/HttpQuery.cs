using Camera_Capture_demo.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static WindowsFormsApp1.InferData.InferServerData;
using BatteryFeederDemo;

namespace WindowsFormsApp1.InferData
{
    public class HttpQuery
    {
        // 自定义协议检测是否连接服务器
        public bool CheckConnected(string IP, int Port, int timeout = 5000)
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
        //检查ip端口号连接是否成功
        public bool TcpClientCheck(string ip, int port)
        {
            IPAddress ipa = IPAddress.Parse(ip);
            IPEndPoint point = new IPEndPoint(ipa, port);
            TcpClient tcp = null;
            try
            {
                tcp = new TcpClient();
                tcp.Connect(point);
                return true;
            }
            catch (Exception ex)
            {              
                return false;
            }
            finally
            {
                if (tcp != null)
                {
                    tcp.Close();
                }
            }
        }
        // 上传样本
        public SampleResultData_NVT CommitSample(string IP, int Port, AlgoSampleCommitRequest_NVT sampleInfo, int timeout = 30000)
        {
            string rep = "";
            string prefix = "http://" + IP + ":" + Port.ToString();
            string url = prefix + "/DefectClassification";
            try
            {
                
                string js=JsonConvert.SerializeObject(sampleInfo);
                //js
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
            GC.Collect();

        }

        public SampleResultData_NVT CommitSample00(string IP, int Port, AlgoSampleCommitRequest_NVT sampleInfo, int timeout = 30000)
        {
            string rep = "";
            string prefix = "http://" + IP + ":" + Port.ToString();
            string url = prefix + "/DefectClassification";
            string js="";
            try
            {
                //Stopwatch st = new Stopwatch();
                //st.Start();
                lock (MotionProcess.JsonConvertLock)
                {
                    js = JsonConvert.SerializeObject(sampleInfo);
                }
                 
                //st.Stop();
                //LogHelper.LogInfo("JsonConvert"+st.ElapsedMilliseconds.ToString());
                //js
                rep = Utils.HttpPost00(url, js, timeout);
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
                LogHelper.LogError("CommitSample00 "+js);
                return null;
            }
            GC.Collect();

        }

        public SampleResultData_NVT CommitSample01(string IP, int Port, AlgoSampleCommitRequest_NVT sampleInfo, int timeout = 30000)
        {
            string rep = "";
            string prefix = "http://" + IP + ":" + Port.ToString();
            string url = prefix + "/DefectClassification";
            string js = "";
            try
            {
                lock (MotionProcess.JsonConvertLock)
                {
                    js = JsonConvert.SerializeObject(sampleInfo);
                }
                 
                //js
                rep = Utils.HttpPost01(url, js, timeout);
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
                LogHelper.LogError("CommitSample01 "+js);
                return null;
            }
            GC.Collect();

        }

        public SampleResultData_NVT CommitSample02(string IP, int Port, AlgoSampleCommitRequest_NVT sampleInfo, int timeout = 30000)
        {
            string rep = "";
            string prefix = "http://" + IP + ":" + Port.ToString();
            string url = prefix + "/DefectClassification";
            string js = "";
            try
            {
                lock (MotionProcess.JsonConvertLock)
                {
                    js = JsonConvert.SerializeObject(sampleInfo);
                }
           
                //js
                rep = Utils.HttpPost02(url, js, timeout);
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
                LogHelper.LogError("CommitSample02 "+js);
                return null;
            }
            GC.Collect();

        }

        // 上传样本
        public SampleResultData2_NVT CommitSample2(string IP, int Port, AlgoSampleCommitRequest2_NVT sampleInfo, int timeout = 30000)
        {
            string rep = "";
            string prefix = "http://" + IP + ":" + Port.ToString();
            string url = prefix + "/sample_query";
            string js = "";
            try
            {
                lock (MotionProcess.JsonConvertLock)
                {
                     js = JsonConvert.SerializeObject(sampleInfo);
                }
                
                rep = Utils.HttpPost(url, js, timeout);
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
            GC.Collect();

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

