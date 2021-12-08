using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Camera_Capture_demo.GlobalVariable;
using WindowsFormsApp1.Models;

namespace Camera_Capture_demo.Models
{
    [XmlRoot(ElementName = "ConfigInfo")]
    public class ConfigInfo
    {

        [XmlElement(ElementName = "UserInfos")]
        public UserInfos UserInfos { get; set; }

        [XmlArray("JobNameList")]
        public List<JobName> JobNameList { get; set; }
        [XmlArray("DefectTypesList")]
        public List<DefectTypes> DefectTypesList { get; set; }

        [XmlElement(ElementName = "File_save_address")]
        public Save_file_Address save_file_Address { get; set; }

        [XmlElement(ElementName = "Serial_port_information")]
        public Serial_port_information serial_PortInformation { get; set; }

        [XmlElement(ElementName = "Light_information")]
        public light_set_Value_config Light_information { get; set; }

        [XmlElement(ElementName = "Camera_information")]
        public Mv_Camera_parameter Camera_information { get; set; }
        [XmlArray("Cameras")]
        public List<MvClass> Cameras { get; set; }

        [XmlElement(ElementName = "total_image")]
        public Frame_parameter total_image { get; set; }

        [XmlElement(ElementName = "Client_data")]
        public Client_data client_Data { get; set; }
        [XmlElement(ElementName = "TCP_data")]
        public TCP_data tcp_Data { get; set; }
    }
    public class JobName
    {
        [XmlAttribute("JobNameNo")]
        public int JobNameNo { get; set; }
        [XmlElement(ElementName = "JobName")]
        public string Jobname { get; set; }

    }
    public class Client_data 
    {
        [XmlAttribute("IP_Client")]
        public string IP_Client { get; set; }
        [XmlElement(ElementName = "Port_Client")]
        public int Port_Client { get; set; }
    }
    public class TCP_data
    {
        [XmlAttribute("IP_Socket")]
        public string IP_Socket { get; set; }
        [XmlElement(ElementName = "Port_Socket")]
        public int Port_Socket { get; set; }
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
}
