using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WindowsFormsApp1.Models
{
    [XmlRoot(ElementName = "DayCountDataToXmlExcel")]
   public class DayCountDataToXmlExcel
    {
        [XmlArray("DayCountProductionDataIdToExl")]
        public  List<string> DayCountProductionDataIdToExl;
        [XmlArray("DayCountProductionDataResultScoreToExl")]
        public  List<float> DayCountProductionDataResultScoreToExl;
        [XmlArray("DayCountProductionDataResultCodeToExl")]
        public  List<string> DayCountProductionDataResultCodeToExl;

        [XmlArray("DayCountConnectProductionDataResultIdToXmlExl")]
        public List<string> DayCountConnectProductionDataResultIdToXmlExl;
        [XmlArray("DayCountConnectProductionDataResultCodeToXmlExl")]
        public List<string> DayCountConnectProductionDataResultCodeToXmlExl;
        [XmlArray("DayCountConnectProductionDataResultIdToXmlExl1")]
        public List<string> DayCountConnectProductionDataResultIdToXmlExl1;
        [XmlArray("DayCountConnectProductionDataResultCodeToXmlExl1")]
        public List<string> DayCountConnectProductionDataResultCodeToXmlExl1;
    }
}
