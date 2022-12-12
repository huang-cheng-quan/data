using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WindowsFormsApp1.Models
{
    [XmlRoot(ElementName = "SnNumberInfo")]
    public class SnNumberInfo
    {
        [XmlArray("BobyQrcode1")]
        public List<string> QrcodeBoby1 { get; set; }
        [XmlArray("BobyQrcode2")]
        public List<string> QrcodeBoby2 { get; set; }
        [XmlArray("BobyQrcode3")]
        public List<string> QrcodeBoby3 { get; set; }
        [XmlArray("BobyQrcode4")]
        public List<string> QrcodeBoby4 { get; set; }
        [XmlArray("BobyQrcodeTemple")]
        public List<string> QrcodeBobyTemple { get; set; }
        [XmlArray("QrcodeConnectPRead")]
        public List<string> QrcodeConnectPRead { get; set; }
        [XmlArray("QrcodeConnectPReadTemple")]
        public List<string> QrcodeConnectPReadTemple { get; set; }

        [XmlArray("QrcodeConnectNRead")]
        public List<string> QrcodeConnectNRead;

        [XmlArray("QrcodeConnectNReadTemple")]
        public List<string> QrcodeConnectNReadTemple { get; set; }

        [XmlArray("ListNumflag1")]
        public List<int> ListNumflag1 { get; set; }
        [XmlArray("ListNumflag2")]
        public List<int> ListNumflag2 { get; set; }
        [XmlArray("ListNumflag3")]
        public List<int> ListNumflag3 { get; set; }
        [XmlArray("ListNumflag4")]
        public List<int> ListNumflag4 { get; set; }

        [XmlElement(ElementName = "numFlag")]
        public int[] numFlag { get; set; }

        [XmlElement(ElementName = "SampleIDFlag")]
        public int[] SampleIDFlag { get; set; }
        [XmlArray(ElementName = "m_listSampleIDs")]
        public List<string> m_listSampleIDs { get; set; }

        [XmlArray(ElementName = "FixtureNumber")]
        public List<string> FixtureNumber { get; set; }

        [XmlArray(ElementName = "FixtureNumberTempel")]
        public List<string> FixtureNumberTempel { get; set; }
        [XmlArray(ElementName = "FixtureNumberToMes")]
        public List<string> FixtureNumberToMes { get; set; }

    }
}
