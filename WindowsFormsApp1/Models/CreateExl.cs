using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WindowsFormsApp1.Models
{
    [XmlRoot(ElementName = "CreateTheExl")]
   public class CreateTheExl
    {
        [XmlArray("ProductionDataIdToExl")]
        public  List<string> ProductionDataIdToExl;
        [XmlArray("ProductionDataResultToExl")]
        public  List<float> ProductionDataResultScoreToExl;
        [XmlArray("ProductionDataResultCodeToExl")]
        public  List<string> ProductionDataResultCodeToExl;

        [XmlArray("ConnectProductionDataResultIdToXmlExl")]
        public List<string> ConnectProductionDataResultIdToXmlExl;
        [XmlArray("ConnectProductionDataResultCodeToXmlExl")]
        public List<string> ConnectProductionDataResultCodeToXmlExl;
        [XmlArray("ConnectProductionDataResultIdToXmlExl1")]
        public List<string> ConnectProductionDataResultIdToXmlExl1;
        [XmlArray("ConnectProductionDataResultCodeToXmlExl1")]
        public List<string> ConnectProductionDataResultCodeToXmlExl1;
        [XmlArray("FixtureNumberToXml")]
        public List<string> FixtureNumberToXml;
    }
}
