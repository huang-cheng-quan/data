using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Camera_Capture_demo.GlobalVariable;
using HslCommunication.Profinet.Omron;
using WindowsFormsApp1.Models;

namespace Camera_Capture_demo.Models
{
    [XmlRoot(ElementName = "ConfigInfo")]
    public class ConfigInfo
    {
        [XmlArray("OmronPlcs")]
        public List<OmronPlcFactory> OmronPlcs { get; set; }

        [XmlArray("calibrationData")]
        public List<CalibrationData> calibrationData { get; set; }

        [XmlElement(ElementName = "UserInfos")]
        public UserInfos UserInfos { get; set; }

        [XmlArray("JobNameList")]
        public List<JobName> JobNameList { get; set; }
        [XmlArray("DefectTypesList")]
        public List<DefectTypes> DefectTypesList { get; set; }

        [XmlElement(ElementName = "File_save_address")]
        public Save_file_Address save_file_Address { get; set; }

        [XmlElement(ElementName = "Save_Time_Period")]
        public SaveTimePeriod Save_Time_Period { get; set; }


        [XmlElement(ElementName = "Serial_port_information")]
        public Serial_port_information serial_PortInformation { get; set; }

        [XmlElement(ElementName = "Light_information")]
        public light_set_Value_config Light_information { get; set; }

        [XmlElement(ElementName = "Camera_information")]
        public Mv_Camera_parameter Camera_information { get; set; }

        [XmlElement(ElementName = "SqlServerData")]
        public SqlServerData sqldata { get; set; }

        [XmlElement(ElementName = "ProductInfos")]
        public ProductInfos ProductInfos { get; set; }
        [XmlArray("Cameras")]
        public List<MvClass> Cameras { get; set; }

        [XmlElement(ElementName = "total_image")]
        public Frame_parameter total_image { get; set; }

        [XmlElement(ElementName = "Client_data")]
        public Client_data client_Data { get; set; }
        [XmlElement(ElementName = "TCP_data")]
        public List<TCP_data> tcp_Data { get; set; }

        [XmlElement(ElementName = "ToolInfos")]
        public ToolInfos ToolInfos { get; set; }

        [XmlElement(ElementName = "sampleId")]
        public int[] sampleId { get; set; }
    }
    public class JobName
    {
        [XmlAttribute("JobNameNo")]
        public int JobNameNo { get; set; }
        [XmlElement(ElementName = "JobName")]
        public string Jobname { get; set; }

    }
    public class CalibrationData
    {
        [XmlAttribute("CameraNum")]
        public int CameraNum { get; set; }
        [XmlAttribute("MotorNum")]
        public int MotorNum { get; set; }
        [XmlElement(ElementName = "SinglePixelAccuracy")]
        public double SinglePixelAccuracy { get; set; }
        [XmlElement(ElementName = "FitCenterRow")]
        public double FitCenterRow { get; set; }
        [XmlElement(ElementName = "FitCenterColumn")]
        public double FitCenterColumn { get; set; }
        [XmlElement(ElementName = "ModelWordX")]
        public double ModelWordX { get; set; }
        [XmlElement(ElementName = "ModelWordY")]
        public double ModelWordY { get; set; }
        [XmlElement(ElementName = "ModelWordU")]
        public double ModelWordU { get; set; }

    }
    public class ProductInfos
    {
        [XmlElement(ElementName = "SelectProject")]
        public string SelectProject { get; set; }//当前选择的项目名称
    }
    public class ToolInfos
    {
        [XmlElement(ElementName = "Xoffset1")]
        public float Xoffset1 { get; set; }
        [XmlElement(ElementName = "Yoffset1")]
        public float Yoffset1 { get; set; }


        [XmlElement(ElementName = "Xoffset2")]
        public float Xoffset2 { get; set; }
        [XmlElement(ElementName = "Yoffset2")]
        public float Yoffset2 { get; set; }


        [XmlElement(ElementName = "Xoffset3")]
        public float Xoffset3 { get; set; }
        [XmlElement(ElementName = "Yoffset3")]
        public float Yoffset3 { get; set; }


        [XmlElement(ElementName = "Xoffset4")]
        public float Xoffset4 { get; set; }
        [XmlElement(ElementName = "Yoffset4")]
        public float Yoffset4 { get; set; }


        [XmlElement(ElementName = "Xoffset5")]
        public float Xoffset5 { get; set; }
        [XmlElement(ElementName = "Yoffset5")]
        public float Yoffset5 { get; set; }


        [XmlElement(ElementName = "CamPositionOnCalib")]
        public float CamPositionOnCalib { get; set; }//标定时的相机坐标

    }
    public class Client_data
    {
        [XmlAttribute("IP_Client")]
        public string IP_Client { get; set; }
        [XmlElement(ElementName = "Port_Client")]
        public int Port_Client { get; set; }
    }
    public class SaveTimePeriod 
    {
        [XmlAttribute("OriginalImagesSaveTime")]
        public int OriginalImagesSaveTime { get; set; }
        [XmlElement(ElementName = "CompressionImagesSaveTime")]
        public int CompressionImagesSaveTime { get; set; }
    }

    public class TCP_data
    {
        [XmlAttribute("Cam_Number")]
        public int Cam_Number { get; set; }
        [XmlElement("IP_Socket")]
        public string IP_Socket { get; set; }
        [XmlElement(ElementName = "Port_Socket")]
        public int Port_Socket { get; set; }

    }
    public class SqlServerData 
    {
        [XmlAttribute("server")]
        public string server { get; set; }
        [XmlElement("database")]
        public string database { get; set; }
        [XmlElement("user id")]
        public string UserId { get; set; }
        [XmlElement("password")]
        public string password { get; set; }
    }

    public class DefectTypes
    {
        [XmlAttribute("DefectTypesNo")]
        public int DefectTypesNo { get; set; }
        [XmlElement(ElementName = "DefectTypesName")]
        public string DefectTypesName { get; set; }
    }

    public class UserInfos
    {
        [XmlElement(ElementName = "Pwd1")]
        public string OperatorPwd { get; set; }
        [XmlElement(ElementName = "Pwd2")]
        public string AdministratorPwd { get; set; }
        [XmlElement(ElementName = "Pwd3")]
        public string DeveloperPwd { get; set; }
    }

    public class Save_file_Address
    {

        [XmlElement(ElementName = "file_Address")]
        public string file_Address { get; set; }
    }
    public class Serial_port_information
    {
        [XmlElement(ElementName = "PortName")]
        public string PortName { get; set; }
        [XmlElement(ElementName = "BaudRate")]
        public string BaudRate { get; set; }
        [XmlElement(ElementName = "DataBits")]
        public string DataBits { get; set; }
        [XmlElement(ElementName = "StopBits")]
        public string StopBits { get; set; }
        [XmlElement(ElementName = "Parity")]
        public string Parity { get; set; }

    }

    public class light_set_Value_config
    {
        [XmlElement(ElementName = "光源1亮度")]
        public string Light1_Value { get; set; }
        [XmlElement(ElementName = "光源2亮度")]
        public string Light2_Value { get; set; }
        [XmlElement(ElementName = "光源3亮度")]
        public string Light3_Value { get; set; }
        [XmlElement(ElementName = "光源4亮度")]
        public string Light4_Value { get; set; }
    }
    public class Mv_Camera_parameter
    {
        [XmlElement(ElementName = "ExposureTime")]
        public long currentExposureTime = 0;    // 当前曝光时间
        [XmlElement(ElementName = "currentGain")]
        public long currentGain = 0;            // 当前增益
        [XmlElement(ElementName = "CamPara")]
        public List<CamPara> CamPara { get; set; }            // 当前相机的参数
    }
    public class OmronPlcFactory
    {
        [XmlAttribute("PlcNo")]
        public int PlcNo { get; set; }//PLC序号
        [XmlElement(ElementName = "IpAddress")]
        public string IpAddr { get; set; }//IP地址
        [XmlElement(ElementName = "Port")]
        public int Port { get; set; }//端口号
        [XmlElement(ElementName = "DA1")]
        public byte DA1 { get; set; }//PLC节点号
        [XmlElement(ElementName = "SA1")]
        public byte SA1 { get; set; }//PC节点号


        private static List<OmronFinsNet> omronPlcList;
        private static readonly object locker = new object();
        public OmronPlcFactory()
        {
        }

        public static OmronFinsNet GetInstance(int plcNo)
        {
            if (omronPlcList == null)
            {
                omronPlcList = new List<OmronFinsNet>();
                for (int i = 0; i < 3; i++)
                {
                     omronPlcList.Add(null);
                    
                }
            }
            OmronPlcFactory omronPlcConfig = ConfigVars.configInfo.OmronPlcs[0];
            if (omronPlcConfig != null && (omronPlcList[plcNo] == null || TcpParamsChanged(plcNo, omronPlcConfig)))
            {
                lock (locker)
                {
                    if (omronPlcList[plcNo] == null || TcpParamsChanged(plcNo, omronPlcConfig))
                    {
                        omronPlcList[plcNo] = new OmronFinsNet(omronPlcConfig.IpAddr, omronPlcConfig.Port);
                        omronPlcList[plcNo].SA1 = omronPlcConfig.SA1;
                        omronPlcList[plcNo].DA1 = omronPlcConfig.DA1;
                    }
                }
            }
            return omronPlcList[plcNo];
        }

        private static bool TcpParamsChanged(int plcNo, OmronPlcFactory config)
        {
            if (omronPlcList != null)
            {
                return (omronPlcList[plcNo].IpAddress != config.IpAddr) || (omronPlcList[plcNo].Port != config.Port) ||
                    (omronPlcList[plcNo].SA1 != config.SA1);
            }
            return false;
        }
    }
}
public class Frame_parameter
{
    [XmlElement(ElementName = "Frame_Total")]
    public int Frame_Total = 0;    // 当前曝光时间

}
public class CamPara
{
    public string SerialNumber;
    public int CamNum;

}

