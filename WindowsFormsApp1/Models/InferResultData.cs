using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static WindowsFormsApp1.InferData.InferServerData;

namespace WindowsFormsApp1.Models
{
    [XmlRoot(ElementName = "InferResultData")]
    public  class InferResultData
    {
        [XmlArray("Pose_result1")]
        public  List<Data_NVT_Defect_Message> Pose_result1 { get; set; }
        [XmlArray("Pose_result2")]
        public List<Data_NVT_Defect_Message> Pose_result2 { get; set; }
        [XmlArray("Pose_result3")]
        public List<Data_NVT_Defect_Message> Pose_result3 { get; set; }
        [XmlArray("Pose_result4")]
        public List<Data_NVT_Defect_Message> Pose_result4 { get; set; }
        [XmlArray("Pose_result5")]
        public List<Data_NVT_Defect_Message> Pose_result5 { get; set; }
        [XmlArray("Pose_result6")]
        public List<Data_NVT_Defect_Message> Pose_result6 { get; set; }
        [XmlArray("Pose_result7")]
        public List<Data_NVT_Defect_Message> Pose_result7 { get; set; }
        [XmlArray("m_ProductionResultId")]
        public List<string> m_ProductionResultId { get; set; }
        [XmlArray("m_ProductionResultcode")]
        public List<string> m_ProductionResultcode { get; set; }
        [XmlArray("m_ProductionResultIdToMoveFile")]
        public List<string> m_ProductionResultIdToMoveFile { get; set; }
        [XmlArray("m_ProductionResultcodeToMoveFile")]
        public List<string> m_ProductionResultcodeToMoveFile { get; set; }
        [XmlArray("ConnectProductionDataResultIdToMoveFile")]
        public List<string> ConnectProductionDataResultIdToMoveFile { get; set; }
        [XmlArray("ConnectProductionDataResultCodeToMoveFile")]
        public List<string> ConnectProductionDataResultCodeToMoveFile { get; set; }
        [XmlArray("ConnectProductionDataResultIdToMoveFile1")]
        public List<string> ConnectProductionDataResultIdToMoveFile1 { get; set; }
        [XmlArray("ConnectProductionDataResultCodeToMoveFile1")]
        public List<string> ConnectProductionDataResultCodeToMoveFile1 { get; set; }

        [XmlArray("m_ConnectionResultId")]
        public List<string> m_ConnectionResultId { get; set; }
        [XmlArray("m_ConnectionResultcode")]
        public List<string> m_ConnectionResultcode { get; set; }

        [XmlArray("m_ConncetionResultIdToExl")]
        public List<string> m_ConncetionResultIdToExl { get; set; }

        [XmlArray("m_ConncetionResultIdTodataGridView")]
        public List<string> m_ConncetionResultIdTodataGridView { get; set; }

        [XmlArray("m_Connection1ResultId")]
        public List<string> m_Connection1ResultId { get; set; }
        [XmlArray("m_Connection1Resultcode")]
        public List<string> m_Connection1Resultcode { get; set; }

        [XmlArray("m_ConncetionResultIdToExl1")]
        public List<string> m_ConncetionResultIdToExl1 { get; set; }

        [XmlArray("m_ConncetionResultIdTodataGridView1")]
        public List<string> m_ConncetionResultIdTodataGridView1 { get; set; }





    }
}
